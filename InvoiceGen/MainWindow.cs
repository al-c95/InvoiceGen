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
        public bool CreatingNewInvoice { get; set; }

        /// <summary>
        /// Constructor for this window.
        /// </summary>
        public mainWindow(string windowTitle)
        {
            InitializeComponent();

            // set the window title
            this.WindowTitle = windowTitle;

            // subscribe to UI events
            this.exitToolStripMenuItem.Click += ((sender, args) => Application.Exit());
            this.aboutToolStripMenuItem.Click += ((sender, args) => new AboutBox().Show());
            this.configurationToolStripMenuItem.Click += ((sender, args) => new ConfigWindow().ShowDialog());
            this.button_viewSelected.Click += Button_viewSelected_Click;
            this.button_updateRecords.Click += Button_updateRecords_Click;
            this.dataGridView_invoiceHistory.SelectionChanged += DataGridView_invoiceHistory_SelectionChanged;
            this.button_addItem.Click += Button_addItem_Click;
            this.radioButton_titleCustom.CheckedChanged += RadioButton_titleCustom_CheckedChanged;
            this.radioButton_titleMonthly.CheckedChanged += RadioButton_titleMonthly_CheckedChanged;
            this.comboBox_Month.TextChanged += ComboBox_Month_TextChanged;
            this.textBox_Year.TextChanged += TextBox_Year_TextChanged;
            this.textBox_newEntryDesc.TextChanged += TextBox_newEntryDesc_TextChanged;
            this.textBox_newEntryAmt.TextChanged += TextBox_newEntryAmt_TextChanged;
            this.listView_items.SelectedIndexChanged += ListView_items_SelectedIndexChanged;
            this.button_removeItem.Click += MainWindow_removeSelectedItemButtonClicked;
            this.button_duplicateItem.Click += MainWindow_duplicateSelectedItemButtonClicked;
            this.textBox_customTitle.TextChanged += TextBox_customTitle_TextChanged;
            this.button_saveExportXL.Click += MainWindow_SaveAndExportXLSXButtonClicked;
            this.button_saveEmail.Click += Button_saveEmail_Click;
            this.button_cancel.Click += Button_cancel_Click;

            invoiceHistoryRecords = new DataTable();
            invoiceHistoryRecords.Columns.Add("ID", typeof(int));
            invoiceHistoryRecords.Columns.Add("Timestamp", typeof(DateTime));
            invoiceHistoryRecords.Columns.Add("Title", typeof(string));
            invoiceHistoryRecords.Columns.Add("Total Amount ($)", typeof(decimal));
            invoiceHistoryRecords.Columns.Add("Paid", typeof(bool));
            invoiceHistoryRecords.Columns.Add("Items", typeof(List<InvoiceItem>));
            this.dataGridView_invoiceHistory.DataSource = invoiceHistoryRecords;
            this.dataGridView_invoiceHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        #region UI event handlers
        private void button_newInvoice_Click_1(object sender, EventArgs args)
        {
            // fire the external event so the subscribed presenter can react
            NewInvoiceButtonClicked?.Invoke(sender, args);
        }

        private void TextBox_customTitle_TextChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            CustomTitleTextBoxTextChanged?.Invoke(this, e);
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

        private void Button_saveEmail_Click(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            SaveAndEmailButtonClicked?.Invoke(this, e);
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
        }

        private void TextBox_newEntryAmt_TextChanged(object sender, EventArgs args)
        {
            // fire the external event so the subscribed presenter can react;
            NewItemDetailsUpdated?.Invoke(sender, args);
        }

        private void TextBox_newEntryDesc_TextChanged(object sender, EventArgs args)
        {
            // fire the external event so the subscribed presenter can react
            NewItemDetailsUpdated?.Invoke(sender, args);
        }

        private void TextBox_Year_TextChanged(object sender, EventArgs args)
        {
            // fire the external event so the subscribed presenter can react
            MonthlyInvoiceMonthYearUpdated?.Invoke(sender, args);
        }

        private void ComboBox_Month_TextChanged(object sender, EventArgs args)
        {
            // fire the external event so the subscribed presenter can react
            MonthlyInvoiceMonthYearUpdated?.Invoke(sender, args);
        }

        private void RadioButton_titleMonthly_CheckedChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            InvoiceTypeSelected?.Invoke(this, e);
        }

        private void RadioButton_titleCustom_CheckedChanged(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            InvoiceTypeSelected?.Invoke(this, e);
        }

        private void Button_addItem_Click(object sender, EventArgs e)
        {
            // fire the external event so the subscribed presenter can react
            AddItemButtonClicked?.Invoke(this, e);
        }
        #endregion

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

        public Color StatusBarColour
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

        
        public string TotalText
        {
            get => this.richTextBox_total.Text;
            set => this.richTextBox_total.Text = value;
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

        public bool RadioButtonCustomChecked
        {
            get => this.radioButton_titleCustom.Checked;
            set => this.radioButton_titleCustom.Checked = value;
        }

        public IEnumerable<Tuple<InvoiceItem, int>> ItemsListEntries
        {
            get
            {
                foreach (ListViewItem listItem in this.listView_items.Items)
                {
                    InvoiceItem invoiceItem = new InvoiceItem
                    {
                        Amount = Decimal.Parse((listItem.SubItems[1].Text)),
                        Description = listItem.SubItems[0].Text
                    };

                    yield return Tuple.Create(invoiceItem, Int32.Parse(listItem.SubItems[2].Text));
                }
            }
        }

        private DataTable invoiceHistoryRecords;

        public IEnumerable<Invoice> InvoiceHistoryEntries
        {
            get
            {
                foreach (DataGridViewRow row in this.dataGridView_invoiceHistory.Rows)
                {
                    Invoice invoice = new Invoice
                    {
                        Id = (int)row.Cells[0].Value,
                        Timestamp = (DateTime)row.Cells[1].Value,
                        Title = (string)row.Cells[2].Value,
                        Paid = (bool)row.Cells[4].Value,
                        Items = (List<InvoiceItem>)row.Cells[5].Value
                    };

                    yield return invoice;
                }
            }

            set
            {
                DataTable dt = (DataTable)this.dataGridView_invoiceHistory.DataSource;
                dt.Rows.Clear();
                foreach (var invoice in value)
                {
                    dt.Rows.Add(new object[] { invoice.Id, invoice.Timestamp, invoice.Title, invoice.GetTotal(), false, invoice.Items });
                }
                this.dataGridView_invoiceHistory.DataSource = dt;
                this.dataGridView_invoiceHistory.Columns[4].Visible = false;
            }
        }

        public void AddEntryToItemsList(InvoiceItem item, int quantity)
        {
            string[] row = { item.Description, item.Amount.ToString("0.00"), quantity.ToString() };
            var newListItem = new ListViewItem(row);
            listView_items.Items.Add(newListItem);
        }

        public void UpdateQuantityInItemsList(InvoiceItem item, int newQuantity)
        {
            for (int index = this.listView_items.Items.Count - 1; index >= 0; index--)
            {
                if (this.listView_items.Items[index].SubItems[0].Text.Equals(item.Description) &&
                    this.listView_items.Items[index].SubItems[1].Text.Equals(item.Amount.ToString("0.00")))
                {
                    string[] row = { item.Description, item.Amount.ToString("0.00"), newQuantity.ToString() };
                    listView_items.Items[index] = new ListViewItem(row);
                }
            }
        }

        public void RemoveItemFromList(InvoiceItem toRemove)
        {
            for (int index = this.listView_items.Items.Count - 1; index >= 0; index--)
            {
                if (this.listView_items.Items[index].SubItems[0].Text.Equals(toRemove.Description) &&
                    this.listView_items.Items[index].SubItems[1].Text.Equals(toRemove.Amount.ToString()))
                {
                    listView_items.Items[index].Remove();
                }
            }
        }

        public int GetNumberOfItemsInList() => this.listView_items.Items.Count;

        public Tuple<InvoiceItem, int> GetSelectedItem()
        {
            if (this.listView_items.SelectedItems.Count != 0)
            {
                ListViewItem selected = this.listView_items.SelectedItems[0]; // only *one* item should be able to be selected
                return Tuple.Create(new InvoiceItem { Description = selected.SubItems[0].Text, Amount = Decimal.Parse(selected.SubItems[1].Text) },
                    Int32.Parse(selected.SubItems[2].Text));
            }
            else
            {
                return null;
            }
        }

        public int GetQuantityOfItemInList(InvoiceItem item)
        {
            for (int index = this.listView_items.Items.Count - 1; index >= 0; index--)
            {
                if (this.listView_items.Items[index].SubItems[0].Text.Equals(item.Description) &&
                    this.listView_items.Items[index].SubItems[1].Text.Equals(item.Amount.ToString("0.00")))
                {
                    return Int32.Parse(this.listView_items.Items[index].SubItems[2].Text);
                }
            }

            return 0;
        }

        public void ClearItemsList()
        {
            this.listView_items.Items.Clear();
        }

        public void ShowErrorDialogOk(string message)
        {
            MessageBox.Show(message, Configuration.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public void PopulateMonthsComboBox(string[] months)
        {
            // fill the Months combobox
            comboBox_Month.Items.Clear();
            foreach (string m in months)
                comboBox_Month.Items.Add(m);
            // and apply autocomplete
            comboBox_Month.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_Month.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        #region UI events
        // new invoice button
        public event EventHandler NewInvoiceButtonClicked;

        // invoice history tab controls
        public event EventHandler ViewSelectedInvoiceButtonClicked;
        public event EventHandler UpdateRecordsButtonClicked;
        public event EventHandler InvoiceHistoryDataGridViewSelectionChanged;

        // description data entry widgets
        public event EventHandler MonthlyInvoiceMonthYearUpdated;
        public event EventHandler CustomTitleTextBoxTextChanged;

        // adding item
        public event EventHandler AddItemButtonClicked;
        public event EventHandler NewItemDetailsUpdated;

        // save buttons
        public event EventHandler SaveAndEmailButtonClicked;
        public event EventHandler SaveAndExportXLSXButtonClicked;

        // cancel button
        public event EventHandler CancelClicked;

        // items list view and associated buttons
        public event EventHandler ItemListSelectedIndexChanged;
        public event EventHandler DuplicateItemButtonClicked;
        public event EventHandler RemoveItemButtonClicked;

        public event EventHandler PaidStatusChanged;
        public event EventHandler InvoiceTypeSelected;
        #endregion
    }
}