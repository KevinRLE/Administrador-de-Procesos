//Creado por Britany Mishel Hernandez Davila 9959-24-4178
//ClaseParcial del form1 para el funcionamiento del contextMenuProcesos en la tabla de procesos
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AdministradorProcesos
{
    public partial class Form1
    {
        //--MENU de prioridades
        //Metodo para seleccionar la fila al hacer click derecho
        private void dgvProcesos_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Comprobar que sea clic derecho sobre una fila valida
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //Seleccion de la fila en la que se dio el click
                dgvProcesos.ClearSelection(); //Limpia la seccion anterior
                dgvProcesos.Rows[e.RowIndex].Selected = true;

                //Restablecer la fila como actual
                dgvProcesos.CurrentCell = dgvProcesos.Rows[e.RowIndex].Cells[e.ColumnIndex];

            }
        }

        //Obtencion del proceso seleccionado
        private int ObtenerProcessIDSeleccionado()
        {
            //Verificacion de fila seleccionada
            if (dgvProcesos.CurrentRow == null) return -1;

            // Buscar por índice (la columna ID es la primera, índice 0)
            //Verificacion del valor de la celda
            if (dgvProcesos.CurrentRow.Cells[0].Value == null) return -1;

            //Conversion del valor de la celda a entero
            if (int.TryParse(dgvProcesos.CurrentRow.Cells[0].Value.ToString(), out int processId))
            {
                return processId;
            }

            return -1;
        }

        //Cambiar la prioridad
        private void CambiarPrioridad(ProcessPriorityClass prioridad)
        {
            //Obtencion de la ID del proceso 
            int processId = ObtenerProcessIDSeleccionado();

            //Verificacion de proceso seleccionado
            if (processId == -1)
            {
                MessageBox.Show("Selecciona un proceso primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_processOperationsService.CambiarPrioridad(processId, prioridad))
            {
                MessageBox.Show("La prioridad se cambió correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarProcesos();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo cambiar la prioridad Es posible que no tengas permisos suficientes.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Metodos submenu de prioridades
        //Prioridad en tiempo real 
        private void menuTiempoReal_Click(object sender, EventArgs e)
        {
            CambiarPrioridad(ProcessPriorityClass.RealTime);
        }

        //Prioridad alta
        private void menuAlta_Click(object sender, EventArgs e)
        {
            CambiarPrioridad(ProcessPriorityClass.High);
        }

        //Prioridad normal
        private void menuNormal_Click(object sender, EventArgs e) { CambiarPrioridad(ProcessPriorityClass.Normal); }

        //Prioridad baja
        private void menuBaja_Click(object sender, EventArgs e)
        { CambiarPrioridad(ProcessPriorityClass.Idle); }


        //Abrir ubicacion del archivo
        private void menuAbrirUbicacion_Click(object sender, EventArgs e)
        {
            int processId = ObtenerProcessIDSeleccionado();

            if (processId == -1)
            {
                MessageBox.Show("Selecciona un proceso primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool resultado = _processOperationsService.AbrirUbicacionArchivo(processId);

            if (!resultado)
            {
                MessageBox.Show("No se pudo abrir la ubicación del archivo. Es posible que no tengas permisos suficientes "
                    + "o que el proceso ya haya finalizado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}