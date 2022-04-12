using System.Security.Cryptography;
using System.Text;

namespace osuToolsV2.Tools
{
    public class MD5String
    {
        public static string GetMd5String(byte[] bts)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var bt in bts)
            {
                sb.Append($"{bt:x2}");
            }
            return sb.ToString();
        }

        public string CalcMd5(byte[] bts)
        {
            var md5Calc = MD5.Create();
            var rslt = md5Calc.ComputeHash(bts);
            return GetMd5String(bts);
        }
    }
}
