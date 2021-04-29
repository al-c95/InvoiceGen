using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public event EventHandler SaveButtonClicked;
        public event EventHandler CancelButtonClicked;
        #endregion

        #region Event handlers
        private void _InputFieldChanged(object sender, EventArgs args)
        {
            // fire the external event so the subscribed presenter can react
            this.InputFieldChanged?.Invoke(sender, args);
        }
        private void Button_save_Click(object sender, EventArgs args)
        {
            // fire the external event so the subscribed presenter can react
            this.SaveButtonClicked?.Invoke(sender, args);
        }

        private void Button_cancel_Click(object sender, EventArgs args)
        {
            // fire the external event so the subscribed presenter can react
            this.CancelButtonClicked?.Invoke(sender, args);
        }
        #endregion
    }
}
