using System;
using System.IO;
using Framework.Logging;
using Framework.Utils;
using OpenQA.Selenium;

namespace Framework.BaseClasses
{
    public class BaseEntity
    {
        protected static IWebDriver Driver;
        protected static readonly Logg Log = Logg.GetInstance();
        private const string ConfigFileName = "config.json";

        protected static readonly Configuration Config =
            Configuration.ParseConfiguration<Configuration>(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, ConfigFileName)));
    }
}