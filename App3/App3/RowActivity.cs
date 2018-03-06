﻿using System;
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
using App3.Resources.DataHelper;

namespace App3
{
    [Activity(Label = "RowActivity")]
    public class RowActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.rowlayout);
            this.Title = this.Resources.GetString(Resource.String.statistics_name);
            string text = Intent.GetStringExtra("MyData");
            TextView wordText = FindViewById<TextView>(Resource.Id.textView1);
            TextView defText = FindViewById<TextView>(Resource.Id.textView2);
            Database db = new Database();
            if (db.CheckIfDatabaseEmpty() == false)
            {
                List<WordTable> words = new List<WordTable>();
                List<WordTable> sortedList = new List<WordTable>();
                List<string> defs = new List<string>();
                words = db.SelectTableWord();
                int x = 0;
                if (Int32.TryParse(text, out x))
                {
                    //for (int i = 0; i < words.Count - 1; i++)
                    //{
                    //    if (words[i].W != words[i + 1].W)// tutaj powinno być spr czy w całej liście instnieje taki element -- group by
                    //    {
                    //        sortedList.Add(words[i]);
                    //    }
                    //}
                    List<WordTable> list = new List<WordTable>();
                    var groupedList = words.GroupBy(i => i.W).Select(i => i).ToList();
                    foreach (var item in groupedList)
                    {
                        list.Add(item.Last());
                    }

                    List<WordTable> alphabeticaList = list.OrderBy(s => s.W).ToList();
                    //int id = alphabeticaList[x].IdWordJson;
                    string word = alphabeticaList[x].W;
                    foreach (var item in words)
                    {
                        if (item.W == word)
                        {
                            defs.Add(item.Def);
                        }
                    }
                    wordText.Text = alphabeticaList[x].W;

                    string d = "";

                    for (int i = 0; i < defs.Count; i++)
                    {
                        d += System.Environment.NewLine +  "- " +  defs[i];
                    }

                    defText.Text = d;
                }
                
            }
            // Create your application here
        }
    }
}