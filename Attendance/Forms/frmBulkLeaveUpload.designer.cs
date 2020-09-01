namespace Attendance.Forms
{
    partial class frmBulkLeaveUpload
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grd_view = new DevExpress.XtraGrid.GridControl();
            this.grd_view1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colEmpUnqID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFromDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colToDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLeaveType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Remarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtBrowse = new System.Windows.Forms.TextBox();
            this.colWrkGrp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHalfDayFlg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPostID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_view)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_view1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupControl2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.1196F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.8804F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(945, 602);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.Appearance.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.grd_view);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(3, 87);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(939, 512);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Data";
            // 
            // grd_view
            // 
            this.grd_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_view.Location = new System.Drawing.Point(2, 21);
            this.grd_view.MainView = this.grd_view1;
            this.grd_view.Name = "grd_view";
            this.grd_view.Size = new System.Drawing.Size(935, 489);
            this.grd_view.TabIndex = 2;
            this.grd_view.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grd_view1});
            // 
            // grd_view1
            // 
            this.grd_view1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colWrkGrp,
            this.colEmpUnqID,
            this.colFromDate,
            this.colToDate,
            this.colLeaveType,
            this.colHalfDayFlg,
            this.colPostID,
            this.colLocation,
            this.Remarks});
            this.grd_view1.GridControl = this.grd_view;
            this.grd_view1.Name = "grd_view1";
            this.grd_view1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grd_view1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.grd_view1.OptionsBehavior.Editable = false;
            this.grd_view1.OptionsBehavior.ReadOnly = true;
            this.grd_view1.OptionsCustomization.AllowColumnMoving = false;
            this.grd_view1.OptionsCustomization.AllowFilter = false;
            this.grd_view1.OptionsCustomization.AllowGroup = false;
            this.grd_view1.OptionsCustomization.AllowQuickHideColumns = false;
            this.grd_view1.OptionsCustomization.AllowRowSizing = true;
            this.grd_view1.OptionsCustomization.AllowSort = false;
            this.grd_view1.OptionsDetail.AllowZoomDetail = false;
            this.grd_view1.OptionsDetail.EnableMasterViewMode = false;
            this.grd_view1.OptionsDetail.ShowDetailTabs = false;
            this.grd_view1.OptionsDetail.SmartDetailExpand = false;
            this.grd_view1.OptionsMenu.EnableColumnMenu = false;
            this.grd_view1.OptionsMenu.EnableFooterMenu = false;
            this.grd_view1.OptionsMenu.EnableGroupPanelMenu = false;
            this.grd_view1.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.False;
            this.grd_view1.OptionsMenu.ShowAutoFilterRowItem = false;
            this.grd_view1.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.grd_view1.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.grd_view1.OptionsMenu.ShowSplitItem = false;
            this.grd_view1.OptionsNavigation.EnterMoveNextColumn = true;
            this.grd_view1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grd_view1.OptionsView.ShowDetailButtons = false;
            this.grd_view1.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grd_view1.OptionsView.ShowGroupPanel = false;
            // 
            // colEmpUnqID
            // 
            this.colEmpUnqID.Caption = "EmpUnqID";
            this.colEmpUnqID.FieldName = "EmpUnqID";
            this.colEmpUnqID.Name = "colEmpUnqID";
            this.colEmpUnqID.OptionsColumn.AllowEdit = false;
            this.colEmpUnqID.OptionsColumn.AllowMove = false;
            this.colEmpUnqID.OptionsColumn.ReadOnly = true;
            this.colEmpUnqID.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.colEmpUnqID.Visible = true;
            this.colEmpUnqID.VisibleIndex = 1;
            this.colEmpUnqID.Width = 97;
            // 
            // colFromDate
            // 
            this.colFromDate.Caption = "FromDate";
            this.colFromDate.FieldName = "FromDate";
            this.colFromDate.Name = "colFromDate";
            this.colFromDate.OptionsColumn.AllowEdit = false;
            this.colFromDate.OptionsColumn.AllowMove = false;
            this.colFromDate.OptionsColumn.ReadOnly = true;
            this.colFromDate.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.colFromDate.Visible = true;
            this.colFromDate.VisibleIndex = 2;
            this.colFromDate.Width = 136;
            // 
            // colToDate
            // 
            this.colToDate.Caption = "ToDate";
            this.colToDate.FieldName = "ToDate";
            this.colToDate.Name = "colToDate";
            this.colToDate.OptionsColumn.AllowEdit = false;
            this.colToDate.OptionsColumn.AllowMove = false;
            this.colToDate.OptionsColumn.ReadOnly = true;
            this.colToDate.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.colToDate.Visible = true;
            this.colToDate.VisibleIndex = 3;
            this.colToDate.Width = 136;
            // 
            // colLeaveType
            // 
            this.colLeaveType.Caption = "LeaveType";
            this.colLeaveType.FieldName = "LeaveType";
            this.colLeaveType.Name = "colLeaveType";
            this.colLeaveType.OptionsColumn.AllowEdit = false;
            this.colLeaveType.OptionsColumn.AllowMove = false;
            this.colLeaveType.OptionsColumn.ReadOnly = true;
            this.colLeaveType.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.colLeaveType.Visible = true;
            this.colLeaveType.VisibleIndex = 4;
            this.colLeaveType.Width = 136;
            // 
            // Remarks
            // 
            this.Remarks.Caption = "Remarks";
            this.Remarks.FieldName = "Remarks";
            this.Remarks.Name = "Remarks";
            this.Remarks.OptionsColumn.AllowEdit = false;
            this.Remarks.OptionsColumn.AllowMove = false;
            this.Remarks.OptionsColumn.ReadOnly = true;
            this.Remarks.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.Remarks.Visible = true;
            this.Remarks.VisibleIndex = 8;
            this.Remarks.Width = 270;
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.Appearance.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.btnExport);
            this.groupControl1.Controls.Add(this.btnPreview);
            this.groupControl1.Controls.Add(this.btnImport);
            this.groupControl1.Controls.Add(this.btnBrowse);
            this.groupControl1.Controls.Add(this.txtBrowse);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(3, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(939, 78);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Import File";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(827, 35);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(103, 23);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "E&xport";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(640, 35);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 3;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(721, 35);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(103, 23);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(559, 35);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtBrowse
            // 
            this.txtBrowse.Location = new System.Drawing.Point(9, 35);
            this.txtBrowse.Name = "txtBrowse";
            this.txtBrowse.Size = new System.Drawing.Size(543, 23);
            this.txtBrowse.TabIndex = 0;
            // 
            // colWrkGrp
            // 
            this.colWrkGrp.Caption = "WrkGrp";
            this.colWrkGrp.FieldName = "WrkGrp";
            this.colWrkGrp.Name = "colWrkGrp";
            this.colWrkGrp.Visible = true;
            this.colWrkGrp.VisibleIndex = 0;
            // 
            // colHalfDayFlg
            // 
            this.colHalfDayFlg.Caption = "HalfDayFlg";
            this.colHalfDayFlg.FieldName = "HalfDayFlg";
            this.colHalfDayFlg.Name = "colHalfDayFlg";
            this.colHalfDayFlg.Visible = true;
            this.colHalfDayFlg.VisibleIndex = 5;
            // 
            // colPostID
            // 
            this.colPostID.Caption = "PostID";
            this.colPostID.FieldName = "PostID";
            this.colPostID.Name = "colPostID";
            this.colPostID.Visible = true;
            this.colPostID.VisibleIndex = 6;
            // 
            // colLocation
            // 
            this.colLocation.Caption = "Location";
            this.colLocation.FieldName = "Location";
            this.colLocation.Name = "colLocation";
            this.colLocation.Visible = true;
            this.colLocation.VisibleIndex = 7;
            // 
            // frmBulkLeaveUpload
            // 
            this.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 602);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmBulkLeaveUpload";
            this.Text = "Bulk Leave Posting";
            this.Load += new System.EventHandler(this.frmBulkLeaveUpload_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_view)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_view1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtBrowse;
        private DevExpress.XtraGrid.GridControl grd_view;
        private DevExpress.XtraGrid.Views.Grid.GridView grd_view1;
        private DevExpress.XtraGrid.Columns.GridColumn colEmpUnqID;
        private System.Windows.Forms.Button btnExport;
        private DevExpress.XtraGrid.Columns.GridColumn colFromDate;
        private DevExpress.XtraGrid.Columns.GridColumn colToDate;
        private DevExpress.XtraGrid.Columns.GridColumn colLeaveType;
        private DevExpress.XtraGrid.Columns.GridColumn Remarks;
        private DevExpress.XtraGrid.Columns.GridColumn colWrkGrp;
        private DevExpress.XtraGrid.Columns.GridColumn colHalfDayFlg;
        private DevExpress.XtraGrid.Columns.GridColumn colPostID;
        private DevExpress.XtraGrid.Columns.GridColumn colLocation;
    }
}