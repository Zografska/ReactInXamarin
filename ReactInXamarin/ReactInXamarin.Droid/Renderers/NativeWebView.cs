using System;
using Android.Content;
using Android.Util;
using Android.Views;
using android = Android;

namespace ReactInXamarin.Droid.Renderers
{
    public class NativeWebView : android.Webkit.WebView, IDisposable
    {
        public NativeWebView(Context context) : base(context)
        {
        }

        public NativeWebView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public NativeWebView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public NativeWebView(IntPtr intPtr, android.Runtime.JniHandleOwnership jniHandleOwnership) : base(intPtr, jniHandleOwnership)
        {

        }

        /// <summary>
        ///     Interception is disallowed here because of scroll gesture conflicts with
        ///     Scrollview scroll
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override bool OnTouchEvent(MotionEvent e)
        {
            base.RequestDisallowInterceptTouchEvent(true);
            return base.OnTouchEvent(e);
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }
}
