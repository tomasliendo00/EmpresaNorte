using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaNorte.Negocio
{
    public class Empleado
    {
        private int id;
        private string nombres;              // varchar(50) not null
        private string apellidos;            // varchar(50) not null
        private string dni;                  // varchar(10) not null
        private DateTime fechaNacimiento;    // date not null
        private string telefono;             // varchar(20) not null
        private string email;                // varchar(50) not null
        private Sucursal sucursal;
        private TipoEmpleado tipoEmpleado;
        private DateTime fechaIngreso;       // date not null

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombres
        {
            get { return nombres; }
            set { nombres = value; }
        }

        public string Apellidos
        {
            get { return apellidos; }
            set { apellidos = value; }
        }

        public string DNI
        {
            get { return dni; }
            set { dni = value; }
        }

        public DateTime FechaNacimiento
        {
            get { return fechaNacimiento; }
            set { fechaNacimiento = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public Sucursal Sucursal
        {
            get { return sucursal; }
            set { sucursal = value; }
        }

        public TipoEmpleado TipoEmpleado
        {
            get { return tipoEmpleado; }
            set { tipoEmpleado = value; }
        }

        public DateTime FechaIngreso
        {
            get { return fechaIngreso; }
            set { fechaIngreso = value; }
        }

        public override string ToString()
        {
            return id + " - " + nombres + ", " + apellidos;
        }
    }
}
