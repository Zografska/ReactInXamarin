using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using ReactInXamarin.MobileApi.Controllers;
using ReactInXamarin.MobileApi.WebServer;
using ReactInXamarin.Pages;
using ReactInXamarin.Services;
using ReactInXamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace ReactInXamarin
{
    public partial class App : PrismApplication
    {
        private MobileWebServer _mobileWebServer;
        public App(IPlatformInitializer initializer) : base(initializer) {}

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ItemPage, ItemPageViewModel>();
            containerRegistry.RegisterSingleton<IItemsService, ItemsService>();
            containerRegistry.RegisterSingleton<IMobileWebServer, MobileWebServer>();
            containerRegistry.RegisterSingleton<IMobileController, BaseMobileController>();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateTo<MainPage>();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            _mobileWebServer.Stop();
            _mobileWebServer.Dispose();
            _mobileWebServer = null;
        }

        public void InitializeApp()
        {
            StartMobileWebServer();
        }

        protected override void OnResume()
        {
            base.OnResume();
            StartMobileWebServer();
        }

        private void StartMobileWebServer()
        {
            var mobileController = App.Current.Container.Resolve<IMobileController>();

            _mobileWebServer = new MobileWebServer(mobileController);
            _mobileWebServer.Init();
            _mobileWebServer.RegisterEmbeddedResources("/", typeof(App).Assembly, $"{typeof(App).Assembly.GetName().Name}.html.build");
            _mobileWebServer.Start();
        }
    }
}