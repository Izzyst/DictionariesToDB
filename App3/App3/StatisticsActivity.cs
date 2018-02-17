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

namespace App3
{
    [Activity(Label = "StatisticsActivity")]
    public class StatisticsActivity : Activity
    {
        private List<string> list;
        ListView listView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Statisticslayout);
            this.Title = this.Resources.GetString(Resource.String.statistics_name);

            listView = FindViewById<ListView>(Resource.Id.listView1);

            list = new List<string>();
            list.Add("słowo 1");
            list.Add("słowo 2");
            list.Add("słowo 3");
            list.Add("słowo 1");
            list.Add("słowo 2");
            list.Add("słowo 3");
            list.Add("słowo 1");
            list.Add("słowo 2");
            list.Add("słowo 3");
            list.Add("słowo 1");
            list.Add("słowo 2");
            list.Add("słowo 3");
            list.Add("słowo 1");
            list.Add("słowo 2");
            list.Add("słowo 3");
            list.Add("słowo 1");
            list.Add("słowo 2");
            list.Add("słowo 3");

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this,Android.Resource.Layout.SimpleListItem1, list);
            listView.Adapter = adapter;

        }
    }
}