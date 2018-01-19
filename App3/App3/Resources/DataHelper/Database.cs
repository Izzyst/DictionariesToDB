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
        //string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public bool CreateDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(path))
                {
                    connection.CreateTable<Word>();
                    
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
                    connection.Query<Word>("DROP TABLE IF EXISTS Word");
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
            
        }

        public bool InsertIntoTableWord(Word word)
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

        public List<Word> SelectTableWord()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(path)))
                {
                    return connection.Table<Word>().ToList();
                    
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return null;
            }
        }

        public List<Word> SelectRandomWord()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(path)))
                {
                    return connection.Query<Word>("SELECT * FROM Word ORDER BY RANDOM() LIMIT 1");

                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return null;
            }
        }

        public bool UpdateTableWord(Word word)
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

        public bool DeleteFromTableWord(Word word)
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
                    connection.Query<Word>("SELECT * FROM Word where Lang=?", lang);
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