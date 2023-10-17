using System.Windows.Input;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using ReactInXamarin.Models;
using Xamarin.Forms;

namespace ReactInXamarin.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigatedAware, IPageLifecycleAware
    {
        private string _sourceUrl;
        public string SourceUrl
        {
            get => _sourceUrl;
            set => SetProperty(ref _sourceUrl, value);
        }
        
        private WebViewMessage _message;
        public WebViewMessage Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
        public ICommand OnClickCommand { get; set; }
        
        public MainPageViewModel()
        {
            OnClickCommand = new Command<string>((color) =>
            {
                Message = new WebViewMessage { Action = "SayHelloFromXamarin", Data = color };
            });
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }

        public void OnAppearing()
        {
            // SourceUrl = "http://127.0.0.1:8081/?listId=list1";
        }

        public void OnDisappearing()
        {
        }
    }
}