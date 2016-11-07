# Using the SDK
## InbeaconManager class

This is the main manager class that does all the heavy lifting. Notice that the inbeaconApplication class handles all initialisations of the InbeaconManagerclass. 

### InbeaconManager.Initialize()
Initialize the SDK with your credentials. This will create a singleton instance that is always accessible using `InbeaconManager.SharedInstance()` 

```csharp
// Android
InbeaconManager.Initialize(context,<your clientid>,<your clientsecret>);
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

### DidReceiveLocalNotification (iOS only)

```csharp
public bool OnNotificationReceived (object notification)
```

### CheckCapabilitiesAndRights

```csharp
sdk.VerifyCapabilities(); //(android)
```

### BeaconState


## Receiving inBeacon SDK events 

The inBeacon event mechanism uses a LocalBroadcastManager and intents with actions. 

#### Android


Android example:

```csharp
using InBeaconAndroid;
	}
		base.OnPause ();
```

#### iOS

* `inb:region` user entered or left a region

iOS example

```csharp
public override bool FinishedLaunching (UIApplication app, NSDictionary options)
}
```	
	
### Android specific details
>this component requires the AndroidAltBeaconLibrary component to be included into the project as well. It is available in the Xamarin Component store.


```csharp
			 }
		} 
}	
```
	
	
####Add required permissions to the manifest

```xml
```

```


```csharp
	}
}	
```

Then, in your MainActivity, pass it on to the Xamarin.Forms App class as follows:

```csharp
}
```
	
	
### iOS specific details

Similar to Android, create an iOS-specific implementation of the interface in your iOS project:

```csharp
		{
			}
		}
			}
		}
   		}
}

```xml
<string> To detect iBeacons </string> 
<key>NSLocationWhenInUseUsageDescription</key> 
<string> To detect iBeacons </string>
```


```xml
```

```csharp
		}
```

#####Include audio resources

