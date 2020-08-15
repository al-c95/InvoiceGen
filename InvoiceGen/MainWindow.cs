using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceGen
{
    public partial class mainWindow : Form
    {
        bool _editable;
        bool editable
        {
            get { return _editable; }
            set
            {
                this._editable = value;
            }
        }

        string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun",
            "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};

        /// <summary>
        /// Constructor for this window.
        /// </summary>
        public mainWindow()
        {
            InitializeComponent();

            // fill the months combobox
            foreach (string m in months)
                comboBox_month.Items.Add(m);
            // and apply autocomplete
            comboBox_month.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_month.AutoCompleteSource = AutoCompleteSource.ListItems;

            // initial check the "monthly" option for title
            radioButton_titleMonthly.Checked = true;
            textBox_customTitle.Enabled = false;

            // initially disable the "cancel" button
            button_cancel.Enabled = false;

            newItemGroupActivated = false;

            itemsListControlsActivated = false;
        }

        bool _titleGroupActivated;
        bool titleGroupActivated
        {
            get { return this._titleGroupActivated; }
            set
            {
                foreach (Control control in titleGroup.Controls)
                    control.Enabled = value;

                _titleGroupActivated = value;

                /*
                if (value == true)
                {
                    foreach (Control control in titleGroup.Controls)
                        control.Enabled = true;
                }
                else if (value == false)
                {
                    foreach (Control control in titleGroup.Controls)
                        control.Enabled = false;
                }
                */
            }
        }

        bool _newItemGroupActivated;
        bool newItemGroupActivated
        {
            get { return this._newItemGroupActivated; }
            set
            {
                if (value == true)
                {
                    foreach (Control control in newItemGroup.Controls)
                        control.Enabled = true;
                }
                else if(value == false)
                {
                    foreach (Control control in newItemGroup.Controls)
                        control.Enabled = false;
                }

                _newItemGroupActivated = value;
            }
        }

        bool _itemsListControlsActivated;
        bool itemsListControlsActivated
        {
            get { return this._itemsListControlsActivated; }
            set
            {
                if (value == true)
                {
                    
                }
                else if (value == false)
                {
                    listView_items.Enabled = false;
                    button_duplicateItem.Enabled = false;
                    button_removeItem.Enabled = false;

                    button_saveEmail.Enabled = false;
                    button_saveExportXL.Enabled = false;
                }

                _itemsListControlsActivated = value;
            }
        }

        #region UI event handlers
        private void radioButton_titleMonthly_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_month.Enabled = (sender as RadioButton).Checked;
            textBox_year.Enabled = (sender as RadioButton).Checked;
        }

        private void radioButton_titleCustom_CheckedChanged(object sender, EventArgs e)
        {
            textBox_customTitle.Enabled = (sender as RadioButton).Checked;
        }

        private void comboBox_month_TextChanged(object sender, EventArgs e)
        {
            if (!months.Any(str => str.Contains(comboBox_month.Text)))
                newItemGroupActivated = false;
        }

        private void textBox_customTitle_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_customTitle.Text))
            {
                // empty title, can't proceed
                newItemGroupActivated = false;
            }
            else
            {
                // non-empty title, can proceed
                newItemGroupActivated = true;
            }
        }
        #endregion
    }
}
