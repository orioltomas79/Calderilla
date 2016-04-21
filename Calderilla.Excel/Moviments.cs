using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Calderilla.Model;

namespace Calderilla.Excel
{
    class Moviments
    {

        public static void updateMoviments(Compte compte, Workbook xlWorkBook)
        {

            var xlWorkSheet = (Worksheet)xlWorkBook.Worksheets["Moviments"];
            
            Range r = xlWorkSheet.get_Range("moviments");
            r.Clear();

            int numCols = 11;
            int numRows = compte.moviments.Count + 1;
            r = r.get_Resize(numRows, numCols);

            //Create an array.
            Object[,] array = new Object[numRows, numCols];

            array[0, 0] = "DIA";
            array[0, 1] = "MES";
            array[0, 2] = "ANY";
            array[0, 3] = "CONCEPTE";
            array[0, 4] = "TIPUS";
            array[0, 5] = "IMPORT";
            array[0, 6] = "IMPORT (Valor absolut)";
            array[0, 7] = "CATEGORIA";
            array[0, 8] = "DESHABILITAT";
            array[0, 9] = "REVISAT";
            array[0, 10] = "COMENTARI";


            int row = 1;
            foreach (var registre in compte.moviments)
            {
                String tipus = "Despesa";
                if (registre.Import >= 0) {
                    tipus = "Ingrés";
                }

                //Create line
                array[row, 0] = registre.Data.Day;
                array[row, 1] = registre.Data.Month;
                array[row, 2] = registre.Data.Year;
                array[row, 3] = registre.Concepte;
                array[row, 4] = tipus;
                array[row, 5] = registre.Import;
                array[row, 6] = Math.Abs(registre.Import);
                array[row, 7] = registre.Categoria;
                array[row, 8] = registre.Deshabilita;
                array[row, 9] = registre.Revisat;
                array[row, 10] = registre.Comentari;
                
                row = row + 1;
            }

            r.set_Value(Type.Missing, array);

        }
        
    }
}
