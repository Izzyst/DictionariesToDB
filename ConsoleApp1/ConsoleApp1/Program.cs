using System;
using System.Collections.Generic;
using ConsoleApp1.Domain;
using ConsoleApp1.Factories;
using System.Web.Script.Serialization;
using System.IO;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Words> list = new List<Words>();
            list.AddRange(collection: Program.GetWordsFromFactory("EnglishW"));

            var json = new JavaScriptSerializer().Serialize(list);
            Console.WriteLine(json);

            using (StreamWriter writetext = new StreamWriter("write.txt"))
            {
                writetext.Write(json);
            }

            Console.ReadKey();
        }


        public static List<Words> GetWordsFromFactory(string typeOfDictionary)
        {
            AbstractDictionary factory;
            switch (typeOfDictionary)
            {
                case "csv":
                    factory = new FromCsvFileFactory();
                    return factory.GetWords();
                case "Excel":
                    factory = new FromExcelFileFactory();
                    return factory.GetWords();
                case "Polish":
                    factory = new PolishEWordsFactory();
                    return factory.GetWords();
                case "PolishK":
                    factory = new PolishWordsFactory();
                    return factory.GetWords();
                case "English":
                    factory = new EnglishWordsFactory();
                    return factory.GetWords();
                case "EnglishW":
                    factory = new EnglishWikiFactory();
                    return factory.GetWords();
                default: throw new NotImplementedException();
            }

        }

    }
}
