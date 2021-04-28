using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Runtime.InteropServices;
using System.Net.Mail;

namespace InvoiceGen
{
    public static class Utils
    {
        public static SecureString ConvertToSecureString(string toConvert)
        {
            if (toConvert is null)
                toConvert = "";

            SecureString secureString = new SecureString();
            if (toConvert.Length > 0)
            {
                foreach (var c in toConvert.ToCharArray())
                    secureString.AppendChar(c);
            }

            return secureString;
        }

        internal static string ConvertSecureStringToNormalString(SecureString toConvert)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(toConvert);

                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
