using System;
using System.Collections.Generic;
using Foundation;
using inBeaconSample;
using InBeaconIOS;

namespace inBeaconSample.iOS
{
	public class iOSInbeaconManager : IInbeacon
	{
		private readonly inBeaconSdk sdk;

		public iOSInbeaconManager ()
		{
			sdk = inBeaconSdk.InBeaconWithClientID ("YOUR_INBEACON_CLIENT_ID", "YOUR_INBEACON_CLIENT_SECRET");
		}

		#region IInbeacon implementation

		public IInbeacon GetInstance ()
		{
			return (IInbeacon)sdk;
		}

		public void Refresh ()
		{
			sdk.Refresh ();
		}

		public void RefreshWithForce (bool force)
		{
			sdk.RefreshWithForce (force);
		}

		public void CheckCapabilitiesAndRights (object error)
		{
			sdk.CheckCapabilitiesAndRights (error as NSError);
		}

		public void CheckCapabilitiesAndRightsWithAlert ()
		{
			sdk.CheckCapabilitiesAndRightsWithAlert ();
		}

		public void AttachUser (Dictionary<string, string> userInfo)
		{
			if (userInfo == null)
				throw new ArgumentNullException ("userInfo");

			var nativeDict = new NSMutableDictionary ();
			foreach (var item in userInfo) {				
				nativeDict.Add ((NSString)item.Key, (NSString)item.Value);
			}

			sdk.AttachUser (nativeDict);
		}

		public void DetachUser ()
		{
			sdk.DetachUser ();
		}

		public object[] GetInRegions ()
		{
			NSArray nativeArray = sdk.InRegions;
			var arr = new object[nativeArray.Count];
			for (nuint i = 0; i < nativeArray.Count; i++) {
				NSObject obj = nativeArray.GetItem<NSObject> (i);
				arr [i] = obj;
			}
			return arr;
		}

		public Dictionary<string, object> GetDeviceInfo ()
		{
			var dict = new Dictionary<string, object> ();
			foreach (var item in sdk.DeviceInfo) {
				dict.Add (item.Key.ToString (), item.Value.ToString ());
			}
			return dict;
		}

		public void ModalClick ()
		{
			sdk.Modalclick ();
		}

		public bool OnNotificationReceived (object notification)
		{
			return sdk.DidReceiveLocalNotification ((UIKit.UILocalNotification)notification);
		}

		#endregion
	}
}