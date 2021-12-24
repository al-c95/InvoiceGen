using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using System.Linq;
using System.Xml.Linq;
using InvoiceGen.Models.DataAccessLayer;
using InvoiceGen.Models.ObjectModel;

namespace InvoiceGen_Tests.Model_Tests.DataAccessLayer_Tests.DataAccessLayer_Tests
{
    [TestFixture]
    class XmlService_Tests
    {
        [Test]
        public void InsertInvoiceInXml_Test()
        {
            #region arrange
            // xml
            StringBuilder testXmlBuilder = new StringBuilder();
            testXmlBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            testXmlBuilder.AppendLine("<invoices>");
            testXmlBuilder.AppendLine(" <invoice id=\"1\" title=\"Aug 2020\" timestamp=\"16/08/2020 09:01:29 AM\" paid=\"true\">");
            testXmlBuilder.AppendLine("     <items>");
            testXmlBuilder.AppendLine("         <item desc=\"item 1 in invoice 1\" amount=\"50\"/>");
            testXmlBuilder.AppendLine("         <item desc=\"item 2 in invoice 1\" amount=\"50\"/>");
            testXmlBuilder.AppendLine("     </items>");
            testXmlBuilder.AppendLine(" </invoice>");
            testXmlBuilder.AppendLine(" <invoice id=\"2\" title=\"Sep 2020\" timestamp=\"16/09/2020 09:01:29 AM\" paid=\"true\">");
            testXmlBuilder.AppendLine("     <items>");
            testXmlBuilder.AppendLine("         <item desc=\"item in invoice 2\" amount=\"100\"/>");
            testXmlBuilder.AppendLine("     </items>");
            testXmlBuilder.AppendLine(" </invoice>");
            testXmlBuilder.AppendLine("</invoices>");
            string testXml = testXmlBuilder.ToString();

            StringBuilder expectedXmlBuilder = new StringBuilder();
            expectedXmlBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            expectedXmlBuilder.AppendLine("<invoices>");
            expectedXmlBuilder.AppendLine(" <invoice id=\"1\" title=\"Aug 2020\" timestamp=\"16/08/2020 09:01:29 AM\" paid=\"true\">");
            expectedXmlBuilder.AppendLine("     <items>");
            expectedXmlBuilder.AppendLine("         <item desc=\"item 1 in invoice 1\" amount=\"50\"/>");
            expectedXmlBuilder.AppendLine("         <item desc=\"item 2 in invoice 1\" amount=\"50\"/>");
            expectedXmlBuilder.AppendLine("     </items>");
            expectedXmlBuilder.AppendLine(" </invoice>");
            expectedXmlBuilder.AppendLine(" <invoice id=\"2\" title=\"Sep 2020\" timestamp=\"16/09/2020 09:01:29 AM\" paid=\"true\">");
            expectedXmlBuilder.AppendLine("     <items>");
            expectedXmlBuilder.AppendLine("         <item desc=\"item in invoice 2\" amount=\"100\"/>");
            expectedXmlBuilder.AppendLine("     </items>");
            expectedXmlBuilder.AppendLine(" </invoice>");
            expectedXmlBuilder.AppendLine(" <invoice id=\"3\" title=\"Oct 2020\" timestamp=\"16/10/2020 09:01:29 AM\" paid=\"false\">");
            expectedXmlBuilder.AppendLine("     <items>");
            expectedXmlBuilder.AppendLine("         <item desc=\"item 1\" amount=\"5.55\"/>");
            expectedXmlBuilder.AppendLine("         <item desc=\"item 2\" amount=\"6.25\"/>");
            expectedXmlBuilder.AppendLine("     </items>");
            expectedXmlBuilder.AppendLine(" </invoice>");
            expectedXmlBuilder.AppendLine("</invoices>");
            string expectedXml = expectedXmlBuilder.ToString();
            XDocument expectedDoc = XDocument.Parse(expectedXml);

            // xml service
            var fakeXmlFileHandler = A.Fake<IXmlFileHandler>();
            A.CallTo(() => fakeXmlFileHandler.GetXML()).Returns(testXml);
            XmlService xmlService = new XmlService(fakeXmlFileHandler, "dd/MM/yyyy hh:mm:ss tt");

            // data
            InvoiceItem item1 = new InvoiceItem();
            item1.Description = "item 1";
            item1.Amount = (decimal)5.55;

            InvoiceItem item2 = new InvoiceItem();
            item2.Description = "item 2";
            item2.Amount = (decimal)6.25;

            Invoice invoice = new Invoice();
            invoice.Id = 3;
            invoice.Title = "Oct 2020";
            invoice.Timestamp = DateTime.ParseExact("16/10/2020 09:01:29 AM", 
                "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
            invoice.Paid = false;
            invoice.Items = new List<InvoiceItem> { item1, item2 };
            #endregion

            #region act
            xmlService.InsertInvoiceInXml(invoice);
            #endregion

            #region assert
            A.CallTo(() => fakeXmlFileHandler.SaveXMLFile(expectedDoc)).MustHaveHappened();
            #endregion
        }

        [Test]
        public void InsertInvoiceInXml_Test_NoItems()
        {
            // arrange
            Invoice invoice = new Invoice();
            var fakeXmlFileHandler = A.Fake<IXmlFileHandler>();
            XmlService xmlService = new XmlService(fakeXmlFileHandler, "dd/MM/yyyy hh:mm:ss tt");

            // act/assert
            Assert.Throws<ArgumentException>(delegate { xmlService.InsertInvoiceInXml(invoice); });
        }

        [Test]
        public void ReadXml_Test()
        {
            #region arrange
            // xml
            StringBuilder testXmlBuilder = new StringBuilder();
            testXmlBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            testXmlBuilder.AppendLine("<invoices>");
            testXmlBuilder.AppendLine(" <invoice id=\"1\" title=\"Aug 2020\" timestamp=\"16/08/2020 09:01:29 AM\" paid=\"true\">");
            testXmlBuilder.AppendLine("     <items>");
            testXmlBuilder.AppendLine("         <item desc=\"item 1 in invoice 1\" amount=\"50\"/>");
            testXmlBuilder.AppendLine("         <item desc=\"item 2 in invoice 1\" amount=\"50\"/>");
            testXmlBuilder.AppendLine("     </items>");
            testXmlBuilder.AppendLine(" </invoice>");
            testXmlBuilder.AppendLine(" <invoice id=\"2\" title=\"Sep 2020\" timestamp=\"16/09/2020 09:01:29 AM\" paid=\"true\">");
            testXmlBuilder.AppendLine("     <items>");
            testXmlBuilder.AppendLine("         <item desc=\"item in invoice 2\" amount=\"100\"/>");
            testXmlBuilder.AppendLine("     </items>");
            testXmlBuilder.AppendLine(" </invoice>");
            testXmlBuilder.AppendLine("</invoices>");
            string testXml = testXmlBuilder.ToString();
            var fakeXmlFileHandler = A.Fake<IXmlFileHandler>();
            A.CallTo(() => fakeXmlFileHandler.GetXML()).Returns(testXml);

            // xml service
            XmlService xmlService = new XmlService(fakeXmlFileHandler, "dd/MM/yyyy hh:mm:ss tt");

            // expected objects
            InvoiceItem item1 = new InvoiceItem { Description = "item 1 in invoice 1", Amount = 50 };
            InvoiceItem item2 = new InvoiceItem { Description = "item 2 in invoice 1", Amount = 50 };
            Invoice invoice1 = new Invoice
            {
                Id = 1,
                Title = "Aug 2020",
                Timestamp = DateTime.ParseExact("16/08/2020 09:01:29 AM", "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture),
                Paid = true,
                Items = new List<InvoiceItem>() { item1, item2 }
            };
            InvoiceItem item3 = new InvoiceItem { Description = "item in invoice 2", Amount = 100 };
            Invoice invoice2 = new Invoice
            {
                Id = 2,
                Title = "Sep 2020",
                Timestamp = DateTime.ParseExact("16/08/2020 09:01:29 AM", "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture),
                Paid = true,
                Items = new List<InvoiceItem> { item3 }
            };
            #endregion

            #region act
            // actual collection of objects
            List<Invoice> actual = xmlService.ReadXml().ToList();
            #endregion

            #region assert
            // TODO: override Equals in the Invoice and InvoiceItem objects
            Assert.That(actual.Any(i => i.Id == invoice1.Id));
            Assert.That(actual.Any(i => i.Paid == invoice1.Paid));
            Assert.That(actual.Any(i => i.Timestamp == invoice1.Timestamp));
            Assert.That(actual.Any(i => i.Title == invoice1.Title));
            Assert.That(actual.Any(i => i.Id == invoice2.Id));
            Assert.That(actual.Any(i => i.Paid == invoice2.Paid));
            Assert.That(actual.Any(i => i.Timestamp == invoice2.Timestamp));
            Assert.That(actual.Any(i => i.Title == invoice2.Title));
            foreach (Invoice i in actual)
            {
                if (i.Id==invoice1.Id)
                {
                    if (i.Items.Any(item => item.Amount == 50))
                    {
                        Assert.Pass();
                    }
                    else
                    {
                        Assert.Fail();
                    }
                }

                if (i.Id == invoice2.Id)
                {
                    if (i.Items.Any(item => item.Amount == 100))
                    {
                        Assert.Pass();
                    }
                    else
                    {
                        Assert.Fail();
                    }
                }
            }
            #endregion
        }
    }
}
