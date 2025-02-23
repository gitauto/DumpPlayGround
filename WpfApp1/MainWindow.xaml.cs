using System.Windows;

namespace WpfApp1;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        InitializeWebView();
    }

    private async void InitializeWebView()
    {
        await webView.EnsureCoreWebView2Async(null);

        webView.Source = new System.Uri("https://www.google.com");
        //webView.CoreWebView2?.NavigateToString(DumpExtensions.GetHtmlPageTemplate());
    }
}