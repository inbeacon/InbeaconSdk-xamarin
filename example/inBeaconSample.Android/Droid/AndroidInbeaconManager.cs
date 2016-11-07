using System;
using Android.Content;
using System.Collections.Generic;
using InBeaconAndroid;

namespace inBeaconSample.Droid
{
	public class AndroidInbeaconManager : IInbeacon
	{

		private IInbeaconManagerInterface sdk;

		public AndroidInbeaconManager (Context context)
		{
			InbeaconManager.Initialize (context, "YOUR_INBEACON_CLIENT_ID", "YOUR_INBEACON_CLIENT_SECRET");
			sdk = InbeaconManager.SharedInstance;
		}

		#region IInbeaconManager implementation

		public IInbeacon GetInstance ()
		{
			return this;
		}

		public void Refresh ()
		{
			sdk.Refresh ();
		}

		public void RefreshWithForce (bool force)
		{
			sdk.Refresh ();
		}

		public void CheckCapabilitiesAndRights (object ignored)
		{
			sdk.VerifyCapabilities ();
		}

		public void CheckCapabilitiesAndRightsWithAlert ()
		{
			sdk.VerifyCapabilities ();
		}

		public void AttachUser (Dictionary<string, string> userInfo)
		{
			sdk.AttachUser (userInfo);
		}

		public void DetachUser ()
		{
			sdk.DetachUser ();
		}

		public object[] GetInRegions ()
		{
			throw new NotImplementedException ();
		}

		public Dictionary<string, object> GetDeviceInfo ()
		{
			throw new NotImplementedException ();
		}

		public void ModalClick ()
		{
			throw new NotImplementedException ();
		}

		public bool OnNotificationReceived (object notification)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}