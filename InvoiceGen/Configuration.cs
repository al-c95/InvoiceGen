using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGen
{
    public static class Configuration
    {
        public const string APP_VERSION = "0.1.0 (beta)";
        public const string APP_NAME = "InvoiceGen";

        // TODO: set this
        public const string XML_FILE_PATH = "";

        // TODO: set this
        public const string DATE_FORMAT = "";

        // TODO: get from config file upon loading
        public static string senderEmailAddress;
        public static string senderName;
        public static string senderPassword;
        public static string host;
        public static int port;
        public static string recipientEmailAddress;
        public static string recipientName;

        // status/progress colours
        public static readonly System.Drawing.Color DEFAULT_COLOUR = System.Drawing.Color.LightGray;
        public static readonly System.Drawing.Color IN_PROGRESS_COLOUR = System.Drawing.Color.FromArgb(255, 128, 128, 255);
        public static readonly System.Drawing.Color SUCCESS_COLOUR = System.Drawing.Color.FromArgb(255, 42, 255, 42);
        public static readonly System.Drawing.Color ERROR_COLOUR = System.Drawing.Color.FromArgb(255, 255, 42, 42);
        public static readonly System.Drawing.Color WARNING_COLOUR = System.Drawing.Color.FromArgb(255, 255, 212, 42);
    }
}
