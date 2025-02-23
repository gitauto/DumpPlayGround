using DumpLibrary;
using Microsoft.Web.WebView2.WinForms;
using System.Globalization;
using WinFormsApp1.Extensions;
using WinFormsApp1.Helpers;
using WinFormsApp1.Test;

namespace WinFormsApp1;

public partial class Form1 : Form
{
    public WebView2? _webView;
    private readonly SynchronizationContext? _syncContext;
    private readonly string _isoCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

    public Form1()
    {
        InitializeComponent();

        this.Text = $"Winforms - Dump Playground ver. {Utils.GetAppVersion()}";
        _syncContext = SynchronizationContext.Current;
        DumpExtensions.AppendRawHTML = AppendRawHTML;

        InitializeWebView();
    }

    private async void InitializeWebView()
    {
        _webView = new WebView2 { Dock = DockStyle.Fill };
        Controls.Add(_webView);

        await _webView.EnsureCoreWebView2Async(null);
        _webView.CoreWebView2?.NavigateToString(DumpExtensions.GetHtmlPageTemplate());
    }

    public void AppendRawHTML(string html)
    {
        var script = $@"appendRawHTML(`{html}`)";

        _syncContext?.Post(async _ =>
        {
            if (_webView?.CoreWebView2 is null) { return; }
            await _webView.CoreWebView2.ExecuteScriptAsync(script);

        }, null);
    }

    private void TestToolStripMenuItem_Click(object sender, EventArgs e)
    {
        DumpTestStuff.DumpTest();
    }

    private void ClearPageToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _webView?.CoreWebView2?.NavigateToString(DumpExtensions.GetHtmlPageTemplate("Dump Playground", _isoCode, Utils.IsDarkModeActive()));
    }

    private void ExportHTMLToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) { return; }

            _webView?.SaveHTMLSourceToFileAsync(saveFileDialog1.FileName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void ClearPageLightModeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _webView?.CoreWebView2?.NavigateToString(DumpExtensions.GetHtmlPageTemplate("Dump Playground", _isoCode, true));
    }

    private void DumpAWindowsFormToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var f = new System.Windows.Forms.Form() { Text = "Windows Form", Width = 400, Height = 200 };
        var b = new Button() { Height = 50, Width = 100, Left = 20, Top = 20, Text = "Click Me" };
        b.Click += (s, e) => MessageBox.Show("Hello World!", "Info");
        f.Controls.Add(b);
        f.Dump();
    }
}
