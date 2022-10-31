using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using NUnit;
using NUnit.Framework;
using InvoiceGen.Models;
using InvoiceGen.Views;
using InvoiceGen.Presenters;
using FakeItEasy;

namespace InvoiceGen_Tests.Presenter_Tests
{
    [TestFixture]
    public class ConfigWindowPresenter_Tests
    {
        [TestCase("", "John Smith", "host", "email@example.com", "Jane Doe", true, false, false, false, false)]
        [TestCase("", "", "host", "email@example.com", "Jane Doe", true, true, false, false, false)]
        [TestCase("", "", "", "email@example.com", "Jane Doe", true, true, true, false, false)]
        [TestCase("", "", "", "", "Jane Doe", true, true, true, true, false)]
        [TestCase("", "", "", "", "", true, true, true, true, true)]
        [TestCase("email@example.com", "", "", "", "", false, true, true, true, true)]
        [TestCase("email@example.com", "John Smith", "", "", "", false, false, true, true, true)]
        [TestCase("email@example.com", "John Smith", "host", "", "", false, false, false, true, true)]
        [TestCase("email@example.com", "John Smith", "host", "email@example.com", "", false, false, false, false, true)]
        public void InputFieldChanged_Test_AtLeastOneInputInvalid(string senderAddress, string senderName, string host, string recipientAddress, string recipientName,
            bool senderAddressFieldRed, bool senderNameFieldRed, bool hostFieldRed, bool recipientAddressFieldRed, bool recipientNameFieldRed)
        {
            // arrange
            var fakeView = A.Fake<IConfigWindow>();
            A.CallTo(() => fakeView.SenderAddress).Returns(senderAddress);
            A.CallTo(() => fakeView.SenderName).Returns(senderName);
            A.CallTo(() => fakeView.Host).Returns(host);
            A.CallTo(() => fakeView.Port).Returns(587);
            A.CallTo(() => fakeView.RecipientAddress).Returns(recipientAddress);
            A.CallTo(() => fakeView.RecipientName).Returns(recipientName);
            var fakeModel = A.Fake<IEmailModel>();
            A.CallTo(() => fakeModel.IsValidEmail("email@example.com")).Returns(true);
            A.CallTo(() => fakeModel.IsValidEmail("")).Returns(false);
            A.CallTo(() => fakeModel.InvalidInputColour).Returns(System.Drawing.Color.Salmon);
            ConfigWindowPresenter presenter = new ConfigWindowPresenter(fakeView, fakeModel);

            // act
            presenter.InputFieldChanged(null, null);

            // assert
            Assert.IsFalse(fakeView.SaveButtonEnabled);
            Assert.AreEqual(senderAddressFieldRed, fakeView.SenderAddressFieldColour==System.Drawing.Color.Salmon);
            Assert.AreEqual(senderNameFieldRed, fakeView.SenderNameFieldColour==System.Drawing.Color.Salmon);
            Assert.AreEqual(hostFieldRed, fakeView.HostFieldColour==System.Drawing.Color.Salmon);
            Assert.AreEqual(recipientAddressFieldRed, fakeView.RecipientAddressFieldColour==System.Drawing.Color.Salmon);
            Assert.AreEqual(recipientNameFieldRed, fakeView.RecipientNameFieldColour==System.Drawing.Color.Salmon);
        }

        [Test]
        public void InputFieldChanged_Test_AllInputsValid()
        {
            // arrange
            var fakeView = A.Fake<IConfigWindow>();
            A.CallTo(() => fakeView.SenderAddress).Returns("email@example.com");
            A.CallTo(() => fakeView.SenderName).Returns("John Smith");
            A.CallTo(() => fakeView.Host).Returns("host");
            A.CallTo(() => fakeView.Port).Returns(587);
            A.CallTo(() => fakeView.RecipientAddress).Returns("email@example.com");
            A.CallTo(() => fakeView.RecipientName).Returns("Jane Doe");
            var fakeModel = A.Fake<IEmailModel>();
            A.CallTo(() => fakeModel.IsValidEmail("email@example.com")).Returns(true);
            A.CallTo(() => fakeModel.InvalidInputColour).Returns(System.Drawing.Color.Salmon);
            ConfigWindowPresenter presenter = new ConfigWindowPresenter(fakeView, fakeModel);

            // act
            presenter.InputFieldChanged(null, null);

            // assert
            Assert.IsTrue(fakeView.SaveButtonEnabled);
            Assert.AreEqual(System.Drawing.Color.Empty, fakeView.SenderAddressFieldColour);
            Assert.AreEqual(System.Drawing.Color.Empty, fakeView.SenderNameFieldColour);
            Assert.AreEqual(System.Drawing.Color.Empty, fakeView.HostFieldColour);
            Assert.AreEqual(System.Drawing.Color.Empty, fakeView.RecipientAddressFieldColour);
            Assert.AreEqual(System.Drawing.Color.Empty, fakeView.RecipientNameFieldColour);
        }
    }
}