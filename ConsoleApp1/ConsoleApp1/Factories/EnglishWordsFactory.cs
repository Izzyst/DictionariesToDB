using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp1.Domain;
using HtmlAgilityPack;

namespace ConsoleApp1.Factories
{
    public class EnglishWordsFactory : AbstractDictionary
    {
        Random rnd = new Random();
        private List<Words> words = new List<Words>();

        public override List<Words> GetWords()
        {
            List<string> links = new List<string>();
            links = this.GetLinks();
            Parallel.ForEach(links, item =>
            {
                var temp = gettingNodesfromURL(item);
                lock (words)//lock blokuje zasoby kolekci które są obecnie używane przez jeden z wątków
                {
                    words.Add(temp);
                }
            });
            return words;
        }


        public Words gettingNodesfromURL(string html)
        {
            List<string> definitions = new List<string>();
            var webGet = new HtmlWeb();
            var doc = webGet.Load(html);
            // Word:
            try
            {
                Words w;
                HtmlNode node = doc.DocumentNode.SelectSingleNode("//span[@class='orth']");
                // definitions for Word
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='def']");
                if (nodes == null)
                    throw new Exception("No matching nodes found!");
                else
                {
                    foreach (HtmlNode link in nodes)
                    {
                        definitions.Add(Strip(link.InnerText));
                    }
                    w = new Words(node.InnerText, definitions);
                    return w;
                }

            }
            catch (Exception)
            {
                //  throw;
                return null;
            }
        }

        public List<String> GetLinks()
        {
            List<String> links = new List<string>();
            Console.WriteLine("Pobieram linki dla liter: ...");
            List<string> subwords = new List<string>();
            var webGet = new HtmlWeb();
            var doc = webGet.Load("https://www.collinsdictionary.com/dictionary/english");
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//ul[@class='browse-letters']/li/a");
            string html = "";
            int count = 0;
            foreach (HtmlNode item in nodes)
            {
                html =  item.Attributes["href"].Value;// letters A-Z
                subwords = GetRandomLinksFromPage(html, 5, "//ul[@class='columns2 browse-list']/li/a", "");// zwraca liste słów zawierającą się w danym przedziale literowym np. ab..., abg...., ath...
                List<string> w = new List<string>();
                if (count == 0)
                    links.AddRange(subwords);// dodanie słów dla przedziału #             
                else// ponieważ słowa w przedziale # nie posiadają podprzedzialów, dlatego je omijam
                {
                    Parallel.ForEach(subwords, (sub) =>
                    {
                        w = GetRandomLinksFromPage(sub, 1, "//ul[@class='columns2 browse-list']/li/a", "");
                        lock (links)
                        {
                            links.AddRange(w);// AddRange - dopisuje liste do listy
                        }
                    });
                    /*   for (int i = 0; i < subwords.Count; i++)
                   {
                        w=getWords(subwords[i], 1);
                        words.AddRange(w);// AddRange - dopisuje liste do listy
                   }*/
                }

                count++;
            }

            // links.ForEach(i => file.WriteLine("{0}\t", i));
            return links;
        }

    }
}