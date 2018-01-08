using Android.App;
using Android.Widget;
using Android.OS;
using App3.utils;
using System;

namespace App3
{
    [Activity(Label = "App3", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Spinner spinner;
        Spinner spinnerLang;
        Switch switchBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            spinner = FindViewById<Spinner>(Resource.Id.spinner);
            spinnerLang = FindViewById<Spinner>(Resource.Id.spinnerLanguage);
            switchBtn = FindViewById<Switch>(Resource.Id.switchButton);

            LockScreen.GetInstance().Init(this);


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

             
            // =================spinner for choosing level==========================================
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpinnerItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.levels_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            // =================spinner for choosing language==========================================
            spinnerLang.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpinnerItemSelected);
            var adapterLang = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.language_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapterLang.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerLang.Adapter = adapterLang;
        }


        private void SpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("Choosen: {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
    }
}

