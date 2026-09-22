//Creado por Astrid Fernanda Ruíz López - 9959 24 2976
//ClaseParcial del form1 para el funcionamiento del filtrado de tabla, botón reanudar/pausar y 
using AdministradorProcesos.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using System.Windows.Forms;

namespace AdministradorProcesos
{
    //Interfaz de Usuario & Filtros

    //  Trabajo asignado d #2:
    //    1) Barra d búsqueda en tiempo real  -> txtBuscar_TextChanged / AplicarFiltro / FiltrarProcesos
    //    2) Botón btnPausar que detiene o reanuda el Timer   -> btnPausar_Click / ActualizarEstadoPausa
    //    3) Estilo visual y alertas  -> AplicarEstilo / dgvProcesos_CellFormatting / ActualizarContadores
    // =====================================================================================
    public partial class Form1
    {

        //variables

        /// Texto exacto que ProcessService.GetActiveProcesses() pone en la propiedad Status
        /// cuando un proceso está congelado (p.Responding == false). Se usa como constante
        /// para no repetir el string para no repetirlo en el codigo
        private const string EstadoNoResponde = "No responde";


        /// Lista COMPLETA de procesos  tal como llegaron del sistema, sin filtrar.
        /// El filtro siempre parte de esta lista, así, al borrar
        /// texto de la búsqueda, los procesos vuelven a aparecer sin volver a consultar al sistema.
        private List<ProcessModel> _listaCompleta = new List<ProcessModel>();

  
        /// Lista que se está MOSTRANDO actualmente en el DataGridView (resultado del filtro).
        /// Como las columnas no se pueden ordenar (SortMode = NotSortable), el índice de fila
        /// del grid coincide siempre con el índice de esta lista. Eso permite consultar el
        /// proceso de una fila con _listaMostrada[indice] sin tocar el grid (más rápido).
        private List<ProcessModel> _listaMostrada = new List<ProcessModel>();

        //Paleta de colores 
        private static readonly Color ColorFondoForm = Color.FromArgb(241, 245, 249); // gris muy claro
        private static readonly Color ColorEncabezado = Color.FromArgb(30, 41, 59);   // azul oscuro
        private static readonly Color ColorPrimario = Color.FromArgb(37, 99, 235);    // azul (selección/botón)
        private static readonly Color ColorAdvertencia = Color.FromArgb(217, 119, 6); // ámbar (botón Pausar)
        private static readonly Color ColorExito = Color.FromArgb(22, 163, 74);       // verde (botón Reanudar)
        private static readonly Color ColorPeligro = Color.FromArgb(220, 38, 38);     // rojo (Terminar proceso)
        private static readonly Color ColorTextoPrincipal = Color.FromArgb(15, 23, 42);
        private static readonly Color ColorTextoSecundario = Color.FromArgb(100, 116, 139);

        // ---- Colores de ALERTA para procesos que "No responden" ----
        private static readonly Color ColorAlertaFondo = Color.FromArgb(254, 226, 226);    // rojo muy claro
        private static readonly Color ColorAlertaTexto = Color.FromArgb(153, 27, 27);      // rojo oscuro
        private static readonly Color ColorAlertaSeleccion = Color.FromArgb(185, 28, 28);  // rojo fuerte

        // ---- Fuentes 
        private readonly Font _fuenteNormal = new Font("Segoe UI", 10F);
        private readonly Font _fuenteNegrita = new Font("Segoe UI", 10F, FontStyle.Bold);
        private readonly Font _fuenteTitulo = new Font("Segoe UI Semibold", 16F);

        // ---------------------------------------------------------------------------------
        //  INICIALIZACIÓN
        /// Se llama UNA vez desde el constructor de Form1 (después de InitializeComponent).
        /// Aplica el estilo visual, deja el botón de pausa en su estado inicial y
        /// se encarga de liberar las fuentes cuando el formulario se cierra.
        private void InicializarInterfaz()
        {
            AplicarEstilo();
            ActualizarEstadoPausa();

            FormClosed += (s, e) =>
            {
                _fuenteNormal.Dispose();
                _fuenteNegrita.Dispose();
                _fuenteTitulo.Dispose();
            };
        }

        
        //  1) BÚSQUEDA EN TIEMPO REAL
        // ---------------------------------------------------------------------------------

