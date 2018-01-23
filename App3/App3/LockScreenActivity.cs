
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using App3.LevelStrategy;
using App3.Models;
using App3.Resources.DataHelper;

namespace App3
{
    [Activity(Label = "LockScreenActivity", LaunchMode = LaunchMode.SingleInstance, Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class LockScreenActivity : Activity
    {
        public static int numberOfClicks = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UnlockScreen);
            MakeFullScreen();
            Button button = (Button)FindViewById(Resource.Id.Unlock);
            TextView textView = (TextView)FindViewById(Resource.Id.textView1);

            EasyLevel level = new EasyLevel();
            DataToLevel data = level.GetWords();

            textView.Text = data.Def;
            button.Text = data.WordList[0].W;

            button.Click += (o, e) =>
            {
                numberOfClicks = 1;
                SetNewScore(data.Score, data.WordList[0]);
                numberOfClicks = 0;
                Finish();
                Android.Graphics.Color color = Android.Graphics.Color.Green;
                button.SetTextColor(color);
            };
        }

        private void SetNewScore(int score, WordTable word)
        {
            Database db = new Database();
            // jesli poprawna odp za pierwszym kliknięciem, update score oraz ilość kliknięć dla danego słowa
            if (numberOfClicks == 1)
            {
                score++;
                db.UpdateTableWord(score, numberOfClicks, word);
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