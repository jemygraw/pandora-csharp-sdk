using EasyHttp.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qiniu.Pandora.Common;
using System;
using System.IO;
using System.Text;

namespace Qiniu.Pandora.Http.Tests
{
    [TestClass()]
    public class PandoraHttpClientTests
    {
        [TestMethod()]
        public void GetTest()
        {
            string accessKey = pandora_csharp_sdkTests.Properties.Settings.Default.AccessKey;
            string secretKey = pandora_csharp_sdkTests.Properties.Settings.Default.SecretKey;
            string repoName = pandora_csharp_sdkTests.Properties.Settings.Default.NewRepoName;
            string getUrl = String.Format("{0}/v2/repos/{1}", Config.PipelineHost, repoName);
            Auth auth = new Auth(accessKey, secretKey);
            Client client = new Client(auth);
            HttpResponse response = client.Get(getUrl, null);
            Console.WriteLine("status code: {0}", response.StatusCode);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            Console.WriteLine("response body: {0}", response.RawText);
        }
    }
}