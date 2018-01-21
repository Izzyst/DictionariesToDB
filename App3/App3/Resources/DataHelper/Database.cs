﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.Util;
using App3.Models;
using SQLite;

namespace App3.Resources.DataHelper
{
    public class Database
    {
        string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDb.db3");

        public bool CreateDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(path))
                {
                    connection.CreateTable<WordTable>();               
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
        }

        public bool DropTable()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(path)))
                {
                    connection.Query<WordTable>("DROP TABLE IF EXISTS WordTable");
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
            
        }

        public bool InsertIntoTableWord(WordTable word)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(path)))
                {
                    connection.Insert(word);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
        }

        public List<WordTable> SelectTableWord()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(path)))
                {
                    return connection.Table<WordTable>().ToList();
                    
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return null;
            }
        }

        public List<WordTable> SelectRandomWord()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(path)))
                {
                    return connection.Query<WordTable>("SELECT * FROM WordTable ORDER BY RANDOM() LIMIT 1");
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return null;
            }
        }

        public bool UpdateTableWord(WordTable word)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(path)))
                {
                   // connection.Query<Word>("UPDATE Word set IdW=?, W=?, Def=?, Lang=? where Id=?", word.IdW, word.W, word.Def, word.Lang, word.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
        }

        public bool DeleteFromTableWord(WordTable word)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(path)))
                {
                    connection.Delete(word);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
        }

        public bool SelectQueryTableWord(string lang)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(path)))
                {
                    connection.Query<WordTable>("SELECT * FROM Word where Lang=?", lang);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
        }
    }
}