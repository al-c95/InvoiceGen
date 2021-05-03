using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace InvoiceGen
{
    internal static class Configuration
    {
        internal const string APP_VERSION = "1.1.1";
        internal const string APP_NAME = "InvoiceGen";

        internal const string XML_FILE_PATH = "invoices.xml";

        internal const string DATE_FORMAT = "dd/MM/yyyy hh:mm:ss tt";

        // email settings
        internal static string SenderEmailAddress;
        internal static string SenderName;
        internal static string SenderPassword;
        internal static string Host;
        internal static int port;
        internal static string RecipientEmailAddress;
        internal static string RecipientName;

        // status/progress colours
        internal static readonly Color DEFAULT_COLOUR = Color.LightGray;
        internal static readonly Color IN_PROGRESS_COLOUR = Color.FromArgb(255, 128, 128, 255);
        internal static readonly Color SUCCESS_COLOUR = Color.FromArgb(255, 42, 255, 42);
        internal static readonly Color ERROR_COLOUR = Color.FromArgb(255, 255, 42, 42);
        internal static readonly Color WARNING_COLOUR = Color.FromArgb(255, 255, 212, 42);

        // input field colours
        internal static readonly Color INVALID_INPUT_COLOUR = Color.Salmon;
    }
}
