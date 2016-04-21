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
            Moviments.updateMoviments(compte, excelWorkBook);
            Console.WriteLine("   Excel moviments updated.");
            
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
