using App;

namespace src;

class Program
{
    static async Task Main(string[] args)
    {
        var waitTimeInSeconds = 600;
        var envWaitTime = Environment.GetEnvironmentVariable("WAIT_TIME");
        if (!string.IsNullOrEmpty(envWaitTime) && int.TryParse(envWaitTime, out var envValue))
        {
            waitTimeInSeconds = envValue;
        }

        Console.WriteLine("About to collect profile");

        //await CollectProfile();

        for (var i = 0; i < waitTimeInSeconds; i++)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] Program will exit in {(waitTimeInSeconds - i)} seconds.");
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }

    //internal static async Task CollectProfile()
    //{
    //    var providers = "Microsoft-Windows-DotNETRuntime";
    //    var profiler = new PerfviewProfiler(providers);

    //    //await profiler.StartProfilingAsync();
    //    for (var i = 0; i < 30; i++)
    //    {
    //        Console.WriteLine($"Running iteration {i} at {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
    //        await Task.Delay(TimeSpan.FromSeconds(60));
    //    }

    //    //await profiler.StopProfilingAsync();
    //}
}