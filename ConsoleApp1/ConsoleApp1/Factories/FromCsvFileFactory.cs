using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleApp1.Domain;

namespace ConsoleApp1.Factories
{
    public class FromCsvFileFactory : AbstractDictionary
    {
        public override List<Words> GetWords(string path)
        {
            List<Words> words = new List<Words>();
            using (var fs = File.OpenRead(@path))
            using (var reader = new StreamReader(fs))
            {
                string word = "";
                List<string> defs;

                while (!reader.EndOfStream)
                {
                    defs = new List<string>();
                    var line = reader.ReadLine();
                    var cols = line.Split(',').Count();
                    var values = line.Split(',');
                    //Word
                    word = values[0];
                    //Word w = new Word();
                    //w.W = values[0];
                    //definition
                    for (int i = 1; i <= cols - 1; i++)
                    {
                        defs.Add(values[i]);
                        //Definition d = new Definition();
                        //d.Def = values[i];
                        //d.WordObj = w;
                    }
                                      
                    var ob = new Words(word, defs, "csv");
                    words.Add(ob);
                    //defs.Clear();
                }
            }
            foreach (var item in words)
            {
                if (item != null)
                {
                   // Console.WriteLine("{0} ", item.Word);

                }
            }
            return words;
        }
    }
}
