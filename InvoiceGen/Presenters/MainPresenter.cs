﻿//MIT License

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
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Net.Mail;
using System.Windows.Forms;
using System.Security;
using InvoiceGen.View;
using InvoiceGen.Models;
using InvoiceGen.Models.Repository;
using InvoiceGen.Models.ObjectModel;
using InvoiceGen.Presenters;

namespace InvoiceGen.Presenter
{
    /// <summary>
    /// Contains most of the logic which controls the main window UI, and interacts with the model.
    /// </summary>
    public class MainPresenter
    {
        public IMainWindow _view;
        public IInvoiceRepository _repo;
        public IInvoiceModel _InvoiceModel;

        /// <summary>
        /// Constructor with View and Model dependency injection.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="repository"></param>
        public MainPresenter(IMainWindow view, IInvoiceRepository repository, IInvoiceModel invoiceModel)
        {
            this._view = view;
            this._repo = repository;
            this._InvoiceModel = invoiceModel;

            // subscribe to the View's events
            this._view.InvoiceTypeSelected += InvoiceTypeSelected;
            this._view.NewInvoiceButtonClicked += NewInvoiceButtonClicked;
            this._view.CancelClicked += CancelButtonClicked;
            this._view.CustomTitleTextBoxTextChanged += CustomTitleTextChanged;
            this._view.MonthlyInvoiceMonthYearUpdated += MonthlyInvoiceMonthYearUpdated;
            this._view.NewItemDetailsUpdated += NewItemDetailsUpdated;
            this._view.AddItemButtonClicked += AddItemButtonClicked;
            this._view.ItemListSelectedIndexChanged += ItemListSelectedIndexChanged;
            this._view.DuplicateItemButtonClicked += DuplicateItemButtonClicked;
            this._view.RemoveItemButtonClicked += RemoveItemButtonClicked;
            this._view.SaveAndEmailButtonClicked += SaveAndEmailButtonClicked;
            this._view.SaveAndExportXLSXButtonClicked += SaveAndExportXLSXButtonClicked;
            this._view.PaidStatusChanged += PaidStatusChanged;
            this._view.UpdateRecordsButtonClicked += UpdateRecordsButtonClicked;
            this._view.InvoiceHistoryDataGridViewSelectionChanged += InvoiceHistorySelectionChanged;
            this._view.ViewSelectedInvoiceButtonClicked += ViewSelectedInvoiceButtonClicked;

            // enable/disable some controls initially
            this._view.NewInvoiceButtonEnabled = true;
            this._view.SaveAndEmailButtonEnabled = false;
            this._view.SaveAndExportXLButtonEnabled = false;
            this._view.CancelButtonEnabled = false;
            this._view.RadioButtonMonthlyEnabled = false;
            this._view.RadioButtonCustomEnabled = false;
            this._view.MonthComboboxEnabled = false;
            this._view.YearTextBoxEnabled = false;
            this._view.CustomTitleTextBoxEnabled = false;
            this._view.ItemDescriptionTextBoxEnabled = false;
            this._view.ItemAmountTextBoxEnabled = false;
            this._view.ItemQuantityUpDownEnabled = false;
            this._view.AddItemButtonEnabled = false;
            this._view.ItemsListViewEnabled = false;
            this._view.DuplicateItemButtonEnabled = false;
            this._view.RemoveItemButtonEnabled = false;
            this._view.ViewSelectedInvoiceButtonEnabled = false;
            this._view.UpdateRecordsButtonEnabled = false;

            this._view.PopulateMonthsComboBox(this._InvoiceModel.ValidMonths);

            this._view.CreatingNewInvoice = true;

            this._view.InvoiceHistoryEntries = this._repo.GetAllInvoices();
        }

