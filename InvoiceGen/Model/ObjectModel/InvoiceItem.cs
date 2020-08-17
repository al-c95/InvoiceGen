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

        public string description { get; set; }
        public decimal amount { get; set; }
    }
}
