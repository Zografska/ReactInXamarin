using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;

namespace ReactInXamarin.ViewModels
{
    public class ItemPageViewModel : BindableBase, INavigatedAware, IPageLifecycleAware
    {
        private string _itemId;
        public string ItemId
        {
            get => _itemId;
            set => SetProperty(ref _itemId, value);
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