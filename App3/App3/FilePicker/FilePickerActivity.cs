using Android.App;
using Android.OS;

namespace App3.FilePicker
{
    [Activity(Label = "FileDialogActivity")]
    public class FilePickerActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.dialoglayout);
            // Create your application here
        }
    }
}