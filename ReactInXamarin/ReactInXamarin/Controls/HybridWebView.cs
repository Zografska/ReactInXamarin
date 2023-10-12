using System;
using System.Collections.Generic;
using System.Windows.Input;
using ReactInXamarin.Models;
using Xamarin.Forms;

namespace ReactInXamarin.Controls
{
    public class HybridWebView : WebView
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(HybridWebView));

        public static readonly BindableProperty MessageProperty =
            BindableProperty.Create(nameof(Message), typeof(WebViewMessage), typeof(HybridWebView));

        public static readonly BindableProperty SourceUrlProperty =
            BindableProperty.Create(nameof(SourceUrl), typeof(string), typeof(HybridWebView),default(string),BindingMode.TwoWay);

        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create(nameof(Source), typeof(HtmlWebViewSource), typeof(HybridWebView));

        public static readonly BindableProperty HasStatusBarOffsetProperty =
            BindableProperty.Create(nameof(HasStatusBarOffset), typeof(bool), typeof(HybridWebView), false);

        public static readonly BindableProperty IsWebViewReloadRequiredProperty =
            BindableProperty.Create(nameof(IsWebViewReloadRequired), typeof(bool), typeof(HybridWebView),
                default(bool), BindingMode.TwoWay, null, propertyChanged: WebViewReloadPropertyChanged);

        public static readonly BindableProperty IsCacheEnabledProperty =
            BindableProperty.Create(nameof(IsCacheEnabled), typeof(bool), typeof(HybridWebView),true);

        public static readonly BindableProperty HasHeaderEnabledProperty =
            BindableProperty.Create(nameof(HasHeaderEnabled), typeof(bool), typeof(HybridWebView), true);

        public static readonly BindableProperty WebViewRequestHeaderProperty =
            BindableProperty.Create(nameof(WebViewRequestHeader), typeof(Dictionary<string,string>), typeof(HybridWebView), null);

        public static readonly BindableProperty WebViewInterceptionUrlProperty =
           BindableProperty.Create(nameof(WebViewInterceptionUrl), typeof(string), typeof(HybridWebView), default(string));

        public static readonly BindableProperty ConnectionFailureUrlProperty =
           BindableProperty.Create(nameof(ConnectionFailureUrl), typeof(string), typeof(HybridWebView), default(string));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public WebViewMessage Message
        {
            get => (WebViewMessage)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public string SourceUrl
        {
            get => (string)GetValue(SourceUrlProperty);
            set => SetValue(SourceUrlProperty, value);
        }

        public HtmlWebViewSource Source
        {
            get => (HtmlWebViewSource)GetValue(SourceProperty);
            set => SetValue(SourceUrlProperty, value);
        }

        public bool HasStatusBarOffset
        {
            get => (bool)GetValue(HasStatusBarOffsetProperty);
            set => SetValue(HasStatusBarOffsetProperty, value);
        }

        public bool IsWebViewReloadRequired
        {
            get => (bool)GetValue(IsWebViewReloadRequiredProperty);
            set => SetValue(IsWebViewReloadRequiredProperty, value);
        }

        public bool IsCacheEnabled
        {
            get => (bool)GetValue(IsCacheEnabledProperty);
            set => SetValue(IsCacheEnabledProperty, value);
        }

        public bool HasHeaderEnabled
        {
            get => (bool)GetValue(HasHeaderEnabledProperty);
            set => SetValue(HasHeaderEnabledProperty, value);
        }

        public Dictionary<string, string> WebViewRequestHeader
        {
            get => (Dictionary<string, string>)GetValue(WebViewRequestHeaderProperty);
            set => SetValue(WebViewRequestHeaderProperty, value);
        }

        public string WebViewInterceptionUrl
        {
            get => (string)GetValue(WebViewInterceptionUrlProperty);
            set => SetValue(WebViewInterceptionUrlProperty, value);
        }

        public string ConnectionFailureUrl
        {
            get => (string)GetValue(ConnectionFailureUrlProperty);
            set => SetValue(ConnectionFailureUrlProperty, value);
        }

        private int _statusBarOffset;
        public int StatusBarOffset
        {
            get => _statusBarOffset;
            set
            {
                _statusBarOffset = value;
                if (HasStatusBarOffset)
                    Margin = new Thickness(0, -value, 0, 0);
            }
        }

        public event EventHandler WebViewReloadEvent;
        public event EventHandler<object> MessageReceivedFromJavascript; // not used atm, but if we need(TBD)
        
        static void WebViewReloadPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (HybridWebView)bindable;
            // Property changed implementation goes here
            if (control != null && control.IsWebViewReloadRequired)
                control.WebViewReloadEvent?.Invoke(control, null);
        }

        public void SendMessageReceivedFromJavascript(object data)
        {
            MessageReceivedFromJavascript?.Invoke(this, data);
        }
    }
}