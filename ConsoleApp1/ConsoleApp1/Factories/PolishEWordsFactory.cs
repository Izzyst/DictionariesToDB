using System;
using System.Collections.Generic;
using ConsoleApp1.Domain;
using HtmlAgilityPack;
using NHibernate.Util;

namespace ConsoleApp1.Factories
{
    public class PolishEWordsFactory : AbstractDictionary
    {
        private List<Words> words = new List<Words>();
        Random rnd = new Random();

        public override List<Words> GetWords()
        {

            List<string> linksAbc = new List<string>();

            Console.WriteLine("laduje linki dla slownika wyraów obcych");
            linksAbc = GetLinks("https://www.bryk.pl/slowniki/slownik-wyrazow-obcych",
                "//div[@class='topLetters']/div/ul[@class='dropdownLettersMobile']/li/a", "https://www.bryk.pl");
            foreach (var link in linksAbc)
            {
                GettingNodesfromUrl(link);
            }
            return words;

        }

        public List<string> GettingNodesfromUrl(string html)
        {

            List<string> linksToWords = new List<string>();
            var webGet = new HtmlWeb();
            var doc = webGet.Load(html);
            //pobranie ilości podstron dla danej kategorii litery
            HtmlNode node =
                doc.DocumentNode.SelectSingleNode("//div[@class='inline-element centered']/ul/li[last()]");
            string amount;
            if (node != null)
                amount = node.InnerText; // ilosc podstron dla danego przedzialu literowego
            else
            {
                amount = "0";
            }
            int iamount = Int32.Parse(amount);
            // pobranie nodes dla poszczególnych wyrazów i ich znaczeń dla randomowych podkategorii
            var x = 0;
            if (iamount == 0)
            {
                linksToWords = GetRandomLinksFromPage(html, 4, "//div[@class='entry-list']/div[@class='single-entry']/div/div/div/a",
                    "https://www.bryk.pl");
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    x = rnd.Next(1, iamount);
                    linksToWords.AddRange(GetRandomLinksFromPage(html + "/" + x, 5, "//div[@class='entry-list']/div[@class='single-entry']/div/div/div/a", "https://www.bryk.pl"));
                }
            }


            linksToWords.ForEach(l=> Console.WriteLine(l.ToString()));

            return linksToWords;
            //linksToWords = GetRandomLinksFromPage(link, 3, , "");
        }



    }
    
}
