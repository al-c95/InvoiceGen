//MIT License

//Copyright (c) 2020-2024

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE

using System;
using System.Collections.Generic;
using System.Linq;
using InvoiceGen.Models.DataAccessLayer;
using InvoiceGen.Models.ObjectModel;

namespace InvoiceGen.Models.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
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
            return GetAllInvoices().Any(i => i.Title == title);
        }

        /// <summary>
        /// Create a new invoice record.
        /// Make sure that we don't add more than one with the same title.
        /// </summary>
        /// <param name="invoice"></param>
        public void AddInvoice(Invoice invoice)
        {
            if (InvoiceWithTitleExists(invoice.Title))
                throw new ApplicationException("Invoice: " + invoice.Title + " already exists.");

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
        /// Update paid status of a single invoice by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paid"></param>
        public void UpdatePaidStatus(int id, bool paid)
        {
            this.service.UpdatePaidStatusInXml(id, paid);
        }

        /// <summary>
        /// Delete a single invoice by id.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteInvoice(int id)
        {
            this.service.DeleteInvoiceInXml(id);
        }//DeleteInvoice
    }//class
}
