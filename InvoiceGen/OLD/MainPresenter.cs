using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceGen.View;
using InvoiceGen.Model.Repository;
using InvoiceGen.Model.ObjectModel;
using InvoiceGen.EmailService;

namespace InvoiceGen.Presenter
{
    public class MainPresenter
    {
        // dependency-injected values
        public IMainWindow _view;
        public IInvoiceRepository _repo;

        // action labels for status/progress reporting
        string spreadsheetExportAction = "Exporting Spreadsheet";
        string createSpreadsheetAction = "Creating Spreadsheet";
        string sendEmailAction = "Sending Email";
        string retrievingRecordAction = "Retrieving Record";
        string retrievingRecordsAction = "Retrieving Records";
        string savingToRecordsAction = "Saving to Records";
        string updateRecordsAction = "Updating Records";

        // button labels
        string saveAndEmail = "Save and Email";
        string emailOnly = "Email";
        string saveAndExport = "Save and Export XLSX";
        string exportOnly = "Export XLSX";

        /// <summary>
        /// Constructor with model and view dependency injection.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="repository"></param>
        public MainPresenter(IMainWindow view, IInvoiceRepository repository)
        {
            // set the injected values
            this._view = view;
            clearViewOrGenerateTab();
            this._repo = repository;

            // subscribe to the view's events

            this._view.NewInvoiceButtonClicked += _view_newInvoiceButtonClicked;
            this._view.AddItemButtonClicked += _view_AddItemButtonClicked;

            this._view.ViewSelectedInvoiceButtonClicked += _view_ViewSelectedInvoiceButtonClicked;
            this._view.UpdateRecordsButtonClicked += _view_UpdateRecordsButtonClicked;

            this._view.MonthlyTitleRadioButtonClicked += _view_MonthlyTitleRadioButtonClicked;
            this._view.CustomTitleRadioButtonClicked += _view_CustomTitleRadioButtonClicked;

            this._view.MonthComboBoxTextChanged += _view_MonthComboBoxTextChanged;
            this._view.YearTextBoxTextChanged += _view_YearTextBoxTextChanged;
            this._view.CustomTitleTextBoxTextChanged += _view_CustomTitleTextBoxTextChanged;

            this._view.AddItemButtonClicked += _view_AddItemButtonClicked;
            this._view.NewItemAmountTextBoxTextChanged += _view_NewItemAmountTextBoxTextChanged;

            this._view.ItemListSelectedIndexChanged += _view_ItemListSelectedIndexChanged;
            this._view.RemoveItemButtonClicked += _view_RemoveItemButtonClicked;
            this._view.DuplicateItemButtonClicked += _view_duplicateSelectedItemButtonClicked;

            this._view.SaveAndExportXLSXButtonClicked += _view_SaveAndExportXLSXButtonClicked;
            this._view.SaveAndEmailButtonClicked += _view_SaveAndEmailButtonClicked;

            this._view.CancelClicked += _view_CancelClicked;

            // populate the invoice history
            this._view.invoiceHistory = this._repo.getAllInvoices();

            this._view.InvoiceHistoryDataGridViewSelectionChanged += _view_InvoiceHistoryDataGridViewSelectionChanged;
            this._view.PaidStatusChanged += _view_PaidStatusChanged;

            this._view.ViewSelectedInvoiceButtonEnabled = false;
            this._view.UpdateRecordsButtonEnabled = false;
        }

