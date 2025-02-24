using System.Reflection;

namespace MauiApp1.Helpers;

public static class Utils
{
    public static string GetAppVersion()
    {
        Version? version = Assembly.GetExecutingAssembly().GetName().Version;
        return $"ver. {version?.Major}.{version?.Minor}.{version?.Build}.{version?.Revision}";
    }
}
