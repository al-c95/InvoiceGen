using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceGen.View
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            StringBuilder attributionBuilder = new StringBuilder();
            attributionBuilder.AppendLine("\r\nThanks To: ");
            attributionBuilder.AppendLine("Icons by Icons8: http://icons8.com");
            attributionBuilder.AppendLine("EPPlus by Jan Källman");
            attributionBuilder.AppendLine("FakeItEasy by Patrik Hägne, FakeItEasy contributors");
            attributionBuilder.AppendLine("Microsoft.NET.Test.Sdk by Microsoft");
            attributionBuilder.AppendLine("Microsoft.NETCore.App by Microsoft");
            attributionBuilder.AppendLine("NUnit by Charlie Poole, Rob Prouse");
            attributionBuilder.AppendLine("NUnit3TestAdapter by Charlie Poole, Terje Sandstrom");

            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            //this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelVersion.Text = Configuration.APP_VERSION;
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.richTextBoxDescription.Text = AssemblyDescription + "\r\n" + attributionBuilder.ToString();
            this.richTextBoxDescription.LinkClicked += ((sender, args) => System.Diagnostics.Process.Start(args.LinkText));
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        #region UI event handlers
        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