        /// Evento TextChanged del TextBox de búsqueda. Windows Forms lo dispara CADA VEZ que
        /// el texto cambia (al escribir, borrar o pegar), por eso el filtrado es "en tiempo real":
        /// no hay botón "Buscar", la tabla se actualiza con cada tecla.

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        /// Devuelve solo los procesos que coinciden con el texto buscado.
        /// - Busca por NOMBRE o por PID (Id) para que el usuario pueda escribir "chrome" o "1234".
        /// - No distingue mayúsculas de minúsculas (StringComparison.OrdinalIgnoreCase).
        /// - Si el texto está vacío devuelve la lista completa.
        private List<ProcessModel> FiltrarProcesos(string texto)
        {
            texto = texto.Trim();

            if (texto.Length == 0)
            {
                return _listaCompleta;
            }

            // LINQ: Where() se queda con los elementos que cumplen la condición.
            return _listaCompleta
                .Where(p => p.Name.Contains(texto, StringComparison.OrdinalIgnoreCase)
                         || p.Id.ToString().Contains(texto))
                .ToList();
        }

      
        /// Método q:toma _listaCompleta, le aplica el filtro actual y
        /// muestra el resultado en el DataGridView. Se ejecuta en dos situaciones:
        ///   a) cuando el usuario escribe en la búsqueda (txtBuscar_TextChanged), y
        ///   b) cada vez que se refresca la lista de procesos (CargarProcesos en Form1.cs).
        /// Por eso el filtro NO se pierde cuando el Timer actualiza los datos cada 3 segundos.
       
        private void AplicarFiltro()
        {
            // 1) Recordar el PID del proceso seleccionado y la posición del scroll.
            int? pidSeleccionado = null;
            if (dgvProcesos.CurrentRow != null && dgvProcesos.CurrentRow.Index < _listaMostrada.Count)
            {
                pidSeleccionado = _listaMostrada[dgvProcesos.CurrentRow.Index].Id;
            }
            int primerIndiceVisible = dgvProcesos.FirstDisplayedScrollingRowIndex;

            // 2) Calcular la lista filtrada.
            _listaMostrada = FiltrarProcesos(txtBuscar.Text);

            // 3) Volver a enlazar el grid con la lista (null primero para forzar el refresco).
            dgvProcesos.DataSource = null;
            dgvProcesos.DataSource = _listaMostrada;

            // 4) Restaurar selección y scroll.
            if (pidSeleccionado.HasValue)
            {
                SeleccionarFilaPorPid(pidSeleccionado.Value);
            }
            if (primerIndiceVisible >= 0 && primerIndiceVisible < dgvProcesos.RowCount)
            {
                dgvProcesos.FirstDisplayedScrollingRowIndex = primerIndiceVisible;
            }

            // 5) Actualizar los contadores de la barra de estado.
            ActualizarContadores();
        }

     
        /// Busca en la lista mostrada el proceso con el PID indicado y selecciona su fila.
        /// Si ese proceso ya no existe (terminó) o quedó fuera del filtro, no hace nada
        /// y el DataGridView deja seleccionada la primera fila por defecto.
        private void SeleccionarFilaPorPid(int pid)
        {
            int indice = _listaMostrada.FindIndex(p => p.Id == pid);
            if (indice < 0)
            {
                return;
            }

            dgvProcesos.ClearSelection();
            dgvProcesos.Rows[indice].Selected = true;
            dgvProcesos.CurrentCell = dgvProcesos.Rows[indice].Cells[0];
        }

        
        //  2) CONTROL  (PAUSAR / REANUDAR)
        ///  - Si el Timer estaba activo lo detiene  -> la tabla deja de actualizarse sola.
        ///  - Si estaba detenido lo activa          -> vuelve el refresco automático.
        /// La propiedad Timer.Enabled equivale a Start()/Stop().
        /// Pausar es útil para "congelar" la tabla y revisar un proceso con calma.
        /// El botón "Actualizar" (manual) sigue funcionando aunque esté en pausa.
        private void btnPausar_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            ActualizarEstadoPausa();

