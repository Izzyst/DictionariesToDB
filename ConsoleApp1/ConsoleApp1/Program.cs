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
            //List<Words> list = new List<Words>();
            //list.AddRange(GetWordsFromFactory("Polish"));
            //list.AddRange(GetWordsFromFactory("English"));

            //NHibernateHelper helper = new NHibernateHelper();
            //NHibernateHelper.GetAllData("pl");

            Console.ReadKey();
        }


        static List<Words> GetWordsFromFactory(string typeOfDictionary)
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
