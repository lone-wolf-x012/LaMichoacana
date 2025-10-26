using System;
using System.Data.SqlClient;
using System.Linq;
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
        private bool ValidarNombre()//Metodo para validar el nombre del producto
        {
            string nombre = txtNombre.Text.Trim();

            // Validar que no esté vacío
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("El nombre del producto no puede estar vacío.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombre.Focus();
                btnNuevo.Enabled = true;
                return false;
            }

            // Validar que no contenga números
            if (nombre.Any(char.IsDigit))
            {
                MessageBox.Show("El nombre del producto no puede contener números.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombre.Focus();
                btnNuevo.Enabled = true;
                return false;
            }
            
            return true;
        }
        private bool ValidarPrecio()// Metodo para validar el precio del producto
        {
            string precio = txtPrecio.Text.Trim();

            // Validar que no esté vacío
            if (string.IsNullOrEmpty(precio))
            {
                MessageBox.Show("El precio no puede estar vacío.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPrecio.Focus();
                btnNuevo.Enabled = true;
                return false;
            }

            // Validar que sea un número entero válido
            if (!int.TryParse(precio, out int precioEntero))
            {
                MessageBox.Show("El precio debe ser un número entero válido.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPrecio.Focus();
                btnNuevo.Enabled = true;
                return false;
            }

            // Validar que sea mayor a 0
            if (precioEntero <= 0)
            {
                MessageBox.Show("El precio debe ser mayor a 0.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPrecio.Focus();
                btnNuevo.Enabled = true;
                return false;
            }

            return true;
        }



        private bool ValidarCategoria()// Metodo para validar la categoria del producto
        {
            // Validar que se haya seleccionado una categoría
            if (string.IsNullOrEmpty(cbCategoria.Text))
            {
                MessageBox.Show("Debe seleccionar una categoría.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbCategoria.Focus();
                btnNuevo.Enabled = true;
                return false;
            }

            return true;
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

                comando.Connection = conexion;
                comando.CommandText = "SELECT DISTINCT categoria FROM Productos " +
                    "Order by categoria;";
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
                cbCategoria.Text = "";
                cbCategoria.SelectedIndex = -1;
                txtPrecio.Text = "";
                txtNombre.Enabled = true;
                cbCategoria.Enabled = true;
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
            // Validar antes de modificar
            if (!ValidarNombre() || !ValidarPrecio() || !ValidarCategoria())
                return;

            try
            {
                btnNuevo.Enabled = true;
                btnModificar.Enabled = false;
                btnBuscar.Enabled = true;
                btnGrabar.Enabled = false;
                conexion.Open();
                SqlDataReader lector;
                int idN = Convert.ToInt32(txtIdProducto.Text);
                comando.CommandText = "UPDATE Productos SET nombre = '" + txtNombre.Text + "', categoria = '" + cbCategoria.Text + "', precio = '" + txtPrecio.Text + "' WHERE id_producto = " + idN + ";";
                
                MessageBox.Show("Producto registrado exitosamente");

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
                cbCategoria.Enabled = true;
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
                    cbCategoria.Text = lector.GetValue(2).ToString();
                    txtPrecio.Text = lector.GetValue(3).ToString();
                }
                else
                {
                    MessageBox.Show("El id " + idx + " no existe");
                    btnNuevo.Enabled= true;
                    btnBuscar.Enabled= true;
                }
                lector.Close();
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Busquéda cancelada");
                btnBuscar.Enabled = true;
                btnNuevo.Enabled = true;
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {

            // Validar antes de grabar
            if (!ValidarNombre() || !ValidarPrecio() || !ValidarCategoria())
                return;

            try
            {
                btnGrabar.Enabled = false;
                btnNuevo.Enabled = true;
                conexion.Open();
                SqlDataReader lector;
                string sqlQuery = "INSERT INTO Productos VALUES(" + "'" + txtNombre.Text + "','" + cbCategoria.Text + "','" + txtPrecio.Text + "')";
                
                MessageBox.Show("Producto registrado exitosamente");
                
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

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números, backspace y delete
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;                
            }
        }
    }
}
