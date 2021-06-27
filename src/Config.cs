using System;
using System.IO;

public static class Config
{
    public static void Load()
    {
        DownloadPath = Environment.GetEnvironmentVariable("DownloadPath") ?? Directory.GetCurrentDirectory();
    }

    public static string DownloadPath { get; set; }
}