using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using InvoiceGen;

namespace InvoiceGen.View
{
    public partial class EmailWindow : Form
    {
        public SecureString Password { get; private set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public string Cc { get; private set; }
        public string Bcc { get; private set; }

        public Color InvalidInputColour { get; private set; }

        public EmailWindow(string title)
        {
            InitializeComponent();

            this.InvalidInputColour = Configuration.INVALID_INPUT_COLOUR;

            this.button_send.Enabled = false;

            this.textBox_To.Text = Configuration.RecipientEmailAddress;
            this.textBox_subject.Text = "Invoice: " + title;
            this.From = Configuration.SenderEmailAddress;

            // register event handlers
            this.button_cancel.Click += (sender, args) => { this.DialogResult = DialogResult.Cancel; };
            this.button_send.Click += (sender, args) => 
            {
                this.Password = Utils.ConvertToSecureString(this.textBox_pwd.Text);
                this.To = textBox_To.Text;
                this.Cc = textBox_Cc.Text;
                this.Bcc = textBox_Bcc.Text;

                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            this.textBox_pwd.TextChanged += TextFieldTextChanged;
            this.textBox_To.TextChanged += TextFieldTextChanged;
            this.textBox_Cc.TextChanged += TextFieldTextChanged;
            this.textBox_Bcc.TextChanged += TextFieldTextChanged;
        }

        #region UI event handlers
        private void TextFieldTextChanged(object sender, EventArgs e)
        {
            // remove any highlighting first
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    control.ResetBackColor();
                }
            }

            // validate inputs
            bool isValid = true;
            isValid = isValid && !(string.IsNullOrEmpty(this.textBox_pwd.Text));
            isValid = isValid && Utils.IsValidEmail(this.textBox_To.Text);
            if (!string.IsNullOrEmpty(this.textBox_Cc.Text))
            {
                isValid = isValid && Utils.IsValidEmail(this.textBox_Cc.Text);
            }
            if (!string.IsNullOrEmpty(this.textBox_Bcc.Text))
            {
                isValid = isValid && Utils.IsValidEmail(this.textBox_Bcc.Text);
            }

            // decide whether to enable the send button
            this.button_send.Enabled = isValid;

            // highlight any problematic fields

            if (!Utils.IsValidEmail(this.textBox_To.Text))
            {
                this.textBox_To.BackColor = this.InvalidInputColour;
            }
            else
            {
                this.textBox_To.ResetBackColor();
            }

            if (!string.IsNullOrWhiteSpace(this.textBox_Cc.Text))
            {
                if (!Utils.IsValidEmail(this.textBox_Cc.Text))
                {
                    this.textBox_Cc.BackColor = this.InvalidInputColour;
                }
                else
                {
                    this.textBox_Cc.ResetBackColor();
                }
            }
            else
            {
                this.textBox_Cc.ResetBackColor();
            }

            if (!string.IsNullOrWhiteSpace(this.textBox_Bcc.Text))
            {
                if (!Utils.IsValidEmail(this.textBox_Bcc.Text))
                {
                    this.textBox_Bcc.BackColor = this.InvalidInputColour;
                }
                else
                {
                    this.textBox_Bcc.ResetBackColor();
                }
            }
            else
            {
                this.textBox_Bcc.ResetBackColor();
            }

            if (String.IsNullOrWhiteSpace(this.textBox_pwd.Text))
            {
                this.textBox_pwd.BackColor = this.InvalidInputColour;
            }
            else
            {
                this.textBox_pwd.ResetBackColor();
            }
        }
        #endregion
    }
}