        #region View event handlers
        public void ViewSelectedInvoiceButtonClicked(object sender, EventArgs args)
        {
            this.CancelButtonClicked(null,null);

            this._view.SelectedTabIndex = 0;
            this._view.CreatingNewInvoice = false;

            this._view.Month = string.Empty;
            this._view.Year = string.Empty;
            this._view.CustomTitleText = string.Empty;

            Invoice selected = this._view.GetSelectedInvoice();
            string title = selected.Title;

            if (this._InvoiceModel.IsMonthlyInvoice(title))
            {
                // monthly invoice - split it by the space between month and year
                string[] split = title.Split(' ');
                this._view.Month = split[0];
                this._view.Year = split[1];
                this._view.RadioButtonMonthlyChecked = true;
            }
            else
            {
                this._view.CustomTitleText = title;
                this._view.RadioButtonCustomChecked = true;
            }

            this._view.SaveAndEmailButtonText = "Email";
            this._view.SaveAndExportXLSXButtonText = "Export XLSX";

            SetControlsForViewingInvoice();

            foreach (var item in selected.Items)
            {
                bool exists = this._view.ItemsListEntries.Any(i => i.Item1.Description.Equals(item.Description) && i.Item1.Amount.Equals(item.Amount));
                if (exists)
                {
                    int currentQuantity = this._view.GetQuantityOfItemInList(item);
                    this._view.UpdateQuantityInItemsList(item, currentQuantity + 1);
                }
                else
                {
                    this._view.AddEntryToItemsList(item,1);
                }
            }

            string toDisplay = this._InvoiceModel.GetAmountToDisplayAsTotal(this._InvoiceModel.GetTotalAmountFromList(this._view.ItemsListEntries));
            this._view.TotalText = toDisplay;
        }

        public void InvoiceHistorySelectionChanged(object sender, EventArgs args)
        {
            Invoice selected = this._view.GetSelectedInvoice();
            this._view.ViewSelectedInvoiceButtonEnabled = (selected != null);
        }

        public void UpdateRecordsButtonClicked(object sender, EventArgs args)
        {
            SetStatusBar("Updating Records", StatusBarState.InProgress);

            try
            {
                foreach (var invoice in this._view.InvoiceHistoryEntries)
                {
                    this._repo.UpdatePaidStatus(invoice.Id, invoice.Paid);
                }
            }
            catch (Exception)
            {
                // TODO: log error
                this._view.ShowErrorDialogOk("Failed to update records");

                return;
            }
            finally
            {
                SetStatusBar("Ready", StatusBarState.Ready);
            }

            // it succeeded
            // reload
            this._view.UpdateRecordsButtonEnabled = false;
            this._view.ViewSelectedInvoiceButtonEnabled = false;
            this._view.InvoiceHistoryEntries = this._repo.GetAllInvoices();
            // tell the user via a dialog
            this._view.ShowSuccessDialog("Updated Records");
        }

        public void PaidStatusChanged(object sender, EventArgs args)
        {
            this._view.UpdateRecordsButtonEnabled = true;
        }

        public void MonthlyInvoiceMonthYearUpdated(object sender, EventArgs args)
        {
            bool valid = true;
            valid = valid && this._InvoiceModel.IsValidMonth(this._view.Month);
            valid = valid && Int32.TryParse(this._view.Year, out int year);

            this._view.ItemDescriptionTextBoxEnabled = valid && this._view.CreatingNewInvoice;
            this._view.ItemAmountTextBoxEnabled = valid && this._view.CreatingNewInvoice;
            this._view.ItemQuantityUpDownEnabled = valid && this._view.CreatingNewInvoice;

            if (this._InvoiceModel.IsValidMonth(this._view.Month) && 
                Int32.TryParse(this._view.Year, out int result) && 
                this._view.GetNumberOfItemsInList() > 0)
            {
                this._view.SaveAndEmailButtonEnabled = true;
                this._view.SaveAndExportXLButtonEnabled = true;
            }
            else
            {
                this._view.SaveAndEmailButtonEnabled = false;
                this._view.SaveAndExportXLButtonEnabled = false;
            }
        }

