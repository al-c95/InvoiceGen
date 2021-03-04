using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using InvoiceGen.Model.ObjectModel;

namespace InvoiceGen_Tests.Model_Tests.ObjectModel_Tests
{
    [TestFixture]
    class Invoice_Tests
    {
        [Test]
        public void GetTotal_Test_ItemsExist()
        {
            // arrange
            Invoice toTest = new Invoice();
            List<InvoiceItem> items = new List<InvoiceItem>()
            {
              new InvoiceItem { Description="item 1", Amount=50.0M },
              new InvoiceItem { Description="item 2", Amount=25.50M }
            };
            toTest.Items = items;
            decimal expectedTotal = 75.50M;

            // act
            decimal actualTotal = toTest.GetTotal();

            // assert
            Assert.AreEqual(expectedTotal, actualTotal);
        }

        [Test]
        public void GetTotal_Test_NoItems()
        {
            // arrange
            Invoice toTest = new Invoice();
            decimal expectedTotal = 0;

            // act
            decimal actualTotal = toTest.GetTotal();

            // assert
            Assert.AreEqual(expectedTotal, actualTotal);
        }
    }
}
