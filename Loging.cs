using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;


namespace LaMichoacana
{
    public partial class Loging : Form
    {
        string cadenaConexion = "Server=(local)\\SQLEXPRESS;Database=LaMichoacana;Trusted_Connection=True;";

        public Loging()
        {
            InitializeComponent();
        }

        private void Loging_Load(object sender, EventArgs e)
        {

        }

        public string GetMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = GetMD5(txtContraseña.Text.Trim());

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT Rol FROM Usuarios WHERE Usuario = @Usuario AND Password = @Password", con);

                cmd.Parameters.Add("@Usuario", System.Data.SqlDbType.NVarChar, 50).Value = usuario;
                cmd.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 32).Value = password;

                object resultado = cmd.ExecuteScalar();

                if (resultado != null)
                {
                    string rol = resultado.ToString();

                    MessageBox.Show("Bienvenido " + rol);

                    Form1 menu = new Form1();
                    menu.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                }
            }
        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Seguro que deseas salir?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
             );

            if (resultado == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
