using System;
using System.Collections.ObjectModel;
using System.Linq;
using Framework.BaseClasses;
using Framework.Utils;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Framework.Elements
{
    public class ElementFinder : BaseEntity
    {
        private static readonly int TimeOutSeconds = JsonReader.GetTimeoutInSeconds();
        private static readonly int TimeOutInMillis = JsonReader.GetPollingIntervalInMillis();

        private IWebElement InternalFinder(By locator)
        {
            try
            {
                return SmartWait.WaitFor(Driver, ExpectedConditions.ElementIsVisible(locator),
                    TimeOutSeconds, TimeOutInMillis);
            }
            catch (Exception)
            {
                throw new Exception(
                    $"WebDriverTimeoutException: Element {locator} was not found for {JsonReader.GetTimeoutInSeconds()} seconds");
            }
        }
        
        protected IWebElement WaitForElement(By locator)
        {
            return InternalFinder(locator);
        }

        protected IWebElement FindChildren(By parentLocator, By childLocator)
        {
            return WaitForElement(parentLocator).FindElements(childLocator).ElementAt(0);
        }

        protected void WaitForChildElement(By parentLocator, By childLocator)
        {
            WaitForElement(parentLocator).FindElements(childLocator);
        }

        protected ReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            return Driver.FindElements(locator);
        }

        private void WaitForCondition<T>(Func<IWebDriver, T> condition)
        {
            SmartWait.WaitFor(Driver, condition, TimeOutSeconds, TimeOutInMillis);
        }


        protected IWebElement FindElement(By locator)
        {
            return InternalFinder(locator);
        }
        
        protected bool IsElementPresent(By locator)
        {
            try
            {
                WaitForCondition(ExpectedConditions.ElementIsVisible(locator));
                return true;
            }
            catch (WebDriverTimeoutException )
            {
                return false;
            }
        }

        public void WaitForPresent(By locator)
        {
            WaitForCondition(ExpectedConditions.ElementExists(locator));
        }

        public IWebElement WaitForElementToBeClickable(By locator)
        {
            WaitForCondition(ExpectedConditions.ElementToBeClickable(locator));
            return InternalFinder(locator);
        }

        public void ScrollToElementAndClick(IWebElement webElement)
        {
            var jse = (IJavaScriptExecutor) Driver;
            jse.ExecuteScript("arguments[0].scrollIntoView(true)", webElement);
            webElement.Click();
        }
    }
}