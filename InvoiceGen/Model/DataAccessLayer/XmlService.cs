using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using InvoiceGen.Model.ObjectModel;

namespace InvoiceGen.Model.DataAccessLayer
{
    public interface IXmlService
    {
        void insertInvoiceInXml(Invoice invoice);
        void updatePaidStatusInXml(int id, bool paid);
        void deleteInvoiceInXml(int id);
        IEnumerable<Invoice> readXml();
    }

    /// <summary>
    /// Manipulates the XML with the data.
    /// </summary>
    public class XmlService : IXmlService
    {
        // dependency injection of external interface (XML file) class
        private IXmlFileHandler _fileHandler;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="fileHandler"></param>
        public XmlService(IXmlFileHandler fileHandler)
        {
            this._fileHandler = fileHandler;
        }

        /// <summary>
        /// Inserts appropriate nodes in the XML to represent a new invoice.
        /// </summary>
        /// <param name="invoice"></param>
        public void insertInvoiceInXml(Invoice invoice)
        {
            if (invoice.items.Count == 0)
                throw new ArgumentException("No items in invoice.");

            // retrieve the XML
            string xml = this._fileHandler.getXML();

            // figure out the new invoice ID
            int maxID = 0;
            foreach (Invoice i in readXml())
                if (i.id > maxID)
                    maxID = i.id;

            // insert the new invoice data in the XMl
            XDocument doc = XDocument.Parse(xml);
            XElement invoiceElement = new XElement(Invoice.XmlName);
            invoiceElement.SetAttributeValue("id", maxID + 1);
            invoiceElement.SetAttributeValue("title", invoice.title);
            invoiceElement.SetAttributeValue("timestamp", invoice.timestamp.ToString("dd/MM/yyyy hh:mm:ss tt",
                System.Globalization.CultureInfo.InvariantCulture));
            invoiceElement.SetAttributeValue("paid", invoice.paid);
            XElement itemsElement = new XElement("items");
            foreach (InvoiceItem item in invoice.items)
            {
                XElement invoiceItemElement = new XElement(InvoiceItem.XmlName);
                invoiceItemElement.SetAttributeValue("desc", item.description);
                invoiceItemElement.SetAttributeValue("amount", item.amount);

                itemsElement.Add(invoiceItemElement);
            }
            invoiceElement.Add(itemsElement);
            doc.Root.Add(invoiceElement);

            // save the XML to the file
            this._fileHandler.saveXMLFile(doc);
        }

        /// <summary>
        /// Updates paid status (boolean) by ID.
        /// Updates paid status (boolean) by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paid"></param>
        public void updatePaidStatusInXml(int id, bool paid)
        {
            // TODO: unit test

            string xml = this._fileHandler.getXML();

            XDocument doc = XDocument.Parse(xml);
            doc.Root.Elements(Invoice.XmlName).Where(e => int.Parse((e.Attribute("id").Value)) == id).First().SetAttributeValue("paid", paid);

            this._fileHandler.saveXMLFile(doc);
        }

        /// <summary>
        /// Deletes an invoice by ID.
        /// </summary>
        /// <param name="id"></param>
        public void deleteInvoiceInXml(int id)
        {
            // TODO: unit test

            string xml = this._fileHandler.getXML();

            XDocument doc = XDocument.Parse(xml);
            doc.Root.Elements(Invoice.XmlName).Where(e => int.Parse((e.Attribute("id").Value)) == id).Remove();

            this._fileHandler.saveXMLFile(doc);
        }

        /// <summary>
        /// Get all the objects represented by the XML file.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Invoice> readXml()
        {
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.IgnoreWhitespace = true;
            xmlReaderSettings.IgnoreComments = true;
            xmlReaderSettings.IgnoreProcessingInstructions = true;

            using (XmlReader reader = XmlReader.Create(new System.IO.StringReader(this._fileHandler.getXML()), xmlReaderSettings))
            {
                while (reader.ReadToFollowing(Invoice.XmlName))
                {
                    Invoice invoice = new Invoice();

                    if (reader.MoveToAttribute("id"))
                        invoice.id = reader.ReadContentAsInt();

                    if (reader.MoveToAttribute("title"))
                        invoice.title = reader.ReadContentAsString();

                    if (reader.MoveToAttribute("timestamp"))
                        invoice.timestamp = DateTime.ParseExact(reader.ReadContentAsString(),
                                "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture); // TODO: factor out date format to config

                    if (reader.MoveToAttribute("paid"))
                    {
                        invoice.paid = reader.ReadContentAsBoolean();
                    }

                    if (reader.ReadToFollowing(InvoiceItem.XmlName))
                    {
                        do
                        {
                            InvoiceItem item = new InvoiceItem();

                            if (reader.MoveToAttribute("desc"))
                                item.description = reader.ReadContentAsString();

                            if (reader.MoveToAttribute("amount"))
                                item.amount = reader.ReadContentAsDecimal();

                            invoice.items.Add(item);

                        } while (reader.ReadToNextSibling(InvoiceItem.XmlName));
                    }

                    yield return invoice;
                }//while
            }//using
        }//readXml
    }
}
