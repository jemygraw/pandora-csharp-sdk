using Qiniu.Pandora.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Qiniu.Pandora.Common
{
    public class Auth
    {
        private string accessKey;
        private string secretKey;

        public Auth(string accessKey, string secretKey)
        {
            this.accessKey = accessKey;
            this.secretKey = secretKey;
        }
        /// <summary>
        /// SignRequest signs the request in the algorithm of Pandora
        /// see doc here at: https://qiniu.github.io/pandora-docs/#/ak?id=token%E7%AD%BE%E5%90%8D%E5%8C%85%E5%90%AB%E7%9A%84%E5%86%85%E5%AE%B9
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="headers">
        /// method - request method
        /// resource - request path
        /// expires - deadline in unix timestamp in seconds
        /// contentType - content type
        /// contentMD5 - content md5, optional
        /// headers - sorted headers whose keys start with X-Qiniu-
        /// </param>
        /// <param name="qiniuSubResource"></param>
        /// <returns></returns>
        public string SignRequest(string url, string method, Dictionary<string, string> headers)
        {
            Dictionary<string, object> tokenDesc = new Dictionary<string, object>();
            //filter the qiniu heades
            List<string> qiniuHeaderKeys = new List<string>();
            foreach (string key in headers.Keys)
            {
                if (key.StartsWith("X-Qiniu-"))
                {
                    qiniuHeaderKeys.Add(key);
                }
            }
            qiniuHeaderKeys.Sort();
            string canHeadersStr = null;
            if (qiniuHeaderKeys.Count > 0)
            {
                List<string> canHeaders = new List<string>();
                foreach (string key in qiniuHeaderKeys)
                {
                    canHeaders.Add(String.Format("{0}:{1}", key.ToLower(), headers[key]));
                }
                canHeadersStr = String.Join("\n", canHeaders);
            }
            if (!String.IsNullOrEmpty(canHeadersStr))
            {
                tokenDesc["headers"] = canHeadersStr;
            }
            //check other headers
            if (headers.ContainsKey(HeaderKey.ContentType))
            {
                tokenDesc["contentType"] = headers[HeaderKey.ContentType];
            }
            if (headers.ContainsKey(HeaderKey.ContentMD5))
            {
                tokenDesc["contentMD5"] = headers[HeaderKey.ContentMD5];
            }
            Uri reqURI = new Uri(url);
            tokenDesc["resource"] = reqURI.AbsolutePath;
            tokenDesc["method"] = method;
            tokenDesc["expires"] = Util.GetUnixTimestampInSeconds() + 600;
            string tokenDescData = Util.JsonEncode(tokenDesc);
            return String.Format("Pandora {0}", this.SignWithData(tokenDescData));
        }

        private string encodedSign(byte[] data)
        {
            HMACSHA1 hmac = new HMACSHA1(Encoding.UTF8.GetBytes(this.secretKey));
            byte[] digest = hmac.ComputeHash(data);
            return Base64.UrlSafeBase64Encode(digest);
        }

        private string encodedSign(string str)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);
            return encodedSign(data);
        }

        /// <summary>
        /// 签名-字节数据
        /// </summary>
        /// <param name="data">待签名的数据</param>
        /// <returns></returns>
        public string Sign(byte[] data)
        {
            return string.Format("{0}:{1}", this.accessKey, encodedSign(data));
        }

        /// <summary>
        /// 签名-字符串数据
        /// </summary>
        /// <param name="str">待签名的数据</param>
        /// <returns></returns>
        public string Sign(string str)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);
            return Sign(data);
        }

        /// <summary>
        /// 附带数据的签名
        /// </summary>
        /// <param name="data">待签名的数据</param>
        /// <returns></returns>
        public string SignWithData(byte[] data)
        {
            string encodedData = Base64.UrlSafeBase64Encode(data);
            return string.Format("{0}:{1}:{2}", this.accessKey, encodedSign(encodedData), encodedData);
        }

        /// <summary>
        /// 附带数据的签名
        /// </summary>
        /// <param name="str">待签名的数据</param>
        /// <returns>签名结果</returns>
        public string SignWithData(string str)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);
            return SignWithData(data);
        }
    }
}
