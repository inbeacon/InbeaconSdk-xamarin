using System;

using Android.App;
using Android.Content.PM;
using Android.OS;
using InBeaconAndroid;
using Android.Content;
using Android.Support.V4.Content;

namespace inBeaconSample.Droid
{
	[Activity (Label = "inBeaconSample.Android.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		readonly BeaconEventReceiver receiver = new BeaconEventReceiver ();
		readonly IntentFilter filter = new IntentFilter ();

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			global::Xamarin.Forms.Forms.Init (this, savedInstanceState);

			// Create an event listener for inBeacon events
			filter.AddAction (BeaconEventReceiver.EnterRegionAction);
			receiver.EnterRegionEvent += delegate {
				Console.WriteLine ("Entered a region!");
			};

			// Get inbeacon instance from the Application class and pass it along to the App
			var inbeacon = (Application as SampleApp).InBeacon;
			LoadApplication (new App (inbeacon));
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			// Start listening for inBeacon events
			LocalBroadcastManager.GetInstance (this).RegisterReceiver (receiver, filter);
		}

		protected override void OnPause ()
		{
			base.OnPause ();
			// Stop listening for inBeacon events
			LocalBroadcastManager.GetInstance (this).UnregisterReceiver (receiver);
		}
	}
}

