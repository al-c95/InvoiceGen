﻿//MIT License

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
using System.Net;
using System.Net.Mail;
using System.Security;
using System.IO;

namespace InvoiceGen
{
    public class EmailService : IEmailService
    {
        private MailAddress _fromAddress;
        private MailAddress _toAddress;
        private MailAddress _ccAddress;
        private MailAddress _bccAddress;
        private string _fromPassword;

        private SmtpClient _smtpClient;

        /// <summary>
        /// Constructor. Reads SMTP information from configuration.
        /// </summary>
        public EmailService(SecureString password, string fromAddress, string toAddress, string ccAddress, string bccAddress)
        {
            if (password is null)
            {
                throw new ArgumentNullException("Email sender password cannot be null.");
            }
            else
            {
                this._fromPassword = Utils.ConvertSecureStringToNormalString(password);
            }

            if (string.IsNullOrWhiteSpace(fromAddress))
            {
                throw new ArgumentNullException("Email sender address cannot be null.");
            }
            else
            {
                this._fromAddress = new MailAddress(fromAddress);
            }

            if (string.IsNullOrWhiteSpace(toAddress))
            {
                throw new ArgumentNullException("Email recipient address cannot be null.");
            }
            else
            {
                this._toAddress = new MailAddress(toAddress);
            }

            if (!string.IsNullOrWhiteSpace(ccAddress))
            {
                this._ccAddress = new MailAddress(ccAddress);
            }
            else
            {
                this._ccAddress = null;
            }

            if (!string.IsNullOrEmpty(bccAddress))
            {
                this._bccAddress = new MailAddress(bccAddress);
            }
            else
            {
                this._bccAddress = null;
            }

            // set the parameters of the SMTP client
            _smtpClient = new SmtpClient();
            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.Host = Configuration.Host;
            _smtpClient.Port = Configuration.port;
            _smtpClient.EnableSsl = true;
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtpClient.Credentials = new NetworkCredential(_fromAddress.Address, _fromPassword);
            _smtpClient.Timeout = 20000;
        }

        /// <summary>
        /// Send the message. Throws SmtpException with Status Code when errors occur.
        /// </summary>
        public void SendInvoice(string subject, string body, MemoryStream attachment)
        {
            // create the message
            MailMessage message = new MailMessage();
            message.Sender = _fromAddress;
            message.From = _fromAddress;
            message.To.Add(_toAddress);
            if (!(this._ccAddress is null))
            {
                message.CC.Add(this._ccAddress);
            }
            if (!(this._bccAddress is null))
            {
                message.Bcc.Add(this._bccAddress);
            }
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = false;
            message.Priority = MailPriority.High;
            message.Attachments.Add(new Attachment(attachment, "Invoice " + subject + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));

            // now send it
            _smtpClient.Send(message);
        }
    }
}