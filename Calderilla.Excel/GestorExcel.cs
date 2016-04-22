using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Calderilla.Model;

namespace Calderilla.Excel
{
    public class GestorExcel
    {

        public static void updateSpreadsheet(Compte compte, String excelFile, String pdfFile)

        {

            //Read input file (Excel)
            Console.WriteLine("   Reading spreadsheet.");
            Application excelApp;
            Workbook excelWorkBook;

            excelApp = new Application();
            excelApp.DisplayAlerts = false;
            excelWorkBook = excelApp.Workbooks.Open(excelFile, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Update Excel Data sheet
            updateMoviments(compte, excelWorkBook);
            Console.WriteLine("   Excel moviments updated.");
            updatePatrimoniMes(compte, excelWorkBook);
            Console.WriteLine("   Excel patrimoni mes updated.");

            //Refresh pivot tables
            Common.refreshPivotTables(excelWorkBook);
            Console.WriteLine("   Excel pivot tables refreshed.");

            //Write output file
            excelWorkBook.Save();
            
            //Export to PDF
            exportPdf(excelWorkBook, pdfFile);

            excelWorkBook.Close(true, Type.Missing, Type.Missing);
            excelApp.Quit();
            releaseExcelObject(excelWorkBook);
            releaseExcelObject(excelApp);
        }

        public static void updateMoviments(Compte compte, Workbook xlWorkBook)
        {

            var xlWorkSheet = (Worksheet)xlWorkBook.Worksheets["#Moviments#"];

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
                if (registre.Import >= 0)
                {
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

        public static void updatePatrimoniMes(Compte compte, Workbook xlWorkBook)
        {

            var xlWorkSheet = (Worksheet)xlWorkBook.Worksheets["#Patrimoni#"];

            Range r = xlWorkSheet.get_Range("patrimoni");
            r.Clear();

            int numCols = 3;
            int numRows = compte.patrimoniMes.Count + 1;
            r = r.get_Resize(numRows, numCols);

            //Create an array.
            Object[,] array = new Object[numRows, numCols];

            array[0, 0] = "DATA";
            array[0, 1] = "TIPUS";
            array[0, 2] = "VALOR";
            
            int row = 1;
            foreach (var patrimoniMes in compte.patrimoniMes)
            {
               
                //Create line
                array[row, 0] = String.Format("{0:dd/MM/yyyy}",patrimoniMes.Data);
                array[row, 1] = patrimoniMes.Tipus;
                array[row, 2] = patrimoniMes.Valor;
               
                row = row + 1;
            }

            r.set_Value(Type.Missing, array);

        }

        private static void exportPdf(Workbook xlWorkBook, String pdfFile)
        {
            Console.Write("   Exporting to pdf ... ");
            foreach (Worksheet sheet in xlWorkBook.Sheets)
            {
                if ((sheet.Name.ToLower().StartsWith("#")))
                {
                    sheet.Visible = XlSheetVisibility.xlSheetHidden;
                }
            }

            xlWorkBook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, pdfFile, Type.Missing, true, true, Type.Missing, Type.Missing, false, Type.Missing);

            foreach (Worksheet sheet in xlWorkBook.Sheets)
            {
                sheet.Visible = XlSheetVisibility.xlSheetVisible;
            }

            Console.WriteLine("Completed");

        }

        private static void releaseExcelObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                obj = null;
            }

        }

    }
}
