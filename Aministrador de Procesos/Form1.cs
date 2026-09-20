using System;
using System.Windows.Forms;
using AdministradorProcesos.Services;

namespace AdministradorProcesos
{
    public partial class Form1 : Form
    {
        private readonly ProcessService _processService;
        private readonly MetricsService _metricsService;
        private float _cpuUsageActual = 0;
        private List<float> _cpuHistory = new List<float>();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public Form1()
        {
            InitializeComponent();
            _processService = new ProcessService();
            _metricsService = new MetricsService();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarProcesos();
        }

        private void CargarProcesos()
        {
            // Guardar la fila seleccionada y la posición del scroll
            int indexFilaSeleccionada = -1;
            if (dgvProcesos.CurrentRow != null)
            {
                indexFilaSeleccionada = dgvProcesos.CurrentRow.Index;
            }

            int primerIndiceVisible = dgvProcesos.FirstDisplayedScrollingRowIndex;

            // Obtener la nueva lista de procesos
            var listaProcesos = _processService.GetActiveProcesses();

            //Refrescar los datos
            dgvProcesos.DataSource = null;
            dgvProcesos.DataSource = listaProcesos;

            // Restaura Posicion
            if (primerIndiceVisible >= 0 && primerIndiceVisible < dgvProcesos.RowCount)
            {
                dgvProcesos.FirstDisplayedScrollingRowIndex = primerIndiceVisible;
            }

            // Restaurar la fila seleccionada
            if (indexFilaSeleccionada >= 0 && indexFilaSeleccionada < dgvProcesos.RowCount)
            {
                dgvProcesos.Rows[indexFilaSeleccionada].Selected = true;
                // no null
                dgvProcesos.CurrentCell = dgvProcesos.Rows[indexFilaSeleccionada].Cells[0];
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarProcesos();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Refresco automático de la tabla
            CargarProcesos();
            // 1. Obtener métricas
            float cpuUsage = _metricsService.GetCpuUsage();
            float availableRam = _metricsService.GetAvailableRam();
            double totalRam = _metricsService.GetTotalRam();
            float ramPercentage = _metricsService.GetUsedRamPercentage();

            double usedRamGB = (totalRam - availableRam) / 1024.0;
            double totalRamGB = totalRam / 1024.0;

            // 2. RAM Visual
            lblRam.Text = $"{usedRamGB:F1} GB / {totalRamGB:F0} GB";
            pbRam.Value = (int)Math.Round(ramPercentage);

            int estadoRam = 1;
            if (ramPercentage > 85) estadoRam = 2;
            else if (ramPercentage > 60) estadoRam = 3;
            SendMessage(pbRam.Handle, 1040, (IntPtr)estadoRam, IntPtr.Zero);

            // 3. CPU Visual (Reloj y Gráfica)
            _cpuUsageActual = cpuUsage;
            picCpu.Invalidate();

            _cpuHistory.Add(cpuUsage);
            if (_cpuHistory.Count > 60) _cpuHistory.RemoveAt(0);
            picChartCpu.Invalidate();
        }

        private void btnTerminar_Click(object sender, EventArgs e)
        {
            if (dgvProcesos.CurrentRow != null)
            {
                int processId = Convert.ToInt32(dgvProcesos.CurrentRow.Cells["Id"].Value);

                if (_processService.KillProcess(processId))
                {
                    MessageBox.Show("Proceso finalizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarProcesos();
                }
                else
                {
                    MessageBox.Show("No se pudo finalizar el proceso (permisos insuficientes o ya finalizó).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void picCpu_Paint(object sender, PaintEventArgs e)
        {
            // Mejorar la calidad del dibujo para que no se vea pixelado
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Grosor de la barra circular
            int grosor = 15;
            Rectangle rect = new Rectangle(grosor, grosor, picCpu.Width - (grosor * 2), picCpu.Height - (grosor * 2));

            // 1. Dibujar el círculo de fondo (color gris claro)
            using (Pen penFondo = new Pen(Color.LightGray, grosor))
            {
                e.Graphics.DrawArc(penFondo, rect, 0, 360);
            }

            // 2. Dibujar el progreso del CPU (color azul)
            float grados = (_cpuUsageActual / 100f) * 360f;
            using (Pen penCpu = new Pen(Color.FromArgb(0, 122, 204), grosor))
            {
                penCpu.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                penCpu.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                e.Graphics.DrawArc(penCpu, rect, -90, grados);
            }

            // 3. Dibujar los textos (CPU y Porcentaje) en el centro
            string titulo = "CPU";
            string texto = $"{Math.Round(_cpuUsageActual)}%";

            using (Font fuenteTitulo = new Font("Segoe UI", 11, FontStyle.Regular))
            using (Font fuentePorcentaje = new Font("Segoe UI", 18, FontStyle.Bold))
            using (SolidBrush brocha = new SolidBrush(Color.Black))
            {
                SizeF tamañoTitulo = e.Graphics.MeasureString(titulo, fuenteTitulo);
                SizeF tamañoTexto = e.Graphics.MeasureString(texto, fuentePorcentaje);

                PointF posicionTitulo = new PointF(
                    (picCpu.Width - tamañoTitulo.Width) / 2,
                    (picCpu.Height / 2) - tamañoTitulo.Height + 5);

                PointF posicionTexto = new PointF(
                    (picCpu.Width - tamañoTexto.Width) / 2,
                    (picCpu.Height / 2) + 2);

                e.Graphics.DrawString(titulo, fuenteTitulo, brocha, posicionTitulo);
                e.Graphics.DrawString(texto, fuentePorcentaje, brocha, posicionTexto);
            }
        }

        private void picChartCpu_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // 1. PINTAR FONDO Y CUADRÍCULA
            e.Graphics.Clear(Color.Black);

            using (Pen penGrid = new Pen(Color.FromArgb(40, 255, 255, 255), 1))
            {
                for (int i = 0; i < 5; i++)
                {
                    int y = i * (picChartCpu.Height / 4);
                    e.Graphics.DrawLine(penGrid, 0, y, picChartCpu.Width, y);
                }
                for (int i = 0; i < 10; i++)
                {
                    int x = i * (picChartCpu.Width / 9);
                    e.Graphics.DrawLine(penGrid, x, 0, x, picChartCpu.Height);
                }
            }

            // 2. DIBUJAR LA LÍNEA DEL HISTORIAL DE CPU
            if (_cpuHistory.Count < 2) return;

            float anchoPaso = (float)picChartCpu.Width / 60f;

            using (Pen penLinea = new Pen(Color.FromArgb(0, 122, 204), 2))
            {
                for (int i = 0; i < _cpuHistory.Count - 1; i++)
                {
                    float x1 = i * anchoPaso;
                    float y1 = picChartCpu.Height - ((_cpuHistory[i] / 100f) * picChartCpu.Height);

                    float x2 = (i + 1) * anchoPaso;
                    float y2 = picChartCpu.Height - ((_cpuHistory[i + 1] / 100f) * picChartCpu.Height);

                    e.Graphics.DrawLine(penLinea, x1, y1, x2, y2);
                }
            }
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
    }
}