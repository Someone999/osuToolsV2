using System.Diagnostics;
using System.Text;
using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Game.Legacy;
using osuToolsV2.Game.Mods;
using osuToolsV2.GameInfo;
using osuToolsV2.Writer;

namespace osuToolsV2
{
    /// <summary>
    ///     扩展方法
    /// </summary>
    public static class ExtraMethod
    {
        static string RemoveComment(string orignal)
        {
            if (orignal.Contains("//"))
            {
                int index = 0;
                for (int i = 0; i < orignal.Length; i++)
                {
                    if (orignal[i] == '/' && orignal[i + 1] == '/')
                        index = i;
                }

                return orignal.Remove(index).Trim();
            }

            return orignal;
        }
        /// <summary>
        ///     将Mod数组转换成ModList
        /// </summary>
        /// <param name="modarr"></param>
        /// <returns></returns>
        public static ModList ToModList(this Mod[] modarr)
        {
            return ModList.FromModArray(modarr);
        }
        /// <summary>
        /// 将字符串转换成<seealso cref="Nullable{DateTime}"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime? ToNullableDateTime(this string s)
        {
            return DateTime.TryParse(RemoveComment(s), out var d) ? (DateTime?) d : null;
        }
        /// <summary>
        /// 将字符串转换成DateTime
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s)
        {
            return DateTime.Parse(RemoveComment(s));
        }

        /// <summary>
        /// 将整数化成bool，0为false，非0为true
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool ToBool(this int i)
        {
            return Convert.ToBoolean(i);
        }
        /// <summary>
        /// 将字符串转换为bool，不分大小写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ToBool(this string s)
        {
            s = RemoveComment(s);
            return Convert.ToBoolean(s == "1" || string.Equals(s, "True", StringComparison.OrdinalIgnoreCase)
                ? "True"
                : "False");
        }
        /// <summary>
        /// 将字符串转换成<see cref="Nullable{Boolean}"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool? ToNullableBool(this string s)
        {
            s = RemoveComment(s);
            return string.IsNullOrEmpty(s) ? null : (bool?) Convert.ToBoolean(s == "1" ? "True" : "False");
        }
        /// <summary>
        /// 将字符串转换成int
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt32(this string s)
        {
            s = RemoveComment(s);
            return int.Parse(s);
        }
        /// <summary>
        ///将字符串转换成<seealso cref="Nullable{Int32}"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int? ToNullableInt32(this string s)
        {
            s = RemoveComment(s);
            return string.IsNullOrEmpty(s) ? null : (int?) int.Parse(s);
        }

        /// <summary>
        ///将字符串转换成uint
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static uint ToUInt32(this string s)
        {
            s = RemoveComment(s);
            return uint.Parse(s);
        }
        /// <summary>
        ///将字符串转换成<seealso cref="Nullable{UInt32}"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static uint? ToNullableUInt32(this string s)
        {
            s = RemoveComment(s);
            return string.IsNullOrEmpty(s) ? null : (uint?) uint.Parse(s);
        }
        /// <summary>
        ///将字符串转换成double
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double ToDouble(this string s)
        {
            s = RemoveComment(s);
            return double.Parse(s);
        }
        /// <summary>
        ///将字符串转换成<seealso cref="Nullable{Double}"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double? ToNullableDouble(this string s)
        {
            s = RemoveComment(s);
            return string.IsNullOrEmpty(s) ? null : (double?) double.Parse(s);
        }
        /*
        /// <summary>
        /// 将使用任意分隔符隔开的3个数字转换成<seealso cref="RgbColor"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static RgbColor ToRgbColor(this string s)
        {
            s = RemoveComment(s);
            return RgbColor.Parse(s);
        }
        /// <summary>
        /// 将使用任意分隔符隔开的4个数字转换成<seealso cref="RgbColor"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static RgbaColor ToRgbaColor(this string s)
        {
            s = RemoveComment(s);
            return RgbaColor.Parse(s);
        }*/
        /// <summary>
        /// 判断字符是不是数字
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsDigit(this char c)
        {
            return char.IsDigit(c);
        }

        internal static Keys CheckIndexAndGetValue(this Dictionary<string, Keys> var, string index)
        {
            try
            {
                var tmp =
                    index == "LeftShift" ? "LShiftKey" :
                    index == "RightShift" ? "RShiftKey" :
                    index == "LeftControl" ? "LControlKey" :
                    index == "RightControl" ? "RControlKey" :
                    index == "LeftAlt" ? "LMenu" :
                    index == "RightAlt" ? "RMenu" : index;
                return var[tmp];
            }
            catch (KeyNotFoundException)
            {
                Debug.WriteLine($"找不到键位:{index}");
                return (Keys) (-1);
            }
        }

        internal static LegacyGameMod CheckIndexAndGetValue(this Dictionary<string, LegacyGameMod> var, string index)
        {
            try
            {
                if (index == "Auto") index = "AutoPlay";
                if (index == "Autopilot") index = "AutoPilot";
                if (index.StartsWith("key")) return var[index.Replace("key", "")];
                return var[index];
            }
            catch (KeyNotFoundException)
            {
                Debug.WriteLine($"不支持的Mod:{index}");
                return LegacyGameMod.Unknown;
            }
        }

        internal static Keys CheckIndexAndGetValue(this Dictionary<LegacyGameMod, Keys> var, LegacyGameMod index)
        {
            try
            {
                var tmp = index;
                if (index.ToString().Contains("Key")) tmp = LegacyGameMod.Relax;
                if (index == LegacyGameMod.NightCore) tmp = LegacyGameMod.DoubleTime;
                if (index == LegacyGameMod.Perfect) tmp = LegacyGameMod.SuddenDeath;
                if (index == LegacyGameMod.FadeIn) tmp = LegacyGameMod.Hidden;
                if (index == LegacyGameMod.Random) tmp = LegacyGameMod.AutoPilot;
                return var[tmp];
            }
            catch (KeyNotFoundException)
            {
                Debug.WriteLine($"不支持的Mod:{index}");
                return (Keys) (-1);
            }
        }
        /// <summary>
        /// 将字符串转换成字节数组，可以指定编码器，默认为UTF8
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str, Encoding? encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetBytes(str);
        }
        /// <summary>
        /// 将字节数组转换成字符串，可以指定编码器，默认为UTF8
        /// </summary>
        /// <param name="b"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetString(this byte[] b, Encoding? encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetString(b);
        }
        
        
        public static void WriteKeyValuePairIfNotNull<T>(this IObjectWriter<T> objectWriter,string? key, object? val)
        {
            if (key == null || val == null)
            {
                return;
            }
            objectWriter.WriteKeyValuePair(key, val);
        }

       public static void WriteKeyValuePair<T>(this IObjectWriter<T> objectWriter, string key, object val)
        {
            objectWriter.Write($"{key}: {val}");
        }
    }
}