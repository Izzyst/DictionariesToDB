using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App3
{
    [Activity(Label = "MediumLevelActivity")]//, LaunchMode = LaunchMode.SingleInstance, Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class MediumLevelActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MediumLevelScreen);
            MakeFullScreen();
            Button button1 = (Button)FindViewById(Resource.Id.medium1Btn);
            Button button2 = (Button)FindViewById(Resource.Id.medium2Btn);
            TextView textView = (TextView)FindViewById(Resource.Id.textView1);
            
            //get data from strategy
            //public Words(int id, string word, string defs, string Language)
            Words w1tmp = new Words(1, "home", "place where you live", "ang");
            Words w2tmp = new Words(2, "bedroom", "room where you sleep", "ang");
            
            List<Words> list = new List<Words>();
            list.Add(w1tmp);
            list.Add(w2tmp);
            textView.Text = list[0].Defs[0];

            List<Words> list2 = new List<Words>();
            list2 = this.GetWords(list);

            button1.Text = list2[0].Word;
            button2.Text = list2[1].Word;

            button1.Click += (o, e) => {
                if(list2[0].Id == 1)
                {
                    Finish();
                }
                
            };
            button2.Click += (o, e) => {

                if (list2[1].Id == 1)
                {
                    Finish();
                }
            };
            
        }

        private List<Words> GetWords(List<Words> list)
        {
            List<Words> result = new List<Words>();
            var rnd = new Random();
            var randomNumbers = Enumerable.Range(0, 2).OrderBy(x => rnd.Next()).Take(2).ToList();
            foreach(var i in randomNumbers)
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