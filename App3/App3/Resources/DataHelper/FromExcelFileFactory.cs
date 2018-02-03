using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using App3.Models;
using ExcelDataReader;


namespace App3.Resources.DataHelper
{
    public class FromExcelFileFactory
    {
        public List<Words> GetWords(string path)
        {

            var file = new FileInfo(path);
            using (
                var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader reader;

                if (file.Extension.Equals(".xls"))
                    reader = ExcelDataReader.ExcelReaderFactory.CreateBinaryReader(stream);
                else if (file.Extension.Equals(".xlsx"))
                    reader = ExcelDataReader.ExcelReaderFactory.CreateOpenXmlReader(stream);
                else
                    throw new Exception("Invalid FileName");

                //// reader.IsFirstRowAsColumnNames
                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };

                System.Data.DataSet result = reader.AsDataSet();

                //3. DataSet - Create column names from first row
                DataTable dt = result.Tables[0];

                List<Words> words = new List<Words>();

                int rowCount = dt.Rows.Count;
                int colCount = dt.Columns.Count;

                List<string> def;
                List<string> word2 = new List<string>();
                for (int i = 0; i < rowCount; i++)
                {
                    def = new List<string>();
                    string w = "";
                    if (dt.Rows[i][0].ToString() != "")
                    {
                        w = FileFactory.Strip(dt.Rows[i][0].ToString());
                    }

                    for (int j = 1; j < colCount; j++)
                    {
                        if (dt.Rows[i][j].ToString() != "")
                        {
                            def.Add(FileFactory.Strip(dt.Rows[i][j].ToString()));
                        }

                    }
                    words.Add(new Words(w, def, "exel"));
                }
                return words;
            }
        }
    }
}