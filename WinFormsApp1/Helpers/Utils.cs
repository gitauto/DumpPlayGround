using Microsoft.Win32;
using System.Reflection;

namespace WinFormsApp1.Helpers;

public static class Utils
{
    public static bool IsDarkModeActive()
    {
        using RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
        if (key != null)
        {
            object? appsUseLightTheme = key.GetValue("AppsUseLightTheme");
            return appsUseLightTheme != null && Convert.ToInt32(appsUseLightTheme) == 0;
        }
        return false; // Default to Light Mode if the registry cannot be read
    }
    public static string GetAppVersion()
    {
        Version? version = Assembly.GetExecutingAssembly().GetName().Version;
        return $"ver. {version?.Major}.{version?.Minor}.{version?.Build}.{version?.Revision}";
    }
}
