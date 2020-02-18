using Framework.Logging;
using Framework.Utils;
using OpenQA.Selenium;

namespace Framework.Browsers
{
    public static class Browser
    {
        private static IWebDriver _instance;
        private static readonly object SyncRoot = new object();
        private static readonly Logg Log = Logg.GetInstance();

        public static IWebDriver GetInstance(string browser)
        {
            if (_instance == null)
            {
                lock (SyncRoot)
                {
                    if (_instance == null)
                        _instance = BrowserFactory.InitDriver(browser);
                }
            }
            return _instance;
        }

        public static void Quit()
        {
            if (_instance == null) return;
            _instance.Quit();
            _instance = null;
        }

        public static string GetCurrentUrl(this IWebDriver driver)
        {
            return driver.Url;
        }

        public static void OpenBaseUrl()
        {
            Log.Debug("browser");
            _instance.Url = JsonReader.GetBaseUrl();
        }

        public static void WaitForPageLoaded()
        {
            SmartWait.WaitFor(_instance,
                b => ((IJavaScriptExecutor) b).ExecuteScript("return document.readyState").Equals("complete"),
                JsonReader.GetPageLoadTimeOutInSeconds());
        }

        public static void RefreshPage()
        {
            _instance.Navigate().Refresh();
        }

        public static int GetWindowSize()
        {
            return _instance.Manage().Window.Size.Height;
        }

        public static void ScrollToElement(IWebElement webElement)
        {
            var jse = (IJavaScriptExecutor) _instance;
            jse.ExecuteScript("arguments[0].scrollIntoView(true)", webElement);
        }

        public static void ScrollToMiddle()
        {
            var jse = (IJavaScriptExecutor) _instance;
            jse.ExecuteScript($"window.scrollBy(0, {GetWindowSize() / 2})");
        }
    }
}