        public void CancelButtonClicked(object sender, EventArgs e)
        {
            this._view.NewInvoiceButtonEnabled = true;
            this._view.SaveAndEmailButtonEnabled = false;
            this._view.SaveAndExportXLButtonEnabled = false;
            this._view.CancelButtonEnabled = false;
            this._view.RadioButtonMonthlyEnabled = false;
            this._view.RadioButtonCustomEnabled = false;
            this._view.MonthComboboxEnabled = false;
            this._view.YearTextBoxEnabled = false;
            this._view.CustomTitleTextBoxEnabled = false;
            this._view.ItemDescriptionTextBoxEnabled = false;
            this._view.ItemAmountTextBoxEnabled = false;
            this._view.ItemQuantityUpDownEnabled = false;
            this._view.AddItemButtonEnabled = false;
            this._view.ItemsListViewEnabled = false;
            this._view.DuplicateItemButtonEnabled = false;
            this._view.RemoveItemButtonEnabled = false;
            this._view.ItemQuantity = 1;

            // clear entered data
            this._view.CustomTitleText = string.Empty;
            this._view.ItemDescription = string.Empty;
            this._view.ItemAmount = string.Empty;
            this._view.Year = string.Empty;
            this._view.Month = string.Empty;
            this._view.ClearItemsList();

            this._view.TotalText = "0.00";

            this._view.CreatingNewInvoice = true;

            this._view.SaveAndEmailButtonText = "Save and Email";
            this._view.SaveAndExportXLSXButtonText = "Save and Export XLSX";

            SetStatusBar("Ready", StatusBarState.Ready);
        }

        public void InvoiceTypeSelected(object sender, EventArgs args)
        {
            if (this._view.RadioButtonMonthlyChecked && !this._view.RadioButtonCustomChecked)
            {
                this._view.MonthComboboxEnabled = this._view.CreatingNewInvoice;
                this._view.YearTextBoxEnabled = this._view.CreatingNewInvoice;

                this._view.CustomTitleTextBoxEnabled = false;

                if (this._InvoiceModel.IsValidMonth(this._view.Month) && 
                    Int32.TryParse(this._view.Year, out int result) && 
                    this._view.GetNumberOfItemsInList() > 0)
                {
                    this._view.SaveAndEmailButtonEnabled = this._view.CreatingNewInvoice;
                    this._view.SaveAndExportXLButtonEnabled = this._view.CreatingNewInvoice;
                }
                else
                {
                    this._view.SaveAndEmailButtonEnabled = false;
                    this._view.SaveAndExportXLButtonEnabled = false;
                }
            }
            else if (!this._view.RadioButtonMonthlyChecked && this._view.RadioButtonCustomChecked)
            {
                this._view.MonthComboboxEnabled = false;
                this._view.YearTextBoxEnabled = false;

                this._view.CustomTitleTextBoxEnabled = this._view.CreatingNewInvoice;

                if (!string.IsNullOrWhiteSpace(this._view.CustomTitleText) && 
                    this._view.GetNumberOfItemsInList() > 0)
                {
                    this._view.SaveAndEmailButtonEnabled = this._view.CreatingNewInvoice;
                    this._view.SaveAndExportXLButtonEnabled = this._view.CreatingNewInvoice;
                }
                else
                {
                    this._view.SaveAndEmailButtonEnabled = false;
                    this._view.SaveAndExportXLButtonEnabled = false;
                }
            }
        }

        public void NewInvoiceButtonClicked(object sender, EventArgs args)
        {
            this._view.NewInvoiceButtonEnabled = false;
            this._view.CancelButtonEnabled = true;

            this._view.RadioButtonMonthlyEnabled = true;
            this._view.RadioButtonCustomEnabled = true;

            this._view.RadioButtonMonthlyChecked = true;
            this._view.MonthComboboxEnabled = true;
            this._view.YearTextBoxEnabled = true;

            SetStatusBar("Ready", StatusBarState.Ready);
        }

        private void SetControlsForViewingInvoice()
        {
            this._view.RadioButtonCustomEnabled = false;
            this._view.RadioButtonMonthlyEnabled = false;
            this._view.MonthComboboxEnabled = false;
            this._view.YearTextBoxEnabled = false;
            this._view.CustomTitleTextBoxEnabled = false;
            this._view.NewInvoiceButtonEnabled = false;
            this._view.CancelButtonEnabled = true;
            this._view.SaveAndEmailButtonEnabled = true;
            this._view.SaveAndExportXLButtonEnabled = true;
        }

