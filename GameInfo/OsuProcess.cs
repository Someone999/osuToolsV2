using System.Diagnostics;
using HsManCommonLibrary.Exceptions;
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

        public void WaitGameExit() => _osuProcess.WaitForExit();
        public async Task WaitGameExitAsync() => await Task.Run(() => _osuProcess.WaitForExit());
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

        public string GameDirectory
        {
            get
            {
                var dir = Path.GetDirectoryName(GamePath);
                if (string.IsNullOrEmpty(dir))
                {
                    throw new HsManException("Unable to get game directory");
                }

                return dir;
            }
        }
    }
}
