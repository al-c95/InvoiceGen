using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using InvoiceGen.Model.ObjectModel;
using InvoiceGen.Model.Repository;
using InvoiceGen.View;
using InvoiceGen.Presenter;
using FakeItEasy;

namespace InvoiceGen_Tests.Presenter_Tests
{
    [TestFixture]
    public class MainPresenter_Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CancelButtonClicked_Test()
        {
            // arrange
            var fakeView = A.Fake<IMainWindow>();
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var presenter = new MainPresenter(fakeView, fakeRepo);
            presenter.NewInvoiceButtonClicked(null, null);

            // act
            presenter.CancelButtonClicked(null, null);

            // assert
            Assert.IsTrue(fakeView.NewInvoiceButtonEnabled);
            Assert.IsFalse(fakeView.SaveAndEmailButtonEnabled);
            Assert.IsFalse(fakeView.CancelButtonEnabled);
            Assert.IsFalse(fakeView.RadioButtonMonthlyEnabled);
            Assert.IsFalse(fakeView.RadioButtonCustomEnabled);
            Assert.IsFalse(fakeView.MonthComboboxEnabled);
            Assert.IsFalse(fakeView.YearTextBoxEnabled);
            Assert.IsFalse(fakeView.CustomTitleTextBoxEnabled);
            Assert.IsFalse(fakeView.ItemDescriptionTextBoxEnabled);
            Assert.IsFalse(fakeView.ItemAmountTextBoxEnabled);
            Assert.IsFalse(fakeView.ItemQuantityUpDownEnabled);
            Assert.IsFalse(fakeView.AddItemButtonEnabled);
            Assert.IsFalse(fakeView.ItemsListViewEnabled);
            Assert.IsFalse(fakeView.DuplicateItemButtonEnabled);
            Assert.IsFalse(fakeView.RemoveItemButtonEnabled);
            Assert.AreEqual("", fakeView.CustomTitleText);
            Assert.AreEqual("", fakeView.ItemDescription);
            Assert.AreEqual("", fakeView.ItemAmount);
            Assert.AreEqual("", fakeView.Year);
            Assert.AreEqual("", fakeView.Month);
            Assert.AreEqual(1, fakeView.ItemQuantity);
            Assert.AreEqual("0.00", fakeView.TotalText);
        }

        [Test]
        public void NewInvoiceButtonClicked_Test()
        {
            // arrange
            var fakeView = A.Fake<IMainWindow>();
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var presenter = new MainPresenter(fakeView, fakeRepo);

            // act
            presenter.NewInvoiceButtonClicked(null, null);

            // assert
            Assert.IsTrue(fakeView.RadioButtonMonthlyChecked);
            Assert.IsTrue(fakeView.RadioButtonMonthlyEnabled);
            Assert.IsTrue(fakeView.RadioButtonCustomEnabled);
            Assert.IsTrue(fakeView.CancelButtonEnabled);
            Assert.IsFalse(fakeView.NewInvoiceButtonEnabled);
            Assert.IsTrue(fakeView.MonthComboboxEnabled);
            Assert.IsTrue(fakeView.YearTextBoxEnabled);
        }

        [Test]
        public void InvoiceTypeSelected_Test_Monthly()
        {
            // arrange
            var fakeView = A.Fake<IMainWindow>();
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var presenter = new MainPresenter(fakeView, fakeRepo);
            presenter.NewInvoiceButtonClicked(null, null);
            A.CallTo(() => fakeView.RadioButtonMonthlyChecked).Returns(true);
            A.CallTo(() => fakeView.RadioButtonCustomChecked).Returns(false);

            // act
            presenter.InvoiceTypeSelected(null, null);

            // assert
            Assert.IsTrue(fakeView.MonthComboboxEnabled);
            Assert.IsTrue(fakeView.YearTextBoxEnabled);
            Assert.IsFalse(fakeView.CustomTitleTextBoxEnabled);
        }
     
