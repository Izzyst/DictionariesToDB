
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using App3.LevelStrategy;
using App3.Models;
using App3.Resources.DataHelper;
using App3.utils;
using System.Threading;
using System.Threading.Tasks;

namespace App3
{
    [Activity(Label = "LockScreenActivity", LaunchMode = LaunchMode.SingleInstance, Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    //[IntentFilter(new[] { Intent.ActionMain },
    //Categories = new[] { Intent.CategoryLauncher })]
    public class LockScreenActivity : Activity
    {
        ISharedPreferences prefs;
        ISharedPreferencesEditor editor;
        string levelString;
        public static int numberOfClicks = 0;
        private static bool isFinished = false;
        Button button;
        Button button2;
        Button button3;
        TextView textView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            numberOfClicks = 0;
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UnlockScreen);
            MakeFullScreen();
            button = (Button)FindViewById(Resource.Id.Unlock);
            button2 = (Button)FindViewById(Resource.Id.hard2Btn);
            button3 = (Button)FindViewById(Resource.Id.hard3Btn);
            textView = (TextView)FindViewById(Resource.Id.textView1);
            
            // ThreadPool.QueueUserWorkItem(o => SetViewForChosenLevel());
            SetViewForChosenLevel();
        }

        private void SetViewForChosenLevel()
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext);
            string lev = prefs.GetString("level_data", levelString);

