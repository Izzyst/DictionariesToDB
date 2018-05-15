using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Preferences;
using App3.LevelStrategy;
using Android.Views;
using App3.Resources.DataHelper;
using System.Collections.Generic;
using System;

namespace App3
{
    [Activity(Label = "Linguistic lock screen", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public static bool isWorking;
        TextView scoresText;
        TextView dictionaryText;
        TextView correctText;
        TextView wrongText;
        TextView textView;

        public static string level;
        ISharedPreferences prefs;
        ISharedPreferencesEditor editor;
        int isDbCreated = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            scoresText = FindViewById<TextView>(Resource.Id.textView1);
            dictionaryText = FindViewById<TextView>(Resource.Id.textView3);
            correctText = FindViewById<TextView>(Resource.Id.textView2);
            wrongText = FindViewById<TextView>(Resource.Id.textView4);
            //textView = FindViewById<TextView>(Resource.Id.textView5);
            

            int isShown = 0;
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            editor = prefs.Edit();
            editor.PutInt("isShown", isShown);
            editor.Apply();

            Database ob = new Database();
            var scoresResults = ob.GetScoreResults();
            List<string> list = new List<string>();
            if (scoresResults != null)
            {
                foreach (var item in scoresResults)
                {
                    list.Add(item.Scores + ", " + item.Answers);
                }
            }          

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.myMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
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

        protected override void OnResume()
        {
            base.OnResume();
            Database bd = new Database();
            if (GetSharedPreferences("isDbCreated", isDbCreated) == 1)
            {
                var correct = bd.GetAmountOfCorrectAnswers();
                var wrong = bd.GetAmountOfWrongAnswers();
                var sumOfPoints = correct + wrong;
                double amount = 0;
                if (sumOfPoints != 0)
                    amount = Math.Round((double)correct / sumOfPoints, 2)*100;
                scoresText.Text = " Wynik:         " + amount.ToString() + "%";
                correctText.Text = "Poprawnie: " + System.Environment.NewLine +"     "+ bd.GetAmountOfCorrectAnswers().ToString();
                wrongText.Text = "        Źle: " + System.Environment.NewLine +"         "+ bd.GetAmountOfWrongAnswers().ToString();
                string language = GetSharedPreferences("language_data");
                dictionaryText.Text = "Słownik: " + language;
            }

            bool firstRun = prefs.GetBoolean("firstRun", true);
            if (firstRun)
            {
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutBoolean("firstRun", false);
                // editor.Commit();    // applies changes synchronously on older APIs
                editor.Apply();        // applies changes asynchronously on newer APIs
                bd.CreateScoresTable();
                Intent intent = new Intent(Application.Context, typeof(InfoActivity));
                intent.SetFlags(ActivityFlags.NewTask);
                Application.Context.StartActivity(intent);
            }
        }

        public int GetSharedPreferences(string keyName, int level)
        {
            //getting data from shared prefs
            prefs = PreferenceManager.GetDefaultSharedPreferences(this.ApplicationContext);
            int levelData = prefs.GetInt(keyName, level);
            return levelData;
        }

        public string GetSharedPreferences(string keyName)
        {
            //getting data from shared prefs
            prefs = PreferenceManager.GetDefaultSharedPreferences(this.ApplicationContext);
            string levelData = prefs.GetString(keyName, level);
            return levelData;
        }
    }

}

