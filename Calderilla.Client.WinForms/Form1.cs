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
        String fileExcel = @"C:\Users\Oriol\Google Drive\Calderilla\Calderilla.xlsx";
        String filePdf = @"C:\Users\Oriol\Google Drive\Calderilla\Calderilla.pdf";

        String nomCompte = "Oriol";

        private void button1_Click(object sender, EventArgs e)
        {
            //Load compte
            compte = Calderilla.Negoci.GestorCompte.CarregaCompte(fileCompte, nomCompte);

            //Sort by date
            compte.registres = compte.registres.OrderByDescending(o => o.Data).ToList();

            //Show data
            registresBindingSource.DataSource = compte.registres;
            dataGridView1.DataSource = registresBindingSource;
            //dataGridView1.Columns[0].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            dataGridView1.AutoResizeColumns();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            Calderilla.Negoci.GestorCompte.GuardaCompte(compte, fileCompte);
        }
               
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            
            //Imports Red or Green
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

            // Pijama per mes
            if (compte != null)
            {
                if (e.RowIndex < compte.registres.Count)
                {
                    Registre reg = compte.registres[e.RowIndex];
                    if (reg.Data.Month % 2 == 1)
                    {
                        e.CellStyle.BackColor = Color.FromArgb(184, 204, 228); 
                    }else
                    {
                        e.CellStyle.BackColor = Color.FromArgb(220, 230, 241); 
                    }
                }
            }

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (compte != null)
            {
                String str = "";

                if (e.RowIndex < compte.registres.Count)
                {
                    Registre reg = compte.registres[e.RowIndex];
                    foreach (var line in compte.registres.Where(r => r.Concepte.Equals(reg.Concepte))
                    .GroupBy(r => r.Categoria)
                    .Select(group => new
                    {
                        Categoria = group.Key,
                        Count = group.Count()
                    })
                    .OrderBy(x => x.Categoria))
                    {
                        str = str + String.Format("{0} - {1}", line.Categoria, line.Count) + "\n";
                    }
  
                    label1.Text = reg.GetString() + "POSSIBLES CATEGORIES" + "\n" + str;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            String line;
            line = "DATA\t";
            line += "CONCEPTE\t";
            line += "IMPORT\t";
            line += "CATEGORIA\t";
            line += "DESHABILITAT\t";
            line += "REVISAT\t";
            line += "COMENTARI";
            builder.AppendLine(line);

            foreach (Registre reg in compte.registres)
            {
                line = "";
                line += reg.Data + "\t";
                line += reg.Concepte + "\t";
                line += reg.Import + "\t";
                line += reg.Categoria + "\t";
                line += reg.Deshabilita  + "\t";
                line += reg.Revisat + "\t";
                line += reg.Comentari;
                builder.AppendLine(line);
            }

            Clipboard.SetText(builder.ToString(), TextDataFormat.UnicodeText);
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Calderilla.Excel.GestorExcel.updateSpreadsheet(compte, fileExcel, filePdf);        
        }
    }
}

