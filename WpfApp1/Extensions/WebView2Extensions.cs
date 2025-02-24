using System.IO;
using System.Windows;

namespace WpfApp1.Extensions;

public static class WebView2Extensions
{
    public static async Task<bool> SaveHTMLSourceToFileAsync(this Microsoft.Web.WebView2.Wpf.WebView2 webView, string filePath)
    {
        try
        {
            // Esegue uno script JavaScript per ottenere il contenuto HTML della pagina
            string? html = await webView.CoreWebView2.ExecuteScriptAsync("document.documentElement.outerHTML;");
            html = System.Text.Json.JsonSerializer.Deserialize<string>(html);
            await File.WriteAllTextAsync(filePath, html);
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        return false;
    }
}
