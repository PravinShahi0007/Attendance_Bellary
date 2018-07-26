namespace Attendance.Forms
{
    partial class frmMastCont
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtContName = new DevExpress.XtraEditors.TextEdit();
            this.txtContCode = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUnitDesc = new DevExpress.XtraEditors.TextEdit();
            this.txtUnitCode = new DevExpress.XtraEditors.TextEdit();
            this.txtCompName = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCompCode = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWrkGrpDesc = new DevExpress.XtraEditors.TextEdit();
            this.txtWrkGrpCode = new DevExpress.XtraEditors.TextEdit();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpUserRights = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAdd1 = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPhone = new DevExpress.XtraEditors.TextEdit();
            this.txtAdd2 = new DevExpress.XtraEditors.TextEdit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtContName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWrkGrpDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWrkGrpCode.Properties)).BeginInit();
            this.grpUserRights.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdd1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdd2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtAdd1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtPhone);
            this.groupBox1.Controls.Add(this.txtAdd2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtContName);
            this.groupBox1.Controls.Add(this.txtContCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtUnitDesc);
            this.groupBox1.Controls.Add(this.txtUnitCode);
            this.groupBox1.Controls.Add(this.txtCompName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCompCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtWrkGrpDesc);
            this.groupBox1.Controls.Add(this.txtWrkGrpCode);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(455, 250);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "Cont. Name";
            // 
            // txtContName
            // 
            this.txtContName.Location = new System.Drawing.Point(102, 122);
            this.txtContName.Name = "txtContName";
            this.txtContName.Properties.Mask.EditMask = "[A-Za-z ]+";
            this.txtContName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtContName.Properties.Mask.ShowPlaceHolders = false;
            this.txtContName.Properties.MaxLength = 50;
            this.txtContName.Size = new System.Drawing.Size(340, 20);
            this.txtContName.TabIndex = 4;
            // 
            // txtContCode
            // 
            this.txtContCode.Location = new System.Drawing.Point(102, 96);
            this.txtContCode.Name = "txtContCode";
            this.txtContCode.Properties.Mask.EditMask = "[A-Za-Z]+";
            this.txtContCode.Properties.Mask.ShowPlaceHolders = false;
            this.txtContCode.Properties.MaxLength = 3;
            this.txtContCode.Size = new System.Drawing.Size(96, 20);
            this.txtContCode.TabIndex = 3;
            this.txtContCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContCode_KeyDown);
            this.txtContCode.Validated += new System.EventHandler(this.txtContCode_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Cont Code";
            // 
            // txtUnitDesc
            // 
            this.txtUnitDesc.Location = new System.Drawing.Point(204, 70);
            this.txtUnitDesc.Name = "txtUnitDesc";
            this.txtUnitDesc.Properties.Mask.ShowPlaceHolders = false;
            this.txtUnitDesc.Properties.ReadOnly = true;
            this.txtUnitDesc.Size = new System.Drawing.Size(238, 20);
            this.txtUnitDesc.TabIndex = 5;
            // 
            // txtUnitCode
            // 
            this.txtUnitCode.Location = new System.Drawing.Point(102, 70);
            this.txtUnitCode.Name = "txtUnitCode";
            this.txtUnitCode.Properties.Mask.EditMask = "[0-9]+";
            this.txtUnitCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtUnitCode.Properties.Mask.ShowPlaceHolders = false;
            this.txtUnitCode.Properties.MaxLength = 3;
            this.txtUnitCode.Size = new System.Drawing.Size(96, 20);
            this.txtUnitCode.TabIndex = 2;
            this.txtUnitCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnitCode_KeyDown);
            this.txtUnitCode.Validated += new System.EventHandler(this.txtUnitCode_Validated);
            // 
            // txtCompName
            // 
            this.txtCompName.Location = new System.Drawing.Point(150, 16);
            this.txtCompName.Name = "txtCompName";
            this.txtCompName.Properties.ReadOnly = true;
            this.txtCompName.Size = new System.Drawing.Size(292, 20);
            this.txtCompName.TabIndex = 1;
            this.txtCompName.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "CompCode";
            // 
            // txtCompCode
            // 
            this.txtCompCode.Location = new System.Drawing.Point(102, 16);
            this.txtCompCode.Name = "txtCompCode";
            this.txtCompCode.Size = new System.Drawing.Size(42, 20);
            this.txtCompCode.TabIndex = 0;
            this.txtCompCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCompCode_KeyDown);
            this.txtCompCode.Validated += new System.EventHandler(this.txtCompCode_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "UnitCode";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "WrkGrpCode";
            // 
            // txtWrkGrpDesc
            // 
            this.txtWrkGrpDesc.Location = new System.Drawing.Point(204, 44);
            this.txtWrkGrpDesc.Name = "txtWrkGrpDesc";
            this.txtWrkGrpDesc.Properties.Mask.EditMask = "\\w{3,50}";
            this.txtWrkGrpDesc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtWrkGrpDesc.Properties.Mask.ShowPlaceHolders = false;
            this.txtWrkGrpDesc.Properties.ReadOnly = true;
            this.txtWrkGrpDesc.Size = new System.Drawing.Size(238, 20);
            this.txtWrkGrpDesc.TabIndex = 3;
            this.txtWrkGrpDesc.TabStop = false;
            // 
            // txtWrkGrpCode
            // 
            this.txtWrkGrpCode.Location = new System.Drawing.Point(102, 44);
            this.txtWrkGrpCode.Name = "txtWrkGrpCode";
            this.txtWrkGrpCode.Properties.Mask.EditMask = "\\w{3,10}";
            this.txtWrkGrpCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtWrkGrpCode.Properties.Mask.ShowPlaceHolders = false;
            this.txtWrkGrpCode.Size = new System.Drawing.Size(96, 20);
            this.txtWrkGrpCode.TabIndex = 1;
            this.txtWrkGrpCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWrkGrpCode_KeyDown);
            this.txtWrkGrpCode.Validated += new System.EventHandler(this.txtWrkGrpCode_Validated);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Cornsilk;
            this.btnAdd.Location = new System.Drawing.Point(6, 14);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 32);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // grpUserRights
            // 
            this.grpUserRights.Controls.Add(this.btnClose);
            this.grpUserRights.Controls.Add(this.btnCancel);
            this.grpUserRights.Controls.Add(this.btnDelete);
            this.grpUserRights.Controls.Add(this.btnUpdate);
            this.grpUserRights.Controls.Add(this.btnAdd);
            this.grpUserRights.Location = new System.Drawing.Point(12, 259);
            this.grpUserRights.Name = "grpUserRights";
            this.grpUserRights.Size = new System.Drawing.Size(455, 52);
            this.grpUserRights.TabIndex = 2;
            this.grpUserRights.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Cornsilk;
            this.btnClose.Location = new System.Drawing.Point(329, 14);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 32);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Clos&e";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Cornsilk;
            this.btnCancel.Location = new System.Drawing.Point(249, 14);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Cornsilk;
            this.btnDelete.Location = new System.Drawing.Point(168, 14);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 32);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Cornsilk;
            this.btnUpdate.Location = new System.Drawing.Point(87, 14);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 32);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 15);
            this.label6.TabIndex = 20;
            this.label6.Text = "Address 1";
            // 
            // txtAdd1
            // 
            this.txtAdd1.Location = new System.Drawing.Point(102, 148);
            this.txtAdd1.Name = "txtAdd1";
            this.txtAdd1.Properties.MaxLength = 35;
            this.txtAdd1.Size = new System.Drawing.Size(340, 20);
            this.txtAdd1.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 201);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 15);
            this.label7.TabIndex = 18;
            this.label7.Text = "ContactNo";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 15);
            this.label8.TabIndex = 16;
            this.label8.Text = "Address 2";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(102, 200);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Properties.Mask.ShowPlaceHolders = false;
            this.txtPhone.Properties.MaxLength = 35;
            this.txtPhone.Size = new System.Drawing.Size(340, 20);
            this.txtPhone.TabIndex = 7;
            // 
            // txtAdd2
            // 
            this.txtAdd2.Location = new System.Drawing.Point(102, 174);
            this.txtAdd2.Name = "txtAdd2";
            this.txtAdd2.Properties.MaxLength = 35;
            this.txtAdd2.Size = new System.Drawing.Size(340, 20);
            this.txtAdd2.TabIndex = 6;
            // 
            // frmMastCont
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 318);
            this.Controls.Add(this.grpUserRights);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMastCont";
            this.Text = "Contractor Master";
            this.Load += new System.EventHandler(this.frmMastCont_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtContName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWrkGrpDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWrkGrpCode.Properties)).EndInit();
            this.grpUserRights.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtAdd1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdd2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtWrkGrpDesc;
        private DevExpress.XtraEditors.TextEdit txtWrkGrpCode;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtCompCode;
        private DevExpress.XtraEditors.TextEdit txtCompName;
        private System.Windows.Forms.GroupBox grpUserRights;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtUnitDesc;
        private DevExpress.XtraEditors.TextEdit txtUnitCode;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.TextEdit txtContName;
        private DevExpress.XtraEditors.TextEdit txtContCode;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit txtAdd1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.TextEdit txtPhone;
        private DevExpress.XtraEditors.TextEdit txtAdd2;
    }
}