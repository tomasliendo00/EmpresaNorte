using EmpresaNorte.Datos;
using EmpresaNorte.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace EmpresaNorte.Presentacion
{
    public partial class FrmEmpleados : Form
    {
        AccesoDatos accesoDatos;
        public FrmEmpleados()
        {
            InitializeComponent();
            accesoDatos = new AccesoDatos();
            CargarCombo("Sucursales", cboSucursal);
            CargarCombo("Tipos_Empleado", cboTipoEmpleado);
            // prueba
        }

        private void CargarCombo(string nombreTabla, ComboBox combo)
        {
            DataTable dt = accesoDatos.ConsultarTabla(nombreTabla);
            combo.DataSource = dt;
            combo.DisplayMember = dt.Columns[1].ColumnName;
            combo.ValueMember = dt.Columns[0].ColumnName;
            combo.SelectedIndex = -1;
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
                cboTipoEmpleado.Enabled = false;
                cboTipoEmpleado.SelectedIndex = -1;
                btnConsultar.Focus();
            }
            else
            {
                txtNombre.Enabled = true;
                cboSucursal.Enabled = true;
                cboTipoEmpleado.Enabled = true;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmDetalles frmDetalles = new FrmDetalles(Modo.NUEVO,null);
            frmDetalles.ShowDialog();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            dgvEmpleados.Columns.Clear();
            //string consultaSQL = "Select * from Empleados where 1=1 ";
            string consultaSQL = "SELECT e.id_empleado 'ID', " +
                                 "e.Nombre, " +
                                 "e.Apellido, " +
                                 "s.id_sucursal 'ID_Sucursal', " +
                                 "s.Direccion 'Sucursal', " +
                                 "t.id_tipo_empleado 'ID_TipoEmpleado', " +
                                 "t.Descripcion 'Cargo', " +
                                 "e.DNI, " +
                                 "e.Telefono 'Teléfono', " +
                                 "e.Email, " +
                                 "e.fecha_nac 'Fecha Nacimiento', " +
                                 "e.fecha_ingreso 'Fecha Ingreso' " +
                                 "from Empleados e " +
                                 "join Sucursales s ON s.id_sucursal = e.id_sucursal " +
                                 "join Tipos_Empleado t ON t.id_tipo_empleado = e.id_tipo_empleado " +
                                 "where 1=1 ";
            if (chbBarrioProv.Checked == true)
            {
                consultaSQL = "select e.id_empleado 'ID', e.Nombre, e.Apellido, s.id_sucursal 'ID_Sucursal', s.Direccion 'Sucursal', b.descripcion 'Barrio', p.descripcion 'Provincia', t.id_tipo_empleado 'ID_TipoEmpleado', t.Descripcion 'Cargo', e.DNI, e.Telefono 'Teléfono', e.Email, e.fecha_nac 'Fecha Nacimiento', e.fecha_ingreso 'Fecha Ingreso' from Empleados e join Sucursales s ON s.id_sucursal = e.id_sucursal join Tipos_Empleado t ON t.id_tipo_empleado = e.id_tipo_empleado join Barrios b ON b.id_barrio = S.id_barrio join Provincias p ON p.id_provincia = b.id_provincia where 1=1";
            }
            if (!string.IsNullOrEmpty(txtNombre.Text))
                consultaSQL += $"and (nombre like '%{txtNombre.Text}%' or apellido like '%{txtNombre.Text}%') ";
            if (cboSucursal.SelectedIndex != -1)
                consultaSQL += $"and s.id_sucursal = {cboSucursal.SelectedValue} ";
            if (cboTipoEmpleado.SelectedIndex != -1)
                consultaSQL += $"and t.id_tipo_empleado = {cboTipoEmpleado.SelectedValue} ";

            dgvEmpleados.DataSource = accesoDatos.ConsultarBD(consultaSQL);

            foreach (DataGridViewColumn col in dgvEmpleados.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            // Ocultar columnas que no deben mostrarse al usuario
            if (dgvEmpleados.Columns.Count > 0)
                dgvEmpleados.Columns[0].Visible = false; // ID del empleado
            
            // Ocultar las columnas de IDs internos
            if (dgvEmpleados.Columns.Contains("ID_Sucursal"))
                dgvEmpleados.Columns["ID_Sucursal"].Visible = false;
            if (dgvEmpleados.Columns.Contains("ID_TipoEmpleado"))
                dgvEmpleados.Columns["ID_TipoEmpleado"].Visible = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dgvEmpleados.Columns.Clear();
            txtNombre.Clear();
            cboSucursal.SelectedIndex = -1;
            cboTipoEmpleado.SelectedIndex = -1;
            chbTodos.Checked = false;
            chbBarrioProv.Checked = false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un empleado para editar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener los datos del empleado seleccionado desde el DataGridView
            Empleado empleadoSeleccionado = new Empleado
            {
                ID = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells["ID"].Value), // Convert string to int
                Nombres = dgvEmpleados.CurrentRow.Cells["Nombre"].Value?.ToString(),
                Apellidos = dgvEmpleados.CurrentRow.Cells["Apellido"].Value?.ToString(),
                DNI = dgvEmpleados.CurrentRow.Cells["DNI"].Value?.ToString(),
                Telefono = dgvEmpleados.CurrentRow.Cells["Teléfono"].Value?.ToString(),
                Email = dgvEmpleados.CurrentRow.Cells["Email"].Value?.ToString(),
                FechaNacimiento = dgvEmpleados.CurrentRow.Cells["Fecha Nacimiento"].Value != null ? Convert.ToDateTime(dgvEmpleados.CurrentRow.Cells["Fecha Nacimiento"].Value) : DateTime.MinValue,
                FechaIngreso = dgvEmpleados.CurrentRow.Cells["Fecha Ingreso"].Value != null ? Convert.ToDateTime(dgvEmpleados.CurrentRow.Cells["Fecha Ingreso"].Value) : DateTime.MinValue,
                Sucursal = new Sucursal 
                    { 
                    ID = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells["ID_Sucursal"].Value),
                    Direccion = dgvEmpleados.CurrentRow.Cells["Sucursal"].Value?.ToString() 
                    },
                TipoEmpleado = new TipoEmpleado 
                    { 
                    ID = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells["ID_TipoEmpleado"].Value),
                    Descripcion = dgvEmpleados.CurrentRow.Cells["Cargo"].Value?.ToString() 
                    }
            };

            FrmDetalles frmDetalles = new FrmDetalles(Modo.EDITAR, empleadoSeleccionado);
            frmDetalles.Text = "Editar Empleado";
            frmDetalles.ShowDialog();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if(dgvEmpleados.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un empleado para borrar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Empleado empleadoSeleccionado = new Empleado
            {
                ID = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells["ID"].Value), // Convert string to int
                Nombres = dgvEmpleados.CurrentRow.Cells["Nombre"].Value?.ToString(),
                Apellidos = dgvEmpleados.CurrentRow.Cells["Apellido"].Value?.ToString(),
                DNI = dgvEmpleados.CurrentRow.Cells["DNI"].Value?.ToString(),
                Telefono = dgvEmpleados.CurrentRow.Cells["Teléfono"].Value?.ToString(),
                Email = dgvEmpleados.CurrentRow.Cells["Email"].Value?.ToString(),
                FechaNacimiento = dgvEmpleados.CurrentRow.Cells["Fecha Nacimiento"].Value != null ? Convert.ToDateTime(dgvEmpleados.CurrentRow.Cells["Fecha Nacimiento"].Value) : DateTime.MinValue,
                FechaIngreso = dgvEmpleados.CurrentRow.Cells["Fecha Ingreso"].Value != null ? Convert.ToDateTime(dgvEmpleados.CurrentRow.Cells["Fecha Ingreso"].Value) : DateTime.MinValue,
                Sucursal = new Sucursal
                {
                    ID = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells["ID_Sucursal"].Value),
                    Direccion = dgvEmpleados.CurrentRow.Cells["Sucursal"].Value?.ToString()
                },
                TipoEmpleado = new TipoEmpleado 
                    { 
                    ID = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells["ID_TipoEmpleado"].Value),
                    Descripcion = dgvEmpleados.CurrentRow.Cells["Cargo"].Value?.ToString() 
                    }
            }
            ;

            FrmDetalles frmDetalles = new FrmDetalles(Modo.BORRAR, empleadoSeleccionado);
            frmDetalles.Text = "Borrar Empleado";
            frmDetalles.ShowDialog();
        }

        private void chbBarrioProv_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un empleado para ver.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Empleado empleadoSeleccionado = new Empleado
            {
                ID = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells["ID"].Value), // Convert string to int
                Nombres = dgvEmpleados.CurrentRow.Cells["Nombre"].Value?.ToString(),
                Apellidos = dgvEmpleados.CurrentRow.Cells["Apellido"].Value?.ToString(),
                DNI = dgvEmpleados.CurrentRow.Cells["DNI"].Value?.ToString(),
                Telefono = dgvEmpleados.CurrentRow.Cells["Teléfono"].Value?.ToString(),
                Email = dgvEmpleados.CurrentRow.Cells["Email"].Value?.ToString(),
                FechaNacimiento = dgvEmpleados.CurrentRow.Cells["Fecha Nacimiento"].Value != null ? Convert.ToDateTime(dgvEmpleados.CurrentRow.Cells["Fecha Nacimiento"].Value) : DateTime.MinValue,
                FechaIngreso = dgvEmpleados.CurrentRow.Cells["Fecha Ingreso"].Value != null ? Convert.ToDateTime(dgvEmpleados.CurrentRow.Cells["Fecha Ingreso"].Value) : DateTime.MinValue,
                Sucursal = new Sucursal
                {
                    ID = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells["ID_Sucursal"].Value),
                    Direccion = dgvEmpleados.CurrentRow.Cells["Sucursal"].Value?.ToString()
                },
                TipoEmpleado = new TipoEmpleado 
                    { 
                    ID = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells["ID_TipoEmpleado"].Value),
                    Descripcion = dgvEmpleados.CurrentRow.Cells["Cargo"].Value?.ToString() 
                    }
            }
            ;

            FrmDetalles frmDetalles = new FrmDetalles(Modo.VER, empleadoSeleccionado);
            frmDetalles.Text = "Ver Empleado";
            frmDetalles.ShowDialog();
        }
    }
}
