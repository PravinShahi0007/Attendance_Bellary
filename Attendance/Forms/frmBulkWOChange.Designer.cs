namespace Attendance.Forms
{
    partial class frmBulkWOChange
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
            this.tblp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtStatDesc = new DevExpress.XtraEditors.TextEdit();
            this.txtStatCode = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDeptDesc = new DevExpress.XtraEditors.TextEdit();
            this.txtDeptCode = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUnitDesc = new DevExpress.XtraEditors.TextEdit();
            this.txtUnitCode = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWrkGrpDesc = new DevExpress.XtraEditors.TextEdit();
            this.txtWrkGrpCode = new DevExpress.XtraEditors.TextEdit();
            this.tblp_Left = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grd_avbl = new DevExpress.XtraGrid.GridControl();
            this.gv_avbl = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.SEL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.EmpUnqID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EmpName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.tblp_Right = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grd_Sel = new DevExpress.XtraGrid.GridControl();
            this.gv_Sel = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Sel_EmpUnqID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Sel_EmpName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Sel_Remarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnSanction = new System.Windows.Forms.Button();
            this.lblDesc = new System.Windows.Forms.Label();
            this.cmbList = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtToDt = new DevExpress.XtraEditors.DateEdit();
            this.txtFromDt = new DevExpress.XtraEditors.DateEdit();
            this.tblp_Main.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeptDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeptCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWrkGrpDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWrkGrpCode.Properties)).BeginInit();
            this.tblp_Left.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_avbl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_avbl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.tblp_Right.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Sel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Sel)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDt.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDt.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDt.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tblp_Main
            // 
            this.tblp_Main.ColumnCount = 2;
            this.tblp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblp_Main.Controls.Add(this.groupBox1, 0, 0);
            this.tblp_Main.Controls.Add(this.tblp_Left, 0, 1);
            this.tblp_Main.Controls.Add(this.tblp_Right, 1, 1);
            this.tblp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblp_Main.Location = new System.Drawing.Point(0, 0);
            this.tblp_Main.Name = "tblp_Main";
            this.tblp_Main.RowCount = 2;
            this.tblp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblp_Main.Size = new System.Drawing.Size(915, 627);
            this.tblp_Main.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.tblp_Main.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.txtStatDesc);
            this.groupBox1.Controls.Add(this.txtStatCode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtDeptDesc);
            this.groupBox1.Controls.Add(this.txtDeptCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtUnitDesc);
            this.groupBox1.Controls.Add(this.txtUnitCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtWrkGrpDesc);
            this.groupBox1.Controls.Add(this.txtWrkGrpCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(909, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtStatDesc
            // 
            this.txtStatDesc.Location = new System.Drawing.Point(637, 47);
            this.txtStatDesc.Name = "txtStatDesc";
            this.txtStatDesc.Properties.Mask.EditMask = "[0-9A-Za-z ]+";
            this.txtStatDesc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtStatDesc.Properties.Mask.ShowPlaceHolders = false;
            this.txtStatDesc.Properties.MaxLength = 50;
            this.txtStatDesc.Properties.ReadOnly = true;
            this.txtStatDesc.Size = new System.Drawing.Size(238, 20);
            this.txtStatDesc.TabIndex = 28;
            // 
            // txtStatCode
            // 
            this.txtStatCode.Location = new System.Drawing.Point(535, 46);
            this.txtStatCode.Name = "txtStatCode";
            this.txtStatCode.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtStatCode.Properties.Appearance.Options.UseBackColor = true;
            this.txtStatCode.Properties.Mask.EditMask = "[0-9]+";
            this.txtStatCode.Properties.Mask.ShowPlaceHolders = false;
            this.txtStatCode.Properties.MaxLength = 3;
            this.txtStatCode.Size = new System.Drawing.Size(96, 20);
            this.txtStatCode.TabIndex = 3;
            this.txtStatCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStatCode_KeyDown);
            this.txtStatCode.Validated += new System.EventHandler(this.txtStatCode_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(462, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 14);
            this.label5.TabIndex = 26;
            this.label5.Text = "Sec.Code";
            // 
            // txtDeptDesc
            // 
            this.txtDeptDesc.Location = new System.Drawing.Point(637, 21);
            this.txtDeptDesc.Name = "txtDeptDesc";
            this.txtDeptDesc.Properties.Mask.EditMask = "[0-9A-Za-z ]+";
            this.txtDeptDesc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtDeptDesc.Properties.Mask.ShowPlaceHolders = false;
            this.txtDeptDesc.Properties.MaxLength = 50;
            this.txtDeptDesc.Properties.ReadOnly = true;
            this.txtDeptDesc.Size = new System.Drawing.Size(238, 20);
            this.txtDeptDesc.TabIndex = 25;
            // 
            // txtDeptCode
            // 
            this.txtDeptCode.Location = new System.Drawing.Point(535, 20);
            this.txtDeptCode.Name = "txtDeptCode";
            this.txtDeptCode.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDeptCode.Properties.Appearance.Options.UseBackColor = true;
            this.txtDeptCode.Properties.Mask.EditMask = "[0-9]+";
            this.txtDeptCode.Properties.Mask.ShowPlaceHolders = false;
            this.txtDeptCode.Properties.MaxLength = 3;
            this.txtDeptCode.Size = new System.Drawing.Size(96, 20);
            this.txtDeptCode.TabIndex = 2;
            this.txtDeptCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDeptCode_KeyDown);
            this.txtDeptCode.Validated += new System.EventHandler(this.txtDeptCode_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(462, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 14);
            this.label3.TabIndex = 23;
            this.label3.Text = "DeptCode";
            // 
            // txtUnitDesc
            // 
            this.txtUnitDesc.Location = new System.Drawing.Point(212, 44);
            this.txtUnitDesc.Name = "txtUnitDesc";
            this.txtUnitDesc.Properties.Mask.ShowPlaceHolders = false;
            this.txtUnitDesc.Properties.ReadOnly = true;
            this.txtUnitDesc.Size = new System.Drawing.Size(238, 20);
            this.txtUnitDesc.TabIndex = 21;
            // 
            // txtUnitCode
            // 
            this.txtUnitCode.Location = new System.Drawing.Point(110, 44);
            this.txtUnitCode.Name = "txtUnitCode";
            this.txtUnitCode.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtUnitCode.Properties.Appearance.Options.UseBackColor = true;
            this.txtUnitCode.Properties.Mask.EditMask = "[0-9]+";
            this.txtUnitCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtUnitCode.Properties.Mask.ShowPlaceHolders = false;
            this.txtUnitCode.Properties.MaxLength = 3;
            this.txtUnitCode.Size = new System.Drawing.Size(96, 20);
            this.txtUnitCode.TabIndex = 1;
            this.txtUnitCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnitCode_KeyDown);
            this.txtUnitCode.Validated += new System.EventHandler(this.txtUnitCode_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 14);
            this.label2.TabIndex = 22;
            this.label2.Text = "UnitCode";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 14);
            this.label1.TabIndex = 20;
            this.label1.Text = "WrkGrpCode";
            // 
            // txtWrkGrpDesc
            // 
            this.txtWrkGrpDesc.Location = new System.Drawing.Point(212, 20);
            this.txtWrkGrpDesc.Name = "txtWrkGrpDesc";
            this.txtWrkGrpDesc.Properties.Mask.EditMask = "\\w{3,50}";
            this.txtWrkGrpDesc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtWrkGrpDesc.Properties.Mask.ShowPlaceHolders = false;
            this.txtWrkGrpDesc.Properties.ReadOnly = true;
            this.txtWrkGrpDesc.Size = new System.Drawing.Size(238, 20);
            this.txtWrkGrpDesc.TabIndex = 18;
            this.txtWrkGrpDesc.TabStop = false;
            // 
            // txtWrkGrpCode
            // 
            this.txtWrkGrpCode.Location = new System.Drawing.Point(110, 20);
            this.txtWrkGrpCode.Name = "txtWrkGrpCode";
            this.txtWrkGrpCode.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtWrkGrpCode.Properties.Appearance.Options.UseBackColor = true;
            this.txtWrkGrpCode.Properties.Mask.EditMask = "\\w{3,10}";
            this.txtWrkGrpCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtWrkGrpCode.Properties.Mask.ShowPlaceHolders = false;
            this.txtWrkGrpCode.Size = new System.Drawing.Size(96, 20);
            this.txtWrkGrpCode.TabIndex = 0;
            this.txtWrkGrpCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWrkGrpCode_KeyDown);
            this.txtWrkGrpCode.Validated += new System.EventHandler(this.txtWrkGrpCode_Validated);
            // 
            // tblp_Left
            // 
            this.tblp_Left.ColumnCount = 1;
            this.tblp_Left.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblp_Left.Controls.Add(this.groupBox2, 0, 1);
            this.tblp_Left.Controls.Add(this.groupBox4, 0, 0);
            this.tblp_Left.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblp_Left.Location = new System.Drawing.Point(3, 83);
            this.tblp_Left.Name = "tblp_Left";
            this.tblp_Left.RowCount = 2;
            this.tblp_Left.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblp_Left.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblp_Left.Size = new System.Drawing.Size(451, 541);
            this.tblp_Left.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grd_avbl);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(445, 475);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Available Employee";
            // 
            // grd_avbl
            // 
            this.grd_avbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_avbl.Location = new System.Drawing.Point(3, 18);
            this.grd_avbl.MainView = this.gv_avbl;
            this.grd_avbl.Name = "grd_avbl";
            this.grd_avbl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.grd_avbl.Size = new System.Drawing.Size(439, 454);
            this.grd_avbl.TabIndex = 3;
            this.grd_avbl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_avbl});
            // 
            // gv_avbl
            // 
            this.gv_avbl.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.SEL,
            this.EmpUnqID,
            this.EmpName});
            this.gv_avbl.GridControl = this.grd_avbl;
            this.gv_avbl.Name = "gv_avbl";
            this.gv_avbl.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_avbl.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_avbl.OptionsBehavior.AllowIncrementalSearch = true;
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
            this.gv_avbl.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gv_avbl_CellValueChanged);
            this.gv_avbl.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gv_avbl_CellValueChanging);
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
            // EmpUnqID
            // 
            this.EmpUnqID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmpUnqID.AppearanceHeader.Options.UseFont = true;
            this.EmpUnqID.AppearanceHeader.Options.UseTextOptions = true;
            this.EmpUnqID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.EmpUnqID.Caption = "EmpUnqID";
            this.EmpUnqID.FieldName = "EmpUnqID";
            this.EmpUnqID.Name = "EmpUnqID";
            this.EmpUnqID.OptionsColumn.AllowEdit = false;
            this.EmpUnqID.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.EmpUnqID.OptionsColumn.AllowMove = false;
            this.EmpUnqID.OptionsColumn.AllowShowHide = false;
            this.EmpUnqID.OptionsColumn.ReadOnly = true;
            this.EmpUnqID.OptionsFilter.AllowAutoFilter = false;
            this.EmpUnqID.OptionsFilter.AllowFilter = false;
            this.EmpUnqID.Visible = true;
            this.EmpUnqID.VisibleIndex = 1;
            this.EmpUnqID.Width = 96;
            // 
            // EmpName
            // 
            this.EmpName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmpName.AppearanceHeader.Options.UseFont = true;
            this.EmpName.AppearanceHeader.Options.UseTextOptions = true;
            this.EmpName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.EmpName.Caption = "EmpName";
            this.EmpName.FieldName = "EmpName";
            this.EmpName.Name = "EmpName";
            this.EmpName.OptionsColumn.AllowEdit = false;
            this.EmpName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.EmpName.OptionsColumn.AllowMove = false;
            this.EmpName.OptionsColumn.AllowShowHide = false;
            this.EmpName.OptionsColumn.ReadOnly = true;
            this.EmpName.OptionsFilter.AllowAutoFilter = false;
            this.EmpName.OptionsFilter.AllowFilter = false;
            this.EmpName.Visible = true;
            this.EmpName.VisibleIndex = 2;
            this.EmpName.Width = 284;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSelectAll);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(445, 54);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(6, 15);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(130, 34);
            this.btnSelectAll.TabIndex = 0;
            this.btnSelectAll.Text = "Select ALL";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // tblp_Right
            // 
            this.tblp_Right.ColumnCount = 1;
            this.tblp_Right.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblp_Right.Controls.Add(this.groupBox3, 0, 1);
            this.tblp_Right.Controls.Add(this.groupBox5, 0, 0);
            this.tblp_Right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblp_Right.Location = new System.Drawing.Point(460, 83);
            this.tblp_Right.Name = "tblp_Right";
            this.tblp_Right.RowCount = 2;
            this.tblp_Right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblp_Right.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblp_Right.Size = new System.Drawing.Size(452, 541);
            this.tblp_Right.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grd_Sel);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 103);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(446, 435);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Selected Employee";
            // 
            // grd_Sel
            // 
            this.grd_Sel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_Sel.Location = new System.Drawing.Point(3, 18);
            this.grd_Sel.MainView = this.gv_Sel;
            this.grd_Sel.Name = "grd_Sel";
            this.grd_Sel.Size = new System.Drawing.Size(440, 414);
            this.grd_Sel.TabIndex = 0;
            this.grd_Sel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_Sel});
            // 
            // gv_Sel
            // 
            this.gv_Sel.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Sel_EmpUnqID,
            this.Sel_EmpName,
            this.Sel_Remarks});
            this.gv_Sel.GridControl = this.grd_Sel;
            this.gv_Sel.Name = "gv_Sel";
            this.gv_Sel.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_Sel.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_Sel.OptionsBehavior.AllowIncrementalSearch = true;
            this.gv_Sel.OptionsBehavior.Editable = false;
            this.gv_Sel.OptionsBehavior.ReadOnly = true;
            this.gv_Sel.OptionsCustomization.AllowColumnMoving = false;
            this.gv_Sel.OptionsCustomization.AllowFilter = false;
            this.gv_Sel.OptionsCustomization.AllowGroup = false;
            this.gv_Sel.OptionsCustomization.AllowQuickHideColumns = false;
            this.gv_Sel.OptionsCustomization.AllowRowSizing = true;
            this.gv_Sel.OptionsCustomization.AllowSort = false;
            this.gv_Sel.OptionsDetail.AllowZoomDetail = false;
            this.gv_Sel.OptionsDetail.EnableMasterViewMode = false;
            this.gv_Sel.OptionsDetail.ShowDetailTabs = false;
            this.gv_Sel.OptionsDetail.SmartDetailExpand = false;
            this.gv_Sel.OptionsMenu.EnableColumnMenu = false;
            this.gv_Sel.OptionsMenu.EnableFooterMenu = false;
            this.gv_Sel.OptionsMenu.EnableGroupPanelMenu = false;
            this.gv_Sel.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.False;
            this.gv_Sel.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gv_Sel.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.gv_Sel.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gv_Sel.OptionsMenu.ShowSplitItem = false;
            this.gv_Sel.OptionsNavigation.EnterMoveNextColumn = true;
            this.gv_Sel.OptionsView.ShowDetailButtons = false;
            this.gv_Sel.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gv_Sel.OptionsView.ShowGroupPanel = false;
            // 
            // Sel_EmpUnqID
            // 
            this.Sel_EmpUnqID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sel_EmpUnqID.AppearanceHeader.Options.UseFont = true;
            this.Sel_EmpUnqID.AppearanceHeader.Options.UseTextOptions = true;
            this.Sel_EmpUnqID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Sel_EmpUnqID.Caption = "EmpUnqID";
            this.Sel_EmpUnqID.FieldName = "EmpUnqID";
            this.Sel_EmpUnqID.Name = "Sel_EmpUnqID";
            this.Sel_EmpUnqID.OptionsColumn.AllowEdit = false;
            this.Sel_EmpUnqID.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.Sel_EmpUnqID.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.Sel_EmpUnqID.OptionsColumn.AllowMove = false;
            this.Sel_EmpUnqID.OptionsColumn.ReadOnly = true;
            this.Sel_EmpUnqID.Visible = true;
            this.Sel_EmpUnqID.VisibleIndex = 0;
            this.Sel_EmpUnqID.Width = 80;
            // 
            // Sel_EmpName
            // 
            this.Sel_EmpName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sel_EmpName.AppearanceHeader.Options.UseFont = true;
            this.Sel_EmpName.AppearanceHeader.Options.UseTextOptions = true;
            this.Sel_EmpName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Sel_EmpName.Caption = "EmpName";
            this.Sel_EmpName.FieldName = "EmpName";
            this.Sel_EmpName.Name = "Sel_EmpName";
            this.Sel_EmpName.OptionsColumn.AllowEdit = false;
            this.Sel_EmpName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.Sel_EmpName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.Sel_EmpName.OptionsColumn.AllowMove = false;
            this.Sel_EmpName.OptionsColumn.ReadOnly = true;
            this.Sel_EmpName.Visible = true;
            this.Sel_EmpName.VisibleIndex = 1;
            this.Sel_EmpName.Width = 220;
            // 
            // Sel_Remarks
            // 
            this.Sel_Remarks.Caption = "Remarks";
            this.Sel_Remarks.FieldName = "Remarks";
            this.Sel_Remarks.Name = "Sel_Remarks";
            this.Sel_Remarks.Visible = true;
            this.Sel_Remarks.VisibleIndex = 2;
            this.Sel_Remarks.Width = 122;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnExport);
            this.groupBox5.Controls.Add(this.btnClearAll);
            this.groupBox5.Controls.Add(this.btnSanction);
            this.groupBox5.Controls.Add(this.lblDesc);
            this.groupBox5.Controls.Add(this.cmbList);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.txtToDt);
            this.groupBox5.Controls.Add(this.txtFromDt);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(446, 94);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(380, 53);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(60, 34);
            this.btnExport.TabIndex = 25;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(315, 53);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(60, 34);
            this.btnClearAll.TabIndex = 4;
            this.btnClearAll.Text = "Clear";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnSanction
            // 
            this.btnSanction.Location = new System.Drawing.Point(221, 53);
            this.btnSanction.Name = "btnSanction";
            this.btnSanction.Size = new System.Drawing.Size(88, 34);
            this.btnSanction.TabIndex = 3;
            this.btnSanction.Text = "Sanction";
            this.btnSanction.UseVisualStyleBackColor = true;
            this.btnSanction.Click += new System.EventHandler(this.btnSanction_Click);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(27, 63);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(65, 14);
            this.lblDesc.TabIndex = 24;
            this.lblDesc.Text = "WO Days :";
            // 
            // cmbList
            // 
            this.cmbList.Location = new System.Drawing.Point(105, 61);
            this.cmbList.Name = "cmbList";
            this.cmbList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbList.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbList.Size = new System.Drawing.Size(100, 20);
            this.cmbList.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(249, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 14);
            this.label6.TabIndex = 22;
            this.label6.Text = "To Date :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 14);
            this.label4.TabIndex = 21;
            this.label4.Text = "From Date :";
            // 
            // txtToDt
            // 
            this.txtToDt.EditValue = null;
            this.txtToDt.Location = new System.Drawing.Point(315, 21);
            this.txtToDt.Name = "txtToDt";
            this.txtToDt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtToDt.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtToDt.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtToDt.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtToDt.Size = new System.Drawing.Size(100, 20);
            this.txtToDt.TabIndex = 1;
            // 
            // txtFromDt
            // 
            this.txtFromDt.EditValue = null;
            this.txtFromDt.Location = new System.Drawing.Point(105, 21);
            this.txtFromDt.Name = "txtFromDt";
            this.txtFromDt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFromDt.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFromDt.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtFromDt.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtFromDt.Size = new System.Drawing.Size(100, 20);
            this.txtFromDt.TabIndex = 0;
            this.txtFromDt.EditValueChanged += new System.EventHandler(this.txtFromDt_EditValueChanged);
            // 
            // frmBulkWOChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 627);
            this.Controls.Add(this.tblp_Main);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "frmBulkWOChange";
            this.Text = "Bulk Week Off Change";
            this.Load += new System.EventHandler(this.frmBulkWOChange_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBulkWOChange_KeyDown);
            this.tblp_Main.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeptDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeptCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWrkGrpDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWrkGrpCode.Properties)).EndInit();
            this.tblp_Left.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_avbl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_avbl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.tblp_Right.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_Sel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Sel)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDt.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDt.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDt.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblp_Main;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tblp_Left;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tblp_Right;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private DevExpress.XtraGrid.GridControl grd_avbl;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_avbl;
        private DevExpress.XtraGrid.GridControl grd_Sel;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_Sel;
        private DevExpress.XtraEditors.TextEdit txtStatDesc;
        private DevExpress.XtraEditors.TextEdit txtStatCode;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.TextEdit txtDeptDesc;
        private DevExpress.XtraEditors.TextEdit txtDeptCode;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtUnitDesc;
        private DevExpress.XtraEditors.TextEdit txtUnitCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtWrkGrpDesc;
        private DevExpress.XtraEditors.TextEdit txtWrkGrpCode;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSanction;
        private System.Windows.Forms.Label lblDesc;
        private DevExpress.XtraEditors.ComboBoxEdit cmbList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.DateEdit txtToDt;
        private DevExpress.XtraEditors.DateEdit txtFromDt;
        private DevExpress.XtraGrid.Columns.GridColumn SEL;
        private DevExpress.XtraGrid.Columns.GridColumn EmpUnqID;
        private DevExpress.XtraGrid.Columns.GridColumn EmpName;
        private DevExpress.XtraGrid.Columns.GridColumn Sel_EmpUnqID;
        private DevExpress.XtraGrid.Columns.GridColumn Sel_EmpName;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn Sel_Remarks;
        private System.Windows.Forms.Button btnExport;
    }
}