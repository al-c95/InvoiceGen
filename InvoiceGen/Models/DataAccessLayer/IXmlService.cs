using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceGen.Models.ObjectModel;

namespace InvoiceGen.Models.DataAccessLayer
{
    public interface IXmlService
    {
        void InsertInvoiceInXml(Invoice invoice);
        void UpdatePaidStatusInXml(int id, bool paid);
        void DeleteInvoiceInXml(int id);
        IEnumerable<Invoice> ReadXml();
    }
}
