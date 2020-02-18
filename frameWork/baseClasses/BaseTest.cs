﻿using Framework.Utils;
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
            var driver = Browsers.Browser.GetInstance();
            FileUtils.CleanDirectory(FileUtils.GetOutputDirectory());
            Browsers.Browser.OpenBaseUrl();
        }

        [TestCleanup]
        public void TearDown()
        {
            ScreenShotUtils.TakeScreenshot(TestContext);
            Browsers.Browser.Quit();
        }
    }
}