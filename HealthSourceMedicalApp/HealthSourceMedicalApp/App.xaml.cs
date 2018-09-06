using System;
using Xamarin.Forms;
using HealthSourceMedicalApp.Views;
using Xamarin.Forms.Xaml;
using HealthSourceMedicalApp.Common;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace HealthSourceMedicalApp
{
	public partial class App : Application
	{
		
		public App ()
		{
			InitializeComponent();
            //MainPage = new MainPage();
            MainPage = new StartupPage();
            SharedPush.Initialize();
            //OneSignal.Current.StartInit("YOUR_ONESIGNAL_APP_ID").EndInit();Unhandled Exception:

        }

		protected override void OnStart ()
		{
            // Handle when your app starts
            AppCenter.Start("ios=bdfd7c01-0a79-4201-8cbb-6d882dc1924f;" +
                  "uwp=hseap_uwp;" +
                  "android=hseap_android",
                  typeof(Analytics), typeof(Crashes));
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
            // Handle when your app resumes
            SharedPush.Initialize();
        }

    }
}
