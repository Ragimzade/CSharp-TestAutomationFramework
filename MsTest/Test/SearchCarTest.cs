using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Assertions;
using Framework.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsTest.Pages;
using TestAutomation.model;

namespace MsTest.Test
{
    [TestClass]
    public class SearchCarTest : BaseTest
    {
        private const string Brand = "Lexus";
        private const string Model = "LX";
        private static List<CarData> _cars = new List<CarData>();
        private static List<CarData> _carsSortedByPrice = new List<CarData>();
        private static List<CarData> _carsSortedByYear = new List<CarData>();
        private static List<CarData> _carsSortedByDate = new List<CarData>();

        [TestMethod]
        public void SearchCar()
        {
            var softAssert = new SoftAssertions();

            Log.Step(1, "Go to cars sale page");
            var mainPage = new MainPage();
            var carsSalePage = mainPage.GoToCarsSalePage();

            Log.Step(2, "Searching for cars");
            carsSalePage.FilterCars(Brand, Model);
            var resultPage = new ResultPage();
            _cars = resultPage.GetAllCars();

            Log.Step(3, "Verifying there are searching results with correct cars");
            foreach (var car in _cars)
                softAssert.True("Searching results don't contain correct brand", car.Name.Contains(Brand));

            Log.Step(4, "Filter result by price");
            _carsSortedByPrice = resultPage.FilterByPrice();

            Log.Step(5, "Verify that result is filtered  by price");
            var expectedSortingByPrice = _cars.OrderByDescending(car => car.Price)
                .ThenByDescending(car => car.Year).ToList();
            softAssert.True("Cars are not sorted correctly by Price",
                expectedSortingByPrice.SequenceEqual(_carsSortedByPrice));

            Log.Step(6, "Sort result by year");
            _carsSortedByYear = resultPage.FilterByYear();

            Log.Step(7, "Verify that result is sorted by year");
            var expectedSortingByYear = _cars.OrderByDescending(car => car.Year).ToList();
            softAssert.True("Cars are not sorted correctly by Year",
                expectedSortingByYear.SequenceEqual(_carsSortedByYear, new CarDataComparer()));

            Log.Step(8, "Sort result by publish date");
            _carsSortedByDate = resultPage.FilterByDate();


            Log.Step(9, "Verify that result is sorted by publish date");
            var expectedSortingByDate = _cars.OrderByDescending(x =>
                {
                    DateTime.TryParse(x.Date, out var date);
                    return date;
                })
                .ToList();

            softAssert.True("Cars are not sorted correctly by Date",
                expectedSortingByDate.SequenceEqual(_carsSortedByDate, new CarDataComparer()));

            Log.Step(10, "Get assertion errors");
            softAssert.AssertAll();
        }
    }
}