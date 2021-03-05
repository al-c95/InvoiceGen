using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Net.Mail;
using InvoiceGen.View;
using InvoiceGen.Model.Repository;
using InvoiceGen.Model.ObjectModel;
using InvoiceGen.EmailService;

namespace InvoiceGen.Presenter
{
    public class MainPresenter
    {
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

            // populate months combo box
            this._view.PopulateMonthsComboBox(Months);

            this._view.CreatingNewInvoice = true;
        }

        #region View event handlers
        public void MonthlyInvoiceMonthYearUpdated(object sender, EventArgs args)
        {
            bool valid = true;
            valid = valid && this.Months.Contains(this._view.Month);
            valid = valid && Int32.TryParse(this._view.Year, out int year);

            this._view.ItemDescriptionTextBoxEnabled = valid;
            this._view.ItemAmountTextBoxEnabled = valid;
            this._view.ItemQuantityUpDownEnabled = valid;

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

            // reset the status bar
            SetStatusBarTextAndColour("Ready", StatusBarState.Ready);
        }

        public void InvoiceTypeSelected(object sender, EventArgs args)
        {
            if (this._view.RadioButtonMonthlyChecked && !this._view.RadioButtonCustomChecked)
            {
                this._view.MonthComboboxEnabled = true;
                this._view.YearTextBoxEnabled = true;

                this._view.CustomTitleTextBoxEnabled = false;

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
            else if (!this._view.RadioButtonMonthlyChecked && this._view.RadioButtonCustomChecked)
            {
                this._view.MonthComboboxEnabled = false;
                this._view.YearTextBoxEnabled = false;

                this._view.CustomTitleTextBoxEnabled = true;

                if (!string.IsNullOrWhiteSpace(this._view.CustomTitleText) && this._view.GetNumberOfItemsInList() > 0)
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
                this._view.ItemDescriptionTextBoxEnabled = true;
                this._view.ItemAmountTextBoxEnabled = true;
                this._view.ItemQuantityUpDownEnabled = true;
            }

            if (!string.IsNullOrWhiteSpace(this._view.CustomTitleText) && this._view.GetNumberOfItemsInList() > 0)
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

        private string GetNewInvoiceTitle()
        {
            string title = "";
            if (this._view.RadioButtonCustomChecked && !this._view.RadioButtonMonthlyChecked)
            {
                title = this._view.CustomTitleText;
            }
            else if (!this._view.RadioButtonCustomChecked && this._view.RadioButtonMonthlyEnabled)
            {
                title = this._view.Month + " " + this._view.Year;
            }

            return title;
        }

        public void SaveAndEmailButtonClicked(object sender, EventArgs args)
        {
            // disable controls the user shouldn't play with at this point
            DisableControlsWhilePerformingOperation();

            // first check if an invoice with this title already exists
            string title = GetNewInvoiceTitle();
            bool exists = this._repo.InvoiceWithTitleExists(title);
            if (exists)
            {
                this._view.ShowErrorDialogOk("Invoice with title: " + title + " already exists. Please choose a different title.");

                return;
            }

            // send email
            SetStatusBarTextAndColour("Sending Email", StatusBarState.InProgress);
            ExcelWriter excelWriter = new ExcelWriter(null, "Invoice: " + title, Configuration.SenderEmailAddress, Configuration.RecipientEmailAddress);
            excelWriter.AddItems(this._view.ItemsListEntries.ToList());
            // do it on a worker thread so the UI remains responsive
            BackgroundWorker sendEmailWorker = new BackgroundWorker();
            sendEmailWorker.DoWork += BeginSendEmail;
            sendEmailWorker.RunWorkerCompleted += EndSendEmail; // new invoice will be saved to record upon successful sending of email
            sendEmailWorker.RunWorkerAsync(new object[] { title, excelWriter.CloseAndGetMemoryStream() });
        }

        private void BeginSendEmail(object sender, DoWorkEventArgs args)
        {
            // unpack arguments
            object[] arguments = (object[])args.Argument;
            string title = (string)arguments[0];
            MemoryStream attachment = (MemoryStream)arguments[1];

            // send email
            EmailService.EmailService emailService = new EmailService.EmailService();
            emailService.SendInvoice("Invoice: " + title, "", attachment); // TODO: default or custom email body text?
        }

        private void EndSendEmail(object sender, RunWorkerCompletedEventArgs args)
        {
            Exception error = args.Error;
            if (error==null)
            {
                // success
                SetStatusBarTextAndColour("Sending Email", StatusBarState.CompletedSuccessfully);

                // fire the event to save the invoice to the records
                SendEmailFinished += OnSendEmailFinished;
                SendEmailFinished?.Invoke(null,null);
            }
            else
            {
                // it failed
                if (error is SmtpException)
                {
                    SetStatusBarTextAndColour("Sending Email", StatusBarState.Failed);
                }
                else
                {
                    throw error;
                }
            }
        }

        public void SaveAndExportXLSXButtonClicked(object sender, EventArgs args)
        {
            // first check if an invoice with this title already exists
            string title = GetNewInvoiceTitle();
            bool exists = this._repo.InvoiceWithTitleExists(title);
            if (exists)
            {
                this._view.ShowErrorDialogOk("Invoice with title: " + title + " already exists. Please choose a different title.");

                return;
            }

            // disable controls the user shouldn't play with at this point
            DisableControlsWhilePerformingOperation();

            // ask the user for the export directory
            string outputDir = this._view.ShowFolderPickerDialog();
            if (outputDir == null)
                return;

            // now save
            SaveToExcel(outputDir, false);
            SaveToRecords();
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
            string title = GetNewInvoiceTitle();

            // finally, write the data and save the spreadsheet
            SetStatusBarTextAndColour("Exporting Spreadsheet", StatusBarState.InProgress);
            MemoryStream ms = null;
            try
            {
                ExcelWriter excelWriter = new ExcelWriter(outputDir, title, Configuration.SenderEmailAddress, Configuration.RecipientEmailAddress);
                excelWriter.AddItems(this._view.ItemsListEntries.ToList());
                
                if (getMemoryStream)
                    ms = excelWriter.CloseAndGetMemoryStream();
                else      
                    excelWriter.CloseAndSave();
            }
            catch (Exception ex)
            {
                // it failed
                // update the status bar (and show a dialog?)
                SetStatusBarTextAndColour("Exporting Spreadsheet", StatusBarState.Failed);

                return null;
            }

            // successful
            SetStatusBarTextAndColour("Exporting Spreadsheet", StatusBarState.CompletedSuccessfully);

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

        private void SaveToRecords()
        {
            SetStatusBarTextAndColour("Saving To Records", StatusBarState.InProgress);

            try
            {
                // grab the title, and create the new invoice
                string title = GetNewInvoiceTitle();
                Invoice newInvoice = new Invoice();
                newInvoice.Title = title;

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
            catch (Exception ex)
            {
                // it failed
                SetStatusBarTextAndColour("Saving To Records", StatusBarState.Failed);

                return;
            }

            // it succeeded
            CancelButtonClicked(null, null);
            SetStatusBarTextAndColour("Saving To Records", StatusBarState.CompletedSuccessfully);
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