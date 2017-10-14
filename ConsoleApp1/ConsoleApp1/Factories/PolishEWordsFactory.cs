using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp1.Domain;
using HtmlAgilityPack;

namespace ConsoleApp1.Factories
{
    public class PolishEWordsFactory : AbstractDictionary
    {
        private List<Words> words = new List<Words>();
        Random rnd = new Random();

        public override List<Words> GetWords()
        {
            List<string> linksAbc = new List<string>();
            List<string> linksToWords = new List<string>();
            Console.WriteLine("laduje linki dla slownika wyraów obcych");
            linksAbc = GetLinks("https://www.bryk.pl/slowniki/slownik-wyrazow-obcych",
                "//div[@class='topLetters']/div/ul[@class='dropdownLettersMobile']/li/a", "https://www.bryk.pl");
            foreach (var link in linksAbc)
            {
               linksToWords.AddRange(GettingLinksFromSubCategory(link));
            }

            Parallel.ForEach(linksToWords, toWord =>
            {
                var word = GetWordFromNode(toWord, "//div[@class='left-col-wrap']/div/h1",
                    "//div[@class='entry-top']/div[@class='explanation']/ol/li");
                if (word != null)
                {
                    lock (words)
                    {
                        words.Add(word);
                    }
                }

            });
            //words.ForEach(l => Console.WriteLine(l.Word + " - " + l.Defs[0]));
            return words;

        }

        public List<string> GettingLinksFromSubCategory(string html)
        {

            List<string> linksToWords = new List<string>();
            var webGet = new HtmlWeb();
            var doc = webGet.Load(html);
            //pobranie ilości podstron dla danej kategorii litery
            HtmlNode node =
                doc.DocumentNode.SelectSingleNode("//div[@class='inline-element centered']/ul/li[last()]");
            string amount;
            if (node == null)
                amount = "0";
            else
                amount = node.InnerText; // ilosc podstron dla danego przedzialu literowego
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
//            linksToWords.ForEach(l=> Console.WriteLine(l.ToString()));

            return linksToWords;
        }

    }
    
}
