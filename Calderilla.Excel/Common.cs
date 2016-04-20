using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Calderilla.Excel
{
    class Common
    {
        public static ChartObject getChart(Workbook xlWorkBook, String name)
        {

            foreach (Worksheet sheet in xlWorkBook.Sheets)
            {

                ChartObjects chartObjects = (ChartObjects)(sheet.ChartObjects(Type.Missing));
                for (int i = 1; i <= chartObjects.Count; i++)
                {
                    ChartObject oChartObject = (ChartObject)(chartObjects.Item(i));
                    if (name.Equals(oChartObject.Name))
                    {
                        return oChartObject;
                    }
                }

            }

            return null;
        }

        public static void refreshPivotTables(Workbook xlWorkBook)
        {
            //Refresh pivot tables
            foreach (Microsoft.Office.Interop.Excel.Worksheet pivotSheet in xlWorkBook.Worksheets)
            {
                Microsoft.Office.Interop.Excel.PivotTables pivotTables = pivotSheet.PivotTables();
                int pivotTablesCount = pivotTables.Count;
                if (pivotTablesCount > 0)
                {
                    for (int i = 1; i <= pivotTablesCount; i++)
                    {
                        pivotTables.Item(i).RefreshTable(); //The Item method throws an exception
                    }
                }
            }
        }
    }
}
