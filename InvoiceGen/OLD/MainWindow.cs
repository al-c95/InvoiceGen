using System;
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
        bool _CreatingNewInvoice;
        public bool CreatingNewInvoice
        {
            get => _CreatingNewInvoice;
            set => _CreatingNewInvoice = value;
        }

        string[] Months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun",
            "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        DataTable invoiceHistoryRecords;

        /// <summary>
        /// Constructor for this window.
        /// </summary>
        public mainWindow(string WindowTitle)
        {
            InitializeComponent();

            // set the window title
            this.WindowTitle = WindowTitle;

            // fill the Months combobox
            foreach (string m in Months)
                comboBox_Month.Items.Add(m);
            // and apply autocomplete
            comboBox_Month.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_Month.AutoCompleteSource = AutoCompleteSource.ListItems;

            // create the invoice history table and bind it to the UI
            invoiceHistoryRecords = new DataTable("Invoices");
            invoiceHistoryRecords.Columns.Add("ID", typeof(int));
            invoiceHistoryRecords.Columns.Add("Timestamp", typeof(DateTime));
            invoiceHistoryRecords.Columns.Add("Title", typeof(string));
            invoiceHistoryRecords.Columns.Add("Total Amount ($)", typeof(decimal));
            invoiceHistoryRecords.Columns.Add("Paid", typeof(bool));
            invoiceHistoryRecords.Columns.Add("Items", typeof(IList<InvoiceItem>));
            dataGridView_invoiceHistory.DataSource = invoiceHistoryRecords;
            dataGridView_invoiceHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // subscribe to UI events

            this.button_newInvoice.Click += Button_newInvoice_Click;

            this.button_viewSelected.Click += Button_viewSelected_Click;
            this.button_updateRecords.Click += Button_updateRecords_Click;
            this.dataGridView_invoiceHistory.SelectionChanged += DataGridView_invoiceHistory_SelectionChanged;

            this.button_addItem.Click += Button_addItem_Click;

            this.radioButton_titleCustom.CheckedChanged += RadioButton_titleCustom_CheckedChanged;
            //this.CustomTitleTextBoxTextChanged += textBox_customTitle_TextChanged;

            this.radioButton_titleMonthly.CheckedChanged += RadioButton_titleMonthly_CheckedChanged;
            this.comboBox_Month.TextChanged += ComboBox_Month_TextChanged;
            this.textBox_Year.TextChanged += TextBox_Year_TextChanged;

            this.textBox_newEntryDesc.TextChanged += TextBox_newEntryDesc_TextChanged;
            this.textBox_newEntryAmt.TextChanged += TextBox_newEntryAmt_TextChanged;

            this.listView_items.SelectedIndexChanged += ListView_items_SelectedIndexChanged;
            this.button_removeItem.Click += MainWindow_removeSelectedItemButtonClicked;
            this.button_duplicateItem.Click += MainWindow_duplicateSelectedItemButtonClicked;

            this.button_saveExportXL.Click += MainWindow_SaveAndExportXLSXButtonClicked;
            this.button_saveEmail.Click += Button_saveEmail_Click;

            this.button_cancel.Click += Button_cancel_Click;

            this.exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;

            this.configurationToolStripMenuItem.Click += ConfigurationToolStripMenuItem_Click;

            this.aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;

            this.dataGridView_invoiceHistory.CellValueChanged += DataGridView_invoiceHistory_CellValueChanged;
            this.dataGridView_invoiceHistory.CellContentClick += DataGridView_invoiceHistory_CellContentClick;
        }

        #region UI event handlers
        private void DataGridView_invoiceHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView_invoiceHistory.CommitEdit(DataGridViewDataErrorContexts.Commit);
            // this will fire the CellValueChanged event
        }

        private void DataGridView_invoiceHistory_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            PaidStatusChanged?.Invoke(this, e);
        }

        private void DataGridView_invoiceHistory_SelectionChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            InvoiceHistoryDataGridViewSelectionChanged?.Invoke(this, e);
        }

        private void Button_updateRecords_Click(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            UpdateRecordsButtonClicked?.Invoke(this, e);
        }

        private void Button_viewSelected_Click(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            ViewSelectedInvoiceButtonClicked?.Invoke(this, e);
        }

        private void Button_cancel_Click(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            CancelClicked?.Invoke(this, e);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Button_saveEmail_Click(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            SaveAndEmailButtonClicked?.Invoke(this, e);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MainWindow_SaveAndExportXLSXButtonClicked(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            SaveAndExportXLSXButtonClicked?.Invoke(this, e);
        }

        private void MainWindow_duplicateSelectedItemButtonClicked(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            DuplicateItemButtonClicked?.Invoke(this, e);
        }

        private void MainWindow_removeSelectedItemButtonClicked(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            RemoveItemButtonClicked?.Invoke(this, e);
        }

        private void ListView_items_SelectedIndexChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            ItemListSelectedIndexChanged?.Invoke(this, e);

            // TODO: put this logic in the presenter
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
            NewItemAmountTextBoxTextChanged?.Invoke(this, e);
        }

        private void TextBox_newEntryDesc_TextChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            NewItemDescriptionTextBoxTextChanged?.Invoke(this, e);
        }

        private void TextBox_Year_TextChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            YearTextBoxTextChanged?.Invoke(this, e);
        }

        private void ComboBox_Month_TextChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            MonthComboBoxTextChanged?.Invoke(this, e);
        }

        private void RadioButton_titleMonthly_CheckedChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            MonthlyTitleRadioButtonClicked?.Invoke(this, e);
        }

        private void RadioButton_titleCustom_CheckedChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            CustomTitleRadioButtonClicked?.Invoke(this, e);
        }

        private void textBox_customTitle_TextChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            CustomTitleTextBoxTextChanged?.Invoke(this, e);
        }

        private void comboBox_Month_TextUpdate(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            MonthComboBoxTextChanged?.Invoke(this, e);
        }

        private void Button_newInvoice_Click(object sender, EventArgs e)
        {

        }

        private void Button_addItem_Click(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            AddItemButtonClicked?.Invoke(this, e);
        }
        #endregion

        public void clearNewInvoiceItemsList()
        {
            this.listView_items.Items.Clear();
        }

        public string getTitle()
        {
            if (this.radioButton_titleMonthly.Checked)
            {
                return comboBox_Month.Text + " " + textBox_Year.Text;
            }
            else
            {
                return this.textBox_customTitle.Text;
            }
        }
  
        public string WindowTitle
        {
            get => this.Text;
            set => this.Text = value;
        }

        public int SelectedTabIndex
        {
            get => this.tabControl.SelectedIndex;
            set => this.tabControl.SelectedIndex = value;
        }

        public string StatusBarText
        {
            get => this.toolStripStatusLabel.Text;
            set => this.toolStripStatusLabel.Text = value;
        }

        public System.Drawing.Color StatusBarColour
        {
            get => this.statusStrip.BackColor;
            set => this.statusStrip.BackColor = value;
        }

        public string SaveAndEmailButtonText
        {
            get => this.button_saveEmail.Text;
            set => this.button_saveEmail.Text = value;
        }

        public string SaveAndExportXLSXButtonText
        {
            get => this.button_saveExportXL.Text;
            set => this.button_saveExportXL.Text = value;
        }

        public bool RadioButtonMonthlyEnabled
        {
            get => this.radioButton_titleMonthly.Enabled;
            set => this.radioButton_titleMonthly.Enabled = value;
        }

        public bool RadioButtonCustomEnabled
        {
            get => this.radioButton_titleCustom.Enabled;
            set => this.radioButton_titleCustom.Enabled = value;
        }

        public bool MonthComboboxEnabled
        {
            get => this.comboBox_Month.Enabled;
            set => this.comboBox_Month.Enabled = value;
        }

        public string Month
        {
            get => this.comboBox_Month.Text;
            set => this.comboBox_Month.Text = value;
        }

        public bool YearTextBoxEnabled
        {
            get => this.textBox_Year.Enabled;
            set => this.textBox_Year.Enabled = value;
        }

        public string Year
        {
            get => textBox_Year.Text;
            set => textBox_Year.Text = value;
        }

        public bool CustomTitleTextBoxEnabled
        {
            get => this.textBox_customTitle.Enabled;
            set => this.textBox_customTitle.Enabled = value;
        }

        public string CustomTitleText
        {
            get => this.textBox_customTitle.Text;
            set => this.textBox_customTitle.Text = value;
        }

        public bool AddItemButtonEnabled
        {
            get => this.button_addItem.Enabled;
            set => this.button_addItem.Enabled = value;
        }

        public bool ItemDescriptionTextBoxEnabled
        {
            get => this.textBox_newEntryDesc.Enabled;
            set => this.textBox_newEntryDesc.Enabled = value;
        }

        public string ItemDescription
        {
            get => this.textBox_newEntryDesc.Text;
            set => this.textBox_newEntryDesc.Text = value;
        }

        public bool ItemAmountTextBoxEnabled
        {
            get => this.textBox_newEntryAmt.Enabled;
            set => this.textBox_newEntryAmt.Enabled = value;
        }

        public string ItemAmount
        {
            get => textBox_newEntryAmt.Text;
            set => this.textBox_newEntryAmt.Text = value;
        }

        public bool ItemQuantityUpDownEnabled
        {
            get => this.numericUpDown_newEntryQ.Enabled;
            set => this.numericUpDown_newEntryQ.Enabled = value;
        }

        public int ItemQuantity
        {
            get => (int)numericUpDown_newEntryQ.Value;
            set => numericUpDown_newEntryQ.Value = value;
        }

        public bool ItemsListViewEnabled
        {
            get => listView_items.Enabled;
            set => listView_items.Enabled = value;
        }

        public bool DuplicateItemButtonEnabled
        {
            get => button_duplicateItem.Enabled;
            set => button_duplicateItem.Enabled = value;
        }

        public bool RemoveItemButtonEnabled
        {
            get => button_removeItem.Enabled;
            set => button_removeItem.Enabled = value;
        }

        public bool SaveAndEmailButtonEnabled
        {
            get => button_saveEmail.Enabled;
            set => button_saveEmail.Enabled = value;
        }

        public bool SaveAndExportXLButtonEnabled
        {
            get => button_saveExportXL.Enabled;
            set => button_saveExportXL.Enabled = value;
        }

        public bool CancelButtonEnabled
        {
            get => button_cancel.Enabled;
            set => button_cancel.Enabled = value;
        }

        public bool InvoiceHistoryDataGridViewEnabled
        {
            get => dataGridView_invoiceHistory.Enabled;
            set => dataGridView_invoiceHistory.Enabled = value;
        }

        public bool RadioButtonMonthlyChecked
        {
            get => this.radioButton_titleMonthly.Checked;
            set => this.radioButton_titleMonthly.Checked = value;
        }

        public bool radioButtonCustomChecked
        {
            get => this.radioButton_titleCustom.Checked;
            set => this.radioButton_titleCustom.Checked = value;
        }

        /// <summary>
        /// The full list of invoice items displayed in the "View or Generate" tab. Quantities not specified.
        /// </summary>
        public IEnumerable<InvoiceItem> invoiceItems
        {
            get
            {
                foreach (ListViewItem item in this.listView_items.Items)
                {
                    yield return new InvoiceItem
                    {
                        description = item.SubItems[0].Text,
                        amount = decimal.Parse(item.SubItems[1].Text)
                    };
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
        /// The list of invoice items displayed in the "View or Generate" tab, that have been selected. Quantities not specified.
        /// </summary>
        public IEnumerable<InvoiceItem> selectedInvoiceItems
        {
            get
            {
                foreach (ListViewItem selectedItem in this.listView_items.SelectedItems)
                {
                    yield return new InvoiceItem
                    {
                        description = selectedItem.SubItems[0].Text,
                        amount = decimal.Parse(selectedItem.SubItems[1].Text)
                    };
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
        /// Get the number of currently-selected rows in the invoice history grid.
        /// </summary>
        public int numberSelectedInvoiceRecords
        {
            get => this.dataGridView_invoiceHistory.SelectedRows.Count;
        }

        public int selectedInvoiceID
        {
            get => (int)dataGridView_invoiceHistory.SelectedRows[0].Cells[0].Value;
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
                    decimal.Parse(existingItem.SubItems[1].Text) == item.amount)
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
                if (listItem.SubItems[0].Text.Equals(item.description) && decimal.Parse(listItem.SubItems[1].Text) == item.amount)
                {
                    return int.Parse(listItem.SubItems[2].Text);
                }
            }

            return 0;
        }

        /// <summary>
        /// Intended to remove a selected item.
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
        /// All invoices in the DataGridView in the "History" tab.
        /// </summary>
        public IEnumerable<Invoice> invoiceHistory
        {
            set
            {
                // clear it
                invoiceHistoryRecords.Rows.Clear();

                // populate it
                dataGridView_invoiceHistory.Columns["Items"].Visible = false;
                dataGridView_invoiceHistory.Columns["ID"].ReadOnly = true;
                dataGridView_invoiceHistory.Columns["Timestamp"].ReadOnly = true;
                dataGridView_invoiceHistory.Columns["Total Amount ($)"].ReadOnly = true;
                dataGridView_invoiceHistory.Columns["Title"].ReadOnly = true;
                foreach (Invoice invoice in value)
                    invoiceHistoryRecords.Rows.Add(new object[] { invoice.id, invoice.timestamp, invoice.title, invoice.getTotal(), invoice.paid, invoice.items });
                invoiceHistoryRecords.AcceptChanges();
                dataGridView_invoiceHistory.EndEdit();
            }
        }

        /// <summary>
        /// Invoice records in the DataGridView in the "History" tab which have been modified by the user.
        /// </summary>
        public IEnumerable<Invoice> modifiedInvoiceRecords
        {
            get
            {
                dataGridView_invoiceHistory.EndEdit();
                DataRowCollection modifiedRows = invoiceHistoryRecords.GetChanges(DataRowState.Modified)?.Rows;
                foreach (DataRow row in modifiedRows)
                {
                    yield return new Invoice
                    {
                        id = (int)row["ID"],
                        timestamp = (DateTime)row["Timestamp"],
                        title = (string)row["Title"],
                        paid = (bool)row["Paid"],
                        items = (List<InvoiceItem>)row["Items"]
                    };
                }
            }
        }

        public string TotalText
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

        public bool NewInvoiceButtonEnabled
        {
            get => this.button_newInvoice.Enabled;
            set => this.button_newInvoice.Enabled = value;
        }

        public bool ViewSelectedInvoiceButtonEnabled
        {
            get => this.button_viewSelected.Enabled;
            set => this.button_viewSelected.Enabled = value;
        }

        public bool UpdateRecordsButtonEnabled
        {
            get => this.button_updateRecords.Enabled;
            set => this.button_updateRecords.Enabled = value;
        }

        public bool exitToolStripMenuItemEnabled
        {
            get => this.exitToolStripMenuItem.Enabled;
            set => this.exitToolStripMenuItem.Enabled = value;
        }

        public void ShowErrorDialogOk(string message)
        {
            System.Windows.Forms.MessageBox.Show(message, Configuration.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowErrorDialogAbortRetryIgnore(string message)
        {
            throw new NotImplementedException();
        }

        public void ShowSuccessDialog(string message)
        {
            throw new NotImplementedException();
        }

        public void ShowSaveFileDialog()
        {
            throw new NotImplementedException();
        }

        public string ShowFolderPickerDialog()
        {
            FolderBrowserDialog folderPicker = new FolderBrowserDialog();
            folderPicker.ShowNewFolderButton = true;
            if (folderPicker.ShowDialog() == DialogResult.OK)
            {
                return folderPicker.SelectedPath;
            }
            else
            {
                return null; 
            }
        }

        #region UI events
        // new invoice button
        public event EventHandler NewInvoiceButtonClicked;

        // invoice history tab controls
        public event EventHandler ViewSelectedInvoiceButtonClicked;
        public event EventHandler UpdateRecordsButtonClicked;
        public event EventHandler InvoiceHistoryDataGridViewSelectionChanged;

        // title/description radio buttons
        public event EventHandler MonthlyTitleRadioButtonClicked;
        public event EventHandler CustomTitleRadioButtonClicked;

        // description data entry widgets
        public event EventHandler MonthComboBoxTextChanged;
        public event EventHandler YearTextBoxTextChanged;
        public event EventHandler CustomTitleTextBoxTextChanged;

        // adding item
        public event EventHandler AddItemButtonClicked;
        public event EventHandler NewItemDescriptionTextBoxTextChanged;
        public event EventHandler NewItemAmountTextBoxTextChanged;

        // save buttons
        public event EventHandler SaveAndEmailButtonClicked;
        public event EventHandler SaveAndExportXLSXButtonClicked;

        // cancel button
        public event EventHandler CancelClicked;

        // file menu
        public event EventHandler FileNewInvoiceMenuItemClicked;
        public event EventHandler FileLoadInvoiceMenuItemClicked;
        public event EventHandler FileSaveAndEmailMenuItemClicked;
        public event EventHandler FileSaveAndExportXLSXMenuItemClicked;
        //public event EventHandler FileExitMenuItemClicked;

        // settings menu
        //public event EventHandler SettingsConfigMenuItemClicked;

        // help menu
        /*
        public event EventHandler HelpManualMenuItemClicked;
        public event EventHandler HelpAboutMenuItemClicked;
        */

        // items list view and associated buttons
        public event EventHandler ItemListSelectedIndexChanged;
        public event EventHandler DuplicateItemButtonClicked;
        public event EventHandler RemoveItemButtonClicked;

        public event EventHandler PaidStatusChanged;
        #endregion  
    }
}
