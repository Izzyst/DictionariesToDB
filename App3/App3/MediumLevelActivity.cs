using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using App3.LevelStrategy;
using App3.Models;
using App3.Resources.DataHelper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App3
{
    [Activity(Label = "MediumLevelActivity", LaunchMode = LaunchMode.SingleInstance, Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]//, LaunchMode = LaunchMode.SingleInstance, Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class MediumLevelActivity : Activity
    {
        public static int numberOfClicks = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MediumLevelScreen);
            MakeFullScreen();
            Button button1 = (Button)FindViewById(Resource.Id.medium1Btn);
            Button button2 = (Button)FindViewById(Resource.Id.medium2Btn);
            TextView textView = (TextView)FindViewById(Resource.Id.textView1);

            MediumLevel level = new MediumLevel();
            DataToLevel data = level.GetWords();
            
            textView.Text = data.Def;
            button1.Text = data.WordList[0].W;
            button2.Text = data.WordList[1].W;

            button1.Click += (o, e) => {
                numberOfClicks++;
                if (data.WordList[0].Id == data.Id)
                {
                    SetNewScore(data.Score, data.WordList[0]);
                    Android.Graphics.Color color = Android.Graphics.Color.Green;
                    button1.SetTextColor(color);
                    numberOfClicks = 0;
                    Finish();
                }
                else
                {
                    Android.Graphics.Color color = Android.Graphics.Color.Red;
                    button1.SetTextColor(color);
                }

            };
            button2.Click += (o, e) => {
                numberOfClicks++;
                if (data.WordList[1].Id == data.Id)
                {
                    SetNewScore(data.Score, data.WordList[1]);
                    Android.Graphics.Color color = Android.Graphics.Color.Green;
                    button2.SetTextColor(color);
                    numberOfClicks = 0;
                    Finish();
                }
                else
                {
                    Android.Graphics.Color color = Android.Graphics.Color.Red;
                    button2.SetTextColor(color);
                }
            };       
        }

        private void SetNewScore(int score, WordTable word)
        {
            Database db = new Database();
            // jesli poprawna odp za pierwszym kliknięciem, update score oraz ilość kliknięć dla danego słowa
            if (numberOfClicks == 1)
            {
                score++;
                db.UpdateTableWord(score, 1, word);
            }
            else
            {
                // jesli ilosc wybranych odp różna od 1, to update ilość klinięć
                db.UpdateTableWord(-1, 1, word);
            }
        }

        public override void OnBackPressed()
        {
            return; //Do nothing!
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