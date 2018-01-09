using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using App3.LevelStrategy;
using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App3
{
    [Activity(Label = "HardLevelActivity")]
    public class HardLevelActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HardLevelScreen);
            MakeFullScreen();
            Button button1 = (Button)FindViewById(Resource.Id.hard1Btn);
            Button button2 = (Button)FindViewById(Resource.Id.hard2Btn);
            Button button3 = (Button)FindViewById(Resource.Id.hard3Btn);
            TextView textView = (TextView)FindViewById(Resource.Id.textView1);

            HardLevel level = new HardLevel();
            DataToLevel data = level.GetWords();

            textView.Text = data.Def;
            button1.Text = data.WordList[0].Word;
            button2.Text = data.WordList[1].Word;
            button3.Text = data.WordList[2].Word;

            button1.Click += (o, e) => {
                if (data.WordList[0].Id == data.Id)
                {
                    Finish();
                }

            };
            button2.Click += (o, e) => {

                if (data.WordList[1].Id == data.Id)
                {
                    Finish();
                }
            };

            button3.Click += (o, e) => {

                if (data.WordList[2].Id == data.Id)
                {
                    Finish();
                }
            };
        }

        private List<Words> GetWords(List<Words> list)
        {
            List<Words> result = new List<Words>();
            var rnd = new Random();
            var randomNumbers = Enumerable.Range(0, 3).OrderBy(x => rnd.Next()).Take(3).ToList();
            foreach (var i in randomNumbers)
            {
                result.Add(list[i]);
            }

            return result;
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