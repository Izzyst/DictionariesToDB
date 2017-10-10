using System;
using System.Collections.Generic;

namespace ConsoleApp1.Domain
{
    public class Words
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public  List<string> Defs { get;  }

        public Words(string word, List<string> defs)
        {
            this.Word = word;
            this.Defs = defs;
        }

        public void ToString()
        {
            Console.WriteLine(this.Word + ": \t");
            this.Defs.ForEach(i => Console.WriteLine("{0}\t", i));
        }
    }
}
