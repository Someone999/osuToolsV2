using HsManCommonLibrary.Exceptions;
using HsManCommonLibrary.Versioning;
using osuToolsV2.GameInfo.Config;

namespace osuToolsV2.GameInfo;

public class OsuGameInfoLoader
{
    private OsuGameInfoLoader()
    {
    }
    
    public OsuProcess? GameProcess { get; private set; }
    public OsuConfig? GameConfig { get; private set; }

    public static OsuGameInfoLoader? CreateInstance(string gameDirectory)
    {
        string cfgFileName = $"osu!.{Environment.UserName}.cfg";
        string cfgFilePath = Path.Combine(gameDirectory, cfgFileName);
        if (!Directory.Exists(gameDirectory))
        {
            return null;
        }

        OsuGameInfoLoader loader = new OsuGameInfoLoader
        {
            GameProcess = null,
        };
        
        if (File.Exists(cfgFilePath))
        {
            loader.GameConfig = new OsuConfig(cfgFilePath);
        }
        
        return loader;
    }

    public static OsuGameInfoLoader? CreateInstance(OsuProcess? process)
    {
        if (process == null)
        {
            return null;
        }
        
        var ins = CreateInstance(process.GameDirectory);
        if (ins == null)
        {
            return null;
        }

        ins.GameProcess = process;
        return ins;
    }

    private static int _lastProcessVer, _lastInsVer;
    private static ObjectIntegerVersionManager<OsuProcess>? _lastProcess;
    private static ObjectIntegerVersionManager<OsuGameInfoLoader>? _lastIns;
    private static readonly object Locker = new object();

    private static void InitVersionManagers()
    {
        if (_lastProcess == null)
        {
            _lastProcess = new ObjectIntegerVersionManager<OsuProcess>();
            _lastProcess.VersionChanged += (_, _) =>
            {
                _lastIns ??= new ObjectIntegerVersionManager<OsuGameInfoLoader>();
                _lastIns.Update(null);
            };
        }

        _lastIns ??= new ObjectIntegerVersionManager<OsuGameInfoLoader>();
    }

    private static void UpdateProcess(int processWaitTimeoutMilliseconds = -1)
    {
        _lastProcess ??= new ObjectIntegerVersionManager<OsuProcess>();
        var  currentProcess = OsuProcessFinder.WaitForOsuProcess(timeoutMilliseconds: processWaitTimeoutMilliseconds);
        _lastProcess.Update(currentProcess);
        _lastProcessVer = _lastProcess.Version;
    }

    private static void UpdateInstance(int processWaitTimeoutMilliseconds = -1)
    {
        if (_lastProcess?.CurrentObject is not {IsRunning: true})
        {
            UpdateProcess(processWaitTimeoutMilliseconds);
        }

        if (_lastProcess?.CurrentObject == null)
        {
            throw new HsManException("Unable to find process.");
        }

        _lastIns ??= new ObjectIntegerVersionManager<OsuGameInfoLoader>();
        
        var currentProcess = _lastProcess.CurrentObject;
        _lastIns.Update(CreateInstance(currentProcess));
        _lastInsVer = _lastIns.Version;
        
    }
    public static OsuGameInfoLoader? GetFromCurrentOsuProcess(int processWaitTimeoutMilliseconds = -1)
    {
        lock (Locker)
        {
            InitVersionManagers();
            if (_lastProcess == null || _lastIns == null)
            {
                return null;
            }


            var currentProcess = _lastProcess.CurrentObject;
            var currentIns = _lastIns.CurrentObject;
            if (currentProcess is not { IsRunning: true })
            {
                UpdateProcess(processWaitTimeoutMilliseconds);
                UpdateInstance(processWaitTimeoutMilliseconds);
                return _lastIns.CurrentObject;
            }

            if (currentIns == null)
            {
                UpdateInstance(processWaitTimeoutMilliseconds);
                return _lastIns.CurrentObject;
            }
           
            bool isSameVersion = _lastIns.Version == _lastInsVer && _lastProcess.Version == _lastProcessVer;
            bool isRunning = _lastProcess.CurrentObject is { IsRunning: true };
            if (isSameVersion && isRunning)
            {
                return _lastIns.CurrentObject;
            }

            return null;
        }
    }
}