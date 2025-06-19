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
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void integrantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("412354 Alcalá, Esteban\n" +
                            "407944 Caiguara Ramírez, Alexis\n" +
                            "412057 De Girolamo, Luca\n" +
                            "411994 Liendo, Tomás Agustín\n" +
                            "412569 Márquez, Manuel\n" +
                            "412090 Navarrete, Federico\n" +
                            "412134 Venturi, Filippo ",
                            "Integrantes",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Confirmar salida",
                            "Saliendo.",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetalles frmDetalles = new FrmDetalles();
            frmDetalles.ShowDialog();
        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Versión 5.7.98",
                            "Versión",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
        }

        private void empleadosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmEmpleados frmEmpleados = new FrmEmpleados();
            frmEmpleados.ShowDialog();
        }
    }
}
