using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaNorte.Negocio
{
    public class Sucursal
    {
        public int ID { get; set; }
        public string Direccion { get; set; } // varchar(50) not null
        public string Telefono { get; set; } // varchar(20) not null
        public string Email { get; set; } // varchar (50) 
        public Barrio Barrio { get; set; }

        public override string ToString()
        {
            return Direccion;
        }
    }
}
