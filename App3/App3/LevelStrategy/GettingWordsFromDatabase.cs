using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Content.Res;
using App3.Models;
using App3.Resources.DataHelper;
using Newtonsoft.Json;

namespace App3.LevelStrategy
{
    public static class GettingWordsFromDatabase
    {
        public static List<Words> GetWords(string language, int amount)
        {
            List<Words> list = new List<Words>();

            //using (WebClient wc = new WebClient())
            //{
            //    var json = wc.DownloadString("http://jsonplaceholder.typicode.com/posts");

            //}

            // Read the contents of our asset
            string json;
            Database db;
            AssetManager assets = Application.Context.Assets;// Asset umożliwia otwieranie plików na systemie android
            using (StreamReader sr = new StreamReader(assets.Open("pl.json")))
            {
                json = sr.ReadToEnd();
                dynamic items = JsonConvert.DeserializeObject<List<Word>>(json);
                db = new Database();
                db.CreateDatabase();
                foreach(var item in items)
                {
                    db.InsertIntoTableWord(item);
                }

                List<Word> w = db.SelectTableWord();
            }

            return list;
        }


    }
}