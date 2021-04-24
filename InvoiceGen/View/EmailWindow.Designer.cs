
namespace InvoiceGen.View
{
    partial class EmailWindow
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
            this.textBox_To = new System.Windows.Forms.TextBox();
            this.textBox_Cc = new System.Windows.Forms.TextBox();
            this.textBox_Bcc = new System.Windows.Forms.TextBox();
            this.textBox_body = new System.Windows.Forms.TextBox();
            this.textBox_subject = new System.Windows.Forms.TextBox();
            this.button_send = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_To
            // 
            this.textBox_To.Location = new System.Drawing.Point(81, 22);
            this.textBox_To.Name = "textBox_To";
            this.textBox_To.Size = new System.Drawing.Size(465, 22);
            this.textBox_To.TabIndex = 0;
            // 
            // textBox_Cc
            // 
            this.textBox_Cc.Location = new System.Drawing.Point(81, 63);
            this.textBox_Cc.Name = "textBox_Cc";
            this.textBox_Cc.Size = new System.Drawing.Size(465, 22);
            this.textBox_Cc.TabIndex = 1;
            // 
            // textBox_Bcc
            // 
            this.textBox_Bcc.Location = new System.Drawing.Point(81, 107);
            this.textBox_Bcc.Name = "textBox_Bcc";
            this.textBox_Bcc.Size = new System.Drawing.Size(465, 22);
            this.textBox_Bcc.TabIndex = 2;
            // 
            // textBox_body
            // 
            this.textBox_body.Location = new System.Drawing.Point(15, 204);
            this.textBox_body.Multiline = true;
            this.textBox_body.Name = "textBox_body";
            this.textBox_body.Size = new System.Drawing.Size(531, 162);
            this.textBox_body.TabIndex = 3;
            // 
            // textBox_subject
            // 
            this.textBox_subject.Location = new System.Drawing.Point(81, 150);
            this.textBox_subject.Name = "textBox_subject";
            this.textBox_subject.Size = new System.Drawing.Size(465, 22);
            this.textBox_subject.TabIndex = 4;
            // 
            // button_send
            // 
            this.button_send.Location = new System.Drawing.Point(15, 381);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(117, 34);
            this.button_send.TabIndex = 5;
            this.button_send.Text = "Send";
            this.button_send.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(429, 381);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(117, 34);
            this.button_cancel.TabIndex = 6;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "To:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Cc:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Bcc:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Subject:";
            // 
            // EmailWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 427);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.textBox_subject);
            this.Controls.Add(this.textBox_body);
            this.Controls.Add(this.textBox_Bcc);
            this.Controls.Add(this.textBox_Cc);
            this.Controls.Add(this.textBox_To);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmailWindow";
            this.ShowIcon = false;
            this.Text = "Email Invoice";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_To;
        private System.Windows.Forms.TextBox textBox_Cc;
        private System.Windows.Forms.TextBox textBox_Bcc;
        private System.Windows.Forms.TextBox textBox_body;
        private System.Windows.Forms.TextBox textBox_subject;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}