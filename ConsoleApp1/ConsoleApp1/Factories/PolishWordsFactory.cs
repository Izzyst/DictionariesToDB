﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp1.Domain;
using HtmlAgilityPack;

namespace ConsoleApp1.Factories
{
    public class PolishWordsFactory : AbstractDictionary
    {
        private List<Words> words = new List<Words>();

        public override List<Words> GetWords()
        {
            List<string> links = new List<string>();
            links = this.GetLinksPL();
            Parallel.ForEach(links, item =>
            {
                var temp = gettingNodesFromURLPL(item);
                if (temp != null)
                {
                    lock (words)
                    {
                        words.AddRange(temp);// w tym przypadku dopisuje całą listę
                    }
                }
            });
            /*foreach (var item in words)
            {
                if (item != null)
                {
                    Console.WriteLine("{0}, {1}\t", item.Word, item.Defs[0]);
                }
            }*/
            return words;
        }

        

        public List<Words> gettingNodesFromURLPL(string html)
        {
            List<string> definitions = new List<string>();
            List<string> defs = new List<string>();
            var webGet = new HtmlWeb();
            var doc = webGet.Load(html);
            List<Words> w = new List<Words>();
            // Word:
            try
            {
                HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//td/b/font[@class='10ptGeorgia']");// słowa
                // definitions for Word
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//td/font[@class='10ptGeorgia']");// definicje dla danego słowa
                if (nodes != null && node != null)
                {
                    for (int i = 0; i < node.Count; i++)
                    {
                        if (node[i] != null)
                        {
                            if (nodes[i] != null)
                            {
                                if (nodes[i].InnerText.Length > 10)
                                {
                                    List<String> d = new List<String>();


                                    defs.Add(Strip(node[i].InnerText));
                                    definitions.Add(Strip(nodes[i].InnerText));
                                    d.Add(definitions.Last());
                                    //Console.WriteLine(Defs.Last() + " - " + definitions.Last());
                                    Words n = new Words(defs.Last(), d);
                                    w.Add(n);
                                }
                            }
                        }
                    }
                    return w;
                }
                else
                {
                    // return null;
                    throw new Exception("No matching nodes found!");
                }
            }
            catch (Exception)
            {
                //  throw;
                return null;
            }
        }

        public List<String> GetLinksPL()
        {
            List<String> links = new List<string>();
            List<string> subLinks = new List<string>();
            var webGet = new HtmlWeb();
            var doc = webGet.Load("http://www.slownik-online.pl/indeks_hasel.php");
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@align='CENTER']/a");
            string html = "";
            int amountOfWords = 5;
            Console.WriteLine("Laduje linki dla Kopalinskiego: ...");
            foreach (HtmlNode item in nodes)
            {
                html = "http://www.slownik-online.pl" + item.Attributes["href"].Value;// letterz A-Z

                subLinks = GetRandomLinksFromPage(html, amountOfWords, "//td[@valign='top']/a", "http://www.slownik-online.pl");// zwraca liste słów zawierającą się w danym przedziale literowym np. a,b,...,z
                                                                                                                                // subwords.ForEach(i => Console.WriteLine("{0}", i ));
                links.AddRange(subLinks);// AddRange - dopisuje liste do listy
            }
            return links;
        }
    }
}
