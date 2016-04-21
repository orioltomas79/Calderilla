using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calderilla.Client.WinForms
{
    public partial class FormInicial : Form
    {
        public FormInicial()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormPrincipal frmPrincipal = new FormPrincipal();
            frmPrincipal.inicialitza(Properties.Settings.Default.RutaOriol);
            frmPrincipal.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormPrincipal frmPrincipal = new FormPrincipal();
            frmPrincipal.inicialitza(Properties.Settings.Default.RutaComuna);
            frmPrincipal.ShowDialog();
            this.Close();
        }
        
    }
}
