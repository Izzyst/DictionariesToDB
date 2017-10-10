using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace ConsoleApp1
{
    public class NHibernateHelper
    {
        /*  public static ISessionFactory CreateSessionFactory()
          {

              return Fluently.Configure()
                  .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c=>c.FromConnectionStringWithKey("localDB")))
                  .Mappings(m => m.FluentMappings
                      .AddFromAssemblyOf<Program>())
                      .ExposeConfiguration(cfg=>new SchemaExport(cfg)
                      .Create(true,true))
                      .BuildSessionFactory();

          }

          private static MySQLConfiguration CreateMysqlConfig()
          {
              return MySQLConfiguration.Standard
                  .ShowSql()
                  .ConnectionString(c => c
                      .Database("WCF")
                      .Server("localhost")
                      .Password("")
                      .Username("root"));

          }*/
        private static ISessionFactory _sessionFactory;

        /* public static ISessionFactory SessionFactory
         {
             get
             {
                 if (_sessionFactory == null)
                 {
                     InitializeSessionFactory();
                 }
                 return _sessionFactory;
             }
         }*/
        public static ISessionFactory CreateSessionFactory()
        {
           return  _sessionFactory = Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString
                    ("Data Source=ROCA-BLANDA\\SQLEXPRESS01;Initial Catalog=WCF;Integrated Security=True").ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, true)).BuildSessionFactory();
        }
    

    /* public static ISession OpenSession()
     {
         return SessionFactory.OpenSession();
     }*/
    }
}
