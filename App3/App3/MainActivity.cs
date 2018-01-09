using Android.App;
using Android.Widget;
using Android.OS;
using App3.utils;
using System;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Json;
using Android.Content;
using Android.Preferences;

namespace App3
{
    [Activity(Label = "App3", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Spinner spinner;
        Spinner spinnerLang;
        Switch switchBtn;
        WebClient mClient;
        Uri mUrl;
        Button dataBtn;
        public static string level;
        static string language;
        ISharedPreferences prefs;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            spinner = FindViewById<Spinner>(Resource.Id.spinner);
            spinnerLang = FindViewById<Spinner>(Resource.Id.spinnerLanguage);
            switchBtn = FindViewById<Switch>(Resource.Id.switchButton);
            dataBtn = FindViewById<Button>(Resource.Id.getDataBtn);

            LockScreen.GetInstance().Init(this);

            // =================spinner for choosing level==========================================
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpinnerItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.levels_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            level = spinner.SelectedItem.ToString();

            int spinnerPosition = adapter.GetPosition(GetSharedPreferences("level_data"));
            spinner.SetSelection(spinnerPosition);

            // =================spinner for choosing language==========================================
            spinnerLang.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpinnerItemSelected);
            var adapterLang = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.language_array, Android.Resource.Layout.SimpleSpinnerItem);

            // do odkomentowania po zrobieniu osobnej metody dla zapisu 
            //adapterLang.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            //spinnerLang.Adapter = adapterLang;

            //language = spinnerLang.SelectedItem.ToString();

            // spr czy LockScreen jest aktywny i ustawienie odpowiedniej wartości na togglebuttonie
            if (LockScreen.GetInstance().IsActive())
            {
                switchBtn.Checked = true;
                
                
            }
            else { switchBtn.Checked = false; }

            switchBtn.Click += (o, e) => {
                if (switchBtn.Checked)
                    LockScreen.GetInstance().Active();
                else
                    LockScreen.GetInstance().Deactivate();
            };

             
            

            dataBtn.Click += async (sender, e) => {

                // Get the latitude and longitude entered by the user and create a query.
                string url = "http://localhost:51915/Word/GetWords?language=pl/";// +
                                                                                 //latitude.Text +
                mUrl = new Uri("http://127.0.0.1:51915/Word/GetWords?language=pl/");                                                       //"&lng=" +
                                                                                 //longitude.Text +
                                                                                 //"&username=demo";

                // Fetch the weather information asynchronously, 
                // parse the results, then update the screen:
                JsonValue json = await FetchDataAsync(url);
                //mClient.DownloadDataAsync(mUrl);
            };
        }
 

        // Gets weather data from the passed URL.
        private async Task<JsonValue> FetchDataAsync(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    return jsonDoc;
                }
            }
        }

        private void SpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
            level = toast;
            prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("level_data", toast);
            editor.Apply();
        }

        public string GetSharedPreferences(string keyName)
        {
            //getting data from shared prefs
            prefs = PreferenceManager.GetDefaultSharedPreferences(this.ApplicationContext);
            string levelData = prefs.GetString(keyName, level);
            Toast.MakeText(this, levelData, ToastLength.Long).Show();
            return levelData;
        }
    }
}

