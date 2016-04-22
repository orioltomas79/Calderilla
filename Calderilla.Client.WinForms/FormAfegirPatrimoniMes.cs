using Calderilla.Model;
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
    public partial class FormAfegirPatrimoniMes : Form
    {
        public FormAfegirPatrimoniMes()
        {
            InitializeComponent();
        }

        private BindingSource binding;

        public void inicialitza(BindingSource binding)
        {
            this.binding = binding;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PatrimoniMes patMes = (PatrimoniMes)binding.AddNew();
            patMes.Data = dataDateTimePicker.Value.Date;
            patMes.Tipus = tipusTextBox.Text;
            patMes.Valor = decimal.Parse(valorTextBox.Text);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
