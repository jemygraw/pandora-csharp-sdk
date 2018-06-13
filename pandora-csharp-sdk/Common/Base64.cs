using System;
using System.Text;

namespace Qiniu.Pandora.Common
{
    public class Base64
    {
        public static string UrlSafeBase64Encode(string text)
        {
            return UrlSafeBase64Encode(Encoding.UTF8.GetBytes(text));
        }

        public static string UrlSafeBase64Encode(byte[] data)
        {
            return Convert.ToBase64String(data).Replace('+', '-').Replace('/', '_');
        }

        public static string UrlSafeBase64Encode(string bucket, string key)
        {
            return UrlSafeBase64Encode(bucket + ":" + key);
        }

        public static byte[] UrlsafeBase64Decode(string text)
        {
            return Convert.FromBase64String(text.Replace('-', '+').Replace('_', '/'));
        }
    }
}