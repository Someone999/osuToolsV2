using System.Diagnostics;

namespace osuToolsV2.GameInfo;

public static class OsuProcessFinder
{
    public static OsuProcess? FindOsuProcess()
    {
        Process[] processes = Process.GetProcessesByName("osu!");
        return processes.Length > 0 ? new OsuProcess(processes[0]) : null;
    }

    public static OsuProcess? WaitForOsuProcess(int detectInterval = 500, int timeoutMilliseconds = -1)
    {
        OsuProcess? process = null;
        int totalTime = 0;
        while (process == null)
        {
            process = FindOsuProcess();
            Thread.Sleep(detectInterval);
            totalTime += detectInterval;
            if (timeoutMilliseconds != -1 && totalTime > timeoutMilliseconds)
            {
                return null;
            }
        }

        return process;
    }

    public static async Task<OsuProcess?> WaitForOsuProcessAsync(int detectInterval = 500, int timeoutMilliseconds = -1)
    {
        OsuProcess? process = null;
        int totalTime = 0;
        while (process == null)
        {
            process = FindOsuProcess();
            await Task.Delay(detectInterval);
            totalTime += detectInterval;
            if (totalTime > timeoutMilliseconds)
            {
                return null;
            }
        }

        return process;
    }
}