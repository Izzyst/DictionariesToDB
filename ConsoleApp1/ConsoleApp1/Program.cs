using System;
using System.Collections.Generic;
using ConsoleApp1.Domain;
using ConsoleApp1.Entities;
using ConsoleApp1.Factories;
using FluentNHibernate.Testing.Values;
using NHibernate.Mapping;
using NHibernate;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            //InsertWordToDatabase(GetWordsFromFactory("csv"));
            //InsertWordToDatabase(GetWordsFromFactory("Excel"));
           // List<Words> list = new List<Words>();
           // list.AddRange(GetWordsFromFactory("Polish"));
            //list.AddRange(GetWordsFromFactory("English"));
            // InsertWordToDatabase(list);
            // InsertWordToDatabase(list);
            GetFromDatabase();
            Console.ReadKey();
        }

        static void GetFromDatabase()
        {
            var sessionFactory = NHibernateHelper.CreateSessionFactory();
            ISessionFactory factory = new NHibernate.Cfg.Configuration().Configure().BuildSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {

                ICriteria criteria = session.CreateCriteria(typeof(Word));
                IList<Word> list = criteria.List<Word>();
                foreach (var item in list)
                {
                    Console.WriteLine("Id: {0}, Slowo: {1}, Jezyk: {2}", item.Id, item.W, item.Lang);
                }


            }

            /*
             using (mySession.BeginTransaction())
            {
                // Poniżej trworzymy kryteria pobierania danych z tabeli
                ICriteria criteria = mySession.CreateCriteria<Car>();
                IList<Car> list = criteria.List<Car>();
                // Gdybyśmy chcieli zdefiniować warunki wyszukiwania wystarczy zrobić to w poniższy sposób
                // IList<Car> list = criteria.List<Car>().Where(a => a.CarId > 3).ToList();
                foreach (var item in list)
                {
                    Console.WriteLine("Id: {0}, Marka: {1}, Model: {2}", item.CarId, item.Brand, item.Model);
                }
            }
            */

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
                        var w = new Word { W = word.Word, Lang=word.Language};

                        foreach (var def in word.Defs)
                        {
                            var d = new Definition { Def = def };
                            w.AddDefinition(d);
                        }
                        //session.SaveOrUpdate(w);
                        session.Save(w);
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
