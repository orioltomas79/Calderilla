﻿using Calderilla.Model;
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

            //Merge data
            Calderilla.Negoci.GestorCompte.CombinaCompte(compte);

            //Sort by date
            compte.registres = compte.registres.OrderByDescending(o => o.Data).ToList();

            //Show data
            registresBindingSource.DataSource = compte.registres;
            dataGridView1.DataSource = registresBindingSource;
            dataGridView1.AutoResizeColumns();
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
                String str = "";

                if (e.RowIndex < compte.registres.Count)
                {
                    Registre reg = compte.registres[e.RowIndex];

                    Dictionary<String, Int32> diccionari = compte.DonaCategoriesConcepte(reg.Concepte);

                    foreach (var keyValue in diccionari)
                    {
                        str = str + String.Format("{0} - {1}", keyValue.Key, keyValue.Value) + "\n";
                    }
 
                    label1.Text = reg.GetString() + "POSSIBLES CATEGORIES" + "\n" + str;
                }
            }
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
                if (e.RowIndex < compte.registres.Count)
                {
                    Registre reg = compte.registres[e.RowIndex];
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

        #endregion


    }
}
