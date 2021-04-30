using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
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
                // useful for debugging
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);

                // TODO: log the crash
                // TODO: show an error dialog/window
            }
        }
    }
}
