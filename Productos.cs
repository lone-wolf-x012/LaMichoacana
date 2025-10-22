using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LaMichoacana
{
    public partial class Productos : Form
    {
        //SqlConnection conexion = new SqlConnection("Server=RAIKIRI\\SQLEXPRESS;Database=LaMichoacana;Trusted_Connection=True;");
        SqlConnection conexion = new SqlConnection("Server=(local)\\SQLEXPRESS;Database=LaMichoacana;Trusted_Connection=True;");

        SqlCommand comando = new SqlCommand();
        SqlDataReader lector;
        public Productos()
        {
            InitializeComponent();
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                comando.CommandText = "SELECT * FROM Productos";
                lector = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (lector.Read())
                {
                    // Agregar los valores correctos a las columnas del DataGridView
                    dataGridView1.Rows.Add(
                        lector.GetValue(0),
                        lector.GetValue(1),
                        lector.GetValue(2),
                        lector.GetValue(3)
                    );
                }
                lector.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                btnModificar.Enabled = false;
                btnNuevo.Enabled = false;
                btnGrabar.Enabled = true;
                txtIdProducto.Text = "";
                txtIdProducto.Enabled = false;
                txtNombre.Text = "";
                txtCategoria.Text = "";
                txtPrecio.Text = "";
                txtNombre.Enabled = true;
                txtCategoria.Enabled = true;
                txtPrecio.Enabled = true;
                int n = 0;
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SELECT count (*) FROM Productos";
                n = Convert.ToInt32(comando.ExecuteScalar()) + 1;
                txtIdProducto.Text = n.ToString();
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al grabar el repartidor: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                btnModificar.Enabled = false;
                btnBuscar.Enabled = true;
                btnGrabar.Enabled = false;
                conexion.Open();
                SqlDataReader lector;
                int idN = Convert.ToInt32(txtIdProducto.Text);
                comando.CommandText = "UPDATE Productos SET nombre = '" + txtNombre.Text + "', categoria = '" + txtCategoria.Text + "', precio = '" + txtPrecio.Text + "' WHERE id_producto = " + idN + ";";
                comando.ExecuteNonQuery();
                dataGridView1.Rows.Clear(); //Refrescar la rejilla
                                            //Consultar de nuevo
                comando.CommandText = "SELECT * FROM Productos";
                lector = comando.ExecuteReader();
                while (lector.Read())
                    dataGridView1.Rows.Add(
                        lector.GetValue(0),
                        lector.GetValue(1),
                        lector.GetValue(2),
                        lector.GetValue(3));
                lector.Close();
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al grabar el producto: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                btnBuscar.Enabled = false;
                btnModificar.Enabled = true;
                btnGrabar.Enabled = false;
                txtNombre.Enabled = true;
                txtPrecio.Enabled = true;
                txtCategoria.Enabled = true;
                conexion.Open();
                SqlDataReader lector;
                int idx = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox("Introduzca el ID"));
                String r;
                comando.CommandText = "SELECT * FROM Productos WHERE id_producto = " + idx;
                lector = comando.ExecuteReader();
                if (lector.Read())
                {
                    txtIdProducto.Text = lector.GetValue(0).ToString();
                    txtNombre.Text = lector.GetValue(1).ToString();
                    txtCategoria.Text = lector.GetValue(2).ToString();
                    txtPrecio.Text = lector.GetValue(3).ToString();
                }
                else
                {
                    MessageBox.Show("El id " + idx + " no existe");
                }
                lector.Close();
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al grabar el producto: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                btnGrabar.Enabled = false;
                btnNuevo.Enabled = true;
                conexion.Open();
                SqlDataReader lector;
                string sqlQuery = "INSERT INTO Productos VALUES(" + "'" + txtNombre.Text + "','" + txtCategoria.Text + "','" + txtPrecio.Text + "')";
                MessageBox.Show(sqlQuery);
                comando.CommandText = sqlQuery;
                comando.ExecuteNonQuery();
                dataGridView1.Rows.Clear(); //Refrescar la rejilla
                                            //Consultar de nuevo
                comando.CommandText = "SELECT * FROM Productos";
                lector = comando.ExecuteReader();
                while (lector.Read())
                    dataGridView1.Rows.Add(
                        lector.GetValue(0),
                        lector.GetValue(1),
                        lector.GetValue(2),
                        lector.GetValue(3));
                lector.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al grabar el producto: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
