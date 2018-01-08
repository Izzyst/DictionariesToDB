
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace App3
{
    [Activity(Label = "LockScreenActivity", LaunchMode = LaunchMode.SingleInstance, Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class LockScreenActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UnlockScreen);
            MakeFullScreen();
            Button button = (Button)FindViewById(Resource.Id.Unlock);

            button.Click += (o, e) => {
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