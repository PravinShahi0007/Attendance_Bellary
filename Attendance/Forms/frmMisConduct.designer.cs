namespace Attendance.Forms
{
    partial class frmMisConduct
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
            this.grpUserRights = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.ctrlEmp1 = new Attendance.ctrlEmp();
            this.GrpMain = new System.Windows.Forms.GroupBox();
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.gridview = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.txtRemarks = new DevExpress.XtraEditors.MemoEdit();
            this.txtFinActionDesc = new DevExpress.XtraEditors.MemoEdit();
            this.txtActionDesc = new DevExpress.XtraEditors.MemoEdit();
            this.txtMisConDesc = new DevExpress.XtraEditors.MemoEdit();
            this.txtFinActionDt = new DevExpress.XtraEditors.DateEdit();
            this.txtActionDt = new DevExpress.XtraEditors.DateEdit();
            this.txtMisConDt = new DevExpress.XtraEditors.DateEdit();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grpUserRights.SuspendLayout();
            this.GrpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemarks.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFinActionDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtActionDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMisConDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFinActionDt.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFinActionDt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtActionDt.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtActionDt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMisConDt.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMisConDt.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grpUserRights
            // 
            this.grpUserRights.Controls.Add(this.btnClose);
            this.grpUserRights.Controls.Add(this.btnCancel);
            this.grpUserRights.Controls.Add(this.btnDelete);
            this.grpUserRights.Controls.Add(this.btnUpdate);
            this.grpUserRights.Controls.Add(this.btnAdd);
            this.grpUserRights.Location = new System.Drawing.Point(12, 479);
            this.grpUserRights.Name = "grpUserRights";
            this.grpUserRights.Size = new System.Drawing.Size(930, 52);
            this.grpUserRights.TabIndex = 2;
            this.grpUserRights.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Cornsilk;
            this.btnClose.Location = new System.Drawing.Point(581, 14);
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
            this.btnCancel.Location = new System.Drawing.Point(501, 14);
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
            this.btnDelete.Location = new System.Drawing.Point(420, 14);
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
            this.btnUpdate.Location = new System.Drawing.Point(339, 14);
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
            this.btnAdd.Location = new System.Drawing.Point(258, 14);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 32);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // ctrlEmp1
            // 
            this.ctrlEmp1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlEmp1.Location = new System.Drawing.Point(12, 13);
            this.ctrlEmp1.Name = "ctrlEmp1";
            this.ctrlEmp1.Size = new System.Drawing.Size(933, 171);
            this.ctrlEmp1.TabIndex = 0;
            // 
            // GrpMain
            // 
            this.GrpMain.Controls.Add(this.grid);
            this.GrpMain.Controls.Add(this.label1);
            this.GrpMain.Controls.Add(this.txtID);
            this.GrpMain.Controls.Add(this.txtRemarks);
            this.GrpMain.Controls.Add(this.txtFinActionDesc);
            this.GrpMain.Controls.Add(this.txtActionDesc);
            this.GrpMain.Controls.Add(this.txtMisConDesc);
            this.GrpMain.Controls.Add(this.txtFinActionDt);
            this.GrpMain.Controls.Add(this.txtActionDt);
            this.GrpMain.Controls.Add(this.txtMisConDt);
            this.GrpMain.Controls.Add(this.label15);
            this.GrpMain.Controls.Add(this.label14);
            this.GrpMain.Controls.Add(this.label12);
            this.GrpMain.Controls.Add(this.label7);
            this.GrpMain.Controls.Add(this.label5);
            this.GrpMain.Location = new System.Drawing.Point(13, 184);
            this.GrpMain.Name = "GrpMain";
            this.GrpMain.Size = new System.Drawing.Size(930, 289);
            this.GrpMain.TabIndex = 1;
            this.GrpMain.TabStop = false;
            // 
            // grid
            // 
            this.grid.Location = new System.Drawing.Point(500, 131);
            this.grid.MainView = this.gridview;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(419, 152);
            this.grid.TabIndex = 9;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridview});
            // 
            // gridview
            // 
            this.gridview.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gridview.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridview.GridControl = this.grid;
            this.gridview.Name = "gridview";
            this.gridview.OptionsBehavior.Editable = false;
            this.gridview.OptionsCustomization.AllowColumnMoving = false;
            this.gridview.OptionsCustomization.AllowFilter = false;
            this.gridview.OptionsCustomization.AllowGroup = false;
            this.gridview.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridview.OptionsCustomization.AllowSort = false;
            this.gridview.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridview.OptionsFilter.AllowFilterEditor = false;
            this.gridview.OptionsFilter.AllowFilterIncrementalSearch = false;
            this.gridview.OptionsFilter.AllowMRUFilterList = false;
            this.gridview.OptionsFilter.FilterEditorUseMenuForOperandsAndOperators = false;
            this.gridview.OptionsFind.AllowFindPanel = false;
            this.gridview.OptionsMenu.EnableColumnMenu = false;
            this.gridview.OptionsMenu.EnableFooterMenu = false;
            this.gridview.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridview.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.False;
            this.gridview.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gridview.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.gridview.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gridview.OptionsMenu.ShowSplitItem = false;
            this.gridview.OptionsView.ShowDetailButtons = false;
            this.gridview.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridview.OptionsView.ShowGroupPanel = false;
            this.gridview.DoubleClick += new System.EventHandler(this.gridview_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(497, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "History  :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(66, 20);
            this.txtID.Name = "txtID";
            this.txtID.Properties.Mask.EditMask = "[0-9]+";
            this.txtID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtID.Size = new System.Drawing.Size(85, 20);
            this.txtID.TabIndex = 0;
            this.txtID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtID_KeyDown);
            this.txtID.Validated += new System.EventHandler(this.txtID_Validated);
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(500, 46);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Properties.MaxLength = 200;
            this.txtRemarks.Size = new System.Drawing.Size(419, 51);
            this.txtRemarks.TabIndex = 7;
            // 
            // txtFinActionDesc
            // 
            this.txtFinActionDesc.Location = new System.Drawing.Point(10, 213);
            this.txtFinActionDesc.Name = "txtFinActionDesc";
            this.txtFinActionDesc.Properties.MaxLength = 200;
            this.txtFinActionDesc.Size = new System.Drawing.Size(454, 70);
            this.txtFinActionDesc.TabIndex = 6;
            // 
            // txtActionDesc
            // 
            this.txtActionDesc.Location = new System.Drawing.Point(9, 129);
            this.txtActionDesc.Name = "txtActionDesc";
            this.txtActionDesc.Properties.MaxLength = 200;
            this.txtActionDesc.Size = new System.Drawing.Size(455, 52);
            this.txtActionDesc.TabIndex = 4;
            // 
            // txtMisConDesc
            // 
            this.txtMisConDesc.Location = new System.Drawing.Point(9, 46);
            this.txtMisConDesc.Name = "txtMisConDesc";
            this.txtMisConDesc.Properties.MaxLength = 200;
            this.txtMisConDesc.Size = new System.Drawing.Size(455, 51);
            this.txtMisConDesc.TabIndex = 2;
            // 
            // txtFinActionDt
            // 
            this.txtFinActionDt.EditValue = null;
            this.txtFinActionDt.Location = new System.Drawing.Point(364, 187);
            this.txtFinActionDt.Name = "txtFinActionDt";
            this.txtFinActionDt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFinActionDt.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFinActionDt.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtFinActionDt.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtFinActionDt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.txtFinActionDt.Size = new System.Drawing.Size(100, 20);
            this.txtFinActionDt.TabIndex = 5;
            // 
            // txtActionDt
            // 
            this.txtActionDt.EditValue = null;
            this.txtActionDt.Location = new System.Drawing.Point(364, 103);
            this.txtActionDt.Name = "txtActionDt";
            this.txtActionDt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtActionDt.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtActionDt.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtActionDt.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtActionDt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.txtActionDt.Size = new System.Drawing.Size(100, 20);
            this.txtActionDt.TabIndex = 3;
            // 
            // txtMisConDt
            // 
            this.txtMisConDt.EditValue = null;
            this.txtMisConDt.Location = new System.Drawing.Point(364, 22);
            this.txtMisConDt.Name = "txtMisConDt";
            this.txtMisConDt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtMisConDt.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtMisConDt.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtMisConDt.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtMisConDt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.txtMisConDt.Size = new System.Drawing.Size(100, 20);
            this.txtMisConDt.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(235, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(123, 15);
            this.label15.TabIndex = 97;
            this.label15.Text = "Date of MisConduct  :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(497, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(66, 15);
            this.label14.TabIndex = 96;
            this.label14.Text = "Remarks  :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(7, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 19);
            this.label12.TabIndex = 95;
            this.label12.Text = "UnqID  :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(238, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 15);
            this.label7.TabIndex = 94;
            this.label7.Text = "Date Of Final Action :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(235, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 15);
            this.label5.TabIndex = 93;
            this.label5.Text = "Date of Action :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmMisConduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 543);
            this.Controls.Add(this.GrpMain);
            this.Controls.Add(this.ctrlEmp1);
            this.Controls.Add(this.grpUserRights);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "frmMisConduct";
            this.Text = "Employee MisConduct Register";
            this.Load += new System.EventHandler(this.frmMisConduct_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMisConduct_KeyDown);
            this.grpUserRights.ResumeLayout(false);
            this.GrpMain.ResumeLayout(false);
            this.GrpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemarks.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFinActionDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtActionDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMisConDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFinActionDt.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFinActionDt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtActionDt.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtActionDt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMisConDt.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMisConDt.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpUserRights;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private ctrlEmp ctrlEmp1;
        private System.Windows.Forms.GroupBox GrpMain;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.MemoEdit txtRemarks;
        private DevExpress.XtraEditors.MemoEdit txtFinActionDesc;
        private DevExpress.XtraEditors.MemoEdit txtActionDesc;
        private DevExpress.XtraEditors.MemoEdit txtMisConDesc;
        private DevExpress.XtraEditors.DateEdit txtFinActionDt;
        private DevExpress.XtraEditors.DateEdit txtActionDt;
        private DevExpress.XtraEditors.DateEdit txtMisConDt;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtID;
        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridview;
    }
}