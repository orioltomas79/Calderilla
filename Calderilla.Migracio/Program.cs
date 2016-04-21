using Calderilla.Model;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calderilla.Migracio
{
    class Program
    {
        static void Main(string[] args)
        {
            String fileAntic = @"C:\tmp\AnticComuna2015.csv";
            String fileCalderilla = @"C:\Users\Oriol\Google Drive\Calderilla\ComunaSerialitzacio.json";
            carregaDades(fileAntic, fileCalderilla);

        }

        private static void carregaDades(String fileAntic, string fileCalderilla)
        {

            Compte compte;
            compte = new Compte();
            compte.registres = new List<Registre>();
            
            var parser = new TextFieldParser(fileAntic);
            parser.SetDelimiters("|");
            while (!parser.EndOfData)
            {
                var registre = new Registre();
                var currentRow = parser.ReadFields();
                {
                    //FECHA OPER  CONCEPTO FECHA VALOR IMPORTE SALDO REFERENCIA 1	REFERENCIA 2
                    registre.Data = DateTime.ParseExact(currentRow[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    registre.Concepte = currentRow[1];
                    registre.Import = Convert.ToDecimal(currentRow[2], new CultureInfo("es-ES"));
                    registre.Categoria = currentRow[3];
                }
                compte.registres.Add(registre);
            }
            
            // Serialitza
            string json = JsonConvert.SerializeObject(compte);
            System.IO.File.WriteAllText(fileCalderilla, json);

        }

    }
}

