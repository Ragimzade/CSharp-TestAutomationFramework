using Framework.BaseClasses;
using OpenQA.Selenium;

namespace MsTest.Pages
{
    public class MainPage : BaseForm
    {
        private static readonly By BtnCarsSale = By.XPath("//a[contains(@title,'Продажа автомобилей')]");
        private static readonly By FormMainPage = By.XPath("//a[contains(@title,'Продажа автомобилей')]");
        
        public MainPage() : base(FormMainPage, "MainPage")
        {
        }

        public CarsSalePage GoToCarsSalePage()
        {
            WaitForElement(BtnCarsSale).Click();
            return new CarsSalePage();
        }
    }
}