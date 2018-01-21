using System.Collections.Generic;

namespace App3.Models
{
    public class DataToLevel
    {
        public int Id { get; set; }
        public string Def { get; set; }

        public int Score { get; set; }

        public int NumberOfAnsw { get; set; }
        public List<WordTable> WordList { get; set; }

    }
}