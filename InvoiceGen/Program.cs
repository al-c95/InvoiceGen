using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            var mainWindow = new mainWindow(Configuration.APP_NAME + " v" + Configuration.VERSION);
            var xmlFileHandler = new InvoiceGen.Model.DataAccessLayer.XmlFileHandler(Configuration.XML_FILE_PATH);
            var xmlService = new InvoiceGen.Model.DataAccessLayer.XmlService(xmlFileHandler);
            var repository = new InvoiceGen.Model.Repository.InvoiceRepository(xmlService);
            var mainPresenter = new InvoiceGen.Presenter.MainPresenter(mainWindow, repository);

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
            }
        }
    }
}
