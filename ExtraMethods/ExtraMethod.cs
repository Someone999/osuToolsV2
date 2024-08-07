using System.Security.Cryptography;
using System.Text;

namespace osuToolsV2
{
    /// <summary>
    ///   
    /// </summary>
    public static class ExtraMethod
    {
        /// <summary>
        /// Converts a byte array to its corresponding MD5 hash string representation.
        /// Each byte in the array is represented as a two-digit hexadecimal number.
        /// </summary>
        /// <param name="bytes">The byte array to convert to an MD5 hash string.</param>
        /// <returns>A hexadecimal string representing the MD5 hash of the byte array.</returns>
        /// <example>
        /// <code>
        /// byte[] md5Hash = MD5.HashData(inputData);
        /// string md5HashString = md5Hash.GetMd5String(); // Converts byte array to MD5 hash string
        /// </code>
        /// </example>
        public static string GetMd5String(this byte[] bytes)
       {
           StringBuilder builder = new StringBuilder();
           foreach (var b in bytes)
           {
               builder.Append(b.ToString("x2"));
           }

           return builder.ToString();
       }
       
#if !NETCOREAPP2_0_OR_GREATER
        public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                return false;
            }
            
            dictionary.Add(key, value);
            return true;
        }
        
        public static TValue? GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue? defVal)
        {
            return !dictionary.TryGetValue(key, out var value) ? defVal : value;
        }
        
        public static TValue? GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            return !dictionary.TryGetValue(key, out var value) ? default : value;
        }
#endif
    }
}