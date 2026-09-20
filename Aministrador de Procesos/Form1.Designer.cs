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
            dgvProcesos = new DataGridView();
            btnActualizar = new Button();
            btnTerminar = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            pbRam = new ProgressBar();
            label1 = new Label();
            lblRam = new Label();
            picCpu = new PictureBox();
            picChartCpu = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvProcesos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picCpu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picChartCpu).BeginInit();
            SuspendLayout();
            // 
            // dgvProcesos
            // 
            dgvProcesos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProcesos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProcesos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProcesos.Location = new Point(519, 273);
            dgvProcesos.Margin = new Padding(2);
            dgvProcesos.Name = "dgvProcesos";
            dgvProcesos.RowHeadersWidth = 62;
            dgvProcesos.Size = new Size(472, 271);
            dgvProcesos.TabIndex = 0;
            // 
            // btnActualizar
            // 
            btnActualizar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnActualizar.Location = new Point(519, 549);
            btnActualizar.Margin = new Padding(2);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(90, 27);
            btnActualizar.TabIndex = 1;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnTerminar
            // 
            btnTerminar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnTerminar.Location = new Point(686, 549);
            btnTerminar.Margin = new Padding(2);
            btnTerminar.Name = "btnTerminar";
            btnTerminar.Size = new Size(90, 27);
            btnTerminar.TabIndex = 2;
            btnTerminar.Text = "Terminar Proceso";
            btnTerminar.UseVisualStyleBackColor = true;
            btnTerminar.Click += btnTerminar_Click;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 3000;
            timer1.Tick += timer1_Tick;
            // 
            // pbRam
            // 
            pbRam.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pbRam.Location = new Point(519, 233);
            pbRam.Name = "pbRam";
            pbRam.Size = new Size(472, 35);
            pbRam.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(523, 207);
            label1.Name = "label1";
            label1.Size = new Size(94, 20);
            label1.TabIndex = 4;
            label1.Text = "Uso de RAM:";
            label1.Click += label1_Click;
            // 
            // lblRam
            // 
            lblRam.AutoSize = true;
            lblRam.Location = new Point(620, 208);
            lblRam.Name = "lblRam";
            lblRam.Size = new Size(12, 20);
            lblRam.TabIndex = 5;
            lblRam.Text = ".";
            lblRam.Click += label2_Click;
            // 
            // picCpu
            // 
            picCpu.Anchor = AnchorStyles.Left;
            picCpu.Location = new Point(61, 186);
            picCpu.Name = "picCpu";
            picCpu.Size = new Size(200, 200);
            picCpu.TabIndex = 6;
            picCpu.TabStop = false;
            picCpu.Paint += picCpu_Paint;
            // 
            // picChartCpu
            // 
            picChartCpu.Anchor = AnchorStyles.Left;
            picChartCpu.Location = new Point(61, 419);
            picChartCpu.Name = "picChartCpu";
            picChartCpu.Size = new Size(325, 157);
            picChartCpu.TabIndex = 7;
            picChartCpu.TabStop = false;
            picChartCpu.Paint += picChartCpu_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(40, 149);
            label2.Name = "label2";
            label2.Size = new Size(204, 20);
            label2.TabIndex = 8;
            label2.Text = "Monitoreo de Sistema Global";
            label2.Click += label2_Click_1;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(61, 396);
            label3.Name = "label3";
            label3.Size = new Size(86, 20);
            label3.TabIndex = 9;
            label3.Text = "Uso de CPU";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 597);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(picChartCpu);
            Controls.Add(picCpu);
            Controls.Add(lblRam);
            Controls.Add(label1);
            Controls.Add(pbRam);
            Controls.Add(btnTerminar);
            Controls.Add(btnActualizar);
            Controls.Add(dgvProcesos);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProcesos).EndInit();
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
        private ProgressBar pbRam;
        private Label label1;
        private Label lblRam;
        private PictureBox picCpu;
        private PictureBox picChartCpu;
        private Label label2;
        private Label label3;
    }
}
