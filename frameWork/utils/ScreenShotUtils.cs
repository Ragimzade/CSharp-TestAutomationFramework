using System.IO;
using Framework.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Framework.Utils
{
    public class ScreenshotUtils : BaseEntity
    {
        public static void TakeScreenshot(TestContext testContext)
        {
            switch (testContext.CurrentTestOutcome)
            {
                case UnitTestOutcome.Failed:
                case UnitTestOutcome.Error:
                case UnitTestOutcome.Aborted:
                    GetScreenshot(testContext);
                    break;
                case UnitTestOutcome.Passed:
                    Log.Info("The test passed successfully");
                    break;
                case UnitTestOutcome.Timeout:
                    Log.Info("The test timed out");
                    break;
            }
        }

        private static void GetScreenshot(TestContext testContext)
        {
            Log.Error("The test failed and about to grab a screenshot");
            var filename = Path.Combine(FileUtils.BuildDirectoryPath(),
                DateUtils.GetTimeStamp() + "-" + testContext.TestName + ".png");

            ((ITakesScreenshot) Driver).GetScreenshot()
                .SaveAsFile(filename, ScreenshotImageFormat.Png);
            testContext.AddResultFile(filename);
            Log.Error("The screen has been taken and stored as " + filename);
        }
    }
}