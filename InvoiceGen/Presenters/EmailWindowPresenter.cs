using System;
using InvoiceGen.View;
using InvoiceGen.Models;
using static InvoiceGen.Utils;

namespace InvoiceGen.Presenters
{
    public class EmailWindowPresenter
    {
        public IEmailWindow _view { get; private set; }
        private IEmailModel _model;

        public EmailWindowPresenter(IEmailWindow view, IEmailModel model)
        {
            this._view = view;
            this._model = model;

            // subscribe to the view's events
            this._view.InputFieldTextChanged += InputFieldTextChanged;
        }

        public System.Windows.Forms.DialogResult ShowDialog()
        {
            return ((System.Windows.Forms.Form)(this._view)).ShowDialog();
        }

        public void DisposeDialog()
        {
            ((System.Windows.Forms.Form)(this._view)).Dispose();
        }

        #region event handlers
        public void InputFieldTextChanged(object sender, EventArgs e)
        {
            // remove any highlighting first
            this._view.ResetInputFieldColours();

            // validate inputs
            bool isValid = true;
            isValid = isValid && !(string.IsNullOrEmpty(ConvertSecureStringToNormalString(this._view.Password)));
            isValid = isValid && this._model.IsValidEmail(this._view.To);
            if (!string.IsNullOrEmpty(this._view.Cc))
            {
                isValid = isValid && this._model.IsValidEmail(this._view.Cc);
            }
            if (!string.IsNullOrEmpty(this._view.Bcc))
            {
                isValid = isValid && this._model.IsValidEmail(this._view.Bcc);
            }
            this._view.SaveAndSendButtonEnabled = isValid;

            if (!this._model.IsValidEmail(this._view.To))
            {
                this._view.ToFieldColour = this._model.InvalidInputColour;
            }
            else
            {
                this._view.ResetToFieldColour();
            }

            if (!string.IsNullOrWhiteSpace(this._view.Cc))
            {
                if (!this._model.IsValidEmail(this._view.Cc))
                {
                    this._view.CcFieldColour = this._model.InvalidInputColour;
                }
                else
                {
                    this._view.ResetCcFieldColour();
                }
            }
            else
            {
                this._view.ResetCcFieldColour();
            }

            if (!string.IsNullOrWhiteSpace(this._view.Bcc))
            {
                if (!this._model.IsValidEmail(this._view.Bcc))
                {
                    this._view.BccFieldColour = this._model.InvalidInputColour;
                }
                else
                {
                    this._view.ResetBccFieldColour();
                }
            }
            else
            {
                this._view.ResetBccFieldColour();
            }

            if (String.IsNullOrWhiteSpace(ConvertSecureStringToNormalString(this._view.Password)))
            {
                this._view.PwdFieldColour = this._model.InvalidInputColour;
            }
            else
            {
                this._view.ResetPasswordFieldColour();
            }
        }
        #endregion
    }
}