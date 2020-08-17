using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using System.Linq;
using InvoiceGen.Model.DataAccessLayer;
using InvoiceGen.Model.ObjectModel;
using InvoiceGen.Model.Repository;

namespace InvoiceGen_Tests.Model_Tests.Repository_Tests
{
    [TestFixture]
    class InvoiceRepository_Tests
    {
        [Test]
        public void getAllInvoices_Test()
        {
            // arrange
            var fakeService = A.Fake<IXmlService>();
            InvoiceGen.Model.Repository.InvoiceRepository repo = new InvoiceGen.Model.Repository.InvoiceRepository(fakeService);

            // act
            repo.getAllInvoices();

            // assert
            A.CallTo(() => fakeService.readXml()).MustHaveHappened();
        }

        [Test]
        public void getInvoiceById_Test_multipleRecordsSameId()
        {
            // arrange
            var fakeService = A.Fake<IXmlService>();
            int iD = 1;
            A.CallTo(() => fakeService.readXml()).Returns(new List<Invoice> { new Invoice { id = iD }, new Invoice { id = iD } });
            InvoiceGen.Model.Repository.InvoiceRepository repo = new InvoiceGen.Model.Repository.InvoiceRepository(fakeService);

            // act/assert
            Assert.Throws<System.ApplicationException>(delegate { repo.getInvoiceById(iD); });
        }

        [Test]
        public void getInvoiceById_Test_noRecordFound()
        {
            // arrange
            var fakeService = A.Fake<IXmlService>();
            int iD = 1;
            A.CallTo(() => fakeService.readXml()).Returns(new List<Invoice> { new Invoice { id = 2 }, new Invoice { id = 3 } });
            InvoiceGen.Model.Repository.InvoiceRepository repo = new InvoiceGen.Model.Repository.InvoiceRepository(fakeService);

            // act/assert
            Assert.AreEqual(null, repo.getInvoiceById(iD));
        }

        [Test]
        public void getInvoiceById_Test_recordFound()
        {
            // arrange
            var fakeService = A.Fake<IXmlService>();
            int iD = 1;
            string expectedTitle = "I am the one!";
            A.CallTo(() => fakeService.readXml()).Returns(new List<Invoice> {
                new Invoice { id = 1, title = expectedTitle }, new Invoice { id = 3 } });
            InvoiceGen.Model.Repository.InvoiceRepository repo = new InvoiceGen.Model.Repository.InvoiceRepository(fakeService);

            // act
            Invoice result = repo.getInvoiceById(iD);

            // assert
            Assert.AreEqual(expectedTitle, result.title);
        }
    }
}
