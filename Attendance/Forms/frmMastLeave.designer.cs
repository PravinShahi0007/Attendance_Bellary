namespace Attendance.Forms
{
    partial class frmMastLeave
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
            this.label8 = new System.Windows.Forms.Label();
            this.txtWrkGrpDesc = new DevExpress.XtraEditors.TextEdit();
            this.txtWrkGrpCode = new DevExpress.XtraEditors.TextEdit();
            this.chkPaid = new DevExpress.XtraEditors.CheckEdit();
            this.grpUserRights = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.chkHalf = new DevExpress.XtraEditors.CheckEdit();
            this.chkPublicHL = new DevExpress.XtraEditors.CheckEdit();
            this.chkShowEntry = new DevExpress.XtraEditors.CheckEdit();
            this.chkEncash = new DevExpress.XtraEditors.CheckEdit();
            this.chkKeepAdv = new DevExpress.XtraEditors.CheckEdit();
            this.chkKeepBal = new DevExpress.XtraEditors.CheckEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSeqNo = new DevExpress.XtraEditors.TextEdit();
            this.txtCompName = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCompCode = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.txtLeaveTyp = new DevExpress.XtraEditors.TextEdit();
            this.Group2 = new System.Windows.Forms.GroupBox();
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtWrkGrpDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWrkGrpCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPaid.Properties)).BeginInit();
            this.grpUserRights.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkHalf.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPublicHL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowEntry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEncash.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKeepAdv.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKeepBal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeqNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLeaveTyp.Properties)).BeginInit();
            this.Group2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtWrkGrpDesc);
            this.groupBox1.Controls.Add(this.txtWrkGrpCode);
            this.groupBox1.Controls.Add(this.chkPaid);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.chkHalf);
            this.groupBox1.Controls.Add(this.chkPublicHL);
            this.groupBox1.Controls.Add(this.chkShowEntry);
            this.groupBox1.Controls.Add(this.chkEncash);
            this.groupBox1.Controls.Add(this.chkKeepAdv);
            this.groupBox1.Controls.Add(this.chkKeepBal);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtSeqNo);
            this.groupBox1.Controls.Add(this.txtCompName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCompCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.txtLeaveTyp);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(865, 124);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 15);
            this.label8.TabIndex = 29;
            this.label8.Text = "WrkGrpCode";
            // 
            // txtWrkGrpDesc
            // 
            this.txtWrkGrpDesc.Location = new System.Drawing.Point(200, 42);
            this.txtWrkGrpDesc.Name = "txtWrkGrpDesc";
            this.txtWrkGrpDesc.Properties.Mask.EditMask = "\\w{3,50}";
            this.txtWrkGrpDesc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtWrkGrpDesc.Properties.Mask.ShowPlaceHolders = false;
            this.txtWrkGrpDesc.Properties.ReadOnly = true;
            this.txtWrkGrpDesc.Size = new System.Drawing.Size(183, 20);
            this.txtWrkGrpDesc.TabIndex = 28;
            this.txtWrkGrpDesc.TabStop = false;
            // 
            // txtWrkGrpCode
            // 
            this.txtWrkGrpCode.Location = new System.Drawing.Point(98, 42);
            this.txtWrkGrpCode.Name = "txtWrkGrpCode";
            this.txtWrkGrpCode.Properties.Mask.EditMask = "\\w{3,10}";
            this.txtWrkGrpCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtWrkGrpCode.Properties.Mask.ShowPlaceHolders = false;
            this.txtWrkGrpCode.Size = new System.Drawing.Size(96, 20);
            this.txtWrkGrpCode.TabIndex = 1;
            this.txtWrkGrpCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWrkGrpCode_KeyDown);
            this.txtWrkGrpCode.Validated += new System.EventHandler(this.txtWrkGrpCode_Validated);
            // 
            // chkPaid
            // 
            this.chkPaid.Location = new System.Drawing.Point(744, 44);
            this.chkPaid.Name = "chkPaid";
            this.chkPaid.Properties.Caption = "Is Paid :";
            this.chkPaid.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chkPaid.Size = new System.Drawing.Size(110, 19);
            this.chkPaid.TabIndex = 11;
            // 
            // grpUserRights
            // 
            this.grpUserRights.Controls.Add(this.btnClose);
            this.grpUserRights.Controls.Add(this.btnCancel);
            this.grpUserRights.Controls.Add(this.btnDelete);
            this.grpUserRights.Controls.Add(this.btnUpdate);
            this.grpUserRights.Controls.Add(this.btnAdd);
            this.grpUserRights.Location = new System.Drawing.Point(12, 133);
            this.grpUserRights.Name = "grpUserRights";
            this.grpUserRights.Size = new System.Drawing.Size(865, 52);
            this.grpUserRights.TabIndex = 22;
            this.grpUserRights.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Cornsilk;
            this.btnClose.Location = new System.Drawing.Point(520, 14);
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
            this.btnCancel.Location = new System.Drawing.Point(440, 14);
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
            this.btnDelete.Location = new System.Drawing.Point(359, 14);
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
            this.btnUpdate.Location = new System.Drawing.Point(278, 14);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 32);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Cornsilk;
            this.btnAdd.Location = new System.Drawing.Point(197, 14);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 32);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkRed;
            this.label6.Location = new System.Drawing.Point(479, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 15);
            this.label6.TabIndex = 21;
            this.label6.Text = "Leave Features";
            // 
            // chkHalf
            // 
            this.chkHalf.Location = new System.Drawing.Point(591, 71);
            this.chkHalf.Name = "chkHalf";
            this.chkHalf.Properties.Caption = "Allow Half Posting :";
            this.chkHalf.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chkHalf.Size = new System.Drawing.Size(133, 19);
            this.chkHalf.TabIndex = 9;
            // 
            // chkPublicHL
            // 
            this.chkPublicHL.Location = new System.Drawing.Point(482, 97);
            this.chkPublicHL.Name = "chkPublicHL";
            this.chkPublicHL.Properties.Caption = "Public Holiday :";
            this.chkPublicHL.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chkPublicHL.Size = new System.Drawing.Size(103, 19);
            this.chkPublicHL.TabIndex = 7;
            // 
            // chkShowEntry
            // 
            this.chkShowEntry.Location = new System.Drawing.Point(591, 45);
            this.chkShowEntry.Name = "chkShowEntry";
            this.chkShowEntry.Properties.Caption = "Show In Leave Entry :";
            this.chkShowEntry.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chkShowEntry.Size = new System.Drawing.Size(133, 19);
            this.chkShowEntry.TabIndex = 8;
            // 
            // chkEncash
            // 
            this.chkEncash.Location = new System.Drawing.Point(591, 98);
            this.chkEncash.Name = "chkEncash";
            this.chkEncash.Properties.Caption = "Allow Encash :";
            this.chkEncash.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chkEncash.Size = new System.Drawing.Size(133, 19);
            this.chkEncash.TabIndex = 10;
            // 
            // chkKeepAdv
            // 
            this.chkKeepAdv.Location = new System.Drawing.Point(482, 70);
            this.chkKeepAdv.Name = "chkKeepAdv";
            this.chkKeepAdv.Properties.Caption = "Advance Leave :";
            this.chkKeepAdv.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chkKeepAdv.Size = new System.Drawing.Size(103, 19);
            this.chkKeepAdv.TabIndex = 6;
            // 
            // chkKeepBal
            // 
            this.chkKeepBal.Location = new System.Drawing.Point(482, 44);
            this.chkKeepBal.Name = "chkKeepBal";
            this.chkKeepBal.Properties.Caption = "Keep Balance :";
            this.chkKeepBal.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chkKeepBal.Size = new System.Drawing.Size(103, 19);
            this.chkKeepBal.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "Leave Entry Show Seq :";
            // 
            // txtSeqNo
            // 
            this.txtSeqNo.Location = new System.Drawing.Point(146, 95);
            this.txtSeqNo.Name = "txtSeqNo";
            this.txtSeqNo.Properties.Mask.EditMask = "n0";
            this.txtSeqNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtSeqNo.Size = new System.Drawing.Size(48, 20);
            this.txtSeqNo.TabIndex = 4;
            // 
            // txtCompName
            // 
            this.txtCompName.Location = new System.Drawing.Point(146, 16);
            this.txtCompName.Name = "txtCompName";
            this.txtCompName.Properties.ReadOnly = true;
            this.txtCompName.Size = new System.Drawing.Size(237, 20);
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
            this.txtCompCode.Location = new System.Drawing.Point(98, 16);
            this.txtCompCode.Name = "txtCompCode";
            this.txtCompCode.Size = new System.Drawing.Size(42, 20);
            this.txtCompCode.TabIndex = 0;
            this.txtCompCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCompCode_KeyDown);
            this.txtCompCode.Validated += new System.EventHandler(this.txtCompCode_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Leave Type :";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(146, 69);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.Mask.BeepOnError = true;
            this.txtDescription.Properties.Mask.EditMask = "[A-Za-z0-9()-.[\\] ]+";
            this.txtDescription.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtDescription.Properties.MaxLength = 30;
            this.txtDescription.Size = new System.Drawing.Size(237, 20);
            this.txtDescription.TabIndex = 3;
            // 
            // txtLeaveTyp
            // 
            this.txtLeaveTyp.Location = new System.Drawing.Point(98, 68);
            this.txtLeaveTyp.Name = "txtLeaveTyp";
            this.txtLeaveTyp.Properties.Mask.EditMask = "[A-Z]+";
            this.txtLeaveTyp.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtLeaveTyp.Properties.Mask.ShowPlaceHolders = false;
            this.txtLeaveTyp.Properties.MaxLength = 2;
            this.txtLeaveTyp.Size = new System.Drawing.Size(42, 20);
            this.txtLeaveTyp.TabIndex = 2;
            this.txtLeaveTyp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLeaveTyp_KeyDown);
            this.txtLeaveTyp.Validated += new System.EventHandler(this.txtLeaveTyp_Validated);
            // 
            // Group2
            // 
            this.Group2.Controls.Add(this.grid);
            this.Group2.Location = new System.Drawing.Point(12, 191);
            this.Group2.Name = "Group2";
            this.Group2.Size = new System.Drawing.Size(865, 305);
            this.Group2.TabIndex = 3;
            this.Group2.TabStop = false;
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(3, 17);
            this.grid.MainView = this.gridView1;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(859, 285);
            this.grid.TabIndex = 0;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gridView1.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridView1.GridControl = this.grid;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView1.OptionsFilter.AllowFilterEditor = false;
            this.gridView1.OptionsFilter.AllowFilterIncrementalSearch = false;
            this.gridView1.OptionsFilter.AllowMRUFilterList = false;
            this.gridView1.OptionsFilter.FilterEditorUseMenuForOperandsAndOperators = false;
            this.gridView1.OptionsFind.AllowFindPanel = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsMenu.EnableFooterMenu = false;
            this.gridView1.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView1.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gridView1.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.gridView1.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gridView1.OptionsMenu.ShowSplitItem = false;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // frmMastLeave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 508);
            this.Controls.Add(this.Group2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpUserRights);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMastLeave";
            this.Text = "Leave Type Configuration Master";
            this.Load += new System.EventHandler(this.frmMastLeave_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtWrkGrpDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWrkGrpCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPaid.Properties)).EndInit();
            this.grpUserRights.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkHalf.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPublicHL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowEntry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEncash.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKeepAdv.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKeepBal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeqNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLeaveTyp.Properties)).EndInit();
            this.Group2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtDescription;
        private DevExpress.XtraEditors.TextEdit txtLeaveTyp;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtCompCode;
        private DevExpress.XtraEditors.TextEdit txtCompName;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.CheckEdit chkHalf;
        private DevExpress.XtraEditors.CheckEdit chkPublicHL;
        private DevExpress.XtraEditors.CheckEdit chkShowEntry;
        private DevExpress.XtraEditors.CheckEdit chkEncash;
        private DevExpress.XtraEditors.CheckEdit chkKeepAdv;
        private DevExpress.XtraEditors.CheckEdit chkKeepBal;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.TextEdit txtSeqNo;
        private System.Windows.Forms.GroupBox Group2;
        private System.Windows.Forms.GroupBox grpUserRights;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.CheckEdit chkPaid;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.TextEdit txtWrkGrpDesc;
        private DevExpress.XtraEditors.TextEdit txtWrkGrpCode;
    }
}