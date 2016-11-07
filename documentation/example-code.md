# Examples

## Sample app

You will find a sample in the repository:

[Example project](https://github.com/inbeacon/InbeaconSdk-xamarin/tree/master/example)


##Implementation exampleWhen using Xamarin.Forms and a shared code library, we suggest the following structure in order to avoid implementing the library with many  `#ifdef`  statements.

###The shared code projectThis interface represents a common codebase for the Android and iOS libraries.

```csharpusing System;using System.Collections.Generic;namespace Sample {	public interface IInbeacon {		IInbeacon GetInstance ();		void Refresh ();		void RefreshWithForce (bool force);		void CheckCapabilitiesAndRights (Object error);		void CheckCapabilitiesAndRightsWithAlert ();		void AttachUser (Dictionary<string,string> userInfo);		void DetachUser ();		object[] GetInRegions ();		Dictionary<string,object> GetDeviceInfo ();		void ModalClick ();		bool OnNotificationReceived (object notification);
	}
}
```	

####Enhance the App class in the shared code projectNext, you can add an instance of this interface as a parameter to your shared App class:

```csharp	readonly IInbeacon inbeacon;	...
	public App (IInbeacon inbeacon)
	{		this.inbeacon = inbeacon;		... 
	}	protected override void OnResume ()	{		if (inbeacon != null)		{			inbeacon.Refresh ();
		}
	}
```	