using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LaMichoacana
{
    public partial class Venta : Form
    {
        SqlConnection conexion = new SqlConnection("Server=RAIKIRI\\SQLEXPRESS;Database=LaMichoacana;Trusted_Connection=True;");
        SqlCommand comando = new SqlCommand();
        SqlDataReader lector;
        Double total;
        public Venta()
        {
            InitializeComponent();
        }

        private void Venta_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txtIdProducto.Text = "";
            txtPrecio.Text = "";
            try
            {
                conexion.Open();
                int n = 0;
                comando.Connection = conexion;
                comando.CommandText = "SELECT COUNT (*) FROM Ventas"; //Modificar
                n = Convert.ToInt32(comando.ExecuteScalar()) + 1;
                txtVenta.Text = n.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
            //Lectura del menu
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SELECT categoria FROM Productos";
                lector = comando.ExecuteReader();
                cbCategoria.Items.Clear();
                while (lector.Read())
                {
                    cbCategoria.Items.Add(lector.GetValue(0).ToString());
                }
                lector.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void cbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SELECT nombre FROM Productos Where categoria = '" +cbCategoria.Text + "';";
                lector = comando.ExecuteReader();
                cbProducto.Items.Clear();
                while (lector.Read())
                {
                    cbProducto.Items.Add(lector.GetValue(0));
                }
                lector.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void cbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SELECT id_producto, precio from Productos Where nombre = '" + cbProducto.Text + "';";
                lector = comando.ExecuteReader();
                if (lector.Read())
                {
                    txtIdProducto.Text = lector.GetValue(0).ToString();
                    txtPrecio.Text = lector.GetValue(1).ToString();
                }
                lector.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                int importe = 0;
                importe = Convert.ToInt32(txtCantidad.Text) * Convert.ToInt32(txtPrecio.Text);
                int bandera = 0;
                int contador = 0;
                int filasVisibles = dataGridView1.Rows.Count -1;
                //int filasVisibles = dataGridView1.ColumnCount -1;

                //Repetidos
                while (bandera == 0 && contador <= filasVisibles)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[contador].Cells[0].Value) == Convert.ToInt32(txtIdProducto.Text))
                    {
                        bandera = 1;
                        int cantidadActual = 0;
                        cantidadActual = Convert.ToInt32(dataGridView1.Rows[contador].Cells[3].Value);
                        cantidadActual = Convert.ToInt32(txtCantidad.Text) + cantidadActual;
                        dataGridView1.Rows[contador].Cells[3].Value = cantidadActual;
                        importe = Convert.ToInt32(dataGridView1.Rows[contador].Cells[3].Value) * Convert.ToInt32(txtPrecio.Text);
                        dataGridView1.Rows[contador].Cells[4].Value = importe;
                        total += importe;
                    }
                    contador++;
                }
                //Registro unico
                if (bandera == 0)
                {
                    dataGridView1.Rows.Add(
                        txtIdProducto.Text,
                        cbProducto.Text,
                        txtPrecio.Text,
                        txtCantidad.Text,
                        importe
                        );
                }
                cbCategoria.Text = "";
                cbProducto.Text = "";
                txtPrecio.Text = "";
                txtCantidad.Text = "";
                txtIdProducto.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            cbCategoria.Text = "";
            cbProducto.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
            txtIdProducto.Text = "";
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
