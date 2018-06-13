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
            //set the time between CST times and timezone use +08
            string querySql = "select * from cs1 where time > '2018-06-13T10:36:28Z' and time < '2018-06-13T10:36:59Z'";
            HttpResponse response = manager.Query(repoName, null, querySql);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            //the returned time are all in utc format
            Console.WriteLine("response body: {0}", response.RawText);
        }
    }
}