using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ConsoleApp1.Domain;
using HtmlAgilityPack;

namespace ConsoleApp1.Factories
{
    public abstract class AbstractDictionary
    {
        public abstract List<Words> GetWords();
        Random rnd = new Random();

        public List<string> GetRandomLinksFromPage(string html, int amount, string path, string domain)
        {
            List<String> links = new List<string>();

            var webGet = new HtmlWeb();
            var doc = webGet.Load(html);
            string html2 = "";
            //HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//ul[@class='columns2 browse-list']/li/a");
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(path);
            if (nodes == null)
                throw new Exception("No matching nodes found!");


            //int j = rnd.Next(0, nodes.Count);
            var j = Enumerable.Range(1, nodes.Count-1).OrderBy(x => rnd.Next()).Take(amount).ToList();
            foreach (var i in j)
            {
                html2 = domain + nodes[i].Attributes["href"].Value;

                links.Add(html2);
            }
            return links;
        }

        public virtual List<string> GetLinks(string html, string path, string domain)
        {
            List<String> links = new List<string>();
            var webGet = new HtmlWeb();
            var doc = webGet.Load(html);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(path);
            if (nodes == null)
                throw new Exception("No matching nodes found!");
            foreach (var node in nodes)
            {
                var html2 =domain + node.Attributes["href"].Value;
                if(CheckException(html2)== false)
                    links.Add(html2);
            }
            return links;
        }

        //usuwanie tagów html
        protected static string Strip(string text)
        {
            //usuwanie komentarzy 
            text = Regex.Replace(text, @"(<![^<]*>)", string.Empty);
            //usuwanie skryptów oraz arkuszy styli
            text = Regex.Replace(text, @"(<script[^<]*</script>)|(<style[^<]*</style>)|(&[^;]*;)", string.Empty);
            text = Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
            return text;
        }

        public virtual Words GetWordFromNode(string html, string wordPath, string definitionsPath)
        {
            List<string> definitions = new List<string>();
            var webGet = new HtmlWeb();
            var doc = webGet.Load(html);
            // Word:
            Words w;
            HtmlNode node = doc.DocumentNode.SelectSingleNode(wordPath);
            // definitions for Word
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(definitionsPath);
            if (nodes == null || node == null)
                return null;

            foreach (HtmlNode link in nodes)
            {
                definitions.Add(Strip(link.InnerText));
            }

            w = new Words(node.InnerText, definitions);
            return w;
        }

        private bool CheckException(string link)
        {
            if (link ==
                "http://anchoredgoods.com/?lib/my-wedding-vendor-workbook-organizer")
                return true;
            if (link ==
                "http://www.toonyunmusic.com/?ebooks/organizational-change-cross-functional-teams-foreword-by-francoise-chevalier-hec-paris-bert-a"
            )
                return true;
            if (link ==
                "http://www.vicelimoservices.com/books/mid-atlantic-lighthouses-2-nd-hudson-river-to-chesapeake-bay-lighthouse-series"
            )
                return true;
            if (link ==
                    "http://slownik-wyrazowobcych.eu/freebooks/squirrel-me-timbers-fiction-picture-books"
                )
                return true;
            return false;
        }
    }
}
