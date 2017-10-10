using System;
using System.Collections.Generic;
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
            try
            {
                var webGet = new HtmlWeb();
                var doc = webGet.Load(html);
                string html2 = "";
                int max = 6;
                int min = 0;
                int i = rnd.Next(min, max);
                //HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//ul[@class='columns2 browse-list']/li/a");
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(path);
                if (nodes == null)
                    throw new Exception("No matching nodes found!");
                else
                {
                    while (amount > 0)
                    {
                        int j = rnd.Next(0, nodes.Count);
                        html2 = domain + nodes[j].Attributes["href"].Value;
                        links.Add(html2.ToString());
                        amount--;
                    }
                }
                return links;
            }
            catch (Exception)
            {
                throw;
            }
            // return links;
        }

        //usuwanie tagów html
        public static string Strip(string text)
        {
            //usuwanie komentarzy 
            text = Regex.Replace(text, @"(<![^<]*>)", string.Empty);
            //usuwanie skryptów oraz arkuszy styli
            text = Regex.Replace(text, @"(<script[^<]*</script>)|(<style[^<]*</style>)|(&[^;]*;)", string.Empty);
            text = Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
            return text;
        }
    }
}
