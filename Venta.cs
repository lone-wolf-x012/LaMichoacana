using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LaMichoacana
{
    public partial class Venta : Form
    {
        SqlConnection conexion = new SqlConnection("Server=(local)\\SQLEXPRESS;Database=LaMichoacana;Trusted_Connection=True;");
        SqlCommand comando = new SqlCommand();
        SqlDataReader lector;
        int total;
        public Venta()
        {
            InitializeComponent();
        }

        private void Venta_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtIdProducto.Text = "";
            txtPrecio.Text = "";
            btnGrabar.Enabled = false;
            btnNuevo.Enabled = true;
            btnSalir.Enabled = true;
            btnOk.Enabled = false;
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
            //Lectura de categorias
            try
            {
                conexion.Open();
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
                cbProducto.Enabled = true;
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "SELECT nombre FROM Productos Where categoria = '" +cbCategoria.Text + "' " +
                    "Order by nombre;";
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
                MessageBox.Show("Error al cargar los datos (cbCategoria): " + ex.Message);
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
                btnOk.Enabled = true;
                txtCantidad.Enabled = true; 
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
                MessageBox.Show("Error al cargar los datos(cbProducto): " + ex.Message);
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
                total = total + importe;

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
                txtTotal.Text = total.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos (btnOK): " + ex.Message);
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
                btnGrabar.Enabled = true;
                btnNuevo.Enabled = false;
                btnSalir.Enabled = true;
                btnOk.Enabled = false;
                cbCategoria.Enabled = true;
                cbProducto.Enabled = true;
                cbCategoria.Text = "";
                cbProducto.Text = "";
                txtPrecio.Text = "";
                txtCantidad.Text = "";
                txtIdProducto.Text = "";
                dataGridView1.Rows.Clear();
                txtTotal.Text = "";
                
                //Ejecucion
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "Select count(*) from Ventas";
                int datoV = Convert.ToInt32(comando.ExecuteScalar()) + 1;
                txtVenta.Text = datoV.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos (btnNuevo): " + ex.Message);
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
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "INSERT INTO Ventas (fecha, total) VALUES ('" + txtFecha.Text + "', " + Convert.ToInt32(txtTotal.Text) + ");";
                comando.ExecuteNonQuery();

                //Ciclo for para recorrer los detalles de la venta
                int filasVisibles = dataGridView1.Rows.Count - 1;
                for (int i = 0; i < filasVisibles; i++)
                {
                    int id_venta = Convert.ToInt32(txtVenta.Text);
                    int id_producto = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                    int cantidad = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                    int precio_unitario = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);

                    //Insercion individual al detalle de venta, se repite en cada vuelta del ciclo 
                    comando.CommandText = "INSERT INTO DetalleVenta (id_venta, id_producto, cantidad, precio_unitario) VALUES (" + id_venta + ", " + id_producto + ", " + cantidad + ", " + precio_unitario + ");";
                    comando.ExecuteNonQuery();
                }
                MessageBox.Show("Registro exitoso");
                btnGrabar.Enabled = false;
                btnNuevo.Enabled = true;
                btnSalir.Enabled = true;
                btnOk.Enabled = false;
                cbCategoria.Enabled = false;
                cbProducto.Enabled = false;
                cbCategoria.Text = "";
                cbProducto.Text = "";
                txtPrecio.Text = "";
                txtCantidad.Text = "";
                txtIdProducto.Text = "";
                txtVenta.Text = "";
                dataGridView1.Rows.Clear();
                txtTotal.Text = "";
                total = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos (btnGrabar): " + ex.Message);
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
