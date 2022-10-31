using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGen.Models.ObjectModel
{
    public class Invoice
    {
        public const string XmlName = "invoice";

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Paid { get; set; }
        public IList<InvoiceItem> Items { get; set; }

        /// <summary>
        /// Default constructor. Initialises the collection of items.
        /// </summary>
        public Invoice()
        {
            this.Items = new List<InvoiceItem>();
        }

        /// <summary>
        /// Get total of all item amounts.
        /// </summary>
        /// <returns></returns>
        public decimal GetTotal()
        {
            if (Items == null)
                return 0;

            decimal total = 0;

            foreach (InvoiceItem item in this.Items)
                total += item.Amount;

            return total;
        }
    }//class
}