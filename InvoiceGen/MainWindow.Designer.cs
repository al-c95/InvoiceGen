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
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.newItemGroup = new System.Windows.Forms.GroupBox();
            this.button_addItem = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_newEntryAmt = new System.Windows.Forms.TextBox();
            this.numericUpDown_newEntryQ = new System.Windows.Forms.NumericUpDown();
            this.textBox_newEntryDesc = new System.Windows.Forms.TextBox();
            this.titleGroup = new System.Windows.Forms.GroupBox();
            this.textBox_Year = new System.Windows.Forms.TextBox();
            this.comboBox_Month = new System.Windows.Forms.ComboBox();
            this.textBox_customTitle = new System.Windows.Forms.TextBox();
            this.radioButton_titleCustom = new System.Windows.Forms.RadioButton();
            this.radioButton_titleMonthly = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button_viewSelected = new System.Windows.Forms.Button();
            this.button_updateRecords = new System.Windows.Forms.Button();
            this.dataGridView_invoiceHistory = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.newItemGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_newEntryQ)).BeginInit();
            this.titleGroup.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_invoiceHistory)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1067, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.configurationToolStripMenuItem.Text = "Configuration";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewManualToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewManualToolStripMenuItem
            // 
            this.viewManualToolStripMenuItem.Enabled = false;
            this.viewManualToolStripMenuItem.Name = "viewManualToolStripMenuItem";
            this.viewManualToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.viewManualToolStripMenuItem.Text = "View Manual";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // button_newInvoice
            // 
            this.button_newInvoice.Location = new System.Drawing.Point(8, 7);
            this.button_newInvoice.Margin = new System.Windows.Forms.Padding(4);
            this.button_newInvoice.Name = "button_newInvoice";
            this.button_newInvoice.Size = new System.Drawing.Size(155, 28);
            this.button_newInvoice.TabIndex = 1;
            this.button_newInvoice.Text = "New Invoice";
            this.button_newInvoice.UseVisualStyleBackColor = true;
            this.button_newInvoice.Click += new System.EventHandler(this.button_newInvoice_Click_1);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 28);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1067, 734);
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
            this.tabPage1.Controls.Add(this.newItemGroup);
            this.tabPage1.Controls.Add(this.titleGroup);
            this.tabPage1.Controls.Add(this.button_newInvoice);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1059, 705);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "View or Generate";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cancel.Image = global::InvoiceGen.Properties.Resources.cancelButton;
            this.button_cancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_cancel.Location = new System.Drawing.Point(948, 547);
            this.button_cancel.Margin = new System.Windows.Forms.Padding(4);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(100, 113);
            this.button_cancel.TabIndex = 11;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // richTextBox_total
            // 
            this.richTextBox_total.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBox_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F);
            this.richTextBox_total.Location = new System.Drawing.Point(224, 542);
            this.richTextBox_total.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox_total.Name = "richTextBox_total";
            this.richTextBox_total.ReadOnly = true;
            this.richTextBox_total.Size = new System.Drawing.Size(537, 117);
            this.richTextBox_total.TabIndex = 10;
            this.richTextBox_total.Text = "0.00";
            // 
            // button_saveExportXL
            // 
            this.button_saveExportXL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_saveExportXL.Location = new System.Drawing.Point(116, 547);
            this.button_saveExportXL.Margin = new System.Windows.Forms.Padding(4);
            this.button_saveExportXL.Name = "button_saveExportXL";
            this.button_saveExportXL.Size = new System.Drawing.Size(100, 113);
            this.button_saveExportXL.TabIndex = 9;
            this.button_saveExportXL.Text = "Save and Export XLSX";
            this.button_saveExportXL.UseVisualStyleBackColor = true;
            // 
            // button_saveEmail
            // 
            this.button_saveEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_saveEmail.Location = new System.Drawing.Point(8, 547);
            this.button_saveEmail.Margin = new System.Windows.Forms.Padding(4);
            this.button_saveEmail.Name = "button_saveEmail";
            this.button_saveEmail.Size = new System.Drawing.Size(100, 113);
            this.button_saveEmail.TabIndex = 8;
            this.button_saveEmail.Text = "Save and Email";
            this.button_saveEmail.UseVisualStyleBackColor = true;
            // 
            // button_removeItem
            // 
            this.button_removeItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_removeItem.Location = new System.Drawing.Point(893, 266);
            this.button_removeItem.Margin = new System.Windows.Forms.Padding(4);
            this.button_removeItem.Name = "button_removeItem";
            this.button_removeItem.Size = new System.Drawing.Size(155, 28);
            this.button_removeItem.TabIndex = 7;
            this.button_removeItem.Text = "Remove Selected";
            this.button_removeItem.UseVisualStyleBackColor = true;
            // 
            // button_duplicateItem
            // 
            this.button_duplicateItem.Location = new System.Drawing.Point(8, 266);
            this.button_duplicateItem.Margin = new System.Windows.Forms.Padding(4);
            this.button_duplicateItem.Name = "button_duplicateItem";
            this.button_duplicateItem.Size = new System.Drawing.Size(155, 28);
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
            this.listView_items.FullRowSelect = true;
            this.listView_items.GridLines = true;
            this.listView_items.HideSelection = false;
            this.listView_items.Location = new System.Drawing.Point(8, 302);
            this.listView_items.Margin = new System.Windows.Forms.Padding(4);
            this.listView_items.MultiSelect = false;
            this.listView_items.Name = "listView_items";
            this.listView_items.Size = new System.Drawing.Size(1039, 237);
            this.listView_items.TabIndex = 5;
            this.listView_items.UseCompatibleStateImageBehavior = false;
            this.listView_items.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Item/Description";
            this.columnHeader1.Width = 563;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Amount ($)";
            this.columnHeader2.Width = 138;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Quantity";
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
            this.newItemGroup.Location = new System.Drawing.Point(8, 133);
            this.newItemGroup.Margin = new System.Windows.Forms.Padding(4);
            this.newItemGroup.Name = "newItemGroup";
            this.newItemGroup.Padding = new System.Windows.Forms.Padding(4);
            this.newItemGroup.Size = new System.Drawing.Size(1040, 126);
            this.newItemGroup.TabIndex = 3;
            this.newItemGroup.TabStop = false;
            this.newItemGroup.Text = "New Item";
            // 
            // button_addItem
            // 
            this.button_addItem.Location = new System.Drawing.Point(8, 82);
            this.button_addItem.Margin = new System.Windows.Forms.Padding(4);
            this.button_addItem.Name = "button_addItem";
            this.button_addItem.Size = new System.Drawing.Size(155, 28);
            this.button_addItem.TabIndex = 12;
            this.button_addItem.Text = "Add";
            this.button_addItem.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(915, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Quantity";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(553, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Amount ($)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Description";
            // 
            // textBox_newEntryAmt
            // 
            this.textBox_newEntryAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_newEntryAmt.Location = new System.Drawing.Point(557, 50);
            this.textBox_newEntryAmt.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_newEntryAmt.Name = "textBox_newEntryAmt";
            this.textBox_newEntryAmt.Size = new System.Drawing.Size(352, 22);
            this.textBox_newEntryAmt.TabIndex = 7;
            // 
            // numericUpDown_newEntryQ
            // 
            this.numericUpDown_newEntryQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_newEntryQ.Location = new System.Drawing.Point(919, 50);
            this.numericUpDown_newEntryQ.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown_newEntryQ.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_newEntryQ.Name = "numericUpDown_newEntryQ";
            this.numericUpDown_newEntryQ.Size = new System.Drawing.Size(113, 22);
            this.numericUpDown_newEntryQ.TabIndex = 6;
            this.numericUpDown_newEntryQ.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textBox_newEntryDesc
            // 
            this.textBox_newEntryDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_newEntryDesc.Location = new System.Drawing.Point(8, 50);
            this.textBox_newEntryDesc.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_newEntryDesc.Name = "textBox_newEntryDesc";
            this.textBox_newEntryDesc.Size = new System.Drawing.Size(540, 22);
            this.textBox_newEntryDesc.TabIndex = 5;
            // 
            // titleGroup
            // 
            this.titleGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleGroup.Controls.Add(this.textBox_Year);
            this.titleGroup.Controls.Add(this.comboBox_Month);
            this.titleGroup.Controls.Add(this.textBox_customTitle);
            this.titleGroup.Controls.Add(this.radioButton_titleCustom);
            this.titleGroup.Controls.Add(this.radioButton_titleMonthly);
            this.titleGroup.Location = new System.Drawing.Point(8, 43);
            this.titleGroup.Margin = new System.Windows.Forms.Padding(4);
            this.titleGroup.Name = "titleGroup";
            this.titleGroup.Padding = new System.Windows.Forms.Padding(4);
            this.titleGroup.Size = new System.Drawing.Size(1040, 82);
            this.titleGroup.TabIndex = 2;
            this.titleGroup.TabStop = false;
            this.titleGroup.Text = "Title";
            // 
            // textBox_Year
            // 
            this.textBox_Year.Location = new System.Drawing.Point(108, 50);
            this.textBox_Year.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_Year.Name = "textBox_Year";
            this.textBox_Year.Size = new System.Drawing.Size(99, 22);
            this.textBox_Year.TabIndex = 6;
            // 
            // comboBox_Month
            // 
            this.comboBox_Month.FormattingEnabled = true;
            this.comboBox_Month.Location = new System.Drawing.Point(8, 50);
            this.comboBox_Month.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_Month.Name = "comboBox_Month";
            this.comboBox_Month.Size = new System.Drawing.Size(91, 24);
            this.comboBox_Month.TabIndex = 5;
            // 
            // textBox_customTitle
            // 
            this.textBox_customTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_customTitle.Location = new System.Drawing.Point(557, 50);
            this.textBox_customTitle.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_customTitle.Name = "textBox_customTitle";
            this.textBox_customTitle.Size = new System.Drawing.Size(473, 22);
            this.textBox_customTitle.TabIndex = 4;
            // 
            // radioButton_titleCustom
            // 
            this.radioButton_titleCustom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton_titleCustom.AutoSize = true;
            this.radioButton_titleCustom.Location = new System.Drawing.Point(956, 22);
            this.radioButton_titleCustom.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton_titleCustom.Name = "radioButton_titleCustom";
            this.radioButton_titleCustom.Size = new System.Drawing.Size(76, 21);
            this.radioButton_titleCustom.TabIndex = 1;
            this.radioButton_titleCustom.TabStop = true;
            this.radioButton_titleCustom.Text = "Custom";
            this.radioButton_titleCustom.UseVisualStyleBackColor = true;
            // 
            // radioButton_titleMonthly
            // 
            this.radioButton_titleMonthly.AutoSize = true;
            this.radioButton_titleMonthly.Location = new System.Drawing.Point(8, 23);
            this.radioButton_titleMonthly.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton_titleMonthly.Name = "radioButton_titleMonthly";
            this.radioButton_titleMonthly.Size = new System.Drawing.Size(78, 21);
            this.radioButton_titleMonthly.TabIndex = 0;
            this.radioButton_titleMonthly.TabStop = true;
            this.radioButton_titleMonthly.Text = "Monthly";
            this.radioButton_titleMonthly.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Controls.Add(this.dataGridView_invoiceHistory);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1059, 705);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "History";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 469F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 631);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1056, 44);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.button_viewSelected, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.button_updateRecords, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(297, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(461, 36);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // button_viewSelected
            // 
            this.button_viewSelected.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button_viewSelected.Location = new System.Drawing.Point(4, 4);
            this.button_viewSelected.Margin = new System.Windows.Forms.Padding(4);
            this.button_viewSelected.Name = "button_viewSelected";
            this.button_viewSelected.Size = new System.Drawing.Size(155, 28);
            this.button_viewSelected.TabIndex = 0;
            this.button_viewSelected.Text = "View Selected";
            this.button_viewSelected.UseVisualStyleBackColor = true;
            // 
            // button_updateRecords
            // 
            this.button_updateRecords.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_updateRecords.Location = new System.Drawing.Point(302, 4);
            this.button_updateRecords.Margin = new System.Windows.Forms.Padding(4);
            this.button_updateRecords.Name = "button_updateRecords";
            this.button_updateRecords.Size = new System.Drawing.Size(155, 28);
            this.button_updateRecords.TabIndex = 1;
            this.button_updateRecords.Text = "Update Records";
            this.button_updateRecords.UseVisualStyleBackColor = true;
            // 
            // dataGridView_invoiceHistory
            // 
            this.dataGridView_invoiceHistory.AllowUserToAddRows = false;
            this.dataGridView_invoiceHistory.AllowUserToDeleteRows = false;
            this.dataGridView_invoiceHistory.AllowUserToResizeRows = false;
            this.dataGridView_invoiceHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_invoiceHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_invoiceHistory.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_invoiceHistory.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView_invoiceHistory.MultiSelect = false;
            this.dataGridView_invoiceHistory.Name = "dataGridView_invoiceHistory";
            this.dataGridView_invoiceHistory.RowHeadersVisible = false;
            this.dataGridView_invoiceHistory.RowHeadersWidth = 51;
            this.dataGridView_invoiceHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_invoiceHistory.Size = new System.Drawing.Size(1056, 627);
            this.dataGridView_invoiceHistory.TabIndex = 0;
            this.dataGridView_invoiceHistory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_invoiceHistory_CellContentClick);
            this.dataGridView_invoiceHistory.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_invoiceHistory_CellValueChanged);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 736);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(1067, 26);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(50, 20);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 762);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
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
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_invoiceHistory)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
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
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.GroupBox titleGroup;
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
        private System.Windows.Forms.ComboBox comboBox_Month;
        private System.Windows.Forms.RichTextBox richTextBox_total;
        private System.Windows.Forms.Button button_saveExportXL;
        private System.Windows.Forms.Button button_saveEmail;
        private System.Windows.Forms.Button button_removeItem;
        private System.Windows.Forms.Button button_duplicateItem;
        private System.Windows.Forms.ListView listView_items;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.TextBox textBox_Year;
        private System.Windows.Forms.Button button_addItem;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.DataGridView dataGridView_invoiceHistory;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button_viewSelected;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button button_updateRecords;
    }
}