        private void clearViewOrGenerateTab()
        {
            this._view.SaveAndEmailButtonText = saveAndEmail;
            this._view.SaveAndExportXLSXButtonText = saveAndExport;

            this._view.NewInvoiceButtonEnabled = true;
 
            this._view.RadioButtonMonthlyChecked = true;
            this._view.RadioButtonMonthlyEnabled = false;
            this._view.RadioButtonCustomChecked = false;
            this._view.RadioButtonCustomEnabled = false;

            this._view.MonthComboboxEnabled = false;
            this._view.YearTextBoxEnabled = false;
            this._view.Year = string.Empty;
            this._view.CustomTitleTextBoxEnabled = false;
            this._view.CustomTitleText = string.Empty;

            this._view.AddItemButtonEnabled = false;
            this._view.ItemDescriptionTextBoxEnabled = false;
            this._view.ItemDescription = string.Empty;
            this._view.ItemAmountTextBoxEnabled = false;
            this._view.ItemAmount = string.Empty;
            this._view.ItemQuantityUpDownEnabled = false;
            this._view.ItemQuantity = 1;

            this._view.clearNewInvoiceItemsList();
            
            this._view.DuplicateItemButtonEnabled = false;
            this._view.RemoveItemButtonEnabled = false;
            this._view.ItemsListViewEnabled = false;

            this._view.SaveAndEmailButtonEnabled = false;
            this._view.SaveAndExportXLButtonEnabled = false;

            this._view.CancelButtonEnabled = false;

            this._view.TotalText = "0.00";

            this._view.StatusBarText = "Ready";
            this._view.StatusBarColour = System.Drawing.Color.LightGray;

            this._view.CreatingNewInvoice = false;
        }

        #region view event handlers
        private void _view_PaidStatusChanged(object sender, EventArgs e)
        {
            // if one row is selected, enable the update button
            this._view.UpdateRecordsButtonEnabled = (this._view.numberSelectedInvoiceRecords == 1);
        }

        private void _view_InvoiceHistoryDataGridViewSelectionChanged(object sender, EventArgs e)
        {
            // if one row is selected, enable the view button
            this._view.ViewSelectedInvoiceButtonEnabled = (this._view.numberSelectedInvoiceRecords == 1);
        }

        private void _view_UpdateRecordsButtonClicked(object sender, EventArgs e)
        {
            // disable controls the user shouldn't play with now
            // namely the DataGridView and update button
            this._view.UpdateRecordsButtonEnabled = false;
            this._view.InvoiceHistoryDataGridViewEnabled = false;

            // update the status bar
            this._view.StatusBarColour = Configuration.IN_PROGRESS_COLOUR;
            this._view.StatusBarText = updateRecordsAction + " In Progress";

            // get the modified records 
            IEnumerable<Invoice> modifiedInvoices = this._view.modifiedInvoiceRecords;
            try
            {
                foreach (Invoice invoice in modifiedInvoices)
                    // update the paid status in the XML
                    this._repo.updatePaidStatus(invoice.id, invoice.paid);
            }
            catch (System.IO.IOException ioEx)
            {
                // something bad happened while saving the file
                // tell the user via the status bar
                this._view.StatusBarColour = Configuration.ERROR_COLOUR;
                this._view.StatusBarText = updateRecordsAction + " Failed";
            }
            // no need to reload the records

            // re-enable the controls, whatever happened
            this._view.UpdateRecordsButtonEnabled = false;
            this._view.InvoiceHistoryDataGridViewEnabled = true;

            // update the status bar
            this._view.StatusBarColour = Configuration.SUCCESS_COLOUR;
            this._view.StatusBarText = updateRecordsAction + " Completed Successfully";
        }

        private void _view_ViewSelectedInvoiceButtonClicked(object sender, EventArgs e)
        {
            // retrieve the selected invoice from the loaded records
            int id = this._view.selectedInvoiceID;
            Invoice selectedInvoice = this._repo.getInvoiceById(id);

            // display it in the "View or Generate" tab
            this._view.CreatingNewInvoice = false;
            this._view.SelectedTabIndex = 0;

            // change the appropriate button texts and enable/disable appropriate controls
            this._view.SaveAndEmailButtonText = emailOnly;
            this._view.SaveAndExportXLSXButtonText = exportOnly;
            this._view.NewInvoiceButtonEnabled = false;
            this._view.CancelButtonEnabled = true;
        }

        private void _view_CancelClicked(object sender, EventArgs e)
        {
            clearViewOrGenerateTab();
        }

        private void _view_helpAboutMenuItemClicked(object sender, EventArgs e)
        {
            // show the about box
            AboutBox aboutBox = new AboutBox();
            aboutBox.Show();
        }

