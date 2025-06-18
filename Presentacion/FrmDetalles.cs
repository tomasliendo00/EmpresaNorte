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
    public partial class FrmDetalles : Form
    {
        AccesoDatos accesoDatos;
        public FrmDetalles()
        {
            InitializeComponent();
            accesoDatos = new AccesoDatos();
            FechaMaxMin();
            CargarCombo("sucursales", cboSucursal);
            CargarCombo("tipos_empleado", cboTipoEmpleado);
        }

        private void CargarCombo(string nombreTabla, ComboBox combo)
        {
            DataTable dt = accesoDatos.ConsultarTabla(nombreTabla);
            combo.DataSource = dt;
            combo.DisplayMember = dt.Columns[1].ColumnName;
            combo.ValueMember = dt.Columns[0].ColumnName;
            combo.SelectedIndex = -1;
        }

        private void FechaMaxMin() // Método para fijar fecha mínima y máxima en DateTimePicker de FechaNac y FechaIngreso
        {
            dtpFechaNac.MaxDate = DateTime.Today;
            dtpFechaNac.MinDate = DateTime.Today.AddYears(-90);
            dtpFechaIng.MaxDate = DateTime.Today;
            dtpFechaIng.MinDate = DateTime.Today.AddYears(-15);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
