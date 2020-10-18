using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InvoiceGen.Model.ObjectModel;

namespace InvoiceGen.View
{
    public interface IMainWindow
    {
        bool creatingNewInvoice { get; set; }

        string windowTitle { get; set; }
        string statusBarText { get; set; }
        System.Drawing.Color statusBarColour { get; set; }
        string saveAndEmailButtonText { get; set; }
        string saveAndExportXLSXButtonText { get; set; }

        void setToReadyState();

        bool newInvoiceButtonEnabled { get; set; }

        bool viewSelectedInvoiceButtonEnabled { get; set; }
        bool updateRecordsButtonEnabled { get; set; }

        bool yearTextBoxEnabled { get; set; }
        bool radioButtonMonthlyEnabled { get; set; }
        bool radioButtonCustomEnabled { get; set; }
        bool monthComboboxEnabled { get; set; }
        string month { get; set; }
        string year { get; set; }
        bool customTitleTextBoxEnabled { get; set; }
        string customTitleText { get; set; }
        bool addItemButtonEnabled { get; set; }
        string getTitle();

        bool itemDescriptionTextBoxEnabled { get; set; }
        string itemDescription { get; set; }
        bool itemAmountTextBoxEnabled { get; set; }
        string itemAmount { get; set; }
        bool itemQuantityUpDownEnabled { get; set; }
        int itemQuantity { get; set; }

        bool itemsListViewEnabled { get; set; }
        bool duplicateItemButtonEnabled { get; set; }
        bool removeItemButtonEnabled { get; set; }

        bool saveAndEmailButtonEnabled { get; set; }
        bool saveAndExportXLButtonEnabled { get; set; }
        bool cancelButtonEnabled { get; set; }

        bool invoiceHistoryDataGridViewEnabled { get; set; }
        IEnumerable<Invoice> invoiceHistory { set; }
        IEnumerable<Invoice> modifiedInvoiceRecords { get; }
        int numberSelectedInvoiceRecords { get; }

        IEnumerable<InvoiceItem> invoiceItems { get; set; }
        IEnumerable<InvoiceItem> selectedInvoiceItems { get; }
        int numberSelectedInvoiceItems { get; }
        void addItemToNewInvoice(InvoiceItem item, int quantity);
        int getQuantityOfExistingItem(InvoiceItem item);
        void removeItemFromInvoice(InvoiceItem item);

        string totalText { get; set; }
        void displayTotal();

        void showErrorDialogOk(string message);
        void showErrorDialogAbortRetryIgnore(string message);
        void showSuccessDialog(string message);
        void showSaveFileDialog();
        string showFolderPickerDialog();

        #region UI event handlers
        event EventHandler newInvoiceButtonClicked;

        event EventHandler viewSelectedInvoiceButtonClicked;
        event EventHandler updateRecordsButtonClicked;
        event EventHandler invoiceHistoryDataGridViewSelectionChanged;

        event EventHandler monthlyTitleRadioButtonClicked;
        event EventHandler monthComboBoxTextChanged;
        event EventHandler yearTextBoxTextChanged;
        event EventHandler customTitleRadioButtonClicked;
        event EventHandler customTitleTextBoxTextChanged;

        event EventHandler addItemButtonClicked;
        event EventHandler newItemDescriptionTextBoxTextChanged;
        event EventHandler newItemAmountTextBoxTextChanged;

        event EventHandler saveAndEmailButtonClicked;
        event EventHandler saveAndExportXLSXButtonClicked;

        event EventHandler cancelClicked;

        event EventHandler fileNewInvoiceMenuItemClicked;
        event EventHandler fileLoadInvoiceMenuItemClicked;
        event EventHandler fileSaveAndEmailMenuItemClicked;
        event EventHandler fileSaveAndExportXLSXMenuItemClicked;
        event EventHandler fileExitMenuItemClicked;

        event EventHandler settingsConfigMenuItemClicked;

        event EventHandler helpManualMenuItemClicked;
        event EventHandler helpAboutMenuItemClicked;

        event EventHandler itemListSelectedIndexChanged;
        event EventHandler duplicateItemButtonClicked;
        event EventHandler removeItemButtonClicked;

        event EventHandler paidStatusChanged;
        #endregion
    }
}
