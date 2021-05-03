using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using InvoiceGen.Models;
using InvoiceGen.Models.DataAccessLayer;
using InvoiceGen.Models.Repository;
using InvoiceGen.Presenter;

namespace InvoiceGen
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainWindow = new mainWindow(Configuration.APP_NAME + " v" + Configuration.APP_VERSION);
            var xmlFileHandler = new XmlFileHandler(Configuration.XML_FILE_PATH);
            var xmlService = new XmlService(xmlFileHandler, Configuration.DATE_FORMAT);
            var repository = new InvoiceRepository(xmlService);
            var newInvoiceModel = new InvoiceModel();

            var mainPresenter = new MainPresenter(mainWindow, repository, newInvoiceModel);

            // load the configuration data
            Configuration.SenderEmailAddress = ConfigurationManager.AppSettings["senderEmail"];
            Configuration.SenderName = ConfigurationManager.AppSettings["SenderName"];
            Configuration.SenderPassword = ConfigurationManager.AppSettings["SenderPassword"];
            Configuration.Host = ConfigurationManager.AppSettings["Host"];
            Configuration.port = int.Parse(ConfigurationManager.AppSettings["port"]);
            Configuration.RecipientEmailAddress = ConfigurationManager.AppSettings["recipientEmail"];
            Configuration.RecipientName = ConfigurationManager.AppSettings["recipientName"];

            try
            {
                // show the main window
                Application.Run(mainWindow);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);

                // show an error dialog and create crashlog
                string crashlogFilePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\crash.log";
                if (File.Exists(crashlogFilePath))
                {
                    File.Delete(crashlogFilePath);
                }
                using (FileStream fs = File.Create(crashlogFilePath))
                {
                    byte[] message = new UTF8Encoding(true).GetBytes(ex.Message);
                    fs.Write(message, 0, message.Length);
                    byte[] author = new UTF8Encoding(true).GetBytes(ex.StackTrace);
                    fs.Write(author, 0, author.Length);
                }
                MessageBox.Show("An unexpected error occurred: \r\r" + ex.Message + "\r\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