        private async void _view_SaveAndEmailButtonClicked(object sender, EventArgs e)
        {
            this._view.SaveAndEmailButtonEnabled = false;
            this._view.SaveAndExportXLButtonEnabled = false;
            this._view.CancelButtonEnabled = false;

            // disable some controls that shouldn't be played with at this point (the "New Item" group and the ListView)
            this._view.ItemDescriptionTextBoxEnabled = false;
            this._view.ItemAmountTextBoxEnabled = false;
            this._view.ItemQuantityUpDownEnabled = false;
            this._view.ItemsListViewEnabled = false;
            this._view.AddItemButtonEnabled = false;

            // update the status
            this._view.StatusBarColour = Configuration.IN_PROGRESS_COLOUR;
            this._view.StatusBarText = savingToRecordsAction + " In Progress";

            // get the data
            string title = this._view.getTitle();
            List<InvoiceItem> itemsFromList = this._view.invoiceItems.ToList();
            List<Tuple<InvoiceItem, int>> items = new List<Tuple<InvoiceItem, int>>();
            foreach (InvoiceItem i in this._view.invoiceItems)
            {
                Tuple<InvoiceItem, int> t = new Tuple<InvoiceItem, int>(i, this._view.getQuantityOfExistingItem(i));
                items.Add(t);
            }

            // save to records
            try
            {
                Invoice invoice = new Invoice();
                invoice.timestamp = DateTime.Now;
                foreach (var i in items)
                {
                    for (int j = 1; j <= i.Item2; j++)
                    {
                        InvoiceItem invoiceItem = new InvoiceItem
                        {
                            description = i.Item1.description,
                            amount = i.Item1.amount
                        };
                        invoice.items.Add(invoiceItem);
                        invoice.title = title;
                    }
                }
                this._repo.addInvoice(invoice);
                // repopulate the invoice history
                this._view.invoiceHistory = this._repo.getAllInvoices();
            }
            catch (Exception ex)
            {
                // error saving to records
                // tell the user
                this._view.StatusBarText = "Error Saving To Records";
                this._view.StatusBarColour = Configuration.ERROR_COLOUR;

                // re-enable some controls

                this._view.ItemDescriptionTextBoxEnabled = true;
                this._view.ItemAmountTextBoxEnabled = true;
                this._view.ItemQuantityUpDownEnabled = true;
                this._view.ItemsListViewEnabled = true;

                // nothing more can do
                return;
            }

            // update the status
            this._view.StatusBarColour = Configuration.IN_PROGRESS_COLOUR;
            this._view.StatusBarText = createSpreadsheetAction + " In Progress";

            // create it
            ExcelWriter excelWriter = null;
            try
            {
                await Task.Run(() =>
                {
                    // write and save spreadsheet
                    excelWriter = new ExcelWriter(null, title, Configuration.senderEmailAddress, Configuration.recipientEmailAddress);
                    excelWriter.addItems(items);
                });
            }
            catch (Exception ex)
            {
                // it failed
                this._view.StatusBarText = createSpreadsheetAction + " Failed: " + ex.Message;
                this._view.StatusBarColour = Configuration.ERROR_COLOUR;

                // re-enable some controls
                this._view.ItemDescriptionTextBoxEnabled = false;
                this._view.ItemAmountTextBoxEnabled = false;
                this._view.ItemQuantityUpDownEnabled = false;
                this._view.ItemsListViewEnabled = false;

                return;
            }

            // send it an email
            // update the status
            this._view.StatusBarText = sendEmailAction + " In Progress";
            this._view.StatusBarColour = Configuration.IN_PROGRESS_COLOUR;

            // send it
            try
            {
                await Task.Run(() =>
                {
                    EmailService.EmailService emailService = new EmailService.EmailService();
                    emailService.sendInvoice("Invoice " + title, "", excelWriter.closeAndGetMemoryStream());
                });
            }
            catch(System.Net.Mail.SmtpException ex)
            {
                // it failed
                // tell the user
                this._view.StatusBarText = sendEmailAction + " Failed: " + ex.Message;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                this._view.StatusBarColour = Configuration.ERROR_COLOUR;
                // TODO: log it

                // re-enable some controls
                this._view.ItemDescriptionTextBoxEnabled = false;
                this._view.ItemAmountTextBoxEnabled = false;
                this._view.ItemQuantityUpDownEnabled = false;
                this._view.ItemsListViewEnabled = false;

                return;
            }

            // clear 
            clearViewOrGenerateTab();
            // at this point, it succeeded
            this._view.StatusBarText = sendEmailAction + " Completed Successfully";
            this._view.StatusBarColour = Configuration.SUCCESS_COLOUR;
        }

