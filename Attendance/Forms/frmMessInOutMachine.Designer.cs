namespace Attendance.Forms
{
    partial class frmMessInOutMachine
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
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDinnerInTime = new DevExpress.XtraEditors.TimeEdit();
            this.txtDinnerOutTime = new DevExpress.XtraEditors.TimeEdit();
            this.txtLunchInTime = new DevExpress.XtraEditors.TimeEdit();
            this.txtLunchOutTime = new DevExpress.XtraEditors.TimeEdit();
            this.label12 = new System.Windows.Forms.Label();
            this.txtLocation = new DevExpress.XtraEditors.TextEdit();
            this.txtInMachine = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutMachine = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLunchMachine = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grd_avbl = new DevExpress.XtraGrid.GridControl();
            this.gv_avbl = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.LunchMachine = new DevExpress.XtraGrid.Columns.GridColumn();
            this.OutMachine = new DevExpress.XtraGrid.Columns.GridColumn();
            this.InMachine = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LunchOutTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LunchInTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DinnerOutTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DinnerInTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Location = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.grpUserRights = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDinnerInTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDinnerOutTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLunchInTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLunchOutTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInMachine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOutMachine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLunchMachine.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_avbl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_avbl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.grpUserRights.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtDinnerInTime);
            this.groupBox1.Controls.Add(this.txtDinnerOutTime);
            this.groupBox1.Controls.Add(this.txtLunchInTime);
            this.groupBox1.Controls.Add(this.txtLunchOutTime);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtLocation);
            this.groupBox1.Controls.Add(this.txtInMachine);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtOutMachine);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtLunchMachine);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(9, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(873, 112);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(578, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 15);
            this.label7.TabIndex = 23;
            this.label7.Text = "Dinner IN End :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(367, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 15);
            this.label6.TabIndex = 22;
            this.label6.Text = "Dinner Out Start :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(578, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 15);
            this.label5.TabIndex = 21;
            this.label5.Text = " Lunch IN End :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(367, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 15);
            this.label4.TabIndex = 20;
            this.label4.Text = " Lunch Out Start :";
            // 
            // txtDinnerInTime
            // 
            this.txtDinnerInTime.EditValue = new System.DateTime(2017, 12, 1, 0, 0, 0, 0);
            this.txtDinnerInTime.Location = new System.Drawing.Point(674, 74);
            this.txtDinnerInTime.Name = "txtDinnerInTime";
            this.txtDinnerInTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDinnerInTime.Properties.Appearance.Options.UseFont = true;
            this.txtDinnerInTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDinnerInTime.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtDinnerInTime.Properties.Mask.EditMask = "HH:mm";
            this.txtDinnerInTime.Properties.MaxLength = 5;
            this.txtDinnerInTime.Properties.NullValuePrompt = "Please Enter Time";
            this.txtDinnerInTime.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtDinnerInTime.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtDinnerInTime.Size = new System.Drawing.Size(76, 20);
            this.txtDinnerInTime.TabIndex = 19;
            // 
            // txtDinnerOutTime
            // 
            this.txtDinnerOutTime.EditValue = new System.DateTime(2017, 12, 1, 0, 0, 0, 0);
            this.txtDinnerOutTime.Location = new System.Drawing.Point(473, 74);
            this.txtDinnerOutTime.Name = "txtDinnerOutTime";
            this.txtDinnerOutTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDinnerOutTime.Properties.Appearance.Options.UseFont = true;
            this.txtDinnerOutTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDinnerOutTime.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtDinnerOutTime.Properties.Mask.EditMask = "HH:mm";
            this.txtDinnerOutTime.Properties.MaxLength = 5;
            this.txtDinnerOutTime.Properties.NullValuePrompt = "Please Enter Time";
            this.txtDinnerOutTime.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtDinnerOutTime.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtDinnerOutTime.Size = new System.Drawing.Size(76, 20);
            this.txtDinnerOutTime.TabIndex = 18;
            // 
            // txtLunchInTime
            // 
            this.txtLunchInTime.EditValue = new System.DateTime(2017, 12, 1, 0, 0, 0, 0);
            this.txtLunchInTime.Location = new System.Drawing.Point(674, 48);
            this.txtLunchInTime.Name = "txtLunchInTime";
            this.txtLunchInTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLunchInTime.Properties.Appearance.Options.UseFont = true;
            this.txtLunchInTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtLunchInTime.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtLunchInTime.Properties.Mask.EditMask = "HH:mm";
            this.txtLunchInTime.Properties.MaxLength = 5;
            this.txtLunchInTime.Properties.NullValuePrompt = "Please Enter Time";
            this.txtLunchInTime.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtLunchInTime.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtLunchInTime.Size = new System.Drawing.Size(76, 20);
            this.txtLunchInTime.TabIndex = 17;
            // 
            // txtLunchOutTime
            // 
            this.txtLunchOutTime.EditValue = new System.DateTime(2017, 12, 1, 0, 0, 0, 0);
            this.txtLunchOutTime.Location = new System.Drawing.Point(473, 48);
            this.txtLunchOutTime.Name = "txtLunchOutTime";
            this.txtLunchOutTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLunchOutTime.Properties.Appearance.Options.UseFont = true;
            this.txtLunchOutTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtLunchOutTime.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtLunchOutTime.Properties.Mask.EditMask = "HH:mm";
            this.txtLunchOutTime.Properties.MaxLength = 5;
            this.txtLunchOutTime.Properties.NullValuePrompt = "Please Enter Time";
            this.txtLunchOutTime.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtLunchOutTime.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtLunchOutTime.Size = new System.Drawing.Size(76, 20);
            this.txtLunchOutTime.TabIndex = 16;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(407, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 15);
            this.label12.TabIndex = 15;
            this.label12.Text = "Location :";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(473, 22);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Properties.Mask.EditMask = "[a-zA-Z0-9@./:,]+";
            this.txtLocation.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtLocation.Properties.Mask.ShowPlaceHolders = false;
            this.txtLocation.Size = new System.Drawing.Size(277, 20);
            this.txtLocation.TabIndex = 14;
            // 
            // txtInMachine
            // 
            this.txtInMachine.Location = new System.Drawing.Point(138, 74);
            this.txtInMachine.Name = "txtInMachine";
            this.txtInMachine.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtInMachine.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtInMachine.Properties.Mask.EditMask = "[0-9.]+";
            this.txtInMachine.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtInMachine.Properties.Mask.ShowPlaceHolders = false;
            this.txtInMachine.Properties.ReadOnly = true;
            this.txtInMachine.Size = new System.Drawing.Size(188, 20);
            this.txtInMachine.TabIndex = 10;
            this.txtInMachine.EditValueChanged += new System.EventHandler(this.txtInMachine_EditValueChanged);
            this.txtInMachine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInMachine_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Lunch/Dinner IN IP :";
            // 
            // txtOutMachine
            // 
            this.txtOutMachine.Location = new System.Drawing.Point(138, 48);
            this.txtOutMachine.Name = "txtOutMachine";
            this.txtOutMachine.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtOutMachine.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtOutMachine.Properties.Mask.EditMask = "[0-9.]+";
            this.txtOutMachine.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtOutMachine.Properties.Mask.ShowPlaceHolders = false;
            this.txtOutMachine.Properties.ReadOnly = true;
            this.txtOutMachine.Size = new System.Drawing.Size(188, 20);
            this.txtOutMachine.TabIndex = 8;
            this.txtOutMachine.EditValueChanged += new System.EventHandler(this.txtOutMachine_EditValueChanged);
            this.txtOutMachine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOutMachine_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Lunch/Dinner Out IP :";
            // 
            // txtLunchMachine
            // 
            this.txtLunchMachine.Location = new System.Drawing.Point(138, 22);
            this.txtLunchMachine.Name = "txtLunchMachine";
            this.txtLunchMachine.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtLunchMachine.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtLunchMachine.Properties.Mask.EditMask = "[0-9.]+";
            this.txtLunchMachine.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtLunchMachine.Properties.Mask.ShowPlaceHolders = false;
            this.txtLunchMachine.Properties.ReadOnly = true;
            this.txtLunchMachine.Size = new System.Drawing.Size(188, 20);
            this.txtLunchMachine.TabIndex = 6;
            this.txtLunchMachine.EditValueChanged += new System.EventHandler(this.txtLunchMachine_EditValueChanged);
            this.txtLunchMachine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLunchMachine_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Lunch/Dinner IP :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grd_avbl);
            this.groupBox2.Location = new System.Drawing.Point(8, 185);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(873, 351);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // grd_avbl
            // 
            this.grd_avbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_avbl.Location = new System.Drawing.Point(3, 17);
            this.grd_avbl.MainView = this.gv_avbl;
            this.grd_avbl.Name = "grd_avbl";
            this.grd_avbl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.grd_avbl.Size = new System.Drawing.Size(867, 331);
            this.grd_avbl.TabIndex = 4;
            this.grd_avbl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_avbl});
            this.grd_avbl.DoubleClick += new System.EventHandler(this.grd_avbl_DoubleClick);
            // 
            // gv_avbl
            // 
            this.gv_avbl.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.LunchMachine,
            this.OutMachine,
            this.InMachine,
            this.LunchOutTime,
            this.LunchInTime,
            this.DinnerOutTime,
            this.DinnerInTime,
            this.Location});
            this.gv_avbl.GridControl = this.grd_avbl;
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
            this.gv_avbl.DoubleClick += new System.EventHandler(this.gv_avbl_DoubleClick);
            // 
            // LunchMachine
            // 
            this.LunchMachine.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LunchMachine.AppearanceHeader.Options.UseFont = true;
            this.LunchMachine.AppearanceHeader.Options.UseTextOptions = true;
            this.LunchMachine.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LunchMachine.Caption = "LunchMachine";
            this.LunchMachine.FieldName = "LunchMachine";
            this.LunchMachine.Name = "LunchMachine";
            this.LunchMachine.OptionsColumn.AllowEdit = false;
            this.LunchMachine.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.LunchMachine.OptionsColumn.AllowMove = false;
            this.LunchMachine.OptionsColumn.AllowShowHide = false;
            this.LunchMachine.OptionsColumn.ReadOnly = true;
            this.LunchMachine.OptionsFilter.AllowAutoFilter = false;
            this.LunchMachine.OptionsFilter.AllowFilter = false;
            this.LunchMachine.Visible = true;
            this.LunchMachine.VisibleIndex = 0;
            this.LunchMachine.Width = 98;
            // 
            // OutMachine
            // 
            this.OutMachine.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutMachine.AppearanceHeader.Options.UseFont = true;
            this.OutMachine.AppearanceHeader.Options.UseTextOptions = true;
            this.OutMachine.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.OutMachine.Caption = "OutMachine";
            this.OutMachine.FieldName = "OutMachine";
            this.OutMachine.Name = "OutMachine";
            this.OutMachine.OptionsColumn.AllowEdit = false;
            this.OutMachine.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.OutMachine.OptionsColumn.AllowMove = false;
            this.OutMachine.OptionsColumn.AllowShowHide = false;
            this.OutMachine.OptionsColumn.ReadOnly = true;
            this.OutMachine.OptionsFilter.AllowAutoFilter = false;
            this.OutMachine.OptionsFilter.AllowFilter = false;
            this.OutMachine.Visible = true;
            this.OutMachine.VisibleIndex = 1;
            this.OutMachine.Width = 130;
            // 
            // InMachine
            // 
            this.InMachine.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InMachine.AppearanceHeader.Options.UseFont = true;
            this.InMachine.Caption = "InMachine";
            this.InMachine.FieldName = "InMachine";
            this.InMachine.Name = "InMachine";
            this.InMachine.OptionsColumn.AllowEdit = false;
            this.InMachine.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.InMachine.OptionsColumn.AllowMove = false;
            this.InMachine.OptionsColumn.AllowShowHide = false;
            this.InMachine.OptionsColumn.ReadOnly = true;
            this.InMachine.OptionsFilter.AllowAutoFilter = false;
            this.InMachine.OptionsFilter.AllowFilter = false;
            this.InMachine.Visible = true;
            this.InMachine.VisibleIndex = 2;
            this.InMachine.Width = 116;
            // 
            // LunchOutTime
            // 
            this.LunchOutTime.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LunchOutTime.AppearanceHeader.Options.UseFont = true;
            this.LunchOutTime.Caption = "LunchOutTime";
            this.LunchOutTime.FieldName = "LunchOutTime";
            this.LunchOutTime.Name = "LunchOutTime";
            this.LunchOutTime.OptionsColumn.AllowEdit = false;
            this.LunchOutTime.OptionsColumn.AllowMove = false;
            this.LunchOutTime.OptionsColumn.AllowShowHide = false;
            this.LunchOutTime.OptionsColumn.ReadOnly = true;
            this.LunchOutTime.OptionsFilter.AllowAutoFilter = false;
            this.LunchOutTime.OptionsFilter.AllowFilter = false;
            this.LunchOutTime.Visible = true;
            this.LunchOutTime.VisibleIndex = 3;
            this.LunchOutTime.Width = 98;
            // 
            // LunchInTime
            // 
            this.LunchInTime.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LunchInTime.AppearanceHeader.Options.UseFont = true;
            this.LunchInTime.Caption = "LunchInTime";
            this.LunchInTime.FieldName = "LunchInTime";
            this.LunchInTime.Name = "LunchInTime";
            this.LunchInTime.OptionsColumn.AllowEdit = false;
            this.LunchInTime.OptionsColumn.AllowMove = false;
            this.LunchInTime.OptionsColumn.AllowShowHide = false;
            this.LunchInTime.OptionsColumn.ReadOnly = true;
            this.LunchInTime.OptionsFilter.AllowAutoFilter = false;
            this.LunchInTime.OptionsFilter.AllowFilter = false;
            this.LunchInTime.Visible = true;
            this.LunchInTime.VisibleIndex = 4;
            this.LunchInTime.Width = 98;
            // 
            // DinnerOutTime
            // 
            this.DinnerOutTime.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DinnerOutTime.AppearanceHeader.Options.UseFont = true;
            this.DinnerOutTime.Caption = "DinnerOutTime";
            this.DinnerOutTime.FieldName = "DinnerOutTime";
            this.DinnerOutTime.Name = "DinnerOutTime";
            this.DinnerOutTime.OptionsColumn.AllowEdit = false;
            this.DinnerOutTime.OptionsColumn.AllowMove = false;
            this.DinnerOutTime.OptionsColumn.AllowShowHide = false;
            this.DinnerOutTime.OptionsColumn.ReadOnly = true;
            this.DinnerOutTime.OptionsFilter.AllowAutoFilter = false;
            this.DinnerOutTime.OptionsFilter.AllowFilter = false;
            this.DinnerOutTime.Visible = true;
            this.DinnerOutTime.VisibleIndex = 5;
            this.DinnerOutTime.Width = 98;
            // 
            // DinnerInTime
            // 
            this.DinnerInTime.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DinnerInTime.AppearanceHeader.Options.UseFont = true;
            this.DinnerInTime.Caption = "DinnerInTime";
            this.DinnerInTime.FieldName = "DinnerInTime";
            this.DinnerInTime.Name = "DinnerInTime";
            this.DinnerInTime.OptionsColumn.AllowEdit = false;
            this.DinnerInTime.OptionsColumn.AllowMove = false;
            this.DinnerInTime.OptionsColumn.AllowShowHide = false;
            this.DinnerInTime.OptionsColumn.ReadOnly = true;
            this.DinnerInTime.OptionsFilter.AllowAutoFilter = false;
            this.DinnerInTime.OptionsFilter.AllowFilter = false;
            this.DinnerInTime.Visible = true;
            this.DinnerInTime.VisibleIndex = 6;
            this.DinnerInTime.Width = 98;
            // 
            // Location
            // 
            this.Location.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Location.AppearanceHeader.Options.UseFont = true;
            this.Location.Caption = "Location";
            this.Location.FieldName = "Location";
            this.Location.Name = "Location";
            this.Location.OptionsColumn.AllowEdit = false;
            this.Location.OptionsColumn.AllowMove = false;
            this.Location.OptionsColumn.AllowShowHide = false;
            this.Location.OptionsColumn.ReadOnly = true;
            this.Location.OptionsFilter.AllowAutoFilter = false;
            this.Location.OptionsFilter.AllowFilter = false;
            this.Location.Visible = true;
            this.Location.VisibleIndex = 7;
            this.Location.Width = 113;
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
            // grpUserRights
            // 
            this.grpUserRights.Controls.Add(this.btnClose);
            this.grpUserRights.Controls.Add(this.btnCancel);
            this.grpUserRights.Controls.Add(this.btnDelete);
            this.grpUserRights.Controls.Add(this.btnUpdate);
            this.grpUserRights.Controls.Add(this.btnAdd);
            this.grpUserRights.Location = new System.Drawing.Point(12, 127);
            this.grpUserRights.Name = "grpUserRights";
            this.grpUserRights.Size = new System.Drawing.Size(868, 52);
            this.grpUserRights.TabIndex = 3;
            this.grpUserRights.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Cornsilk;
            this.btnClose.Location = new System.Drawing.Point(527, 14);
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
            this.btnCancel.Location = new System.Drawing.Point(447, 14);
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
            this.btnDelete.Location = new System.Drawing.Point(366, 14);
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
            this.btnUpdate.Location = new System.Drawing.Point(285, 14);
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
            this.btnAdd.Location = new System.Drawing.Point(204, 14);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 32);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // frmMessInOutMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 548);
            this.Controls.Add(this.grpUserRights);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMessInOutMachine";
            this.Text = "Mess InOut Machine & Time Config";
            this.Load += new System.EventHandler(this.frmMessInOutMachine_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDinnerInTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDinnerOutTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLunchInTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLunchOutTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInMachine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOutMachine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLunchMachine.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_avbl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_avbl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.grpUserRights.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.TextEdit txtInMachine;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtOutMachine;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtLunchMachine;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label12;
        private DevExpress.XtraEditors.TextEdit txtLocation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TimeEdit txtDinnerInTime;
        private DevExpress.XtraEditors.TimeEdit txtDinnerOutTime;
        private DevExpress.XtraEditors.TimeEdit txtLunchInTime;
        private DevExpress.XtraEditors.TimeEdit txtLunchOutTime;
        private System.Windows.Forms.GroupBox grpUserRights;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private DevExpress.XtraGrid.GridControl grd_avbl;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_avbl;
        private DevExpress.XtraGrid.Columns.GridColumn LunchMachine;
        private DevExpress.XtraGrid.Columns.GridColumn OutMachine;
        private DevExpress.XtraGrid.Columns.GridColumn InMachine;
        private DevExpress.XtraGrid.Columns.GridColumn LunchOutTime;
        private DevExpress.XtraGrid.Columns.GridColumn LunchInTime;
        private DevExpress.XtraGrid.Columns.GridColumn DinnerOutTime;
        private DevExpress.XtraGrid.Columns.GridColumn DinnerInTime;
        private DevExpress.XtraGrid.Columns.GridColumn Location;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
    }
}