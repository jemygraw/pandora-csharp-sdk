using EasyHttp.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qiniu.Pandora.Common;
using System;

namespace Qiniu.Pandora.Tsdb.Tests
{
    [TestClass()]
    public class QueryManagerTests
    {
        [TestMethod()]
        public void QueryTest()
        {
            string accessKey = pandora_csharp_sdkTests.Properties.Settings.Default.AccessKey;
            string secretKey = pandora_csharp_sdkTests.Properties.Settings.Default.SecretKey;
            string repoName = pandora_csharp_sdkTests.Properties.Settings.Default.NewRepoName;
            Auth auth = new Auth(accessKey, secretKey);
            QueryManager manager = new QueryManager(auth);
            string querySql = "select * from cs1";
            HttpResponse response = manager.Query(repoName, null, querySql);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            Console.WriteLine("response body: {0}", response.RawText);
        }
    }
}