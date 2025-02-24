using DumpLibrary;
using MauiApp1.Helpers;
using System.Globalization;
using System.Net.Http;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    private const string _HTMLPageTitle = "Dump Playground";
    private readonly string _isoCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

    public MainPage()
    {
        InitializeComponent();

        this.Title = $".NET MAUI - {_HTMLPageTitle} ver. {Utils.GetAppVersion()}";
        DumpExtensions.AppendRawHTML = AppendRawHTML;

        var htmlSource = new HtmlWebViewSource { Html = DumpExtensions.GetHtmlPageTemplate("Dump Playground", _isoCode, false) };

        // Assegna la sorgente al WebView
        webView.Source = htmlSource;
    }

    public async void AppendRawHTML(string html)
    {
        var script = $@"appendRawHTML(`{html}`)";
        await webView.EvaluateJavaScriptAsync(script);
    }
}

