using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App3.utils
{
    [BroadcastReceiver]
    public class LockScreenReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {

            if (intent.Action.Equals(Intent.ActionScreenOff)|| intent.Action.Equals(Intent.ActionBootCompleted))
            {
                Intent startLockScreenActIntent = new Intent(Application.Context, typeof(LockScreenActivity));
                startLockScreenActIntent.SetFlags(ActivityFlags.NewTask);
                Application.Context.StartActivity(startLockScreenActIntent);
            }

        }
    }
}