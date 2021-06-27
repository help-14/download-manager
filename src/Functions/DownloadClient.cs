using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Threading.Tasks;

public class DownloadClient : IDisposable 
{
    HttpClient client;
    public string DownloadUrl { get; set; }
    public string FileName { get; set; }
    public string Extension { get; set; }
    public int Progress { get; set; }
    public bool Completed { get; set; }

    public DownloadClient(string url)
    {
        var progressMessageHandler = new ProgressMessageHandler(new HttpClientHandler());
        progressMessageHandler.HttpReceiveProgress += UpdateProgress;

        client = new HttpClient(progressMessageHandler);
        
        DownloadUrl = url;
        FileName = Guid.NewGuid().ToString();

        var urlParts = url.Split('/');
        if (urlParts.Length > 0 && urlParts[urlParts.Length - 1].Contains("."))
        {
            FileName = urlParts[urlParts.Length - 1];
            var extParts = FileName.Split('.');
            Extension = extParts[extParts.Length - 1];
        }
    }

    public async Task DownloadAsync()
    {
        try
        {
            var fileToWriteTo = Path.Combine(Config.DownloadPath, FileName);

            using (var filestream = new FileStream(fileToWriteTo, FileMode.OpenOrCreate))
            {
                var netstream = await client.GetStreamAsync(DownloadUrl);
                await netstream.CopyToAsync(filestream);
            }
            Completed = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void UpdateProgress(object sender, HttpProgressEventArgs e)
    {
        Progress = e.ProgressPercentage;
    }

    public void Dispose()
    {
        client?.CancelPendingRequests();
        client?.Dispose();
        client = null;
    }

}
