﻿using System;
using System.Collections.Generic;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System.Data.SqlClient;
using ConsoleApp1.Domain;
using System.Linq;

namespace WebServiceDictionaries.Models
{
    public static class NHibernateHelper
    {       

        public static ISessionFactory sessionFactory;

        public static ISessionFactory CreateSessionFactory()
        {
            return sessionFactory = Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString
                     ("Data Source=ROCA-BLANDA\\SQLEXPRESS01;Initial Catalog=WCF;Integrated Security=True").ShowSql())
                 .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Word>().AddFromAssemblyOf<Definition>())
                 // .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, true))// odpowiada za nadipywanie bazy
                 .BuildSessionFactory();
        }

        public static void InsertWordToDatabase(List<Words> list)
        {
            var sessionFactory = NHibernateHelper.CreateSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    foreach (var word in list)
                    {
                        var w = new Word { W = word.Word, Lang = word.Language };

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
        // dpisac opcje wybierającą dany język z bazy danych i nie powtarzające się elementy
        public static IList<Word> GetAllData(string language)
        {
            var sessionFactory = NHibernateHelper.CreateSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    ICriteria criteria = session.CreateCriteria<Word>();
                    IList<Word> list = criteria.List<Word>();
                    ICriteria criteria2 = session.CreateCriteria<Definition>();
                    IList<Definition> list2 = criteria2.List<Definition>();
                    // Gdybyśmy chcieli zdefiniować warunki wyszukiwania wystarczy zrobić to w poniższy sposób
                    // IList<Car> list = criteria.List<Car>().Where(a => a.CarId > 3).ToList();
                   /* foreach (var item in list)
                    {
                        Console.WriteLine("Id: {0}, Marka: {1}, Model: {2}", item.Id, item.W, item.Lang);

                    }
                    foreach (var item in list2)
                    {
                        Console.WriteLine("Id: {0}, Marka: {1}, Model: {2}", item.Id, item.Def, item.WordObj.Id);

                    }*/

                    return list;
                }
            }
           
        }

        public static List<WordTest> GetRandomWordsFromDictionary(string language)
        {
            var sessionFactory = NHibernateHelper.CreateSessionFactory();
            language = "pl";// rozwiązanie chwilowe, nie działa przekazywanie parametru
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    ICriteria criteria = session.CreateCriteria<Word>();
                    //IList<Word> list = criteria.List<Word>();
                    ICriteria criteria2 = session.CreateCriteria<Definition>();
                    IList<Definition> list2 = criteria2.List<Definition>();
                    // Gdybyśmy chcieli zdefiniować warunki wyszukiwania wystarczy zrobić to w poniższy sposób
                    List<Word> list = criteria.List<Word>().Where(a => a.Lang == language).ToList();

                    var innerJoinQuery =
                        from words in list
                        join definitions in list2 on words.Id equals definitions.WordObj.Id
                        select new { id = words.Id, word = words.W, definition = definitions.Def };

                    
                    List<WordTest> wordsTest = new List<WordTest>();

                    foreach (var item in list)
                    {
                        List<Definition> definitions = new List<Definition>();
                        var w = new WordTest();
                        w.Id = item.Id;
                        w.W = item.W;
                        w.Lang = item.Lang;
                        //definitions.Add(item.Defs);
                        w.Defs.Add(item.Defs[0]?.Def);
                        wordsTest.Add(w);

                    }


                    return wordsTest;
                }

            }
        }


        public static void DeleteData()
        {
            // Usunięcie wszystkich rekordów
            using (ISession session = sessionFactory.OpenSession())
            {
                SqlConnection con = session.Connection as SqlConnection;
                SqlCommand cmd = new SqlCommand("Delete from Word, Definition", con);
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Rezultat operacji: Dane zostały usunięte z tabel Word, Definition");
            Console.WriteLine();
 
        }

        

    }
}