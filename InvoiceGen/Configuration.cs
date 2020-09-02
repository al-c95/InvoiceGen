using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGen
{
    public static class Configuration
    {
        public const string VERSION = "0.1.0 (beta)";
        public const string APP_NAME = "InvoiceGen";

        // TODO: set this
        public const string XML_FILE_PATH = "";

        // TODO: set this
        public const string DATE_FORMAT = "";

        // TODO: get from config file upon loading
        public static readonly string recipientEmailAddress;
        public static readonly string senderEmailAddress;
    }
}
