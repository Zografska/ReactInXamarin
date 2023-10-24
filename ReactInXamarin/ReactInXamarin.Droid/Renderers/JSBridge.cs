using System;
using Android.Webkit;
using Java.Interop;

namespace ReactInXamarin.Droid.Renderers
{
    public class JsBridge : Java.Lang.Object
    {
        private readonly WeakReference<HybridWebViewRenderer> _hybridWebViewRenderer;

        public JsBridge(HybridWebViewRenderer hybridRenderer)
        {
            _hybridWebViewRenderer = new WeakReference<HybridWebViewRenderer>(hybridRenderer);
        }

        /// <summary>
        /// Mapping JS function with html
        /// </summary>
        /// <param name="data"></param>
        // Annotation that allows exposing methods to JavaScript.
        [JavascriptInterface]
        // Used on a method to indicate Java code generator to export a Java method that becomes an
        // Android Callable Wrapper (ACW).
        [Export("invokeAction")]
        public void InvokeAction(string data)
        {
            if (_hybridWebViewRenderer != null && _hybridWebViewRenderer.TryGetTarget(out var hybridRenderer)
                                               && !string.IsNullOrEmpty(data))
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    hybridRenderer?.Element?.Command?.Execute(data);
                });
            }
        }
    }
}
