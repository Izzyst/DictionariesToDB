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
    [Activity(Label = "InfoActivity")]
    public class InfoActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MoreInfoLayout);
            this.Title =this.Resources.GetString(Resource.String.info_name);
            // Create your application here
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