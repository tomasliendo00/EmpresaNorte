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
        public FrmDetalles()
        {
            InitializeComponent();
            FechaMaxMin();
        }

        private void FechaMaxMin() // Método para fijar fecha mínima y máxima en DateTimePicker de FechaNac y FechaIngreso
        {
            dtpFechaNac.MaxDate = DateTime.Today;
            dtpFechaNac.MinDate = DateTime.Today.AddYears(-90);
            dtpFechaIng.MaxDate = DateTime.Today;
            dtpFechaIng.MinDate = DateTime.Today.AddYears(15);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