        public void CustomTitleTextChanged(object sender, EventArgs args)
        {
            if (string.IsNullOrWhiteSpace(this._view.CustomTitleText))
            {
                this._view.ItemDescriptionTextBoxEnabled = false;
                this._view.ItemAmountTextBoxEnabled = false;
                this._view.ItemQuantityUpDownEnabled = false;
            }
            else
            {
                this._view.ItemDescriptionTextBoxEnabled = this._view.CreatingNewInvoice;
                this._view.ItemAmountTextBoxEnabled = this._view.CreatingNewInvoice;
                this._view.ItemQuantityUpDownEnabled = this._view.CreatingNewInvoice;
            }

            if (!string.IsNullOrWhiteSpace(this._view.CustomTitleText) && this._view.GetNumberOfItemsInList() > 0)
            {
                this._view.SaveAndEmailButtonEnabled = this._view.CreatingNewInvoice;
                this._view.SaveAndExportXLButtonEnabled = this._view.CreatingNewInvoice;
            }
            else
            {
                this._view.SaveAndEmailButtonEnabled = false;
                this._view.SaveAndExportXLButtonEnabled = false;
            }
        }

        public void NewItemDetailsUpdated(object sender, EventArgs args)
        {
            // perform validation of entered item details
            bool valid = true;
            valid = valid && !string.IsNullOrWhiteSpace(this._view.ItemDescription);
            valid = valid && this._InvoiceModel.AmountEntryValid(this._view.ItemAmount);
            valid = valid && this._view.ItemQuantity > 0;

            // enable or disable "Add Item" button and the items list
            this._view.AddItemButtonEnabled = valid;
            this._view.ItemsListViewEnabled = valid;
        }

        public void AddItemButtonClicked(object sender, EventArgs args)
        {
            // grab the entered data from the UI
            string description = this._view.ItemDescription;
            decimal amount = Math.Round(Decimal.Parse(this._view.ItemAmount),2);
            int quantity = this._view.ItemQuantity;

            // check if this item already exists
            // if it does, update the quantity. if not, add it.
            bool exists = this._view.ItemsListEntries.Any(i => i.Item1.Description.Equals(description) && i.Item1.Amount.Equals(amount));
            if (exists)
            {
                InvoiceItem item = new InvoiceItem { Description = description, Amount = amount };
                int currentQuantity = this._view.GetQuantityOfItemInList(item);
                this._view.UpdateQuantityInItemsList(item, currentQuantity + quantity);
            }
            else
            {
                this._view.AddEntryToItemsList(new InvoiceItem { Description = description, Amount = amount }, quantity);
            }

            // update the total
            string toDisplay = this._InvoiceModel.GetAmountToDisplayAsTotal(this._InvoiceModel.GetTotalAmountFromList(this._view.ItemsListEntries));
            this._view.TotalText = toDisplay;

            SetSaveButtonsEnabled();
        }

        public void ItemListSelectedIndexChanged(object sender, EventArgs args)
        {
            // enable or disable "Duplicate" and "Remove" item buttons
            this._view.DuplicateItemButtonEnabled = (this._view.GetSelectedItem() != null);
            this._view.RemoveItemButtonEnabled = (this._view.GetSelectedItem() != null);

            // display total or current item amount
            if (this._view.GetSelectedItem() == null)
            {
                string toDisplay = this._InvoiceModel.GetAmountToDisplayAsTotal(this._InvoiceModel.GetTotalAmountFromList(this._view.ItemsListEntries));
                this._view.TotalText = toDisplay;
            }
            else
            {
                var selectedListItem = this._view.GetSelectedItem();
                string toDisplay = this._InvoiceModel.GetAmountToDisplay(selectedListItem.Item1.Amount);
                this._view.TotalText = toDisplay;
            }
        }

        public void DuplicateItemButtonClicked(object sender, EventArgs args)
        {
            // double the quantity of this item in the list
            Tuple<InvoiceItem, int> selectedListItem = this._view.GetSelectedItem();
            this._view.UpdateQuantityInItemsList(selectedListItem.Item1, selectedListItem.Item2*2);

            // update the total
            string toDisplay = this._InvoiceModel.GetAmountToDisplayAsTotal(this._InvoiceModel.GetTotalAmountFromList(this._view.ItemsListEntries));
            this._view.TotalText = toDisplay;

            SetSaveButtonsEnabled();
        }

        public void RemoveItemButtonClicked(object sender, EventArgs args)
        {
            // remove this item from the list
            Tuple<InvoiceItem, int> selectedListItem = this._view.GetSelectedItem();
            this._view.RemoveItemFromList(selectedListItem.Item1);

            // update the total
            string toDisplay = this._InvoiceModel.GetAmountToDisplayAsTotal(this._InvoiceModel.GetTotalAmountFromList(this._view.ItemsListEntries));
            this._view.TotalText = toDisplay;

            SetSaveButtonsEnabled();
        }

