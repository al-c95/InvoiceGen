using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace InvoiceGen.Model.DataAccessLayer
{
    public interface IXmlFileHandler
    {
        string getXML();
        void saveXMLFile(XDocument doc);
    }

    /// <summary>
    /// Loads and saves the XML file.
    /// </summary>
    public class XmlFileHandler : IXmlFileHandler
    {
        private string _fileName; // TODO: get from config
        private XDocument _doc;

        /// <summary>
        /// Constructor with filename dependency injection. Opens the file.
        /// </summary>
        /// <param name="fileName"></param>
        public XmlFileHandler(string fileName)
        {
            this._fileName = fileName;

            load();
        }

        /// <summary>
        /// Save the XML file.
        /// </summary>
        public void saveXMLFile(XDocument doc)
        {
            // first try saving it
            doc.Save(_fileName);

            // then reload it
            load();
        }

        /// <summary>
        /// Return the XML as a string.
        /// </summary>
        /// <returns></returns>
        public string getXML()
        {
            return _doc.ToString();
        }

        /// <summary>
        /// Opens the file.
        /// </summary>
        private void load()
        {
            //this._doc = XDocument.Load(_fileName);
        }
    }
}
