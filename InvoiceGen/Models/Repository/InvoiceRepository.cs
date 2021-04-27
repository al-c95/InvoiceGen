﻿using System;
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
        Invoice GetInvoiceById(int id);
        IEnumerable<Invoice> GetAllInvoices();
        void AddInvoice(Invoice invoice);
        void UpdatePaidStatus(int id, bool paid);
        bool InvoiceWithTitleExists(string title);
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
        /// Check if a record with the given title already exists.
        /// </summary>
        /// <param name="title"></param>
        /// <returns>boolean</returns>
        public bool InvoiceWithTitleExists(string title)
        {
            //return getAllInvoices().Any(i => i.title == title);
            foreach (Invoice i in GetAllInvoices())
                if (i.Title == title)
                    return true;

            return false;
        }

        /// <summary>
        /// Create a new invoice record.
        /// </summary>
        /// <param name="invoice"></param>
        public void AddInvoice(Invoice invoice)
        {
            // make sure that we don't add more than one with the same title
            // should have been prevented by this point with data validation, but still check it
            if (InvoiceWithTitleExists(invoice.Title))
                return; // TODO: throw an exception here

            // now add it to the records
            service.InsertInvoiceInXml(invoice);
        }

        /// <summary>
        /// Get all invoice records.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Invoice> GetAllInvoices()
        {
            return service.ReadXml();
        }

        /// <summary>
        /// Get invoice record with specific ID.
        /// </summary>
        /// <param name="id">Unique id for the invoice.</param>
        /// <returns>Retrieved invoice record.</returns>
        public Invoice GetInvoiceById(int id)
        {
            IEnumerable<Invoice> result = GetAllInvoices()
                .Where(i => i.Id == id);

            // validate that either 0 or 1 record found
            if (result.ToList().Count > 1)
                throw new ApplicationException("Found more than one record with the same unique id: " + id); // this shouldn't happen

            if (result.ToList().Count == 1)
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

        /// <summary>
        /// Does what it says.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paid"></param>
        public void UpdatePaidStatus(int id, bool paid)
        {
            this.service.UpdatePaidStatusInXml(id, paid);
        }

        /// <summary>
        /// Does what it says.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteInvoice(int id)
        {
            this.service.DeleteInvoiceInXml(id);
        }
    }
}
