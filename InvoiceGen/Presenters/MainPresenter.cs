using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
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
    /// Contains most of the logic which controls the UI, and interacts with the model.
    /// </summary>
    public class MainPresenter
    {
        // dependency injection
        public IMainWindow _view;
        public IInvoiceRepository _repo;

        public readonly string[] Months = new string[] { "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December" };

        /// <summary>
        /// Constructor with View and Model dependency injection.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="repository"></param>
        public MainPresenter(IMainWindow view, IInvoiceRepository repository)
        {
            // dependency injection
            this._view = view;
            this._repo = repository;

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

            // populate months combo box
            this._view.PopulateMonthsComboBox(Months);

            this._view.CreatingNewInvoice = true;

            // populate invoice history data grid
            this._view.InvoiceHistoryEntries = this._repo.GetAllInvoices();
        }

        #region View event handlers
        public void ViewSelectedInvoiceButtonClicked(object sender, EventArgs args)
        {
            // switch to the create or view invoice tab
            this._view.SelectedTabIndex = 0;
            this._view.CreatingNewInvoice = false;

            // clear titles
            this._view.Month = string.Empty;
            this._view.Year = string.Empty;
            this._view.CustomTitleText = string.Empty;

            // grab the title of the invoice and determine its type, then display it
            Invoice selected = this._view.GetSelectedInvoice();
            string title = selected.Title;
            bool startsWithMonth = false;
            foreach (var m in Months)
            {
                if (title.StartsWith(m))
                {
                    startsWithMonth = true;
                }
            }
            if (startsWithMonth)
            {
                // check if it ends with a space followed by a year
                if (Regex.IsMatch(title, @"[A-Za-z]+ \d+"))
                {
                    // a monthly invoice
                    // split it by the space between month and year
                    string[] split = title.Split(' ');
                    this._view.Month = split[0];
                    this._view.Year = split[1];
                    this._view.RadioButtonMonthlyChecked = true;
                }
                else
                {
                    // a custom-title invoice
                    this._view.CustomTitleText = title;
                    this._view.RadioButtonCustomChecked = true;
                }
            }
            else
            {
                // a custom-title invoice
                this._view.CustomTitleText = title;
                this._view.RadioButtonCustomChecked = true;
            }

            // change the text of the save buttons
            this._view.SaveAndEmailButtonText = "Email";
            this._view.SaveAndExportXLSXButtonText = "Export XLSX";

            // enable or disable appropriate controls
            this._view.RadioButtonCustomEnabled = false;
            this._view.RadioButtonMonthlyEnabled = false;
            this._view.MonthComboboxEnabled = false;
            this._view.YearTextBoxEnabled = false;
            this._view.CustomTitleTextBoxEnabled = false;
            this._view.NewInvoiceButtonEnabled = false;
            this._view.CancelButtonEnabled = true;
            this._view.SaveAndEmailButtonEnabled = true;
            this._view.SaveAndExportXLButtonEnabled = true;

            // display items
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

            // update the total
            string toDisplay = "Total: " + GetTotalAmountFromList().ToString("C2", new System.Globalization.CultureInfo("en-AU"));
            this._view.TotalText = toDisplay;
        }

        public void InvoiceHistorySelectionChanged(object sender, EventArgs args)
        {
            Invoice selected = this._view.GetSelectedInvoice();
            this._view.ViewSelectedInvoiceButtonEnabled = (selected != null);
        }

        public void UpdateRecordsButtonClicked(object sender, EventArgs args)
        {
            SetStatusBarTextAndColour("Updating Records", StatusBarState.InProgress);

            // update records
            try
            {
                foreach (var invoice in this._view.InvoiceHistoryEntries)
                {
                    this._repo.UpdatePaidStatus(invoice.Id, invoice.Paid);
                }
            }
            catch (Exception)
            {
                // it failed
                // tell the user via a dialog
                // TODO: log error
                this._view.ShowErrorDialogOk("Failed to update records");
                return;
            }
            finally
            {
                // whatever happened, reset the status bar
                SetStatusBarTextAndColour("Ready", StatusBarState.Ready);
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
            valid = valid && this.Months.Contains(this._view.Month);
            valid = valid && Int32.TryParse(this._view.Year, out int year);

            this._view.ItemDescriptionTextBoxEnabled = valid && this._view.CreatingNewInvoice;
            this._view.ItemAmountTextBoxEnabled = valid && this._view.CreatingNewInvoice;
            this._view.ItemQuantityUpDownEnabled = valid && this._view.CreatingNewInvoice;

            if (Months.Contains(this._view.Month) && Int32.TryParse(this._view.Year, out int result) && this._view.GetNumberOfItemsInList() > 0)
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

            // reset the status bar
            SetStatusBarTextAndColour("Ready", StatusBarState.Ready);
        }

        public void InvoiceTypeSelected(object sender, EventArgs args)
        {
            if (this._view.RadioButtonMonthlyChecked && !this._view.RadioButtonCustomChecked)
            {
                this._view.MonthComboboxEnabled = this._view.CreatingNewInvoice;
                this._view.YearTextBoxEnabled = this._view.CreatingNewInvoice;

                this._view.CustomTitleTextBoxEnabled = false;

                if (Months.Contains(this._view.Month) && Int32.TryParse(this._view.Year, out int result) && this._view.GetNumberOfItemsInList() > 0)
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

            // reset the status bar
            SetStatusBarTextAndColour("Ready", StatusBarState.Ready);
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
            valid = valid && Regex.IsMatch(this._view.ItemAmount, @"\d+\.(\d{2})");
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
            string toDisplay = "Total: " + GetTotalAmountFromList().ToString("C2", new System.Globalization.CultureInfo("en-AU"));
            this._view.TotalText = toDisplay;

            EnableOrDisableSaveButtons();
        }

        public void ItemListSelectedIndexChanged(object sender, EventArgs args)
        {
            // enable or disable "Duplicate" and "Remove" item buttons
            this._view.DuplicateItemButtonEnabled = (this._view.GetSelectedItem() != null);
            this._view.RemoveItemButtonEnabled = (this._view.GetSelectedItem() != null);

            // display total or current item amount
            if (this._view.GetSelectedItem() == null)
            {
                string toDisplay = "Total: " + GetAmountInDollarsToDisplay(GetTotalAmountFromList()); 
                this._view.TotalText = toDisplay;
            }
            else
            {
                var selectedListItem = this._view.GetSelectedItem();
                string toDisplay = GetAmountInDollarsToDisplay(selectedListItem.Item1.Amount);
                this._view.TotalText = toDisplay;
            }
        }

        public void DuplicateItemButtonClicked(object sender, EventArgs args)
        {
            // double the quantity of this item in the list
            Tuple<InvoiceItem, int> selectedListItem = this._view.GetSelectedItem();
            this._view.UpdateQuantityInItemsList(selectedListItem.Item1, selectedListItem.Item2*2);

            // update the total
            string toDisplay = "Total: " + GetAmountInDollarsToDisplay(GetTotalAmountFromList());
            this._view.TotalText = toDisplay;

            EnableOrDisableSaveButtons();
        }

        public void RemoveItemButtonClicked(object sender, EventArgs args)
        {
            // remove this item from the list
            Tuple<InvoiceItem, int> selectedListItem = this._view.GetSelectedItem();
            this._view.RemoveItemFromList(selectedListItem.Item1);

            // update the total
            string toDisplay = "Total: " + GetAmountInDollarsToDisplay(GetTotalAmountFromList());
            this._view.TotalText = toDisplay;

            EnableOrDisableSaveButtons();
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
            DisableControlsWhilePerformingOperation();

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

                    ReenableControlsAfterOperationCompletedOrAborted();
                    return;
                }
            }

            // show send email dialog
            EmailWindowPresenter emailWindowPresenter = new EmailWindowPresenter(new EmailWindow(title, Configuration.INVALID_INPUT_COLOUR, Configuration.SenderEmailAddress, Configuration.RecipientEmailAddress),
                                                                                 new EmailModel(Configuration.INVALID_INPUT_COLOUR));
            DialogResult emailDialogResult = emailWindowPresenter.ShowDialog();
            // send email, or cancel
            if (emailDialogResult == DialogResult.OK)
            {
                SetStatusBarTextAndColour("Sending Email", StatusBarState.InProgress);
                ExcelWriter excelWriter = new ExcelWriter(null, "Invoice: " + title, Configuration.SenderEmailAddress, Configuration.RecipientEmailAddress);
                excelWriter.AddItems(this._view.ItemsListEntries.ToList());
                SecureString password = emailWindowPresenter.View.Password;
                string from = emailWindowPresenter.View.From;
                string to = emailWindowPresenter.View.To;
                string cc = emailWindowPresenter.View.Cc;
                string bcc = emailWindowPresenter.View.Bcc;
                // do it on a background worker thread so the UI remains responsive
                BackgroundWorker sendEmailWorker = new BackgroundWorker();
                sendEmailWorker.DoWork += BeginSendEmail;
                sendEmailWorker.RunWorkerCompleted += EndSendEmail; // new invoice will be saved to records upon successful sending of email
                sendEmailWorker.RunWorkerAsync(new object[] { title, excelWriter.CloseAndGetMemoryStream(), password, from, to, cc, bcc });
            }
            else
            {
                ReenableControlsAfterOperationCompletedOrAborted();
                return;
            }
            // dispose send email dialog
            emailWindowPresenter.DisposeDialog();
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

            // send email
            EmailService emailService = new EmailService(password, from, to, cc, bcc);
            emailService.SendInvoice("Invoice: " + title, "", attachment);
        }

        private void EndSendEmail(object sender, RunWorkerCompletedEventArgs args)
        {
            Exception error = args.Error;
            if (error==null)
            {
                // success
                // tell the user via a dialog and reset the status bar
                this._view.ShowSuccessDialog("Sent email");
                SetStatusBarTextAndColour("Sending Email", StatusBarState.CompletedSuccessfully);

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
                }
            }
            else
            {
                // it failed
                // tell the user via a dialog and reset the status bar
                this._view.ShowErrorDialogOk("Error sending email");
                SetStatusBarTextAndColour("Ready", StatusBarState.Ready);
                
                ReenableControlsAfterOperationCompletedOrAborted();

                if (!(error is SmtpException))
                    throw error;
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
            DisableControlsWhilePerformingOperation();

            // ask the user for the export directory
            string outputDir = this._view.ShowFolderPickerDialog();
            if (outputDir == null)
            {
                return;
            }

            // now save
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

        private decimal GetTotalAmountFromList()
        {
            decimal total = 0;
            foreach (var listItem in this._view.ItemsListEntries)
            {
                total += listItem.Item1.Amount * listItem.Item2;
            }

            return total;
        }

        private string GetAmountInDollarsToDisplay(decimal amount) => amount.ToString("C2", new System.Globalization.CultureInfo("en-AU"));

        private void EnableOrDisableSaveButtons()
        {
            this._view.SaveAndEmailButtonEnabled = (this._view.GetNumberOfItemsInList() > 0);
            this._view.SaveAndExportXLButtonEnabled = (this._view.GetNumberOfItemsInList() > 0);
        }

        private MemoryStream SaveToExcel(string outputDir, bool getMemoryStream)
        {
            // grab the invoice title
            string title = GetInvoiceTitle();

            // finally, write the data and save the spreadsheet
            SetStatusBarTextAndColour("Exporting Spreadsheet", StatusBarState.InProgress);
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
                SetStatusBarTextAndColour("Ready", StatusBarState.Ready);
                return null;
            }

            // it succeeded
            // tell the user via a dialog and reset the status bar
            this._view.ShowSuccessDialog("Exported spreadsheet");
            SetStatusBarTextAndColour("Ready", StatusBarState.Ready);

            return ms;
        }

        private event EventHandler SendEmailFinished;
        private void OnSendEmailFinished(object sender, EventArgs args)
        {
            SaveToRecords();
        }

        private void DisableControlsWhilePerformingOperation()
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

        private void ReenableControlsAfterOperationCompletedOrAborted()
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
            SetStatusBarTextAndColour("Saving To Records", StatusBarState.InProgress);

            try
            {
                // grab the title, and create the new invoice
                string title = GetInvoiceTitle();
                Invoice newInvoice = new Invoice();
                newInvoice.Title = title;
                newInvoice.Timestamp = DateTime.Now;

                // add items to the new invoice
                foreach (var listItem in this._view.ItemsListEntries)
                {
                    InvoiceItem invoiceItem = listItem.Item1;
                    int quantity = listItem.Item2;
                    for (int i = 1; i <= quantity; i++)
                    {
                        newInvoice.Items.Add(invoiceItem);
                    }
                }

                // finally, save it to the records
                this._repo.AddInvoice(newInvoice);
            }
            catch (Exception)
            {
                // it failed
                // tell the user via a dialog and reset the status bar
                // TODO: log error
                this._view.ShowErrorDialogOk("Error saving to records");
                SetStatusBarTextAndColour("Ready", StatusBarState.Ready);

                return;
            }

            // it succeeded
            // tell the user via a dialog and reset the status bar
            SetStatusBarTextAndColour("Ready", StatusBarState.Ready);
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

        private void SetStatusBarTextAndColour(string task, StatusBarState state)
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
        }
    }
}