using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading;

namespace UnhandledExceptionExample
{
    [Activity(Label = "UnhandledExceptionExample", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            // This is how to set the _CurrentActivity on the Application
            ((UnhandledExceptionExampleApplication)Application).CurrentActivity = this;
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { ThreadPool.QueueUserWorkItem(o => CrashBackground()); };
            //This Exception will be caught by  AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) =>{}; in the UnhandledExceptionExmapleApplication.cs
            throw new Exception("Crashed UI thread.");
        }
        protected void CrashBackground()
        {
            //This Exception will be caught by  AppDomain.CurrentDomain.UnhandledException += (s, e){}; in the UnhandledExceptionExmapleApplication.cs
            throw new Exception("Crashed Background thread.");
        }
    }
}

