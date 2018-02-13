using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.App;
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

        public string GetScore()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(path)))
                {
                    // var result = connection.Query<ScoreTable>("select 'Score' as 'Scores', 'NumberOfAnswers' as 'Answers' from WordTable");
                    var result = connection.Query<ScoreTable>("select Score as Scores, NumberOfAnswers as Answers from WordTable");
                    int scores = result.Sum(i => i.Scores);
                    int answers = result.Sum(i => i.Answers);
                    return Application.Context.GetString(Resource.String.yourScore) + scores.ToString() + Application.Context.GetString(Resource.String.points) + answers.ToString() + Application.Context.GetString(Resource.String.answers);
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return null;
            }
        }

        public bool CheckIfNewWordsNeeded()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(path)))
                {
                    // var result = connection.Query<ScoreTable>("select 'Score' as 'Scores', 'NumberOfAnswers' as 'Answers' from WordTable");
                    var result = connection.Query<ScoreTable>("select Score as Scores, NumberOfAnswers as Answers from WordTable");
                    int scores = result.Sum(i => i.Scores);
                    int answers = result.Sum(i => i.Answers);
                    int count = 0;
                    foreach(var item in result)
                    {
                        if (item.Scores > 3)
                            count++;
                    }

                    var statistic = count / result.Count();
                    // jeśli ilość słów, na które użytkownik odpowiedział conajmniej trzy razy poprawnie jest więcej niż 80% => zwraca true
                    if (statistic > 0.8 && count>80)
                    {
                        return true;
                    }
                    else return false;

                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
        }

        public bool UpdateTableWord(int score, WordTable word)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(path)))
                {
                    if(score!=-1)
                    {
                        connection.Query<WordTable>("UPDATE WordTable set Score=?, NumberOfAnswers=? where Id=?", score, ++word.NumberOfAnswers, word.Id);
                        var s = connection.Query<WordTable>("SELECT * FROM WordTable where Id=?", word.Id);
                    }
                    else connection.Query<WordTable>("UPDATE WordTable set NumberOfAnswers=? where Id=?", ++word.NumberOfAnswers, word.Id);
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

        public bool CheckIfDatabaseEmpty()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(path)))
                {
                    SQLite.TableMapping map = new TableMapping(typeof(WordTable)); // Instead of mapping to a specific table just map the whole database type
                    object[] ps = new object[0]; // An empty parameters object since I never worked out how to use it properly! (At least I'm honest)

                    Int32 tableCount = connection.Query(map, "SELECT * FROM sqlite_master WHERE type = 'table' AND name = 'WordTable'", ps).Count; // Executes the query from which we can count the results
                    if (tableCount == 0)
                    {
                        return true;// database is empty
                    }
                    else
                    {
                        return false;
                    }
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