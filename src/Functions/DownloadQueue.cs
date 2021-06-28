using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

public static class DownloadQueue
{
    public static List<DownloadClient> Queue = new List<DownloadClient>();

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
        checkTimer.Stop();
        if (Queue.Count > 0)
        {
            for (var i = Queue.Count - 1; i >= 0; i--)
            {
                var job = Queue[i];
                if (job.Completed)
                {
                    Queue.RemoveAt(i);
                    job.Dispose();
                }
            }
        }
        if (Queue.Count > 0)
        {
            if (Queue.Count(x => x.Running) < Config.ConcurrentDownload)
                Queue.FirstOrDefault(x => !x.Running && !x.Completed)?.DownloadAsync();
            checkTimer.Start();
        }
    }

    public static void Add(DownloadClient client)
    {
        Queue.Add(client);
        Start();
    }

    public static void Add(string url)
    {
        Add(new DownloadClient(url));
    }

}