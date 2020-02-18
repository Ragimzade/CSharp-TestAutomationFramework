using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Framework.BaseClasses;
using Framework.Utils;
using OpenQA.Selenium;
using TestAutomation.model;

namespace MsTest.Pages
{
    public class ResultPage : BaseForm
    {
        private static readonly By BtnNext = By.XPath("//a[@class='next']");

        private static readonly By BtnSortByPrice =
            By.XPath("//div[@class='b-lo_right']//a[@class='by_price']");

        private static readonly By BtnSortByYear = By.XPath("//div[@class='b-lo_right']//a[@class='by_year']");

        private static readonly By BtnSortByDate = By.XPath("//div[@class='b-lo_right']//a[@class='by_date']");

        private static readonly By LblCarDescription = By.XPath("//div[@class='b-item_description']");

        private static readonly By LblChildCarName = By.XPath(".//div[@class='b-item_title']/a");

        private static readonly By LblChildCarPrice = By.XPath(".//div[@class='b-item_price']");
        
        private static readonly By LblChildCarYear = By.XPath(".//div[@class='b-descr_item_info']");
        
        private static readonly By LblChildCarDate = By.XPath(".//p[@class='b-le_company_inf']");

        private List<CarData> GetCarsOnPage()
        {
            WaitForChildElement(LblCarDescription, LblChildCarName);
            var cars = Driver.FindElements(LblCarDescription);
            return cars.Select(GetCarData).ToList();
        }

        private CarData GetCarData(ISearchContext car)
        {
            var name = car.FindElement(LblChildCarName).Text;
            var price = CutNonDigitCharacters(car.FindElement(LblChildCarPrice).Text);
            var year = CutNonDigitCharacters(car.FindElement(LblChildCarYear).Text);
            var date = CutCharactersAfterComma(car.FindElement(LblChildCarDate).Text);
            var carData = new CarData(name, int.Parse(price), year, DateConverter.ConvertDate(date));
            return carData;
        }

        public List<CarData> GetAllCars()
        {
            var allCars = GetCarsOnPage();
            while (IsElementPresent(BtnNext))
            {
                ScrollToElementAndClick(WaitForElement(BtnNext));
                var carsOnPage = GetCarsOnPage();
                allCars.AddRange(carsOnPage);
            }

            return allCars;
        }

        private string CutCharactersAfterComma(string valueToCut) =>
            Regex.Match(valueToCut, @"([^,]+$)")
                .Value
                .Replace(" ", string.Empty);

        private string CutNonDigitCharacters(string valueToCut) =>
            Regex.Match(valueToCut, @"^[^aA-aA-zZ-яЯ]*")
                .Value
                .Replace(" ", string.Empty);

        public List<CarData> FilterByPrice()
        {
            WaitForElementToBeClickable(BtnSortByPrice).Click();
            return GetAllCars();
        }

        public List<CarData> FilterByYear()
        {
            WaitForElementToBeClickable(BtnSortByYear).Click();
            return GetAllCars();
        }

        public List<CarData> FilterByDate()
        {
            WaitForElementToBeClickable(BtnSortByDate).Click();
            return GetAllCars();
        }

        public ResultPage() : base(By.ClassName("products-list"), "ResultPage")
        {
        }
    }
}