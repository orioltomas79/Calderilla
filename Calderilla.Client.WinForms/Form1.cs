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

        private void button1_Click(object sender, EventArgs e)
        {
            String fileSabadell = @"C:\Users\Oriol\Google Drive\Calderilla\SabadellOriol.txt";
            String fileCalderilla = @"C:\Users\Oriol\Google Drive\Calderilla\CompteOriol.json";
            String nomCompte = "Oriol";
            carregaDades(fileSabadell, fileCalderilla, nomCompte);

        }

        private void carregaDades(String fileSabadell, string fileCalderilla, string nomCompte)
        {
            // Get data from the bank
            List<RegistreSabadell> registresSabadell = Calderilla.IO.SabadellReader.ReadSabadellFile(fileSabadell);

            // Deserialise previous data 
            Compte compte;
            
            if (System.IO.File.Exists(fileCalderilla))
            {
                string content = System.IO.File.ReadAllText(fileCalderilla);
                compte = JsonConvert.DeserializeObject<Compte>(content);
            }
            else
            {
                compte = new Compte();
                compte.nom = nomCompte;
                compte.registres = new List<Registre>();
            }

            // Merge - Per cada registre sabadell l'afegeix a la base de dades (Si no existeix)
            foreach (RegistreSabadell regSabadell in registresSabadell)
            {
                if (!Existeix(regSabadell, compte)){
                    var reg = new Registre();
                    reg.Data = regSabadell.Data;
                    reg.Concepte = regSabadell.Concepte;
                    reg.Import = regSabadell.Import;
                    compte.registres.Add(reg);
                }
            }

            // Serialitza
            string json = JsonConvert.SerializeObject(compte);
            System.IO.File.WriteAllText(fileCalderilla,json);

        }

        private Boolean Existeix(RegistreSabadell regSabadell, Compte compte)
        {
            foreach (Registre reg in compte.registres)
            {
                if (reg.Data.Equals(regSabadell.Data) &&
                    reg.Concepte.Equals(regSabadell.Concepte) &&
                    reg.Import.Equals(regSabadell.Import))
                {
                    return true;
                }
            }
            return false;
        }
        
    }
}
