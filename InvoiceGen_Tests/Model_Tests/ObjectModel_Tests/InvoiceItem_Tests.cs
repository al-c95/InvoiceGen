using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using InvoiceGen.Model.ObjectModel;

namespace InvoiceGen_Tests.Model_Tests.ObjectModel_Tests
{
    [TestFixture]
    public class InvoiceItem_Tests
    {
        [Test]
        public void Equals_Test_TypeMismatch()
        {
            // arrange
            InvoiceItem a = new InvoiceItem { Description = "Item", Amount = 2.50M };
            int b = 0;

            // act/assert
            NUnit.Framework.Assert.Throws<InvalidOperationException>(() => a.Equals(b));
        }

        [Test]
        public void Equals_Test_AreEqual()
        {
            // arrange
            InvoiceItem a = new InvoiceItem { Description = "Item", Amount = 2.50M };
            InvoiceItem b = new InvoiceItem { Description = "Item", Amount = 2.50M };

            // act/assert
            NUnit.Framework.Assert.IsTrue(a.Equals(b));
        }

        [TestCase("Item1", 2.50, "Item2", 3.50)]
        [TestCase("Item1", 2.50, "Item1", 3.50)]
        [TestCase("Item2", 2.50, "Item2", 3.50)]
        [TestCase("Item2", 2.50, "Item2", 3.50)]
        public void Equals_Test_AreNotEqual(string descriptionA, decimal amountA,
                                            string descriptionB, decimal amountB)
        {
            // arrange
            InvoiceItem a = new InvoiceItem { Description = descriptionA, Amount = amountA };
            InvoiceItem b = new InvoiceItem { Description = descriptionB, Amount = amountB };

            // act/assert
            NUnit.Framework.Assert.IsFalse(a.Equals(b));
        }
    }
}
