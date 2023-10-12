using System.Reflection;

namespace ReactInXamarin.MobileApi.WebServer
{
    public interface IMobileWebServer
    {
        void Init();
        void Start();
        void Stop();
        void RegisterEmbeddedResources(string url, Assembly assembly, string prefix);
    }
}