namespace App
{
    internal sealed class PerfviewProfiler
    {
        private string _profilesPath = string.Empty;
        private string _logFilePath = string.Empty;
        private string _traceDataFilePath = string.Empty;
        private string _executablePath = string.Empty;

        private string _providers = "";
        internal PerfviewProfiler(string providers)
        {
            _providers = providers;
            _executablePath = Path.GetFullPath("../tools/Perfview.exe");
        }
        internal async ValueTask StartProfilingAsync()
        {
            var perfviewExist = File.Exists(_executablePath);
            if (!perfviewExist)
            {
                throw new ArgumentNullException($"Perfview.exe not found at {_executablePath}");
            }

            Console.WriteLine($"{_executablePath} exist:{perfviewExist}");

            _profilesPath = Path.GetDirectoryName(_executablePath);
            Console.WriteLine($"Profile path: {_profilesPath}");

            var timestamp = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm_ss");
            _logFilePath = $@"{_profilesPath}\PerfViewLog_{timestamp}.txt";
            _traceDataFilePath = $@"{_profilesPath}\PerfviewData_{timestamp}.etl";

            Console.WriteLine($"Starting Perfview profiling...");

            string startArgs = $"start /AcceptEula {_traceDataFilePath}";

            using (var startProcess = new ProcessRunner(TimeSpan.FromSeconds(60)))
            {
                await startProcess.Run(_executablePath, startArgs);
                Console.WriteLine($"Perfview started. Elapsed: {startProcess.ElapsedTime.Seconds} seconds");
            }
        }

        internal async ValueTask StopProfilingAsync()
        {
            Console.WriteLine($"Stopping Perfview profiling...");

            string stopArgs = $"stop /Merge=true /Zip=false";

            using (var stopProcess = new ProcessRunner(TimeSpan.FromSeconds(120)))
            {
                await stopProcess.Run(_executablePath, stopArgs);
                Console.WriteLine($"Perfview stopped. Elapsed time: {stopProcess.ElapsedTime.Seconds} seconds. . Profile file:{_traceDataFilePath}");
            }
        }
    }
}
