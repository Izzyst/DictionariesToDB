using Android.App;
using Android.Widget;
using Android.OS;
using App3.utils;
using System;
using Android.Content;
using Android.Preferences;
using Android.App.Usage;
using System.Collections.Generic;
using App3.LevelStrategy;
using Android.Content.Res;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Android.Net;
using System.Threading;
using System.Threading.Tasks;
using App3.Resources.DataHelper;
using Android.App.Admin;

namespace App3
{
    [Activity(Label = "Linguistic lock screen", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public static bool isWorking;
        Spinner spinner;
        Spinner spinnerLang;
        Switch switchBtn;
        Button fileBtn;
        ProgressBar progressBar;
        TextView scores;
        TextView fileText;
        TextView chooseLanguageText;
        RadioButton fileRadioBtn;
        RadioButton externRadioBtn;

        public static string level;
        ISharedPreferences prefs;
        ISharedPreferencesEditor editor;

        private List<string> fileFist = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            spinner = FindViewById<Spinner>(Resource.Id.spinner);
            spinnerLang = FindViewById<Spinner>(Resource.Id.spinnerLanguage);
            switchBtn = FindViewById<Switch>(Resource.Id.switchButton);
            fileBtn = FindViewById<Button>(Resource.Id.chooseFileBtn);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            scores = FindViewById<TextView>(Resource.Id.textView1);
            fileText = FindViewById<TextView>(Resource.Id.textView2);
            chooseLanguageText = FindViewById<TextView>(Resource.Id.ChooseLanguageTextView);
            fileRadioBtn = FindViewById<RadioButton>(Resource.Id.radio_file);
            externRadioBtn = FindViewById<RadioButton>(Resource.Id.radio_extern);
            if(GettingItemsFromDatabase.CheckIfDbEmpty()==false)
                scores.Text = GettingItemsFromDatabase.GetScoresFromDatabase();
            LockScreen.GetInstance().Init(this);

            fileRadioBtn.Click += delegate
            {
                fileBtn.Visibility = Android.Views.ViewStates.Visible;
                chooseLanguageText.Visibility = Android.Views.ViewStates.Gone;
                spinnerLang.Visibility = Android.Views.ViewStates.Gone;
            };
            externRadioBtn.Click += delegate
            {
                HandleClickExternalRadioButton();
            };

            // =================spinner for choosing level==========================================
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpinnerItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.levels_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            level = spinner.SelectedItem.ToString();

            //ustawienie spinnera wg wczesniejszego ustawienia
            int spinnerPosition = adapter.GetPosition(GetSharedPreferences("level_data"));
            spinner.SetSelection(spinnerPosition);

            // =================spinner for choosing language==========================================
            spinnerLang.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpinnerLangItemSelected);
            var adapterLang = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.language_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapterLang.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerLang.Adapter = adapterLang;
            //ustawienie spinnera wg wczesniejszego ustawienia
            string language = GetSharedPreferences("language_data");
            int spinnerPositionLang = adapterLang.GetPosition(language);
            spinnerLang.SetSelection(spinnerPositionLang);
            //spr. czy słownik w podręcznej bazie jest poprawny
            GettingItemsFromDatabase.CheckIfDictionaryInDatabaseIsCorrect(language);
            //===============================================================

            // spr czy LockScreen jest aktywny i ustawienie odpowiedniej wartości na togglebuttonie
            if (LockScreen.GetInstance().IsActive())  { switchBtn.Checked = true;}
            else { switchBtn.Checked = false; }

            switchBtn.Click += async (o, e) =>
            {
                if (progressBar.Visibility == Android.Views.ViewStates.Visible)
                {
                    switchBtn.Checked = false;
                    LockScreen.GetInstance().Deactivate();
                    Toast.MakeText(this, this.GetString(Resource.String.WaitDownloading), ToastLength.Long).Show();
                }
                if (switchBtn.Checked) {
                    LockScreen.GetInstance().Active();
                    // spr czy baza nie jest pusta oraz czy wybrany słownik zgadza się z tym w podręcznej bazie
                    if((GettingItemsFromDatabase.CheckIfNewWordsNeededToDownload()==true || GettingItemsFromDatabase.CheckIfDictionaryInDatabaseIsCorrect(language)==true) && externRadioBtn.Checked==true)
                    {
                        await DownloadDictionaryAsync();
                    }
                    if(GettingItemsFromDatabase.CheckIfDatabaseIsEmpty() == true)
                    {
                        switchBtn.Checked = false;
                        LockScreen.GetInstance().Deactivate();
                        Toast.MakeText(this, this.GetString(Resource.String.noInternetConnection), ToastLength.Long).Show();
                    }

                } 
                else  LockScreen.GetInstance().Deactivate();
            };

            
            fileBtn.Click += async delegate
            {
                try
                {
                    var filePath = "";
                    var crossFilePicker = Plugin.FilePicker.CrossFilePicker.Current;
                    var myResult = await crossFilePicker.PickFile();

                    filePath = myResult.FilePath;
                    fileText.Visibility = Android.Views.ViewStates.Visible;
                    fileText.Text = filePath;
                    if (GettingItemsFromDatabase.InsertFile(filePath) == false)
                    {
                        Toast.MakeText(this, this.GetString(Resource.String.wrongFileExtension), ToastLength.Long).Show();
                        externRadioBtn.Checked = true;
                        HandleClickExternalRadioButton();
                        fileText.Visibility = Android.Views.ViewStates.Gone;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    ex.ToString(); //"Only one operation can be active at a time"
                }
        };

            int isShown = 0;
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            editor = prefs.Edit();
            editor.PutInt("isShown", isShown);
            editor.Apply();
        }

        void HandleClickExternalRadioButton()
        {
            chooseLanguageText.Visibility = Android.Views.ViewStates.Visible;
            spinnerLang.Visibility = Android.Views.ViewStates.Visible;
            fileBtn.Visibility = Android.Views.ViewStates.Gone;
        }

        protected override void OnResume()
        {
            base.OnResume();
            scores.Text = GettingItemsFromDatabase.GetScoresFromDatabase();
        }

        private void SpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            level = toast;
            prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            editor = prefs.Edit();
            editor.PutString("level_data", toast);
            editor.Apply();
            LockScreen.GetInstance().Deactivate();
            switchBtn.Checked = false;
        }