            // Al reanudar se refresca de inmediato, sin esperar los 3 segundos del siguiente Tick.
            if (timer1.Enabled)
            {
                CargarProcesos();
            }
        }

  
        /// Sincroniza la interfaz con el estado actual del Timer: cambia el texto y el color
        /// del botón (ámbar "Pausar" / verde "Reanudar") y el mensaje de la barra de estado,
        /// que se pone en rojo cuando la actualización está pausada para que sea evidente.
        private void ActualizarEstadoPausa()
        {
            bool activo = timer1.Enabled;

            btnPausar.Text = activo ? "Pausar" : "Reanudar";
            btnPausar.BackColor = activo ? ColorAdvertencia : ColorExito;

            lblActualizacion.Text = activo
                ? $"Actualización automática: cada {timer1.Interval / 1000} s"
                : "Actualización automática: PAUSADA";
            lblActualizacion.ForeColor = activo ? ColorTextoSecundario : ColorAlertaTexto;
            lblActualizacion.Font = activo ? _fuenteNormal : _fuenteNegrita;
        }

        // ---------------------------------------------------------------------------------
        //  3) ESTILO VISUAL Y ESTADOS DE ALERTA
       
        /// Las posiciones y tamaños de los controles están en el Designer;
        /// aquí solo va la parte "estética", para que sea fácil de cambiar en un solo sitio.
        private void AplicarEstilo()
        {
            // ----- Formulario -----
            BackColor = ColorFondoForm;

            // ----- Panel superior (título + búsqueda) -----
            pnlSuperior.BackColor = ColorEncabezado;
            lblTitulo.Font = _fuenteTitulo;
            lblTitulo.ForeColor = Color.White;

            txtBuscar.Font = _fuenteNormal;
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.BackColor = Color.White;
            txtBuscar.ForeColor = ColorTextoPrincipal;

            // ----- Panel inferior (botones) -----
            pnlInferior.BackColor = ColorFondoForm;
            EstilizarBoton(btnPausar, ColorAdvertencia);
            EstilizarBoton(btnActualizar, ColorPrimario);
            EstilizarBoton(btnTerminar, ColorPeligro);

            // ----- Barra de estado -----
            statusStrip1.BackColor = Color.White;
            foreach (ToolStripItem item in statusStrip1.Items)
            {
                item.Font = _fuenteNormal;
                item.ForeColor = ColorTextoSecundario;
            }

            // ----- Panel de monitoreo (CPU / RAM) -----
            pnlMonitor.BackColor = ColorFondoForm;
            picCpu.BackColor = ColorFondoForm;
            label2.Font = _fuenteNegrita;
            label2.ForeColor = ColorTextoPrincipal;
            label1.Font = _fuenteNormal;
            label1.ForeColor = ColorTextoSecundario;
            lblRam.Font = _fuenteNegrita;
            lblRam.ForeColor = ColorTextoPrincipal;
            label3.Font = _fuenteNormal;
            label3.ForeColor = ColorTextoSecundario;

            // ----- Tabla de procesos -----
            AplicarEstiloTabla();
        }

        private void EstilizarBoton(Button boton, Color colorFondo)
        {
            boton.FlatStyle = FlatStyle.Flat;              
            boton.FlatAppearance.BorderSize = 0;           // sin borde
            boton.UseVisualStyleBackColor = false;
            boton.BackColor = colorFondo;
            boton.ForeColor = Color.White;
            boton.Font = _fuenteNegrita;
            boton.Cursor = Cursors.Hand;
        }

