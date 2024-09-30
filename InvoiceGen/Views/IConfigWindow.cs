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
    }
}
