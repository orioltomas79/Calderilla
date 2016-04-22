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
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private String fileCompte;
        private Compte compte;

        //Inicialitza
        public void inicialitza(String fileCompte)
        {
            this.fileCompte = fileCompte;

            //Load compte
            compte = Calderilla.Negoci.GestorCompte.CarregaCompte(fileCompte);

            //Merge data moviments
            Calderilla.Negoci.GestorCompte.CombinaCompte(compte);

            //Sort moviemnts by date
            if (compte.moviments != null)
            {
                compte.moviments = compte.moviments.OrderByDescending(o => o.Data).ToList();
            }
            
            //Sort patrimoniMes by date
            if (compte.patrimoniMes!= null)
            {
                compte.patrimoniMes = compte.patrimoniMes.OrderByDescending(o => o.Data).ThenBy(o => o.Tipus).ToList();
            }
            

            //Show moviments
            movimentsBindingSource.DataSource = compte.moviments;
            dataGridView1.DataSource = movimentsBindingSource;
            dataGridView1.AutoResizeColumns();

            //Show patrimoni mes
            patrimoniMesBindingSource.DataSource = compte.patrimoniMes;
            dataGridView2.DataSource = patrimoniMesBindingSource;
            dataGridView2.AutoResizeColumns();

        }
                
        //Guarda
        private void button2_Click(object sender, EventArgs e)
        {
            Calderilla.Negoci.GestorCompte.GuardaCompte(compte, fileCompte);
        }

        //Excel
        private void button6_Click(object sender, EventArgs e)
        {
            Calderilla.Excel.GestorExcel.updateSpreadsheet(compte, compte.rutaInformesExcel + "Calderilla.xlsx", compte.rutaInformesPdf + "Calderilla.pdf");
        }
        
        //Panel dret
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (compte != null)
            {
                                
                if (e.RowIndex < compte.moviments.Count)
                {
                    Moviment reg = compte.moviments[e.RowIndex];

                    //Info row
                    String str1 = "";
                    Dictionary<String, Int32> diccionariCategoriesConcepte = compte.DonaCategoriesConcepte(reg.Concepte);
                    foreach (var keyValue in diccionariCategoriesConcepte)
                    {
                        str1 = str1 + String.Format("{0} - {1}", keyValue.Key, keyValue.Value) + "\n";
                    }
                    label1.Text = reg.GetString() + "POSSIBLES CATEGORIES" + "\n" + str1;

                    //Llista categories
                    String str2 = "";
                    Dictionary<String, Int32> diccionariCategories = compte.DonaCategories();
                    foreach (var keyValue in diccionariCategories)
                    {
                        str2 = str2 + String.Format("{0} - {1}", keyValue.Key, keyValue.Value) + "\n";
                    }
                    label2.Text = "\n\nCATEGORIES:" + "\n\n" + str2;


                }
            }

            Int32 totalMoviments = compte.moviments.Count;
            Int32 totalMovimentsNoRevisats = compte.moviments.Where(m => m.Revisat == false).Count();
            Int32 totalDeshabilitatsAmbCategoria = compte.moviments.Where(m => m.Categoria != null && m.Deshabilita == true).Count();

            label3.Text = "( " + totalMovimentsNoRevisats + " moviments no revisats de un total de " + totalMoviments + 
                ")         ( " + totalDeshabilitatsAmbCategoria + " moviments deshabilitats amb categoria )";

        }

        //Copy All
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

            foreach (Moviment reg in compte.moviments)
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

        //Afegir
        private void button3_Click(object sender, EventArgs e)
        {
            var frmAfegir = new FormAfegirMoviment();
            frmAfegir.inicialitza(movimentsBindingSource);
            frmAfegir.ShowDialog();
            //Sort moviemnts by date
            compte.moviments = compte.moviments.OrderByDescending(o => o.Data).ToList();

            //Refresh
            movimentsBindingSource.DataSource = null;
            dataGridView1.DataSource = null;
            movimentsBindingSource.DataSource = compte.moviments;
            dataGridView1.DataSource = movimentsBindingSource;
            dataGridView1.AutoResizeColumns();

            this.dataGridView1.Refresh();
        }

        //Elimina
        private void button4_Click(object sender, EventArgs e)
        {

            var selectedCells = this.dataGridView1.SelectedCells;
            if (selectedCells.Count == 1)
            {
                var cell = selectedCells[0];
                Moviment reg = compte.moviments[cell.RowIndex];

                DialogResult dialogResult = MessageBox.Show("Segur que vols eliminar aquest moviment?\n\n" + reg.GetString(), "Elimina moviment", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    compte.moviments.Remove(reg);
                    this.dataGridView1.Refresh();
                }
            }
            
        }
        
        #region "Formatting"

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
                if (e.RowIndex < compte.moviments.Count)
                {
                    Moviment reg = compte.moviments[e.RowIndex];
                    if (reg.Data.Month % 2 == 1)
                    {
                        e.CellStyle.BackColor = Color.FromArgb(184, 204, 228);
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.FromArgb(220, 230, 241);
                    }
                }
            }

        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Pijama per mes
            if (compte != null && compte.patrimoniMes != null)
            {
                if (e.RowIndex < compte.patrimoniMes.Count)
                {
                    PatrimoniMes patMes = compte.patrimoniMes[e.RowIndex];
                    if (patMes.Data.Month % 2 == 1)
                    {
                        e.CellStyle.BackColor = Color.FromArgb(184, 204, 228);
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.FromArgb(220, 230, 241);
                    }
                }
            }

        }

        #endregion

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Pijama per mes
            if (compte != null)
            {
                if (e.RowIndex < compte.moviments.Count)
                {
                    Moviment reg = compte.moviments[e.RowIndex];

                    Dictionary<String, Int32> diccionari = compte.DonaCategoriesConcepte(reg.Concepte);
                    if (diccionari.Count == 1)
                    {
                        reg.Categoria = diccionari.Keys.First();
                    }
                    
                }
            }
        }



        //Patrimoni Mes
        private void button8_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            String line;
            line = "DATA\t";
            line += "TIPUS\t";
            line += "VALOR";
            builder.AppendLine(line);

            foreach (PatrimoniMes reg in compte.patrimoniMes)
            {
                line = "";
                line += reg.Data + "\t";
                line += reg.Tipus + "\t";
                line += reg.Valor;
                builder.AppendLine(line);
            }

            Clipboard.SetText(builder.ToString(), TextDataFormat.UnicodeText);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frmAfegir = new FormAfegirPatrimoniMes();
            frmAfegir.inicialitza(patrimoniMesBindingSource);
            frmAfegir.ShowDialog();
            //Sort moviemnts by date
            compte.patrimoniMes = compte.patrimoniMes.OrderByDescending(o => o.Data).ToList();

            //Refresh
            patrimoniMesBindingSource.DataSource = null;
            dataGridView2.DataSource = null;
            patrimoniMesBindingSource.DataSource = compte.patrimoniMes;
            dataGridView2.DataSource = patrimoniMesBindingSource;
            dataGridView2.AutoResizeColumns();

            this.dataGridView2.Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var selectedCells = this.dataGridView2.SelectedCells;
            if (selectedCells.Count == 1)
            {
                var cell = selectedCells[0];
                PatrimoniMes reg = compte.patrimoniMes[cell.RowIndex];

                DialogResult dialogResult = MessageBox.Show("Segur que vols eliminar aquest patrimoni mes?\n\n", "Elimina patrimoni", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    compte.patrimoniMes.Remove(reg);
                    this.dataGridView2.Refresh();
                }
            }
        }
    }
}

