using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGen.Model.ObjectModel
{
    public class Invoice
    {
        public const string XmlName = "invoice";

        public int id { get; set; }
        public string title { get; set; }
        public DateTime timestamp { get; set; }
        public bool paid { get; set; }
        public IList<InvoiceItem> items { get; set; }

        /// <summary>
        /// Default constructor. Initialises the collection of items.
        /// </summary>
        public Invoice()
        {
            this.items = new List<InvoiceItem>();
        }

        /// <summary>
        /// Get total of all item amounts.
        /// </summary>
        /// <returns></returns>
        public decimal getTotal()
        {
            if (items == null)
                return 0;

            decimal total = 0;

            foreach (InvoiceItem item in this.items)
                total += item.amount;

            return total;
        }
    }
}
