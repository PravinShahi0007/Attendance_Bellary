namespace Attendance.Forms
{
    partial class frmDataDownload
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
            this.tblp = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grpGrid = new DevExpress.XtraGrid.GridControl();
            this.gv_avbl = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.SEL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.Location = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MachineIP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MachineNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Records = new DevExpress.XtraGrid.Columns.GridColumn();
            this.AutoClear = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Type = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Remarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Mess = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grpButtons = new System.Windows.Forms.GroupBox();
            this.btnClearMach = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnUnockMach = new DevExpress.XtraEditors.SimpleButton();
            this.btnRestartMach = new DevExpress.XtraEditors.SimpleButton();
            this.btnSetTime = new DevExpress.XtraEditors.SimpleButton();
            this.btnDownload = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelAll = new DevExpress.XtraEditors.SimpleButton();
            this.tblp.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_avbl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.grpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblp
            // 
            this.tblp.ColumnCount = 1;
            this.tblp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblp.Controls.Add(this.groupBox2, 0, 1);
            this.tblp.Controls.Add(this.grpButtons, 0, 0);
            this.tblp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblp.Location = new System.Drawing.Point(0, 0);
            this.tblp.Name = "tblp";
            this.tblp.RowCount = 2;
            this.tblp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tblp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblp.Size = new System.Drawing.Size(951, 576);
            this.tblp.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grpGrid);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(945, 501);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Available Machine";
            // 
            // grpGrid
            // 
            this.grpGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpGrid.Location = new System.Drawing.Point(3, 17);
            this.grpGrid.MainView = this.gv_avbl;
            this.grpGrid.Name = "grpGrid";
            this.grpGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.grpGrid.Size = new System.Drawing.Size(939, 481);
            this.grpGrid.TabIndex = 3;
            this.grpGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_avbl});
            // 
            // gv_avbl
            // 
            this.gv_avbl.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.SEL,
            this.Location,
            this.MachineIP,
            this.MachineNo,
            this.Records,
            this.AutoClear,
            this.Type,
            this.Remarks,
            this.Mess});
            this.gv_avbl.GridControl = this.grpGrid;
            this.gv_avbl.Name = "gv_avbl";
            this.gv_avbl.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_avbl.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_avbl.OptionsCustomization.AllowColumnMoving = false;
            this.gv_avbl.OptionsCustomization.AllowFilter = false;
            this.gv_avbl.OptionsCustomization.AllowGroup = false;
            this.gv_avbl.OptionsCustomization.AllowQuickHideColumns = false;
            this.gv_avbl.OptionsCustomization.AllowRowSizing = true;
            this.gv_avbl.OptionsCustomization.AllowSort = false;
            this.gv_avbl.OptionsDetail.AllowZoomDetail = false;
            this.gv_avbl.OptionsDetail.EnableMasterViewMode = false;
            this.gv_avbl.OptionsDetail.ShowDetailTabs = false;
            this.gv_avbl.OptionsDetail.SmartDetailExpand = false;
            this.gv_avbl.OptionsMenu.EnableColumnMenu = false;
            this.gv_avbl.OptionsMenu.EnableFooterMenu = false;
            this.gv_avbl.OptionsMenu.EnableGroupPanelMenu = false;
            this.gv_avbl.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.False;
            this.gv_avbl.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gv_avbl.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.gv_avbl.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gv_avbl.OptionsMenu.ShowSplitItem = false;
            this.gv_avbl.OptionsNavigation.EnterMoveNextColumn = true;
            this.gv_avbl.OptionsSelection.MultiSelect = true;
            this.gv_avbl.OptionsView.ShowDetailButtons = false;
            this.gv_avbl.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gv_avbl.OptionsView.ShowGroupPanel = false;
            // 
            // SEL
            // 
            this.SEL.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SEL.AppearanceHeader.Options.UseFont = true;
            this.SEL.AppearanceHeader.Options.UseTextOptions = true;
            this.SEL.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SEL.Caption = "SEL";
            this.SEL.ColumnEdit = this.repositoryItemCheckEdit1;
            this.SEL.FieldName = "SEL";
            this.SEL.Name = "SEL";
            this.SEL.OptionsColumn.AllowMove = false;
            this.SEL.OptionsColumn.AllowShowHide = false;
            this.SEL.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.SEL.OptionsColumn.FixedWidth = true;
            this.SEL.OptionsFilter.AllowAutoFilter = false;
            this.SEL.OptionsFilter.AllowFilter = false;
            this.SEL.Visible = true;
            this.SEL.VisibleIndex = 0;
            this.SEL.Width = 41;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.DisplayValueChecked = "1";
            this.repositoryItemCheckEdit1.DisplayValueGrayed = "0";
            this.repositoryItemCheckEdit1.DisplayValueUnchecked = "0";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.ValueGrayed = false;
            // 
            // Location
            // 
            this.Location.Caption = "Location";
            this.Location.FieldName = "MachineDesc";
            this.Location.Name = "Location";
            this.Location.OptionsColumn.AllowEdit = false;
            this.Location.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.Location.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.Location.OptionsColumn.AllowMove = false;
            this.Location.OptionsColumn.ReadOnly = true;
            this.Location.Visible = true;
            this.Location.VisibleIndex = 1;
            // 
            // MachineIP
            // 
            this.MachineIP.Caption = "IP Address";
            this.MachineIP.FieldName = "MachineIP";
            this.MachineIP.Name = "MachineIP";
            this.MachineIP.OptionsColumn.AllowEdit = false;
            this.MachineIP.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.MachineIP.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.MachineIP.OptionsColumn.AllowMove = false;
            this.MachineIP.OptionsColumn.ReadOnly = true;
            this.MachineIP.Visible = true;
            this.MachineIP.VisibleIndex = 3;
            // 
            // MachineNo
            // 
            this.MachineNo.Caption = "MNO";
            this.MachineNo.FieldName = "MachineNo";
            this.MachineNo.Name = "MachineNo";
            this.MachineNo.OptionsColumn.AllowEdit = false;
            this.MachineNo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.MachineNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.MachineNo.OptionsColumn.AllowMove = false;
            this.MachineNo.OptionsColumn.ReadOnly = true;
            this.MachineNo.Visible = true;
            this.MachineNo.VisibleIndex = 2;
            // 
            // Records
            // 
            this.Records.Caption = "Records";
            this.Records.FieldName = "Records";
            this.Records.Name = "Records";
            this.Records.OptionsColumn.AllowEdit = false;
            this.Records.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.Records.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.Records.OptionsColumn.AllowMove = false;
            this.Records.OptionsColumn.ReadOnly = true;
            this.Records.Visible = true;
            this.Records.VisibleIndex = 4;
            // 
            // AutoClear
            // 
            this.AutoClear.Caption = "AutoClear";
            this.AutoClear.FieldName = "AutoClear";
            this.AutoClear.Name = "AutoClear";
            this.AutoClear.OptionsColumn.AllowEdit = false;
            this.AutoClear.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.AutoClear.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.AutoClear.OptionsColumn.AllowMove = false;
            this.AutoClear.OptionsColumn.ReadOnly = true;
            this.AutoClear.Visible = true;
            this.AutoClear.VisibleIndex = 5;
            // 
            // Type
            // 
            this.Type.Caption = "Type";
            this.Type.FieldName = "IOFLG";
            this.Type.Name = "Type";
            this.Type.OptionsColumn.AllowEdit = false;
            this.Type.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.Type.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.Type.OptionsColumn.AllowMove = false;
            this.Type.OptionsColumn.ReadOnly = true;
            this.Type.Visible = true;
            this.Type.VisibleIndex = 6;
            // 
            // Remarks
            // 
            this.Remarks.Caption = "Remarks";
            this.Remarks.FieldName = "Remarks";
            this.Remarks.Name = "Remarks";
            this.Remarks.OptionsColumn.AllowEdit = false;
            this.Remarks.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.Remarks.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.Remarks.OptionsColumn.AllowMove = false;
            this.Remarks.OptionsColumn.ReadOnly = true;
            this.Remarks.Visible = true;
            this.Remarks.VisibleIndex = 7;
            // 
            // Mess
            // 
            this.Mess.Caption = "Mess";
            this.Mess.FieldName = "CanteenFLG";
            this.Mess.Name = "Mess";
            this.Mess.OptionsColumn.AllowEdit = false;
            this.Mess.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.Mess.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.Mess.OptionsColumn.AllowMove = false;
            this.Mess.OptionsColumn.ReadOnly = true;
            this.Mess.Visible = true;
            this.Mess.VisibleIndex = 8;
            // 
            // grpButtons
            // 
            this.grpButtons.Controls.Add(this.btnClearMach);
            this.grpButtons.Controls.Add(this.btnExport);
            this.grpButtons.Controls.Add(this.btnUnockMach);
            this.grpButtons.Controls.Add(this.btnRestartMach);
            this.grpButtons.Controls.Add(this.btnSetTime);
            this.grpButtons.Controls.Add(this.btnDownload);
            this.grpButtons.Controls.Add(this.btnSelAll);
            this.grpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpButtons.Location = new System.Drawing.Point(3, 3);
            this.grpButtons.Name = "grpButtons";
            this.grpButtons.Size = new System.Drawing.Size(945, 63);
            this.grpButtons.TabIndex = 4;
            this.grpButtons.TabStop = false;
            // 
            // btnClearMach
            // 
            this.btnClearMach.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearMach.Appearance.Options.UseFont = true;
            this.btnClearMach.Location = new System.Drawing.Point(642, 23);
            this.btnClearMach.Name = "btnClearMach";
            this.btnClearMach.Size = new System.Drawing.Size(120, 27);
            this.btnClearMach.TabIndex = 7;
            this.btnClearMach.Text = "&Clear Machine";
            this.btnClearMach.Click += new System.EventHandler(this.btnClearMach_Click);
            // 
            // btnExport
            // 
            this.btnExport.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Appearance.Options.UseFont = true;
            this.btnExport.Location = new System.Drawing.Point(816, 23);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(120, 27);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "&Export Errors";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnUnockMach
            // 
            this.btnUnockMach.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnockMach.Appearance.Options.UseFont = true;
            this.btnUnockMach.Location = new System.Drawing.Point(516, 23);
            this.btnUnockMach.Name = "btnUnockMach";
            this.btnUnockMach.Size = new System.Drawing.Size(120, 27);
            this.btnUnockMach.TabIndex = 5;
            this.btnUnockMach.Text = "&Unlock Machine";
            this.btnUnockMach.Click += new System.EventHandler(this.btnUnockMach_Click);
            // 
            // btnRestartMach
            // 
            this.btnRestartMach.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestartMach.Appearance.Options.UseFont = true;
            this.btnRestartMach.Location = new System.Drawing.Point(390, 23);
            this.btnRestartMach.Name = "btnRestartMach";
            this.btnRestartMach.Size = new System.Drawing.Size(120, 27);
            this.btnRestartMach.TabIndex = 4;
            this.btnRestartMach.Text = "&Restart Machine";
            this.btnRestartMach.Click += new System.EventHandler(this.btnRestartMach_Click);
            // 
            // btnSetTime
            // 
            this.btnSetTime.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetTime.Appearance.Options.UseFont = true;
            this.btnSetTime.Location = new System.Drawing.Point(264, 23);
            this.btnSetTime.Name = "btnSetTime";
            this.btnSetTime.Size = new System.Drawing.Size(120, 27);
            this.btnSetTime.TabIndex = 2;
            this.btnSetTime.Text = "Set &DateTime";
            this.btnSetTime.Click += new System.EventHandler(this.btnSetTime_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Appearance.Options.UseFont = true;
            this.btnDownload.Location = new System.Drawing.Point(138, 23);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(120, 27);
            this.btnDownload.TabIndex = 1;
            this.btnDownload.Text = "Download &Logs";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnSelAll
            // 
            this.btnSelAll.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelAll.Appearance.Options.UseFont = true;
            this.btnSelAll.Location = new System.Drawing.Point(12, 23);
            this.btnSelAll.Name = "btnSelAll";
            this.btnSelAll.Size = new System.Drawing.Size(120, 27);
            this.btnSelAll.TabIndex = 0;
            this.btnSelAll.Text = "Select &All/None";
            this.btnSelAll.Click += new System.EventHandler(this.btnSelAll_Click);
            // 
            // frmDataDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 576);
            this.Controls.Add(this.tblp);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmDataDownload";
            this.Text = "Data Download";
            this.Load += new System.EventHandler(this.frmDataDownload_Load);
            this.tblp.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_avbl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.grpButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblp;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl grpGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_avbl;
        private DevExpress.XtraGrid.Columns.GridColumn SEL;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn Location;
        private DevExpress.XtraGrid.Columns.GridColumn MachineIP;
        private DevExpress.XtraGrid.Columns.GridColumn MachineNo;
        private DevExpress.XtraGrid.Columns.GridColumn Records;
        private DevExpress.XtraGrid.Columns.GridColumn AutoClear;
        private DevExpress.XtraGrid.Columns.GridColumn Type;
        private DevExpress.XtraGrid.Columns.GridColumn Remarks;
        private DevExpress.XtraGrid.Columns.GridColumn Mess;
        private System.Windows.Forms.GroupBox grpButtons;
        private DevExpress.XtraEditors.SimpleButton btnRestartMach;
        private DevExpress.XtraEditors.SimpleButton btnSetTime;
        private DevExpress.XtraEditors.SimpleButton btnDownload;
        private DevExpress.XtraEditors.SimpleButton btnSelAll;
        private DevExpress.XtraEditors.SimpleButton btnUnockMach;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraEditors.SimpleButton btnClearMach;
    }
}