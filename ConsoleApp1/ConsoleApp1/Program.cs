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
                    FromExcelFileFactory ob = new FromExcelFileFactory();
                    var list = ob.GetWords();

                    foreach (var word in list)
                    {
                        var w = new Word {W = word.Word};

                        foreach (var def in word.Defs)
                        {
                            var d = new Definition {Def = def};
                            w.AddDefinition(d);
                        }
                        session.SaveOrUpdate(w);
                    }

                    transaction.Commit();
                }

            }
            
            Console.ReadKey();
        }



    }
}
