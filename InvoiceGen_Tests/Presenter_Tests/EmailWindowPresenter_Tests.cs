using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using NUnit;
using NUnit.Framework;
using InvoiceGen.Models;
using InvoiceGen.View;
using InvoiceGen.Presenters;
using static InvoiceGen.Utils;
using FakeItEasy;

namespace InvoiceGen_Tests.Presenter_Tests
{
    [TestFixture]
    public class EmailWindowPresenter_Tests
    {
        [TestCase("")]
        [TestCase(null)]
        public void InputFieldTextChanged_Test_PasswordEntryInvalidOthersValid(string password)
        {
            // arrange
            var fakeView = A.Fake<IEmailWindow>();
            A.CallTo(() => fakeView.To).Returns("email@email.com");
            A.CallTo(() => fakeView.Cc).Returns("email@email.com");
            A.CallTo(() => fakeView.Bcc).Returns("email@email.com");
            A.CallTo(() => fakeView.Password).Returns(ConvertToSecureString(password));
            var fakeModel = A.Fake<IEmailModel>();
            A.CallTo(() => fakeModel.IsValidEmail("email@email.com")).Returns(true);
            A.CallTo(() => fakeModel.InvalidInputColour).Returns(System.Drawing.Color.Salmon);
            EmailWindowPresenter presenter = new EmailWindowPresenter(fakeView, fakeModel);

            // act
            presenter.InputFieldTextChanged(null, null);

            // assert
            Assert.IsFalse(fakeView.SaveAndSendButtonEnabled);
            Assert.AreEqual(System.Drawing.Color.Salmon, fakeView.PwdFieldColour);
            Assert.AreEqual(System.Drawing.Color.Empty, fakeView.ToFieldColour);
            Assert.AreEqual(System.Drawing.Color.Empty, fakeView.CcFieldColour);
            Assert.AreEqual(System.Drawing.Color.Empty, fakeView.BccFieldColour);
        }

        [TestCase("", "email@email.com", "email@email.com", true, false, false)]
        [TestCase("", "", "email@email.com", true, false, false)]
        [TestCase("", "", "", true, false, false)]
        public void InputFieldTextChanged_Test_PasswordEntryValidOthersInvalid(string to, string cc, string bcc,
            bool toFieldRed, bool ccFieldRed, bool bccFieldRed)
        {
            // arrange
            var fakeView = A.Fake<IEmailWindow>();
            A.CallTo(() => fakeView.To).Returns(to);
            A.CallTo(() => fakeView.Cc).Returns(cc);
            A.CallTo(() => fakeView.Bcc).Returns(bcc);
            A.CallTo(() => fakeView.Password).Returns(ConvertToSecureString("password"));
            var fakeModel = A.Fake<IEmailModel>();
            A.CallTo(() => fakeModel.IsValidEmail("email@email.com")).Returns(true);
            A.CallTo(() => fakeModel.IsValidEmail("")).Returns(false);
            A.CallTo(() => fakeModel.InvalidInputColour).Returns(System.Drawing.Color.Salmon);
            EmailWindowPresenter presenter = new EmailWindowPresenter(fakeView, fakeModel);

            // act
            presenter.InputFieldTextChanged(null, null);

            // assert
            Assert.IsFalse(fakeView.SaveAndSendButtonEnabled);
            Assert.IsTrue(fakeView.PwdFieldColour == System.Drawing.Color.Empty);
            Assert.AreEqual(toFieldRed, fakeView.ToFieldColour == System.Drawing.Color.Salmon);
            Assert.AreEqual(ccFieldRed, fakeView.CcFieldColour == System.Drawing.Color.Salmon);
            Assert.AreEqual(bccFieldRed, fakeView.BccFieldColour == System.Drawing.Color.Salmon);
        }

        [Test]
        public void InputFieldTextChanged_Test_AllEntriesValid()
        {
            // arrange
            var fakeView = A.Fake<IEmailWindow>();
            A.CallTo(() => fakeView.To).Returns("email@email.com");
            A.CallTo(() => fakeView.Cc).Returns("email@email.com");
            A.CallTo(() => fakeView.Bcc).Returns("email@email.com");
            A.CallTo(() => fakeView.Password).Returns(ConvertToSecureString("password"));
            var fakeModel = A.Fake<IEmailModel>();
            A.CallTo(() => fakeModel.IsValidEmail("email@email.com")).Returns(true);
            A.CallTo(() => fakeModel.InvalidInputColour).Returns(System.Drawing.Color.Salmon);
            EmailWindowPresenter presenter = new EmailWindowPresenter(fakeView, fakeModel);

            // act
            presenter.InputFieldTextChanged(null, null);

            // assert
            Assert.IsTrue(fakeView.SaveAndSendButtonEnabled);
            Assert.AreEqual(System.Drawing.Color.Empty, fakeView.PwdFieldColour);
            Assert.AreEqual(System.Drawing.Color.Empty, fakeView.ToFieldColour);
            Assert.AreEqual(System.Drawing.Color.Empty, fakeView.CcFieldColour);
            Assert.AreEqual(System.Drawing.Color.Empty, fakeView.BccFieldColour);
        }
    }
}
