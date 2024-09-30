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
using InvoiceGen.Models;
using InvoiceGen.Views;

namespace InvoiceGen.Presenters
{
    public class ConfigWindowPresenter
    {
        private IEmailModel _model;
        public IConfigWindow _view;

        public ConfigWindowPresenter(IConfigWindow view, IEmailModel model)
        {
            this._model = model;
            this._view = view;

            // subscribe to the view's events
            this._view.InputFieldChanged += InputFieldChanged;
        }

        #region Event handlers
        public void InputFieldChanged(object sender, EventArgs args)
        {
            // remove any highlighting first
            this._view.ResetInputFieldColours();

            // validate inputs
            bool isValid = true;
            isValid = isValid && !string.IsNullOrWhiteSpace(this._view.SenderName);
            isValid = isValid && !string.IsNullOrWhiteSpace(this._view.RecipientName);
            isValid = isValid && !string.IsNullOrWhiteSpace(this._view.Host);
            if (!string.IsNullOrWhiteSpace(this._view.SenderAddress))
            {
                isValid = isValid && (this._model.IsValidEmail(this._view.SenderAddress));
            }
            else
            {
                isValid = false;
            }
            if (!string.IsNullOrWhiteSpace(this._view.RecipientAddress))
            {
                isValid = isValid && (this._model.IsValidEmail(this._view.RecipientAddress));
            }
            else
            {
                isValid = false;
            }
            this._view.SaveButtonEnabled = isValid;

            // highlight any fields with invalid input
            if (!this._model.IsValidEmail(this._view.SenderAddress))
            {
                this._view.SenderAddressFieldColour = this._model.InvalidInputColour;
            }
            else
            {
                this._view.ResetSenderAddressFieldColour();
            }
            if (string.IsNullOrWhiteSpace(this._view.SenderName))
            {
                this._view.SenderNameFieldColour = this._model.InvalidInputColour;
            }
            else
            {
                this._view.ResetSenderNameFieldColour();
            }
            if (string.IsNullOrWhiteSpace(this._view.Host))
            {
                this._view.HostFieldColour = this._model.InvalidInputColour;
            }
            else
            {
                this._view.ResetHostFieldColour();
            }
            if (!this._model.IsValidEmail(this._view.RecipientAddress))
            {
                this._view.RecipientAddressFieldColour = this._model.InvalidInputColour;
            }
            else
            {
                this._view.ResetRecipientAddressFieldColour();
            }
            if (string.IsNullOrWhiteSpace(this._view.RecipientName))
            {
                this._view.RecipientNameFieldColour = this._model.InvalidInputColour;
            }
            else
            {
                this._view.ResetRecipientNameFieldColour();
            }
        }
        #endregion
    }//class
}