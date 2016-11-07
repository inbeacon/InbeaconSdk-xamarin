using System;
using Android.App;
using Android.Runtime;

namespace inBeaconSample.Droid
{

#if DEBUG
	[Application (Debuggable = true)]
#else
	[Application(Debuggable=false)]
#endif
	public class SampleApp : Application
	{

		private AndroidInbeaconManager _inBeaconManager;

		public SampleApp ()
		{
		}

		public SampleApp (IntPtr handle, JniHandleOwnership transfer)
			: base (handle, transfer)
		{
		}

		public override void OnCreate ()
		{
			base.OnCreate ();
			// Create new inBeacon instance
			_inBeaconManager = new AndroidInbeaconManager (this);
		}

		public AndroidInbeaconManager InBeacon {
			get {
				return _inBeaconManager;
			}
		}
	}
}

