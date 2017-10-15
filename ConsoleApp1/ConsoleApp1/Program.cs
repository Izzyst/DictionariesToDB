using System;
using System.Collections.Generic;
using ConsoleApp1.Domain;
using ConsoleApp1.Entities;
using ConsoleApp1.Factories;
using FluentNHibernate.Testing.Values;
using NHibernate.Mapping;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            //InsertWordToDatabase(GetWordsFromFactory("csv"));
            //InsertWordToDatabase(GetWordsFromFactory("Excel"));
            InsertWordToDatabase(GetWordsFromFactory("Polish"));
            //InsertWordToDatabase(GetWordsFromFactory("English"));
            Console.ReadKey();
        }

        static void InsertWordToDatabase(List<Words> list)
        {
            var sessionFactory = NHibernateHelper.CreateSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    foreach (var word in list)
                    {
                        var w = new Word { W = word.Word };

                        foreach (var def in word.Defs)
                        {
                            var d = new Definition { Def = def };
                            w.AddDefinition(d);
                        }
                        session.SaveOrUpdate(w);
                    }

                    transaction.Commit();
                }

            }
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
