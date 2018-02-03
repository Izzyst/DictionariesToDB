﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App3.Models;

namespace App3.Resources.DataHelper
{
    public class FromCsvFileFactory
    {
        public List<Words> GetWords(string path)
        {
            List<Words> words = new List<Words>();
            using (var fs = File.OpenRead(@path))
            using (var reader = new StreamReader(fs))
            {
                string word = "";
                List<string> defs;

                while (!reader.EndOfStream)
                {
                    defs = new List<string>();
                    var line = reader.ReadLine();
                    var cols = line.Split(',').Count();
                    var values = line.Split(',');
                    //Word
                    word = Strip(values[0]);

                    //definitions
                    for (int i = 1; i <= cols - 1; i++)
                    {
                        defs.Add(Strip(values[i]));
                    }

                    var ob = new Words(word, defs, "csv");
                    words.Add(ob);
                    //defs.Clear();
                }
            }

            return words;
        }
        protected static string Strip(string text)
        {
            //usuwanie komentarzy 
            text = Regex.Replace(text, @"([<>\?\*\\\""/\|])+", string.Empty);
            //usuwanie skryptów oraz arkuszy styli
            text = Regex.Replace(text, @"(<script[^<]*</script>)|(<style[^<]*</style>)|(&[^;]*;)", string.Empty);
            text = Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
            return text;
        }
    }
}