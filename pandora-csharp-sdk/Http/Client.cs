
using EasyHttp.Http;
using Qiniu.Pandora.Common;
using System.Collections.Generic;

namespace Qiniu.Pandora.Http
{
    public class Client
    {
        private Auth auth;
        public Client(Auth auth)
        {
            this.auth = auth;
        }

        public HttpResponse Get(string url, Dictionary<string, string> headers)
        {
            System.DateTime now = System.DateTime.Now;
            string date = Util.CreateHttpDateTime(now);
            if (headers == null)
            {
                headers = new Dictionary<string, string>();
            }
            headers.Add(HeaderKey.Date, date);
            string token = this.auth.SignRequest(url, "GET", headers);
            headers.Add(HeaderKey.Authorization, token);

            //fire request
            HttpClient httpClient = new HttpClient();
            //set date with proper header
            headers.Remove(HeaderKey.Date);
            httpClient.Request.Date = now;
            foreach (string key in headers.Keys)
            {
                httpClient.Request.AddExtraHeader(key, headers[key]);
            }
            return httpClient.Get(url);
        }

        public HttpResponse Post(string url, string body, Dictionary<string, string> headers, string contentType)
        {
            System.DateTime now = System.DateTime.Now;
            string date = Util.CreateHttpDateTime(now);
            if (headers == null)
            {
                headers = new Dictionary<string, string>();
            }
            headers.Add(HeaderKey.Date, date);
            headers.Add(HeaderKey.ContentType, contentType);
            string token = this.auth.SignRequest(url, "POST", headers);
            headers.Add(HeaderKey.Authorization, token);

            //fire request
            HttpClient httpClient = new HttpClient();
            //set date with proper header
            headers.Remove(HeaderKey.Date);
            headers.Remove(HeaderKey.ContentType);
            httpClient.Request.Date = now;
            httpClient.LoggingEnabled = true;

            foreach (string key in headers.Keys)
            {
                httpClient.Request.AddExtraHeader(key, headers[key]);
            }
            return httpClient.Post(url, body, contentType);
        }
    }
}
