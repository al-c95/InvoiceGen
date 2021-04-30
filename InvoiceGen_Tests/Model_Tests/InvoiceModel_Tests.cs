using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using InvoiceGen.Models;
using InvoiceGen.Models.ObjectModel;
using InvoiceGen.Models.Repository;
using InvoiceGen.View;
using InvoiceGen.Presenter;
using FakeItEasy;

namespace InvoiceGen_Tests.Model_Tests
{
    [TestFixture]
    public class InvoiceModel_Tests
    {
        [TestCase("January 0000", true)]
        [TestCase("February 0000", true)]
        [TestCase("March 0000", true)]
        [TestCase("April 0000", true)]
        [TestCase("May 0000", true)]
        [TestCase("June 0000", true)]
        [TestCase("July 0000", true)]
        [TestCase("August 0000", true)]
        [TestCase("September 0000", true)]
        [TestCase("October 0000", true)]
        [TestCase("November 0000", true)]
        [TestCase("December 0000", true)]
        [TestCase("Jan 0000", false)]
        [TestCase("test invoice", false)]
        public void IsMonthlyInvoice_Test(string title, bool shouldBeMonthly)
        {
            // arrange
            InvoiceModel model = new InvoiceModel();

            // act
            bool isMonthly = model.IsMonthlyInvoice(title);

            // assert
            Assert.AreEqual(shouldBeMonthly, isMonthly);
        }

        [Test]
        public void AmountToDisplay_Test()
        {
            // arrange
            InvoiceModel model = new InvoiceModel();
            decimal amount = 2.50M;
            string expectedResult = @"$2.50";

            // act
            string actualResult = model.GetAmountToDisplay(amount);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void AmountToDisplayAsTotal_Test()
        {
            // arrange
            InvoiceModel model = new InvoiceModel();
            decimal amount = 2.50M;
            string expectedResult = @"Total: $2.50";

            // act
            string actualResult = model.GetAmountToDisplayAsTotal(amount);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("0.50", true)]
        [TestCase("0.5", false)]
        [TestCase("0.501", true)]
        [TestCase("test", false)]
        public void AmountEntryValid_Test(string entry, bool shouldBeValid)
        {
            // arrange
            InvoiceModel model = new InvoiceModel();

            // act
            bool isValid = model.AmountEntryValid(entry);

            // assert
            Assert.AreEqual(shouldBeValid, isValid);
        }
    }
}
