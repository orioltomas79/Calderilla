﻿using Calderilla.Model;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calderilla.Negoci
{
    public class GestorCompte
    {

        public static Compte CarregaCompte(String fileCompte, String nomCompte)
        {

            // Deserialise previous data 
            Compte compte;

            if (System.IO.File.Exists(fileCompte))
            {
                string content = System.IO.File.ReadAllText(fileCompte);
                compte = JsonConvert.DeserializeObject<Compte>(content);
            }
            else
            {
                compte = new Compte();
                compte.nom = nomCompte;
                compte.registres = new List<Registre>();
            }

            return compte;
        }

        public static void ActualitzaCompte(Compte compte, String fileBanc)
        {
            // Get data from the bank
            List<RegistreSabadell> registresSabadell = ReadSabadellFile(fileBanc);

            // Merge - Per cada registre sabadell l'afegeix a la base de dades (Si no existeix)
            foreach (RegistreSabadell regSabadell in registresSabadell)
            {
                if (!Existeix(regSabadell, compte))
                {
                    var reg = new Registre();
                    reg.Data = regSabadell.Data;
                    reg.Concepte = regSabadell.Concepte;
                    reg.Import = regSabadell.Import;
                    compte.registres.Add(reg);
                }
            }
        }

        public static void GuardaCompte(Compte compte, String fileCompte)
        {
            // Serialitza
            string json = JsonConvert.SerializeObject(compte);
            System.IO.File.WriteAllText(fileCompte, json);
        }

        private static Boolean Existeix(RegistreSabadell regSabadell, Compte compte)
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
                }
                llista.Add(registre);
            }

            return llista;

        }

    }
}
