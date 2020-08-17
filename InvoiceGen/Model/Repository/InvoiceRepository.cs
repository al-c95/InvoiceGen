using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceGen.Model.DataAccessLayer;
using InvoiceGen.Model.ObjectModel;

namespace InvoiceGen.Model.Repository
{
    public interface IInvoiceRepository
    {
        Invoice getInvoiceById(int id);
        IEnumerable<Invoice> getAllInvoices();
        void addInvoice(Invoice invoice);
        void updatePaidStatus(int id, bool paid);
    }

    public class InvoiceRepository : IInvoiceRepository
    {
        // service dependency injection
        public IXmlService service { get; private set; }

        /// <summary>
        /// Constructor with service dependency injection.
        /// </summary>
        /// <param name="service"></param>
        public InvoiceRepository(IXmlService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Create a new invoice record.
        /// </summary>
        /// <param name="invoice"></param>
        public void addInvoice(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all invoice records.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Invoice> getAllInvoices()
        {
            return service.readXml();
        }

        /// <summary>
        /// Get invoice record with specific ID.
        /// </summary>
        /// <param name="id">Unique id for the invoice.</param>
        /// <returns>Retrieved invoice record.</returns>
        public Invoice getInvoiceById(int id)
        {
            IEnumerable<Invoice> result = getAllInvoices().Where(i => i.id == id);

            // validate that either 0 or 1 record found
            if (result.ToList().Count > 1)
                throw new ApplicationException("Found more than one record with the same unique id: " + id);

            if (result.ToList().Count==1)
            {
                // the record is found
                return result.First<Invoice>();
            }
            else
            {
                // nothing found
                return null;
            }
        }

        public void updatePaidStatus(int id, bool paid)
        {
            throw new NotImplementedException();
        }
    }
}
