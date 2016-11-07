using System;
using System.Collections.Generic;

namespace inBeaconSample
{
	public interface IInbeacon
	{

		IInbeacon GetInstance ();

		void Refresh ();

		void RefreshWithForce (bool force);

		void CheckCapabilitiesAndRights (Object error);

		void CheckCapabilitiesAndRightsWithAlert ();

		void AttachUser (Dictionary<string,string> userInfo);

		void DetachUser ();

		object[] GetInRegions ();

		Dictionary<string,object> GetDeviceInfo ();

		void ModalClick ();

		bool OnNotificationReceived (object notification);

	}
}