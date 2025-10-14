using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace LaMichoacana
{
    public partial class Reportes : Form
    {
        SqlConnection conexion = new SqlConnection("Server=(local)\\SQLEXPRESS;Database=LaMichoacana;Trusted_Connection=True;");
        SqlCommand comando = new SqlCommand();
        SqlDataAdapter Adaptador = new SqlDataAdapter();
        public Reportes()
        {
            InitializeComponent();
        }

        private void Reportes_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            reportViewer1.Clear();
            Fechas.Enabled = true;
            txtReporteOrden.Enabled = true;
        }

        private void txtReporteProductos_Click(object sender, EventArgs e)
        {
            try
            {
                //RESETEAR COMPLETAMENTE EL REPORTVIEWER
                reportViewer1.Reset();
                reportViewer1.LocalReport.ReportPath = null;
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.ReleaseSandboxAppDomain();

                Fechas.Visible = false;
                Fechas.Enabled = false;
                dtpInic.Enabled = false;
                dtpFin.Enabled = false;

                comando.Parameters.Clear(); 

                comando.Connection = conexion;
                comando.CommandText = "Reporte1";
                comando.CommandType = CommandType.StoredProcedure;


                Adaptador.SelectCommand = comando;

                conexion.Open();
                DataSet Data = new DataSet();
                Adaptador.Fill(Data);

                string directorioProyecto = Directory.GetParent(Application.StartupPath).Parent.FullName;
                string ruta = Path.Combine(directorioProyecto, "Report1.rdlc");

                reportViewer1.LocalReport.ReportPath = ruta;


                // Finalmente los datos
                Data.DataSetName = "DataSet1";
                ReportDataSource Reportes = new ReportDataSource("DataSet1", Data.Tables[0]);
                reportViewer1.LocalReport.DataSources.Add(Reportes);

                //FORZAR CARGA COMPLETA
                reportViewer1.RefreshReport();

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

        private void txtReporteOrden_Click(object sender, EventArgs e)
        {
            try
            {
                Fechas.Visible = true;
                Fechas.Enabled = true;
                dtpInic.Enabled = true;
                dtpFin.Enabled = true;
                //RESETEAR COMPLETAMENTE EL REPORTVIEWER
                reportViewer1.Reset();
                reportViewer1.LocalReport.ReportPath = null;
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.ReleaseSandboxAppDomain();

                //OBTENER DATOS DE LA BASE DE DATOS
                //lblCambio.Text = "Reporte Ventas";

                comando.Connection = conexion;
                comando.CommandText = "Reporte2";
                comando.CommandType = CommandType.StoredProcedure;

                string FI = dtpInic.Value.ToString("yyyy-MM-dd");
                string FF = dtpFin.Value.ToString("yyyy-MM-dd");

                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@FI", DateTime.Parse(FI));
                comando.Parameters.AddWithValue("@FF", DateTime.Parse(FF));

                Adaptador.SelectCommand = comando;

                conexion.Open();
                DataSet Data = new DataSet();
                Adaptador.Fill(Data);

                if (Data.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("No hay ventas en el rango de fechas seleccionado");
                    return;
                }

                //CONFIGURAR RUTA DEL REPORTE (VERIFICAR QUE EXISTE)
                string directorioProyecto = Directory.GetParent(Application.StartupPath).Parent.FullName;
                string ruta = Path.Combine(directorioProyecto, "Report2.rdlc");

                //MessageBox.Show($"Buscando reporte en: {ruta}\nExiste: {File.Exists(ruta)}"); // DEBUG

                /*if (!File.Exists(ruta))
                {
                    MessageBox.Show($"❌ El archivo no existe en: {ruta}");
                    return;
                }*/

                //CONFIGURAR REPORTE PASO A PASO
                // Primero la ruta del reporte
                reportViewer1.LocalReport.ReportPath = ruta;

                // Luego los parámetros
                ReportParameter p1 = new ReportParameter("FI", FI);
                ReportParameter p2 = new ReportParameter("FF", FF);
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });

                // Finalmente los datos
                Data.DataSetName = "DataSet1";
                ReportDataSource Reportes = new ReportDataSource("DataSet1", Data.Tables[0]);
                reportViewer1.LocalReport.DataSources.Add(Reportes);

                //FORZAR CARGA COMPLETA
                reportViewer1.RefreshReport();

                //MessageBox.Show("Reporte configurado correctamente");

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

        private void btnProductoMasVendido_Click(object sender, EventArgs e)
        {
            try
            {
                Fechas.Visible = true;
                Fechas.Enabled = true;
                dtpInic.Enabled = true;
                dtpFin.Enabled = true;
                //RESETEAR COMPLETAMENTE EL REPORTVIEWER
                reportViewer1.Reset();
                reportViewer1.LocalReport.ReportPath = null;
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.ReleaseSandboxAppDomain();

                //OBTENER DATOS DE LA BASE DE DATOS
                //lblCambio.Text = "Reporte Ventas";

                comando.Connection = conexion;
                comando.CommandText = "Reporte3";
                comando.CommandType = CommandType.StoredProcedure;

                string FI = dtpInic.Value.ToString("yyyy-MM-dd");
                string FF = dtpFin.Value.ToString("yyyy-MM-dd");

                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@FI", DateTime.Parse(FI));
                comando.Parameters.AddWithValue("@FF", DateTime.Parse(FF));

                Adaptador.SelectCommand = comando;

                conexion.Open();
                DataSet Data = new DataSet();
                Adaptador.Fill(Data);

                if (Data.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("No hay ventas en el rango de fechas seleccionado");
                    return;
                }

                //CONFIGURAR RUTA DEL REPORTE (VERIFICAR QUE EXISTE)
                string directorioProyecto = Directory.GetParent(Application.StartupPath).Parent.FullName;
                string ruta = Path.Combine(directorioProyecto, "Report3.rdlc");

                //MessageBox.Show($"Buscando reporte en: {ruta}\nExiste: {File.Exists(ruta)}"); // DEBUG

                /*if (!File.Exists(ruta))
                {
                    MessageBox.Show($"❌ El archivo no existe en: {ruta}");
                    return;
                }*/

                //CONFIGURAR REPORTE PASO A PASO
                // Primero la ruta del reporte
                reportViewer1.LocalReport.ReportPath = ruta;

                // Luego los parámetros
                ReportParameter p1 = new ReportParameter("FI", FI);
                ReportParameter p2 = new ReportParameter("FF", FF);
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });

                // Finalmente los datos
                Data.DataSetName = "DataSet3";
                ReportDataSource Reportes = new ReportDataSource("DataSet3", Data.Tables[0]);
                reportViewer1.LocalReport.DataSources.Add(Reportes);

                //FORZAR CARGA COMPLETA
                reportViewer1.RefreshReport();

                //MessageBox.Show("Reporte configurado correctamente");

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
