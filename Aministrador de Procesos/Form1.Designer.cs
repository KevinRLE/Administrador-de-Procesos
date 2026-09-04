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
            ((System.ComponentModel.ISupportInitialize)dgvProcesos).BeginInit();
            SuspendLayout();
            // 
            // dgvProcesos
            // 
            dgvProcesos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProcesos.Location = new Point(12, 12);
            dgvProcesos.Name = "dgvProcesos";
            dgvProcesos.RowHeadersWidth = 62;
            dgvProcesos.Size = new Size(1226, 533);
            dgvProcesos.TabIndex = 0;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(335, 551);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(112, 34);
            btnActualizar.TabIndex = 1;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnTerminar
            // 
            btnTerminar.Location = new Point(857, 551);
            btnTerminar.Name = "btnTerminar";
            btnTerminar.Size = new Size(112, 34);
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1250, 611);
            Controls.Add(btnTerminar);
            Controls.Add(btnActualizar);
            Controls.Add(dgvProcesos);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProcesos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvProcesos;
        private Button btnActualizar;
        private Button btnTerminar;
        private System.Windows.Forms.Timer timer1;
    }
}
