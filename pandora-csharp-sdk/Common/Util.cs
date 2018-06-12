using System;

namespace Qiniu.Pandora.Common
{
    public class Util
    {
        /// <summary>
        /// GetUnixTimestampInSeconds get the unix timestamp in seconds
        /// </summary>
        /// <returns></returns>
        public static long GetUnixTimestampInSeconds()
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // current zone
            long timeStamp = (long)(DateTime.Now - startTime).TotalMilliseconds / 1000; // timestamp in seconds
            return timeStamp;
        }

        /// <summary>
        /// JsonEncode encode the oject to string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string JsonEncode(object data)
        {
            return new JsonFx.Json.JsonWriter().Write(data);
        }

        /// <summary>
        /// CreateHttpDateTime create time string like "Sat, 16 Aug 2008 10:38:39 GMT"
        /// </summary>
        /// <returns></returns>
        public static string CreateHttpDateTime(DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString("r");
        }

        public static string CreateRFC3339DateTime(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd'T'HH:mm:ssZ");
        }

    }
}
