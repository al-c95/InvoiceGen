using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace InvoiceGen.EmailService
{
    public class EmailService
    {
        private MailAddress _fromAddress;
        private MailAddress _toAddress;
        private string _fromPassword;
        private string _subject;
        private string _body;

        private SmtpClient _smtpClient;
        private MemoryStream _ms;

        public EmailService(string subject, string body, MemoryStream attachment)
        {
            // read from config
            _fromAddress = new MailAddress(Configuration.senderEmailAddress);
            _toAddress = new MailAddress(Configuration.recipientEmailAddress);
            _fromPassword = Configuration.senderPassword;

            // set the subject and body
            _subject = subject;
            _body = body;

            _ms = attachment;

            // set the parameters of the SMTP client
            _smtpClient.Host = Configuration.host;
            _smtpClient.Port = Configuration.port;
            _smtpClient.EnableSsl = true;
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtpClient.Credentials = new NetworkCredential(_fromAddress.Address, _toAddress.Address);
            _smtpClient.Timeout = 20000;
        }

        /// <summary>
        /// Send the message. Throws SmtpException with Status Code when errors occur.
        /// </summary>
        public void sendInvoice()
        {
            // create the message
            MailMessage message = new MailMessage();
            message.Sender = _fromAddress;
            message.From = _fromAddress;
            message.To.Add(_toAddress);
            message.Subject = _subject;
            message.Body = _body;
            message.IsBodyHtml = false;
            message.Priority = MailPriority.High;
            message.Attachments.Add(new Attachment(""));

            // now send it
            _smtpClient.Send(message);
        }
    }
}
