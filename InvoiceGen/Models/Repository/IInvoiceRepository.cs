using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceGen.Models.ObjectModel;

namespace InvoiceGen.Models.Repository
{
    public interface IInvoiceRepository
    {
        Invoice GetInvoiceById(int id);
        IEnumerable<Invoice> GetAllInvoices();
        void AddInvoice(Invoice invoice);
        void UpdatePaidStatus(int id, bool paid);
        bool InvoiceWithTitleExists(string title);
    }
}
