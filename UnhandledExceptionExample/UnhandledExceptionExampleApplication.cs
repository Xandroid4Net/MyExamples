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

namespace UnhandledExceptionExample
{
    [Application]
    class UnhandledExceptionExampleApplication : Application, Application.IActivityLifecycleCallbacks
    {
        private Activity _CurrentActivity;
        public Activity CurrentActivity
        {
            get { return _CurrentActivity; }
            set { _CurrentActivity = value; }
        }
        public UnhandledExceptionExampleApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }
        public override void OnCreate()
        {
            base.OnCreate();
            AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) =>
            {
                /*
                 * When the UI Thread crashes this is the code that will be executed. There is no context at this point
                 * and no way to recover from the exception. This is where you would capture the error and log it to a 
                 * file for example. You might be able to post to a web handler, I have not tried that.
                 * 
                 * You can access the information about the exception in the args.Exception object.
                 */
            };
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                /*
                 * When a background thread crashes this is the code that will be executed. You can
                 * recover from this.
                 * You might for example:
                 *  _CurrentActivity.RunOnUiThread(() => Toast.MakeText(_CurrentActivity, "Unhadled Exception was thrown", ToastLength.Short).Show());
                 *  
                 * or
                 * 
                 * _CurrentActivity.StartActivity(typeof(SomeClass));
                 * _CurrentActivity.Finish();
                 *
                 * It is up to the developer as to what he/she wants to do here.
                 * 
                 * If you are requiring a minimum version less than API 14, you would have to set _CurrentActivity in each time
                 * the a different activity is brought to the foreground.
                 */
            };
        }
        // IActivityLifecycleCallbacks Requires App to target API 14 and above. This can be used to keep track of the current Activity
        #region IActivityLifecycleCallbacks Members

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            _CurrentActivity = activity;
            //throw new NotImplementedException();
        }

        public void OnActivityDestroyed(Activity activity)
        {
            //throw new NotImplementedException();
        }

        public void OnActivityPaused(Activity activity)
        {
            //throw new NotImplementedException();
        }

        public void OnActivityResumed(Activity activity)
        {
            //throw new NotImplementedException();
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
            //throw new NotImplementedException();
        }

        public void OnActivityStarted(Activity activity)
        {
            throw new NotImplementedException();
        }

        public void OnActivityStopped(Activity activity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}