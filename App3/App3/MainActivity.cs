using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Preferences;
using App3.LevelStrategy;
using Android.Views;

namespace App3
{
    [Activity(Label = "Linguistic lock screen", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public static bool isWorking;
        TextView scores;

        public static string level;
        ISharedPreferences prefs;
        ISharedPreferencesEditor editor;
        int isDbCreated = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);       

            scores = FindViewById<TextView>(Resource.Id.textView1);

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