        [Test]
        public void InvoiceTypeSelected_Test_Custom()
        {
            // arrange
            var fakeView = A.Fake<IMainWindow>();
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var presenter = new MainPresenter(fakeView, fakeRepo);
            presenter.NewInvoiceButtonClicked(null, null);
            A.CallTo(() => fakeView.RadioButtonMonthlyChecked).Returns(false);
            A.CallTo(() => fakeView.RadioButtonCustomChecked).Returns(true);

            // act
            presenter.InvoiceTypeSelected(null, null);

            // assert
            Assert.IsFalse(fakeView.MonthComboboxEnabled);
            Assert.IsFalse(fakeView.YearTextBoxEnabled);
            Assert.IsTrue(fakeView.CustomTitleTextBoxEnabled);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void CustomTitleTextChanged_Test_NullOrWhiteSpace(string text)
        {
            // arrange
            var fakeView = A.Fake<IMainWindow>();
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var presenter = new MainPresenter(fakeView, fakeRepo);
            presenter.NewInvoiceButtonClicked(null, null);
            A.CallTo(() => fakeView.RadioButtonMonthlyChecked).Returns(false);
            A.CallTo(() => fakeView.RadioButtonCustomChecked).Returns(true);
            A.CallTo(() => fakeView.CustomTitleText).Returns("");

            // act
            presenter.CustomTitleTextChanged(null, null);

            // assert
            Assert.IsFalse(fakeView.ItemDescriptionTextBoxEnabled);
            Assert.IsFalse(fakeView.ItemAmountTextBoxEnabled);
            Assert.IsFalse(fakeView.ItemQuantityUpDownEnabled);
        }

        [Test]
        public void CustomTitleTextChanged_Test_TitleEntered()
        {
            // arrange
            var fakeView = A.Fake<IMainWindow>();
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var presenter = new MainPresenter(fakeView, fakeRepo);
            presenter.NewInvoiceButtonClicked(null, null);
            A.CallTo(() => fakeView.RadioButtonMonthlyChecked).Returns(false);
            A.CallTo(() => fakeView.RadioButtonCustomChecked).Returns(true);
            A.CallTo(() => fakeView.CustomTitleText).Returns("My invoice");

            // act
            presenter.CustomTitleTextChanged(null, null);

            // assert
            Assert.IsTrue(fakeView.ItemDescriptionTextBoxEnabled);
            Assert.IsTrue(fakeView.ItemAmountTextBoxEnabled);
            Assert.IsTrue(fakeView.ItemQuantityUpDownEnabled);
        }

        [TestCase("3.50")]
        [TestCase("3.501")]
        public void NewItemDetailsUpdated_Test_ValidEntries(string amountEntered)
        {
            // arrange
            var fakeView = A.Fake<IMainWindow>();
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var presenter = new MainPresenter(fakeView, fakeRepo);
            presenter.NewInvoiceButtonClicked(null, null);
            A.CallTo(() => fakeView.CustomTitleText).Returns("My invoice");
            A.CallTo(() => fakeView.ItemDescription).Returns("Item");
            A.CallTo(() => fakeView.ItemAmount).Returns(amountEntered);
            A.CallTo(() => fakeView.ItemQuantity).Returns(1);

            // act
            presenter.NewItemDetailsUpdated(null, null);

            // assert
            Assert.IsTrue(fakeView.AddItemButtonEnabled);
            Assert.IsTrue(fakeView.ItemsListViewEnabled);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void NewItemDetailsUpdated_Test_EmptyDescriptionValidAmountValidQuantity(string descriptionEntered)
        {
            // arrange
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var fakeView = A.Fake<IMainWindow>();
            var presenter = new MainPresenter(fakeView, fakeRepo);
            presenter.NewInvoiceButtonClicked(null, null);
            A.CallTo(() => fakeView.CustomTitleText).Returns("My invoice");
            A.CallTo(() => fakeView.ItemDescription).Returns(descriptionEntered);
            A.CallTo(() => fakeView.ItemAmount).Returns("2.5");
            A.CallTo(() => fakeView.ItemQuantity).Returns(1);

            // act
            presenter.NewItemDetailsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.AddItemButtonEnabled);
            Assert.IsFalse(fakeView.ItemsListViewEnabled);
        }

        [TestCase("")]
        [TestCase(" ")]
        public void NewItemDetailsUpdated_Test_EmptyAmountValidDescriptionValidQuantity(string amountEntered)
        {
            // arrange
            var fakeView = A.Fake<IMainWindow>();
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var presenter = new MainPresenter(fakeView, fakeRepo);
            presenter.NewInvoiceButtonClicked(null, null);
            A.CallTo(() => fakeView.CustomTitleText).Returns("My invoice");
            A.CallTo(() => fakeView.ItemDescription).Returns("Item");
            A.CallTo(() => fakeView.ItemAmount).Returns(amountEntered);
            A.CallTo(() => fakeView.ItemQuantity).Returns(1);

            // act
            presenter.NewItemDetailsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.AddItemButtonEnabled);
            Assert.IsFalse(fakeView.ItemsListViewEnabled);
        }

