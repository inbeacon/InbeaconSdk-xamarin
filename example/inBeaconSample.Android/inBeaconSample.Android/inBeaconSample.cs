using System;
using Xamarin.Forms;

namespace inBeaconSample
{
	
	public class App : Application
	{
		
		readonly IInbeacon inbeacon;

		public App (IInbeacon inbeacon)
		{
			this.inbeacon = inbeacon;

			// The root page of your application
			MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						new Label {
							XAlign = TextAlignment.Center,
							Text = "Welcome to the inBeacon example!"
						}
					}
				}
			};
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			if (inbeacon != null) {
				inbeacon.Refresh ();
			}
		}
	}
}

