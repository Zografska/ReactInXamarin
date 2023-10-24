using System.Collections.Generic;
using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Prism.Common;
using Prism.Navigation;
using ReactInXamarin.Models;
using ReactInXamarin.Pages;
using ReactInXamarin.Services;
using Xamarin.Forms;

namespace ReactInXamarin.MobileApi.Controllers
{
    public class BaseMobileController : WebApiController, IMobileController
    {
        protected readonly IItemsService _itemsService;
        protected readonly INavigationService _navigationService;
        public BaseMobileController(IItemsService itemsService, INavigationService navigationService)
        {
            _itemsService = itemsService;
            _navigationService = navigationService;
        }
        public string BaseRoute => "/api/items";

        public void RegisterController(WebApiModule module)
        {
            module.WithController(() => this);
        }

        [Route(HttpVerbs.Get, "/{listId}")]
        public async Task<IEnumerable<Item>> GetItems(string listId)
        {
            return _itemsService.GetItems(listId);
        }
        
        [Route(HttpVerbs.Get, "/navigate/{itemId}")]
        public async Task<string> NavigateToAnotherPage(string itemId)
        {
            Device.BeginInvokeOnMainThread(async () => 
                await _navigationService.NavigateTo<ItemPage>
                    (new NavigationParameters { { "ItemId", itemId } }));
            return "OK";
        }
    }
}