using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

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
            var xmlFileHandler = new InvoiceGen.Model.DataAccessLayer.XmlFileHandler(Configuration.XML_FILE_PATH);
            var xmlService = new InvoiceGen.Model.DataAccessLayer.XmlService(xmlFileHandler);
            var repository = new InvoiceGen.Model.Repository.InvoiceRepository(xmlService);
            var mainPresenter = new InvoiceGen.Presenter.MainPresenter(mainWindow, repository);

            // load the configuration data
            Configuration.senderEmailAddress = ConfigurationManager.AppSettings["senderEmail"];
            Configuration.senderName = ConfigurationManager.AppSettings["senderName"];
            Configuration.senderPassword = ConfigurationManager.AppSettings["senderPassword"];
            Configuration.host = ConfigurationManager.AppSettings["host"];
            Configuration.port = int.Parse(ConfigurationManager.AppSettings["port"]);
            Configuration.recipientEmailAddress = ConfigurationManager.AppSettings["recipientEmail"];
            Configuration.recipientName = ConfigurationManager.AppSettings["recipientName"];

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

                // TODO: show an error dialog/window
            }
        }
    }
}
