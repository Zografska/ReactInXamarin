using System.Collections.Generic;
using System.ComponentModel;
using Android.Content;
using Android.OS;
using Android.Webkit;
using ReactInXamarin.Controls;
using ReactInXamarin.Droid.Renderers;
using Swan;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace ReactInXamarin.Droid.Renderers
{
    public class HybridWebViewRenderer : ViewRenderer<HybridWebView, NativeWebView>
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(Element.Message) && Element.Message != null)
            {
                Control.EvaluateJavascript($"window.onReceiveMessage({Element.Message.ToJson()})", null);
                int resourceId = Resources.GetIdentifier("status_bar_height", "dimen", "android");
                (Element as HybridWebView).StatusBarOffset = (int)(Resources.GetDimensionPixelSize(resourceId) / Resources.DisplayMetrics.Density);
            }
            else if (e.PropertyName == nameof(Element.SourceUrl) && !string.IsNullOrEmpty(Element.SourceUrl))
            {
                LoadUrlInControl();
            }
        }
        
        private void LoadUrlInControl(Dictionary<string, string> headers = null)
        {
            if (Element.SourceUrl == null) return;

            if (headers != null)
            {
                Control.LoadUrl(Element.SourceUrl, headers);
            }
            else
            {
                Control.LoadUrl(Element.SourceUrl);
            }
        }

        private readonly Context _context;
        private const string JavascriptFunction = "function invokeCSharpAction(data){jsBridge.invokeAction(data);}";
        
        public HybridWebViewRenderer(Context context) : base(context)
        {
            _context = context;
#if DEBUG
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                NativeWebView.SetWebContentsDebuggingEnabled(true);
            }
#endif
        }



        protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            if (Control == null)
            {
                var webView = new NativeWebView(_context);
                webView.Settings.JavaScriptEnabled = true;
                webView.Settings.DomStorageEnabled = true;
                webView.SetWebViewClient(new JavascriptWebViewClient($"javascript: {JavascriptFunction}",
                    e.NewElement.WebViewInterceptionUrl, e.NewElement.ConnectionFailureUrl));
                SetNativeControl(webView);
            }

            if (e.OldElement != null)
            {
                Control.RemoveJavascriptInterface("jsBridge");
                e.OldElement.WebViewReloadEvent -= HybridWebViewReloadEvent;
            }

            if (e.NewElement != null)
            {
                Control.Settings.SetAppCacheEnabled(e.NewElement.IsCacheEnabled);
                if (!e.NewElement.IsCacheEnabled)
                {
                    Control.Settings.CacheMode = CacheModes.NoCache;
                    var cookieManager = CookieManager.Instance;
                    cookieManager.RemoveAllCookie();
                }
                else
                    Control.Settings.CacheMode = CacheModes.Default;

                e.NewElement.WebViewReloadEvent += HybridWebViewReloadEvent;
                Control.AddJavascriptInterface(new JsBridge(this), "jsBridge");
                LoadContent();
            }
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            // https://stackoverflow.com/questions/43727651/why-did-modifying-the-webview-layout-parameter-fix-the-0-height-body-issue
            if (Control != null)
            {
                Control.LayoutParameters.Height = -1;
            }

            base.OnSizeChanged(w, h, oldw, oldh);
        }

        private void LoadContent()
        {
            if (!(Element.Source is null))
                Control.LoadData(Element.Source.Html, "text/html", "utf-8");
            else
            {
                var headers = Element.HasHeaderEnabled && Element.WebViewRequestHeader != null ?
                        GenerateWebViewRequestHeaders(Element.WebViewRequestHeader) : null;
                LoadUrlInControl(headers);
            }
        }

        private void HybridWebViewReloadEvent(object sender, System.EventArgs e)
        {
            Control?.Reload();
        }


        // <summary>
        /// Creating default header values for current request
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        private Dictionary<string, string> GenerateWebViewRequestHeaders(Dictionary<string, string> headers)
        {
            var keyValues = new Dictionary<string, string>();
            foreach (var item in headers)
            {
                keyValues.Add(item.Key, item.Value);
            }
            return keyValues;
        }
    }
}
