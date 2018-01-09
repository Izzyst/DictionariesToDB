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
            // list.AddRange(collection: Program.GetWordsFromFactory("English"));
            //  NHibernateHelper.InsertWordToDatabase(list);

            //string url = @"http://api.wunderground.com/api/02e5dd8c34e3e657/geolookup/conditions/forecast/q/Dhaka,Bangladesh.json";

            //using (var client = new HttpClient())
            //{
            //    var result = await client.GetStringAsync(url);
            //    return JsonConvert.DeserializeObject<YourModelForTheResponse>(result);
            //}
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
