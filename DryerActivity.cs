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
        public class DryerActivity : Activity
        {
            Button backButton, minusButton, plusButton, tempStateButton, timeStateButton, powerButton;
            ImageView imageStateView;

            static Dictionary<int, int> tempStates = new Dictionary<int, int>();
            static Dictionary<int, int> timeStates = new Dictionary<int, int>();
            static Dictionary<int, int> tempButtonStates = new Dictionary<int, int>();
            static Dictionary<int, int> timeButtonStates = new Dictionary<int, int>();
            static int createCount = 0;

            MQTTManager mqttManager = new MQTTManager();
            

            void FillDictionaries()
            {
                tempStates.Add(0, Resource.Mipmap.state0);
                tempStates.Add(1, Resource.Mipmap.state1);
                tempStates.Add(2, Resource.Mipmap.state2);
                tempStates.Add(3, Resource.Mipmap.state3);
                tempStates.Add(4, Resource.Mipmap.state4);
                tempStates.Add(5, Resource.Mipmap.state5);
                tempStates.Add(6, Resource.Mipmap.state6);
                tempStates.Add(7, Resource.Mipmap.state7);
                tempStates.Add(8, Resource.Mipmap.state8);
                tempStates.Add(9, Resource.Mipmap.state9);
                tempStates.Add(10, Resource.Mipmap.state10);
                tempStates.Add(11, Resource.Mipmap.state11);
                tempStates.Add(12, Resource.Mipmap.state12);
                tempStates.Add(13, Resource.Mipmap.state13);
                tempStates.Add(14, Resource.Mipmap.state14);
                tempStates.Add(15, Resource.Mipmap.state15);
                tempStates.Add(16, Resource.Mipmap.state16);
                tempStates.Add(17, Resource.Mipmap.state17);
                tempStates.Add(18, Resource.Mipmap.state18);
                tempStates.Add(19, Resource.Mipmap.state19); 
                tempStates.Add(20, Resource.Mipmap.state20);
                tempStates.Add(21, Resource.Mipmap.state21);
                tempStates.Add(22, Resource.Mipmap.state22);
                tempStates.Add(23, Resource.Mipmap.state23);

                timeStates.Add(0, Resource.Mipmap.statetime0);
                timeStates.Add(1, Resource.Mipmap.statetime1);
                timeStates.Add(2, Resource.Mipmap.statetime2);
                timeStates.Add(3, Resource.Mipmap.statetime3);
                timeStates.Add(4, Resource.Mipmap.statetime4);
                timeStates.Add(5, Resource.Mipmap.statetime5);
                timeStates.Add(6, Resource.Mipmap.statetime6);
                timeStates.Add(7, Resource.Mipmap.statetime7);
                timeStates.Add(8, Resource.Mipmap.statetime8);
                timeStates.Add(9, Resource.Mipmap.statetime9);
                timeStates.Add(10, Resource.Mipmap.statetime10);
                timeStates.Add(11, Resource.Mipmap.statetime11);
                timeStates.Add(12, Resource.Mipmap.statetime12);
                timeStates.Add(13, Resource.Mipmap.statetime13);
                timeStates.Add(14, Resource.Mipmap.statetime14);
                timeStates.Add(15, Resource.Mipmap.statetime15);
                timeStates.Add(16, Resource.Mipmap.statetime16);
                timeStates.Add(17, Resource.Mipmap.statetime17);
                timeStates.Add(18, Resource.Mipmap.statetime18);
                timeStates.Add(19, Resource.Mipmap.statetime19);
                timeStates.Add(20, Resource.Mipmap.statetime20);
                timeStates.Add(21, Resource.Mipmap.statetime21);
                timeStates.Add(22, Resource.Mipmap.statetime22);
                timeStates.Add(23, Resource.Mipmap.statetime23);

                tempButtonStates.Add(0, Resource.Mipmap.temperaturestate0);
                tempButtonStates.Add(1, Resource.Mipmap.temperaturestate1);
                tempButtonStates.Add(2, Resource.Mipmap.temperaturestate2);
                tempButtonStates.Add(3, Resource.Mipmap.temperaturestate3);
                tempButtonStates.Add(4, Resource.Mipmap.temperaturestate4);
                tempButtonStates.Add(5, Resource.Mipmap.temperaturestate5);
                tempButtonStates.Add(6, Resource.Mipmap.temperaturestate6);
                tempButtonStates.Add(7, Resource.Mipmap.temperaturestate7);
                tempButtonStates.Add(8, Resource.Mipmap.temperaturestate8);
                tempButtonStates.Add(9, Resource.Mipmap.temperaturestate9);
                tempButtonStates.Add(10, Resource.Mipmap.temperaturestate10);
                tempButtonStates.Add(11, Resource.Mipmap.temperaturestate11);
                tempButtonStates.Add(12, Resource.Mipmap.temperaturestate12);
                tempButtonStates.Add(13, Resource.Mipmap.temperaturestate13);
                tempButtonStates.Add(14, Resource.Mipmap.temperaturestate14);
                tempButtonStates.Add(15, Resource.Mipmap.temperaturestate15);
                tempButtonStates.Add(16, Resource.Mipmap.temperaturestate16);
                tempButtonStates.Add(17, Resource.Mipmap.temperaturestate17);
                tempButtonStates.Add(18, Resource.Mipmap.temperaturestate18);
                tempButtonStates.Add(19, Resource.Mipmap.temperaturestate19);
                tempButtonStates.Add(20, Resource.Mipmap.temperaturestate20);
                tempButtonStates.Add(21, Resource.Mipmap.temperaturestate21);
                tempButtonStates.Add(22, Resource.Mipmap.temperaturestate22);
                tempButtonStates.Add(23, Resource.Mipmap.temperaturestate23);

                timeButtonStates.Add(0, Resource.Mipmap.timestate0);
                timeButtonStates.Add(1, Resource.Mipmap.timestate1);
                timeButtonStates.Add(2, Resource.Mipmap.timestate2);
                timeButtonStates.Add(3, Resource.Mipmap.timestate3);
                timeButtonStates.Add(4, Resource.Mipmap.timestate4);
                timeButtonStates.Add(5, Resource.Mipmap.timestate5);
                timeButtonStates.Add(6, Resource.Mipmap.timestate6);
                timeButtonStates.Add(7, Resource.Mipmap.timestate7);
                timeButtonStates.Add(8, Resource.Mipmap.timestate8);
                timeButtonStates.Add(9, Resource.Mipmap.timestate9);
                timeButtonStates.Add(10, Resource.Mipmap.timestate10);
                timeButtonStates.Add(11, Resource.Mipmap.timestate11);
                timeButtonStates.Add(12, Resource.Mipmap.timestate12);
                timeButtonStates.Add(13, Resource.Mipmap.timestate13);
                timeButtonStates.Add(14, Resource.Mipmap.timestate14);
                timeButtonStates.Add(15, Resource.Mipmap.timestate15);
                timeButtonStates.Add(16, Resource.Mipmap.timestate16);
                timeButtonStates.Add(17, Resource.Mipmap.timestate17);
                timeButtonStates.Add(18, Resource.Mipmap.timestate18);
                timeButtonStates.Add(19, Resource.Mipmap.timestate19);
                timeButtonStates.Add(20, Resource.Mipmap.timestate20);
                timeButtonStates.Add(21, Resource.Mipmap.timestate21);
                timeButtonStates.Add(22, Resource.Mipmap.timestate22);
                timeButtonStates.Add(23, Resource.Mipmap.timestate23);
                createCount++;
            
        }

        

            protected override void OnCreate(Bundle savedInstanceState)
            {
                
                base.OnCreate(savedInstanceState);
                Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                SetContentView(Resource.Layout.dryer_layout);
                if (createCount == 0)
                    FillDictionaries();
                int tempCount = 0;
                int timeCount = 0;
                bool stateTemp = true;

                

                imageStateView = FindViewById<ImageView>(Resource.Id.imageStateView);

                //Кнопка настройки температуры
                tempStateButton = FindViewById<Button>(Resource.Id.tempStateButton);
                tempStateButton.Click += (sender, e) =>
                {
                    stateTemp = true;
                    tempStates.TryGetValue(tempCount, out int value1);
                    imageStateView.Background = GetDrawable(value1);
                };


                //Кнопка настройки времени
                timeStateButton = FindViewById<Button>(Resource.Id.timeStateButton);
                timeStateButton.Click += (sender, e) =>
                {
                    stateTemp = false;
                    timeStates.TryGetValue(timeCount, out int value1);
                    imageStateView.Background = GetDrawable(value1);
                };
                
                
                //Кнопки прибавление и вычитания значений State
                plusButton = FindViewById<Button>(Resource.Id.plusButton);
                plusButton.Click += (sender, e) =>
                {
                    plusButton.Background = GetDrawable(Resource.Mipmap.plusbuttonpressed);
                    if (stateTemp && tempCount < 23)
                    {
                        tempCount++;
                        tempStates.TryGetValue(tempCount, out int value1);
                        imageStateView.Background = GetDrawable(value1);
                        tempButtonStates.TryGetValue(tempCount, out int value2);
                        tempStateButton.Background = GetDrawable(value2);
                        
                    }
                    else
                    {
                        if (timeCount < 23)
                        {
                            timeCount++;
                            timeStates.TryGetValue(timeCount, out int value1);
                            imageStateView.Background = GetDrawable(value1);
                            timeButtonStates.TryGetValue(timeCount, out int value2);
                            timeStateButton.Background = GetDrawable(value2);
                            
                        }
                    }
                    plusButton.Background = GetDrawable(Resource.Mipmap.plusbuttonunpressed);

                };

                minusButton = FindViewById<Button>(Resource.Id.minusButton);
                minusButton.Click += (sender, e) =>
                {
                    minusButton.Background = GetDrawable(Resource.Mipmap.minusbuttonpressed);
                    if (stateTemp && tempCount > 0)
                    {
                        tempCount--;
                        tempStates.TryGetValue(tempCount, out int value1);
                        imageStateView.Background = GetDrawable(value1);
                        tempButtonStates.TryGetValue(tempCount, out int value2);
                        tempStateButton.Background = GetDrawable(value2);
                        

                    }
                    else
                    {
                        if (timeCount > 0)
                        {
                            timeCount--;
                            timeStates.TryGetValue(timeCount, out int value1);
                            imageStateView.Background = GetDrawable(value1);
                            timeButtonStates.TryGetValue(timeCount, out int value2);
                            timeStateButton.Background = GetDrawable(value2);
                            

                        }
                    }
                    minusButton.Background = GetDrawable(Resource.Mipmap.minusbuttonunpressed);

                };

            //Кнопка вкл/выкл сушилки
            powerButton = FindViewById<Button>(Resource.Id.powerButton);
            powerButton.Click += (sender, e) =>
            {
                mqttManager.ClientPublish("Sushilka/Temp", (40 + 2 * tempCount).ToString());
                mqttManager.ClientPublish("Sushilka/Time", (30 + 2 * timeCount).ToString());

                if (mqttManager.ClientRecieveMessage("Sushilka/Power") == "0")
                {
                    powerButton.Background = GetDrawable(Resource.Mipmap.powerbuttonon);
                    mqttManager.ClientPublish("Sushilka/Power", "1");
                }
                else
                {
                    powerButton.Background = GetDrawable(Resource.Mipmap.powerbuttonoff);
                    mqttManager.ClientPublish("Sushilka/Power", "0");
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
