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
using System.Windows.Forms;
using InvoiceGen.Views;

namespace InvoiceGen.View
{
    public partial class ConfigWindow : Form, IConfigWindow
    {
        public ConfigWindow()
        {
            InitializeComponent();

            // load the values from the configuration
            this.textBox_senderAddress.Text = Configuration.SenderEmailAddress;
            this.textBox_SenderName.Text = Configuration.SenderName;
            this.textBox_Host.Text = Configuration.Host;
            this.textBox_recipientAddress.Text = Configuration.RecipientEmailAddress;
            this.textBox_recipientName.Text = Configuration.RecipientName;
            this.numericUpDown_port.Value = Configuration.port;

            // register event handlers
            this.textBox_senderAddress.TextChanged += _InputFieldChanged;
            this.textBox_SenderName.TextChanged += _InputFieldChanged;
            this.textBox_Host.TextChanged += _InputFieldChanged;
            this.textBox_recipientAddress.TextChanged += _InputFieldChanged;
            this.textBox_recipientName.TextChanged += _InputFieldChanged;
            this.numericUpDown_port.ValueChanged += _InputFieldChanged;
            this.button_cancel.Click += Button_cancel_Click;
            this.button_save.Click += Button_save_Click;
        }

        private void SetValue(string key, string value)
        {
            System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Windows.Forms.Application.ExecutablePath);
            config.AppSettings.Settings[key].Value = value;
            config.Save();
        }

        #region Properties
        public string SenderAddress
        {
            get => this.textBox_senderAddress.Text;
            set => this.textBox_senderAddress.Text = value;
        }

        public string SenderName
        {
            get => this.textBox_SenderName.Text;
            set => this.textBox_SenderName.Text = value;
        }

        public string Host
        {
            get => this.textBox_Host.Text;
            set => this.textBox_Host.Text = value;
        }

        public int Port
        {
            get => (int)this.numericUpDown_port.Value;
            set => this.numericUpDown_port.Value = (int)value;
        }

        public string RecipientAddress
        {
            get => this.textBox_recipientAddress.Text;
            set => this.textBox_recipientAddress.Text = value;
        }

        public string RecipientName
        {
            get => this.textBox_recipientName.Text;
            set => this.textBox_recipientName.Text = value;
        }

        public Color SenderAddressFieldColour
        {
            get => this.textBox_senderAddress.BackColor;
            set => this.textBox_senderAddress.BackColor = value;
        }

        public Color SenderNameFieldColour
        {
            get => this.textBox_SenderName.BackColor;
            set => this.textBox_SenderName.BackColor = value;
        }

        public Color HostFieldColour 
        {
            get => this.textBox_Host.BackColor;
            set => this.textBox_Host.BackColor = value;
        }

        public Color RecipientAddressFieldColour
        {
            get => this.textBox_recipientAddress.BackColor;
            set => this.textBox_recipientAddress.BackColor = value;
        }

        public Color RecipientNameFieldColour
        {
            get => this.textBox_recipientName.BackColor;
            set => this.textBox_recipientName.BackColor = value;
        }

        public bool SaveButtonEnabled 
        {
            get => this.button_save.Enabled;
            set => this.button_save.Enabled = value;
        }

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

        public void ResetSenderAddressFieldColour()
        {
            this.textBox_senderAddress.ResetBackColor();
        }

        public void ResetSenderNameFieldColour()
        {
            this.textBox_SenderName.ResetBackColor();
        }

        public void ResetHostFieldColour()
        {
            this.textBox_Host.ResetBackColor();
        }

        public void ResetRecipientAddressFieldColour()
        {
            this.textBox_recipientAddress.ResetBackColor();
        }

        public void ResetRecipientNameFieldColour()
        {
            this.textBox_recipientName.ResetBackColor();
        }
        #endregion

        #region Events
        public event EventHandler InputFieldChanged;
        #endregion

        #region Event handlers
        private void _InputFieldChanged(object sender, EventArgs args)
        {
            // fire the external event so the subscribed presenter can react
            this.InputFieldChanged?.Invoke(sender, args);
        }

        private void Button_save_Click(object sender, EventArgs args)
        {
            SetValue("senderEmail", this.SenderAddress);
            SetValue("SenderName", this.SenderName);
            SetValue("Host", this.Host);
            SetValue("port", this.Port.ToString());
            SetValue("recipientEmail", this.RecipientAddress);
            SetValue("recipientName", this.RecipientName);

            Configuration.SenderEmailAddress = this.SenderAddress;
            Configuration.SenderName = this.SenderName;
            Configuration.Host = this.Host;
            Configuration.port = this.Port;
            Configuration.RecipientName = this.RecipientName;
            Configuration.RecipientEmailAddress = this.RecipientAddress;

            this.DialogResult = DialogResult.OK;
        }

        private void Button_cancel_Click(object sender, EventArgs args)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        #endregion
    }
}