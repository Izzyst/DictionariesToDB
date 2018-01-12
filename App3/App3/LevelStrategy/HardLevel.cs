﻿using System;
using System.Collections.Generic;
using App3.Models;

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
            List<Words> list = new List<Words>();
            DataToLevel data = new DataToLevel();
            list = GenerateExampleList();
            var rnd = new Random();
            int x = rnd.Next(0, 3);
            data.Id = list[x].Id;
            data.Def = list[x].Defs[0];
            data.WordList = list;

            return data;
        }

        public List<Words> GenerateExampleList()
        {
            List<Words> list = new List<Words>();

            Words w1tmp = new Words(0, "home", "place where you live", "ang");
            Words w2tmp = new Words(1, "bedroom", "room where you sleep", "ang");
            Words w3tmp = new Words(2, "bathroom", "room where you take a bath", "ang");
            list.Add(w1tmp);
            list.Add(w2tmp);
            list.Add(w3tmp);

            return list;
        }

    }
}