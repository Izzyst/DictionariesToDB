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
using Newtonsoft.Json;
using SQLite;

namespace App3.Models
{

    public class WordsJson
    {
        [JsonProperty("Word")]
        public Word Word { get; set; }
    }

    public class Word
    {
      //  public int Id { get; set; }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("W")]
        public string W { get; set; }

        [JsonProperty("Defs")]
        public string Def { get; set; }

        [JsonProperty("Lang")]
        public string Lang { get; set; }
    }

    [Table("WordTable")]
    public class WordTable
    {
        [Column("Id")]
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [Column("IdWordJson")]
        public int IdWordJson { get; set; }

        [Column("W")]
        public string W { get; set; }

        [Column("Def")]
        public string Def { get; set; }

        [Column("Lang")]
        public string Lang { get; set; }

        [Column("Score")]
        public int Score { get; set; }

        [Column("NumberOfAnswers")]
        public int NumberOfAnswers { get; set; }

        
    }
}
