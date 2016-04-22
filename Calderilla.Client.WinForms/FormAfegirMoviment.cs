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
    public partial class FormAfegirMoviment : Form
    {
        public FormAfegirMoviment()
        {
            InitializeComponent();
        }

        private BindingSource binding;

        public void inicialitza(BindingSource binding)
        {
            this.binding = binding;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Moviment mov = (Moviment) binding.AddNew();
            mov.Categoria = categoriaTextBox.Text;
            mov.Comentari = comentariTextBox.Text;
            mov.Concepte = concepteTextBox.Text;
            mov.Data = dataDateTimePicker.Value.Date;
            mov.Deshabilita = deshabilitaCheckBox.Checked;
            mov.Import = decimal.Parse(importTextBox.Text);
            mov.Revisat = revisatCheckBox.Checked;
            this.Close();
        }
    }
}
