using Android.App;
using Android.Content;
using Android.Preferences;

namespace App3.utils
{
    [BroadcastReceiver]
    public class LockScreenReceiver : BroadcastReceiver
    {
        string level;
        public override void OnReceive(Context context, Intent intent)
        {
             ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(context);
             string levelData = prefs.GetString("level_data", level);

            if (intent.Action.Equals(Intent.ActionScreenOff) || intent.Action.Equals(Intent.ActionBootCompleted))
            {
                //if (levelData == "Hard")
                //{
                //    Intent startLockScreenActIntent = new Intent(Application.Context, typeof(HardLevelActivity));
                //    startLockScreenActIntent.SetFlags(ActivityFlags.NewTask);
                //    Application.Context.StartActivity(startLockScreenActIntent);
                //}
                //else if (levelData == "Medium")
                //{
                //    Intent startLockScreenActIntent = new Intent(Application.Context, typeof(MediumLevelActivity));
                //    startLockScreenActIntent.SetFlags(ActivityFlags.NewTask);
                //    Application.Context.StartActivity(startLockScreenActIntent);
                //}
                //else
                //{
                    Intent startLockScreenActIntent = new Intent(Application.Context, typeof(LockScreenActivity));
                    startLockScreenActIntent.SetFlags(ActivityFlags.NewTask);
                    Application.Context.StartActivity(startLockScreenActIntent);
                //}
                
            }


        }
    }
}