using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qiniu.Pandora.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}