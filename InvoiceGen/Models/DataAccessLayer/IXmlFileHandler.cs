using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace InvoiceGen.Models.DataAccessLayer
{
    public interface IXmlFileHandler
    {
        string GetXML();
        void SaveXMLFile(XDocument doc);
    }
}
