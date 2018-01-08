using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace App3.utils
{
    [Service]
    public class LockScreenService : Service
    {
        LockScreenReceiver receiver;
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Toast.MakeText(this.ApplicationContext, "LockScreenService works", ToastLength.Short).Show();

            StateRecever(true);

        }

        public override void OnDestroy()
        {
            Toast.MakeText(this.ApplicationContext, "LockScreenService has been stopped", ToastLength.Short).Show();
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