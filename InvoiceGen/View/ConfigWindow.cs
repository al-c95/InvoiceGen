using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceGen.View
{
    public partial class ConfigWindow : Form
    {
        public ConfigWindow()
        {
            InitializeComponent();

            // load the values from the configuration
            this.textBox_senderAddress.Text = Configuration.SenderEmailAddress;
            this.textBox_SenderName.Text = Configuration.SenderName;
            this.textBox_Host.Text = Configuration.Host;
            this.textBox_senderEmailPassword.Text = Configuration.SenderPassword;
            this.textBox_recipientAddress.Text = Configuration.RecipientEmailAddress;
            this.textBox_recipientName.Text = Configuration.RecipientName;
            this.numericUpDown_port.Value = Configuration.port;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            SetValue("senderEmail", textBox_senderAddress.Text);
            SetValue("SenderName", textBox_SenderName.Text);
            SetValue("SenderPassword", textBox_senderEmailPassword.Text);
            SetValue("Host", textBox_Host.Text);
            SetValue("port", numericUpDown_port.Value.ToString());
            SetValue("recipientEmail", textBox_recipientAddress.Text);
            SetValue("recipientName", textBox_recipientName.Text);

            this.Close();
            Application.Exit();
        }

        private void SetValue(string key, string value)
        {
            System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings[key].Value = value;
            config.Save();
        }
    }
}
