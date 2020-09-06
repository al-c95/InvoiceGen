using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceGen.View;
using InvoiceGen.Model.Repository;
using InvoiceGen.Model.ObjectModel;

namespace InvoiceGen.Presenter
{
    public class MainPresenter
    {
        // dependency-injected values
        public IMainWindow _view;
        public IInvoiceRepository _repo;

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
        }
 
        #region view event handlers
        private void _view_saveAndExportXLSXButtonClicked(object sender, EventArgs e)
        {
            // TODO: save to history

            // export spreadsheet
            // get the folder to save it to
            string dir = this._view.showFolderPickerDialog();
            // get the data
            List<Tuple<InvoiceItem, int>> items = new List<Tuple<InvoiceItem, int>>();
            foreach (InvoiceItem i in this._view.invoiceItems)
            {
                Tuple<InvoiceItem, int> t = new Tuple<InvoiceItem, int>(i, this._view.getQuantityOfExistingItem(i));
                items.Add(t);
            }
            // save the spreadsheet
            ExcelWriter excelWriter = new ExcelWriter(dir, this._view.getTitle(), "me", "you"); // TODO: get sender and recipient from config
            excelWriter.addItems(items);
            try
            {
                excelWriter.close();
            }
            catch (System.IO.IOException ex)
            {
                // error saving the file
                this._view.showErrorDialogOk("Error saving the file.");
                // TODO: log it
            }
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
            switch (this._view.numberSelectedInvoiceItems)
            {
                case 0:
                    // no entry is selected
                    // disable the "duplicate" and "remove" item buttons
                    this._view.duplicateItemButtonEnabled = false;
                    this._view.removeItemButtonEnabled = false;
                    break;
                case 1:
                    // one entry is selected
                    // enable the "duplicate" and "remove" item buttons
                    this._view.duplicateItemButtonEnabled = true;
                    this._view.removeItemButtonEnabled = true;
                    break;
                default:
                    // multiple entries are selected
                    // enable the "duplicate" and "remove" item buttons
                    this._view.duplicateItemButtonEnabled = true;
                    this._view.removeItemButtonEnabled = true;
                    break;
            }
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
            this._view.loadInvoiceButtonEnabled = false;

            this._view.monthComboboxEnabled = true;
            this._view.radioButtonMonthlyEnabled = true;
            this._view.radioButtonCustomEnabled = true;
            this._view.yearTextBoxEnabled = true;

            this._view.cancelButtonEnabled = true;
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
                if (itemDescriptionIsValid() && itemAmountIsValid())
                {
                    this._view.addItemButtonEnabled = true;
                }
                else
                {
                    this._view.addItemButtonEnabled = false;
                }
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

        public void addNewInvoice()
        {

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
