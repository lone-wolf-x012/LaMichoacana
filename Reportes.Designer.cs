namespace LaMichoacana
{
    partial class Reportes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnProductoMasVendido = new System.Windows.Forms.Button();
            this.Fechas = new System.Windows.Forms.GroupBox();
            this.dtpInic = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.txtReporteProductos = new System.Windows.Forms.Button();
            this.lblCambio = new System.Windows.Forms.Label();
            this.txtReporteOrden = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Fechas.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnProductoMasVendido
            // 
            this.btnProductoMasVendido.Location = new System.Drawing.Point(974, 320);
            this.btnProductoMasVendido.Name = "btnProductoMasVendido";
            this.btnProductoMasVendido.Size = new System.Drawing.Size(172, 33);
            this.btnProductoMasVendido.TabIndex = 52;
            this.btnProductoMasVendido.Text = "Producto más Vendidos";
            this.btnProductoMasVendido.UseVisualStyleBackColor = true;
            this.btnProductoMasVendido.Click += new System.EventHandler(this.btnProductoMasVendido_Click);
            // 
            // Fechas
            // 
            this.Fechas.Controls.Add(this.dtpInic);
            this.Fechas.Controls.Add(this.label1);
            this.Fechas.Controls.Add(this.label2);
            this.Fechas.Controls.Add(this.dtpFin);
            this.Fechas.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Fechas.Location = new System.Drawing.Point(461, 20);
            this.Fechas.Name = "Fechas";
            this.Fechas.Size = new System.Drawing.Size(759, 82);
            this.Fechas.TabIndex = 49;
            this.Fechas.TabStop = false;
            this.Fechas.Text = "Fechas";
            this.Fechas.Visible = false;
            // 
            // dtpInic
            // 
            this.dtpInic.Enabled = false;
            this.dtpInic.Location = new System.Drawing.Point(94, 45);
            this.dtpInic.Name = "dtpInic";
            this.dtpInic.Size = new System.Drawing.Size(249, 22);
            this.dtpInic.TabIndex = 23;
            this.dtpInic.Value = new System.DateTime(2025, 10, 13, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fecha inicial:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(382, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha final:";
            // 
            // dtpFin
            // 
            this.dtpFin.Enabled = false;
            this.dtpFin.Location = new System.Drawing.Point(476, 45);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(249, 22);
            this.dtpFin.TabIndex = 24;
            this.dtpFin.Value = new System.DateTime(2025, 10, 13, 0, 0, 0, 0);
            // 
            // txtReporteProductos
            // 
            this.txtReporteProductos.Location = new System.Drawing.Point(982, 174);
            this.txtReporteProductos.Name = "txtReporteProductos";
            this.txtReporteProductos.Size = new System.Drawing.Size(140, 33);
            this.txtReporteProductos.TabIndex = 48;
            this.txtReporteProductos.Text = "Reporte Productos";
            this.txtReporteProductos.UseVisualStyleBackColor = true;
            this.txtReporteProductos.Click += new System.EventHandler(this.txtReporteProductos_Click);
            // 
            // lblCambio
            // 
            this.lblCambio.AutoSize = true;
            this.lblCambio.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCambio.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCambio.Location = new System.Drawing.Point(49, 20);
            this.lblCambio.Name = "lblCambio";
            this.lblCambio.Size = new System.Drawing.Size(158, 38);
            this.lblCambio.TabIndex = 45;
            this.lblCambio.Text = "Reportes";
            // 
            // txtReporteOrden
            // 
            this.txtReporteOrden.Location = new System.Drawing.Point(982, 244);
            this.txtReporteOrden.Name = "txtReporteOrden";
            this.txtReporteOrden.Size = new System.Drawing.Size(140, 33);
            this.txtReporteOrden.TabIndex = 43;
            this.txtReporteOrden.Text = "Reporte Orden";
            this.txtReporteOrden.UseVisualStyleBackColor = true;
            this.txtReporteOrden.Click += new System.EventHandler(this.txtReporteOrden_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.DocumentMapWidth = 81;
            this.reportViewer1.Location = new System.Drawing.Point(3, 3);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(868, 399);
            this.reportViewer1.TabIndex = 57;
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(982, 397);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(140, 33);
            this.btnSalir.TabIndex = 58;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.reportViewer1);
            this.panel1.Location = new System.Drawing.Point(53, 127);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(874, 405);
            this.panel1.TabIndex = 59;
            // 
            // Reportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(84)))), ((int)(((byte)(159)))));
            this.ClientSize = new System.Drawing.Size(1232, 603);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnProductoMasVendido);
            this.Controls.Add(this.Fechas);
            this.Controls.Add(this.txtReporteProductos);
            this.Controls.Add(this.lblCambio);
            this.Controls.Add(this.txtReporteOrden);
            this.Name = "Reportes";
            this.Text = "Reportes";
            this.Load += new System.EventHandler(this.Reportes_Load);
            this.Fechas.ResumeLayout(false);
            this.Fechas.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnProductoMasVendido;
        private System.Windows.Forms.GroupBox Fechas;
        private System.Windows.Forms.DateTimePicker dtpInic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.Button txtReporteProductos;
        private System.Windows.Forms.Label lblCambio;
        private System.Windows.Forms.Button txtReporteOrden;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Panel panel1;
    }
}