        private void _view_settingsConfigMenuItemClicked(object sender, EventArgs e)
        {
            // show the config window
            ConfigWindow configWindow = new ConfigWindow();
            configWindow.ShowDialog();
        }

        private async void _view_SaveAndExportXLSXButtonClicked(object sender, EventArgs e)
        {
            this._view.SaveAndEmailButtonEnabled = false;
            this._view.SaveAndExportXLButtonEnabled = false;
            this._view.CancelButtonEnabled = false;

            // update the status
            this._view.StatusBarColour = Configuration.IN_PROGRESS_COLOUR;
            this._view.StatusBarText = savingToRecordsAction + " In Progress";

            // disable some controls that shouldn't be played with at this point (the "New Item" group and the ListView)
            this._view.ItemDescriptionTextBoxEnabled = false;
            this._view.ItemAmountTextBoxEnabled = false;
            this._view.ItemQuantityUpDownEnabled = false;
            this._view.ItemsListViewEnabled = false;
            this._view.AddItemButtonEnabled = false;

            // get the folder to save it to from a folder picker dialog
            string dir = this._view.ShowFolderPickerDialog();

            // get the data
            string title = this._view.getTitle();
            List<InvoiceItem> itemsFromList = this._view.invoiceItems.ToList();
            List<Tuple<InvoiceItem, int>> items = new List<Tuple<InvoiceItem, int>>();
            foreach (InvoiceItem i in this._view.invoiceItems)
            {
                Tuple<InvoiceItem, int> t = new Tuple<InvoiceItem, int>(i, this._view.getQuantityOfExistingItem(i));
                items.Add(t);
            }

            // save to history records
            try
            {
                Invoice invoice = new Invoice();
                invoice.timestamp = DateTime.Now;
                foreach (var i in items)
                {
                    for (int j = 1; j <= i.Item2; j++)
                    {
                        InvoiceItem invoiceItem = new InvoiceItem
                        {
                            description = i.Item1.description,
                            amount = i.Item1.amount
                        };
                        invoice.items.Add(invoiceItem);
                        invoice.title = title;
                    }
                }
                this._repo.addInvoice(invoice);
                // repopulate the invoice history
                this._view.invoiceHistory = this._repo.getAllInvoices();
            }
            catch (Exception ex)
            {
                // error saving to records
                // tell the user
                this._view.StatusBarText = "Error Saving To Records";
                this._view.StatusBarColour = Configuration.ERROR_COLOUR;

                // re-enable some controls
                this._view.ItemDescriptionTextBoxEnabled = true;
                this._view.ItemAmountTextBoxEnabled = true;
                this._view.ItemQuantityUpDownEnabled = true;
                this._view.ItemsListViewEnabled = true;

                // nothing more can do
                return;
            }

            // update the status
            this._view.StatusBarColour = Configuration.IN_PROGRESS_COLOUR;
            this._view.StatusBarText = spreadsheetExportAction + " In Progress";

            try
            { 
                await Task.Run(() =>
                {
                    // write and save spreadsheet
                    ExcelWriter excelWriter = new ExcelWriter(dir, title, Configuration.senderEmailAddress, Configuration.recipientEmailAddress); 
                    excelWriter.addItems(items);
                    excelWriter.close();
                });
            }
            catch (System.IO.IOException ex)
            {
                // error saving the file
                // inform the user
                this._view.StatusBarText = "Error Saving File";
                this._view.StatusBarColour = Configuration.ERROR_COLOUR;
                // TODO: log it

                // re-enable some controls
                this._view.ItemDescriptionTextBoxEnabled = true;
                this._view.ItemAmountTextBoxEnabled = true;
                this._view.ItemQuantityUpDownEnabled = true;
                this._view.ItemsListViewEnabled = true;

                // nothing more we can do
                return;
            }

            // at this point, it succeeded
            // clear
            clearViewOrGenerateTab();
            // inform the user
            this._view.StatusBarText = spreadsheetExportAction + " Completed Successfully";
            this._view.StatusBarColour = Configuration.SUCCESS_COLOUR;
        }

