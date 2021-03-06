﻿using System;
using System.Collections.Generic;
using App3.Models;
using App3.Resources.DataHelper;

namespace App3.LevelStrategy
{
    public class EasyLevel : IStrategy
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public DataToLevel GetWords()
        {
            List<WordTable> list = new List<WordTable>();
            DataToLevel data = new DataToLevel();
            //list = GenerateExampleList();
            Database db = new Database();
            list.AddRange(db.SelectRandomWord());
            data.Id = list[0].IdWordJson;
            data.Def = list[0].Def;
            data.Score = list[0].Score;
            data.NumberOfAnsw = list[0].NumberOfAnswers;

            data.WordList = list;

            return data;
        }

    }
}