            if (lev == "Easy" || lev == "Latwy" || lev == "Łatwy")
            {
                EasyLevel level = new EasyLevel();
                DataToLevel data = level.GetWords();

                RunOnUiThread(() => {
                    textView.Text = data.Def;
                    button.Text = data.WordList[0].W;

                    button.Click += (o, e) =>
                    {
                        numberOfClicks = 1;
                        SetNewScore(data.Score, data.WordList[0]);
                        numberOfClicks = 0;                      
                        Android.Graphics.Color color = Android.Graphics.Color.Green;
                        button.SetTextColor(color);
                        isFinished = true;
                        Finish();
                    };
                });
                
                
            }
            else if(lev == "Medium" || lev == "Sredni" || lev == "Średni")
            {
                MediumLevel level = new MediumLevel();
                DataToLevel data = level.GetWords();
                RunOnUiThread(() =>
                {
                    button2.Visibility = ViewStates.Visible;
                    textView.Text = data.Def;
                    button.Text = data.WordList[0].W;
                    button2.Text = data.WordList[1].W;

                    button.Click += (o, e) =>
                    {
                        numberOfClicks++;
                        if (data.WordList[0].Id == data.Id)
                        {
                            SetNewScore(data.Score, data.WordList[0]);
                            Android.Graphics.Color color = Android.Graphics.Color.Green;
                            button.SetTextColor(color);
                            numberOfClicks = 0;
                            isFinished = true;
                            Finish();
                            
                        }
                        else
                        {
                            SetNewScore(-1, data.WordList[0]);
                            Android.Graphics.Color color = Android.Graphics.Color.Red;
                            button.SetTextColor(color);
                            //Thread.Sleep(1000);
                            ShowAnotherDefinition();
                        }

                    };
                    button2.Click += (o, e) =>
                    {
                        numberOfClicks++;
                        if (data.WordList[1].Id == data.Id)
                        {
                            SetNewScore(data.Score, data.WordList[1]);
                            Android.Graphics.Color color = Android.Graphics.Color.Green;
                            button2.SetTextColor(color);
                            numberOfClicks = 0;                      
                            isFinished = true;
                            Finish();
                        }
                        else
                        {
                            SetNewScore(-1, data.WordList[1]);
                            Android.Graphics.Color color = Android.Graphics.Color.Red;
                            button2.SetTextColor(color);
                            //Thread.Sleep(2000);
                            ShowAnotherDefinition();
                        }
                    };
                });
            }
            else
            {
                HardLevel level = new HardLevel();
                DataToLevel data = level.GetWords();

                RunOnUiThread(() =>
                {
                    button2.Visibility = ViewStates.Visible;
                    button3.Visibility = ViewStates.Visible;

                    textView.Text = data.Def;
                    button.Text = data.WordList[0].W;
                    button2.Text = data.WordList[1].W;
                    button3.Text = data.WordList[2].W;

                    button.Click += (o, e) =>
                    {
                        numberOfClicks++;
                        if (data.WordList[0].Id == data.Id)
                        {
                            SetNewScore(data.Score, data.WordList[0]);
                            Android.Graphics.Color color = Android.Graphics.Color.Green;
                            button.SetTextColor(color);
                            numberOfClicks = 0;                           
                            isFinished = true;
                            Finish();
                        }
                        else
                        {
                            SetNewScore(-1, data.WordList[0]);
                            Android.Graphics.Color color = Android.Graphics.Color.Red;
                            button.SetTextColor(color);
                            ShowAnotherDefinition();
                        }

                    };
                    button2.Click += (o, e) =>
                    {
                        numberOfClicks++;
                        if (data.WordList[1].Id == data.Id)
                        {
                            SetNewScore(data.Score, data.WordList[1]);
                            Android.Graphics.Color color = Android.Graphics.Color.Green;
                            button2.SetTextColor(color);
                            numberOfClicks = 0;              
                            isFinished = true;
                            Finish();

                        }
                        else
                        {
                            SetNewScore(-1, data.WordList[1]);
                            Android.Graphics.Color color = Android.Graphics.Color.Red;
                            button2.SetTextColor(color);
                            ShowAnotherDefinition();
                        }
                    };

                    button3.Click += (o, e) =>
                    {
                        numberOfClicks++;
                        if (data.WordList[2].Id == data.Id)
                        {
                            SetNewScore(data.Score, data.WordList[2]);
                            Android.Graphics.Color color = Android.Graphics.Color.Green;
                            button3.SetTextColor(color);
                            numberOfClicks = 0;
                            isFinished = true;
                            Finish();
                        }
                        else
                        {
                            SetNewScore(-1, data.WordList[2]);
                            Android.Graphics.Color color = Android.Graphics.Color.Red;
                            button3.SetTextColor(color);
                            ShowAnotherDefinition();
                        }
                    };
                });
            }
        }

        private void ShowAnotherDefinition()
        {
            Finish();
            Intent startLockScreenActIntent = new Intent(Application.Context, typeof(LockScreenActivity));
            startLockScreenActIntent.SetFlags(ActivityFlags.NewTask);
            Application.Context.StartActivity(startLockScreenActIntent);
        }

        private void SetNewScore(int score, WordTable word)
        {
            Database db = new Database();
            // jesli poprawna odp za pierwszym kliknięciem, update score oraz ilość kliknięć dla danego słowa
            if (score>=0)
            {
                score++;
                db.UpdateTableWord(score, word);
            }
            else
            {
                // -1 oznacza niepoprawną odp
                db.UpdateTableWord(-1, word);
            }
        }

        public override void OnBackPressed()
        {
            return; //Do nothing!
        }


        protected override void OnPause()
        {
            base.OnPause();
            int isShown = 0;
            //if (isFinished == false)
            //{
                //Intent homeIntent = new Intent(Intent.ActionMain);
                //homeIntent.AddCategory(Intent.CategoryHome);
                //homeIntent.SetFlags(ActivityFlags.NewTask);
                //StartActivity(homeIntent);
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                editor = prefs.Edit();
                editor.PutInt("isShown", isShown);
                editor.Apply();
            //}
        }

    protected override void OnResume()
        {
            base.OnResume();
            int isShown = 0;
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            editor = prefs.Edit();
            editor.PutInt("isShown", isShown);
            editor.Apply();
        }

        public void MakeFullScreen()
        {
            View decorView = Window.DecorView;
            var uiOptions = (int)decorView.SystemUiVisibility;
            var newUiOptions = (int)uiOptions;

            newUiOptions |= (int)SystemUiFlags.LowProfile;
            newUiOptions |= (int)SystemUiFlags.Fullscreen;
            newUiOptions |= (int)SystemUiFlags.HideNavigation;
            newUiOptions |= (int)SystemUiFlags.Immersive;

            decorView.SystemUiVisibility = (StatusBarVisibility)newUiOptions;

        }
    }
}