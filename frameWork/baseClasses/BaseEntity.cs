using Framework.Logging;
using OpenQA.Selenium;

namespace Framework.BaseClasses
{
    public class BaseEntity
    {
        protected static readonly IWebDriver Driver = Browser.Browser.GetInstance();
        protected static readonly Logg Log = Logg.GetInstance();
        
    }
}