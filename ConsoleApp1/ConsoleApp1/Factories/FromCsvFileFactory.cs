using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleApp1.Domain;

namespace ConsoleApp1.Factories
{
    public class FromCsvFileFactory : AbstractDictionary
    {
        public override List<Words> GetWords()
        {
            List<Words> words = new List<Words>();
            using (var fs = File.OpenRead(@"C:\Users\Izabela\Documents\words.csv"))
            using (var reader = new StreamReader(fs))
            {
                string word = "";
                List<string> defs = new List<string>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var cols = line.Split(',').Count();
                    var values = line.Split(',');
                    //Word
                    word = values[0];
                    //definition
                    for (int i = 1; i <= cols - 1; i++)
                    {
                        defs.Add(values[i]);
                    }
                    // Defs.ForEach(i => Console.WriteLine("{0}\t", i));

                    words.Add(new Words(word, defs));
                    defs.Clear();
                }
            }
            foreach (var item in words)
            {
                if (item != null)
                {
                    Console.WriteLine("{0}\t", item.Word);
                    //Console.WriteLine("{0}\t", item.Defs[0]);
                }
            }
            return words;
        }
    }
}
