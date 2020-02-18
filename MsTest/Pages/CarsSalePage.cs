using Framework.BaseClasses;
using OpenQA.Selenium;

namespace MsTest.Pages
{
    public class CarsSalePage : BaseForm
    {
        private static readonly By BtnBrand = By.XPath("//button[@title='Марка']");
        private static readonly By BtnModel = By.XPath("//button[@title='Модель']");
        private static readonly By BtnResult = By.XPath("//a[@class='js-filter-result']");
        private static By BrandValue(string brand) => By.XPath($"//a[@role='option']/span[contains(.,'{brand}')]");
        private static By ModelValue(string model) => By.XPath($"//a[@role='option']/span[contains(.,'{model}')]");

        public CarsSalePage() : base(By.Id("cars-sell-form"), "CarsSell")
        {
        }

        public void FilterCars(string brand, string model = null)
        {
            WaitForElementToBeClickable(BtnBrand).Click();
            WaitForElementToBeClickable(BrandValue(brand)).Click();
            if (model != null)
            {
                WaitForElementToBeClickable(BtnModel).Click();
                WaitForElementToBeClickable(ModelValue(model)).Click();
            }

            WaitForElementToBeClickable(BtnResult).Click();
        }
    }
}