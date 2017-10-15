using System;
using System.Collections.Generic;
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
           var sessionFactory = NHibernateHelper.CreateSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    // create a couple of Stores each with some Products and Employees
                    var barginBasin = new Word {W = "Table"};
                    var superMart = new Word {W = "Bar"};

                    var potatoes = new Definition {Def = "stol"};
                    var fish = new Definition {Def = "bar"};
                    var milk = new Definition {Def = "belka"};
                    var bread = new Definition {Def = "listwa"};
                    var cheese = new Definition {Def = "baton"};


                    // add products to the stores, there's some crossover in the products in each
                    // store, because the store-product relationship is many-to-many
                    AddDefToWord(barginBasin, potatoes, fish, milk, bread, cheese);
                    AddDefToWord(superMart, bread, cheese);

                    // save both stores, this saves everything else via cascading
                    session.SaveOrUpdate(barginBasin);
                    session.SaveOrUpdate(superMart);
                    /*FromExcelFileFactory ob = new FromExcelFileFactory();
                    var list = ob.GetWords();

                    foreach (var word in list)
                    {
                        var w = new Word {W = word.Word};

                        foreach (var def in word.Defs)
                        {
                            var d = new Definition {Def = def};
                            w.AddDefinition(d);
                        }
                    }*/

                    transaction.Commit();
                }

                // retreive all stores and display them
                using (session.BeginTransaction())
                {
                    var stores = session.CreateCriteria(typeof(Word))
                        .List<Word>();

                    foreach (var store in stores)
                    {
                        WriteStorePretty(store);
                    }
                }

                Console.ReadKey();
            }
            
            Console.ReadKey();
        }

        private static void WriteStorePretty(object store)
        {
            Console.WriteLine("{0}", store.ToString());
        }

        public static void AddDefToWord(Word word, params Definition[] defs)
        {
            foreach (var def in defs)
            {
                word.AddDefinition(def);
            }
        }


    }
}
