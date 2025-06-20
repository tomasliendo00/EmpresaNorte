using EmpresaNorte.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmpresaNorte.Presentacion
{
    public partial class FrmBarrios : Form
    {
        AccesoDatos accesoDatos;
        public FrmBarrios()
        {
            InitializeComponent();
            accesoDatos = new AccesoDatos();
            CargarCombo(cboProvincia);
        }

        private void CargarCombo(ComboBox combo)
        {
            DataTable dt = accesoDatos.ConsultarTabla("Provincias");
            combo.DataSource = dt;
            combo.DisplayMember = dt.Columns[1].ColumnName;
            combo.ValueMember = dt.Columns[0].ColumnName;
            combo.SelectedIndex = -1;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            string consultaSQL = string.Empty;

            if(cboProvincia.SelectedIndex == -1 && chbTodos.Checked == false)
            {
                MessageBox.Show("Seleccione un filtro para continuar.", "Atención",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (chbTodos.Checked)
                {
                    consultaSQL = "SELECT b.descripcion 'Barrio', p.descripcion 'Provincia' " +
                                  "FROM Barrios b " +
                                  "JOIN Provincias p ON p.id_provincia = b.id_provincia ";
                }
                else
                {
                    consultaSQL = "SELECT b.descripcion 'Barrio', p.descripcion 'Provincia' " +
                                     "FROM Barrios b " +
                                     "JOIN Provincias p ON p.id_provincia = b.id_provincia " +
                                     $"WHERE b.id_provincia LIKE {cboProvincia.SelectedValue}";
                }

                DataTable dt = accesoDatos.ConsultarBD(consultaSQL);
                dgvBarrios.DataSource = dt;

                cboProvincia.SelectedIndex = -1;
            }
            foreach (DataGridViewColumn col in dgvBarrios.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void chbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chbTodos.Checked)
            {
                cboProvincia.Enabled = false;
                cboProvincia.SelectedIndex = -1;
            }
            else            
                cboProvincia.Enabled = true;
            
        }
    }
}
