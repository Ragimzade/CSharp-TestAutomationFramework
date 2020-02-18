using System.Diagnostics;
using System.IO;
using Framework.BaseClasses;
using Framework.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MsTest.Test
{
    [TestClass]
    public class JustTest : BaseTest
    {
        [TestMethod]
        public void Just()
        {
            const string configFileName = "config.json";

            using var sr = new StreamReader(configFileName);
            var reader = new JsonTextReader(sr);
            var jObject = JObject.Load(reader);
        }
    }
}