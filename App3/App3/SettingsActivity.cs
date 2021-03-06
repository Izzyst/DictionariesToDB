﻿using Android.App;
using Android.Widget;
using Android.OS;
using App3.utils;
using System;
using Android.Content;
using Android.Preferences;
using System.Collections.Generic;
using App3.LevelStrategy;
using Android.Net;
using System.Threading.Tasks;
using Android.Views;

namespace App3
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity
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
        int isDbCreated = 0;

        private List<string> fileFist = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.settingsPagelayout);

            this.Title = this.Resources.GetString(Resource.String.settings);
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
            // Create your application here

            if (GetSharedPreferences("isDbCreated", isDbCreated) == 1)
                if (GettingItemsFromDatabase.CheckIfDbEmpty() == false)
                    //scores.Text = GettingItemsFromDatabase.GetScoresFromDatabase();
            LockScreen.GetInstance().Init(this, true);

            fileRadioBtn.Click += delegate
            {
                ChangeToFileView(true);
            };

            externRadioBtn.Click += delegate
            {
                ChangeToFileView(false);
            };
            


            //    // =================spinner for choosing level==========================================
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpinnerItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.levels_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            level = spinner.SelectedItem.ToString();

            //ustawienie spinnera wg wczesniejszego ustawienia
            int spinnerPosition = adapter.GetPosition(GetSharedPreferences("level_data"));
            spinner.SetSelection(spinnerPosition);

            //    // =================spinner for choosing language==========================================
            spinnerLang.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpinnerLangItemSelected);
            var adapterLang = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.language_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapterLang.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerLang.Adapter = adapterLang;
            //ustawienie spinnera wg wczesniejszego ustawienia
            string language = GetSharedPreferences("language_data");
            int spinnerPositionLang = adapterLang.GetPosition(language);
            spinnerLang.SetSelection(spinnerPositionLang);

            //    //===============================================================

            //    // spr czy LockScreen jest aktywny i ustawienie odpowiedniej wartości na togglebuttonie
            if (LockScreen.GetInstance().IsActive()) { switchBtn.Checked = true; }
            else { switchBtn.Checked = false; }

            switchBtn.Click += (o, e) =>
            {
                language = GetSharedPreferences("language_data");
                ValidationForSwitchButton(language);
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
                        ChangeToFileView(false);
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

            prefs = PreferenceManager.GetDefaultSharedPreferences(this.ApplicationContext);
            int l = -1;
            int isOn = prefs.GetInt("isOn", l);
            if (isOn == 1)
            {
                switchBtn.SetOnCheckedChangeListener(null);
                switchBtn.Click += delegate {
                    switchBtn.Checked = true;
                };
                // switchBtn.SetOnCheckedChangeListener();
            }
        }
        protected override void OnResume()
        {
            base.OnResume();
           // if (GetSharedPreferences("isDbCreated", isDbCreated) == 1)
                //scores.Text = GettingItemsFromDatabase.GetScoresFromDatabase();
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
            int isOn = 0;
            editor = prefs.Edit();
            editor.PutInt("isOn", isOn);
            editor.Apply();
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
            int isOn = 0;
            editor = prefs.Edit();
            editor.PutInt("isOn", isOn);
            editor.Apply();
            switchBtn.Checked = false;

            // spr czy baza jest pusta, jeśli nie to spr czy ten sam język, jeśli tak, to nie wykonuje pobierania danych

            if (GetSharedPreferences("isDbCreated", isDbCreated) == 1)
                if (GettingItemsFromDatabase.CheckIfDownloadDictionaryIsNeeded(toast) == true)
                {
                    await DownloadDictionaryAsync();
                }
        }

        public string GetSharedPreferences(string keyName)
        {
            //getting data from shared prefs
            prefs = PreferenceManager.GetDefaultSharedPreferences(this.ApplicationContext);
            string levelData = prefs.GetString(keyName, level);
            return levelData;
        }
        public int GetSharedPreferences(string keyName, int level)
        {
            //getting data from shared prefs
            prefs = PreferenceManager.GetDefaultSharedPreferences(this.ApplicationContext);
            int levelData = prefs.GetInt(keyName, level);
            return levelData;
        }

        public bool CheckConnection()
        {
            bool isOnline = false;
            ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;
            if (networkInfo != null)
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
                    int isOn = 0;
                    editor = prefs.Edit();
                    editor.PutInt("isOn", isOn);
                    editor.Apply();
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
                LockScreen.GetInstance().Deactivate();
                int isOn = 0;
                editor = prefs.Edit();
                editor.PutInt("isOn", isOn);
                editor.Apply();
                switchBtn.Checked = false;
            }
        }

        private async void ValidationForSwitchButton(string language)
        {
            int isOn = 0;
            if (switchBtn.Checked)
            {
                LockScreen.GetInstance().Init(this);
                if (progressBar.Visibility == Android.Views.ViewStates.Visible)
                {
                    switchBtn.Checked = false;
                    LockScreen.GetInstance().Deactivate();
                    isOn = 0;
                    editor = prefs.Edit();
                    editor.PutInt("isOn", isOn);
                    editor.Apply();
                    Toast.MakeText(this, this.GetString(Resource.String.WaitDownloading), ToastLength.Long).Show();
                }

                if (GettingItemsFromDatabase.CheckIfDbEmpty() == true)
                {
                    if (externRadioBtn.Checked == true)
                    {
                        await DownloadDictionaryAsync();
                        LockScreen.GetInstance().Active();
                        isOn = 1;
                        editor = prefs.Edit();
                        editor.PutInt("isOn", isOn);
                        editor.Apply();
                    }
                    else
                    {
                        //przypadek, gdy wybrano słownik z własnego pliku, ale nie wgrano pliku
                        externRadioBtn.Checked = true;
                        Toast.MakeText(this, this.GetString(Resource.String.wrongFileExtension), ToastLength.Long).Show();
                        ChangeToFileView(false);
                        await DownloadDictionaryAsync();
                    }
                }
                else
                {
                    if (externRadioBtn.Checked == false && GettingItemsFromDatabase.GetTypeOfDictionaryInDb() != "csv" && GettingItemsFromDatabase.GetTypeOfDictionaryInDb() != "xlsx" && GettingItemsFromDatabase.GetTypeOfDictionaryInDb() != "xls")
                    {
                        //przypadek, gdy wybrano słownik z własnego pliku, ale nie wgrano pliku a w bazie są załadowane dane z poptzredniego pobierania zewnętrznego słownika
                        externRadioBtn.Checked = true;
                        Toast.MakeText(this, this.GetString(Resource.String.wrongFileExtension), ToastLength.Long).Show();
                        ChangeToFileView(false);
                    }
                    //po
                    if (externRadioBtn.Checked == true && GettingItemsFromDatabase.CheckIfDictionaryInDatabaseIsCorrect(language) == false)
                    {
                        await DownloadDictionaryAsync();
                        LockScreen.GetInstance().Active();
                        isOn = 1;
                        editor = prefs.Edit();
                        editor.PutInt("isOn", isOn);
                        editor.Apply();
                    }
                    LockScreen.GetInstance().Active();
                    isOn = 1;
                    editor = prefs.Edit();
                    editor.PutInt("isOn", isOn);
                    editor.Apply();
                }
            }
            else
            {
                prefs = PreferenceManager.GetDefaultSharedPreferences(this.ApplicationContext);
                int l = -1;
                isOn = prefs.GetInt("isOn", l);
                if (isOn != 1)
                    LockScreen.GetInstance().Deactivate();
            }
        }

        private void ChangeToFileView(bool change)
        {
            switchBtn.Checked = false;
            if (change)
            {
                fileBtn.Visibility = Android.Views.ViewStates.Visible;
                chooseLanguageText.Visibility = Android.Views.ViewStates.Gone;
                spinnerLang.Visibility = Android.Views.ViewStates.Gone;
            }
            else
            {
                fileBtn.Visibility = Android.Views.ViewStates.Gone;
                fileText.Visibility = Android.Views.ViewStates.Gone;
                chooseLanguageText.Visibility = Android.Views.ViewStates.Visible;
                spinnerLang.Visibility = Android.Views.ViewStates.Visible;
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.myMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.about:
                    Intent intent1 = new Intent(Application.Context, typeof(InfoActivity));
                    intent1.SetFlags(ActivityFlags.NewTask);
                    Application.Context.StartActivity(intent1);
                    return true;
                case Resource.Id.sett:
                    Intent intent2 = new Intent(Application.Context, typeof(SettingsActivity));
                    intent2.SetFlags(ActivityFlags.NewTask);
                    Application.Context.StartActivity(intent2);
                    return true;
                case Resource.Id.dict:
                    Intent intent3 = new Intent(Application.Context, typeof(StatisticsActivity));
                    intent3.SetFlags(ActivityFlags.NewTask);
                    Application.Context.StartActivity(intent3);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }


    }

}