        private string GetInvoiceTitle()
        {
            string title = "";
            if (this._view.RadioButtonCustomChecked && !this._view.RadioButtonMonthlyChecked)
            {
                title = this._view.CustomTitleText;
            }
            else if (!this._view.RadioButtonCustomChecked && this._view.RadioButtonMonthlyChecked)
            {
                title = this._view.Month + " " + this._view.Year;
            }

            return title;
        }

        public void SaveAndEmailButtonClicked(object sender, EventArgs args)
        {
            // disable controls the user shouldn't play with at this point
            DisableControlsDuringOperation();

            // check if an invoice with this title already exists
            // if not, grab the title
            string title = GetInvoiceTitle();
            if (this._view.CreatingNewInvoice)
            {
                bool exists = this._repo.InvoiceWithTitleExists(title);
                if (exists)
                {
                    // invoice with this title already exists
                    // tell the user via a dialog
                    this._view.ShowErrorDialogOk("Invoice with title: " + title + " already exists. Please choose a different title.");

                    EnableControlsAfterOperation();
                    return;
                }
            }

            // show send email dialog
            EmailWindow dialog = new EmailWindow(title, Configuration.INVALID_INPUT_COLOUR, Configuration.SenderEmailAddress, Configuration.RecipientEmailAddress);
            EmailWindowPresenter emailWindowPresenter = new EmailWindowPresenter(dialog,
                                                                                 new EmailModel(Configuration.INVALID_INPUT_COLOUR));
            emailWindowPresenter._view.Subject = "Invoice: " + title; // set default email subject
            if (this._view.CreatingNewInvoice)
            {
                emailWindowPresenter._view.SendButtonText = "Save and Send";
                emailWindowPresenter._view.CancelButtonText = "Cancel Save and Send";
            }
            else
            {
                emailWindowPresenter._view.SendButtonText = "Send";
                emailWindowPresenter._view.CancelButtonText = "Cancel Send";
            }
            DialogResult emailDialogResult = dialog.ShowDialog();
            // send email, or cancel
            if (emailDialogResult == DialogResult.OK)
            {
                SetStatusBar("Sending Email", StatusBarState.InProgress);
                ExcelWriter excelWriter = new ExcelWriter(null, "Invoice: " + title, Configuration.SenderEmailAddress, Configuration.RecipientEmailAddress);
                excelWriter.AddItems(this._view.ItemsListEntries.ToList());
                SecureString password = emailWindowPresenter._view.Password;
                string from = emailWindowPresenter._view.From;
                string to = emailWindowPresenter._view.To;
                string cc = emailWindowPresenter._view.Cc;
                string bcc = emailWindowPresenter._view.Bcc;
                string subject = emailWindowPresenter._view.Subject;
                string body = emailWindowPresenter._view.Body;

                // do it on a background thread to avoid blocking the UI
                BackgroundWorker sendEmailWorker = new BackgroundWorker();
                sendEmailWorker.DoWork += BeginSendEmail;
                sendEmailWorker.RunWorkerCompleted += EndSendEmail; // new invoice will be saved to records upon successful sending of email
                sendEmailWorker.RunWorkerAsync(new object[] { title, excelWriter.CloseAndGetMemoryStream(), password, from, to, cc, bcc, subject, body });
            }
            else
            {
                if (this._view.CreatingNewInvoice)
                {
                    EnableControlsAfterOperation();
                }
                else
                {
                    this._view.SaveAndEmailButtonEnabled = true;
                    this._view.SaveAndExportXLButtonEnabled = true;
                    this._view.CancelButtonEnabled = true;
                    SetStatusBar("Ready", StatusBarState.Ready);
                }
            }

            dialog.Dispose();
        }

        private void BeginSendEmail(object sender, DoWorkEventArgs args)
        {
            // unpack arguments
            object[] arguments = (object[])args.Argument;
            string title = (string)arguments[0];
            MemoryStream attachment = (MemoryStream)arguments[1];
            SecureString password = (SecureString)arguments[2];
            string from = (string)arguments[3];
            string to = (string)arguments[4];
            string cc = (string)arguments[5];
            string bcc = (string)arguments[6];
            string subject = (string)arguments[7];
            string body = (string)arguments[8];

            // send email
            EmailService emailService = new EmailService(password, from, to, cc, bcc);
            emailService.SendInvoice(subject, body, attachment);
        }

