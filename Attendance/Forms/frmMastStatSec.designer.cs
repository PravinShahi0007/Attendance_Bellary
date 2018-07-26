namespace Attendance.Forms
{
    partial class frmMastStatSec
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpUserRights = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtStatDesc = new DevExpress.XtraEditors.TextEdit();
            this.txtStatCode = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDeptDesc = new DevExpress.XtraEditors.TextEdit();
            this.txtDeptCode = new DevExpress.XtraEditors.TextEdit();
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
            this.txtSecDesc = new DevExpress.XtraEditors.TextEdit();
            this.txtSecCode = new DevExpress.XtraEditors.TextEdit();
            this.label9 = new System.Windows.Forms.Label();
            this.grpUserRights.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeptDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeptCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWrkGrpDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWrkGrpCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Cornsilk;
            this.btnAdd.Location = new System.Drawing.Point(100, 13);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 30);
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
            this.grpUserRights.Location = new System.Drawing.Point(14, 204);
            this.grpUserRights.Name = "grpUserRights";
            this.grpUserRights.Size = new System.Drawing.Size(621, 49);
            this.grpUserRights.TabIndex = 2;
            this.grpUserRights.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Cornsilk;
            this.btnClose.Location = new System.Drawing.Point(423, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Clos&e";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Cornsilk;
            this.btnCancel.Location = new System.Drawing.Point(343, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Cornsilk;
            this.btnDelete.Location = new System.Drawing.Point(262, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Cornsilk;
            this.btnUpdate.Location = new System.Drawing.Point(181, 13);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 30);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 12);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(655, 323);
            this.xtraTabControl1.TabIndex = 3;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.groupBox1);
            this.xtraTabPage1.Controls.Add(this.grpUserRights);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(649, 295);
            this.xtraTabPage1.Text = "Section Master";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSecDesc);
            this.groupBox1.Controls.Add(this.txtSecCode);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtStatDesc);
            this.groupBox1.Controls.Add(this.txtStatCode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtDeptDesc);
            this.groupBox1.Controls.Add(this.txtDeptCode);
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
            this.groupBox1.Location = new System.Drawing.Point(14, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(621, 187);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // txtStatDesc
            // 
            this.txtStatDesc.Location = new System.Drawing.Point(204, 117);
            this.txtStatDesc.Name = "txtStatDesc";
            this.txtStatDesc.Properties.Mask.EditMask = "[0-9A-Za-z ]+";
            this.txtStatDesc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtStatDesc.Properties.Mask.ShowPlaceHolders = false;
            this.txtStatDesc.Properties.MaxLength = 50;
            this.txtStatDesc.Properties.ReadOnly = true;
            this.txtStatDesc.Size = new System.Drawing.Size(238, 20);
            this.txtStatDesc.TabIndex = 16;
            // 
            // txtStatCode
            // 
            this.txtStatCode.Location = new System.Drawing.Point(102, 116);
            this.txtStatCode.Name = "txtStatCode";
            this.txtStatCode.Properties.Mask.EditMask = "[0-9]+";
            this.txtStatCode.Properties.Mask.ShowPlaceHolders = false;
            this.txtStatCode.Properties.MaxLength = 3;
            this.txtStatCode.Properties.ReadOnly = true;
            this.txtStatCode.Size = new System.Drawing.Size(96, 20);
            this.txtStatCode.TabIndex = 15;
            this.txtStatCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStatCode_KeyDown);
            this.txtStatCode.Validated += new System.EventHandler(this.txtStatCode_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 14);
            this.label5.TabIndex = 14;
            this.label5.Text = "Stat.Code";
            // 
            // txtDeptDesc
            // 
            this.txtDeptDesc.Location = new System.Drawing.Point(204, 91);
            this.txtDeptDesc.Name = "txtDeptDesc";
            this.txtDeptDesc.Properties.Mask.EditMask = "[0-9A-Za-z ]+";
            this.txtDeptDesc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtDeptDesc.Properties.Mask.ShowPlaceHolders = false;
            this.txtDeptDesc.Properties.MaxLength = 50;
            this.txtDeptDesc.Properties.ReadOnly = true;
            this.txtDeptDesc.Size = new System.Drawing.Size(238, 20);
            this.txtDeptDesc.TabIndex = 13;
            // 
            // txtDeptCode
            // 
            this.txtDeptCode.Location = new System.Drawing.Point(102, 90);
            this.txtDeptCode.Name = "txtDeptCode";
            this.txtDeptCode.Properties.Mask.EditMask = "[0-9]+";
            this.txtDeptCode.Properties.Mask.ShowPlaceHolders = false;
            this.txtDeptCode.Properties.MaxLength = 3;
            this.txtDeptCode.Properties.ReadOnly = true;
            this.txtDeptCode.Size = new System.Drawing.Size(96, 20);
            this.txtDeptCode.TabIndex = 12;
            this.txtDeptCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDeptCode_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 14);
            this.label3.TabIndex = 11;
            this.label3.Text = "DeptCode";
            // 
            // txtUnitDesc
            // 
            this.txtUnitDesc.Location = new System.Drawing.Point(204, 65);
            this.txtUnitDesc.Name = "txtUnitDesc";
            this.txtUnitDesc.Properties.Mask.ShowPlaceHolders = false;
            this.txtUnitDesc.Properties.ReadOnly = true;
            this.txtUnitDesc.Size = new System.Drawing.Size(238, 20);
            this.txtUnitDesc.TabIndex = 5;
            // 
            // txtUnitCode
            // 
            this.txtUnitCode.Location = new System.Drawing.Point(102, 65);
            this.txtUnitCode.Name = "txtUnitCode";
            this.txtUnitCode.Properties.Mask.EditMask = "[0-9]+";
            this.txtUnitCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtUnitCode.Properties.Mask.ShowPlaceHolders = false;
            this.txtUnitCode.Properties.MaxLength = 3;
            this.txtUnitCode.Properties.ReadOnly = true;
            this.txtUnitCode.Size = new System.Drawing.Size(96, 20);
            this.txtUnitCode.TabIndex = 4;
            this.txtUnitCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnitCode_KeyDown);
            // 
            // txtCompName
            // 
            this.txtCompName.Location = new System.Drawing.Point(150, 15);
            this.txtCompName.Name = "txtCompName";
            this.txtCompName.Properties.ReadOnly = true;
            this.txtCompName.Size = new System.Drawing.Size(292, 20);
            this.txtCompName.TabIndex = 1;
            this.txtCompName.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "CompCode";
            // 
            // txtCompCode
            // 
            this.txtCompCode.Location = new System.Drawing.Point(102, 15);
            this.txtCompCode.Name = "txtCompCode";
            this.txtCompCode.Properties.ReadOnly = true;
            this.txtCompCode.Size = new System.Drawing.Size(42, 20);
            this.txtCompCode.TabIndex = 0;
            this.txtCompCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCompCode_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "UnitCode";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "WrkGrpCode";
            // 
            // txtWrkGrpDesc
            // 
            this.txtWrkGrpDesc.Location = new System.Drawing.Point(204, 41);
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
            this.txtWrkGrpCode.Location = new System.Drawing.Point(102, 41);
            this.txtWrkGrpCode.Name = "txtWrkGrpCode";
            this.txtWrkGrpCode.Properties.Mask.EditMask = "\\w{3,10}";
            this.txtWrkGrpCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtWrkGrpCode.Properties.Mask.ShowPlaceHolders = false;
            this.txtWrkGrpCode.Properties.ReadOnly = true;
            this.txtWrkGrpCode.Size = new System.Drawing.Size(96, 20);
            this.txtWrkGrpCode.TabIndex = 2;
            this.txtWrkGrpCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWrkGrpCode_KeyDown);
            // 
            // txtSecDesc
            // 
            this.txtSecDesc.Location = new System.Drawing.Point(204, 143);
            this.txtSecDesc.Name = "txtSecDesc";
            this.txtSecDesc.Properties.Mask.EditMask = "[0-9A-Za-z ]+";
            this.txtSecDesc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtSecDesc.Properties.Mask.ShowPlaceHolders = false;
            this.txtSecDesc.Properties.MaxLength = 50;
            this.txtSecDesc.Size = new System.Drawing.Size(238, 20);
            this.txtSecDesc.TabIndex = 19;
            // 
            // txtSecCode
            // 
            this.txtSecCode.Location = new System.Drawing.Point(102, 142);
            this.txtSecCode.Name = "txtSecCode";
            this.txtSecCode.Properties.Mask.EditMask = "[0-9]+";
            this.txtSecCode.Properties.Mask.ShowPlaceHolders = false;
            this.txtSecCode.Properties.MaxLength = 3;
            this.txtSecCode.Size = new System.Drawing.Size(96, 20);
            this.txtSecCode.TabIndex = 18;
            this.txtSecCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSecCode_KeyDown);
            this.txtSecCode.Validated += new System.EventHandler(this.txtSecCode_Validated);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 146);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 14);
            this.label9.TabIndex = 17;
            this.label9.Text = "Sec.Code";
            // 
            // frmMastStatSec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 338);
            this.Controls.Add(this.xtraTabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMastStatSec";
            this.Text = "Section/Station Master";
            this.Load += new System.EventHandler(this.frmMastStatSec_Load);
            this.grpUserRights.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeptDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeptCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWrkGrpDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWrkGrpCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecCode.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox grpUserRights;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit txtStatDesc;
        private DevExpress.XtraEditors.TextEdit txtStatCode;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.TextEdit txtDeptDesc;
        private DevExpress.XtraEditors.TextEdit txtDeptCode;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtUnitDesc;
        private DevExpress.XtraEditors.TextEdit txtUnitCode;
        private DevExpress.XtraEditors.TextEdit txtCompName;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtCompCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtWrkGrpDesc;
        private DevExpress.XtraEditors.TextEdit txtWrkGrpCode;
        private DevExpress.XtraEditors.TextEdit txtSecDesc;
        private DevExpress.XtraEditors.TextEdit txtSecCode;
        private System.Windows.Forms.Label label9;
    }
}