using Android.App;
using Android.Widget;
using Android.OS;
using App3.utils;
using App3.FilePicker;
using System;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Json;
using Android.Content;
using Android.Preferences;
using Android.App.Usage;
using System.Collections.Generic;
using App3.LevelStrategy;
using Android.Content.Res;

namespace App3
{
    [Activity(Label = "App3", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Spinner spinner;
        Spinner spinnerLang;
        Switch switchBtn;
        Button dataBtn;
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
            dataBtn = FindViewById<Button>(Resource.Id.getDataBtn);
          //  openDialogBtn = FindViewById<Button>(Resource.Id.openDialog);


            LockScreen.GetInstance().Init(this);

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

            // do odkomentowania po zrobieniu osobnej metody dla zapisu ====
            adapterLang.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerLang.Adapter = adapterLang;
            //ustawienie spinnera wg wczesniejszego ustawienia
            int spinnerPositionLang = adapterLang.GetPosition(GetSharedPreferences("language_data"));
            spinnerLang.SetSelection(spinnerPositionLang);
            //===============================================================

            // spr czy LockScreen jest aktywny i ustawienie odpowiedniej wartości na togglebuttonie
            if (LockScreen.GetInstance().IsActive())  { switchBtn.Checked = true;}
            else { switchBtn.Checked = false; }

            switchBtn.Click += (o, e) =>
            {
                if (switchBtn.Checked)  LockScreen.GetInstance().Active();
                else  LockScreen.GetInstance().Deactivate();
            };

            dataBtn.Click += (o, e) =>
            {
                GettingWordsFromDatabase.GetWords("pl", 50);
            };

           
            

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

        private void SpinnerLangItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
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
        }

        public string GetSharedPreferences(string keyName)
        {
            //getting data from shared prefs
            prefs = PreferenceManager.GetDefaultSharedPreferences(this.ApplicationContext);
            string levelData = prefs.GetString(keyName, level);
            //Toast.MakeText(this, levelData, ToastLength.Long).Show();
            return levelData;
        }


    }

}