        private void AplicarEstiloTabla()
        {
            // Apariencia general
            dgvProcesos.BackgroundColor = Color.White;
            dgvProcesos.BorderStyle = BorderStyle.None;
            dgvProcesos.GridColor = Color.FromArgb(226, 232, 240);
            dgvProcesos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // solo líneas horizontales
            dgvProcesos.RowHeadersVisible = false;                                       // quita la columna vacía de la izquierda

            // Comportamiento: la tabla es solo de lectura y se selecciona la fila completa.
            dgvProcesos.ReadOnly = true;
            dgvProcesos.AllowUserToAddRows = false;
            dgvProcesos.AllowUserToDeleteRows = false;
            dgvProcesos.AllowUserToResizeRows = false;
            dgvProcesos.MultiSelect = false;
            dgvProcesos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Encabezado de columnas. EnableHeadersVisualStyles = false es OBLIGATORIO
            // para que Windows respete los colores personalizados del encabezado.
            dgvProcesos.EnableHeadersVisualStyles = false;
            dgvProcesos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvProcesos.ColumnHeadersDefaultCellStyle.BackColor = ColorEncabezado;
            dgvProcesos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProcesos.ColumnHeadersDefaultCellStyle.Font = _fuenteNegrita;
            dgvProcesos.ColumnHeadersDefaultCellStyle.SelectionBackColor = ColorEncabezado;
            dgvProcesos.ColumnHeadersDefaultCellStyle.Padding = new Padding(6);

            // Filas normales, filas alternas (efecto "cebra") y color de selección.
            dgvProcesos.DefaultCellStyle.BackColor = Color.White;
            dgvProcesos.DefaultCellStyle.ForeColor = ColorTextoPrincipal;
            dgvProcesos.DefaultCellStyle.Font = _fuenteNormal;
            dgvProcesos.DefaultCellStyle.SelectionBackColor = ColorPrimario;
            dgvProcesos.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvProcesos.DefaultCellStyle.Padding = new Padding(4, 2, 4, 2);
            dgvProcesos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            // Alineación: PID centrado, memoria a la derecha (como en los números).
            colId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMemoria.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colPrioridad.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn columna in dgvProcesos.Columns)
            {
                columna.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

       // ESTADO DE ALERTA: si el proceso de esa fila está en estado
        /// "No responde", toda la fila se pinta en rojo 
  
        private void dgvProcesos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // RowIndex < 0 es la fila del encabezado: no se le aplica alerta.
            if (e.RowIndex < 0 || e.RowIndex >= _listaMostrada.Count || e.CellStyle == null)
            {
                return;
            }

            if (_listaMostrada[e.RowIndex].Status == EstadoNoResponde)
            {
                e.CellStyle.BackColor = ColorAlertaFondo;
                e.CellStyle.ForeColor = ColorAlertaTexto;
                e.CellStyle.Font = _fuenteNegrita;
                e.CellStyle.SelectionBackColor = ColorAlertaSeleccion;
                e.CellStyle.SelectionForeColor = Color.White;
            }
        }

        /// Actualiza la barra de estado inferior con:
        ///  - Cuántos procesos hay (o "Mostrando X de Y" si hay un filtro activo).
        ///  - Cuántos procesos NO RESPONDEN: en rojo y negrita si hay alguno, en verde si no hay.

        private void ActualizarContadores()
        {
            int total = _listaCompleta.Count;
            int mostrados = _listaMostrada.Count;
            int noResponden = _listaCompleta.Count(p => p.Status == EstadoNoResponde);

            lblTotal.Text = (mostrados == total)
                ? $"Procesos: {total}"
                : $"Mostrando {mostrados} de {total} procesos";

            lblNoResponden.Text = $"No responden: {noResponden}";
            if (noResponden > 0)
            {
                lblNoResponden.ForeColor = ColorAlertaTexto;
                lblNoResponden.Font = _fuenteNegrita;
            }
            else
            {
                lblNoResponden.ForeColor = ColorExito;
                lblNoResponden.Font = _fuenteNormal;
            }
        }
    }
}