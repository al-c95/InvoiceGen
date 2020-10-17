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
            this._repo = repository;

            // subscribe to the view's events

            this._view.newInvoiceButtonClicked += _view_newInvoiceButtonClicked;
            this._view.addItemButtonClicked += _view_addItemButtonClicked;

            this._view.viewSelectedInvoiceButtonClicked += _view_viewSelectedInvoiceButtonClicked;
            this._view.updateRecordsButtonClicked += _view_updateRecordsButtonClicked;

            this._view.monthlyTitleRadioButtonClicked += _view_monthlyTitleRadioButtonClicked;
            this._view.customTitleRadioButtonClicked += _view_customTitleRadioButtonClicked;

            this._view.monthComboBoxTextChanged += _view_monthComboBoxTextChanged;
            this._view.yearTextBoxTextChanged += _view_yearTextBoxTextChanged;
            this._view.customTitleTextBoxTextChanged += _view_customTitleTextBoxTextChanged;

            this._view.newItemDescriptionTextBoxTextChanged += _view_newItemDescriptionTextBoxTextChanged;
            this._view.newItemAmountTextBoxTextChanged += _view_newItemAmountTextBoxTextChanged;

            this._view.itemListSelectedIndexChanged += _view_itemListSelectedIndexChanged;
            this._view.removeItemButtonClicked += _view_removeItemButtonClicked;
            this._view.duplicateItemButtonClicked += _view_duplicateSelectedItemButtonClicked;

            this._view.saveAndExportXLSXButtonClicked += _view_saveAndExportXLSXButtonClicked;
            this._view.saveAndEmailButtonClicked += _view_saveAndEmailButtonClicked;

            this._view.cancelClicked += _view_cancelClicked;

            this._view.settingsConfigMenuItemClicked += _view_settingsConfigMenuItemClicked;

            this._view.helpAboutMenuItemClicked += _view_helpAboutMenuItemClicked;

            // populate the invoice history
            this._view.invoiceHistory = this._repo.getAllInvoices();

            this._view.viewSelectedInvoiceButtonEnabled = false;
            this._view.updateRecordsButtonEnabled = false;
        }

        #region view event handlers
        private void _view_updateRecordsButtonClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _view_viewSelectedInvoiceButtonClicked(object sender, EventArgs e)
        {
            // retrieve the selected invoice
            try
            {

            }
            catch (System.IO.IOException ex)
            {

            }

            // display it in the "View or Generate" tab
            this._view.creatingNewInvoice = false;

            // change the appropriate button texts
            this._view.saveAndEmailButtonText = emailOnly;
            this._view.saveAndExportXLSXButtonText = exportOnly;
        }

        private void _view_cancelClicked(object sender, EventArgs e)
        {
            // at this point, it succeeded
            this._view.statusBarText = "Ready";
            this._view.statusBarColour = Configuration.DEFAULT_COLOUR;
            // reset
            this._view.setToReadyState();
        }

        private void _view_helpAboutMenuItemClicked(object sender, EventArgs e)
        {
            // show the about box
            AboutBox aboutBox = new AboutBox();
            aboutBox.Show();
        }

        private async void _view_saveAndEmailButtonClicked(object sender, EventArgs e)
        {
            this._view.saveAndEmailButtonEnabled = false;
            this._view.saveAndExportXLButtonEnabled = false;
            this._view.cancelButtonEnabled = false;

            // TODO: save to history

            // disable some controls that shouldn't be played with at this point (the "New Item" group and the ListView)
            this._view.itemDescriptionTextBoxEnabled = false;
            this._view.itemAmountTextBoxEnabled = false;
            this._view.itemQuantityUpDownEnabled = false;
            this._view.itemsListViewEnabled = false;
            this._view.addItemButtonEnabled = false;

            // create the spreadsheet
            // update the status
            this._view.statusBarColour = Configuration.IN_PROGRESS_COLOUR;
            this._view.statusBarText = createSpreadsheetAction + " In Progress";
            // get the data
            string title = this._view.getTitle();
            List<InvoiceItem> itemsFromList = this._view.invoiceItems.ToList();
            List<Tuple<InvoiceItem, int>> items = new List<Tuple<InvoiceItem, int>>();
            foreach (InvoiceItem i in this._view.invoiceItems)
            {
                Tuple<InvoiceItem, int> t = new Tuple<InvoiceItem, int>(i, this._view.getQuantityOfExistingItem(i));
                items.Add(t);
            }
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
                this._view.statusBarText = createSpreadsheetAction + " Failed: " + ex.Message;
                this._view.statusBarColour = Configuration.ERROR_COLOUR;

                // re-enable some controls
                this._view.itemDescriptionTextBoxEnabled = false;
                this._view.itemAmountTextBoxEnabled = false;
                this._view.itemQuantityUpDownEnabled = false;
                this._view.itemsListViewEnabled = false;

                return;
            }

            // send it an email
            // update the status
            this._view.statusBarText = sendEmailAction + " In Progress";
            this._view.statusBarColour = Configuration.IN_PROGRESS_COLOUR;
            // send it
            try
            {
                await Task.Run(() =>
                {
                    EmailService.EmailService emailService = new InvoiceGen.EmailService.EmailService();
                    emailService.sendInvoice("Invoice " + title, "", excelWriter.closeAndGetMemoryStream());
                });
            }
            catch(System.Net.Mail.SmtpException ex)
            {
                // it failed
                this._view.statusBarText = sendEmailAction + " Failed: " + ex.Message;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                this._view.statusBarColour = Configuration.ERROR_COLOUR;

                // re-enable some controls
                this._view.itemDescriptionTextBoxEnabled = false;
                this._view.itemAmountTextBoxEnabled = false;
                this._view.itemQuantityUpDownEnabled = false;
                this._view.itemsListViewEnabled = false;

                return;
            }

            // at this point, it succeeded
            this._view.statusBarText = sendEmailAction + " Completed Successfully";
            this._view.statusBarColour = Configuration.SUCCESS_COLOUR;
            // reset
            this._view.setToReadyState();
        }

        private void _view_settingsConfigMenuItemClicked(object sender, EventArgs e)
        {
            // show the config window
            ConfigWindow configWindow = new ConfigWindow();
            configWindow.ShowDialog();
        }

        private async void _view_saveAndExportXLSXButtonClicked(object sender, EventArgs e)
        {
            this._view.saveAndEmailButtonEnabled = false;
            this._view.saveAndExportXLButtonEnabled = false;
            this._view.cancelButtonEnabled = false;

            // TODO: save to history

            try
            {
                // update the status
                this._view.statusBarColour = Configuration.IN_PROGRESS_COLOUR;
                this._view.statusBarText = spreadsheetExportAction + " In Progress";

                // disable some controls that shouldn't be played with at this point (the "New Item" group and the ListView)
                this._view.itemDescriptionTextBoxEnabled = false;
                this._view.itemAmountTextBoxEnabled = false;
                this._view.itemQuantityUpDownEnabled = false;
                this._view.itemsListViewEnabled = false;
                this._view.addItemButtonEnabled = false;

                // get the folder to save it to from a folder picker dialog
                string dir = this._view.showFolderPickerDialog();

                // get the data
                string title = this._view.getTitle();
                List<InvoiceItem> itemsFromList = this._view.invoiceItems.ToList();
                List<Tuple<InvoiceItem, int>> items = new List<Tuple<InvoiceItem, int>>();
                foreach (InvoiceItem i in this._view.invoiceItems)
                {
                    Tuple<InvoiceItem, int> t = new Tuple<InvoiceItem, int>(i, this._view.getQuantityOfExistingItem(i));
                    items.Add(t);
                }

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
                this._view.showErrorDialogOk("Error saving the file.");
                this._view.statusBarText = "Error Saving File";
                this._view.statusBarColour = Configuration.ERROR_COLOUR;
                // TODO: log it

                // re-enable some controls
                this._view.itemDescriptionTextBoxEnabled = true;
                this._view.itemAmountTextBoxEnabled = true;
                this._view.itemQuantityUpDownEnabled = true;
                this._view.itemsListViewEnabled = true;

                // nothing more we can do
                return;
            }

            // at this point, it succeeded
            // inform the user
            this._view.statusBarText = spreadsheetExportAction + " Completed Successfully";
            this._view.statusBarColour = Configuration.SUCCESS_COLOUR;
            // reset
            this._view.setToReadyState();
        }

        private void _view_duplicateSelectedItemButtonClicked(object sender, EventArgs e)
        {
            // assume one item is already selected
            List<InvoiceItem> selectedItems = this._view.selectedInvoiceItems.ToList();
            int currQuantity = this._view.getQuantityOfExistingItem(selectedItems[0]);
            this._view.addItemToNewInvoice(selectedItems[0], currQuantity);
        }

        private void _view_removeItemButtonClicked(object sender, EventArgs e)
        {
            // assume one item is already selected
            List<InvoiceItem> selectedItems = this._view.selectedInvoiceItems.ToList();
            this._view.removeItemFromInvoice(selectedItems[0]);

            this._view.displayTotal();
        }

        private void _view_itemListSelectedIndexChanged(object sender, EventArgs e)
        {
            this._view.duplicateItemButtonEnabled = this._view.numberSelectedInvoiceItems == 1;
            this._view.removeItemButtonEnabled = this._view.numberSelectedInvoiceItems == 1;
        }

        private void _view_newItemAmountTextBoxTextChanged(object sender, EventArgs e)
        {
            // validate and enable adding the new item if valid
            int itemCount = this._view.invoiceItems.ToList().Count;
            if (itemCount == 0)
            {
                enableItemListAndActionControls(itemDescriptionIsValid() && itemAmountIsValid());
            }
            else
            {
                enableItemListAndActionControls(true);
            }
            this._view.addItemButtonEnabled = itemDescriptionIsValid() && itemAmountIsValid();
        }

        private void _view_newItemDescriptionTextBoxTextChanged(object sender, EventArgs e)
        {
            // validate and enable adding the new item if valid
            enableItemListAndActionControls(itemDescriptionIsValid() && itemAmountIsValid());
            this._view.addItemButtonEnabled = itemDescriptionIsValid() && itemAmountIsValid();
        }

        private void _view_customTitleRadioButtonClicked(object sender, EventArgs e)
        {
            enableNewItemGroup(false);

            // enable the custom description text box
            this._view.customTitleTextBoxEnabled = true;
            this._view.monthComboboxEnabled = false;
            this._view.yearTextBoxEnabled = false;
            
            // validate and enable the adding of items if valid
            enableNewItemGroup(customDescriptionIsValid());

            enableItemListAndActionControls(customDescriptionIsValid());
        }

        private void _view_monthlyTitleRadioButtonClicked(object sender, EventArgs e)
        {
            enableNewItemGroup(false);

            // enable the custom description text box
            this._view.customTitleTextBoxEnabled = false;
            this._view.monthComboboxEnabled = true;
            this._view.yearTextBoxEnabled = true;

            // validate and enable the adding of items if valid
            enableNewItemGroup(yearIsValid() && monthIsValid());

            enableItemListAndActionControls(yearIsValid() && monthIsValid());
        }

        private void _view_customTitleTextBoxTextChanged(object sender, EventArgs e)
        {
            // validate and enable the adding of items if valid
            enableNewItemGroup(customDescriptionIsValid());
        }

        private void _view_yearTextBoxTextChanged(object sender, EventArgs e)
        {
            // validate and enable the adding of items if valid
            enableNewItemGroup(yearIsValid() && monthIsValid());
        }

        private void _view_monthComboBoxTextChanged(object sender, EventArgs e)
        {
            enableNewItemGroup(false);

            // validate and enable the adding of items if valid
            enableNewItemGroup(yearIsValid() && monthIsValid());
        }

        /// <summary>
        /// The button to add a new item to the invoice being created is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_addItemButtonClicked(object sender, EventArgs e)
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
            this._view.newInvoiceButtonEnabled = false;

            this._view.viewSelectedInvoiceButtonEnabled = false;

            this._view.monthComboboxEnabled = true;
            this._view.radioButtonMonthlyEnabled = true;
            this._view.radioButtonCustomEnabled = true;
            this._view.yearTextBoxEnabled = true;

            this._view.cancelButtonEnabled = true;

            this._view.statusBarColour = Configuration.DEFAULT_COLOUR;
            this._view.statusBarText = "Ready";
        }
        #endregion

        #region window operations

        public bool yearIsValid()
        {
            if (string.IsNullOrEmpty(this._view.year))
                return false;

            int year;
            bool isValid = int.TryParse(this._view.year, out year);

            return isValid;
        }

        public bool monthIsValid()
        {
            string month = this._view.month;

            switch(month)
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
            return !(string.IsNullOrEmpty(this._view.customTitleText));
        }

        public bool itemDescriptionIsValid()
        {
            return !(string.IsNullOrEmpty(this._view.itemDescription));
        }

        public bool itemAmountIsValid()
        {
            if (string.IsNullOrEmpty(this._view.itemAmount))
                return false;

            decimal amount;
            bool isValid = decimal.TryParse(this._view.itemAmount, out amount);

            return isValid;
        }

        private void enableNewItemGroup(bool isEnabled)
        {
            this._view.itemDescriptionTextBoxEnabled = isEnabled;
            this._view.itemAmountTextBoxEnabled = isEnabled;
            this._view.itemQuantityUpDownEnabled = isEnabled;

            // check if there is item data entered
            // if so, enable the add item button also
            if (isEnabled)
            {
                this._view.addItemButtonEnabled = (itemDescriptionIsValid() && itemAmountIsValid());
            }
            else
            {
                this._view.addItemButtonEnabled = false;
            }
        }

        private void enableItemListAndActionControls(bool areEnabled)
        {
            this._view.itemsListViewEnabled = areEnabled;

            this._view.saveAndEmailButtonEnabled = areEnabled;
            this._view.saveAndExportXLButtonEnabled = areEnabled;
        }

        /// <summary>
        /// Gather data from the title group and add a new item record to the new invoice.
        /// </summary>
        public void addItemToNewInvoice()
        {
            // gather the data
            InvoiceItem newItem = new InvoiceItem
            {
                description = this._view.itemDescription,

                // this should have been validated by this point, so it seems ok...
                amount = decimal.Parse(this._view.itemAmount)
            };

            this._view.addItemToNewInvoice(newItem, this._view.itemQuantity);
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
