using System;
using Android.AccessibilityServices;
using Android.App;
using Android.Content;
using Android.Preferences;
using Android.Views;
using Android.Views.Accessibility;
using Android.Widget;

namespace App3.utils
{
    [Service]
    [IntentFilter(new[] { "android.accessibilityservice.AccessibilityService" })]
    public class LockWindowAccessibilityService : AccessibilityService
    {
        int isShown = 0;
        protected override bool OnKeyEvent(KeyEvent e)
        {
           // bool lockScreenShow=true;

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext);
            int lockScreenShow = prefs.GetInt("isShown", isShown);

            LockScreen.GetInstance().Init(this);
            if (lockScreenShow==1)
                if (e.KeyCode == Keycode.Home || e.KeyCode == Keycode.DpadCenter)
            {
                return true;
            }
            return base.OnKeyEvent(e);

        }

    public override void OnAccessibilityEvent(AccessibilityEvent e)
        {
            throw new NotImplementedException();
        }

        public override void OnInterrupt()
        {
            throw new NotImplementedException();
        }
    }
}