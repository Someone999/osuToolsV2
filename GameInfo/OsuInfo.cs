using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using osuToolsV2.Utils;

namespace osuToolsV2.GameInfo
{
    /// <summary>
    /// 包含游戏的基础信息与配置文件信息
    /// </summary>
    public class OsuInfo
    {
        [DllImport("kernel32")]
        private static extern bool IsWow64Process(IntPtr hProcess, ref bool wow64Process);

        private readonly Dictionary<string, string> _dataDictionary = new Dictionary<string, string>();
        /// <summary>
        /// 当前的osu!进程
        /// </summary>
        public Process? CurrentProcess { get; private set; }
        Process? FindOsuProcess()
        {
            var processes = Process.GetProcessesByName("osu!");
            foreach (var process in processes)
            {
                try
                {
                    if (process.HasExited)
                    {
                        continue;
                    }
                }
                catch (Exception)
                {
                    continue;
                    // An invalid process.
                }
                
                
                bool isWow64 = true;
                if (Environment.Is64BitOperatingSystem)
                {
                    isWow64 = ProcessUtil.IsWow64Process(process);
                }
                
                if (process.ProcessName == "osu!" && isWow64)
                {
                    CurrentProcess = process;
                }
            }
            return CurrentProcess;
        }
        /// <summary>
        /// 当前osu!的配置文件的路径
        /// </summary>
        public string? ConfigFilePath { get; private set; }
        /// <summary>
        /// osu!主程序所在目录
        /// </summary>
        public string OsuDirectory
        {
            get
            {
                string? cfg = ConfigFilePath;
                string cfgFile = Path.GetFileName(cfg ?? throw new InvalidOperationException());
                return cfg.Replace(cfgFile, "");
            }
        }
        /// <summary>
        /// 谱面目录
        /// </summary>
        public string BeatmapDirectory => Path.Combine(OsuDirectory, _dataDictionary["BeatmapDirectory"]);
        /// <summary>
        /// 当前登录用户的用户名
        /// </summary>
        public string LogonUserName => _dataDictionary["Username"];

        /// <summary>
        /// 当前版本
        /// </summary>
        public string Version => _dataDictionary["LastVersion"];
        /// <summary>
        /// 最早的可登录版本
        /// </summary>
        public string LastPermissionFailsVersion => _dataDictionary["LastVersionPermissionsFailed"];
        Dictionary<string,string> ReadIniFile(string? file)
        {
            if (string.IsNullOrEmpty(file))
            {
                throw new InvalidOperationException();
            }
            if (!File.Exists(file))
            {
                return new Dictionary<string, string>();
            }
            Dictionary<string, string> tmpDictionary = new Dictionary<string, string>();
            string[] lines = File.ReadAllLines(file);
            int commentLines = 0;
            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("#"))
                {
                    tmpDictionary.Add($"#Comment{commentLines}", line);
                    commentLines++;
                    continue;
                }

                int index = line.IndexOf('=');
                if(index == -1)
                    continue;
                string[] pair =
                {
                    line.Substring(0,index),
                    line.Substring(index + 1)
                };
                if(pair.Length < 2)
                    continue;
                string propertyName = pair[0].Trim(),propertyValue = pair[1].Trim();
                tmpDictionary.Add(propertyName,propertyValue);
            }
            return tmpDictionary;
        }
        void SaveAsIni(string file = "OsuInfo.ini")
        {
            if (!string.IsNullOrEmpty(ConfigFilePath))
            {
                File.WriteAllText(file,$"ConfigFilePath = {ConfigFilePath}");
            }
        } 
        void ReadFromFile(string file = "OsuInfo.ini")
        {
            if (!File.Exists(file))
            {
                return;
            }
            var tmp = ReadIniFile(file);
            if(tmp.TryGetValue("ConfigFilePath", out var value))
            {
                ConfigFilePath = value;
            }
        }
        /// <summary>
        /// 初始化新的OsuInfo对象
        /// </summary>
        private OsuInfo()
        {
            if (FindOsuProcess() != null)
            {
                string fullPath = CurrentProcess?.MainModule?.FileName?.Replace("osu!.exe", "") ?? throw new InvalidOperationException();
                ConfigFilePath = Path.Combine(fullPath ?? throw new InvalidOperationException(),
                    $"osu!.{Environment.UserName}.cfg");
                if (!File.Exists(ConfigFilePath))
                    throw new FileNotFoundException();
                SaveAsIni();
            }
            else
            {
                ReadFromFile();
            }
            var tmp = ReadIniFile(ConfigFilePath);
            foreach (var pair in tmp)
                _dataDictionary.Add(pair.Key, pair.Value);
        }
        /// <summary>
        /// 返回指定属性的值
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <returns>存在返回值，不存在返回null</returns>
        public string? GetPropertyValue(string propertyName)
        {
            if (propertyName.StartsWith("#"))
                throw new ArgumentException("请使用GetComments获取所有注释。", nameof(propertyName));
            if (_dataDictionary.ContainsKey(propertyName))
                return _dataDictionary[propertyName];
            return null;
        }
        /// <summary>
        /// 设置指定属性的值，如果指定的属性不存在则不会进行任何操作
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">要设置的值</param>
        public void SetProperty(string propertyName, string value)
        {
            if (propertyName.StartsWith("#"))
            {
                throw new ArgumentException("属性名无效", nameof(propertyName));
            }
            if (_dataDictionary.ContainsKey(propertyName))
            {
                _dataDictionary[propertyName] = value;
            }
        }
        /// <summary>
        /// 获取所有的注释
        /// </summary>
        /// <returns></returns>
        public string[] GetComments()
        {
            List<string> comments = new List<string>();
            foreach (var keyValuePair in _dataDictionary)
            {
                if(keyValuePair.Key.StartsWith("#Comment"))
                    comments.Add(keyValuePair.Value);
            }
            return comments.ToArray();
        }
        /// <summary>
        /// 将字典里的数据按照原文件中的顺序写入流
        /// </summary>
        /// <param name="stream">要写入的流</param>
        public void Save(Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream), "Stream不能为null");
            }
            StringBuilder builder = new StringBuilder();
            foreach (var data in _dataDictionary)
            {
                builder.AppendLine(data.Key.StartsWith("#Comment") ? data.Value : $"{data.Key} = {data.Value}");
            }
            byte[] dataBytes = builder.ToString().ToBytes();
            stream.Write(dataBytes,0,dataBytes.Length);
        }

        private static readonly object StaticLocker = new object();
        private static volatile OsuInfo? _ins;

        public static OsuInfo GetInstance()
        {
            if (_ins != null)
            {
                return _ins;
            }

            lock (StaticLocker)
            {
                if (_ins != null)
                {
                    return _ins;
                }

                _ins = new OsuInfo();
            }

            return _ins;
        }
    }
}
