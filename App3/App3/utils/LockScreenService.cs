﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;

namespace App3.utils
{
    [Service]
    public class LockScreenService : Service
    {
        private static string level;

        LockScreenReceiver receiver;
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            base.OnCreate();
            StateRecever(true);    
        }


        public override void OnDestroy()
        {
            Toast.MakeText(this.ApplicationContext, this.GetString(Resource.String.serviceStopped), ToastLength.Short).Show();
            StateRecever(false);
            base.OnDestroy();
        }

        private void StateRecever(bool isStartRecever)
        {
            if (isStartRecever)
            {
                IntentFilter filter = new IntentFilter();
                filter.AddAction(Intent.ActionScreenOff);
                filter.AddAction(Intent.ActionBootCompleted);
                receiver = new LockScreenReceiver();
                RegisterReceiver(receiver, filter);
            }
            else
            {
                if (null != receiver)
                {
                    UnregisterReceiver(receiver);
                }
            }
        }
    }
}