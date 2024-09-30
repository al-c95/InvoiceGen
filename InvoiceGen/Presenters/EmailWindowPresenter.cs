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