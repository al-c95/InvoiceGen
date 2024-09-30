//MIT License

//Copyright (c) 2020-2024

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE

using System;
using System.Collections.Generic;
using System.Drawing;
using InvoiceGen.Models.ObjectModel;

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

        // month and year selection
        bool MonthComboboxEnabled { get; set; }
        bool YearTextBoxEnabled { get; set; }

        // invoice type radio buttons
        bool RadioButtonMonthlyEnabled { get; set; }
        bool RadioButtonMonthlyChecked { get; set; }
        bool RadioButtonCustomEnabled { get; set; }
        bool RadioButtonCustomChecked { get; set; }

        // monthly invoice - year and month
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
        bool ViewSelectedInvoiceButtonEnabled { get; set; }
        bool UpdateRecordsButtonEnabled { get; set; }
        IEnumerable<Invoice> InvoiceHistoryEntries { get; set; }
        Invoice GetSelectedInvoice();

        string TotalText { get; set; }

        // new invoice items list
        IEnumerable<Tuple<InvoiceItem, int>> ItemsListEntries { get; }
        void AddEntryToItemsList(InvoiceItem item, int quantity);
        void UpdateQuantityInItemsList(InvoiceItem item, int newQuantity);
        void RemoveItemFromList(InvoiceItem toRemove);
        int GetNumberOfItemsInList();
        Tuple<InvoiceItem, int> GetSelectedItem();
        int GetQuantityOfItemInList(InvoiceItem item);
        void ClearItemsList();
        #endregion

        #region show dialog methods
        void ShowErrorDialogOk(string message);
        void ShowErrorDialogAbortRetryIgnore(string message);
        void ShowSuccessDialog(string message);
        void ShowSaveFileDialog();
        string ShowFolderPickerDialog();
        #endregion

        void PopulateMonthsComboBox(string[] months);

        #region UI event handlers
        event EventHandler NewInvoiceButtonClicked;

        event EventHandler ViewSelectedInvoiceButtonClicked;
        event EventHandler UpdateRecordsButtonClicked;
        event EventHandler InvoiceHistoryDataGridViewSelectionChanged;

        event EventHandler InvoiceTypeSelected;
        event EventHandler MonthlyInvoiceMonthYearUpdated;
        event EventHandler CustomTitleTextBoxTextChanged;

        event EventHandler AddItemButtonClicked;
        event EventHandler NewItemDetailsUpdated;

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
