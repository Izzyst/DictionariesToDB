using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

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


            button1.Click += (o, e) => {
                // Toast.MakeText(this, "unlocked!", ToastLength.Short).Show();
                Finish();
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