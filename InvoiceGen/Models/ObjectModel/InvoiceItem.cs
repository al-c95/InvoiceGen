using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGen.Model.ObjectModel
{
    public class InvoiceItem 
    {
        public const string XmlName = "item";

        public string Description { get; set; }
        public decimal Amount { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is InvoiceItem))
                throw new InvalidOperationException("Can only compare InvoiceItems with InvoiceItems.");

            InvoiceItem that = (InvoiceItem)obj;
            return (this.Description.Equals(that.Description) && this.Amount.Equals(that.Amount));
        }

        public override int GetHashCode()
        {
            return Tuple.Create(this.Description, this.Amount).GetHashCode();
        }
    }
}
