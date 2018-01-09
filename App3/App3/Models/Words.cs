using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App3.Models
{
    public class Words
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public List<string> Defs { get; }
        public string Language { get; set; }

        public Words(string word, List<string> defs, string Language)
        {
            this.Word = word;
            this.Defs = defs;
            this.Language = Language;
        }

        public void ToString()
        {
            Console.WriteLine(this.Word + ": \t");
            this.Defs.ForEach(i => Console.WriteLine("{0}\t", i));
        }
    }
}