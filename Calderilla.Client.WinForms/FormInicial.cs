using System;
using System.IO;
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
            inicialitza();
        }

        private void inicialitza()
        {
            //Backup
            String rutaOrigen = Properties.Settings.Default.RutaGoogleDrive + "Calderilla";
            String rutaDesti = Properties.Settings.Default.RutaGoogleDrive + "Calderilla Backup\\" + DateTime.Now.Year + "." + DateTime.Now.Month;

            if (!Directory.Exists(rutaDesti))
            {
                Directory.CreateDirectory(rutaDesti);
                
                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(rutaOrigen, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(rutaOrigen, rutaDesti));
                }
                    
                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(rutaOrigen, "*.*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(rutaOrigen, rutaDesti), true);
                }
                    
            }



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
