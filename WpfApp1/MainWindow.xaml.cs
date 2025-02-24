using DumpLibrary;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Helpers;
using WpfApp1.Test;

namespace WpfApp1;

public partial class MainWindow : Window
{
    private const string _HTMLPageTitle = "Dump Playground";
    private readonly string _isoCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;

        this.Title= $"WPF - {_HTMLPageTitle} ver. {Utils.GetAppVersion()}";
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
        try
        {
            //if (saveFileDialog1.ShowDialog() != DialogResult.OK) { return; }

            //await webView21.SaveHTMLSourceToFileAsync(saveFileDialog1.FileName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK,MessageBoxImage.Error);
        }
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
        var f = new Window() { Title = "WPF Window", Width = 400, Height = 200 };
        var b = new Button() { Height = 50, Width = 100, Content = "Click Me" };
        b.Click += (s, e) => MessageBox.Show("Hello World!", "Info");
        f.Content = b;
        f.Show();
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