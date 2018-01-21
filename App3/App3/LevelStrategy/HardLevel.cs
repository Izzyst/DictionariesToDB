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
            list.AddRange(db.SelectRandomWord());
            list.AddRange(db.SelectRandomWord());
            var rnd = new Random();
            int x = rnd.Next(0, 3);
            data.Id = list[x].Id;
            data.Def = list[x].Def;
            data.WordList = list;

            return data;
        }

    }
}