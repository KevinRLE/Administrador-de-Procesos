using System.Windows.Forms;
using System.Drawing;

namespace AdministradorProcesos
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlSuperior = new Panel(); //a2
            txtBuscar = new TextBox(); //a2
            lblTitulo = new Label(); //a2
            dgvProcesos = new DataGridView(); //a2
            colId = new DataGridViewTextBoxColumn(); //a2
            colNombre = new DataGridViewTextBoxColumn();//a2
            colMemoria = new DataGridViewTextBoxColumn();//a2
            colPrioridad = new DataGridViewTextBoxColumn();//a2
            colEstado = new DataGridViewTextBoxColumn();//a2
            pnlInferior = new Panel();//a2
            btnPausar = new Button();//a2
            btnActualizar = new Button();//a2
            btnTerminar = new Button();//a2
            statusStrip1 = new StatusStrip();//a2
            lblTotal = new ToolStripStatusLabel();//a2
            lblNoResponden = new ToolStripStatusLabel();//a2
            lblActualizacion = new ToolStripStatusLabel();//a2
            timer1 = new System.Windows.Forms.Timer(components);//a2
            timerMetricas = new System.Windows.Forms.Timer(components);
            contextMenuProcesos = new ContextMenuStrip(components);//a2
            cambiarPrioridadToolStripMenuItem = new ToolStripMenuItem();
            abrirUbicaciónDelArchivoToolStripMenuItem = new ToolStripMenuItem();
            tiempoRealToolStripMenuItem = new ToolStripMenuItem();
            altaToolStripMenuItem = new ToolStripMenuItem();
            normalToolStripMenuItem = new ToolStripMenuItem();
            bajaToolStripMenuItem = new ToolStripMenuItem();
            // Boris: Controles del panel de métricas
            //Le cree un panel porque se había puesto todo con coordenadas absolutas
            //y cuando lo mezclaba los controles quedaban encima d mi tabla xd por eso papel a la izq -Astrid
            pnlMonitor = new Panel(); 
            pbRam = new ProgressBar();
            label1 = new Label();
            lblRam = new Label();
            picCpu = new PictureBox();
            picChartCpu = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            pnlSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProcesos).BeginInit();
            pnlInferior.SuspendLayout();
            statusStrip1.SuspendLayout();
            contextMenuProcesos.SuspendLayout();
            pnlMonitor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCpu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picChartCpu).BeginInit();
            SuspendLayout();
            // 
            // pnlSuperior
            // 
            pnlSuperior.Controls.Add(txtBuscar);
            pnlSuperior.Controls.Add(lblTitulo);
            pnlSuperior.Dock = DockStyle.Top;
            pnlSuperior.Location = new Point(0, 0);
            pnlSuperior.Margin = new Padding(2, 2, 2, 2);
            pnlSuperior.Name = "pnlSuperior";
            pnlSuperior.Size = new Size(1000, 72);
            pnlSuperior.TabIndex = 3;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBuscar.Location = new Point(688, 21);
            txtBuscar.Margin = new Padding(2, 2, 2, 2);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por nombre o PID...";
            txtBuscar.Size = new Size(297, 27);
            txtBuscar.TabIndex = 0;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(16, 19);
            lblTitulo.Margin = new Padding(2, 0, 2, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(187, 20);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Administrador de Procesos";
            // 
            // pnlMonitor
            // 
            pnlMonitor.AutoScroll = true;
            pnlMonitor.Controls.Add(label2);
            pnlMonitor.Controls.Add(picCpu);
            pnlMonitor.Controls.Add(label1);
            pnlMonitor.Controls.Add(lblRam);
            pnlMonitor.Controls.Add(pbRam);
            pnlMonitor.Controls.Add(label3);
            pnlMonitor.Controls.Add(picChartCpu);
            pnlMonitor.Dock = DockStyle.Left;
            pnlMonitor.Location = new Point(0, 72);
            pnlMonitor.Name = "pnlMonitor";
            pnlMonitor.Size = new Size(340, 486);
            pnlMonitor.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 10);
            label2.Name = "label2";
            label2.Size = new Size(204, 20);
            label2.TabIndex = 0;
            label2.Text = "Monitoreo de Sistema Global";
            // 
            // picCpu
            // Solo cambié tamaños
            picCpu.Location = new Point(80, 38);
            picCpu.Name = "picCpu";
            picCpu.Size = new Size(180, 180);
            picCpu.TabIndex = 1;
            picCpu.TabStop = false;
            picCpu.Paint += picCpu_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 232);
            label1.Name = "label1";
            label1.Size = new Size(94, 20);
            label1.TabIndex = 2;
            label1.Text = "Uso de RAM:";
            // 
            // lblRam
            // 
            lblRam.AutoSize = true;
            lblRam.Location = new Point(112, 232);
            lblRam.Name = "lblRam";
            lblRam.Size = new Size(12, 20);
            lblRam.TabIndex = 3;
            lblRam.Text = ".";
            // 
            // pbRam
            // 
            pbRam.Location = new Point(12, 258);
            pbRam.Name = "pbRam";
            pbRam.Size = new Size(316, 24);
            pbRam.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 296);
            label3.Name = "label3";
            label3.Size = new Size(86, 20);
            label3.TabIndex = 5;
            label3.Text = "Uso de CPU";
            // 
            // picChartCpu
            // Solo cambié tamaños
            picChartCpu.Location = new Point(12, 320);
            picChartCpu.Name = "picChartCpu";
            picChartCpu.Size = new Size(316, 140);
            picChartCpu.TabIndex = 6;
            picChartCpu.TabStop = false;
            picChartCpu.Paint += picChartCpu_Paint;
            // 
            // dgvProcesos
            // 
            dgvProcesos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProcesos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProcesos.Columns.AddRange(new DataGridViewColumn[] { colId, colNombre, colMemoria, colPrioridad, colEstado });
            dgvProcesos.ContextMenuStrip = contextMenuProcesos;
            dgvProcesos.Dock = DockStyle.Fill;
            dgvProcesos.Location = new Point(340, 72);
            dgvProcesos.Margin = new Padding(2, 2, 2, 2);
            dgvProcesos.Name = "dgvProcesos";
            dgvProcesos.RowHeadersWidth = 62;
            dgvProcesos.Size = new Size(660, 486);
            dgvProcesos.TabIndex = 0;
            dgvProcesos.AutoGenerateColumns = false;
            dgvProcesos.CellFormatting += dgvProcesos_CellFormatting;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.HeaderText = "ID";
            colId.Name = "Id";
            colId.ReadOnly = true;
            // 
            // colNombre
            // 
            colNombre.DataPropertyName = "Name";
            colNombre.HeaderText = "Nombre del Proceso";
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            // 
            // colMemoria
            // 
            colMemoria.DataPropertyName = "MemoryUsage";
            colMemoria.HeaderText = "Memoria RAM";
            colMemoria.Name = "colMemoria";
            colMemoria.ReadOnly = true;
            // 
            // colPrioridad
            // 
            colPrioridad.DataPropertyName = "Priority";
            colPrioridad.HeaderText = "Prioridad";
            colPrioridad.Name = "colPrioridad";
            colPrioridad.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.DataPropertyName = "Status";
            colEstado.HeaderText = "Estado";
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            // 
            // pnlInferior
            // 
            pnlInferior.Controls.Add(btnPausar);
            pnlInferior.Controls.Add(btnActualizar);
            pnlInferior.Controls.Add(btnTerminar);
            pnlInferior.Dock = DockStyle.Bottom;
            pnlInferior.Location = new Point(0, 558);
            pnlInferior.Margin = new Padding(2, 2, 2, 2);
            pnlInferior.Name = "pnlInferior";
            pnlInferior.Size = new Size(1000, 56);
            pnlInferior.TabIndex = 4;
            // 
            // btnPausar
            // 
            btnPausar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPausar.Location = new Point(549, 11);
            btnPausar.Margin = new Padding(2, 2, 2, 2);
            btnPausar.Name = "btnPausar";
            btnPausar.Size = new Size(128, 34);
            btnPausar.TabIndex = 1;
            btnPausar.Text = "Pausar";
            btnPausar.UseVisualStyleBackColor = true;
            btnPausar.Click += btnPausar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnActualizar.Location = new Point(686, 11);
            btnActualizar.Margin = new Padding(2, 2, 2, 2);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(128, 34);
            btnActualizar.TabIndex = 2;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnTerminar
            // 
            btnTerminar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTerminar.Location = new Point(824, 11);
            btnTerminar.Margin = new Padding(2, 2, 2, 2);
            btnTerminar.Name = "btnTerminar";
            btnTerminar.Size = new Size(160, 34);
            btnTerminar.TabIndex = 3;
            btnTerminar.Text = "Terminar Proceso";
            btnTerminar.UseVisualStyleBackColor = true;
            btnTerminar.Click += btnTerminar_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblTotal, lblNoResponden, lblActualizacion });
            statusStrip1.Location = new Point(0, 614);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 11, 0);
            statusStrip1.Size = new Size(1000, 26);
            statusStrip1.TabIndex = 5;
            // 
            // lblTotal
            // 
            lblTotal.Name = "lblTotal";
            lblTotal.Padding = new Padding(10, 0, 10, 0);
            lblTotal.Size = new Size(102, 20);
            lblTotal.Text = "Procesos: 0";
            // 
            // lblNoResponden
            // 
            lblNoResponden.Name = "lblNoResponden";
            lblNoResponden.Padding = new Padding(10, 0, 10, 0);
            lblNoResponden.Size = new Size(138, 20);
            lblNoResponden.Text = "No responden: 0";
            // 
            // lblActualizacion
            // 
            lblActualizacion.Alignment = ToolStripItemAlignment.Right;
            lblActualizacion.Name = "lblActualizacion";
            lblActualizacion.Padding = new Padding(10, 0, 10, 0);
            lblActualizacion.Size = new Size(197, 20);
            lblActualizacion.Text = "Actualización automática";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 3000;
            timer1.Tick += timer1_Tick;
            // 
            // timerMetricas  (independiente de timer1: Pausar no lo detiene)
            // Este lo agregué para que no se congelen las métricas cuando le dan en pausar, o sea se pausa el proceso pero siguen las métricas :) Astrid
            timerMetricas.Enabled = true;
            timerMetricas.Interval = 3000;
            timerMetricas.Tick += timerMetricas_Tick;
            // 
            // contextMenuProcesos
            // 
            contextMenuProcesos.ImageScalingSize = new Size(20, 20);
            contextMenuProcesos.Items.AddRange(new ToolStripItem[] { cambiarPrioridadToolStripMenuItem, abrirUbicaciónDelArchivoToolStripMenuItem });
            contextMenuProcesos.Name = "contextMenuProcesos";
            contextMenuProcesos.Size = new Size(257, 80);
            // 
            // cambiarPrioridadToolStripMenuItem
            // 
            cambiarPrioridadToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tiempoRealToolStripMenuItem, altaToolStripMenuItem, normalToolStripMenuItem, bajaToolStripMenuItem });
            cambiarPrioridadToolStripMenuItem.Name = "cambiarPrioridadToolStripMenuItem";
            cambiarPrioridadToolStripMenuItem.Size = new Size(256, 24);
            cambiarPrioridadToolStripMenuItem.Text = "Cambiar prioridad";
            // 
            // abrirUbicaciónDelArchivoToolStripMenuItem
            // 
            abrirUbicaciónDelArchivoToolStripMenuItem.Name = "abrirUbicaciónDelArchivoToolStripMenuItem";
            abrirUbicaciónDelArchivoToolStripMenuItem.Size = new Size(256, 24);
            abrirUbicaciónDelArchivoToolStripMenuItem.Text = "Abrir ubicación del archivo";
            abrirUbicaciónDelArchivoToolStripMenuItem.Click += menuAbrirUbicacion_Click;
            // 
            // tiempoRealToolStripMenuItem
            // 
            tiempoRealToolStripMenuItem.Name = "tiempoRealToolStripMenuItem";
            tiempoRealToolStripMenuItem.Size = new Size(224, 26);
            tiempoRealToolStripMenuItem.Text = "Tiempo real";
            tiempoRealToolStripMenuItem.Click += menuTiempoReal_Click;
            // 
            // altaToolStripMenuItem
            // 
            altaToolStripMenuItem.Name = "altaToolStripMenuItem";
            altaToolStripMenuItem.Size = new Size(224, 26);
            altaToolStripMenuItem.Text = "Alta";
            altaToolStripMenuItem.Click += menuAlta_Click;
            // 
            // normalToolStripMenuItem
            // 
            normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            normalToolStripMenuItem.Size = new Size(224, 26);
            normalToolStripMenuItem.Text = "Normal";
            normalToolStripMenuItem.Click += menuNormal_Click;
            // 
            // bajaToolStripMenuItem
            // 
            bajaToolStripMenuItem.Name = "bajaToolStripMenuItem";
            bajaToolStripMenuItem.Size = new Size(224, 26);
            bajaToolStripMenuItem.Text = "Baja";
            bajaToolStripMenuItem.Click += menuBaja_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 640);
            // Orden importa para el Dock: dgvProcesos (Fill) primero, luego el panel
            // izquierdo, y al final los paneles que ocupan todo el ancho (inferior, estado, superior).
            Controls.Add(dgvProcesos);
            Controls.Add(pnlMonitor);
            Controls.Add(pnlInferior);
            Controls.Add(statusStrip1);
            Controls.Add(pnlSuperior);
            Margin = new Padding(2, 2, 2, 2);
            MinimumSize = new Size(804, 560);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Administrador de Procesos";
            Load += Form1_Load;
            pnlSuperior.ResumeLayout(false);
            pnlSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProcesos).EndInit();
            pnlInferior.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            contextMenuProcesos.ResumeLayout(false);
            pnlMonitor.ResumeLayout(false);
            pnlMonitor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picCpu).EndInit();
            ((System.ComponentModel.ISupportInitialize)picChartCpu).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvProcesos;
        private Button btnActualizar;
        private Button btnTerminar;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timerMetricas;

        // [IaSTRID] Campos de los controles nuevos
        private Panel pnlSuperior;
        private Label lblTitulo;
        private TextBox txtBuscar;
        private Panel pnlInferior;
        private Button btnPausar;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblTotal;
        private ToolStripStatusLabel lblNoResponden;
        private ToolStripStatusLabel lblActualizacion;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colMemoria;
        private DataGridViewTextBoxColumn colPrioridad;
        private DataGridViewTextBoxColumn colEstado;
        private ContextMenuStrip contextMenuProcesos;
        private ToolStripMenuItem cambiarPrioridadToolStripMenuItem;
        private ToolStripMenuItem tiempoRealToolStripMenuItem;
        private ToolStripMenuItem altaToolStripMenuItem;
        private ToolStripMenuItem normalToolStripMenuItem;
        private ToolStripMenuItem bajaToolStripMenuItem;
        private ToolStripMenuItem abrirUbicaciónDelArchivoToolStripMenuItem;

        // [Monitoreo] Campos del panel de métricas (CPU / RAM)
        private Panel pnlMonitor;
        private ProgressBar pbRam;
        private Label label1;
        private Label lblRam;
        private PictureBox picCpu;
        private PictureBox picChartCpu;
        private Label label2;
        private Label label3;
    }
}