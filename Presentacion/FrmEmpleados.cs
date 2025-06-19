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
    public partial class FrmEmpleados : Form
    {
        AccesoDatos accesoDatos;
        public FrmEmpleados()
        {
            InitializeComponent();
            accesoDatos = new AccesoDatos();
            CargarCombo("Sucursales");
            // prueba
        }

        private void CargarCombo(string nombreTabla)
        {
            DataTable dt = accesoDatos.ConsultarTabla(nombreTabla);
            cboSucursal.DataSource = dt;
            cboSucursal.ValueMember = "id_sucursal";
            cboSucursal.DisplayMember = "direccion";
            cboSucursal.SelectedIndex = -1;
        }

        private void cboSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chbTodos.Checked)
            {
                txtNombre.Enabled = false;
                txtNombre.Text = string.Empty;
                cboSucursal.Enabled = false;
                cboSucursal.SelectedIndex = -1;
                btnConsultar.Focus();
            }
            else
            {
                txtNombre.Enabled = true;
                cboSucursal.Enabled = true;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmDetalles frmDetalles = new FrmDetalles();
            frmDetalles.ShowDialog();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            string consultaSQL = "Select * from Empleados where 1=1 ";
            if (!string.IsNullOrEmpty(txtNombre.Text))
                consultaSQL += $"and (nombre like '%{txtNombre.Text}%' or apellido like '%{txtNombre.Text}%') ";
            if (cboSucursal.SelectedIndex != -1)
                consultaSQL += $"and id_sucursal = {cboSucursal.SelectedValue} ";

            dgvEmpleados.DataSource = accesoDatos.ConsultarBD(consultaSQL);

            if (dgvEmpleados.Columns.Count > 0)
                dgvEmpleados.Columns[0].Visible = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dgvEmpleados.Columns.Clear();
            txtNombre.Clear();
            cboSucursal.SelectedIndex = -1;
            chbTodos.Checked = false;
        }
    }
}
