using System;
using System.Collections.Generic;
using ConsoleApp1.Domain;
using ConsoleApp1.Factories;


namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Words> list = new List<Words>();
            //List<string> defs = new List<string>();
            //defs.Add("def test");
           // list.AddRange(collection: Program.GetWordsFromFactory("English"));
            //Words w1 = new Words("table", defs, "pl");
            //Words w2 = new Words("table2", defs, "pl");
            //list.Add(w1);
            //list.Add(w2);
          //  NHibernateHelper.InsertWordToDatabase(list);

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
                case "English":
                    factory = new EnglishWordsFactory();
                    return factory.GetWords();
                default: throw new NotImplementedException();
            }

        }

    }
}
