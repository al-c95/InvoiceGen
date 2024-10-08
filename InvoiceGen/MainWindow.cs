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
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using InvoiceGen.View;
using InvoiceGen.Models.ObjectModel;

namespace InvoiceGen
{
    /// <summary>
    /// Main window.
    /// </summary>
    public partial class mainWindow : Form, IMainWindow
    {
        public bool CreatingNewInvoice { get; set; }

        private DataTable invoiceHistoryTable = new DataTable("Invoice History");

        /// <summary>
        /// Constructor for this window.
        /// </summary>
        public mainWindow(string windowTitle)
        {
            InitializeComponent();

            this.WindowTitle = windowTitle;

            // subscribe to UI events
            this.exitToolStripMenuItem.Click += ((sender, args) => Application.Exit());
            this.aboutToolStripMenuItem.Click += ((sender, args) => new AboutBox().Show());
            this.configurationToolStripMenuItem.Click += ((sender, args) => 
            {
                ConfigWindow dialog = new ConfigWindow();
                Presenters.ConfigWindowPresenter configWindowPresenter = new Presenters.ConfigWindowPresenter(dialog, new Models.EmailModel(Configuration.INVALID_INPUT_COLOUR));
                dialog.ShowDialog();
                dialog.Dispose();
            });
            // fire the public event so the subscribed presenter can react
            this.button_viewSelected.Click += ((sender, args) =>
            {
                this.ViewSelectedInvoiceButtonClicked?.Invoke(sender, args);
            });
            this.button_updateRecords.Click += ((sender, args) =>
            {
                this.UpdateRecordsButtonClicked?.Invoke(sender, args);
            });
            this.button_newInvoice.Click += ((sender, args) =>
            {
                this.NewInvoiceButtonClicked?.Invoke(sender, args);
            });
            this.dataGridView_invoiceHistory.SelectionChanged += ((sender, args) =>
            {
                this.InvoiceHistoryDataGridViewSelectionChanged?.Invoke(sender, args);
            });
            this.button_addItem.Click += ((sender, args) =>
            {
                this.AddItemButtonClicked?.Invoke(sender, args);
            });
            this.radioButton_titleCustom.CheckedChanged += ((sender, args) =>
            {
                this.InvoiceTypeSelected?.Invoke(sender,args);
            });
            this.radioButton_titleMonthly.CheckedChanged += ((sender, args) =>
            {
                this.InvoiceTypeSelected?.Invoke(sender, args);
            });
            this.comboBox_Month.TextChanged += ((sender, args) =>
            {
                this.MonthlyInvoiceMonthYearUpdated?.Invoke(sender, args);
            });
            this.textBox_Year.TextChanged += ((sender, args) =>
            {
                this.MonthlyInvoiceMonthYearUpdated?.Invoke(sender, args);
            });
            this.textBox_newEntryDesc.TextChanged += ((sender, args) =>
            {
                this.NewItemDetailsUpdated?.Invoke(sender, args);
            });
            this.textBox_newEntryAmt.TextChanged += ((sender, args) =>
            {
                this.NewItemDetailsUpdated?.Invoke(sender, args);
            });
            this.listView_items.SelectedIndexChanged += ((sender, args) =>
            {
                this.ItemListSelectedIndexChanged?.Invoke(sender, args);
            });
            this.button_removeItem.Click += ((sender, args) =>
            {
                this.RemoveItemButtonClicked?.Invoke(sender, args);
            });
            this.button_duplicateItem.Click += ((sender, args) =>
            {
                this.DuplicateItemButtonClicked?.Invoke(sender, args);
            });
            this.textBox_customTitle.TextChanged += ((sender, args) =>
            {
                this.CustomTitleTextBoxTextChanged?.Invoke(sender, args);
            });
            this.button_saveExportXL.Click += ((sender, args) =>
            {
                this.SaveAndExportXLSXButtonClicked(sender, args);
            });
            this.button_saveEmail.Click += ((sender, args) =>
            {
                this.SaveAndEmailButtonClicked?.Invoke(sender, args);
            });
            this.button_cancel.Click += ((sender, args) =>
            {
                this.CancelClicked?.Invoke(sender, args);
            });

            this.dataGridView_invoiceHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        #region UI event handlers
        private void dataGridView_invoiceHistory_CellContentClick(object sender, DataGridViewCellEventArgs args)
        {
            this.dataGridView_invoiceHistory.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGridView_invoiceHistory_CellValueChanged(object sender, DataGridViewCellEventArgs args)
        {
            PaidStatusChanged?.Invoke(this, args);
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

        public IEnumerable<Invoice> InvoiceHistoryEntries
        {
            get
            {
                foreach (DataGridViewRow row in this.dataGridView_invoiceHistory.Rows)
                {
                    Invoice invoice = new Invoice
                    {
                        Id = Int32.Parse(row.Cells[0].Value.ToString()),
                        Timestamp = DateTime.Parse(row.Cells[1].Value.ToString()),
                        Title = (string)row.Cells[2].Value,
                        Paid = (bool)row.Cells[4].Value
                    };

                    yield return invoice;
                }
            }

            set
            {
                invoiceHistoryTable.Columns.Clear();
                invoiceHistoryTable.Rows.Clear();
                invoiceHistoryTable.Columns.Add("Id", typeof(string));
                invoiceHistoryTable.Columns.Add("Timestamp", typeof(string));
                invoiceHistoryTable.Columns.Add("Title", typeof(string));
                invoiceHistoryTable.Columns.Add("Total Amount ($)", typeof(string));
                invoiceHistoryTable.Columns.Add("Paid", typeof(bool));
                invoiceHistoryTable.Columns.Add("Items", typeof(List<InvoiceItem>)); // invisible column
                foreach (var invoice in value)
                {
                    invoiceHistoryTable.Rows.Add(invoice.Id, invoice.Timestamp.ToString(), invoice.Title, invoice.GetTotal(), invoice.Paid, invoice.Items);
                }

                this.dataGridView_invoiceHistory.DataSource = invoiceHistoryTable;
                // user can only edit "paid" column
                this.dataGridView_invoiceHistory.Columns[0].ReadOnly = true;
                this.dataGridView_invoiceHistory.Columns[1].ReadOnly = true;
                this.dataGridView_invoiceHistory.Columns[2].ReadOnly = true;
                this.dataGridView_invoiceHistory.Columns[3].ReadOnly = true;
                this.dataGridView_invoiceHistory.Columns[4].ReadOnly = false;
            }
        }

        public Invoice GetSelectedInvoice()
        {
            if (this.dataGridView_invoiceHistory.SelectedRows.Count > 0)
            {
                int selectedId = Int32.Parse(this.dataGridView_invoiceHistory.SelectedRows[0].Cells[0].Value.ToString());
                foreach (DataRow row in this.invoiceHistoryTable.Rows)
                {
                    if ((Int32.Parse(row[0].ToString()) == selectedId))
                    {
                        return new Invoice
                        {
                            Id = selectedId,
                            Timestamp = DateTime.Parse(row[1].ToString()),
                            Title = (string)row[2],
                            Paid = (bool)row[4],
                            Items = (List<InvoiceItem>)row[5]
                        };
                    }
                }
            }

            return null;
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
            MessageBox.Show(message, Configuration.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            comboBox_Month.Items.Clear();
            foreach (string m in months)
            {
                comboBox_Month.Items.Add(m);
            }

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