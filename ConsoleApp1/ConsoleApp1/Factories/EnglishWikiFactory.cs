using ConsoleApp1.Domain;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1.Factories
{
    public class EnglishWikiFactory : AbstractDictionary
    {
        Random rnd = new Random();
        private List<Words> words = new List<Words>();

        public override List<Words> GetWords()
        {
            //TestFunction();
            List<string> linksABC = new List<string>();
            List<string> links = new List<string>();
            linksABC = this.GetAlphabeticalLinks();
            links = GetLinksToWords(linksABC);
            var j = Enumerable.Range(1, links.Count - 1).OrderBy(x => rnd.Next()).Take(400).ToList();
            foreach (var item in j)
            {
                var temp = GetWordFromNode("https://pl.wiktionary.org" + links[item] + "#en", "//h1[@id='firstHeading']", "//html/body/div[3]/div[3]/div[4]/div/dl[3]", "eng");
                if (temp != null)
                {
                    words.Add(temp);
                }

            }
            return words;
        }

        private List<string> GetAlphabeticalLinks()
        {
            List<string> alphabeticalLinks = new List<string>();
            var webGet = new HtmlWeb();
            string html = "";

            var doc = webGet.Load("https://pl.wiktionary.org/w/index.php?title=Kategoria:phrasal_verbs&from=A#mw-pages");
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//center/p/a[@class='external text']");
            

            foreach (var item in nodes)
            {
                html = item.Attributes["href"].Value;// letters A-Z
                html = Regex.Replace(html, @"(amp;)", string.Empty);
                if(!(html.Contains("X#mw-pages") || html.Contains("Y#mw-pages") || html.Contains("x#mw-pages") || html.Contains("y#mw-pages")) && alphabeticalLinks.Count<24)
                    alphabeticalLinks.Add(html);
            }

            return alphabeticalLinks;

        }

        private List<string> GetLinksToWords(List<string> alphabeticalLinks)
        {
            List<string> linksToWords = new List<string>();
            var webGet = new HtmlWeb();
            string html = "";

            foreach (var item in alphabeticalLinks)
            {
                webGet = new HtmlWeb();
                var doc = webGet.Load("https:" + item);
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div/div[@class='mw-content-ltr']/div/div[1]/ul/li/a");
                if(nodes !=null)
                    foreach (var item2 in nodes)
                    {
                        html = item2.Attributes["href"].Value;// 
                        linksToWords.Add(html);
                    }
            }

            return linksToWords;

        }

        private void TestFunction()
        {
            var webGet = new HtmlWeb();
            var doc = webGet.Load("https://pl.wiktionary.org/wiki/block_up#en");
            HtmlNode node = doc.DocumentNode.SelectSingleNode("//html/body/div[3]/div[3]/div[4]/div/dl[3]");
            string text = node.InnerText;
            HtmlNode node2 = doc.DocumentNode.SelectSingleNode("//h1[@id='firstHeading']");
            string text2 = node2.InnerText;
        }
    }
}
