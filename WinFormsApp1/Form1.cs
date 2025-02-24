using DumpLibrary;
using System.Globalization;
using WinFormsApp1.Extensions;
using WinFormsApp1.Helpers;
using WinFormsApp1.Test;

namespace WinFormsApp1;

public partial class Form1 : Form
{
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
        await webView21.EnsureCoreWebView2Async(null);

        // Imposta manualmente la posizione e le dimensioni del WebView2
        webView21.Location = new System.Drawing.Point(0, menuStrip1.Height);
        webView21.Size = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - menuStrip1.Height);

        webView21.CoreWebView2?.NavigateToString(DumpExtensions.GetHtmlPageTemplate());
    }

    public async void AppendRawHTML(string html)
    {
        var script = $@"appendRawHTML(`{html}`)";
        await webView21.CoreWebView2.ExecuteScriptAsync(script);
    }

    private void TestToolStripMenuItem_Click(object sender, EventArgs e)
    {
        DumpTestStuff.DumpTest();
    }

    private void ClearPageToolStripMenuItem_Click(object sender, EventArgs e)
    {
        webView21.CoreWebView2?.NavigateToString(DumpExtensions.GetHtmlPageTemplate("Dump Playground", _isoCode, Utils.IsDarkModeActive()));
    }

    private async void ExportHTMLToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) { return; }

            await webView21.SaveHTMLSourceToFileAsync(saveFileDialog1.FileName);
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
        webView21?.CoreWebView2?.NavigateToString(DumpExtensions.GetHtmlPageTemplate("Dump Playground", _isoCode, true));
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
