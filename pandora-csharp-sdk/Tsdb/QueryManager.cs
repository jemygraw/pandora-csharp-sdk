using EasyHttp.Codecs;
using EasyHttp.Http;
using Qiniu.Pandora.Common;
using Qiniu.Pandora.Http;
using System;
using System.Collections.Generic;
using System.Web;
namespace Qiniu.Pandora.Tsdb
{
    public class QueryManager
    {
        public const string TimezoneUtc = "+00";
        public const string TimezoneShanghai = "+08";
        private Client client;

        public QueryManager(Auth auth)
        {
            this.client = new Client(auth);
        }

        public HttpResponse Query(string repoName, string timezone, string querySql)
        {
            if (timezone == null)
            {
                timezone = TimezoneShanghai;
            }
            string postUrl = string.Format("{0}/v4/repos/{1}/query?timezone={2}",
                Config.TsdbHost, repoName, Uri.EscapeDataString(timezone));
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("sql", querySql);
            return this.client.Post(postUrl, Util.JsonEncode(data), null,
                HttpContentTypes.ApplicationJson);
        }
    }
}
