using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Mail;
using InvoiceGen;

namespace InvoiceGen.Models
{
    public interface IEmailModel
    {
        Color InvalidInputColour { get; }
        bool IsValidEmail(string address);
    }

    public class EmailModel : IEmailModel
    {
        public Color InvalidInputColour { get; private set; }

        /// <summary>
        /// Production constructor.
        /// </summary>
        /// <param name="invalidInputColour"></param>
        public EmailModel(Color invalidInputColour)
        {
            this.InvalidInputColour = invalidInputColour;
        }

        /// <summary>
        /// Validates an email address as a string.
        /// </summary>
        /// <param name="address">Email address as string.</param>
        /// <returns>True or false.</returns>
        public bool IsValidEmail(string address)
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
