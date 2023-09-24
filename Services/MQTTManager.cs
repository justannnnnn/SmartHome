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
using OpenNETCF.MQTT;

namespace App9.Services
{
    class MQTTManager
    {
        private const string BROKERHOSTNAMENET = "10.100.100.88";
        private const string BROKERHOSTNAMEKEENETIC = "192.162.1.29";
        private MQTTClient Client;


        public MQTTManager()
        {
            Client = new MQTTClient(BROKERHOSTNAMENET);
        }

        public void ClientConnect()
        {
            if (!Client.IsConnected)
            {
                Client.Connect("EspUserMQTT", "EspUserMQTT", "GfhjkmyfMQTT");
            }


        }


        public void ClientRecieveMessage(string topicString, TextView textView, Func<string, string> function = null)
        {

            var receivedMessage = false;
            ClientConnect();
            Client.Subscriptions.Add(new Subscription(topicString));
            Client.MessageReceived += (topic, qos, data) =>
            {
                if (topic == topicString)
                {
                    if (function != null)
                        textView.Text = function(data.Aggregate("", (str, text) => str + char.ConvertFromUtf32((int)text)));
                    else
                        textView.Text = data.Aggregate("", (str, text) => str + char.ConvertFromUtf32(text));
                    receivedMessage = true;
                }

            };


        }

        public void ClientRecieveMessage(string topicString, ImageView imageView, Android.Graphics.Drawables.Drawable first, Android.Graphics.Drawables.Drawable second, string valueForFirst, string valueForSecond)
        {
            var receivedMessage = false;
            ClientConnect();
            Client.Subscriptions.Add(new Subscription(topicString));
            Client.MessageReceived += (topic, qos, data) =>
            {
                if (topic == topicString)
                {
                    string result = data.Aggregate("", (str, text) => str + char.ConvertFromUtf32((int)text));
                    if (result == valueForFirst)
                        imageView.Background = first;
                    else
                        imageView.Background = second;
                    receivedMessage = true;
                }

            };
        }

        public void ClientRecieveMessage(string topicString, Android.Widget.ProgressBar progressBar, Func<int, int> func = null)
        {
            var recievedMessage = false;
            ClientConnect();
            Client.Subscriptions.Add(new Subscription(topicString));
            Client.MessageReceived += (topic, qos, data) =>
            {
                if (topic == topicString)
                {
                    if (func != null)
                        progressBar.Progress = func(int.Parse(data.Aggregate("", (str, text) => str + char.ConvertFromUtf32((int)text))));
                    else
                        progressBar.Progress = int.Parse(data.Aggregate("", (str, text) => str + char.ConvertFromUtf32((int)text)));
                }
            };
        }

        public string ClientRecieveMessage(string topicString)
        {

            string result = "";
            var receivedMessage = false;
            ClientConnect();
            Client.Subscriptions.Add(new Subscription(topicString));

            do
            {
                Client.MessageReceived += (topic, qos, data) =>
                {
                    if (topic == topicString)
                    {
                        result = data.Aggregate("", (str, text) => str + char.ConvertFromUtf32((int)text));
                        receivedMessage = true;
                    }

                };
            } while (result == "");




            return result;

        }

        public void ClientPublish(string topic, string value)
        {
            ClientConnect();
            Client.Publish(topic, value, QoS.FireAndForget, false);
        }

    
}
}
