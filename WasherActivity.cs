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
using App9.Services;

namespace App9
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = false)]
    public class WasherActivity : Activity
    {
        Button washerButton, valveButton, backButton;
        

        

        MQTTManager mqttManager = new MQTTManager();

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.washer_layout);

           

            //Кнопка мойки
            washerButton = FindViewById<Button>(Resource.Id.washerLayoutButton);
            washerButton.Click += (sender, e) =>
            {
                if (mqttManager.ClientRecieveMessage("MegaBasement/WaterWash") == "0")
                {
                    washerButton.Background = GetDrawable(Resource.Mipmap.washerbuttonon);
                    mqttManager.ClientPublish("MegaBasement/WaterWash", "1");
                }
                else
                {
                    washerButton.Background = GetDrawable(Resource.Mipmap.washerbuttonoff);
                    mqttManager.ClientPublish("MegaBasement/WaterWash", "0");
                }
            };

            //Кнопка клапана
            valveButton = FindViewById<Button>(Resource.Id.valveButton);
            valveButton.Click += (sender, e) =>
            {
                if (mqttManager.ClientRecieveMessage("MegaBasement/WaterKl") == "0")
                {
                    valveButton.Background = GetDrawable(Resource.Mipmap.valvebuttonon);
                    mqttManager.ClientPublish("MegaBasement/WaterKl", "1");
                }
                else
                {
                    valveButton.Background = GetDrawable(Resource.Mipmap.valvebuttonoff);
                    mqttManager.ClientPublish("MegaBasement/WaterKl", "0");
                }
            };

            //Кнопка возврата на главную страницу
            backButton = FindViewById<Button>(Resource.Id.backButton);
            backButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };






        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

