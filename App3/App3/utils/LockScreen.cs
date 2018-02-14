using Android.App;
using Android.Content;
using System;

namespace App3.utils
{
    public class LockScreen
    {
        public static bool lockScreenIsShow = false;
        private static LockScreen lockScreen;
        bool disableHomeButton = false;
        Context context;
        public void Init(Context context)
        {
            this.context = context;
        }

        public void Init(Context context, bool homeButton)
        {
            this.context = context;
            this.disableHomeButton = homeButton;
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
            if (disableHomeButton)
            {
                ShowSettingAccesability();
                disableHomeButton = false;
            }
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


        
         private void ShowSettingAccesability(){
        if(!IsServiceRunning(Application.Context, typeof(LockWindowAccessibilityService))) {
            Intent intent = new Intent(Android.Provider.Settings.ActionAccessibilitySettings);
            context.StartActivity(intent);
        }
    }
             
        public bool IsServiceRunning(Context context, Type serviceClass)
        {
            ActivityManager manager = (ActivityManager)context.GetSystemService(Context.ActivityService);
            foreach (var service in manager.GetRunningServices(int.MaxValue))
            {
                if (service.Process == context.PackageName && service.Service.ClassName.EndsWith(serviceClass.Name))
                {
                    return true;
                }
            }
            return false;
        }

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