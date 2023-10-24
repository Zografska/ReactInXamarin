using System.Threading.Tasks;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace ReactInXamarin.ViewModels
{
    public class ItemPageViewModel : BindableBase, INavigatedAware, IPageLifecycleAware
    {
        private readonly INavigationService _navigationService;
        private string _itemId;
        public string ItemId
        {
            get => _itemId;
            set => SetProperty(ref _itemId, value);
        }
        
        public Command GoBackCommand { get; set; }

        public ItemPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GoBackCommand = new Command(GoBack);
        }

        private void GoBack()
        {
            _navigationService.GoBackAsync();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            ItemId = parameters.GetValue<string>("ItemId");
        }

        public void OnAppearing()
        {
        }

        public void OnDisappearing()
        {
        }
    }
}