using System.Collections.Generic;

public static class DownloadQueue
{
    public static PriorityQueue<DownloadClient, int> Queue = new PriorityQueue<DownloadClient, int>();

    public static void Add(DownloadClient client)
    {
        Queue.Enqueue(client, 2);
    }

    public static void Add(string url)
    {
        Add(new DownloadClient(url));
    }
}