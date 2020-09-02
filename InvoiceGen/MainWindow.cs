﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InvoiceGen.View;
using InvoiceGen.Model.ObjectModel;

namespace InvoiceGen
{
    public partial class mainWindow : Form, IMainWindow
    {
        // TODO: maybe put this in the configuration class
        // a collection of all calendar months, to fill the appropriate combobox
        protected string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun",
            "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        /// <summary>
        /// Constructor for this window.
        /// </summary>
        public mainWindow(string windowTitle)
        {
            InitializeComponent();

            // set the window title
            this.windowTitle = windowTitle;

            // fill the months combobox
            foreach (string m in months)
                comboBox_month.Items.Add(m);
            // and apply autocomplete
            comboBox_month.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_month.AutoCompleteSource = AutoCompleteSource.ListItems;

            setToReadyState();

            // subscribe to UI events

            this.button_newInvoice.Click += Button_newInvoice_Click;
            this.button_addItem.Click += Button_addItem_Click;

            this.radioButton_titleCustom.CheckedChanged += RadioButton_titleCustom_CheckedChanged;
            //this.customTitleTextBoxTextChanged += textBox_customTitle_TextChanged;

            this.radioButton_titleMonthly.CheckedChanged += RadioButton_titleMonthly_CheckedChanged;
            this.comboBox_month.TextChanged += ComboBox_month_TextChanged;
            this.textBox_year.TextChanged += TextBox_year_TextChanged;

            this.textBox_newEntryDesc.TextChanged += TextBox_newEntryDesc_TextChanged;
            this.textBox_newEntryAmt.TextChanged += TextBox_newEntryAmt_TextChanged;

            this.listView_items.SelectedIndexChanged += ListView_items_SelectedIndexChanged;
            this.button_removeItem.Click += MainWindow_removeSelectedItemButtonClicked;
            this.button_duplicateItem.Click += MainWindow_duplicateSelectedItemButtonClicked;
        }

        #region UI event handlers
        private void MainWindow_duplicateSelectedItemButtonClicked(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            duplicateItemButtonClicked?.Invoke(this, e);
        }

        private void MainWindow_removeSelectedItemButtonClicked(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            removeItemButtonClicked?.Invoke(this, e);
        }

        private void ListView_items_SelectedIndexChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            itemListSelectedIndexChanged?.Invoke(this, e);

            // TODO: put this logic in the presenter
            /*
            if (this.selectedInvoiceItems.ToList().Count == 1)
            {
                // one item selected
                displaySelectedAmount();
            }
            else if (this.selectedInvoiceItems.ToList().Count==0)
            {
                // no items are selected
                // display the total
                displayTotal();
            }
            else
            {
                // multiple items are selected
                // display the total
                displayTotal();
            }
            */
            switch (this.numberSelectedInvoiceItems)
            {
                case 1:
                    // one item selected
                    // display its amount
                    displaySelectedAmount();
                    break;
                default:
                    // no items or multiple items are selected
                    // display the total
                    displayTotal();
                    break;
            }
        }

