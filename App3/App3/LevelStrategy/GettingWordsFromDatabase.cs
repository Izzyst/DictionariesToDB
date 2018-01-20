using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Android.App;
using Android.Content.Res;
using App3.Models;
using App3.Resources.DataHelper;
using Newtonsoft.Json;
using System.Threading;
using Android.OS;
using Android.Widget;
using Android.Content;
using Android.Preferences;

namespace App3.LevelStrategy
{
    public static class GettingWordsFromDatabase
    {
        public static int InsertWordsToSqlite()
        {
            string data= "";
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            string language = prefs.GetString("language_data", data);

            if (language == "Polish") language = "pl";
            else language = "eng";

            MainActivity.isWorking = true;
            List<Words> list = new List<Words>();
            string json;
            Database db;
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string url = "http://izzyst-001-site1.etempurl.com/Word/GetWords?language=" + language;
                    json = wc.DownloadString(url);
                    dynamic items = JsonConvert.DeserializeObject<List<Word>>(json);
                    db = new Database();
                    db.DropTable();
                    db.CreateDatabase();
                    foreach (var item in items)
                    {
                        db.InsertIntoTableWord(item);
                    }
                }              
                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
           
        }

        public static List<Words> GetWords(string language, int amount)
        {
            List<Words> list = new List<Words>();
            string json;
            Database db;

            // Read the contents of our asset
            //string json;
            
            AssetManager assets = Application.Context.Assets;// Asset umożliwia otwieranie plików na systemie android
            using (StreamReader sr = new StreamReader(assets.Open("pl.json")))
            {
                json = sr.ReadToEnd();
                dynamic items = JsonConvert.DeserializeObject<List<Word>>(json);
                db = new Database();
                db.CreateDatabase();
                foreach (var item in items)
                {
                    db.InsertIntoTableWord(item);
                }

                List<Word> w = db.SelectTableWord();
            }

            return list;
        }


    }
}