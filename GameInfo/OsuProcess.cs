using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osuToolsV2.Exceptions;
using osuToolsV2.GameInfo.Config;

namespace osuToolsV2.GameInfo
{
    public class OsuProcess
    {
        readonly Process _osuProcess;

        public OsuProcess(Process process)
        {
            var mainModule = process.MainModule;
            if (mainModule is not { ModuleName: "osu!.exe" })
            {
                throw new ArgumentException("Wrong process", nameof(process));
            }

            _osuProcess = process;
        }

        public static OsuProcess? FindOsuProcess()
        {
            Process[] processes = Process.GetProcessesByName("osu!");
            return processes.Length > 0 ? new OsuProcess(processes[0]) : null;
        }

        public static OsuProcess WaitForOsuProcess(int detectInterval = 500, int timeoutMilliseconds = -1)
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
                    throw new ProcessFindTimeoutException("寻找进程超时");
                }
            }

            return process;
        }

        public static async Task<OsuProcess> WaitForOsuProcessAsync(int detectInterval = 500, int timeoutMilliseconds = -1)
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
                    throw new ProcessFindTimeoutException("寻找进程超时");
                }
            }

            return process;
        }

        public void WaitGameExit() => _osuProcess.WaitForExit();
        public async Task  WaitGameExitAsync() => await _osuProcess.WaitForExitAsync();
        public bool IsRunning => !_osuProcess.HasExited;
        public event EventHandler OnExited
        {
            add => _osuProcess.Exited += value;
            remove => _osuProcess.Exited -= value;
        }

        public string GamePath
        {
            get
            {
                var mainModule = _osuProcess.MainModule?.FileName;
                return mainModule ?? throw new NotOsuProcessException("The specified process maybe not an Osu process");
            }
        }

        public string GameConfigFilePath
        {
            get
            {
                var gameDirectory = Path.GetDirectoryName(GamePath);
                if (string.IsNullOrEmpty(gameDirectory))
                {
                    throw new NotOsuProcessException("The specified process maybe not an Osu process");
                }

                var configPath = Path.Combine(gameDirectory, $"osu!.{Environment.UserName}.cfg");
                if (!File.Exists(configPath))
                {
                    throw new NotOsuProcessException("The specified process maybe not an Osu process");
                }

                return configPath;
            }
        }

        public OsuConfig LoadGameConfigFromFile()
        {
            return new OsuConfig(GameConfigFilePath);
        }
    }
}
