using System;
using System.Collections.Generic;
using System.ComponentModel;
using Foundation;
using Newtonsoft.Json;
using ReactInXamarin.Controls;
using ReactInXamarin.iOS.Renderers;
using WebKit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace ReactInXamarin.iOS.Renderers
{
    public class HybridWebViewRenderer : ViewRenderer<HybridWebView, WKWebView>, IWKScriptMessageHandler,
        IWKNavigationDelegate
    {
        private const string JavaScriptFunction =
            "function invokeCSharpAction(data){window.webkit.messageHandlers.invokeAction.postMessage(data);}";

        private WKUserContentController _userController;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(Element.Message) && Element.Message != null)
            {
                Control.EvaluateJavaScript($"window.onReceiveMessage({JsonConvert.SerializeObject(Element.Message)})", null);
            }
            else if (e.PropertyName == nameof(Element.SourceUrl) && !string.IsNullOrEmpty(Element.SourceUrl))
            {
                LoadUrl(Element);
            }
            else if (e.PropertyName == nameof(Element.SourceUrl) && Element.Source.Html != null)
            {
                Control.LoadData(Element.Source.Html, "text/html", "utf-8", new NSUrl(""));
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                _userController.RemoveAllUserScripts();
                _userController.RemoveScriptMessageHandler("invokeAction");
                e.OldElement.WebViewReloadEvent -= HybridWebViewReloadEvent;
            }

            if (e.NewElement == null)
                return;

            if (Control == null)
            {
                _userController = new WKUserContentController();
                var script = new WKUserScript(new NSString(JavaScriptFunction), WKUserScriptInjectionTime.AtDocumentStart,
                    false);
                _userController.AddUserScript(script);
                _userController.AddScriptMessageHandler(this, "invokeAction");
                var config = new WKWebViewConfiguration {UserContentController = _userController};
                var webView = new WKWebView(Frame, config);
                webView.ScrollView.Bounces = false;
                webView.ScrollView.AlwaysBounceVertical = false;
                webView.NavigationDelegate = this;

                SetNativeControl(webView);
            }

            if (!e.NewElement.IsCacheEnabled)
            {
                NSHttpCookieStorage.SharedStorage.RemoveCookiesSinceDate(NSDate.DistantPast);
                WKWebsiteDataStore.DefaultDataStore.FetchDataRecordsOfTypes(WKWebsiteDataStore.AllWebsiteDataTypes,
                    records =>
                    {
                        for (nuint i = 0; i < records.Count; i++)
                        {
                            var record = records.GetItem<WKWebsiteDataRecord>(i);

                            WKWebsiteDataStore.DefaultDataStore.RemoveDataOfTypes(record.DataTypes,
                                new[] {record}, () => { });
                        }
                    });
            }
            (Element as HybridWebView).StatusBarOffset = (int)UIKit.UIApplication.SharedApplication.StatusBarFrame.Height;
            e.NewElement.WebViewReloadEvent += HybridWebViewReloadEvent;
            if (!string.IsNullOrEmpty(Element.SourceUrl))
            {
                LoadUrl(e.NewElement);
            }
        }
        
        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            if(message?.Body != null)
                Element?.Command?.Execute(message.Body.ToString());
        }

        private void HybridWebViewReloadEvent(object sender, System.EventArgs e)
        {
            // Page reload.
            var hybridWebView = (HybridWebView)sender;
            if (hybridWebView == null) return;
            LoadUrl(hybridWebView);
        }

        /// <summary>
        /// Creating default header values for current request
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        private NSMutableDictionary<NSString, NSString> GenerateWebViewRequestHeaders(Dictionary<string,string> headers)
        {
            var keyValues = new NSMutableDictionary<NSString, NSString>();
            foreach(var item in headers)
            {
                keyValues.Add((NSString)item.Key,(NSString)item.Value);
            }
            return keyValues;
        }

        /// <summary>
        /// Load url request
        /// </summary>
        /// <param name="webView"></param>
        private void LoadUrl(HybridWebView webView)
        {
            if (string.IsNullOrWhiteSpace(Element.SourceUrl))
                return;

            var webRequest = new NSMutableUrlRequest(new NSUrl(Element.SourceUrl));
            if (webView.HasHeaderEnabled && webView.WebViewRequestHeader != null)
            {
                webRequest.Headers = GenerateWebViewRequestHeaders(webView.WebViewRequestHeader);
            }
            Control.NavigationDelegate = new WKWebViewDelegate(webView);
            Control.LoadRequest(webRequest);
        }
    }

    public class WKWebViewDelegate : WKNavigationDelegate
    {
        HybridWebView _hybridWebView;

        public WKWebViewDelegate(HybridWebView hybrid)
        {
            _hybridWebView = hybrid;
        }

        public override void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            // We call the c# message bus handler when loading is completed
            _hybridWebView?.Command?.Execute(@"{ action: ""DidFinishNavigation""}");
        }

        public override void DidFailProvisionalNavigation(WKWebView webView, WKNavigation navigation, NSError error)
        {
            // This method is used to invoke a viewmodel call during
            // url loading fail
            _hybridWebView?.Command?.Execute(@"{ action: ""DidReceiveError""}");
        }

        public override void DecidePolicy(WKWebView webView, WKNavigationAction navigationAction, Action<WKNavigationActionPolicy> decisionHandler)
        {
            decisionHandler(WKNavigationActionPolicy.Allow);
            if (!string.IsNullOrWhiteSpace(_hybridWebView.WebViewInterceptionUrl) &&
                navigationAction.Request.Url.AbsoluteString == _hybridWebView.WebViewInterceptionUrl)
            {
                // We call the c# message bus handler when loading is completed
                _hybridWebView?.Command?.Execute(@"{ action: ""Redirect""}");
            }
            else if (!string.IsNullOrWhiteSpace(_hybridWebView.ConnectionFailureUrl) &&
                (navigationAction.Request.Url.AbsoluteString == _hybridWebView.ConnectionFailureUrl ||
                navigationAction.Request.Url.AbsoluteString.Contains(_hybridWebView.ConnectionFailureUrl)))
            {
                // We call the c# message bus handler when loading is completed
                _hybridWebView?.Command?.Execute(@"{ action: ""Reject""}");
            }

            // Because of known bug on WebView, phone links are not opened
            // This is workaround until bug is fixed in Xamarin.Forms v5.0
            // Bug status: https://github.com/xamarin/Xamarin.Forms/issues/8386
            if (navigationAction.Request.Url.Scheme == "tel")
            {
                try
                {
                    PhoneDialer.Open(navigationAction.Request.Url.ResourceSpecifier);
                }
                catch (FeatureNotSupportedException ex)
                {
                    
                }
                catch (Exception ex)
                {
                    
                }
            }
        }
    }
}