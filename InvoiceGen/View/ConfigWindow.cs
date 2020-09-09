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
            this.textBox_senderAddress.Text = Configuration.senderEmailAddress;
            this.textBox_senderName.Text = Configuration.senderName;
            this.textBox_host.Text = Configuration.host;
            this.textBox_senderEmailPassword.Text = Configuration.senderPassword;
            this.textBox_recipientAddress.Text = Configuration.recipientEmailAddress;
            this.textBox_recipientName.Text = Configuration.recipientName;
            this.numericUpDown_port.Value = Configuration.port;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            setValue("senderEmail", textBox_senderAddress.Text);
            setValue("senderName", textBox_senderName.Text);
            setValue("senderPassword", textBox_senderEmailPassword.Text);
            setValue("host", textBox_host.Text);
            setValue("port", numericUpDown_port.Value.ToString());
            setValue("recipientEmail", textBox_recipientAddress.Text);
            setValue("recipientName", textBox_recipientName.Text);

            this.Close();
            Application.Exit();
        }

        private void setValue(string key, string value)
        {
            System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings[key].Value = value;
            config.Save();
        }
    }
}
