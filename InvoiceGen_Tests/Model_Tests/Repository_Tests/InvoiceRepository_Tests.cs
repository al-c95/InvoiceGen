using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using System.Linq;
using InvoiceGen.Models.DataAccessLayer;
using InvoiceGen.Models.ObjectModel;
using InvoiceGen.Models.Repository;

namespace InvoiceGen_Tests.Model_Tests.Repository_Tests
{
    [TestFixture]
    class InvoiceRepository_Tests
    {
        [Test]
        public void GetAllInvoices_Test()
        {
            // arrange
            var fakeService = A.Fake<IXmlService>();
            InvoiceRepository repo = new InvoiceRepository(fakeService);

            // act
            repo.GetAllInvoices();

            // assert
            A.CallTo(() => fakeService.ReadXml()).MustHaveHappened();
        }

        [Test]
        public void GetInvoiceById_Test_MultipleRecordsSameId()
        {
            // arrange
            var fakeService = A.Fake<IXmlService>();
            int iD = 1;
            A.CallTo(() => fakeService.ReadXml()).Returns(new List<Invoice> { new Invoice { Id = iD }, new Invoice { Id = iD } });
            InvoiceRepository repo = new InvoiceRepository(fakeService);

            // act/assert
            Assert.Throws<System.ApplicationException>(delegate { repo.GetInvoiceById(iD); });
        }

        [Test]
        public void GetInvoiceById_Test_NoRecordFound()
        {
            // arrange
            var fakeService = A.Fake<IXmlService>();
            int iD = 1;
            A.CallTo(() => fakeService.ReadXml()).Returns(new List<Invoice> { new Invoice { Id = 2 }, new Invoice { Id = 3 } });
            InvoiceRepository repo = new InvoiceRepository(fakeService);

            // act/assert
            Assert.AreEqual(null, repo.GetInvoiceById(iD));
        }

        [Test]
        public void GetInvoiceById_Test_RecordFound()
        {
            // arrange
            var fakeService = A.Fake<IXmlService>();
            int iD = 1;
            string expectedTitle = "I am the one!";
            A.CallTo(() => fakeService.ReadXml()).Returns(new List<Invoice> {
                new Invoice { Id = 1, Title = expectedTitle }, new Invoice { Id = 3 } });
            InvoiceRepository repo = new InvoiceRepository(fakeService);

            // act
            Invoice result = repo.GetInvoiceById(iD);

            // assert
            Assert.AreEqual(expectedTitle, result.Title);
        }

        /*
        [TestCase("exists", ExpectedResult = true)]
        [TestCase("does not exist", ExpectedResult = false)]
        public bool invoiceWithTitleExists_Test(string title, bool ExpectedResult)
        {
            // arrange
            var fakeService = A.Fake<IXmlService>();
            A.CallTo(() => fakeService.readXml()).Returns(new List<Invoice> { new Invoice { title=title } });
            InvoiceRepository repo = new InvoiceRepository(fakeService);

            // act
            bool actualResult = repo.invoiceWithTitleExists(title);

            // assert
            //return result;
        }
        */

        [Test]
        public void UpdatePaidStatus_Test()
        {
            // arrange
            var fakeService = A.Fake<IXmlService>();
            InvoiceRepository repo = new InvoiceRepository(fakeService);
            int id = 1;
            bool paid = true;

            // act
            repo.UpdatePaidStatus(id, paid);

            // assert
            A.CallTo(() => fakeService.UpdatePaidStatusInXml(id, paid)).MustHaveHappened();
        }

        [Test]
        public void DeleteInvoice_Test()
        {
            // arrange
            var fakeService = A.Fake<IXmlService>();
            InvoiceRepository repo = new InvoiceRepository(fakeService);
            int id = 1;

            // act
            repo.DeleteInvoice(id);

            // assert
            A.CallTo(() => fakeService.DeleteInvoiceInXml(id)).MustHaveHappened();
        }
    }
}
