using Qiniu.Pandora.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Qiniu.Pandora.Common.Tests
{
    [TestClass()]
    public class UtilTests
    {
        [TestMethod()]
        public void JsonEncodeTest()
        {
            List<string> alias = new List<string>();
            alias.Add("big");
            alias.Add("fast");
            string jsonStr = Util.JsonEncode(alias);
            Console.WriteLine(jsonStr);
        }

        [TestMethod()]
        public void CreateRFC3339DateTimeTest()
        {
            Console.WriteLine(Util.CreateRFC3339DateTime(DateTime.Now));
        }

        [TestMethod()]
        public void FromRFC3339DateTimeTest()
        {
            string dateTimeStr = "2018-06-13T02:36:28.916Z";
            Console.WriteLine(Util.FromRFC3339DateTime(dateTimeStr));
            Console.WriteLine(Util.CreateRFC3339DateTime(Util.FromRFC3339DateTime(dateTimeStr)));
        }
    }
}