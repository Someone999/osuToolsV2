using System.Text;

namespace osuToolsV2.Replays;

public static class Md5Tools
{
    public static string InvalidMd5 { get; } = new StringBuilder().Append(' ', 32).ToString();
    public static string ToMd5String(this byte[] bytes)
    {
        if (bytes.Length == 32)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.AppendFormat("{0:x2}", b);
            }
        }
        return InvalidMd5;
    }
}