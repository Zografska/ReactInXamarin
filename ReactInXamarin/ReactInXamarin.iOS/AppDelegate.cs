using DryIoc;
using Foundation;
using Prism;
using Prism.Ioc;
using ReactInXamarin.MobileApi.Controllers;
using ReactInXamarin.MobileApi.WebServer;
using UIKit;

namespace ReactInXamarin.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IPlatformInitializer
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public App XamarinApp { get; set; }
        private MobileWebServer _mobileWebServer;
        
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            XamarinApp = new App(this);
            XamarinApp.InitializeApp();
            LoadApplication(XamarinApp);
            
            return base.FinishedLaunching(app, options);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}