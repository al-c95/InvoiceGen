using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGen
{
    public interface IEmailService
    {
        void SendInvoice(string subject, string body, MemoryStream attachment);
    }
}
