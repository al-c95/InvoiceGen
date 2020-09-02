using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using InvoiceGen.Model.Repository;
using InvoiceGen.View;
using InvoiceGen.Presenter;

namespace InvoiceGen_Tests.Presenter_Tests
{
    [TestFixture]
    class Presenter_Tests
    {
        [Test]
        public void updatePaidStatus_Test()
        {
            // arrange
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var fakeView = A.Fake<IMainWindow>();
            MainPresenter presenter = new MainPresenter(fakeView, fakeRepo);
            int id = 1;

            // act
            presenter.setInvoicePaid(id);

            // assert
            A.CallTo(() => fakeRepo.updatePaidStatus(id, true)).MustHaveHappened();
        }

        [Test]
        public void populateInvoiceHistory_Test()
        {
            // arrange
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var fakeView = A.Fake<IMainWindow>();
            MainPresenter presenter = new MainPresenter(fakeView, fakeRepo);

            // act

            // assert
        }
        /*
        [Test]
        public void addItemToNewInvoice_Test()
        {
            // arrange

            // act

            // asert
        }
        */
        [TestCase("my invoice", ExpectedResult = true)]
        [TestCase("", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public bool customDescriptionIsValid_Test(string description)
        {
            // arrange
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var fakeView = A.Fake<IMainWindow>();
            A.CallTo(() => fakeView.customTitleText).Returns(description);
            MainPresenter presenter = new MainPresenter(fakeView, fakeRepo);

            // act
            bool result = presenter.customDescriptionIsValid();

            // assert
            return result;
        }

        [TestCase("test", ExpectedResult = false)]
        [TestCase("2020", ExpectedResult = true)]
        [TestCase("", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public bool yearIsValid_Test(string year)
        {
            // arrange
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var fakeView = A.Fake<IMainWindow>();
            A.CallTo(() => fakeView.year).Returns(year);
            MainPresenter presenter = new MainPresenter(fakeView, fakeRepo);

            // act
            bool result = presenter.yearIsValid();

            // assert
            return result;
        }

        [TestCase("Jan", ExpectedResult = true)]
        [TestCase("Feb", ExpectedResult = true)]
        [TestCase("Mar", ExpectedResult = true)]
        [TestCase("Apr", ExpectedResult = true)]
        [TestCase("May", ExpectedResult = true)]
        [TestCase("Jun", ExpectedResult = true)]
        [TestCase("Jul", ExpectedResult = true)]
        [TestCase("Aug", ExpectedResult = true)]
        [TestCase("Sep", ExpectedResult = true)]
        [TestCase("Oct", ExpectedResult = true)]
        [TestCase("Nov", ExpectedResult = true)]
        [TestCase("Dec", ExpectedResult = true)]
        [TestCase("test", ExpectedResult = false)]
        public bool monthIsValid_Test(string month)
        {
            // arrange
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var fakeView = A.Fake<IMainWindow>();
            A.CallTo(() => fakeView.month).Returns(month);
            MainPresenter presenter = new MainPresenter(fakeView, fakeRepo);

            // act
            bool result = presenter.monthIsValid();

            // assert
            return result;
        }
    }
}