        private async void SpinnerLangItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            level = toast;
            prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            editor = prefs.Edit();
            editor.PutString("language_data", toast);
            editor.Apply();
            LockScreen.GetInstance().Deactivate();
            switchBtn.Checked = false;

            // spr czy baza jest pusta, jeśli nie to spr czy ten sam język, jeśli tak, to nie wykonuje pobierania danych

            if(GettingItemsFromDatabase.CheckIfDownloadDictionaryIsNeeded(toast) == true)
            {
                progressBar.Visibility = Android.Views.ViewStates.Visible;
                await DownloadDictionaryAsync();
                progressBar.Visibility = Android.Views.ViewStates.Gone;
            }          
        }

        public string GetSharedPreferences(string keyName)
        {
            //getting data from shared prefs
            prefs = PreferenceManager.GetDefaultSharedPreferences(this.ApplicationContext);
            string levelData = prefs.GetString(keyName, level);
            return levelData;
        }

        public bool CheckConnection()
        {
            bool isOnline = false;
            ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;
            if (networkInfo!=null)
                isOnline = true; 
            return isOnline;
        }

        public async Task DownloadDictionaryAsync()
        {
            isWorking = true;
            progressBar.Visibility = Android.Views.ViewStates.Visible;
            if (CheckConnection() == true)
            {
                RunOnUiThread(() => progressBar.Visibility = Android.Views.ViewStates.Visible);
                Task<int> taks = new Task<int>(GettingItemsFromDatabase.InsertWordsToSqlite);
                taks.Start();
                int result = await taks;
                if (result == 1)
                {
                    Toast.MakeText(this, this.GetString(Resource.String.finishDownload), ToastLength.Long).Show();
                    progressBar.Visibility = Android.Views.ViewStates.Gone;
                    LockScreen.GetInstance().Deactivate();
                    switchBtn.Checked = false;
                }
                else
                {
                    progressBar.Visibility = Android.Views.ViewStates.Gone;
                    Toast.MakeText(this, this.GetString(Resource.String.developersError), ToastLength.Long).Show();
                    progressBar.Visibility = Android.Views.ViewStates.Gone;
                }
            }
            else
            {
                progressBar.Visibility = Android.Views.ViewStates.Gone;
                Toast.MakeText(this, this.GetString(Resource.String.noInternetConnection), ToastLength.Long).Show();
            }
        }
    }

}

