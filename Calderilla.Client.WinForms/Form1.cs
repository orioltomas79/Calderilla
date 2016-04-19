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
            Calderilla.Negoci.GestorCompte.CarregaCompte("", "");

        }

        
        
    }
}
