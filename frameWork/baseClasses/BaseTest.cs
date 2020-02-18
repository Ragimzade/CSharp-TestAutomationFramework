using Framework.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.BaseClasses
{
    [TestClass]
    public class BaseTest : BaseEntity
    {
        public TestContext TestContext { get; set; }

        
        [TestInitialize]
        public void Setup()
        {
            var driver = Browser.Browser.GetInstance();
            FileUtils.CleanDirectory(FileUtils.GetOutputDirectory());
            Browser.Browser.OpenBaseUrl();
        }

        [TestCleanup]
        public void TearDown()
        {
            ScreenShotUtils.TakeScreenshot(TestContext);
            Browser.Browser.Quit();
        }
    }
}