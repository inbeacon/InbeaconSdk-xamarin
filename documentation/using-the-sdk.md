# Using the SDK
## InbeaconManager class

This is the main manager class that does all the heavy lifting. Notice that the inbeaconApplication class handles all initialisations of the InbeaconManagerclass. 

### InbeaconManager.Initialize()
Initialize the SDK with your credentials. This will create a singleton instance that is always accessible using `InbeaconManager.SharedInstance()` 

```csharp
// Android
InbeaconManager.Initialize(context,<your clientid>,<your clientsecret>);sdk = InbeaconManager.SharedInstance;
// iOS
sdk = inBeaconSdk.InBeaconWithClientID (<your clientid>, <your clientsecret>);
```
Initialize the SDK with your clientID and clientSecret. These credentials are used for communication with the server.
You can find your client-ID and client-Secret in your [account overview](http://console.inbeacon.nl/account) 


### SharedInstance()
Retrieves the shared InbeaconManager singleton class 

```csharp
sdk = InbeaconManager.SharedInstance;
```


### attachUser()

```csharp
sdk.AttachUser(userInfo);
```

The inBeacon backend has user information for each device. The user information are properties that fall in any of the 2 categories:

* Fixed properties. These always exist and control specific functionality. These are the fixed properties
  - `name`: Full user name, both first and family name. Example ‘Dwight Schulz’
  - `email`: User email. Example: ‘dwight@a-team.com’
  - `gender`: User gender: male, female or unknown
  - `country`: ISO3166 country code
  - `id`: inBeacon unique user id (read-only)
  - `avatar`: URL to user avatar

* Custom properties. You can define other properties, like "facebook-ID" or “age”

>Note that attached user info is not stored by the SDK. On app restart, you need to attach the user again.

### detachUser()
Removes all local device user properties.

```csharp
sdk.DetachUser();
```

Log out the current current user. From now only anonymous info is send to inBeacon server.


### refresh()
This starts all services and obtains new information from the server. You should call Refresh every once in a while to make sure server information is updated.

```csharp
    sdk.Refresh();
```

Obtain fresh trigger and region information from the inBeacon platform. Best practice is to call this when the app is 

* started

* returned to the foreground

so info is kept updated. 

### DidReceiveLocalNotification (iOS only)For iOS you need to forward localnotifications to the inBeacon SDK by putting an extra method in your appdelegate: 

```csharp
public bool OnNotificationReceived (object notification){  return sdk.DidReceiveLocalNotification((UIKit.UILocalNotification)notification);}
```

### CheckCapabilitiesAndRightsCheck to see if the app has the rights to run location and notification services. Returns BOOL NO if there is a problem.

```csharpsdk.CheckCapabilitiesAndRights(error as NSError); //(iOS)
sdk.VerifyCapabilities(); //(android)
```

### BeaconStateGet an array of all actual beacons within view, including their respective distance and proximity state. This method returns raw data without any filtering.     The returned beaconState array has 1 entry for each beacon in view. Each array item is a dictionary with the following data:* uuid beacon UUID value* major beacon major value* minor beacon minor value* proxesTimestamps of all proximities last seen, format: <proximity>: <time last seen>Proximities are F, N and I.* rawdist raw beacon distance in metres* rawproxraw proximity (F, N, I or U) U = undefined, beacon currently not visible>Because beacon information is updated once per second, it is not useful to obtain the beaconstate more often.


## Receiving inBeacon SDK events 

The inBeacon event mechanism uses a LocalBroadcastManager and intents with actions. 

#### Android
* `com.inbeacon.sdk.event.enterregion` user entered a region* `com.inbeacon.sdk.event.exitregion` user left a region* `com.inbeacon.sdk.event.enterlocation` user entered a location* `com.inbeacon.sdk.event.exitlocation` user left a location* `com.inbeacon.sdk.event.regionsupdate` region definitions were updated* `com.inbeacon.sdk.event.enterproximity` user entered a beacon proximity* `com.inbeacon.sdk.event.exitproximity` user left a beacon proximity* `com.inbeacon.sdk.event.proximity` low level proximity update, once every second when beacons arearound* `com.inbeacon.sdk.event.appevent` defined in the backend for special* `com.inbeacon.sdk.event.appaction` defined in the backend to handle your own local notifications

Android example:

```csharp
using InBeaconAndroid;	...public class MainActivity : Activity {	...	readonly BeaconEventReceiver receiver = new BeaconEventReceiver();	readonly IntentFilter filter = new IntentFilter();	protected override void OnCreate (Bundle savedInstanceState)	{		base.OnCreate (savedInstanceState);		receiver.EnterRegionEvent += delegate {    	   Console.WriteLine ("Entered region");		};		filter.AddAction (BeaconEventReceiver.EnterRegionAction);		...
	}	protected override void OnResume ()	{       base.OnResume ();       LocalBroadcastManager.GetInstance (this).RegisterReceiver (receiver, filter);	}	protected override void OnPause ()	{
		base.OnPause ();		LocalBroadcastManager.GetInstance (this).UnregisterReceiver (receiver);	}
```

#### iOS

* `inb:region` user entered or left a region* `inb:location` user entered or left a location* `inb:LocationsUpdate` region definitions were updated* `inb:proximity` Fired when proximity to a beacon changes* `inb:AppEvent` defined in the backend for special application events* `inb:AppAction` handle your own local notifications

iOS example

```csharp
public override bool FinishedLaunching (UIApplication app, NSDictionary options){	...	NSNotificationCenter.DefaultCenter.AddObserver (new NSString ("inb:region"), OnNotification);	... 
}public void OnNotification (NSNotification notification){	Console.WriteLine ("InBeacon notification: " + notification.Name);}
```	
	
### Android specific details
>this component requires the AndroidAltBeaconLibrary component to be included into the project as well. It is available in the Xamarin Component store.
In your Droid project, create an Android-specific implementation of the interface:

```csharpusing System;using Android.Content;using System.Collections.Generic;using InBeaconAndroid;namespace Sample.Droid {       public class AndroidInbeaconManager : IInbeacon {           private IInbeaconManagerInterface sdk;           public AndroidInbeaconManager (Context context)           {               InbeaconManager.Initialize (context, Constants.ClientID, Constants.ClientSecret);               sdk = InbeaconManager.SharedInstance;           }           #region IInbeaconManager implementation           public IInbeacon GetInstance ()           {               return this;           }           public void Refresh ()           {               sdk.Refresh ();           }           public void RefreshWithForce (bool force)           {               sdk.Refresh ();           }           public void CheckCapabilitiesAndRights (object ignored)           {               sdk.VerifyCapabilities ();           }           public void CheckCapabilitiesAndRightsWithAlert ()           {               sdk.VerifyCapabilities ();           }           public void AttachUser (Dictionary<string, string> userInfo)           {               sdk.AttachUser (userInfo);           }           public void DetachUser ()           {               sdk.DetachUser ();           }           public object[] GetInRegions ()           {               throw new NotImplementedException ();           }           public Dictionary<string, object> GetDeviceInfo ()           {               throw new NotImplementedException ();           }           public void ModalClick ()           {				throw new NotImplementedException ();
			 }			public bool OnNotificationReceived (object notification)    		{        		throw new NotImplementedException ();    		}			#endregion
		} 
}	
```
	
	
####Add required permissions to the manifestIn the Properties/AndroidManifest.xml, add the following permissions:

```xml       <uses-permission android:name="android.permission.BLUETOOTH" />       <uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />       <uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />       <uses-permission android:name="android.permission.RECEIVE_BOOT_COMPLETED"/>
```Also add the following inside the `<application></application>` block:
```xml       <receiver android:name="org.altbeacon.beacon.startup.StartupBroadcastReceiver">               <intent-filter>                      <action android:name="android.intent.action.BOOT_COMPLETED" />                      <action android:name="android.intent.action.ACTION_POWER_CONNECTED" />                      <action android:name="android.intent.action.ACTION_POWER_DISCONNECTED" />               </intent-filter>       </receiver>       <service android:enabled="true" android:exported="false" android:isolatedProcess="false"       android:label="beacon" android:name="org.altbeacon.beacon.service.BeaconService" />       <service android:name="org.altbeacon.beacon.BeaconIntentProcessor" android:enabled="true"       android:exported="false" />
```
Create interface instance in your Application classIn your Application class, create an instance of the AndroidInbeaconManager:

```csharp#if DEBUG[Application (Debuggable = true)]#else[Application(Debuggable=false)]#endifpublic class SampleApp : Application{	AndroidInbeaconManager inbeaconManager;	public SampleApp () {}	public SampleApp (IntPtr handle, JniHandleOwnership transfer) : base (handle, transfer) { }	public override void OnCreate ()	{		base.OnCreate ();		// Create new inBeacon instance		inbeaconManager = new AndroidInbeaconManager (this);	}	public AndroidInbeaconManager GetInbeacon ()	{			return inbeaconManager;
	}
}	
```

Then, in your MainActivity, pass it on to the Xamarin.Forms App class as follows:

```csharpprotected override void OnCreate (Bundle savedInstanceState){	base.OnCreate (savedInstanceState);	global::Xamarin.Forms.Forms.Init (this, savedInstanceState);	// Get inbeacon instance from the Application class and pass it along to the App	var inbeacon = (Application as SampleApp).GetInbeacon ();	LoadApplication (new App (inbeacon));
}
```
	
	
### iOS specific details

Similar to Android, create an iOS-specific implementation of the interface in your iOS project:

```csharpusing System;using InBeaconIOS;using System.Collections.Generic;using Foundation;using InBeaconIOS;namespace Sample.iOS{	public class iOSInbeaconManager : IInbeacon	{		private readonly inBeaconSdk sdk;		public iOSInbeaconManager ()		{			sdk = inBeaconSdk.InBeaconWithClientID (Constants.ClientID, Constants.ClientSecret);		}		#region IInbeacon implementation		public IInbeacon GetInstance ()		{			return (IInbeacon)sdk;		}		public void Refresh ()		{			sdk.Refresh ();		}		public void RefreshWithForce (bool force)		{			sdk.RefreshWithForce (force);		}		public void CheckCapabilitiesAndRights (object error)		{			sdk.CheckCapabilitiesAndRights (error as NSError);		}		public void CheckCapabilitiesAndRightsWithAlert ()
		{			sdk.CheckCapabilitiesAndRightsWithAlert ();		}		public void AttachUser (Dictionary<string, string> userInfo)		{			if (userInfo == null)				throw new ArgumentNullException ("userInfo");			var nativeDict = new NSMutableDictionary ();			foreach (var item in userInfo) {				nativeDict.Add ((NSString)item.Key, (NSString)item.Value);
			}			sdk.AttachUser (nativeDict);		}		public void DetachUser ()		{			sdk.DetachUser ();		}		public object[] GetInRegions ()		{			NSArray nativeArray = sdk.InRegions;			var arr = new object[nativeArray.Count];			for (nuint i = 0; i < nativeArray.Count; i++) {				NSObject obj = nativeArray.GetItem<NSObject> (i);				arr [i] = obj;			}			return arr;
		}		public Dictionary<string, object> GetDeviceInfo ()		{			var dict = new Dictionary<string, object> ();			foreach (var item in sdk.DeviceInfo) {				dict.Add (item.Key.ToString (), item.Value.ToString ());
			}			return dict;
		}		public void ModalClick ()		{			sdk.Modalclick ();		}		public bool OnNotificationReceived (object notification)		{			return sdk.DidReceiveLocalNotification ((UIKit.UILocalNotification)notification);
   		}		#endregion	} 
}```##### Update Info.plistiOS requires a text that explain why the app should be allowed to use the location services. Add the following keys to the Info.plist file and supply a string value with an explanation:

```xml<key>NSLocationAlwaysUsageDescription</key> 
<string> To detect iBeacons </string> 
<key>NSLocationWhenInUseUsageDescription</key> 
<string> To detect iBeacons </string>
```
If you want the inBeacon platform to run in continuous background mode (see before-you-start), go to the bottom of the Info.plist file editor (section 'Background modes'), enable 'Enable Background Modes' and check 'Location updates'.

```xml       <key>UIBackgroundModes</key>       <array>               <string>location</string>       </array>
```#####Create interface instance in your AppDelegateIn your AppDelegate class, you can now instantiate this class and pass it on to the Xamarin.Forms App class as follows:

```csharp       public override bool FinishedLaunching (UIApplication uiApplication, NSDictionary launchOptions)       {               global::Xamarin.Forms.Forms.Init ();               // Initialize InBeacon SDK               var inbeacon = new iOSInbeaconManager ();               LoadApplication (new App (inbeacon));               return base.FinishedLaunching (uiApplication, launchOptions);		}       public override void ReceivedLocalNotification (UIApplication application, UILocalNotification notification)       {			var inbeacon = iOSInbeaconManager.GetInstance ();			inbeacon.OnNotificationReceived (notification);
		}
```

#####Include audio resourcesWhen using customized sounds with notifications, make sure to copy the audio files from the iOS SDK into your app. To do that, find the  ./resources/*.caf  files in the iOS SDK and copy those files to the  ./Resources  folder in your iOS project. Next, right-click on the files in Xamarin and make sure that 'Build Action' is set to 'BundleResource'. The audio files can also be found in the sample project included in the component.

