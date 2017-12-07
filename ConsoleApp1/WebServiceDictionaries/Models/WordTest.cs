using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceDictionaries.Models
{
    public class WordTest
    {
        public int Id { get; set; }
        public string W { get; set; }
        public string Lang { get; set; }
        public List<string> Defs { get; set; }
    }
}