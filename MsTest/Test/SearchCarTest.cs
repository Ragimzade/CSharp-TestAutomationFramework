using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Framework.Assertions;
using Framework.BaseClasses;
using Framework.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsTest.Model;
using MsTest.Pages;

namespace MsTest.Test
{
    [TestClass]
    public class SearchCarTest : BaseTest
    {
        private const string Brand = "Lexus";
        private const string Model = "LX";

        [TestMethod]
        public void SearchCar()
        {
            Debug.Write(JsonReader.GetBrowser());
            var softAssert = new SoftAssertions();

            Log.Step(1, "Go to cars sale page");
            var mainPage = new MainPage();
            var carsSalePage = mainPage.GoToCarsSalePage();

            Log.Step(2, "Searching for cars");
            carsSalePage.FilterCars(Brand, Model);
            var resultPage = new ResultPage();
            var cars = resultPage.GetAllCars();

            Log.Step(3, "Verifying there are searching results with correct cars");
            foreach (var car in cars)
                Assert.IsTrue(car.Name.Contains(Brand));

            Log.Step(4, "Filter result by price");
            var carsSortedByPrice = resultPage.FilterByPrice();

            Log.Step(5, "Verify that result is filtered  by price");
            var expectedSortingByPrice = cars.OrderByDescending(car => car.Price)
                .ThenByDescending(car => car.Year).ToList();
            softAssert.True("Cars are not sorted correctly by Price",
                expectedSortingByPrice.SequenceEqual(carsSortedByPrice));

            Log.Step(6, "Sort result by year");
            var carsSortedByYear = resultPage.FilterByYear();

            Log.Step(7, "Verify that result is sorted by year");
            var expectedSortingByYear = cars.OrderByDescending(car => car.Year).ToList();
            softAssert.True("Cars are not sorted correctly by Year",
                expectedSortingByYear.SequenceEqual(carsSortedByYear, new CarDataComparer()));

            Log.Step(8, "Sort result by publish date");
            var carsSortedByDate = resultPage.FilterByDate();


            Log.Step(9, "Verify that result is sorted by publish date");
            var expectedSortingByDate = cars.OrderByDescending(x =>
                {
                    DateTime.TryParse(x.Date, out var date);
                    return date;
                })
                .ToList();

            softAssert.True("Cars are not sorted correctly by Date",
                expectedSortingByDate.SequenceEqual(carsSortedByDate, new CarDateComparer()));

            Log.Step(10, "Get assertion errors");
            softAssert.AssertAll();
        }
    }
}