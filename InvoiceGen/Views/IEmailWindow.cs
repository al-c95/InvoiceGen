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