        private void EndSendEmail(object sender, RunWorkerCompletedEventArgs args)
        {
            Exception error = args.Error;
            if (error==null)
            {
                // success
                // tell the user via a dialog and reset the status bar
                this._view.ShowSuccessDialog("Sent email");
                SetStatusBar("Sending Email", StatusBarState.CompletedSuccessfully);

                if (this._view.CreatingNewInvoice)
                {
                    // fire the event to save the invoice to the records
                    SendEmailFinished += OnSendEmailFinished;
                    SendEmailFinished?.Invoke(null, null);
                }
                else
                {
                    this._view.SaveAndEmailButtonEnabled = true;
                    this._view.SaveAndExportXLButtonEnabled = true;
                    this._view.CancelButtonEnabled = true;
                    SetStatusBar("Ready", StatusBarState.Ready);
                }
            }
            else
            {
                if (error is SmtpException)
                {
                    // it failed
                    // tell the user via a dialog and reset the status bar
                    this._view.ShowErrorDialogOk("Error sending email");
                    SetStatusBar("Ready", StatusBarState.Ready);

                    EnableControlsAfterOperation();
                }
            }
        }

        public void SaveAndExportXLSXButtonClicked(object sender, EventArgs args)
        {
            string title = GetInvoiceTitle();
            if (this._view.CreatingNewInvoice)
            {
                // first check if an invoice with this title already exists
                bool exists = this._repo.InvoiceWithTitleExists(title);
                if (exists)
                {
                    this._view.ShowErrorDialogOk("Invoice with title: " + title + " already exists. Please choose a different title.");
                    return;
                }
            }

            // disable controls the user shouldn't play with at this point
            DisableControlsDuringOperation();

            // ask the user for the export directory
            string outputDir = this._view.ShowFolderPickerDialog();
            if (outputDir == null)
            {
                if (this._view.CreatingNewInvoice)
                {
                    EnableControlsAfterOperation();

                    return;
                }
                else
                {
                    this._view.SaveAndEmailButtonEnabled = true;
                    this._view.SaveAndExportXLButtonEnabled = true;
                    this._view.CancelButtonEnabled = true;
                    SetStatusBar("Ready", StatusBarState.Ready);

                    return;
                }
            }

            SaveToExcel(outputDir, false);
            if (this._view.CreatingNewInvoice)
            {
                SaveToRecords();
            }
            else
            {
                this._view.SaveAndEmailButtonEnabled = true;
                this._view.SaveAndExportXLButtonEnabled = true;
                this._view.CancelButtonEnabled = true;
            }
        }
        #endregion

        private void SetSaveButtonsEnabled()
        {
            this._view.SaveAndEmailButtonEnabled = (this._view.GetNumberOfItemsInList() > 0);
            this._view.SaveAndExportXLButtonEnabled = (this._view.GetNumberOfItemsInList() > 0);
        }

        private MemoryStream SaveToExcel(string outputDir, bool getMemoryStream)
        {
            string title = GetInvoiceTitle();

            SetStatusBar("Exporting Spreadsheet", StatusBarState.InProgress);
            MemoryStream ms = null;
            try
            {
                ExcelWriter excelWriter = new ExcelWriter(outputDir, title, Configuration.SenderEmailAddress, Configuration.RecipientEmailAddress);
                excelWriter.AddItems(this._view.ItemsListEntries.ToList());

                if (getMemoryStream)
                {
                    ms = excelWriter.CloseAndGetMemoryStream();
                }
                else
                {
                    excelWriter.CloseAndSave();
                }
            }
            catch (Exception)
            {
                // it failed
                // tell the user via a dialog and reset the status bar
                // TODO: log error
                this._view.ShowErrorDialogOk("Error exporting spreadsheet");
                SetStatusBar("Ready", StatusBarState.Ready);
                return null;
            }

            // it succeeded
            // tell the user via a dialog and reset the status bar
            this._view.ShowSuccessDialog("Exported spreadsheet");
            SetStatusBar("Ready", StatusBarState.Ready);

            return ms;
        }

