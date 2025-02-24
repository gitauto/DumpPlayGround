using Microsoft.Web.WebView2.WinForms;
using System.IO;
using System.Windows;

namespace WpfApp1.Extensions;

public static class WebView2Extensions
{
    public static async Task<bool> SaveHTMLSourceToFileAsync(this WebView2 browser, string filePath)
    {
        try
        {
            // Esegue uno script JavaScript per ottenere il contenuto HTML della pagina
            string? html = await browser.CoreWebView2.ExecuteScriptAsync("document.documentElement.outerHTML;");
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
