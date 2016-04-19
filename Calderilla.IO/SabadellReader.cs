using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using Calderilla.Model;
using System.Globalization;

namespace Calderilla.IO
{
    public class SabadellReader
    {
        public static List<RegistreSabadell> ReadSabadellFile(String path)
        {
            List<RegistreSabadell> llista = new List<RegistreSabadell>();
                        
            var parser = new TextFieldParser(path);
            parser.SetDelimiters("|");
            while (!parser.EndOfData)
            {
                var registre = new RegistreSabadell();
                var currentRow = parser.ReadFields();
                {
                    //FECHA OPER  CONCEPTO FECHA VALOR IMPORTE SALDO REFERENCIA 1	REFERENCIA 2
                    registre.Data = DateTime.ParseExact(currentRow[0], "dd/MM/yyyy", CultureInfo.InvariantCulture); 
                    registre.Concepte = currentRow[1];
                    registre.Import = Convert.ToDecimal(currentRow[3], new CultureInfo("en-GB"));
                    registre.Saldo = Convert.ToDecimal(currentRow[4], new CultureInfo("en-GB"));
                }
                llista.Add(registre);
            }

            return llista;

        }
    }
}
