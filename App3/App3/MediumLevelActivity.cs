﻿using Android.App;
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

            MediumLevel level = new MediumLevel();
            DataToLevel data = level.GetWords();
            
            textView.Text = data.Def;
            button1.Text = data.WordList[0].Word;
            button2.Text = data.WordList[1].Word;

            button1.Click += (o, e) => {
                if(data.WordList[0].Id == data.Id)
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