﻿using DumpLibrary;
using Microsoft.Win32;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Helpers;
using WpfApp1.Extensions;
using TestDump;

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

    private async void ExportHTML_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // Crea un'istanza di SaveFileDialog
            var saveFileDialog = new SaveFileDialog
            {
                // Configura le proprietà del dialogo
                Title = "Save the file",
                Filter = "HTML(*.html)|*.html|All files (*.*)|*.*",                                  // Filtri per i tipi di file
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), // Cartella iniziale
                FileName = ""                                                                        // Nome file predefinito
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                Debug.WriteLine($"{webView.GetType()}");

                await webView.SaveHTMLSourceToFileAsync(saveFileDialog.FileName);
            }
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
        await webView.CoreWebView2.ExecuteScriptAsync(script.Replace("\r\n", ""));
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
        f.Dump();

        //f.Show();

        //f.GetType().Dump();
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