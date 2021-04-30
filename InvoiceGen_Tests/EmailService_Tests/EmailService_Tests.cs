using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using NUnit;
using NUnit.Framework;
using InvoiceGen;
using InvoiceGen.Models;
using InvoiceGen.Models.ObjectModel;
using InvoiceGen.Models.Repository;
using InvoiceGen.View;
using InvoiceGen.Presenter;
using FakeItEasy;

namespace InvoiceGen_Tests.EmailService_Tests
{
    [TestFixture]
    public class EmailService_Tests
    {
        [TestCase("email@email.com", "email@email.com", "email@email.com", "")]
        [TestCase("email@email.com", "email@email.com", "", "")]
        [TestCase("email@email.com", "", "", "")]
        [TestCase("", "", "", "")]
        [TestCase("", "email@email.com", "email@email.com", "email@email.com")]
        [TestCase("", "", "email@email.com", "email@email.com")]
        [TestCase("", "", "", "email@email.com")]
        public void EmailService_Test_EmptyAddressFieldOrFields(string fromAddress, string toAddress, string ccAddress, string bccAddress)
        {
            // arrange
            SecureString pwd = new SecureString();

            // act/assert
            EmailService emailService; 
            Assert.Throws<ArgumentNullException>(() => emailService = new EmailService(pwd, fromAddress, toAddress, ccAddress, bccAddress));
        }

        [TestCase("email@email.com", "email@email.com", "email@email.com", "email@email.com")]
        [TestCase("email@email.com", "email@email.com", "email@email.com", "")]
        [TestCase("email@email.com", "email@email.com", "", "")]
        [TestCase("email@email.com", "", "", "")]
        [TestCase("", "", "", "")]
        [TestCase("", "email@email.com", "email@email.com", "email@email.com")]
        [TestCase("", "", "email@email.com", "email@email.com")]
        [TestCase("", "", "", "email@email.com")]
        public void EmailService_Test_EmptyPwdField(string fromAddress, string toAddress, string ccAddress, string bccAddress)
        {
            // arrange
            SecureString pwd = null;

            // act/assert
            EmailService emailService;
            Assert.Throws<ArgumentNullException>(() => emailService = new EmailService(pwd, fromAddress, toAddress, ccAddress, bccAddress));
        }
    }
}
