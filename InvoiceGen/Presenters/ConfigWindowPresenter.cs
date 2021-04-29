using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using InvoiceGen.Models;
using InvoiceGen.Views;

namespace InvoiceGen.Presenters
{
    public class ConfigWindowPresenter
    {
        private IEmailModel _model;
        public IConfigWindow View;

        public ConfigWindowPresenter(IConfigWindow view, IEmailModel model)
        {
            this._model = model;
            this.View = view;

            // subscribe to the view's events
            this.View.InputFieldChanged += InputFieldChanged;
            this.View.SaveButtonClicked += SaveButtonClicked;
            this.View.CancelButtonClicked += CancelButtonClicked;
        }

        public System.Windows.Forms.DialogResult ShowDialog()
        {
            return ((System.Windows.Forms.Form)(this.View)).ShowDialog();
        }

        public void DisposeDialog()
        {
            ((System.Windows.Forms.Form)(this.View)).Dispose();
        }

        private void SetValue(string key, string value)
        {
            System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Windows.Forms.Application.ExecutablePath);
            config.AppSettings.Settings[key].Value = value;
            config.Save();
        }

        #region Event handlers
        public void InputFieldChanged(object sender, EventArgs args)
        {
            // remove any highlighting first
            this.View.ResetInputFieldColours();

            // validate inputs

            bool isValid = true;
            isValid = isValid && !string.IsNullOrWhiteSpace(this.View.SenderName);
            isValid = isValid && !string.IsNullOrWhiteSpace(this.View.RecipientName);
            isValid = isValid && !string.IsNullOrWhiteSpace(this.View.Host);
            if (!string.IsNullOrWhiteSpace(this.View.SenderAddress))
            {
                isValid = isValid && (this._model.IsValidEmail(this.View.SenderAddress));
            }
            else
            {
                isValid = false;
            }

            if (!string.IsNullOrWhiteSpace(this.View.RecipientAddress))
            {
                isValid = isValid && (this._model.IsValidEmail(this.View.RecipientAddress));
            }
            else
            {
                isValid = false;
            }

            // decide whether to enable the save button
            this.View.SaveButtonEnabled = isValid;

            // highlight any fields with invalid input

            if (!this._model.IsValidEmail(this.View.SenderAddress))
            {
                this.View.SenderAddressFieldColour = this._model.InvalidInputColour;
            }
            else
            {
                this.View.ResetSenderAddressFieldColour();
            }

            if (string.IsNullOrWhiteSpace(this.View.SenderName))
            {
                this.View.SenderNameFieldColour = this._model.InvalidInputColour;
            }
            else
            {
                this.View.ResetSenderNameFieldColour();
            }

            if (string.IsNullOrWhiteSpace(this.View.Host))
            {
                this.View.HostFieldColour = this._model.InvalidInputColour;
            }
            else
            {
                this.View.ResetHostFieldColour();
            }

            if (!this._model.IsValidEmail(this.View.RecipientAddress))
            {
                this.View.RecipientAddressFieldColour = this._model.InvalidInputColour;
            }
            else
            {
                this.View.ResetRecipientAddressFieldColour();
            }

            if (string.IsNullOrWhiteSpace(this.View.RecipientName))
            {
                this.View.RecipientNameFieldColour = this._model.InvalidInputColour;
            }
            else
            {
                this.View.ResetRecipientNameFieldColour();
            }
        }

        public void SaveButtonClicked(object sender, EventArgs args)
        {
            SetValue("senderEmail", this.View.SenderAddress);
            SetValue("SenderName", this.View.SenderName);
            SetValue("Host", this.View.Host);
            SetValue("port", this.View.Port.ToString());
            SetValue("recipientEmail", this.View.RecipientAddress);
            SetValue("recipientName", this.View.RecipientName);

            Configuration.SenderEmailAddress = this.View.SenderAddress;
            Configuration.SenderName = this.View.SenderName;
            Configuration.Host = this.View.Host;
            Configuration.port = this.View.Port;
            Configuration.RecipientName = this.View.RecipientName;
            Configuration.RecipientEmailAddress = this.View.RecipientAddress;

            ((System.Windows.Forms.Form)(this.View)).DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public void CancelButtonClicked(object sender, EventArgs args)
        {
            ((System.Windows.Forms.Form)(this.View)).DialogResult = System.Windows.Forms.DialogResult.Cancel;
            ((System.Windows.Forms.Form)(this.View)).Close();
        }
        #endregion
    }
}