        private void _view_duplicateSelectedItemButtonClicked(object sender, EventArgs e)
        {
            // assume one item is already selected
            List<InvoiceItem> selectedItems = this._view.selectedInvoiceItems.ToList();
            int currQuantity = this._view.getQuantityOfExistingItem(selectedItems[0]);
            this._view.addItemToNewInvoice(selectedItems[0], currQuantity);
        }

        private void _view_RemoveItemButtonClicked(object sender, EventArgs e)
        {
            // assume one item is already selected
            List<InvoiceItem> selectedItems = this._view.selectedInvoiceItems.ToList();
            this._view.removeItemFromInvoice(selectedItems[0]);

            this._view.displayTotal();
        }

        private void _view_ItemListSelectedIndexChanged(object sender, EventArgs e)
        {
            this._view.DuplicateItemButtonEnabled = this._view.numberSelectedInvoiceItems == 1;
            this._view.RemoveItemButtonEnabled = this._view.numberSelectedInvoiceItems == 1;
        }

        private void _view_NewItemAmountTextBoxTextChanged(object sender, EventArgs e)
        {
            // validate and enable adding the new item if valid
            int itemCount = this._view.invoiceItems.ToList().Count;
            if (itemCount == 0)
            {
                enableItemListAndActionControls(ItemDescriptionIsValid() && ItemAmountIsValid());
            }
            else
            {
                enableItemListAndActionControls(true);
            }
            this._view.AddItemButtonEnabled = ItemDescriptionIsValid() && ItemAmountIsValid();
        }

        private void _view_AddItemButtonClicked(object sender, EventArgs e)
        {
            // validate and enable adding the new item if valid
            enableItemListAndActionControls(ItemDescriptionIsValid() && ItemAmountIsValid());
            this._view.AddItemButtonEnabled = ItemDescriptionIsValid() && ItemAmountIsValid();
        }

        private void _view_CustomTitleRadioButtonClicked(object sender, EventArgs e)
        {
            enableNewItemGroup(false);

            // enable the custom description text box
            this._view.CustomTitleTextBoxEnabled = true;
            this._view.MonthComboboxEnabled = false;
            this._view.YearTextBoxEnabled = false;
            
            // validate and enable the adding of items if valid
            enableNewItemGroup(customDescriptionIsValid());

            enableItemListAndActionControls(customDescriptionIsValid());
        }

        private void _view_MonthlyTitleRadioButtonClicked(object sender, EventArgs e)
        {
            enableNewItemGroup(false);

            // enable the custom description text box
            this._view.CustomTitleTextBoxEnabled = false;
            this._view.MonthComboboxEnabled = true;
            this._view.YearTextBoxEnabled = true;

            // validate and enable the adding of items if valid
            enableNewItemGroup(YearIsValid() && MonthIsValid());

            enableItemListAndActionControls(YearIsValid() && MonthIsValid());
        }

        private void _view_CustomTitleTextBoxTextChanged(object sender, EventArgs e)
        {
            // validate and enable the adding of items if valid
            enableNewItemGroup(customDescriptionIsValid());
        }

        private void _view_YearTextBoxTextChanged(object sender, EventArgs e)
        {
            // validate and enable the adding of items if valid
            enableNewItemGroup(YearIsValid() && MonthIsValid());
        }

        private void _view_MonthComboBoxTextChanged(object sender, EventArgs e)
        {
            enableNewItemGroup(false);

            // validate and enable the adding of items if valid
            enableNewItemGroup(YearIsValid() && MonthIsValid());
        }