        [TestCase("", "")]
        [TestCase("", " ")]
        [TestCase("", null)]
        [TestCase(" ", "")]
        [TestCase(" ", " ")]
        [TestCase(" ", null)]
        public void NewItemDetailsUpdated_Test_EmptyDescriptionAndEmptyAmountValidQuantity(string descriptionEntered, string amountEntered)
        {
            // arrange
            var fakeView = A.Fake<IMainWindow>();
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var presenter = new MainPresenter(fakeView, fakeRepo);
            presenter.NewInvoiceButtonClicked(null, null);
            A.CallTo(() => fakeView.CustomTitleText).Returns("My invoice");
            A.CallTo(() => fakeView.ItemDescription).Returns(descriptionEntered);
            A.CallTo(() => fakeView.ItemAmount).Returns(amountEntered);
            A.CallTo(() => fakeView.ItemQuantity).Returns(1);

            // act
            presenter.NewItemDetailsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.AddItemButtonEnabled);
            Assert.IsFalse(fakeView.ItemsListViewEnabled);
        }

        [Test]
        public void NewItemDetailsUpdated_Test_ValidDescriptionValidAmountInvalidQuantity()
        {
            // arrange
            var fakeView = A.Fake<IMainWindow>();
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var presenter = new MainPresenter(fakeView, fakeRepo);
            presenter.NewInvoiceButtonClicked(null, null);
            A.CallTo(() => fakeView.CustomTitleText).Returns("My invoice");
            A.CallTo(() => fakeView.ItemDescription).Returns("Item");
            A.CallTo(() => fakeView.ItemAmount).Returns("2.50");
            A.CallTo(() => fakeView.ItemQuantity).Returns(0);

            // act
            presenter.NewItemDetailsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.AddItemButtonEnabled);
            Assert.IsFalse(fakeView.ItemsListViewEnabled);
        }

        [TestCase("January")]
        [TestCase("February")]
        [TestCase("March")]
        [TestCase("April")]
        [TestCase("May")]
        [TestCase("June")]
        [TestCase("July")]
        [TestCase("August")]
        [TestCase("September")]
        [TestCase("October")]
        [TestCase("November")]
        [TestCase("December")]
        public void MonthlyInvoiceMonthYearUpdated_Test_ValidMonthValidYearEmptyItemsList(string monthEntered)
        {
            // arrange
            var fakeView = A.Fake<IMainWindow>();
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var presenter = new MainPresenter(fakeView, fakeRepo);
            presenter.NewInvoiceButtonClicked(null, null);
            A.CallTo(() => fakeView.RadioButtonMonthlyChecked).Returns(true);
            A.CallTo(() => fakeView.RadioButtonCustomChecked).Returns(false);
            A.CallTo(() => fakeView.Month).Returns(monthEntered);
            A.CallTo(() => fakeView.Year).Returns("2021");
            A.CallTo(() => fakeView.GetNumberOfItemsInList()).Returns(0);

            // act
            presenter.MonthlyInvoiceMonthYearUpdated(null, null);

            // assert
            Assert.IsTrue(fakeView.ItemDescriptionTextBoxEnabled);
            Assert.IsTrue(fakeView.ItemAmountTextBoxEnabled);
            Assert.IsTrue(fakeView.ItemQuantityUpDownEnabled);
            Assert.IsFalse(fakeView.SaveAndEmailButtonEnabled);
            Assert.IsFalse(fakeView.SaveAndExportXLButtonEnabled);
        }

        [TestCase(0, false)]
        [TestCase(1, false)]
        public void MonthlyInvoiceMonthYearUpdated_Test_InvalidMonthValidYear(int numberOfItemsInList,
            bool expectedSaveButtonsEnabled)
        {
            // arrange
            var fakeView = A.Fake<IMainWindow>();
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var presenter = new MainPresenter(fakeView, fakeRepo);
            presenter.NewInvoiceButtonClicked(null, null);
            A.CallTo(() => fakeView.RadioButtonMonthlyChecked).Returns(true);
            A.CallTo(() => fakeView.RadioButtonCustomChecked).Returns(false);
            A.CallTo(() => fakeView.Month).Returns("bla");
            A.CallTo(() => fakeView.Year).Returns("2021");
            A.CallTo(() => fakeView.GetNumberOfItemsInList()).Returns(numberOfItemsInList);

            // act
            presenter.MonthlyInvoiceMonthYearUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.ItemDescriptionTextBoxEnabled);
            Assert.IsFalse(fakeView.ItemAmountTextBoxEnabled);
            Assert.IsFalse(fakeView.ItemQuantityUpDownEnabled);
            Assert.AreEqual(expectedSaveButtonsEnabled, fakeView.SaveAndEmailButtonEnabled);
            Assert.AreEqual(expectedSaveButtonsEnabled, fakeView.SaveAndExportXLButtonEnabled);
        }

        [TestCase(0, false)]
        [TestCase(1, false)]
        public void MonthlyInvoiceMonthYearUpdated_Test_InvalidMonthInvalidYear(int numberOfItemsInList,
            bool expectedSaveButtonsEnabled)
        {
            // arrange
            var fakeView = A.Fake<IMainWindow>();
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var presenter = new MainPresenter(fakeView, fakeRepo);
            presenter.NewInvoiceButtonClicked(null, null);
            A.CallTo(() => fakeView.RadioButtonMonthlyChecked).Returns(true);
            A.CallTo(() => fakeView.RadioButtonCustomChecked).Returns(false);
            A.CallTo(() => fakeView.Month).Returns("bla");
            A.CallTo(() => fakeView.Year).Returns("2021!");
            A.CallTo(() => fakeView.GetNumberOfItemsInList()).Returns(numberOfItemsInList);

            // act
            presenter.MonthlyInvoiceMonthYearUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.ItemDescriptionTextBoxEnabled);
            Assert.IsFalse(fakeView.ItemAmountTextBoxEnabled);
            Assert.IsFalse(fakeView.ItemQuantityUpDownEnabled);
            Assert.AreEqual(expectedSaveButtonsEnabled, fakeView.SaveAndEmailButtonEnabled);
            Assert.AreEqual(expectedSaveButtonsEnabled, fakeView.SaveAndExportXLButtonEnabled);
        }

        [TestCase(0, false)]
        [TestCase(1, false)]
        public void MonthlyInvoiceMonthYearUpdated_Test_ValidMonthInvalidYear(int numberOfItemsInList,
            bool expectedSaveButtonsEnabled)
        {
            // arrange
            var fakeView = A.Fake<IMainWindow>();
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var presenter = new MainPresenter(fakeView, fakeRepo);
            presenter.NewInvoiceButtonClicked(null, null);
            A.CallTo(() => fakeView.RadioButtonMonthlyChecked).Returns(true);
            A.CallTo(() => fakeView.RadioButtonCustomChecked).Returns(false);
            A.CallTo(() => fakeView.Month).Returns("bla");
            A.CallTo(() => fakeView.Year).Returns("2021");
            A.CallTo(() => fakeView.GetNumberOfItemsInList()).Returns(numberOfItemsInList);

            // act
            presenter.MonthlyInvoiceMonthYearUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.ItemDescriptionTextBoxEnabled);
            Assert.IsFalse(fakeView.ItemAmountTextBoxEnabled);
            Assert.IsFalse(fakeView.ItemQuantityUpDownEnabled);
            Assert.AreEqual(expectedSaveButtonsEnabled, fakeView.SaveAndEmailButtonEnabled);
            Assert.AreEqual(expectedSaveButtonsEnabled, fakeView.SaveAndExportXLButtonEnabled);
        }
        
        // TODO: fix this test (associated functionality works...)
        [Test]
        public void AddItemButtonClicked_Test_ItemDoesNotYetExistInList()
        {
            // arrange
            var fakeView = A.Fake<IMainWindow>();
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var presenter = new MainPresenter(fakeView, fakeRepo);
            presenter.NewInvoiceButtonClicked(null, null);
            A.CallTo(() => fakeView.RadioButtonCustomChecked).Returns(true);
            A.CallTo(() => fakeView.RadioButtonMonthlyChecked).Returns(false);
            InvoiceItem newItem = new InvoiceItem { Amount = 2.50M, Description = "Item"};
            int quantity = 1;
            A.CallTo(() => fakeView.ItemDescription).Returns("Item");
            A.CallTo(() => fakeView.ItemQuantity).Returns(1);
            A.CallTo(() => fakeView.ItemAmount).Returns(newItem.Amount.ToString("0.00"));

            // act
            presenter.AddItemButtonClicked(null, null);

            // assert
            Assert.IsTrue(fakeView.SaveAndExportXLButtonEnabled);
            Assert.IsTrue(fakeView.SaveAndEmailButtonEnabled);
            Assert.IsTrue(fakeView.GetQuantityOfItemInList(new InvoiceItem { Amount = 2.5M, Description = "Item"}) == 1);
            Assert.AreEqual(@"Total: $2.50", fakeView.TotalText);
        }

        //[Test]
        public void AddItemButtonClicked_Test_ItemAlreadyExistsInList()
        {
            var fakeView = A.Fake<IMainWindow>();
            var presenter = new MainPresenter(fakeView, null);
            // arrange

            // act

            // assert
        }

        [Test]
        public void ItemListSelectedIndexChanged_Test_NotSelected()
        {
            // arrange
            var fakeView = A.Fake<IMainWindow>();
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var presenter = new MainPresenter(fakeView, fakeRepo);
            A.CallTo(() => fakeView.GetSelectedItem()).Returns(null);
            InvoiceItem item1 = new InvoiceItem { Description = "Item 1", Amount = 2.50M };
            InvoiceItem item2 = new InvoiceItem { Description = "Item 2", Amount = 1.00M };
            List<Tuple<InvoiceItem, int>> listItems = new List<Tuple<InvoiceItem, int>>() { Tuple.Create(item1, 2), Tuple.Create(item2, 1) };
            A.CallTo(() => fakeView.ItemsListEntries).Returns(listItems);

            // act
            presenter.ItemListSelectedIndexChanged(null, null);

            // assert
            Assert.IsFalse(fakeView.DuplicateItemButtonEnabled);
            Assert.IsFalse(fakeView.RemoveItemButtonEnabled);
            Assert.AreEqual(@"Total: $6.00", fakeView.TotalText);
        }

        [TestCase(1, @"2.50")]
        [TestCase(2, @"5.00")]
        public void ItemListSelectedIndexChanged_Test_Selected(int quantity, string expectedTotalDisplayed)
        {
            // arrange
            var fakeView = A.Fake<IMainWindow>();
            var fakeRepo = A.Fake<IInvoiceRepository>();
            var presenter = new MainPresenter(fakeView, fakeRepo);
            A.CallTo(() => fakeView.GetSelectedItem()).Returns(Tuple.Create(new InvoiceItem { Description="Item", Amount=2.5M }, 1));

            // act
            presenter.ItemListSelectedIndexChanged(null, null);

            // assert
            Assert.IsTrue(fakeView.DuplicateItemButtonEnabled);
            Assert.IsTrue(fakeView.RemoveItemButtonEnabled);
            Assert.AreEqual(@"$2.50", fakeView.TotalText);
        }

        [Test]
        public void PaidStatusChanged_Test()
        {
            // arrange
            var fakeView = A.Fake<IMainWindow>();
            var fakeRepo = A.Fake<IInvoiceRepository>();
            fakeView.UpdateRecordsButtonEnabled = false;
            var presenter = new MainPresenter(fakeView, fakeRepo);

            // act
            presenter.PaidStatusChanged(null, null);

            // assert
            Assert.IsTrue(fakeView.UpdateRecordsButtonEnabled);
        }

        // TODO
        //[Test]
        public void DuplicateItemButtonClicked_Test()
        {

        }

        // TODO
        //[Test]
        public void RemoveItemButtonClicked_Test()
        {

        }

        // TODO
        //[Test]
        public void UpdateRecordsButtonClicked_Test()
        {

        }
    }
}
