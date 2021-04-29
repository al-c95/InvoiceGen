using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace InvoiceGen.Views
{
    public interface IConfigWindow
    {
        string SenderAddress { get; set; }
        string SenderName { get; set; }
        string Host { get; set; }
        int Port { get; set; }
        string RecipientAddress { get; set; }
        string RecipientName { get; set; }
        Color SenderAddressFieldColour { get; set; }
        Color SenderNameFieldColour { get; set; }
        Color HostFieldColour { get; set; }
        Color RecipientAddressFieldColour { get; set; }
        Color RecipientNameFieldColour { get; set; }
        bool SaveButtonEnabled { get; set; }

        void ResetInputFieldColours();
        void ResetSenderAddressFieldColour();
        void ResetSenderNameFieldColour();
        void ResetHostFieldColour();
        void ResetRecipientAddressFieldColour();
        void ResetRecipientNameFieldColour();

        event EventHandler InputFieldChanged;
        event EventHandler SaveButtonClicked;
        event EventHandler CancelButtonClicked;
    }
}
