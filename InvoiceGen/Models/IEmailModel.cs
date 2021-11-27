using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using InvoiceGen;

namespace InvoiceGen.Models
{
    public interface IEmailModel
    {
        Color InvalidInputColour { get; }
        bool IsValidEmail(string address);
    }
}
