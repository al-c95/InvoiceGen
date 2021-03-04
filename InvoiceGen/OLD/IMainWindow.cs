using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using InvoiceGen.Model.ObjectModel;

namespace InvoiceGen.View
{
    public interface IMainWindow
    {
        #region properties
        string WindowTitle { get; set; }
        string StatusBarText { get; set; }
        Color StatusBarColour { get; set; }
        int SelectedTabIndex { get; set; }
        bool CreatingNewInvoice { get; set; }

        // "save" buttons texts
        string SaveAndEmailButtonText { get; set; }
        string SaveAndExportXLSXButtonText { get; set; }


        bool NewInvoiceButtonEnabled { get; set; }

        bool ViewSelectedInvoiceButtonEnabled { get; set; }
        bool UpdateRecordsButtonEnabled { get; set; }

        bool YearTextBoxEnabled { get; set; }

        bool RadioButtonMonthlyEnabled { get; set; }
        bool RadioButtonMonthlyChecked { get; set; }

        bool RadioButtonCustomEnabled { get; set; }
        bool RadioButtonCustomChecked { get; set; }

        bool MonthComboboxEnabled { get; set; }

        string Month { get; set; }

        string Year { get; set; }

        bool CustomTitleTextBoxEnabled { get; set; }
        string CustomTitleText { get; set; }

        bool AddItemButtonEnabled { get; set; }

        bool ItemDescriptionTextBoxEnabled { get; set; }
        string ItemDescription { get; set; }

        bool ItemAmountTextBoxEnabled { get; set; }
        string ItemAmount { get; set; }

        bool ItemQuantityUpDownEnabled { get; set; }
        int ItemQuantity { get; set; }

        bool ItemsListViewEnabled { get; set; }
        bool DuplicateItemButtonEnabled { get; set; }
        bool RemoveItemButtonEnabled { get; set; }

        bool SaveAndEmailButtonEnabled { get; set; }
        bool SaveAndExportXLButtonEnabled { get; set; }
        bool CancelButtonEnabled { get; set; }

        bool InvoiceHistoryDataGridViewEnabled { get; set; }
        /*
        IEnumerable<Invoice> invoiceHistory { set; }
        IEnumerable<Invoice> modifiedInvoiceRecords { get; }
        int numberSelectedInvoiceRecords { get; }
        int selectedInvoiceID { get; }
        #endregion

        IEnumerable<InvoiceItem> invoiceItems { get; set; }
        IEnumerable<InvoiceItem> selectedInvoiceItems { get; }
        int numberSelectedInvoiceItems { get; }
        void addItemToNewInvoice(InvoiceItem item, int quantity);
        int getQuantityOfExistingItem(InvoiceItem item);
        void removeItemFromInvoice(InvoiceItem item);
        */

        string TotalText { get; set; }
        #endregion

        #region show dialog methods
        void ShowErrorDialogOk(string message);
        void ShowErrorDialogAbortRetryIgnore(string message);
        void ShowSuccessDialog(string message);
        void ShowSaveFileDialog();
        string ShowFolderPickerDialog();
        #endregion

        #region UI event handlers
        event EventHandler NewInvoiceButtonClicked;

        event EventHandler ViewSelectedInvoiceButtonClicked;
        event EventHandler UpdateRecordsButtonClicked;
        event EventHandler InvoiceHistoryDataGridViewSelectionChanged;

        event EventHandler MonthlyTitleRadioButtonClicked;
        event EventHandler MonthComboBoxTextChanged;
        event EventHandler YearTextBoxTextChanged;
        event EventHandler CustomTitleRadioButtonClicked;
        event EventHandler CustomTitleTextBoxTextChanged;

        event EventHandler AddItemButtonClicked;
        event EventHandler NewItemDescriptionTextBoxTextChanged;
        event EventHandler NewItemAmountTextBoxTextChanged;

        event EventHandler SaveAndEmailButtonClicked;
        event EventHandler SaveAndExportXLSXButtonClicked;

        event EventHandler CancelClicked;

        event EventHandler ItemListSelectedIndexChanged;
        event EventHandler DuplicateItemButtonClicked;
        event EventHandler RemoveItemButtonClicked;

        event EventHandler PaidStatusChanged;
        #endregion
    }
}
