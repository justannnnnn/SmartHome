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
    public class OutsideActivity : Activity
    {
        Button backButton, porchButton;

        MQTTManager mqttManager = new MQTTManager();

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.outside_layout);

            porchButton = FindViewById<Button>(Resource.Id.porchButton);
            porchButton.Click += (sender, e) =>
            {
                if (mqttManager.ClientRecieveMessage("inOut/Spotlight") == "0")
                {
                    porchButton.Background = GetDrawable(Resource.Mipmap.porchon);
                    mqttManager.ClientPublish("inOut/Spotlight", "1");
                }
                else
                {
                    porchButton.Background = GetDrawable(Resource.Mipmap.porchoff);
                    mqttManager.ClientPublish("inOut/Spotlight", "0");
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