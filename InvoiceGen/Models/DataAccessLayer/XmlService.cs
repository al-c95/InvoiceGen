using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using InvoiceGen.Models.ObjectModel;

namespace InvoiceGen.Models.DataAccessLayer
{
    /// <summary>
    /// Manipulates the XML with the data.
    /// </summary>
    public class XmlService : IXmlService
    {
        private IXmlFileHandler _fileHandler; // external interface (XML file)
        private string _dateFormat;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="fileHandler"></param>
        public XmlService(IXmlFileHandler fileHandler, string dateFormat)
        {
            this._fileHandler = fileHandler;
            this._dateFormat = dateFormat;
        }

        /// <summary>
        /// Inserts appropriate nodes in the XML to represent a new invoice.
        /// </summary>
        /// <param name="invoice"></param>
        public void InsertInvoiceInXml(Invoice invoice)
        {
            if (invoice.Items.Count == 0)
                throw new ArgumentException("No items in invoice.");

            // retrieve the XML and find the new invoice ID
            string xml = this._fileHandler.GetXML();
            int maxID = 0;
            foreach (Invoice i in ReadXml())
                if (i.Id > maxID)
                    maxID = i.Id;

            // insert the new invoice data in the XMl
            XDocument doc = XDocument.Parse(xml);
            XElement invoiceElement = new XElement(Invoice.XmlName);
            invoiceElement.SetAttributeValue("id", maxID + 1);
            invoiceElement.SetAttributeValue("title", invoice.Title);
            invoiceElement.SetAttributeValue("timestamp", invoice.Timestamp.ToString(_dateFormat,
                System.Globalization.CultureInfo.InvariantCulture));
            invoiceElement.SetAttributeValue("paid", invoice.Paid);
            XElement itemsElement = new XElement("items");
            foreach (InvoiceItem item in invoice.Items)
            {
                XElement invoiceItemElement = new XElement(InvoiceItem.XmlName);
                invoiceItemElement.SetAttributeValue("desc", item.Description);
                invoiceItemElement.SetAttributeValue("amount", item.Amount);

                itemsElement.Add(invoiceItemElement);
            }
            invoiceElement.Add(itemsElement);
            doc.Root.Add(invoiceElement);

            // save the XML to the file
            this._fileHandler.SaveXMLFile(doc);
        }

        /// <summary>
        /// Updates paid status (boolean) by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paid"></param>
        public void UpdatePaidStatusInXml(int id, bool paid)
        {
            // TODO: unit test

            string xml = this._fileHandler.GetXML();

            XDocument doc = XDocument.Parse(xml);
            doc.Root.Elements(Invoice.XmlName).Where(e => int.Parse((e.Attribute("id").Value)) == id).First().SetAttributeValue("paid", paid);

            this._fileHandler.SaveXMLFile(doc);
        }

        /// <summary>
        /// Deletes an invoice by ID.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteInvoiceInXml(int id)
        {
            // TODO: unit test

            string xml = this._fileHandler.GetXML();

            XDocument doc = XDocument.Parse(xml);
            doc.Root.Elements(Invoice.XmlName).Where(e => int.Parse((e.Attribute("id").Value)) == id).Remove();

            this._fileHandler.SaveXMLFile(doc);
        }

        /// <summary>
        /// Get all the objects represented by the XML file.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Invoice> ReadXml()
        {
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.IgnoreWhitespace = true;
            xmlReaderSettings.IgnoreComments = true;
            xmlReaderSettings.IgnoreProcessingInstructions = true;

            using (XmlReader reader = XmlReader.Create(new System.IO.StringReader(this._fileHandler.GetXML()), xmlReaderSettings))
            {
                while (reader.ReadToFollowing(Invoice.XmlName))
                {
                    Invoice invoice = new Invoice();

                    if (reader.MoveToAttribute("id"))
                        invoice.Id = reader.ReadContentAsInt();

                    if (reader.MoveToAttribute("title"))
                        invoice.Title = reader.ReadContentAsString();
 
                    if (reader.MoveToAttribute("timestamp"))
                        invoice.Timestamp = DateTime.ParseExact(reader.ReadContentAsString(),
                                _dateFormat, System.Globalization.CultureInfo.InvariantCulture); 

                    if (reader.MoveToAttribute("paid"))
                    {
                        invoice.Paid = reader.ReadContentAsBoolean();
                    }

                    if (reader.ReadToFollowing(InvoiceItem.XmlName))
                    {
                        do
                        {
                            InvoiceItem item = new InvoiceItem();

                            if (reader.MoveToAttribute("desc"))
                                item.Description = reader.ReadContentAsString();

                            if (reader.MoveToAttribute("amount"))
                                item.Amount = reader.ReadContentAsDecimal();

                            invoice.Items.Add(item);

                        } while (reader.ReadToNextSibling(InvoiceItem.XmlName));
                    }

                    yield return invoice;
                }//while
            }//using
        }//readXml
    }
}