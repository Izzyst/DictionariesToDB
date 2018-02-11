using System;
using System.Collections.Generic;
using App3.Models;
using App3.Resources.DataHelper;

namespace App3.LevelStrategy
{
    public class HardLevel : IStrategy
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public DataToLevel GetWords()
        {
            List<WordTable> list = new List<WordTable>();
            DataToLevel data = new DataToLevel();
            Database db = new Database();
            //list = GenerateExampleList();
            list.AddRange(db.SelectRandomWord());
            WordTable w2 = db.SelectRandomWord()[0];
            while (CheckIfDifferent(list[0], w2))
            {
                w2 = db.SelectRandomWord()[0];
            }

            list.Add(w2);

            WordTable w3 = db.SelectRandomWord()[0];
            while (CheckIfDifferent(list[0], w3))
            {
                w3 = db.SelectRandomWord()[0];
            }

            list.Add(w3);

            var rnd = new Random();
            int x = rnd.Next(0, 3);
            data.Id = list[x].Id;
            data.Def = list[x].Def;
            data.Score = list[x].Score;
            data.NumberOfAnsw = list[x].NumberOfAnswers;
            
            data.WordList = list;

            return data;
        }
        public bool CheckIfDifferent(WordTable word1, WordTable word2)
        {
            if (!word1.W.Contains(word2.W))
            {
                return false;
            }
            return true;
        }

    }
}