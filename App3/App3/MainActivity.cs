using Android.App;
using Android.Widget;
using Android.OS;
using App3.utils;
using System;
using Android.Content;
using Android.Preferences;
using System.Collections.Generic;
using App3.LevelStrategy;
using Android.Net;
using System.Threading.Tasks;


namespace App3
{
    [Activity(Label = "Linguistic lock screen", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public static bool isWorking;
        TextView scores;
        TextView statText;
        TextView infoText;
        TextView settingsText;

        public static string level;
        ISharedPreferences prefs;
        ISharedPreferencesEditor editor;
        int isDbCreated = 0;
        int firstShown = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);       

            statText = FindViewById<TextView>(Resource.Id.statistictextView);
            infoText = FindViewById<TextView>(Resource.Id.infotextView);
            settingsText = FindViewById<TextView>(Resource.Id.settingstextView);
            scores = FindViewById<TextView>(Resource.Id.textView1);

            statText.Click += delegate
            {
                Intent intent = new Intent(Application.Context, typeof(StatisticsActivity));
                intent.SetFlags(ActivityFlags.NewTask);
                Application.Context.StartActivity(intent);
            };

            infoText.Click += delegate
            {
                Intent intent = new Intent(Application.Context, typeof(InfoActivity));
                intent.SetFlags(ActivityFlags.NewTask);
                Application.Context.StartActivity(intent);
            };

            settingsText.Click += delegate
            {
                Intent intent = new Intent(Application.Context, typeof(SettingsActivity));
                intent.SetFlags(ActivityFlags.NewTask);
                Application.Context.StartActivity(intent);
            };


            int isShown = 0;
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            editor = prefs.Edit();
            editor.PutInt("isShown", isShown);
            editor.Apply();

            bool firstRun = prefs.GetBoolean("firstRun", true);
            if(firstRun)
            {
                Intent intent = new Intent(Application.Context, typeof(InfoActivity));
                intent.SetFlags(ActivityFlags.NewTask);
                Application.Context.StartActivity(intent);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (GetSharedPreferences("isDbCreated", isDbCreated) == 1)
                scores.Text = GettingItemsFromDatabase.GetScoresFromDatabase();
        }

        public int GetSharedPreferences(string keyName, int level)
        {
            //getting data from shared prefs
            prefs = PreferenceManager.GetDefaultSharedPreferences(this.ApplicationContext);
            int levelData = prefs.GetInt(keyName, level);
            return levelData;
        }



    }

}

