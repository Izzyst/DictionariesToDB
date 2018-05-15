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
using App3.Resources.DataHelper;
using static Android.Widget.AdapterView;

namespace App3
{
    [Activity(Label = "StatisticsActivity")]
    public class StatisticsActivity : Activity
    {
        private List<string> list;
        ListView listView;
        TextView textView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Statisticslayout);
            this.Title = this.Resources.GetString(Resource.String.statistics_name);

            listView = FindViewById<ListView>(Resource.Id.listView1);
            textView = FindViewById<TextView>(Resource.Id.emptyDict);

            list = new List<string>();
            List<string> alphabeticaList = new List<string>();
            Database db = new Database();
            if (db.CheckIfDatabaseEmpty() == false)
            {
                textView.Visibility = Android.Views.ViewStates.Gone;
                List<WordTable> words = new List<WordTable>();
                words = db.SelectTableWord();


                var groupedList = words.GroupBy(i => i.W).Select(i => i).ToList();
                foreach(var item in groupedList)
                {
                    int score=0;
                    foreach(var item2 in item)
                    {
                        score += item2.Score;
                    }
                    list.Add(item.Last().W); //+ " - " + score.ToString() + "ptk");
                }
                alphabeticaList = list.OrderBy(s => s).ToList();
            }
            else
            {
                textView.Visibility= Android.Views.ViewStates.Visible;
            }

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, alphabeticaList);
            listView.Adapter = adapter;

            listView.ItemClick += (object sender, ItemClickEventArgs e) => {
                string selectedFromList = listView.GetItemAtPosition(e.Position).ToString();

                Intent intent = new Intent(Application.Context, typeof(RowActivity));
                intent.PutExtra("MyData", e.Position.ToString());
                Application.Context.StartActivity(intent);
            };
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.myMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.about:
                    Intent intent1 = new Intent(Application.Context, typeof(InfoActivity));
                    intent1.SetFlags(ActivityFlags.NewTask);
                    Application.Context.StartActivity(intent1);
                    return true;
                case Resource.Id.sett:
                    Intent intent2 = new Intent(Application.Context, typeof(SettingsActivity));
                    intent2.SetFlags(ActivityFlags.NewTask);
                    Application.Context.StartActivity(intent2);
                    return true;
                case Resource.Id.dict:
                    Intent intent3 = new Intent(Application.Context, typeof(StatisticsActivity));
                    intent3.SetFlags(ActivityFlags.NewTask);
                    Application.Context.StartActivity(intent3);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}