        private event EventHandler SendEmailFinished;
        private void OnSendEmailFinished(object sender, EventArgs args)
        {
            SaveToRecords();
        }

        private void DisableControlsDuringOperation()
        {
            this._view.NewInvoiceButtonEnabled = false;
            this._view.RadioButtonCustomEnabled = false;
            this._view.RadioButtonMonthlyEnabled = false;
            this._view.MonthComboboxEnabled = false;
            this._view.CustomTitleTextBoxEnabled = false;
            this._view.YearTextBoxEnabled = false;
            this._view.ItemDescriptionTextBoxEnabled = false;
            this._view.ItemAmountTextBoxEnabled = false;
            this._view.ItemQuantityUpDownEnabled = false;
            this._view.DuplicateItemButtonEnabled = false;
            this._view.RemoveItemButtonEnabled = false;
            this._view.ItemsListViewEnabled = false;
            this._view.SaveAndEmailButtonEnabled = false;
            this._view.SaveAndExportXLButtonEnabled = false;
            this._view.CancelButtonEnabled = false;
        }

        private void EnableControlsAfterOperation()
        {
            this._view.NewInvoiceButtonEnabled = true;
            this._view.RadioButtonCustomEnabled = true;
            this._view.RadioButtonMonthlyEnabled = true;
            this._view.MonthComboboxEnabled = true;
            this._view.CustomTitleTextBoxEnabled = true;
            this._view.YearTextBoxEnabled = true;
            this._view.ItemDescriptionTextBoxEnabled = true;
            this._view.ItemAmountTextBoxEnabled = true;
            this._view.ItemQuantityUpDownEnabled = true;
            this._view.DuplicateItemButtonEnabled = true;
            this._view.RemoveItemButtonEnabled = true;
            this._view.ItemsListViewEnabled = true;
            this._view.SaveAndEmailButtonEnabled = true;
            this._view.SaveAndExportXLButtonEnabled = true;
            this._view.CancelButtonEnabled = true;
        }

        private void SaveToRecords()
        {
            SetStatusBar("Saving To Records", StatusBarState.InProgress);

            try
            {
                string title = GetInvoiceTitle();
                Invoice newInvoice = new Invoice();
                newInvoice.Title = title;
                newInvoice.Timestamp = DateTime.Now;

                foreach (var listItem in this._view.ItemsListEntries)
                {
                    InvoiceItem invoiceItem = listItem.Item1;
                    int quantity = listItem.Item2;
                    for (int i = 1; i <= quantity; i++)
                    {
                        newInvoice.Items.Add(invoiceItem);
                    }
                }

                this._repo.AddInvoice(newInvoice);
            }
            catch (Exception)
            {
                // it failed
                // tell the user via a dialog and reset the status bar
                // TODO: log error
                this._view.ShowErrorDialogOk("Error saving to records");
                SetStatusBar("Ready", StatusBarState.Ready);

                return;
            }

            // it succeeded
            // tell the user via a dialog and reset the status bar
            SetStatusBar("Ready", StatusBarState.Ready);
            this._view.ShowSuccessDialog("Saved to records");

            // reset the UI
            CancelButtonClicked(null, null);

            // repopulate the invoice history
            this._view.InvoiceHistoryEntries = this._repo.GetAllInvoices(); 
        }

        private enum StatusBarState
        {
            Ready,
            InProgress,
            CompletedSuccessfully,
            Failed
        }

        private void SetStatusBar(string task, StatusBarState state)
        {
            switch (state)
            {
                case StatusBarState.Ready:
                    this._view.StatusBarColour = Configuration.DEFAULT_COLOUR;
                    this._view.StatusBarText = "Ready";
                    break;
                case StatusBarState.InProgress:
                    this._view.StatusBarColour = Configuration.IN_PROGRESS_COLOUR;
                    this._view.StatusBarText = task + " In Progress";
                    break;
                case StatusBarState.CompletedSuccessfully:
                    this._view.StatusBarColour = Configuration.SUCCESS_COLOUR;
                    this._view.StatusBarText = task + " Completed Successfully";
                    break;
                case StatusBarState.Failed:
                    this._view.StatusBarColour = Configuration.ERROR_COLOUR;
                    this._view.StatusBarText = task + " Failed";
                    break;
            }
        }//SetStatusBar
    }//class
}