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
    public class SecurityActivity : Activity
    {
        Button backButton;
        ImageView motionImageView;




        MQTTManager mqttManager = new MQTTManager();

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.security_layout);

            motionImageView = FindViewById<ImageView>(Resource.Id.motionImageView);

            mqttManager.ClientRecieveMessage("inOut/motionIn01", motionImageView, GetDrawable(Resource.Mipmap.motionno), GetDrawable(Resource.Mipmap.motionyes), "0", "1");

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