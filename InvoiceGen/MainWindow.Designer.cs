namespace InvoiceGen
{
    partial class mainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_newInvoice = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button_cancel = new System.Windows.Forms.Button();
            this.richTextBox_total = new System.Windows.Forms.RichTextBox();
            this.button_saveExportXL = new System.Windows.Forms.Button();
            this.button_saveEmail = new System.Windows.Forms.Button();
            this.button_removeItem = new System.Windows.Forms.Button();
            this.button_duplicateItem = new System.Windows.Forms.Button();
            this.listView_items = new System.Windows.Forms.ListView();
            this.button_loadInvoice = new System.Windows.Forms.Button();
            this.newItemGroup = new System.Windows.Forms.GroupBox();
            this.button_addItem = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_newEntryAmt = new System.Windows.Forms.TextBox();
            this.numericUpDown_newEntryQ = new System.Windows.Forms.NumericUpDown();
            this.textBox_newEntryDesc = new System.Windows.Forms.TextBox();
            this.titleGroup = new System.Windows.Forms.GroupBox();
            this.textBox_year = new System.Windows.Forms.TextBox();
            this.comboBox_month = new System.Windows.Forms.ComboBox();
            this.textBox_customTitle = new System.Windows.Forms.TextBox();
            this.radioButton_titleCustom = new System.Windows.Forms.RadioButton();
            this.radioButton_titleMonthly = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dataGridView_invoiceHistory = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.newItemGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_newEntryQ)).BeginInit();
            this.titleGroup.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_invoiceHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // button_newInvoice
            // 
            this.button_newInvoice.Location = new System.Drawing.Point(6, 6);
            this.button_newInvoice.Name = "button_newInvoice";
            this.button_newInvoice.Size = new System.Drawing.Size(75, 23);
            this.button_newInvoice.TabIndex = 1;
            this.button_newInvoice.Text = "New";
            this.button_newInvoice.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 24);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 595);
            this.tabControl.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button_cancel);
            this.tabPage1.Controls.Add(this.richTextBox_total);
            this.tabPage1.Controls.Add(this.button_saveExportXL);
            this.tabPage1.Controls.Add(this.button_saveEmail);
            this.tabPage1.Controls.Add(this.button_removeItem);
            this.tabPage1.Controls.Add(this.button_duplicateItem);
            this.tabPage1.Controls.Add(this.listView_items);
            this.tabPage1.Controls.Add(this.button_loadInvoice);
            this.tabPage1.Controls.Add(this.newItemGroup);
            this.tabPage1.Controls.Add(this.titleGroup);
            this.tabPage1.Controls.Add(this.button_newInvoice);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 569);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Generate";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cancel.Location = new System.Drawing.Point(711, 443);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 92);
            this.button_cancel.TabIndex = 11;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // richTextBox_total
            // 
            this.richTextBox_total.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBox_total.Location = new System.Drawing.Point(168, 439);
            this.richTextBox_total.Name = "richTextBox_total";
            this.richTextBox_total.ReadOnly = true;
            this.richTextBox_total.Size = new System.Drawing.Size(362, 96);
            this.richTextBox_total.TabIndex = 10;
            this.richTextBox_total.Text = "";
            // 
            // button_saveExportXL
            // 
            this.button_saveExportXL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_saveExportXL.Location = new System.Drawing.Point(87, 443);
            this.button_saveExportXL.Name = "button_saveExportXL";
            this.button_saveExportXL.Size = new System.Drawing.Size(75, 92);
            this.button_saveExportXL.TabIndex = 9;
            this.button_saveExportXL.Text = "Save and Export XLSX";
            this.button_saveExportXL.UseVisualStyleBackColor = true;
            // 
            // button_saveEmail
            // 
            this.button_saveEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_saveEmail.Location = new System.Drawing.Point(6, 443);
            this.button_saveEmail.Name = "button_saveEmail";
            this.button_saveEmail.Size = new System.Drawing.Size(75, 92);
            this.button_saveEmail.TabIndex = 8;
            this.button_saveEmail.Text = "Save and Email";
            this.button_saveEmail.UseVisualStyleBackColor = true;
            // 
            // button_removeItem
            // 
            this.button_removeItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_removeItem.Location = new System.Drawing.Point(670, 216);
            this.button_removeItem.Name = "button_removeItem";
            this.button_removeItem.Size = new System.Drawing.Size(116, 23);
            this.button_removeItem.TabIndex = 7;
            this.button_removeItem.Text = "Remove Selected";
            this.button_removeItem.UseVisualStyleBackColor = true;
            // 
            // button_duplicateItem
            // 
            this.button_duplicateItem.Location = new System.Drawing.Point(6, 216);
            this.button_duplicateItem.Name = "button_duplicateItem";
            this.button_duplicateItem.Size = new System.Drawing.Size(116, 23);
            this.button_duplicateItem.TabIndex = 6;
            this.button_duplicateItem.Text = "Duplicate Selected";
            this.button_duplicateItem.UseVisualStyleBackColor = true;
            // 
            // listView_items
            // 
            this.listView_items.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_items.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView_items.GridLines = true;
            this.listView_items.HideSelection = false;
            this.listView_items.Location = new System.Drawing.Point(6, 245);
            this.listView_items.Name = "listView_items";
            this.listView_items.Size = new System.Drawing.Size(780, 192);
            this.listView_items.TabIndex = 5;
            this.listView_items.UseCompatibleStateImageBehavior = false;
            this.listView_items.View = System.Windows.Forms.View.Details;
            // 
            // button_loadInvoice
            // 
            this.button_loadInvoice.Location = new System.Drawing.Point(87, 6);
            this.button_loadInvoice.Name = "button_loadInvoice";
            this.button_loadInvoice.Size = new System.Drawing.Size(75, 23);
            this.button_loadInvoice.TabIndex = 4;
            this.button_loadInvoice.Text = "Load";
            this.button_loadInvoice.UseVisualStyleBackColor = true;
            // 
            // newItemGroup
            // 
            this.newItemGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newItemGroup.Controls.Add(this.button_addItem);
            this.newItemGroup.Controls.Add(this.label3);
            this.newItemGroup.Controls.Add(this.label2);
            this.newItemGroup.Controls.Add(this.label1);
            this.newItemGroup.Controls.Add(this.textBox_newEntryAmt);
            this.newItemGroup.Controls.Add(this.numericUpDown_newEntryQ);
            this.newItemGroup.Controls.Add(this.textBox_newEntryDesc);
            this.newItemGroup.Location = new System.Drawing.Point(6, 108);
            this.newItemGroup.Name = "newItemGroup";
            this.newItemGroup.Size = new System.Drawing.Size(780, 102);
            this.newItemGroup.TabIndex = 3;
            this.newItemGroup.TabStop = false;
            this.newItemGroup.Text = "New Item";
            // 
            // button_addItem
            // 
            this.button_addItem.Location = new System.Drawing.Point(6, 67);
            this.button_addItem.Name = "button_addItem";
            this.button_addItem.Size = new System.Drawing.Size(116, 23);
            this.button_addItem.TabIndex = 12;
            this.button_addItem.Text = "Add";
            this.button_addItem.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(686, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Quantity";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(415, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Amount ($)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Description";
            // 
            // textBox_newEntryAmt
            // 
            this.textBox_newEntryAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_newEntryAmt.Location = new System.Drawing.Point(418, 41);
            this.textBox_newEntryAmt.Name = "textBox_newEntryAmt";
            this.textBox_newEntryAmt.Size = new System.Drawing.Size(265, 20);
            this.textBox_newEntryAmt.TabIndex = 7;
            // 
            // numericUpDown_newEntryQ
            // 
            this.numericUpDown_newEntryQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_newEntryQ.Location = new System.Drawing.Point(689, 41);
            this.numericUpDown_newEntryQ.Name = "numericUpDown_newEntryQ";
            this.numericUpDown_newEntryQ.Size = new System.Drawing.Size(85, 20);
            this.numericUpDown_newEntryQ.TabIndex = 6;
            // 
            // textBox_newEntryDesc
            // 
            this.textBox_newEntryDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_newEntryDesc.Location = new System.Drawing.Point(6, 41);
            this.textBox_newEntryDesc.Name = "textBox_newEntryDesc";
            this.textBox_newEntryDesc.Size = new System.Drawing.Size(406, 20);
            this.textBox_newEntryDesc.TabIndex = 5;
            // 
            // titleGroup
            // 
            this.titleGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleGroup.Controls.Add(this.textBox_year);
            this.titleGroup.Controls.Add(this.comboBox_month);
            this.titleGroup.Controls.Add(this.textBox_customTitle);
            this.titleGroup.Controls.Add(this.radioButton_titleCustom);
            this.titleGroup.Controls.Add(this.radioButton_titleMonthly);
            this.titleGroup.Location = new System.Drawing.Point(6, 35);
            this.titleGroup.Name = "titleGroup";
            this.titleGroup.Size = new System.Drawing.Size(780, 67);
            this.titleGroup.TabIndex = 2;
            this.titleGroup.TabStop = false;
            this.titleGroup.Text = "Title";
            // 
            // textBox_year
            // 
            this.textBox_year.Location = new System.Drawing.Point(81, 41);
            this.textBox_year.Name = "textBox_year";
            this.textBox_year.Size = new System.Drawing.Size(75, 20);
            this.textBox_year.TabIndex = 6;
            // 
            // comboBox_month
            // 
            this.comboBox_month.FormattingEnabled = true;
            this.comboBox_month.Location = new System.Drawing.Point(6, 41);
            this.comboBox_month.Name = "comboBox_month";
            this.comboBox_month.Size = new System.Drawing.Size(69, 21);
            this.comboBox_month.TabIndex = 5;
            this.comboBox_month.TextChanged += new System.EventHandler(this.comboBox_month_TextChanged);
            // 
            // textBox_customTitle
            // 
            this.textBox_customTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_customTitle.Location = new System.Drawing.Point(418, 41);
            this.textBox_customTitle.Name = "textBox_customTitle";
            this.textBox_customTitle.Size = new System.Drawing.Size(356, 20);
            this.textBox_customTitle.TabIndex = 4;
            this.textBox_customTitle.TextChanged += new System.EventHandler(this.textBox_customTitle_TextChanged);
            // 
            // radioButton_titleCustom
            // 
            this.radioButton_titleCustom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton_titleCustom.AutoSize = true;
            this.radioButton_titleCustom.Location = new System.Drawing.Point(714, 18);
            this.radioButton_titleCustom.Name = "radioButton_titleCustom";
            this.radioButton_titleCustom.Size = new System.Drawing.Size(60, 17);
            this.radioButton_titleCustom.TabIndex = 1;
            this.radioButton_titleCustom.TabStop = true;
            this.radioButton_titleCustom.Text = "Custom";
            this.radioButton_titleCustom.UseVisualStyleBackColor = true;
            this.radioButton_titleCustom.CheckedChanged += new System.EventHandler(this.radioButton_titleCustom_CheckedChanged);
            // 
            // radioButton_titleMonthly
            // 
            this.radioButton_titleMonthly.AutoSize = true;
            this.radioButton_titleMonthly.Location = new System.Drawing.Point(6, 19);
            this.radioButton_titleMonthly.Name = "radioButton_titleMonthly";
            this.radioButton_titleMonthly.Size = new System.Drawing.Size(62, 17);
            this.radioButton_titleMonthly.TabIndex = 0;
            this.radioButton_titleMonthly.TabStop = true;
            this.radioButton_titleMonthly.Text = "Monthly";
            this.radioButton_titleMonthly.UseVisualStyleBackColor = true;
            this.radioButton_titleMonthly.CheckedChanged += new System.EventHandler(this.radioButton_titleMonthly_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView_invoiceHistory);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 569);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "History";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 597);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Item/Description";
            this.columnHeader1.Width = 563;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Amount";
            this.columnHeader2.Width = 138;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Quantity";
            // 
            // dataGridView_invoiceHistory
            // 
            this.dataGridView_invoiceHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_invoiceHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_invoiceHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dataGridView_invoiceHistory.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_invoiceHistory.Name = "dataGridView_invoiceHistory";
            this.dataGridView_invoiceHistory.Size = new System.Drawing.Size(792, 548);
            this.dataGridView_invoiceHistory.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Timestamp";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Title";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Total Amount ($)";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Paid";
            this.Column5.Name = "Column5";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 619);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "mainWindow";
            this.Text = "InvoiceGen";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.newItemGroup.ResumeLayout(false);
            this.newItemGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_newEntryQ)).EndInit();
            this.titleGroup.ResumeLayout(false);
            this.titleGroup.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_invoiceHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Button button_newInvoice;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox titleGroup;
        private System.Windows.Forms.Button button_loadInvoice;
        private System.Windows.Forms.GroupBox newItemGroup;
        private System.Windows.Forms.TextBox textBox_customTitle;
        private System.Windows.Forms.RadioButton radioButton_titleCustom;
        private System.Windows.Forms.RadioButton radioButton_titleMonthly;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_newEntryAmt;
        private System.Windows.Forms.NumericUpDown numericUpDown_newEntryQ;
        private System.Windows.Forms.TextBox textBox_newEntryDesc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_month;
        private System.Windows.Forms.RichTextBox richTextBox_total;
        private System.Windows.Forms.Button button_saveExportXL;
        private System.Windows.Forms.Button button_saveEmail;
        private System.Windows.Forms.Button button_removeItem;
        private System.Windows.Forms.Button button_duplicateItem;
        private System.Windows.Forms.ListView listView_items;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.TextBox textBox_year;
        private System.Windows.Forms.Button button_addItem;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.DataGridView dataGridView_invoiceHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    }
}

