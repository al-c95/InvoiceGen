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
        internal static SecureString ConvertToSecureString(string toConvert)
        {
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

        /// <summary>
        /// Validates an email address as a string.
        /// </summary>
        /// <param name="address">Email address as string.</param>
        /// <returns>True or false.</returns>
        public static bool IsValidEmail(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return false;

            try
            {
                var validatedAddress = new MailAddress(address);
            }
            catch (FormatException)
            {
                return false;
            }

            return true;
        }
    }
}
