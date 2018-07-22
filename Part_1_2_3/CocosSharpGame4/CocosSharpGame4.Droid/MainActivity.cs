using System;
using System.Diagnostics;

using Android.Content.PM;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Views;
using Android.Widget;
using Uri = Android.Net.Uri;
using Microsoft.Xna.Framework;

using CocosSharp;
using CocosSharpGame4.Shared;

namespace CocosSharpGame4.Droid
{

    [Activity(Label = "CocosSharpGame4.Droid"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Portrait
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden)]
    public class MainActivity : AndroidGameActivity
    {
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            CCApplication application = new CCApplication();
			application.ApplicationDelegate = new AppDelegate();

			this.SetContentView(application.AndroidContentView);

			application.StartGame();

        }        
    }


}

