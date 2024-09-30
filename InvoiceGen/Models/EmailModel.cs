//MIT License

//Copyright (c) 2020-2024

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE

using System;
using System.Drawing;
using System.Net.Mail;

namespace InvoiceGen.Models
{
    public class EmailModel : IEmailModel
    {
        public Color InvalidInputColour { get; private set; }

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
        }//IsValidEmail
    }//class
}
