using Framework.BaseClasses;
using OpenQA.Selenium;

namespace MsTest.Pages
{
    public class MainPage : BaseForm
    {
        
        private static readonly By BtnCarsSale = By.XPath("//a[contains(@title,'Продажа автомобилей')]");
        
        public MainPage() : base(By.XPath("//a[contains(@title,'Продажа автомобилей')]"), "MainPage")
        {
        }

        public CarsSalePage GoToCarsSalePage()
        {
            WaitForElement(BtnCarsSale).Click();
            return new CarsSalePage();
        }
    }
}