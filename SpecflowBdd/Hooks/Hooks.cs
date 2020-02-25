using System;
using System.IO;
using System.Reflection;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using BoDi;
using Framework.BaseClasses;
using Framework.Utils;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace Bdd.Hooks
{
    [Binding]
    public class Hooks : BaseEntity
    {
        private static ScenarioContext _scenarioContext;
        private static FeatureContext _featureContext;
        private static ExtentReports _extentReports;
        private static ExtentHtmlReporter _extentHtmlReporter;
        private static ExtentTest _feature;
        private static ExtentTest _scenario;
        
        [BeforeTestRun]
        public static void InitializeReport()
        {
            _extentHtmlReporter =
                new ExtentHtmlReporter(Path.Combine(AppContext.BaseDirectory,
                    Config.ScreenshotsFolder, "extent.html"));
            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(_extentHtmlReporter);
        }

        [BeforeFeature]
        public static void BeforeFeatureStart(FeatureContext featureContext)
        {
            if (null != featureContext)
            {
                _feature = _extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title,
                    featureContext.FeatureInfo.Description);
            }
        }

        [BeforeScenario]
        public static void BeforeScenarioStart(ScenarioContext scenarioContext)
        {
            if (null == scenarioContext) return;
            _scenarioContext = scenarioContext;
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title,
                scenarioContext.ScenarioInfo.Description);
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extentReports.Flush();
        }

        [AfterStep]
        public void InsertReportingSteps(ScenarioContext scenarioContext)
        {
            var stepInfo = scenarioContext.StepContext.StepInfo;

            var stepStatus = scenarioContext.ScenarioExecutionStatus;
            ExtentTest test;
            switch (stepInfo.StepDefinitionType)
            {
                case StepDefinitionType.Given:
                    test = _scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text);
                    break;
                case StepDefinitionType.When:
                    test = _scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text);
                    break;
                case StepDefinitionType.Then:
                    test = _scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text);
                    break;
                default:
                    test = _scenario.CreateNode("default");
                    break;
            }

            if (stepStatus != ScenarioExecutionStatus.OK)
            {
                var screenShootName = ScenarioContext.Current.ScenarioInfo.Title.Replace(" ", "").ToLower();
                ScreenshotUtils.TakeScreenshot(screenShootName);
                _extentReports.AddTestRunnerLogs($"<img src=\"" + screenShootName + ".png\"></pre>");
                test.Fail(string.Format("Error from: {0}\nError Details: {1}\nStacktrace: {2}",
                    scenarioContext.TestError.Source, scenarioContext.TestError,
                    scenarioContext.TestError.StackTrace));
                Log.Error(string.Format("Error from: {0}\nError Details: {1}\nStacktrace: {2}",
                    scenarioContext.TestError.Source, scenarioContext.TestError,
                    scenarioContext.TestError.StackTrace));
            }
        }

        // [AfterStep]
        // public void AfterTestStep()
        // {
        //     string stepType = ScenarioContext.Current.StepContext.StepInfo.StepDefinitionType.ToString();
        //     var error = ScenarioContext.Current.TestError;
        //     string stepInfo = ScenarioContext.Current.StepContext.StepInfo.Text;
        //
        //     if (stepType == "Given" && error != null)
        //
        //         _scenario.CreateNode<Given>(stepInfo).Fail(error.Message);
        //
        //     else if (stepType == "Given" && error == null)
        //
        //         _scenario.CreateNode<Given>(stepInfo).Pass("step passed");
        //
        //     if (stepType == "When" && error != null)
        //
        //         _scenario.CreateNode<When>(stepInfo).Fail(error.Message);
        //
        //     else if (stepType == "When" && error == null)
        //
        //         _scenario.CreateNode<When>(stepInfo).Pass("step passed");
        //
        //     if (stepType == "Then" && error != null)
        //     {
        //
        //         // var screenShootName = ScenarioContext.Current.ScenarioInfo.Title.Replace(" ", "").ToLower();
        //         // takeScreenShot(screenShootName);
        //
        //         _scenario.CreateNode<Then>(stepInfo).Fail(error.Message);
        //         _extentReports.AddTestRunnerLogs(ScenarioContext.Current.ScenarioInfo.Title);
        //         _extentReports.AddTestRunnerLogs($"<pre><h6>Step info: </h6>{stepInfo}</pre>");
        //         _extentReports.AddTestRunnerLogs($"<pre><h6>Error description: </h6>{error.Message}</pre>");
        //
        //         _extentReports.AddTestRunnerLogs($"<pre><h6>Error message: </h6>{error}");
        //         // _extentReports.AddTestRunnerLogs($"<img src=\"" + screenShootName + ".png\"></pre>");
        //
        //     }
        //     else if (stepType == "Then" && error == null)
        //
        //         _scenario.CreateNode<Then>(stepInfo).Pass("step passed");
        //
        //     if (stepType == "And" && error != null)
        //
        //         _scenario.CreateNode<And>(stepInfo).Fail(error.Message);
        //
        //     else if (stepType == "And" && error == null)
        //
        //         _scenario.CreateNode<And>(stepInfo).Pass("step passed");
        // }

        [BeforeScenario]
        public void BeforeScenarioSteps()
        {
            Driver = GetDriver(Config.Browser);
            FileUtils.CleanDirectory(FileUtils.BuildDirectoryPath());
        }

        [AfterScenario]
        public void AfterScenarioSteps(ScenarioContext scenarioContext)
        {
            Log.Error(
                $"The scenario {scenarioContext.ScenarioInfo.Title} has finished with test error(s): {scenarioContext.TestError}");
            // ScreenshotUtils.TakeScreenshot();
            QuitBrowser();
        }
    }
}