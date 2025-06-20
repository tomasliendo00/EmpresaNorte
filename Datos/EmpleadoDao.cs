using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpresaNorte.Datos;
using EmpresaNorte.Negocio;

namespace EmpresaNorte.Datos
{
    public class EmpleadoDao
    {
        AccesoDatos accesoDatos;

        public EmpleadoDao()
        {
            accesoDatos = new AccesoDatos();
        }

        public List<Provincia> RecuperarProvincias()
        {
            List<Provincia> ListaProvincias = new List<Provincia>();

            DataTable dtProv = accesoDatos.ConsultarTabla("Provincias");

            foreach(DataRow fila in dtProv.Rows)
            {
                Provincia oProvincia = new Provincia();
                oProvincia.ID = (int)fila["id_provincia"];
                oProvincia.Descripcion = fila["descripcion"].ToString();
                ListaProvincias.Add(oProvincia);
            }
            return ListaProvincias;
        }

        public List<Barrio> RecuperarBarrios()
        {
            List<Barrio> listaBarrios = new List<Barrio>();

            string consultaSQL = "SELECT b.*, p.descripcion 'Provincia' FROM Barrios b JOIN Provincias p ON p.id_provincia = b.id_provincia";

            DataTable dtBarrios = accesoDatos.ConsultarBD(consultaSQL);

            foreach(DataRow fila in dtBarrios.Rows)
            {
                Barrio oBarrio = new Barrio();
                oBarrio.ID = (int)fila["id_barrio"];
                oBarrio.Descripcion = fila["descripcion"].ToString();
                oBarrio.Provincia = new Provincia();
                oBarrio.Provincia.ID = (int)fila["id_provincia"];
                oBarrio.Provincia.Descripcion = fila["Provincia"].ToString();
                listaBarrios.Add(oBarrio);
            }
            return listaBarrios;
        }

        public List<Sucursal> RecuperarSucursales()
        {
            List<Sucursal> ListaSucursales = new List<Sucursal>();

            string consultaSQL = "SELECT s.id_sucursal, s.direccion, s.telefono, s.email, s.id_barrio, " +
                                 "b.descripcion 'Barrio', b.id_provincia, " +
                                 "p.descripcion 'Provincia' " +
                                 "FROM Sucursales s " +
                                 "JOIN Barrios b ON s.id_barrio = b.id_barrio " +
                                 "JOIN Provincias p ON b.id_provincia = p.id_provincia";

            DataTable dtSucursales = accesoDatos.ConsultarBD(consultaSQL);

            foreach(DataRow fila in dtSucursales.Rows)
            {
                Sucursal oSucursal = new Sucursal();
                oSucursal.ID = (int)fila["id_sucursal"];
                oSucursal.Direccion = fila["direccion"].ToString();
                oSucursal.Telefono = fila["telefono"].ToString();
                oSucursal.Email = fila["email"].ToString();
                oSucursal.Barrio = new Barrio();
                oSucursal.Barrio.ID = (int)fila["id_barrio"];
                oSucursal.Barrio.Descripcion = fila["Barrio"].ToString();
                oSucursal.Barrio.Provincia = new Provincia();
                oSucursal.Barrio.Provincia.ID = (int)fila["id_provincia"];
                oSucursal.Barrio.Provincia.Descripcion = fila["Provincia"].ToString();
                ListaSucursales.Add(oSucursal);
            }
            return ListaSucursales;
        }

        public List<TipoEmpleado> RecuperarTiposEmpleado()
        {
            List<TipoEmpleado> listaTiposEmpleados = new List<TipoEmpleado>();

            DataTable dtTE = accesoDatos.ConsultarTabla("Tipos_Empleado");

            foreach (DataRow fila in dtTE.Rows)
            {
                TipoEmpleado oTipoEmpleado = new TipoEmpleado();
                oTipoEmpleado.ID = (int)fila["id_tipo_empleado"];
                oTipoEmpleado.Descripcion = fila["descripcion"].ToString();
                listaTiposEmpleados.Add(oTipoEmpleado);
            }
            return listaTiposEmpleados;
        }
        

        public List<Empleado> RecuperarEmpleados()
        {
            List<Empleado> listaEmpleados = new List<Empleado>();

            string consultaSQL = "SELECT e.id_empleado, e.nombre, e.apellido, e.id_tipo_empleado, " +
                                 "t.descripcion 'Tipo empleado', " +
                                 "e.dni, e.telefono 'Teléfono', e.email, e.fecha_nac, e.fecha_ingreso, " +
                                 "s.id_sucursal, s.direccion, s.telefono 'Tel suc.', s.email 'Mail suc.', s.id_barrio, " +
                                 "b.descripcion 'Barrio', b.id_provincia, " +
                                 "p.descripcion 'Provincia' " +
                                 "FROM Empleados e " +
                                 "JOIN Tipos_Empleado t ON t.id_tipo_empleado = e.id_tipo_empleado " +
                                 "JOIN Sucursales s ON s.id_sucursal = e.id_sucursal " +
                                 "JOIN Barrios b ON s.id_barrio = b.id_barrio " +
                                 "JOIN Provincias p ON b.id_provincia = p.id_provincia;";

            DataTable dtEmpleados = accesoDatos.ConsultarTabla(consultaSQL);

            foreach (DataRow fila in dtEmpleados.Rows)
            {
                Empleado oEmpleado = new Empleado();
                oEmpleado.ID = (int)fila["id_empleado"];
                oEmpleado.Nombres = fila["nombre"].ToString();
                oEmpleado.Apellidos = fila["apellido"].ToString();
                oEmpleado.DNI = fila["dni"].ToString();
                oEmpleado.FechaNacimiento = (DateTime)fila["fecha_nac"];
                oEmpleado.Telefono = fila["Teléfono"].ToString();
                oEmpleado.Email = fila["email"].ToString();
                oEmpleado.FechaIngreso = (DateTime)fila["fecha_ingreso"];

                // TipoEmpleado
                oEmpleado.TipoEmpleado = new TipoEmpleado();
                oEmpleado.TipoEmpleado.ID = (int)fila["id_tipo_empleado"];
                oEmpleado.TipoEmpleado.Descripcion = fila["Tipo empleado"].ToString();

                // Sucursal
                oEmpleado.Sucursal = new Sucursal();
                oEmpleado.Sucursal.ID = (int)fila["id_sucursal"];
                oEmpleado.Sucursal.Direccion = fila["direccion"].ToString();
                oEmpleado.Sucursal.Telefono = fila["Tel suc."].ToString();
                oEmpleado.Sucursal.Email = fila["Mail suc."].ToString();

                // Barrio
                oEmpleado.Sucursal.Barrio = new Barrio();
                oEmpleado.Sucursal.Barrio.ID = (int)fila["id_barrio"];
                oEmpleado.Sucursal.Barrio.Descripcion = fila["Barrio"].ToString();

                // Provincia
                oEmpleado.Sucursal.Barrio.Provincia = new Provincia();
                oEmpleado.Sucursal.Barrio.Provincia.ID = (int)fila["id_provincia"];
                oEmpleado.Sucursal.Barrio.Provincia.Descripcion = fila["Provincia"].ToString();

                listaEmpleados.Add(oEmpleado);
            }
            return listaEmpleados;
        }

        //public int InsertarEmpleado(Empleado oEmpleado)
        //{
        //    // código
        //}

        //public Empleado RecuperarEmpleadoPorID(int codigo)
        //{
        //    // código
        //}

        //public int BorrarEmpleado(int codigo)
        //{
        //    // código
        //}
    }
}
