using DumpLibrary;
using System.Globalization;
using System.Windows;
using WpfApp1.Helpers;
using WpfApp1.Test;

namespace WpfApp1;

public partial class MainWindow : Window
{
    private readonly string _isoCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;

        DumpExtensions.AppendRawHTML = AppendRawHTML;

        InitializeWebView();
    }

    private async void InitializeWebView()
    {
        await webView.EnsureCoreWebView2Async(null);
        webView.CoreWebView2?.NavigateToString(DumpExtensions.GetHtmlPageTemplate("Dump Playground", _isoCode, Utils.IsDarkModeActive()));
    }

    private void ExportHTML_Click(object sender, RoutedEventArgs e)
    {
        // TODO:
    }

    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    public async void AppendRawHTML(string html)
    {
        var script = $@"appendRawHTML(`{html}`)";
        await webView.CoreWebView2.ExecuteScriptAsync(script);
    }

    private void RunDumpTest_Click(object sender, RoutedEventArgs e)
    {
        DumpTestStuff.DumpTest();
    }

    private void DumpWPFWindow_Click(object sender, RoutedEventArgs e)
    {

    }

    private void ClearPageLightMode_Click(object sender, RoutedEventArgs e)
    {
        webView?.CoreWebView2?.NavigateToString(DumpExtensions.GetHtmlPageTemplate("Dump Playground", _isoCode, false));
    }

    private void ClearPageDarkMode_Click(object sender, RoutedEventArgs e)
    {
        webView?.CoreWebView2?.NavigateToString(DumpExtensions.GetHtmlPageTemplate("Dump Playground", _isoCode, true));
    }
}