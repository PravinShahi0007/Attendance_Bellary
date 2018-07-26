namespace Attendance.Forms
{
    partial class frmAutoMailSender 
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
            this.btnSend = new System.Windows.Forms.Button();
            this.cmbRptType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pBar1 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDtTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDtFrom = new System.Windows.Forms.DateTimePicker();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lstWrkGrp = new System.Windows.Forms.CheckedListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lstSubScr = new System.Windows.Forms.ListBox();
            this.txtSubScrID = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lblSid = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(13, 288);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(100, 28);
            this.btnSend.TabIndex = 10;
            this.btnSend.Text = "Send Mail";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // cmbRptType
            // 
            this.cmbRptType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRptType.FormattingEnabled = true;
            this.cmbRptType.Items.AddRange(new object[] {
            "Monthly Attendance Report",
            "Daily Performance Report",
            "Monthly Lunch Halfday Report"});
            this.cmbRptType.Location = new System.Drawing.Point(123, 71);
            this.cmbRptType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRptType.Name = "cmbRptType";
            this.cmbRptType.Size = new System.Drawing.Size(235, 24);
            this.cmbRptType.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 74);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Report Type";
            // 
            // pBar1
            // 
            this.pBar1.Location = new System.Drawing.Point(11, 253);
            this.pBar1.Name = "pBar1";
            this.pBar1.Size = new System.Drawing.Size(726, 28);
            this.pBar1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(381, 115);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "To Date";
            // 
            // txtDtTo
            // 
            this.txtDtTo.CustomFormat = "dd/MM/yyyy";
            this.txtDtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDtTo.Location = new System.Drawing.Point(502, 110);
            this.txtDtTo.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            this.txtDtTo.MinDate = new System.DateTime(2016, 1, 1, 0, 0, 0, 0);
            this.txtDtTo.Name = "txtDtTo";
            this.txtDtTo.Size = new System.Drawing.Size(235, 22);
            this.txtDtTo.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(381, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "From Date";
            // 
            // txtDtFrom
            // 
            this.txtDtFrom.CustomFormat = "dd/MM/yyyy";
            this.txtDtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDtFrom.Location = new System.Drawing.Point(503, 71);
            this.txtDtFrom.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            this.txtDtFrom.MinDate = new System.DateTime(2016, 1, 1, 0, 0, 0, 0);
            this.txtDtFrom.Name = "txtDtFrom";
            this.txtDtFrom.Size = new System.Drawing.Size(235, 22);
            this.txtDtFrom.TabIndex = 3;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(123, 27);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(235, 22);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(502, 27);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(235, 22);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 27);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "User ID";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(381, 30);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Password";
            this.label5.Visible = false;
            // 
            // lstWrkGrp
            // 
            this.lstWrkGrp.FormattingEnabled = true;
            this.lstWrkGrp.Items.AddRange(new object[] {
            "COMP",
            "CONT",
            "COMP",
            "CONT",
            "COMP",
            "CONT",
            "COMP",
            "CONT",
            "COMP",
            "CONT",
            "COMP",
            "CONT"});
            this.lstWrkGrp.Location = new System.Drawing.Point(123, 115);
            this.lstWrkGrp.Name = "lstWrkGrp";
            this.lstWrkGrp.Size = new System.Drawing.Size(235, 123);
            this.lstWrkGrp.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 115);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "WrkGrp Wise";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(381, 147);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "SubScription ID\'s";
            // 
            // lstSubScr
            // 
            this.lstSubScr.FormattingEnabled = true;
            this.lstSubScr.ItemHeight = 16;
            this.lstSubScr.Location = new System.Drawing.Point(502, 172);
            this.lstSubScr.Name = "lstSubScr";
            this.lstSubScr.Size = new System.Drawing.Size(235, 68);
            this.lstSubScr.TabIndex = 9;
            // 
            // txtSubScrID
            // 
            this.txtSubScrID.Location = new System.Drawing.Point(502, 144);
            this.txtSubScrID.Name = "txtSubScrID";
            this.txtSubScrID.Size = new System.Drawing.Size(145, 22);
            this.txtSubScrID.TabIndex = 6;
            this.txtSubScrID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubScrID_KeyDown);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(677, 144);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(27, 24);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(710, 144);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(27, 24);
            this.btnRemove.TabIndex = 8;
            this.btnRemove.Text = "-";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(638, 288);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 28);
            this.btnReset.TabIndex = 19;
            this.btnReset.Text = "Reset Form";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(16, 324);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(720, 78);
            this.richTextBox1.TabIndex = 20;
            this.richTextBox1.Text = "Kindly Note :\n1) if Subscription ID is provided WrkGrp Selection will be ignored." +
    "\n2) In case of Daily Performance Report ToDate parameter will be ignored.";
            // 
            // lblSid
            // 
            this.lblSid.AutoSize = true;
            this.lblSid.Location = new System.Drawing.Point(221, 294);
            this.lblSid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSid.Name = "lblSid";
            this.lblSid.Size = new System.Drawing.Size(0, 16);
            this.lblSid.TabIndex = 21;
            // 
            // frmAutoMailSender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 408);
            this.Controls.Add(this.lblSid);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtSubScrID);
            this.Controls.Add(this.lstSubScr);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lstWrkGrp);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDtTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDtFrom);
            this.Controls.Add(this.pBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbRptType);
            this.Controls.Add(this.btnSend);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmAutoMailSender";
            this.Text = "Auto Mail Sender";
            this.Load += new System.EventHandler(this.frmAutoMailSender_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ComboBox cmbRptType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar pBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker txtDtTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker txtDtFrom;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox lstWrkGrp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox lstSubScr;
        private System.Windows.Forms.TextBox txtSubScrID;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lblSid;
    }
}

