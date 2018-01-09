using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App3.Models;

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
            List<Words> list = new List<Words>();
            DataToLevel data = new DataToLevel();
            list = GenerateExampleList();
            data.Id = list[0].Id;
            data.Def = list[0].Defs[0];
            data.WordList = list;

            return data;
        }

        public List<Words> GenerateExampleList()
        {
            List<Words> list = new List<Words>();

            Words w1tmp = new Words(0, "home", "place where you live", "ang");
            list.Add(w1tmp);
            return list;
        }
    }
}