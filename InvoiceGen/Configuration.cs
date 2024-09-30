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

using System.Drawing;

namespace InvoiceGen
{
    internal static class Configuration
    {
        internal const string APP_VERSION = "1.1.1";
        internal const string APP_NAME = "InvoiceGen";

        internal const string XML_FILE_PATH = "invoices.xml";

        internal const string DATE_FORMAT = "dd/MM/yyyy hh:mm:ss tt";

        // email settings
        internal static string SenderEmailAddress;
        internal static string SenderName;
        internal static string SenderPassword;
        internal static string Host;
        internal static int port;
        internal static string RecipientEmailAddress;
        internal static string RecipientName;

        // status/progress colours
        internal static readonly Color DEFAULT_COLOUR = Color.LightGray;
        internal static readonly Color IN_PROGRESS_COLOUR = Color.FromArgb(255, 128, 128, 255);
        internal static readonly Color SUCCESS_COLOUR = Color.FromArgb(255, 42, 255, 42);
        internal static readonly Color ERROR_COLOUR = Color.FromArgb(255, 255, 42, 42);
        internal static readonly Color WARNING_COLOUR = Color.FromArgb(255, 255, 212, 42);

        // input field colours
        internal static readonly Color INVALID_INPUT_COLOUR = Color.Salmon;
    }
}
