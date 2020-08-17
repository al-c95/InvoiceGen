using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using InvoiceGen.Model.ObjectModel;

namespace InvoiceGen_Tests.Model_Tests.DataAccessLayer_Tests
{
    [TestFixture]
    class Invoice_Tests
    {
        [Test]
        public void getTotal_Test()
        {
            // arrange
            Invoice toTest = new Invoice();
            List<InvoiceItem> items = new List<InvoiceItem>()
            { new InvoiceItem { description="item 1", amount=50.0M },
              new InvoiceItem { description="item 2", amount=25.50M } };
            toTest.items = items;
            decimal expectedTotal = 75.50M;

            // act
            decimal actualTotal = toTest.getTotal();

            // assert
            Assert.AreEqual(expectedTotal, actualTotal);
        }

        [Test]
        public void getTotal_Test_noItems()
        {
            // arrange
            Invoice toTest = new Invoice();
            decimal expectedTotal = 0;

            // act
            decimal actualTotal = toTest.getTotal();

            // assert
            Assert.AreEqual(expectedTotal, actualTotal);
        }
    }
}
