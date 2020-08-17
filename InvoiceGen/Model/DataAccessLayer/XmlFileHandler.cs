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
    }

    public class XmlFileHandler
    {
        private string _fileName; // TODO: get from config

        /// <summary>
        /// Constructor with filename dependency injection.
        /// </summary>
        /// <param name="fileName"></param>
        public XmlFileHandler(string fileName)
        {
            this._fileName = fileName;
        }

        /// <summary>
        /// Load the file and get the XML as a string.
        /// </summary>
        /// <returns></returns>
        public string getXML()
        {
            var xDocument = XDocument.Load(this._fileName);

            return xDocument.ToString();
        }
    }
}
