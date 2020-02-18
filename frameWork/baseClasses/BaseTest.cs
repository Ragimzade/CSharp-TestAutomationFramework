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
            Driver = Browser.GetInstance(Config.Browser);
            FileUtils.CleanDirectory(FileUtils.BuildDirectoryPath());
            Browser.OpenBaseUrl();
            Log.Info(Config.Browser);
            Debug.WriteLine(Config.BaseUrl);
            Debug.WriteLine(Config.ImplicitWait);
            Debug.WriteLine(Config.PollingIntervalInMillis);
            Debug.WriteLine(Config.TimeOutInSeconds);
            Debug.WriteLine(Config.PageLoadTimeOutInSeconds);
            Debug.WriteLine(Config.Browser);
        }

        [TestCleanup]
        public void TearDown()
        {
            ScreenShotUtils.TakeScreenshot(TestContext);
            Browser.Quit();
        }
    }
}