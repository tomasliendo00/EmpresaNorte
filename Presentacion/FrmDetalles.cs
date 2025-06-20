using EmpresaNorte.Datos;
using EmpresaNorte.Negocio;
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

        private bool validarDatos()
        {
            if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Debe cargar el nombre del empleado.","Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                txtNombre.Focus();
                return false;
            }
            else if (txtApellido.Text == string.Empty)
            {
                MessageBox.Show("Debe cargar el apellido del empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtApellido.Focus();
                return false;
            }
            else if (txtDNI.Text == string.Empty)
            {
                MessageBox.Show("Debe cargar el DNI del empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDNI.Focus();
                return false;
            }            
            else if (dtpFechaNac.Value > DateTime.Now.AddYears(-18))
            {
                MessageBox.Show("El empleado no puede tener menos de 18 años.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaNac.Focus();
                return false;
            }
            else if (txtTelefono.Text == string.Empty)
            {
                MessageBox.Show("Debe cargar el número de telefono del empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTelefono.Focus();
                return false;
            }
            else if (txtEmail.Text == string.Empty)
            {
                MessageBox.Show("Debe cargar el mail del empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEmail.Focus();
                return false;
            }
            else if (cboSucursal.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una sucursal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboSucursal.Focus();
                return false;
            }
            else if (cboTipoEmpleado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el tipo de empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboTipoEmpleado.Focus();
                return false;
            }
            else if (!txtNombre.Text.All(char.IsLetter) || !txtApellido.Text.All(char.IsLetter))
            {
                MessageBox.Show("El nombre y el apellido solo pueden contener letras.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (!txtDNI.Text.All(char.IsNumber))
            {
                MessageBox.Show("El DNI solo puede contener números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDNI.Focus();
                return false;
            }
            else if (!txtTelefono.Text.All(char.IsNumber))
            {
                MessageBox.Show("El teléfono solo puede contener números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTelefono.Focus();
                return false;
            }
            else if(!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains(".com"))
            {
                MessageBox.Show("Ingrese un email válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEmail.Focus();
                return false;
            }
            return true;
        }

        private void cargarEmpleado()
        {
            Empleado empleado = new Empleado();
            empleado.Nombres = txtNombre.Text;
            empleado.Apellidos = txtApellido.Text;
            empleado.DNI = txtDNI.Text; 
            empleado.FechaNacimiento = dtpFechaNac.Value;
            empleado.Telefono = txtTelefono.Text; 
            empleado.Email = txtEmail.Text;

            // Reemplazar estas líneas:
            //empleado.Sucursal = Convert.ToInt32(cboSucursal.SelectedValue);
            //empleado.TipoEmpleado = Convert.ToInt32(cboTipoEmpleado.SelectedValue);

            // Por estas líneas:
            empleado.Sucursal = new Sucursal { ID = Convert.ToInt32(cboSucursal.SelectedValue) };
            empleado.TipoEmpleado = new TipoEmpleado { ID = Convert.ToInt32(cboTipoEmpleado.SelectedValue) };
            empleado.FechaIngreso = dtpFechaIng.Value;

            string consultaSQL = "SET DATEFORMAT DMY; " +
                                 "INSERT INTO EMPLEADOS VALUES " +
                                 "(@nombre," +
                                 " @apellido," +
                                 " @sucursal," +
                                 " @tipoEmpleado," +
                                 " @dni," +
                                 " @telefono," +
                                 " @email," +
                                 " @fechaNacimiento," +
                                 " @fechaIngreso)";

            List<Parametro> lEmpleados = new List<Parametro>();

            lEmpleados.Add(new Parametro("@nombre", empleado.Nombres));
            lEmpleados.Add(new Parametro("@apellido", empleado.Apellidos));
            lEmpleados.Add(new Parametro("@dni", empleado.DNI));
            lEmpleados.Add(new Parametro("@fechaNacimiento", empleado.FechaNacimiento));
            lEmpleados.Add(new Parametro("@telefono", empleado.Telefono));
            lEmpleados.Add(new Parametro("@email", empleado.Email));
            lEmpleados.Add(new Parametro("@sucursal", empleado.Sucursal));
            lEmpleados.Add(new Parametro("@tipoEmpleado", empleado.TipoEmpleado));
            lEmpleados.Add(new Parametro("@fechaIngreso", empleado.FechaIngreso));

            if (accesoDatos.ActualizarBD(consultaSQL, lEmpleados) > 0)
            {
                MessageBox.Show("Empleado cargado con exito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
                this.Close();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                cargarEmpleado();
            }
        }
    }
}