        /// <summary>
        /// The button to add a new item to the invoice being created is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_AddItemButtonClicked(object sender, EventArgs e)
        {
            addItemToNewInvoice();
        }

        /// <summary>
        /// "New" invoice button in the view clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_newInvoiceButtonClicked(object sender, EventArgs e)
        {
            // change the appropriate button texts

            this._view.NewInvoiceButtonEnabled = false;

            this._view.ViewSelectedInvoiceButtonEnabled = false;

            this._view.MonthComboboxEnabled = true;
            this._view.RadioButtonMonthlyEnabled = true;
            this._view.RadioButtonCustomEnabled = true;
            this._view.YearTextBoxEnabled = true;

            this._view.CancelButtonEnabled = true;

            this._view.StatusBarColour = Configuration.DEFAULT_COLOUR;
            this._view.StatusBarText = "Ready";
        }
        #endregion

        #region window operations

        public bool YearIsValid()
        {
            if (string.IsNullOrEmpty(this._view.Year))
                return false;

            int Year;
            bool isValid = int.TryParse(this._view.Year, out Year);

            return isValid;
        }

        public bool MonthIsValid()
        {
            string Month = this._view.Month;

            switch(Month)
            {
                case "Jan":
                case "Feb":
                case "Mar":
                case "Apr":
                case "May":
                case "Jun":
                case "Jul":
                case "Aug":
                case "Sep":
                case "Oct":
                case "Nov":
                case "Dec":

                    return true;

                default:

                    return false;
            }
        }

        public bool customDescriptionIsValid()
        {
            return !(string.IsNullOrEmpty(this._view.CustomTitleText));
        }

        public bool ItemDescriptionIsValid()
        {
            return !(string.IsNullOrEmpty(this._view.ItemDescription));
        }

        public bool ItemAmountIsValid()
        {
            if (string.IsNullOrEmpty(this._view.ItemAmount))
                return false;

            decimal amount;
            bool isValid = decimal.TryParse(this._view.ItemAmount, out amount);

            return isValid;
        }

        private void enableNewItemGroup(bool isEnabled)
        {
            this._view.ItemDescriptionTextBoxEnabled = isEnabled;
            this._view.ItemAmountTextBoxEnabled = isEnabled;
            this._view.ItemQuantityUpDownEnabled = isEnabled;

            // check if there is item data entered
            // if so, enable the add item button also
            if (isEnabled)
            {
                this._view.AddItemButtonEnabled = (ItemDescriptionIsValid() && ItemAmountIsValid());
            }
            else
            {
                this._view.AddItemButtonEnabled = false;
            }
        }

        private void enableItemListAndActionControls(bool areEnabled)
        {
            this._view.ItemsListViewEnabled = areEnabled;

            this._view.SaveAndEmailButtonEnabled = areEnabled;
            this._view.SaveAndExportXLButtonEnabled = areEnabled;
        }

        /// <summary>
        /// Gather data from the title group and add a new item record to the new invoice.
        /// </summary>
        public void addItemToNewInvoice()
        {
            // gather the data
            InvoiceItem newItem = new InvoiceItem
            {
                description = this._view.ItemDescription,

                // this should have been validated by this point, so it seems ok...
                amount = decimal.Parse(this._view.ItemAmount)
            };

            this._view.addItemToNewInvoice(newItem, this._view.ItemQuantity);
        }
        #endregion

        #region data operations
        /// <summary>
        /// Retrieve all the invoice records, and tell the view to display them.
        /// </summary>
        public void populateInvoiceHistory()
        {
            this._view.invoiceHistory = this._repo.getAllInvoices();
        }

        public void addNewInvoiceToRecords()
        {
            // add it to the records

            // then re-populate the history in the "History" tab
        }

        /// <summary>
        /// Set the "paid" status of an invoice to "true".
        /// </summary>
        /// <param name="id"></param>
        public void setInvoicePaid(int id)
        {
            this._repo.updatePaidStatus(id, true);
        }
        #endregion
    }
}
