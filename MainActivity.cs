using Android.App;
using Android.Content;
using Android.OS;

using Android.Runtime;
using Android.Widget;
using App9.Services;

namespace App9
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true)]
    public class MainActivity : Activity
    {
        
        Button buttonGate, buttonDoor, buttonOutside, buttonDryer, buttonWasher, buttonBasement, buttonInside, buttonSecurity;


        MQTTManager mqttManager = new MQTTManager();



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            //Кнокпа управления воротами
            buttonGate = FindViewById<Button>(Resource.Id.gateButton);
            buttonGate.Click += (sender, e) =>
            {

                mqttManager.ClientPublish("MegaBasement/Gate", "1");
            };


            //Кнопка управления дверью
            buttonDoor = FindViewById<Button>(Resource.Id.doorButton);
            buttonDoor.Click += (sender, e) =>
            {
                mqttManager.ClientPublish("MegaBasement/Lock", "1");
            };


            //Кнопка управления другими страницами приложения:


            //Страница улицы
            buttonOutside = FindViewById<Button>(Resource.Id.outsideButton);
            buttonOutside.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(OutsideActivity));
                StartActivity(intent);
            };


            //Страница сушилки
            buttonDryer = FindViewById<Button>(Resource.Id.dryerButton);
            buttonDryer.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(DryerActivity));
                StartActivity(intent);
                
            };


            //Страница мойки
            buttonWasher = FindViewById<Button>(Resource.Id.washerButton);
            buttonWasher.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(WasherActivity));
                StartActivity(intent);
            };


            //Страница подвала
            buttonBasement = FindViewById<Button>(Resource.Id.basementButton);
            buttonBasement.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(BasementActivity));
                StartActivity(intent);
            };


            //Страница дома
            buttonInside = FindViewById<Button>(Resource.Id.insideButton);
            buttonInside.Click += (sender, e) =>
            {
                //var intent = new Intent(this, typeof(InsideActivity));
                //StartActivity(intent);
            };


            //Страница охраны
            buttonSecurity = FindViewById<Button>(Resource.Id.securityButton);
            buttonSecurity.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(SecurityActivity));
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