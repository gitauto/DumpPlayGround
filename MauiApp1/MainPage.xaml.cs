using DumpLibrary;
using MauiApp1.Helpers;
using MauiApp1.Test;
using System.Globalization;

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
        webView.Source = htmlSource;
    }

    public async void AppendRawHTML(string html)
    {
        var script = $@"appendRawHTML(`{html}`)";        
        await webView.EvaluateJavaScriptAsync(script.Replace("\r\n", ""));
    }

    private void OnDumpTest_Clicked(object sender, EventArgs e)
    {
        DumpTestStuff.DumpTest();
    }

    private void OnDumpPage_Clicked(object sender, EventArgs e)
    {
        var hwp = new HelloWorldPage();
        hwp.Dump();
    }
}

