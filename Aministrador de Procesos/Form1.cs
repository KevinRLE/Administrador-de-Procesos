using AdministradorProcesos.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AdministradorProcesos.Models;

namespace AdministradorProcesos
{
    public partial class Form1 : Form
    {
        private readonly ProcessService _processService;
        private readonly ProcessOperationsService _processOperationsService;

        // Boris: Servicio de métricas y datos para los gráficos de CPU
        private readonly MetricsService _metricsService;
        private float _cpuUsageActual = 0;
        private List<float> _cpuHistory = new List<float>();

        //Boris color d la barra de RAM
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public Form1()
        {
            InitializeComponent();

            _processService = new ProcessService();
            _processOperationsService = new ProcessOperationsService(); //ServicioAgregado
            _metricsService = new MetricsService();                     // [Boris]

            //Agregado Detectar clic derecho en una fila
            dgvProcesos.CellMouseDown += dgvProcesos_CellMouseDown;

            // [Astrid] AGREGADO: aplica el estilo visual y deja el botón Pausar/Reanudar
            // en su estado inicial. El método está en Form1.Interfaz.cs.
            InicializarInterfaz();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarProcesos();
        }


        //En este si modifiqué lo de boris para que se pudiera ejecutar lo mio
        // [Astrid] MODIFICADO: guarda la lista completa y delega en AplicarFiltro()
        // (Form1.Interfaz.cs), que respeta el texto de la barra de búsqueda aunque el
        // Timer refresque los datos cada 3 segundos.

        //Explicación: Lo de boris guardaba la selección por número de fila y reasignaba el DataSource.
        //El AplicarFiltro(), guarda el filtro y no se pierde 
        private void CargarProcesos()
        {
            // Obtener la nueva lista de procesos y guardarla completa (sin filtrar)
            _listaCompleta = _processService.GetActiveProcesses();

            // Mostrar en la tabla solo lo que coincide con la búsqueda actual
            AplicarFiltro();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarProcesos();
        }

        // Timer de la TABLA: es el que controla el botón Pausar/Reanudar (Form1.Interfaz.cs)
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Refresco automático de la tabla
            CargarProcesos();
        }

        //Este lo agregué para que no se detuvieran las gráficar d boris, llama a ActualizarMetricas
        // [Boris] Timer de las MÉTRICAS: independiente, así al pausar la tabla
        // el reloj de CPU, la gráfica y la barra de RAM siguen en vivo.
        private void timerMetricas_Tick(object sender, EventArgs e)
        {
            ActualizarMetricas();
        }

        //Este lo reemplacé por el timer1_tick de antes, todo el código d boris sigue igual solo que ahora solo llama a CargarProcesos()
        // [Boris] Obtiene las métricas y actualiza barra de RAM, reloj y gráfica de CPU
        private void ActualizarMetricas()
        {
            // 1. Obtener métricas
            float cpuUsage = _metricsService.GetCpuUsage();
            float availableRam = _metricsService.GetAvailableRam();
            double totalRam = _metricsService.GetTotalRam();
            float ramPercentage = _metricsService.GetUsedRamPercentage();

            double usedRamGB = (totalRam - availableRam) / 1024.0;
            double totalRamGB = totalRam / 1024.0;

            // 2. RAM visual (Math.Clamp evita que la barra lance excepción fuera de 0-100)
            lblRam.Text = $"{usedRamGB:F1} GB / {totalRamGB:F0} GB";
            pbRam.Value = Math.Clamp((int)Math.Round(ramPercentage), 0, 100);

            // Color de la barra: 1 = verde, 3 = amarillo, 2 = rojo
            int estadoRam = 1;
            if (ramPercentage > 85) estadoRam = 2;
            else if (ramPercentage > 60) estadoRam = 3;
            SendMessage(pbRam.Handle, 1040, (IntPtr)estadoRam, IntPtr.Zero);

            // 3. CPU visual (reloj y gráfica)
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
                // !!!! la columna del PID sigue llamándose "Id" (ver Designer)
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

        // ------------------------------------------------------------------
        // Boris: Dibujo del reloj circular de CPU
        // ------------------------------------------------------------------
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

        // ------------------------------------------------------------------
        // Boris: Dibujo de la gráfica de historial de CPU
        // ------------------------------------------------------------------
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
    }
}