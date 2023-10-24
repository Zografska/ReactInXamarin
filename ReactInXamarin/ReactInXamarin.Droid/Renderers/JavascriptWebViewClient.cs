using Android.Webkit;
using android = Android;

namespace ReactInXamarin.Droid.Renderers
{
    public class JavascriptWebViewClient : WebViewClient
    {
        private readonly string _javaScriptName;
        private readonly string _interceptionUrl;
        private readonly string _rejectionUrl;
        private bool _isLoadingFailed;


        public JavascriptWebViewClient(string javaScriptName, string interceptionUrl, string rejectionUrl)
        {
            _javaScriptName = javaScriptName;
            _interceptionUrl = interceptionUrl;
            _rejectionUrl = rejectionUrl;
        }

        public override void OnPageFinished(android.Webkit.WebView view, string url)
        {
            base.OnPageFinished(view, url);
            view.EvaluateJavascript(_javaScriptName, null);
            
            if (!_isLoadingFailed)
            {
                view.EvaluateJavascript(@"invokeCSharpAction(JSON.stringify({ action: ""DidFinishNavigation""}));", null);
            }
            _isLoadingFailed = false;
        }

        public override void OnReceivedError(android.Webkit.WebView view, IWebResourceRequest request, WebResourceError error)
        {
            base.OnReceivedError(view, request, error);
            view.EvaluateJavascript(_javaScriptName, null);
            _isLoadingFailed = true;
            
            view.EvaluateJavascript(@"invokeCSharpAction(JSON.stringify({ action: ""DidReceiveError""}));", null);
        }

#pragma warning disable CS0672 // Member overrides obsolete member
        public override bool ShouldOverrideUrlLoading(android.Webkit.WebView view, string url)
#pragma warning restore CS0672 // Member overrides obsolete member
        {
            if (android.OS.Build.VERSION.SdkInt >= android.OS.BuildVersionCodes.N) return false;

            if (!string.IsNullOrWhiteSpace(_interceptionUrl) && url == _interceptionUrl)
            {
                view.EvaluateJavascript(@"invokeCSharpAction(JSON.stringify({ action: ""Redirect""}));", null);
                return true;
            }

            else if (!string.IsNullOrWhiteSpace(_rejectionUrl) && (url == _rejectionUrl ||
                url.Contains(_rejectionUrl)))
            {
                view.EvaluateJavascript(@"invokeCSharpAction(JSON.stringify({ action: ""Reject""}));", null);
            }

#pragma warning disable CS0618 // Type or member is obsolete
            return base.ShouldOverrideUrlLoading(view, url);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public override bool ShouldOverrideUrlLoading(android.Webkit.WebView view, IWebResourceRequest request)
        {
            if (android.OS.Build.VERSION.SdkInt < android.OS.BuildVersionCodes.N) return false;

            if (!string.IsNullOrWhiteSpace(_interceptionUrl) && request.Url.ToString() == _interceptionUrl)
            {   
                view.EvaluateJavascript(@"invokeCSharpAction(JSON.stringify({ action: ""Redirect""}));", null);
                return true;
            }

            if (!string.IsNullOrWhiteSpace(_rejectionUrl) && (request.Url.ToString() == _rejectionUrl ||
                request.Url.ToString().Contains(_rejectionUrl)))
            {
                view.EvaluateJavascript(@"invokeCSharpAction(JSON.stringify({ action: ""Reject""}));", null);
            }

            return base.ShouldOverrideUrlLoading(view, request);
        }
    }
}
