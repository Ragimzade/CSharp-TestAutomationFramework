using System.Diagnostics;
using System.IO;
using Framework.Browsers;
using Framework.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Framework.BaseClasses
{
    [TestClass]
    public class BaseTest : BaseEntity
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Setup()
        {
            Driver = GetInstance(Config.Browser);
            FileUtils.CleanDirectory(FileUtils.BuildDirectoryPath());
            Driver.OpenBaseUrl();
        }

        [TestCleanup]
        public void TearDown()
        {
            ScreenshotUtils.TakeScreenshot(TestContext);
            QuitBrowser();
        }
    }
}