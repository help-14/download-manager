using System;
using System.IO;

public static class Config
{
    public static void Load()
    {
        DownloadPath = Environment.GetEnvironmentVariable("DownloadPath") ?? Directory.GetCurrentDirectory();

        var concurrent = Environment.GetEnvironmentVariable("ConcurrentDownload");
        ConcurrentDownload = string.IsNullOrEmpty(concurrent) ? 2 : Convert.ToInt32(concurrent);
    }

    public static string DownloadPath { get; set; }
    public static int ConcurrentDownload { get; set; }
}