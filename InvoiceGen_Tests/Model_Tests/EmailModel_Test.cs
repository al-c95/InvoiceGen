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
    public class EmailModel_Tests
    {
        protected EmailModel emailModel;

        [SetUp]
        public void Setup()
        {
            // arrange
            this.emailModel = new EmailModel(System.Drawing.Color.Salmon);
        }

        [TestCase("")]
        [TestCase(null)]
        public void IsValidEmail_Test_Empty(string email)
        {
            // act
            bool result = emailModel.IsValidEmail(email);

            // assert
            Assert.IsFalse(result);
        }

        [TestCase("email@email.com")]
        [TestCase("email@email")]
        public void IsValidEmail_Test_validEmail(string email)
        {
            // act
            bool result = emailModel.IsValidEmail(email);

            // assert
            Assert.IsTrue(result);
        }

        [TestCase("email")]
        [TestCase("email@")]
        [TestCase("@email")]
        public void IsValidEmail_Test_invalidEmail(string email)
        {
            // act
            bool result = emailModel.IsValidEmail(email);

            // assert
            Assert.IsFalse(result);
        }
    }
}
