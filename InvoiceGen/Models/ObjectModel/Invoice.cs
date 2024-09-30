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