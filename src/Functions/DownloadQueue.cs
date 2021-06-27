using System;
using System.Collections.Generic;
using System.Timers;

public static class DownloadQueue
{
    public static PriorityQueue<DownloadClient, int> Queue = new PriorityQueue<DownloadClient, int>();
    public static List<DownloadClient> Running = new List<DownloadClient>();

    private static Timer checkTimer;

    public static void Start()
    {
        if (checkTimer == null)
        {
            checkTimer = new Timer(1000);
            checkTimer.Elapsed += Timer_Elapsed;
        }
        checkTimer.Start();
    }

    private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        if (Running.Count > 0)
        {
            for (var i = Running.Count - 1; i >= 0; i--)
            {
                var job = Running[i];
                if (job.Completed)
                {
                    Running.RemoveAt(i);
                    job.Dispose();
                }
            }
        }
        while (Running.Count < Config.ConcurrentDownload)
        {
            var job = Queue.Dequeue();
            Running.Add(job);
            _ = job.DownloadAsync();
        }
    }

    public static void Add(DownloadClient client, int priority = 2)
    {
        Queue.Enqueue(client, priority);
        Start();
    }

    public static void Add(string url, int priority = 2)
    {
        Add(new DownloadClient(url, priority));
    }
}