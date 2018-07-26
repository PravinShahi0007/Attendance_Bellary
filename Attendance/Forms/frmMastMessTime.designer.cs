namespace Attendance.Forms
{
    partial class frmMastMessTime
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFoodDesc = new DevExpress.XtraEditors.TextEdit();
            this.txtFoodCode = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMessDesc = new DevExpress.XtraEditors.TextEdit();
            this.txtMessCode = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUnitDesc = new DevExpress.XtraEditors.TextEdit();
            this.txtUnitCode = new DevExpress.XtraEditors.TextEdit();
            this.txtCompName = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCompCode = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpUserRights = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtStartTime = new DevExpress.XtraEditors.TimeEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEndTime = new DevExpress.XtraEditors.TimeEdit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFoodDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFoodCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompCode.Properties)).BeginInit();
            this.grpUserRights.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndTime.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEndTime);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtStartTime);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtFoodDesc);
            this.groupBox1.Controls.Add(this.txtFoodCode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtMessDesc);
            this.groupBox1.Controls.Add(this.txtMessCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtUnitDesc);
            this.groupBox1.Controls.Add(this.txtUnitCode);
            this.groupBox1.Controls.Add(this.txtCompName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCompCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(455, 156);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 17;
            this.label1.Text = "Start Time";
            // 
            // txtFoodDesc
            // 
            this.txtFoodDesc.Location = new System.Drawing.Point(204, 94);
            this.txtFoodDesc.Name = "txtFoodDesc";
            this.txtFoodDesc.Properties.Mask.EditMask = "[0-9A-Za-z ]+";
            this.txtFoodDesc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtFoodDesc.Properties.Mask.ShowPlaceHolders = false;
            this.txtFoodDesc.Properties.MaxLength = 50;
            this.txtFoodDesc.Properties.ReadOnly = true;
            this.txtFoodDesc.Size = new System.Drawing.Size(238, 20);
            this.txtFoodDesc.TabIndex = 16;
            // 
            // txtFoodCode
            // 
            this.txtFoodCode.Location = new System.Drawing.Point(102, 94);
            this.txtFoodCode.Name = "txtFoodCode";
            this.txtFoodCode.Properties.Mask.EditMask = "[A-Za-z]+";
            this.txtFoodCode.Properties.Mask.ShowPlaceHolders = false;
            this.txtFoodCode.Properties.MaxLength = 3;
            this.txtFoodCode.Size = new System.Drawing.Size(96, 20);
            this.txtFoodCode.TabIndex = 3;
            this.txtFoodCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFoodCode_KeyDown);
            this.txtFoodCode.Validated += new System.EventHandler(this.txtFoodCode_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "Food Code";
            // 
            // txtMessDesc
            // 
            this.txtMessDesc.Location = new System.Drawing.Point(204, 68);
            this.txtMessDesc.Name = "txtMessDesc";
            this.txtMessDesc.Properties.Mask.EditMask = "[0-9A-Za-z ]+";
            this.txtMessDesc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtMessDesc.Properties.Mask.ShowPlaceHolders = false;
            this.txtMessDesc.Properties.MaxLength = 50;
            this.txtMessDesc.Properties.ReadOnly = true;
            this.txtMessDesc.Size = new System.Drawing.Size(238, 20);
            this.txtMessDesc.TabIndex = 13;
            // 
            // txtMessCode
            // 
            this.txtMessCode.Location = new System.Drawing.Point(102, 68);
            this.txtMessCode.Name = "txtMessCode";
            this.txtMessCode.Properties.Mask.EditMask = "[0-9]+";
            this.txtMessCode.Properties.Mask.ShowPlaceHolders = false;
            this.txtMessCode.Properties.MaxLength = 3;
            this.txtMessCode.Size = new System.Drawing.Size(96, 20);
            this.txtMessCode.TabIndex = 2;
            this.txtMessCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMessCode_KeyDown);
            this.txtMessCode.Validated += new System.EventHandler(this.txtMessCode_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Mess Code";
            // 
            // txtUnitDesc
            // 
            this.txtUnitDesc.Location = new System.Drawing.Point(204, 42);
            this.txtUnitDesc.Name = "txtUnitDesc";
            this.txtUnitDesc.Properties.Mask.ShowPlaceHolders = false;
            this.txtUnitDesc.Properties.ReadOnly = true;
            this.txtUnitDesc.Size = new System.Drawing.Size(238, 20);
            this.txtUnitDesc.TabIndex = 5;
            // 
            // txtUnitCode
            // 
            this.txtUnitCode.Location = new System.Drawing.Point(102, 42);
            this.txtUnitCode.Name = "txtUnitCode";
            this.txtUnitCode.Properties.Mask.EditMask = "[0-9]+";
            this.txtUnitCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtUnitCode.Properties.Mask.ShowPlaceHolders = false;
            this.txtUnitCode.Properties.MaxLength = 3;
            this.txtUnitCode.Size = new System.Drawing.Size(96, 20);
            this.txtUnitCode.TabIndex = 1;
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
            this.label4.Location = new System.Drawing.Point(8, 17);
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
            this.label2.Location = new System.Drawing.Point(8, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "UnitCode";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Cornsilk;
            this.btnAdd.Location = new System.Drawing.Point(23, 14);
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
            this.grpUserRights.Location = new System.Drawing.Point(12, 165);
            this.grpUserRights.Name = "grpUserRights";
            this.grpUserRights.Size = new System.Drawing.Size(452, 52);
            this.grpUserRights.TabIndex = 2;
            this.grpUserRights.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Cornsilk;
            this.btnClose.Location = new System.Drawing.Point(346, 14);
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
            this.btnCancel.Location = new System.Drawing.Point(266, 14);
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
            this.btnDelete.Location = new System.Drawing.Point(185, 14);
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
            this.btnUpdate.Location = new System.Drawing.Point(104, 14);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 32);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtStartTime
            // 
            this.txtStartTime.EditValue = new System.DateTime(2017, 10, 27, 0, 0, 0, 0);
            this.txtStartTime.Location = new System.Drawing.Point(102, 121);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtStartTime.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtStartTime.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtStartTime.Size = new System.Drawing.Size(96, 20);
            this.txtStartTime.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(246, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 15);
            this.label6.TabIndex = 19;
            this.label6.Text = "End Time";
            // 
            // txtEndTime
            // 
            this.txtEndTime.EditValue = new System.DateTime(2017, 10, 27, 0, 0, 0, 0);
            this.txtEndTime.Location = new System.Drawing.Point(346, 120);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtEndTime.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtEndTime.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtEndTime.Size = new System.Drawing.Size(96, 20);
            this.txtEndTime.TabIndex = 5;
            // 
            // frmMastMessTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 229);
            this.Controls.Add(this.grpUserRights);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMastMessTime";
            this.Text = "Mess/Canteen Food Timing Master";
            this.Load += new System.EventHandler(this.frmMastMessTime_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFoodDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFoodCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompCode.Properties)).EndInit();
            this.grpUserRights.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtStartTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndTime.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
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
        private DevExpress.XtraEditors.TextEdit txtMessDesc;
        private DevExpress.XtraEditors.TextEdit txtMessCode;
        private DevExpress.XtraEditors.TextEdit txtFoodDesc;
        private DevExpress.XtraEditors.TextEdit txtFoodCode;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TimeEdit txtEndTime;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TimeEdit txtStartTime;
    }
}