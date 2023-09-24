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
    public class BasementActivity : Activity
    {
        Button backButton;
        ImageView waterlvlImageView, gaslvlImageView;




        MQTTManager mqttManager = new MQTTManager();

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.basement_layout);

            waterlvlImageView = FindViewById<ImageView>(Resource.Id.waterlvlImageView);
            mqttManager.ClientRecieveMessage("MegaBasement/Water", waterlvlImageView, GetDrawable(Resource.Mipmap.waterlvlnorm), GetDrawable(Resource.Mipmap.waterlvlalarm), "0", "1");

            gaslvlImageView = FindViewById<ImageView>(Resource.Id.gaslvlImageView);
            mqttManager.ClientRecieveMessage("MegaBasement/Gas", gaslvlImageView, GetDrawable(Resource.Mipmap.gaslvlnorm), GetDrawable(Resource.Mipmap.gaslvlalarm), "0", "1");


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