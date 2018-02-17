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
            Database db = new Database();
            if (db.CheckIfDatabaseEmpty() == false)
            {
                textView.Visibility = Android.Views.ViewStates.Gone;
                List<WordTable> words = new List<WordTable>();
                words = db.SelectTableWord();
                foreach (var item in words)
                {
                    list.Add(item.W + " - " + item.Score.ToString() + "ptk");
                }
            }
            else
            {
                textView.Visibility= Android.Views.ViewStates.Visible;
            }

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, list);
            listView.Adapter = adapter;

            listView.ItemClick += (object sender, ItemClickEventArgs e) => {
                string selectedFromList = listView.GetItemAtPosition(e.Position).ToString();

                Intent intent = new Intent(Application.Context, typeof(RowActivity));
                intent.PutExtra("MyData", e.Position.ToString());
                Application.Context.StartActivity(intent);
            };


        }
    }
}