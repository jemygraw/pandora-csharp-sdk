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
            for (int i = 1; i <= 1000000; i++)
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
                    // send the data first
                    Console.WriteLine("send part batch length: {0}", batch.GetSize());
                    HttpResponse response1 = dataSender.Send(repoName, batch);
                    //Console.WriteLine(batch.ToString());
                    Assert.AreEqual(System.Net.HttpStatusCode.OK, response1.StatusCode);

                    batch.Clear();// clear for next batch
                    batch.Add(p);
                }
            }

            //send the remained data
            Console.WriteLine("send final batch length: {0}", batch.GetSize());
            HttpResponse response2 = dataSender.Send(repoName, batch);
            //Console.WriteLine(batch.ToString());
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response2.StatusCode);

        }
    }
}