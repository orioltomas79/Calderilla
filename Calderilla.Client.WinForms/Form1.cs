using Calderilla.Model;
using Newtonsoft.Json;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Compte compte;
        String fileSabadell = @"C:\Users\Oriol\Google Drive\Calderilla\SabadellOriol.txt";
        String fileCompte = @"C:\Users\Oriol\Google Drive\Calderilla\CompteOriol.json";
        String nomCompte = "Oriol";

        private void button1_Click(object sender, EventArgs e)
        {
            //Load compte
            compte = Calderilla.Negoci.GestorCompte.CarregaCompte(fileCompte, nomCompte);

            //Sort by date
            compte.registres = compte.registres.OrderByDescending(o => o.Data).ToList();

            //Show data
            var bindingList = new BindingList<Model.Registre>(compte.registres);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Calderilla.Negoci.GestorCompte.GuardaCompte(compte, fileCompte);
        }



        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Object obj = dataGridView1.SelectedRows[0].DataBoundItem;
                Registre reg = (Registre)obj;

                foreach (var line in compte.registres.Where(r => r.Concepte.Equals(reg.Concepte) )
                    .GroupBy(r => r.Categoria)
                    .Select(group => new
                    {
                        Categoria = group.Key,
                        Count = group.Count()
                    })
                    .OrderBy(x => x.Categoria))
                {
                    Console.WriteLine("{0} {1}", line.Categoria, line.Count);
                }

                textBox1.Text = reg.Concepte;



            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Object obj = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            if (obj is Decimal)
            {
                Decimal dec = (Decimal)obj;
                if (dec < 0)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = new DataGridViewCellStyle { ForeColor = Color.Green };
                }
            }

        }
    }
}
