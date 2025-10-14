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
            dataGridView1.Rows.Clear();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "Select D.id_producto, P.nombre, D.precio_unitario, D.cantidad, (D.cantidad * D.precio_unitario) AS Importe, V.total, V.fecha " +
                    "FROM Ventas AS V " +
                    "INNER JOIN DetalleVenta AS D ON D.id_venta = V.id_venta " +
                    "INNER JOIN Productos AS P ON P.id_producto = D.id_producto " +
                    "WHERE V.id_venta = " + Convert.ToInt32(txtIdx.Text) + ";";
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
