﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using keepass2android;

namespace keepass2android
{
    [Activity(Label = AppNames.AppName, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden, Theme = "@style/Kp2aTheme_BlueActionBar",
        LaunchMode = LaunchMode.SingleInstance)]
    public class NoSecureDisplayActivity : AndroidX.AppCompat.App.AppCompatActivity
    {
        private readonly ActivityDesign _design;

        public NoSecureDisplayActivity()
        {
            _design = new ActivityDesign(this);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            _design.ApplyTheme();
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.no_secure_display_layout);
            FindViewById<Button>(Resource.Id.btn_goto_settings).Click += (sender, args) =>
            {
                AppSettingsActivity.Launch(this);
            };
            FindViewById<Button>(Resource.Id.disable_secure_screen_check).Click += (sender, args) =>
            {
                var prefs = PreferenceManager.GetDefaultSharedPreferences(LocaleManager.LocalizedAppContext);
                prefs.Edit()
                    .PutBoolean("no_secure_display_check", true)
                    .Commit();
                Finish();
            };
            FindViewById<Button>(Resource.Id.btn_close).Click += (sender, args) =>
            {
                Finish();
            };

        }

        protected override void OnResume()
        {
            base.OnResume();
            _design.ReapplyTheme();
            //close if displays changed
            if (!Util.SecureDisplayConfigured(this) || !Util.HasUnsecureDisplay(this))
                Finish();
        }
    }
}