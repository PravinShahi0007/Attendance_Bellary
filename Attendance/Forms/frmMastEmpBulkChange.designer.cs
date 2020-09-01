namespace Attendance.Forms
{
    partial class frmMastEmpBulkChange
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
            this.EmpUnqID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CatCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GradeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DesgCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Basic = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SPLALL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BAALL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LeftDt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BANKNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IFSCCODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ACCOUNTNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PANCARD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UANNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MobileNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EmailID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BloodGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PermenantAdd1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PermenantAdd2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PermenantAdd3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PermenantAdd4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PermenantDistrict = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PermenantCity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PermenantState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PermenantPinCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PresantAdd1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PresantAdd2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PresantAdd3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PresantAdd4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PresantDistrict = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PresantCity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PresantState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PresantPinCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Remarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtBrowse = new System.Windows.Forms.TextBox();
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
            this.EmpUnqID,
            this.CatCode,
            this.GradeCode,
            this.DesgCode,
            this.Basic,
            this.SPLALL,
            this.BAALL,
            this.LeftDt,
            this.BANKNAME,
            this.IFSCCODE,
            this.ACCOUNTNO,
            this.PANCARD,
            this.UANNO,
            this.MobileNo,
            this.EmailID,
            this.BloodGroup,
            this.PermenantAdd1,
            this.PermenantAdd2,
            this.PermenantAdd3,
            this.PermenantAdd4,
            this.PermenantDistrict,
            this.PermenantCity,
            this.PermenantState,
            this.PermenantPinCode,
            this.PresantAdd1,
            this.PresantAdd2,
            this.PresantAdd3,
            this.PresantAdd4,
            this.PresantDistrict,
            this.PresantCity,
            this.PresantState,
            this.PresantPinCode,
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
            // EmpUnqID
            // 
            this.EmpUnqID.Caption = "EmpUnqID";
            this.EmpUnqID.FieldName = "EmpUnqID";
            this.EmpUnqID.Name = "EmpUnqID";
            this.EmpUnqID.OptionsColumn.AllowEdit = false;
            this.EmpUnqID.OptionsColumn.AllowMove = false;
            this.EmpUnqID.OptionsColumn.ReadOnly = true;
            this.EmpUnqID.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.EmpUnqID.Visible = true;
            this.EmpUnqID.VisibleIndex = 0;
            this.EmpUnqID.Width = 97;
            // 
            // CatCode
            // 
            this.CatCode.Caption = "CatCode";
            this.CatCode.FieldName = "CatCode";
            this.CatCode.Name = "CatCode";
            this.CatCode.OptionsColumn.AllowEdit = false;
            this.CatCode.OptionsColumn.AllowMove = false;
            this.CatCode.OptionsColumn.ReadOnly = true;
            this.CatCode.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.CatCode.Visible = true;
            this.CatCode.VisibleIndex = 1;
            this.CatCode.Width = 136;
            // 
            // GradeCode
            // 
            this.GradeCode.Caption = "GradeCode";
            this.GradeCode.FieldName = "GradeCode";
            this.GradeCode.Name = "GradeCode";
            this.GradeCode.OptionsColumn.AllowEdit = false;
            this.GradeCode.OptionsColumn.AllowMove = false;
            this.GradeCode.OptionsColumn.ReadOnly = true;
            this.GradeCode.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.GradeCode.Visible = true;
            this.GradeCode.VisibleIndex = 2;
            this.GradeCode.Width = 136;
            // 
            // DesgCode
            // 
            this.DesgCode.Caption = "DesgCode";
            this.DesgCode.FieldName = "DesgCode";
            this.DesgCode.Name = "DesgCode";
            this.DesgCode.OptionsColumn.AllowEdit = false;
            this.DesgCode.OptionsColumn.AllowMove = false;
            this.DesgCode.OptionsColumn.ReadOnly = true;
            this.DesgCode.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.DesgCode.Visible = true;
            this.DesgCode.VisibleIndex = 3;
            this.DesgCode.Width = 136;
            // 
            // Basic
            // 
            this.Basic.Caption = "Basic";
            this.Basic.FieldName = "Basic";
            this.Basic.Name = "Basic";
            this.Basic.OptionsColumn.AllowEdit = false;
            this.Basic.OptionsColumn.AllowMove = false;
            this.Basic.OptionsColumn.ReadOnly = true;
            this.Basic.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.Basic.Visible = true;
            this.Basic.VisibleIndex = 4;
            this.Basic.Width = 80;
            // 
            // SPLALL
            // 
            this.SPLALL.Caption = "SPLALL";
            this.SPLALL.FieldName = "SPLALL";
            this.SPLALL.Name = "SPLALL";
            this.SPLALL.Visible = true;
            this.SPLALL.VisibleIndex = 5;
            // 
            // BAALL
            // 
            this.BAALL.Caption = "BAALL";
            this.BAALL.FieldName = "BAALL";
            this.BAALL.Name = "BAALL";
            this.BAALL.Visible = true;
            this.BAALL.VisibleIndex = 6;
            // 
            // LeftDt
            // 
            this.LeftDt.Caption = "LeftDt";
            this.LeftDt.FieldName = "LeftDt";
            this.LeftDt.Name = "LeftDt";
            this.LeftDt.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.LeftDt.Visible = true;
            this.LeftDt.VisibleIndex = 7;
            this.LeftDt.Width = 97;
            // 
            // BANKNAME
            // 
            this.BANKNAME.Caption = "BANKNAME";
            this.BANKNAME.FieldName = "BANKNAME";
            this.BANKNAME.Name = "BANKNAME";
            this.BANKNAME.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.BANKNAME.Visible = true;
            this.BANKNAME.VisibleIndex = 8;
            // 
            // IFSCCODE
            // 
            this.IFSCCODE.Caption = "IFSCCODE";
            this.IFSCCODE.FieldName = "IFSCCODE";
            this.IFSCCODE.Name = "IFSCCODE";
            this.IFSCCODE.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.IFSCCODE.Visible = true;
            this.IFSCCODE.VisibleIndex = 9;
            // 
            // ACCOUNTNO
            // 
            this.ACCOUNTNO.Caption = "ACCOUNTNO";
            this.ACCOUNTNO.FieldName = "ACCOUNTNO";
            this.ACCOUNTNO.Name = "ACCOUNTNO";
            this.ACCOUNTNO.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.ACCOUNTNO.Visible = true;
            this.ACCOUNTNO.VisibleIndex = 10;
            // 
            // PANCARD
            // 
            this.PANCARD.Caption = "PANCARD";
            this.PANCARD.FieldName = "PANCARD";
            this.PANCARD.Name = "PANCARD";
            this.PANCARD.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.PANCARD.Visible = true;
            this.PANCARD.VisibleIndex = 11;
            // 
            // UANNO
            // 
            this.UANNO.Caption = "UANNO";
            this.UANNO.FieldName = "UANNO";
            this.UANNO.Name = "UANNO";
            this.UANNO.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.UANNO.Visible = true;
            this.UANNO.VisibleIndex = 12;
            // 
            // MobileNo
            // 
            this.MobileNo.Caption = "MobileNo";
            this.MobileNo.FieldName = "MobileNo";
            this.MobileNo.Name = "MobileNo";
            this.MobileNo.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.MobileNo.Visible = true;
            this.MobileNo.VisibleIndex = 13;
            // 
            // EmailID
            // 
            this.EmailID.Caption = "EmailID";
            this.EmailID.FieldName = "EmailID";
            this.EmailID.Name = "EmailID";
            this.EmailID.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.EmailID.Visible = true;
            this.EmailID.VisibleIndex = 14;
            // 
            // BloodGroup
            // 
            this.BloodGroup.Caption = "BloodGroup";
            this.BloodGroup.FieldName = "BloodGroup";
            this.BloodGroup.Name = "BloodGroup";
            this.BloodGroup.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.BloodGroup.Visible = true;
            this.BloodGroup.VisibleIndex = 15;
            // 
            // PermenantAdd1
            // 
            this.PermenantAdd1.Caption = "PermenantAdd1";
            this.PermenantAdd1.FieldName = "PermenantAdd1";
            this.PermenantAdd1.Name = "PermenantAdd1";
            this.PermenantAdd1.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.PermenantAdd1.Visible = true;
            this.PermenantAdd1.VisibleIndex = 16;
            // 
            // PermenantAdd2
            // 
            this.PermenantAdd2.Caption = "PermenantAdd2";
            this.PermenantAdd2.FieldName = "PermenantAdd2";
            this.PermenantAdd2.Name = "PermenantAdd2";
            this.PermenantAdd2.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.PermenantAdd2.Visible = true;
            this.PermenantAdd2.VisibleIndex = 17;
            // 
            // PermenantAdd3
            // 
            this.PermenantAdd3.Caption = "PermenantAdd3";
            this.PermenantAdd3.FieldName = "PermenantAdd3";
            this.PermenantAdd3.Name = "PermenantAdd3";
            this.PermenantAdd3.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.PermenantAdd3.Visible = true;
            this.PermenantAdd3.VisibleIndex = 18;
            // 
            // PermenantAdd4
            // 
            this.PermenantAdd4.Caption = "PermenantAdd4";
            this.PermenantAdd4.FieldName = "PermenantAdd4";
            this.PermenantAdd4.Name = "PermenantAdd4";
            this.PermenantAdd4.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.PermenantAdd4.Visible = true;
            this.PermenantAdd4.VisibleIndex = 19;
            // 
            // PermenantDistrict
            // 
            this.PermenantDistrict.Caption = "PermenantDistrict";
            this.PermenantDistrict.FieldName = "PermenantDistrict";
            this.PermenantDistrict.Name = "PermenantDistrict";
            this.PermenantDistrict.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.PermenantDistrict.Visible = true;
            this.PermenantDistrict.VisibleIndex = 20;
            // 
            // PermenantCity
            // 
            this.PermenantCity.Caption = "PermenantCity";
            this.PermenantCity.FieldName = "PermenantCity";
            this.PermenantCity.Name = "PermenantCity";
            this.PermenantCity.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.PermenantCity.Visible = true;
            this.PermenantCity.VisibleIndex = 21;
            // 
            // PermenantState
            // 
            this.PermenantState.Caption = "PermenantState";
            this.PermenantState.FieldName = "PermenantState";
            this.PermenantState.Name = "PermenantState";
            this.PermenantState.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.PermenantState.Visible = true;
            this.PermenantState.VisibleIndex = 22;
            // 
            // PermenantPinCode
            // 
            this.PermenantPinCode.Caption = "PermenantPinCode";
            this.PermenantPinCode.FieldName = "PermenantPinCode";
            this.PermenantPinCode.Name = "PermenantPinCode";
            this.PermenantPinCode.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.PermenantPinCode.Visible = true;
            this.PermenantPinCode.VisibleIndex = 23;
            // 
            // PresantAdd1
            // 
            this.PresantAdd1.Caption = "PresantAdd1";
            this.PresantAdd1.FieldName = "PresantAdd1";
            this.PresantAdd1.Name = "PresantAdd1";
            this.PresantAdd1.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.PresantAdd1.Visible = true;
            this.PresantAdd1.VisibleIndex = 24;
            // 
            // PresantAdd2
            // 
            this.PresantAdd2.Caption = "PresantAdd2";
            this.PresantAdd2.FieldName = "PresantAdd2";
            this.PresantAdd2.Name = "PresantAdd2";
            this.PresantAdd2.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.PresantAdd2.Visible = true;
            this.PresantAdd2.VisibleIndex = 25;
            // 
            // PresantAdd3
            // 
            this.PresantAdd3.Caption = "PresantAdd3";
            this.PresantAdd3.FieldName = "PresantAdd3";
            this.PresantAdd3.Name = "PresantAdd3";
            this.PresantAdd3.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.PresantAdd3.Visible = true;
            this.PresantAdd3.VisibleIndex = 26;
            // 
            // PresantAdd4
            // 
            this.PresantAdd4.Caption = "PresantAdd4";
            this.PresantAdd4.FieldName = "PresantAdd4";
            this.PresantAdd4.Name = "PresantAdd4";
            this.PresantAdd4.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.PresantAdd4.Visible = true;
            this.PresantAdd4.VisibleIndex = 27;
            // 
            // PresantDistrict
            // 
            this.PresantDistrict.Caption = "PresantDistrict";
            this.PresantDistrict.FieldName = "PresantDistrict";
            this.PresantDistrict.Name = "PresantDistrict";
            this.PresantDistrict.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.PresantDistrict.Visible = true;
            this.PresantDistrict.VisibleIndex = 28;
            // 
            // PresantCity
            // 
            this.PresantCity.Caption = "PresantCity";
            this.PresantCity.FieldName = "PresantCity";
            this.PresantCity.Name = "PresantCity";
            this.PresantCity.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.PresantCity.Visible = true;
            this.PresantCity.VisibleIndex = 29;
            // 
            // PresantState
            // 
            this.PresantState.Caption = "PresantState";
            this.PresantState.FieldName = "PresantState";
            this.PresantState.Name = "PresantState";
            this.PresantState.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.PresantState.Visible = true;
            this.PresantState.VisibleIndex = 30;
            // 
            // PresantPinCode
            // 
            this.PresantPinCode.Caption = "PresantPinCode";
            this.PresantPinCode.FieldName = "PresantPinCode";
            this.PresantPinCode.Name = "PresantPinCode";
            this.PresantPinCode.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.PresantPinCode.Visible = true;
            this.PresantPinCode.VisibleIndex = 31;
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
            this.Remarks.VisibleIndex = 32;
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
            // frmMastEmpBulkChange
            // 
            this.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 602);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMastEmpBulkChange";
            this.Text = "Employee Bulk Change (CatCode/GradeCode/DesgCode/Basic)";
            this.Load += new System.EventHandler(this.frmMastEmpBulkChange_Load);
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
        private DevExpress.XtraGrid.Columns.GridColumn EmpUnqID;
        private System.Windows.Forms.Button btnExport;
        private DevExpress.XtraGrid.Columns.GridColumn CatCode;
        private DevExpress.XtraGrid.Columns.GridColumn GradeCode;
        private DevExpress.XtraGrid.Columns.GridColumn DesgCode;
        private DevExpress.XtraGrid.Columns.GridColumn Basic;
        private DevExpress.XtraGrid.Columns.GridColumn Remarks;
        private DevExpress.XtraGrid.Columns.GridColumn SPLALL;
        private DevExpress.XtraGrid.Columns.GridColumn BAALL;
        private DevExpress.XtraGrid.Columns.GridColumn LeftDt;
        private DevExpress.XtraGrid.Columns.GridColumn BANKNAME;
        private DevExpress.XtraGrid.Columns.GridColumn IFSCCODE;
        private DevExpress.XtraGrid.Columns.GridColumn ACCOUNTNO;
        private DevExpress.XtraGrid.Columns.GridColumn PANCARD;
        private DevExpress.XtraGrid.Columns.GridColumn UANNO;
        private DevExpress.XtraGrid.Columns.GridColumn MobileNo;
        private DevExpress.XtraGrid.Columns.GridColumn EmailID;
        private DevExpress.XtraGrid.Columns.GridColumn BloodGroup;
        private DevExpress.XtraGrid.Columns.GridColumn PermenantAdd1;
        private DevExpress.XtraGrid.Columns.GridColumn PermenantAdd2;
        private DevExpress.XtraGrid.Columns.GridColumn PermenantAdd3;
        private DevExpress.XtraGrid.Columns.GridColumn PermenantAdd4;
        private DevExpress.XtraGrid.Columns.GridColumn PermenantDistrict;
        private DevExpress.XtraGrid.Columns.GridColumn PermenantCity;
        private DevExpress.XtraGrid.Columns.GridColumn PermenantState;
        private DevExpress.XtraGrid.Columns.GridColumn PermenantPinCode;
        private DevExpress.XtraGrid.Columns.GridColumn PresantAdd1;
        private DevExpress.XtraGrid.Columns.GridColumn PresantAdd2;
        private DevExpress.XtraGrid.Columns.GridColumn PresantAdd3;
        private DevExpress.XtraGrid.Columns.GridColumn PresantAdd4;
        private DevExpress.XtraGrid.Columns.GridColumn PresantDistrict;
        private DevExpress.XtraGrid.Columns.GridColumn PresantCity;
        private DevExpress.XtraGrid.Columns.GridColumn PresantState;
        private DevExpress.XtraGrid.Columns.GridColumn PresantPinCode;
    }
}