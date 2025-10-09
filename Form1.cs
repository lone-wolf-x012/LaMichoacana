using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioUnico<Productos>();
        }

        
        private void AbrirFormularioUnico<T>() where T : Form, new()
        {
            Form frmAbierto = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f is T);

            if (frmAbierto == null)
            {
                T formulario = new T();
                formulario.Show();
            }
            else
            {
                frmAbierto.BringToFront();
                frmAbierto.WindowState = FormWindowState.Normal; // Lo restaura si estaba minimizado
            }
        }

        private void registrarOrdenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioUnico<Venta>();
        }
    }
}
