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
using System.Linq;
using App3.Resources;

namespace App3.LevelStrategy
{
    public static class GettingItemsFromDatabase
    {
        public static int InsertWordsToSqlite()
        {
            string data= "";
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            string language = prefs.GetString("language_data", data);

            if (language == "Polish") language = "pl";
            else language = "eng";

            MainActivity.isWorking = true;
            string json;
            Database db;
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string url = "http://izzyst-001-site1.etempurl.com/Word/GetWords?language=" + language;
                    json = wc.DownloadString(url);
                    List<Word> items = JsonConvert.DeserializeObject<List<Word>>(json);
                    db = new Database();
                    db.DropTable();
                    db.CreateDatabase();
                    foreach (var item in items)
                    {
                        WordTable wordTable = new WordTable();
                        wordTable.IdWordJson = item.Id;
                        wordTable.W = FirstCharToUpper(item.W);
                        wordTable.Def = FirstCharToUpper(item.Def);
                        wordTable.Lang = item.Lang;
                        wordTable.Score = 0;
                        wordTable.NumberOfAnswers = 0;

                        
                        db.InsertIntoTableWord(wordTable);
                    }
                }              
                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
           
        }

        public static int InsertFromFileToSqlite(List<Words> items)
        {
            MainActivity.isWorking = true;
            Database db;
            try
            {
                using (WebClient wc = new WebClient())
                {
                    db = new Database();
                    db.DropTable();
                    db.CreateDatabase();
                    foreach (var item in items)
                    {

                        WordTable wordTable = new WordTable();
                        wordTable.IdWordJson = item.Id;
                        wordTable.W = FirstCharToUpper(item.Word);
                        wordTable.Def = FirstCharToUpper(item.Defs[0]);
                        wordTable.Lang = item.Language;
                        wordTable.Score = 0;
                        wordTable.NumberOfAnswers = 0;


                        db.InsertIntoTableWord(wordTable);
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static string GetScoresFromDatabase()
        {
            Database db = new Database();
            return db.GetScore();
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
                List<WordTable> w = db.SelectTableWord();
            }
            return list;
        }

        public static bool CheckIfDownloadDictionaryIsNeeded(string lang)
        {
            // spr czy baza jest pusta, jeśli nie to spr czy ten sam język, jeśli tak, to nie wykonuje pobierania danych
            Database db = new Database();
            var word = db.SelectRandomWord();//wybieram losowe słowo z podręcznej bazy danych
            if (word[0].Lang != ChangeDictionaryAlias(lang) || db.CheckIfDatabaseEmpty() == true)
            {
                return true;
            }
            else return false;
        }

        public static bool CheckIfDictionaryInDatabaseIsCorrect(string lang)
        {
            Database db = new Database();
            var word = db.SelectRandomWord();//wybieram losowe słowo z podręcznej bazy danych
            if (word[0].Lang != ChangeDictionaryAlias(lang))
            {
                return true;
            }
            else return false;
        }

        public static bool CheckIfDatabaseIsEmpty()
        {
            // spr czy baza jest pusta, jeśli nie to spr czy ten sam język, jeśli tak, to nie wykonuje pobierania danych
            Database db = new Database();
            var word = db.SelectRandomWord();
            if (db.CheckIfDatabaseEmpty() == true)
            {
                return true;
            }
            else return false;
        }


        public static string ChangeDictionaryAlias(string language)
        {
            if (language == "Polish" || language =="Polski") return  "pl";
            else return  "eng";
        }

        public static bool CheckIfNewWordsNeededToDownload()
        {
            Database db = new Database();
            if(db.CheckIfNewWordsNeeded()==true || db.CheckIfDatabaseEmpty() == true)
            {
                return true;
            }
            return false;
        }

        public static bool CheckIfDbEmpty()
        {
            Database db = new Database();
            if (db.CheckIfDatabaseEmpty() == true)
            {
                return true;
            }
            return false;
        }

        public static bool InsertFile(string path)
        {
            var typeOfFile = FileFactory.CheckTypeOfFile(path);
            List<Words> list = GetFromFile(typeOfFile, path);

            // spr czy odp rozszerzenie
            if (list.Count() != 0)
            {
                InsertFromFileToSqlite(list);
                return true;
            }
            return false;
        }

        public static List<Words> GetFromFile(string typeOfFile, string path)
        {
            switch(typeOfFile)
            {
                case "xls":
                    return new FromExcelFileFactory().GetWords(path);
                case "xlsx":
                    return new FromExcelFileFactory().GetWords(path);
                case "csv":
                    return new FromCsvFileFactory().GetWords(path);
                default: return new List<Words>();
            }
        }
    }
}