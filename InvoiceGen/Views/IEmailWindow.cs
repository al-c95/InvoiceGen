using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Drawing;

namespace InvoiceGen.View
{
    public interface IEmailWindow
    {
        SecureString Password { get; }
        string From { get; }
        string To { get; }
        string Cc { get; }
        string Bcc { get; }
        string Subject { get; set; }
        string Body { get; set; }
        Color PwdFieldColour { get; set; }
        Color ToFieldColour { get; set; }
        Color CcFieldColour { get; set; }
        Color BccFieldColour { get; set; }
        bool SaveAndSendButtonEnabled { get; set; }
        string SendButtonText { get; set; }
        string CancelButtonText { get; set; }

        void ResetInputFieldColours();
        void ResetPasswordFieldColour();
        void ResetToFieldColour();
        void ResetCcFieldColour();
        void ResetBccFieldColour();

        event EventHandler InputFieldTextChanged;
    }
}
