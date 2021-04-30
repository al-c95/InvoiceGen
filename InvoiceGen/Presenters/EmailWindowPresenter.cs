using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceGen.View;
using InvoiceGen.Models;
using static InvoiceGen.Utils;

namespace InvoiceGen.Presenters
{
    public class EmailWindowPresenter
    {
        public IEmailWindow View { get; private set; }
        private IEmailModel _model;

        public EmailWindowPresenter(IEmailWindow view, IEmailModel model)
        {
            this.View = view;
            this._model = model;

            // subscribe to the view's events
            this.View.SaveAndSendButtonClicked += SaveAndSendButtonClicked;
            this.View.InputFieldTextChanged += InputFieldTextChanged;
        }

        public System.Windows.Forms.DialogResult ShowDialog()
        {
            return ((System.Windows.Forms.Form)(this.View)).ShowDialog();
        }

        public void DisposeDialog()
        {
            ((System.Windows.Forms.Form)(this.View)).Dispose();
        }

        #region event handlers
        public void InputFieldTextChanged(object sender, EventArgs e)
        {
            // remove any highlighting first
            this.View.ResetInputFieldColours();

            // validate inputs
            bool isValid = true;
            isValid = isValid && !(string.IsNullOrEmpty(ConvertSecureStringToNormalString(this.View.Password)));
            isValid = isValid && this._model.IsValidEmail(this.View.To);
            if (!string.IsNullOrEmpty(this.View.Cc))
            {
                isValid = isValid && this._model.IsValidEmail(this.View.Cc);
            }
            if (!string.IsNullOrEmpty(this.View.Bcc))
            {
                isValid = isValid && this._model.IsValidEmail(this.View.Bcc);
            }

            // decide whether to enable the send button
            this.View.SaveAndSendButtonEnabled = isValid;

            // highlight any fields with invalid input

            if (!this._model.IsValidEmail(this.View.To))
            {
                this.View.ToFieldColour = this._model.InvalidInputColour;
            }
            else
            {
                this.View.ResetToFieldColour();
            }

            if (!string.IsNullOrWhiteSpace(this.View.Cc))
            {
                if (!this._model.IsValidEmail(this.View.Cc))
                {
                    this.View.CcFieldColour = this._model.InvalidInputColour;
                }
                else
                {
                    this.View.ResetCcFieldColour();
                }
            }
            else
            {
                this.View.ResetCcFieldColour();
            }

            if (!string.IsNullOrWhiteSpace(this.View.Bcc))
            {
                if (!this._model.IsValidEmail(this.View.Bcc))
                {
                    this.View.BccFieldColour = this._model.InvalidInputColour;
                }
                else
                {
                    this.View.ResetBccFieldColour();
                }
            }
            else
            {
                this.View.ResetBccFieldColour();
            }

            if (String.IsNullOrWhiteSpace(ConvertSecureStringToNormalString(this.View.Password)))
            {
                this.View.PwdFieldColour = this._model.InvalidInputColour;
            }
            else
            {
                this.View.ResetPasswordFieldColour();
            }
        }

        public void SaveAndSendButtonClicked(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Form)(this.View)).DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        #endregion
    }
}