        private void TextBox_newEntryAmt_TextChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react;
            newItemAmountTextBoxTextChanged?.Invoke(this, e);
        }

        private void TextBox_newEntryDesc_TextChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            newItemDescriptionTextBoxTextChanged?.Invoke(this, e);
        }

        private void TextBox_year_TextChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            yearTextBoxTextChanged?.Invoke(this, e);
        }

        private void ComboBox_month_TextChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            monthComboBoxTextChanged?.Invoke(this, e);
        }

        private void RadioButton_titleMonthly_CheckedChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            monthlyTitleRadioButtonClicked?.Invoke(this, e);
        }

        private void RadioButton_titleCustom_CheckedChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            customTitleRadioButtonClicked?.Invoke(this, e);
        }

        private void textBox_customTitle_TextChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            customTitleTextBoxTextChanged?.Invoke(this, e);
        }

        private void comboBox_month_TextUpdate(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            monthComboBoxTextChanged?.Invoke(this, e);
        }

        private void Button_newInvoice_Click(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            newInvoiceButtonClicked?.Invoke(this, e);
        }

        private void Button_addItem_Click(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            addItemButtonClicked?.Invoke(this, e);
        }
        #endregion

        public void setToReadyState()
        {
            statusBarText = "Ready";

            // set which controls are currently active

            button_newInvoice.Enabled = true;
            button_loadInvoice.Enabled = true;

            radioButton_titleMonthly.Checked = true;
            radioButton_titleMonthly.Enabled = false;
            radioButton_titleCustom.Checked = false;
            radioButton_titleCustom.Enabled = false;

            comboBox_month.Enabled = false;
            textBox_year.Enabled = false;
            textBox_year.Text = string.Empty;
            textBox_customTitle.Enabled = false;
            textBox_customTitle.Text = string.Empty;

            button_addItem.Enabled = false;
            textBox_newEntryDesc.Enabled = false;
            textBox_newEntryDesc.Text = string.Empty;
            textBox_newEntryAmt.Enabled = false;
            textBox_newEntryAmt.Text = string.Empty;
            numericUpDown_newEntryQ.Value = 1;
            numericUpDown_newEntryQ.Enabled = false;

            button_duplicateItem.Enabled = false;
            button_removeItem.Enabled = false;
            listView_items.Enabled = false;
            listView_items.Items.Clear();

            button_saveEmail.Enabled = false;
            button_saveExportXL.Enabled = false;

            button_cancel.Enabled = false;
        }

        public string windowTitle
        {
            get => this.Text;
            set => this.Text = value;
        }

        public string statusBarText
        {
            get => this.statusStrip.Text;
            set => this.statusStrip.Text = value;
        }

        public bool radioButtonMonthlyEnabled
        {
            get => this.radioButton_titleMonthly.Enabled;
            set => this.radioButton_titleMonthly.Enabled = value;
        }

        public bool radioButtonCustomEnabled
        {
            get => this.radioButton_titleCustom.Enabled;
            set => this.radioButton_titleCustom.Enabled = value;
        }

        public bool monthComboboxEnabled
        {
            get => this.comboBox_month.Enabled;
            set => this.comboBox_month.Enabled = value;
        }

        public string month
        {
            get => this.comboBox_month.Text;
            set => this.comboBox_month.Text = value;
        }

        public bool yearTextBoxEnabled
        {
            get => this.textBox_year.Enabled;
            set => this.textBox_year.Enabled = value;
        }

        public string year
        {
            get => textBox_year.Text;
            set => textBox_year.Text = value;
        }

        public bool customTitleTextBoxEnabled
        {
            get => this.textBox_customTitle.Enabled;
            set => this.textBox_customTitle.Enabled = value;
        }

        public string customTitleText
        {
            get => this.textBox_customTitle.Text;
            set => this.textBox_customTitle.Text = value;
        }

        public bool addItemButtonEnabled
        {
            get => this.button_addItem.Enabled;
            set => this.button_addItem.Enabled = value;
        }

        public bool itemDescriptionTextBoxEnabled
        {
            get => this.textBox_newEntryDesc.Enabled;
            set => this.textBox_newEntryDesc.Enabled = value;
        }

        public string itemDescription
        {
            get => this.textBox_newEntryDesc.Text;
            set => this.textBox_newEntryDesc.Text = value;
        }

        public bool itemAmountTextBoxEnabled
        {
            get => this.textBox_newEntryAmt.Enabled;
            set => this.textBox_newEntryAmt.Enabled = value;
        }

        public string itemAmount
        {
            get => textBox_newEntryAmt.Text;
            set => this.textBox_newEntryAmt.Text = value;
        }

        public bool itemQuantityUpDownEnabled
        {
            get => this.numericUpDown_newEntryQ.Enabled;
            set => this.numericUpDown_newEntryQ.Enabled = value;
        }

        public int itemQuantity
        {
            get => (int)numericUpDown_newEntryQ.Value;
            set => numericUpDown_newEntryQ.Value = value;
        }

        public bool itemsListViewEnabled
        {
            get => listView_items.Enabled;
            set => listView_items.Enabled = value;
        }

        public bool duplicateItemButtonEnabled
        {
            get => button_duplicateItem.Enabled;
            set => button_duplicateItem.Enabled = value;
        }

        public bool removeItemButtonEnabled
        {
            get => button_removeItem.Enabled;
            set => button_removeItem.Enabled = value;
        }

        public bool saveAndEmailButtonEnabled
        {
            get => button_saveEmail.Enabled;
            set => button_saveEmail.Enabled = value;
        }

        public bool saveAndExportXLButtonEnabled
        {
            get => button_saveExportXL.Enabled;
            set => button_saveExportXL.Enabled = value;
        }

        public bool cancelButtonEnabled
        {
            get => button_cancel.Enabled;
            set => button_cancel.Enabled = value;
        }

        /// <summary>
        /// The full list of invoice items displayed in the "View or Generate" tab.
        /// </summary>
        public IEnumerable<InvoiceItem> invoiceItems
        {
            get
            {
                foreach (ListViewItem item in this.listView_items.Items)
                {
                    for (int i = 1; i <= int.Parse(item.SubItems[2].Text); i++)
                    {
                        yield return new InvoiceItem
                        {
                            description = item.SubItems[0].Text,
                            amount = decimal.Parse(item.SubItems[1].Text)
                        };
                    }
                }
            }

            set
            {
                // clear the list
                this.listView_items.Items.Clear();

                // add the items to the list one by one
                foreach (InvoiceItem item in value)
                {
                    addItemToNewInvoice(item,1);
                }
            }
        }

        /// <summary>
        /// The list of invoice items displayed in the "View or Generate" tab, that have been selected.
        /// </summary>
        public IEnumerable<InvoiceItem> selectedInvoiceItems
        {
            get
            {
                foreach (ListViewItem selectedItem in this.listView_items.SelectedItems)
                {
                    for (int i = 1; i <= int.Parse(selectedItem.SubItems[2].Text); i++)
                    {
                        yield return new InvoiceItem
                        {
                            description = selectedItem.SubItems[0].Text,
                            amount = decimal.Parse(selectedItem.SubItems[1].Text)
                        };
                    }
                }
            }
        }

        /// <summary>
        /// Get the number of currently-selected items in the new invoice item list.
        /// </summary>
        public int numberSelectedInvoiceItems
        {
            get => this.listView_items.SelectedItems.Count;
        }

        /// <summary>
        /// Add an item to the list when creating a new invoice.
        /// </summary>
        /// <param name="item"></param>
        public void addItemToNewInvoice(InvoiceItem item, int quantity)
        {
            // check if this item is already in the list
            bool exists = false;
            foreach (ListViewItem existingItem in listView_items.Items)
            {
                if (existingItem.SubItems[0].Text.Equals(item.description, StringComparison.InvariantCulture) &&
                    decimal.Parse(existingItem.SubItems[1].Text)==item.amount)
                {
                    exists = true;

                    int currQuantity = int.Parse(existingItem.SubItems[2].Text);
                    existingItem.SubItems[2].Text = (currQuantity + quantity).ToString();
                }
            }

            if (!exists)
            {
                // create the new row and add it
                string[] row = { item.description, item.amount.ToString(), quantity.ToString() };
                var listViewItem = new ListViewItem(row);
                this.listView_items.Items.Add(listViewItem);
            }

            displayTotal();
        }

        /// <summary>
        /// Does what it says.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int getQuantityOfExistingItem(InvoiceItem item)
        {
            foreach (ListViewItem listItem in listView_items.Items)
            {
                if (listItem.SubItems[0].Text.Equals(item.description) && decimal.Parse(listItem.SubItems[1].Text)==item.amount)
                {
                    return int.Parse(listItem.SubItems[2].Text);
                }
            }

            return 0;
        }

        /// <summary>
        /// Does what it says. Intended to remove a selected item.
        /// </summary>
        /// <param name="item"></param>
        public void removeItemFromInvoice(InvoiceItem item)
        {
            for (int i = 0; i < listView_items.Items.Count;)
            {
                if (listView_items.Items[i].SubItems[0].Text.Equals(item.description, StringComparison.InvariantCulture) && 
                    decimal.Parse(listView_items.Items[i].SubItems[1].Text) == item.amount)
                {
                    // found it
                    // now deselect and remove it
                    listView_items.Items[i].Selected = false;
                    listView_items.Items[i].Remove();

                    return;
                }
                else
                {
                    i++;
                }
            }
        }

        /// <summary>
        /// The list of invoices in the DataGridView in the "History" tab.
        /// </summary>
        public IEnumerable<Invoice> invoiceHistory
        {
            set
            {
                // clear it
                dataGridView_invoiceHistory.Rows.Clear();

                // now add the invoice records to the list one by one
                foreach (Invoice invoice in value)
                {
                    object[] row = new object[] { invoice.id, invoice.timestamp, invoice.title, invoice.getTotal(), invoice.paid };
                    dataGridView_invoiceHistory.Rows.Add(row);
                }
            }
        }

        public string totalText
        {
            get => this.richTextBox_total.Text;
            set => this.richTextBox_total.Text = value;
        }

        /// <summary>
        /// Display the total amount for the invoice in the "total" text box.
        /// TODO: put this logic in the presenter
        /// </summary>
        public void displayTotal()
        {
            decimal total = 0;
            foreach (ListViewItem item in listView_items.Items)
            {
                total += decimal.Parse(item.SubItems[1].Text) * int.Parse(item.SubItems[2].Text);
            }

            string toDisplay = "Total: " + total.ToString("C2", new System.Globalization.CultureInfo("en-AU"));
            richTextBox_total.Text = toDisplay;
        }

        /// <summary>
        /// Display the amount for the currently-selected item in the "total" text box.
        /// TODO: put this logic in the presenter.
        /// </summary>
        public void displaySelectedAmount()
        {
            if (listView_items.SelectedItems.Count == 0 || listView_items.SelectedItems.Count > 1)
                return;

            // one item is selected
            ListViewItem selected = listView_items.SelectedItems[0];
            string toDisplay = (decimal.Parse(selected.SubItems[1].Text) * int.Parse(selected.SubItems[2].Text)).ToString("C2", new System.Globalization.CultureInfo("en-AU"));
            richTextBox_total.Text = toDisplay;
        }

        public bool loadInvoiceButtonEnabled
        {
            get => this.button_loadInvoice.Enabled;
            set => this.button_loadInvoice.Enabled = value;
        }

        public bool newInvoiceButtonEnabled
        {
            get => this.button_newInvoice.Enabled;
            set => this.button_newInvoice.Enabled = value;
        }

        public void showErrorDialogOk(string message)
        {
            throw new NotImplementedException();
        }

        public void showErrorDialogAbortRetryIgnore(string message)
        {
            throw new NotImplementedException();
        }

        public void showSuccessDialog(string message)
        {
            throw new NotImplementedException();
        }

        public void showSaveFileDialog()
        {
            throw new NotImplementedException();
        }

        #region UI events
        // new and load buttons
        public event EventHandler newInvoiceButtonClicked;
        public event EventHandler loadInvoieButtonClicked;

        // title/description radio buttons
        public event EventHandler monthlyTitleRadioButtonClicked;
        public event EventHandler customTitleRadioButtonClicked;

        // description data entry widgets
        public event EventHandler monthComboBoxTextChanged;
        public event EventHandler yearTextBoxTextChanged;
        public event EventHandler customTitleTextBoxTextChanged;

        // adding item
        public event EventHandler addItemButtonClicked;
        public event EventHandler newItemDescriptionTextBoxTextChanged;
        public event EventHandler newItemAmountTextBoxTextChanged;

        // save buttons
        public event EventHandler saveAndEmailButtonClicked;
        public event EventHandler saveAndExportXLSXButtonClicked;

        // cancel button
        public event EventHandler cancelClicked;

        // file menu
        public event EventHandler fileNewInvoiceMenuItemClicked;
        public event EventHandler fileLoadInvoiceMenuItemClicked;
        public event EventHandler fileSaveAndEmailMenuItemClicked;
        public event EventHandler fileSaveAndExportXLSXMenuItemClicked;
        public event EventHandler fileExitMenuItemClicked;

        // settings menu
        public event EventHandler settingsConfigMenuItemClicked;

        // help menu
        public event EventHandler helpManualMenuItemClicked;
        public event EventHandler helpAboutMenuItemClicked;

        // items list view and associated buttons
        public event EventHandler itemListSelectedIndexChanged;
        public event EventHandler duplicateItemButtonClicked;
        public event EventHandler removeItemButtonClicked;
        #endregion  
    }
}
