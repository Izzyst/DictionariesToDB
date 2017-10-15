using System;
using System.Collections.Generic;
using ConsoleApp1.Domain;
using Excel = Microsoft.Office.Interop.Excel;

namespace ConsoleApp1.Factories
{
    public class FromExcelFileFactory : AbstractDictionary
    {

        public override List<Words> GetWords()
        {
            List<Words> words = new List<Words>();
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\Izabela\Documents\words.xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            Excel.Worksheet x = xlApp.ActiveSheet as Excel.Worksheet;

            int rowCount = xlWorksheet.UsedRange.Rows.Count;
            int colCount = xlWorksheet.UsedRange.Columns.Count;

            // Console.WriteLine("ilosc wierszy: " + rowCount + " ilość kolumn: " + colCount);
            List<string> def;
            List<string> word2 = new List<string>();
            for (int i = 1; i <= rowCount; i++)
            {
                def = new List<string>();
                string w = "";
                if (x.Cells[i, 1].Value2 != null)
                {
                    Console.WriteLine(x.Cells[i, 1].Value2);
                    w = x.Cells[i, 1].Value2;
                }

                for (int j = 2; j <= colCount; j++)
                {
                    if (x.Cells[i, j].Value2 != null)
                    {
                        def.Add(x.Cells[i, j].Value2);
                        Console.WriteLine(x.Cells[i, j].Value2);
                    }

                }
                words.Add(new Words(w, def));
                //def.Clear();
                
            }
            return words;
        }
    }
}
