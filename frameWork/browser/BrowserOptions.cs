﻿using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Test.utils;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Test.browser
{
    public class BrowserOptions
    {
        protected internal static ChromeOptions GetChromeOptions()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("profile.default_content_settings.popups", 0);
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", "false");
            chromeOptions.AddUserProfilePreference("download.default_directory", JsonReader.GetBrowserDownloadPath());
            chromeOptions.AddUserProfilePreference("download.directory_upgrade", "true");
            chromeOptions.AddUserProfilePreference("safebrowsing.enabled", "true");
            chromeOptions.AddArguments("--lang=eng");
            chromeOptions.AddArguments("start-maximized");
            return chromeOptions;
        }

        protected internal static FirefoxOptions GetFirefoxOptions()
        {
            new DriverManager().SetUpDriver(new FirefoxConfig());
            var profile = new FirefoxProfile();
            profile.SetPreference("intl.accept_languages", "eng");
            profile.SetPreference("browser.download.folderList", 2);
            profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/octet-stream");
            profile.SetPreference("browser.download.dir", JsonReader.GetBrowserDownloadPath());
            var firefoxOptions = new FirefoxOptions {Profile = profile};
            return firefoxOptions;
        }
    }
}