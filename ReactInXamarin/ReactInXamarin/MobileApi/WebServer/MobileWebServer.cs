using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using EmbedIO;
// using EmbedIO.BearerToken;
using ReactInXamarin.MobileApi.Controllers;
using Xamarin.Forms.Internals;

namespace ReactInXamarin.MobileApi.WebServer
{
    public class MobileWebServer : EmbedIO.WebServer, IMobileWebServer
    {
        private IReadOnlyList<IMobileController> _mobileControllers;
        private CancellationTokenSource _cancellationTokenSource;

        public MobileWebServer(IMobileController mobileController) :
            base(HttpListenerMode.EmbedIO, "http://127.0.0.1:8081")
        {
            _mobileControllers = new List<IMobileController>()
            {
                mobileController
            }.AsReadOnly();
        }

        public void Init()
        {
            this.WithLocalSessionManager();
            this.WithCors();
            // this.WithBearerToken("/index.html*", 
            //     (Guid.NewGuid() + Guid.NewGuid().ToString()).Replace("-", "")
            //     .Substring(0,40), new BasicAuthorizationServerProvider());
            _mobileControllers.ForEach(p => this.WithWebApi(p.BaseRoute, p.RegisterController));
        }

        public void Start()
        {
            if (_cancellationTokenSource != null)
            {
                throw new Exception("Server is already started.");
            }

            _cancellationTokenSource = new CancellationTokenSource();
            _ = RunAsync(_cancellationTokenSource.Token);
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = null;
        }

        public void RegisterEmbeddedResources(string url, Assembly assembly, string prefix)
        {
            this.WithEmbeddedResources(url, assembly, prefix);
        }
    }
}