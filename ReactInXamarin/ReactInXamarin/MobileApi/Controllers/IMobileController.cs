using EmbedIO.WebApi;

namespace ReactInXamarin.MobileApi.Controllers
{
    public interface IMobileController
    {
        string BaseRoute { get; }

        void RegisterController(WebApiModule module);
    }
}