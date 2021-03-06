﻿using System;
using System.IO;
using Framework.Browsers;
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
        private static IWebDriver _instance;
        private static readonly object SyncRoot = new object();

        protected static readonly Configuration.Configuration Config =
            Configuration.Configuration.ParseConfiguration<Configuration.Configuration>(
                File.ReadAllText(Path.Combine(AppContext.BaseDirectory, ConfigFileName)));

        protected static IWebDriver GetInstance(string browser)
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

        protected static void QuitBrowser()
        {
            if (_instance == null) return;
            _instance.Quit();
            _instance = null;
        }
    }
}