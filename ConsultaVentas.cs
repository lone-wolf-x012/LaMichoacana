using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LaMichoacana
{
    public partial class ConsultaVentas : Form
    {
        SqlConnection conexion = new SqlConnection("Server=(local)\\SQLEXPRESS;Database=LaMichoacana;Trusted_Connection=True;");
        SqlCommand comando = new SqlCommand();
        SqlDataReader lector;
        public ConsultaVentas()
        {
            InitializeComponent();
        }

        private void ConsultaVentas_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Validar antes de buscar
            if (!ValidarIdOrden())
                return;

            dataGridView1.Rows.Clear();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                int idOrden = Convert.ToInt32(txtIdx.Text);

                comando.CommandText = "Select D.id_producto, P.nombre, D.precio_unitario, D.cantidad, (D.cantidad * D.precio_unitario) AS Importe, V.total, V.fecha " +
                    "FROM Ventas AS V " +
                    "INNER JOIN DetalleVenta AS D ON D.id_venta = V.id_venta " +
                    "INNER JOIN Productos AS P ON P.id_producto = D.id_producto " +
                    "WHERE V.id_venta = " + idOrden + ";";
                lector = comando.ExecuteReader();

                bool tieneDatos = false;
                while (lector.Read())
                {
                    tieneDatos = true;
                    dataGridView1.Rows.Add(
                        lector.GetValue(0),
                        lector.GetValue(1),
                        lector.GetValue(2),
                        lector.GetValue(3),
                        lector.GetValue(4)
                    );
                    txtTotal.Text = lector.GetValue(5).ToString();
                    DateTime fechaDesdeBD = lector.GetDateTime(6);
                    txtFecha.Text = fechaDesdeBD.ToString("yyyy-MM-dd");
                }
                lector.Close();

                // Si no hay datos en el detalle, pero la venta existe, mostrar mensaje
                if (!tieneDatos)
                {
                    MessageBox.Show($"El pedido #{idOrden} existe pero no tiene productos registrados.", "Información",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTotal.Text = "0";
                    txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txtIdx_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números, backspace y delete
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private bool ValidarIdOrden()
        {
            if (string.IsNullOrEmpty(txtIdx.Text.Trim()))
            {
                MessageBox.Show("Debe ingresar un número de pedido.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtIdx.Focus();
                return false;
            }

            if (!int.TryParse(txtIdx.Text, out int idOrden))
            {
                MessageBox.Show("El número de pedido debe ser un número entero válido.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtIdx.Focus();
                return false;
            }

            // Validar que el pedido exista en la base de datos
            return ValidarExistenciaPedido(idOrden);
        }

        private bool ValidarExistenciaPedido(int idOrden)
        {
            try
            {
                conexion.Open();
                comando.Connection = conexion;

                // Verificar si el pedido existe
                comando.CommandText = "SELECT COUNT(*) FROM Ventas WHERE id_venta = " + idOrden;
                int count = Convert.ToInt32(comando.ExecuteScalar());

                if (count == 0)
                {
                    MessageBox.Show($"El pedido #{idOrden} no existe. Ingrese un número de pedido válido.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtIdx.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar el pedido: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                conexion.Close();
            }
        }

    }
}
