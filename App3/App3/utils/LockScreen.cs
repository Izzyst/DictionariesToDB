using Android.App;
using Android.Content;

namespace App3.utils
{
    public class LockScreen
    {
        public static bool lockScreenIsShow = false;
        private static LockScreen lockScreen;

        Context context;
        public void Init(Context context)
        {
            this.context = context;
        }

        public static LockScreen GetInstance()
        {
            if (lockScreen == null)
            {
                lockScreen = new LockScreen();
            }
            return lockScreen;
        }


        public bool IsActive()
        {
            if (context != null)
            {
                return IsMyServiceRunning(typeof(LockScreenService));
            }
            else
            {
                return false;
            }
        }

        public void Active()
        {
            if (context != null)
            {
                context.StartService(new Intent(context, typeof(LockScreenService)));
            }
        }

        public void Deactivate()
        {
            if (context != null)
            {
                context.StopService(new Intent(context, typeof(LockScreenService)));
            }
        }


        /*
         private void showSettingAccesability(){
        if(!isMyServiceRunning(LockWindowAccessibilityService.class)) {
            Intent intent = new Intent(Settings.ACTION_ACCESSIBILITY_SETTINGS);
            context.startActivity(intent);
        }
    }
             */

        private bool IsMyServiceRunning(System.Type cls)
        {
            ActivityManager manager = (ActivityManager)context.GetSystemService(Context.ActivityService);
            foreach (var service in manager.GetRunningServices(int.MaxValue))
            {
                if (service.Service.ClassName.Equals(Java.Lang.Class.FromType(cls).CanonicalName))
                {
                    return true;
                }
            }
            return false;

        }
    }
}