using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Prism.Common;

namespace ReactInXamarin.MobileApi.Controllers
{
    public class BaseMobileController : WebApiController, IMobileController
    {
        public string BaseRoute => "/api/items";

        public void RegisterController(WebApiModule module)
        {
            module.WithController(() => this);
        }

        [Route(HttpVerbs.Get, "/item-details/{serverId}/{localId}")]
        public async Task<string> NavigateToItemDetails(int serverId, int localId)
        {
            return "OK";
        }
    }
}