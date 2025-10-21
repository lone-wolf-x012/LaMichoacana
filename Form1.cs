using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaMichoacana
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.IsMdiContainer = true; // Hace que el Form sea un contenedor MDI
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioUnico<Productos>();
        }

        private void registrarOrdenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioUnico<Venta>();
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioUnico<ConsultaVentas>();
        }

        private void reportesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioUnico<Reportes>();
        }

        //Método  que se encarga de abrir los formularios "dentro" del Form1
        private void AbrirFormularioUnico<T>() where T : Form, new()
        {
            // Cierra cualquier otro formulario abierto dentro del contenedor
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }

            // Abre el formulario nuevo dentro del contenedor
            T formulario = new T
            {
                MdiParent = this,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill 
            };
            formulario.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            menuStrip1.BackColor = Color.FromArgb(253, 84, 159);
            menuStrip1.ForeColor = Color.White;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Cambia el color del área MDI
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is MdiClient)
                {
                    ctrl.BackColor = Color.FromArgb(255, 182, 193);
                }
            }
        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string rutaProyecto = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\Ayuda\index.html");
            string rutaAbsoluta = Path.GetFullPath(rutaProyecto);

            if (File.Exists(rutaAbsoluta))
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo()
                {
                    FileName = rutaAbsoluta,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("No se encontró el archivo de ayuda en: " + rutaAbsoluta);
            }
        }
    }
}

