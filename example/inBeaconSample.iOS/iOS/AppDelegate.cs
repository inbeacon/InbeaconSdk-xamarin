using System;
using System.Collections.Generic;
using System.Linq;
using InBeaconIOS;
using Foundation;
using UIKit;

namespace inBeaconSample.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		private IInbeacon inbeacon;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			inbeacon = new iOSInbeaconManager ();
			LoadApplication (new App (inbeacon));

			return base.FinishedLaunching (app, options);
		}

		public override void ReceivedLocalNotification (UIApplication application, UILocalNotification notification)
		{
			inbeacon.OnNotificationReceived (notification);
		}
	}
}