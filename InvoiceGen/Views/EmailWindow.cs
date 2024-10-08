﻿//MIT License

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
using System.Windows.Forms;
using System.Security;
using static InvoiceGen.Utils;

namespace InvoiceGen.View
{
    public partial class EmailWindow : Form, IEmailWindow
    {
        #region Properties
        public SecureString Password
        {
            get
            {
                return ConvertToSecureString(this.textBox_pwd.Text);
            }
            private set { }
        }

        private string _from;
        public string From
        {
            get => this._from;
            private set => this._from = value;
        }

        public string To
        { 
            get => this.textBox_To.Text;
            private set => this.textBox_To.Text = value;
        }

        public string Cc
        { 
            get
            {
                return this.textBox_Cc.Text;
            }
            private set { }
        }

        public string Bcc
        {
            get
            {
                return this.textBox_Bcc.Text;
            }
            private set { }
        }

        public string Subject
        {
            get => this.textBox_subject.Text;
            set => this.textBox_subject.Text = value;
        }

        public string Body
        {
            get => this.textBox_body.Text;
            set => this.textBox_body.Text = value;
        }

        public Color PwdFieldColour
        {
            get => this.textBox_pwd.BackColor;
            set => this.textBox_pwd.BackColor = value;
        }

        public Color ToFieldColour
        {
            get => this.textBox_To.BackColor;
            set => this.textBox_To.BackColor = value;
        }

        public Color CcFieldColour
        {
            get => this.textBox_Cc.BackColor;
            set => this.textBox_Cc.BackColor = value;
        }

        public Color BccFieldColour
        {
            get => this.textBox_Bcc.BackColor;
            set => this.textBox_Bcc.BackColor = value;
        }

        public bool SaveAndSendButtonEnabled
        {
            get => this.button_send.Enabled;
            set => this.button_send.Enabled = value;
        }

        public string SendButtonText
        {
            get => this.button_send.Text;
            set => this.button_send.Text = value;
        }

        public string CancelButtonText
        {
            get => this.button_cancel.Text;
            set => this.button_cancel.Text = value;
        }
        #endregion

        public EmailWindow(string title, Color invalidInputColour, string senderAddress, string recipientAddress)
        {
            InitializeComponent();

            this.button_send.Enabled = false;

            this.To = recipientAddress;
            this.textBox_subject.Text = "Invoice: " + title;
            this.From = senderAddress;
            this.PwdFieldColour = invalidInputColour;

            // register event handlers
            this.button_cancel.Click += (sender, args) => 
            { 
                this.DialogResult = DialogResult.Cancel; 
            };
            this.button_send.Click += (sender, args) => 
            {
                this.DialogResult = DialogResult.OK;
            };
            this.textBox_pwd.TextChanged += TextFieldTextChanged;
            this.textBox_To.TextChanged += TextFieldTextChanged;
            this.textBox_Cc.TextChanged += TextFieldTextChanged;
            this.textBox_Bcc.TextChanged += TextFieldTextChanged;
        }

        #region Methods
        public void ResetInputFieldColours()
        {
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    control.ResetBackColor();
                }
            }
        }

        public void ResetPasswordFieldColour()
        {
            this.textBox_pwd.ResetBackColor();
        }

        public void ResetToFieldColour()
        {
            this.textBox_To.ResetBackColor();
        }

        public void ResetCcFieldColour()
        {
            this.textBox_Cc.ResetBackColor();
        }

        public void ResetBccFieldColour()
        {
            this.textBox_Bcc.ResetBackColor();
        }
        #endregion

        #region Events
        public event EventHandler InputFieldTextChanged;
        #endregion

        #region UI event handlers
        private void TextFieldTextChanged(object sender, EventArgs args)
        {
            // fire the external event so the subscribed presenter can react
            this.InputFieldTextChanged?.Invoke(sender, args);
        }
        #endregion
    }
}