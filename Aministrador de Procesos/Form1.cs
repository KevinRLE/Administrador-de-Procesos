using System;
using System.Windows.Forms;
using AdministradorProcesos.Services;

namespace AdministradorProcesos
{
    public partial class Form1 : Form
    {
        private readonly ProcessService _processService;

        public Form1()
        {
            InitializeComponent();
            _processService = new ProcessService();
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
    }
}