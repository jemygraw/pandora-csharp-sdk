using EasyHttp.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qiniu.Pandora.Common;
using System;
using System.Collections.Generic;

namespace Qiniu.Pandora.Pipeline.Tests
{
    [TestClass()]
    public class DataSenderTests
    {
        [TestMethod()]
        public void SendTest()
        {
            string accessKey = pandora_csharp_sdkTests.Properties.Settings.Default.AccessKey;
            string secretKey = pandora_csharp_sdkTests.Properties.Settings.Default.SecretKey;
            string repoName = pandora_csharp_sdkTests.Properties.Settings.Default.NewRepoName;
            Auth auth = new Auth(accessKey, secretKey);
            DataSender dataSender = new DataSender(auth);
            Batch batch = new Batch();
            //timestamp date
            //sensor    string
            //length    long
            //value     float
            //working   boolean
            //alias     jsonstring
            DateTime timestamp = DateTime.Now;
            for (int i = 1; i <= 100; i++)
            {
                string sensor = string.Format("sensor{0}", i);
                int length = new Random().Next(1000);
                double value = new Random().NextDouble() * 1000;
                bool working = length % 2 == 0;
                List<string> alias = new List<string>();
                alias.Add("big");
                alias.Add("fast");

                Point p = new Point();
                p.Append("timestamp", timestamp);
                p.Append("sensor", sensor);
                p.Append("length", length);
                p.Append("value", value);
                p.Append("working", working);
                p.Append("alias", alias);
                if (batch.CanAdd(p))
                {
                    batch.Add(p);
                }
                else
                {
                    Assert.Fail("too many points");
                }
            }
            HttpResponse response = dataSender.Send(repoName, batch);
            Console.WriteLine(batch.ToString());
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}