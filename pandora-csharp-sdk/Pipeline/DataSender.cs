using EasyHttp.Http;
using Qiniu.Pandora.Common;
using Qiniu.Pandora.Http;

namespace Qiniu.Pandora.Pipeline
{
    public class DataSender
    {

        private Client client;
        public DataSender(Auth auth)
        {
            this.client = new Client(auth);
        }

        public HttpResponse Send(string repoName,Batch points)
        {
            string sendUrl = string.Format("{0}/v2/repos/{1}/data", Config.PipelineHost, repoName);
            return this.client.Post(sendUrl, points.ToString(), null, HttpContentTypes.TextPlain);
        }
    }
}
