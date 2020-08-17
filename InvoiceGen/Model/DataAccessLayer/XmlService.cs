using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using InvoiceGen.Model.ObjectModel;

namespace InvoiceGen.Model.DataAccessLayer
{
    public interface IXmlService
    {
        IEnumerable<Invoice> readXml();
    }

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
            // save the file
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
                }
            }
        }
    }
}
