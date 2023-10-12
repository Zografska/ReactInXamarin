using System.Collections.Generic;
using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Prism.Common;
using ReactInXamarin.Models;
using ReactInXamarin.Services;

namespace ReactInXamarin.MobileApi.Controllers
{
    public class BaseMobileController : WebApiController, IMobileController
    {
        protected readonly IItemsService _itemsService;
        public BaseMobileController(IItemsService itemsService)
        {
            _itemsService = itemsService;
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
    }
}