using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace InvoiceGen.Models.DataAccessLayer
{
    /// <summary>
    /// Loads and saves the XML file.
    /// </summary>
    public class XmlFileHandler : IXmlFileHandler
    {
        private string _fileName; 
        private XDocument _doc;

        /// <summary>
        /// Constructor with filename dependency injection. Opens the file.
        /// </summary>
        /// <param name="fileName"></param>
        public XmlFileHandler(string fileName)
        {
            this._fileName = fileName;

            Load();
        }

        /// <summary>
        /// Save the XML file.
        /// </summary>
        public void SaveXMLFile(XDocument doc)
        {
            // first try saving it
            doc.Save(_fileName);

            // then reload it
            Load();
        }

        /// <summary>
        /// Return the XML as a string.
        /// </summary>
        /// <returns></returns>
        public string GetXML()
        {
            return _doc.ToString();
        }

        /// <summary>
        /// Opens the file.
        /// </summary>
        private void Load()
        {
            this._doc = XDocument.Load(_fileName);
        }//Load
    }//class
}
