using System;
using Android;
using Android.AccessibilityServices;
using Android.App;
using Android.Content;
using Android.Preferences;
using Android.Views;
using Android.Views.Accessibility;
using Android.Widget;

namespace App3.utils
{
    [Service(Label = "LockWindowAccessibilityService", Permission = Manifest.Permission.BindAccessibilityService)]
    [IntentFilter(new[] { "android.accessibilityservice.AccessibilityService" })]
    [MetaData("android.accessibilityservice", Resource = "@xml/accessibilityservice")]
    public class LockWindowAccessibilityService : AccessibilityService
    {
        int isShown = 0;
        protected override bool OnKeyEvent(KeyEvent e)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this.ApplicationContext);
            int isActive = prefs.GetInt("isShown", isShown);
            //Toast.MakeText(this, levelData, ToastLength.Long).Show();

            if (isActive == 1)
            {
                if (e.KeyCode == Keycode.Home || e.KeyCode == Keycode.DpadCenter || e.KeyCode == Keycode.AppSwitch || e.KeyCode == Keycode.Menu)
                {
                    return true;
                }
            }
            return base.OnKeyEvent(e);
        }

        public override void OnAccessibilityEvent(AccessibilityEvent e)
        {
            //throw new NotImplementedException();
        }

        public override void OnInterrupt()
        {
            //throw new NotImplementedException();
        }
    }
}