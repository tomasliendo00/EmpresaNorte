using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaNorte.Negocio
{
    public class Barrio
    {
        public int ID { get; set; }
        public string Descripcion { get; set; } // varchar(50)
        public Provincia Provincia { get; set; }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}
