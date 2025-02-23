namespace MauiApp1;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        webView.Source = new System.Uri("https://www.google.com");
    }
}

