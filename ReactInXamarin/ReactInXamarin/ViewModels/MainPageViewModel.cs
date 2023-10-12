using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;

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
        
        public MainPageViewModel()
        {
            
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }

        public void OnAppearing()
        {
            SourceUrl = "http://127.0.0.1:8081/?listId=list1";
        }

        public void OnDisappearing()
        {
        }
    }
}