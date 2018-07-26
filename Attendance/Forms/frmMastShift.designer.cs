namespace Attendance.Forms
{
    partial class frmMastShift
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
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtOUT_To = new DevExpress.XtraEditors.TimeEdit();
            this.txtOUT_From = new DevExpress.XtraEditors.TimeEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtIN_To = new DevExpress.XtraEditors.TimeEdit();
            this.txtIN_From = new DevExpress.XtraEditors.TimeEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtShiftEnd = new DevExpress.XtraEditors.TimeEdit();
            this.txtShiftStart = new DevExpress.XtraEditors.TimeEdit();
            this.txtBreakHrs = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtShiftHrs = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.grpUserRights = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtShiftSeq = new DevExpress.XtraEditors.TextEdit();
            this.chkNight = new DevExpress.XtraEditors.CheckEdit();
            this.txtCompName = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCompCode = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.txtShiftCode = new DevExpress.XtraEditors.TextEdit();
            this.Group2 = new System.Windows.Forms.GroupBox();
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOUT_To.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOUT_From.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIN_To.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIN_From.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBreakHrs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftHrs.Properties)).BeginInit();
            this.grpUserRights.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftSeq.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftCode.Properties)).BeginInit();
            this.Group2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtOUT_To);
            this.groupBox1.Controls.Add(this.txtOUT_From);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtIN_To);
            this.groupBox1.Controls.Add(this.txtIN_From);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtShiftEnd);
            this.groupBox1.Controls.Add(this.txtShiftStart);
            this.groupBox1.Controls.Add(this.txtBreakHrs);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtShiftHrs);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.grpUserRights);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtShiftSeq);
            this.groupBox1.Controls.Add(this.chkNight);
            this.groupBox1.Controls.Add(this.txtCompName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCompCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.txtShiftCode);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(865, 174);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(537, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 15);
            this.label10.TabIndex = 41;
            this.label10.Text = "Allow OUT up to :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(341, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(102, 15);
            this.label11.TabIndex = 40;
            this.label11.Text = "Allow OUT From :";
            // 
            // txtOUT_To
            // 
            this.txtOUT_To.EditValue = new System.DateTime(2017, 10, 30, 0, 0, 0, 0);
            this.txtOUT_To.Location = new System.Drawing.Point(643, 71);
            this.txtOUT_To.Name = "txtOUT_To";
            this.txtOUT_To.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtOUT_To.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtOUT_To.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtOUT_To.Size = new System.Drawing.Size(71, 20);
            this.txtOUT_To.TabIndex = 12;
            // 
            // txtOUT_From
            // 
            this.txtOUT_From.EditValue = new System.DateTime(2017, 10, 30, 0, 0, 0, 0);
            this.txtOUT_From.Location = new System.Drawing.Point(447, 71);
            this.txtOUT_From.Name = "txtOUT_From";
            this.txtOUT_From.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtOUT_From.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtOUT_From.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtOUT_From.Size = new System.Drawing.Size(71, 20);
            this.txtOUT_From.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(550, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 15);
            this.label8.TabIndex = 37;
            this.label8.Text = "Allow IN up to :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(354, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 15);
            this.label9.TabIndex = 36;
            this.label9.Text = "Allow IN From :";
            // 
            // txtIN_To
            // 
            this.txtIN_To.EditValue = new System.DateTime(2017, 10, 30, 0, 0, 0, 0);
            this.txtIN_To.Location = new System.Drawing.Point(643, 42);
            this.txtIN_To.Name = "txtIN_To";
            this.txtIN_To.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtIN_To.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtIN_To.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtIN_To.Size = new System.Drawing.Size(71, 20);
            this.txtIN_To.TabIndex = 10;
            // 
            // txtIN_From
            // 
            this.txtIN_From.EditValue = new System.DateTime(2017, 10, 30, 0, 0, 0, 0);
            this.txtIN_From.Location = new System.Drawing.Point(447, 42);
            this.txtIN_From.Name = "txtIN_From";
            this.txtIN_From.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtIN_From.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtIN_From.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtIN_From.Size = new System.Drawing.Size(71, 20);
            this.txtIN_From.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(572, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 15);
            this.label6.TabIndex = 33;
            this.label6.Text = "Shift End :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(378, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 15);
            this.label3.TabIndex = 32;
            this.label3.Text = "Shift Start :";
            // 
            // txtShiftEnd
            // 
            this.txtShiftEnd.EditValue = new System.DateTime(2017, 10, 30, 0, 0, 0, 0);
            this.txtShiftEnd.Location = new System.Drawing.Point(643, 16);
            this.txtShiftEnd.Name = "txtShiftEnd";
            this.txtShiftEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtShiftEnd.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtShiftEnd.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtShiftEnd.Size = new System.Drawing.Size(71, 20);
            this.txtShiftEnd.TabIndex = 8;
            // 
            // txtShiftStart
            // 
            this.txtShiftStart.EditValue = new System.DateTime(2017, 10, 30, 0, 0, 0, 0);
            this.txtShiftStart.Location = new System.Drawing.Point(447, 16);
            this.txtShiftStart.Name = "txtShiftStart";
            this.txtShiftStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtShiftStart.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtShiftStart.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtShiftStart.Size = new System.Drawing.Size(71, 20);
            this.txtShiftStart.TabIndex = 7;
            // 
            // txtBreakHrs
            // 
            this.txtBreakHrs.Location = new System.Drawing.Point(98, 97);
            this.txtBreakHrs.Name = "txtBreakHrs";
            this.txtBreakHrs.Properties.Mask.EditMask = "n0";
            this.txtBreakHrs.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtBreakHrs.Size = new System.Drawing.Size(42, 20);
            this.txtBreakHrs.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(197, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 28;
            this.label2.Text = "Shift Hrs :";
            // 
            // txtShiftHrs
            // 
            this.txtShiftHrs.Location = new System.Drawing.Point(284, 71);
            this.txtShiftHrs.Name = "txtShiftHrs";
            this.txtShiftHrs.Properties.Mask.EditMask = "f";
            this.txtShiftHrs.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtShiftHrs.Size = new System.Drawing.Size(42, 20);
            this.txtShiftHrs.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 15);
            this.label7.TabIndex = 26;
            this.label7.Text = "Break Hrs.";
            // 
            // grpUserRights
            // 
            this.grpUserRights.Controls.Add(this.btnClose);
            this.grpUserRights.Controls.Add(this.btnCancel);
            this.grpUserRights.Controls.Add(this.btnDelete);
            this.grpUserRights.Controls.Add(this.btnUpdate);
            this.grpUserRights.Controls.Add(this.btnAdd);
            this.grpUserRights.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpUserRights.Location = new System.Drawing.Point(3, 119);
            this.grpUserRights.Name = "grpUserRights";
            this.grpUserRights.Size = new System.Drawing.Size(859, 52);
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "Shift Seq. :";
            // 
            // txtShiftSeq
            // 
            this.txtShiftSeq.Location = new System.Drawing.Point(98, 71);
            this.txtShiftSeq.Name = "txtShiftSeq";
            this.txtShiftSeq.Properties.Mask.EditMask = "n0";
            this.txtShiftSeq.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtShiftSeq.Size = new System.Drawing.Size(42, 20);
            this.txtShiftSeq.TabIndex = 3;
            // 
            // chkNight
            // 
            this.chkNight.Location = new System.Drawing.Point(216, 97);
            this.chkNight.Name = "chkNight";
            this.chkNight.Properties.Caption = "Is Night Shift :";
            this.chkNight.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chkNight.Size = new System.Drawing.Size(110, 19);
            this.chkNight.TabIndex = 6;
            // 
            // txtCompName
            // 
            this.txtCompName.Location = new System.Drawing.Point(146, 16);
            this.txtCompName.Name = "txtCompName";
            this.txtCompName.Properties.ReadOnly = true;
            this.txtCompName.Size = new System.Drawing.Size(180, 20);
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
            this.label1.Location = new System.Drawing.Point(8, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Shift Code";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(146, 43);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.Mask.BeepOnError = true;
            this.txtDescription.Properties.Mask.EditMask = "[A-Za-z0-9()-.[\\] ]+";
            this.txtDescription.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtDescription.Size = new System.Drawing.Size(180, 20);
            this.txtDescription.TabIndex = 2;
            // 
            // txtShiftCode
            // 
            this.txtShiftCode.Location = new System.Drawing.Point(98, 43);
            this.txtShiftCode.Name = "txtShiftCode";
            this.txtShiftCode.Properties.Mask.EditMask = "[A-Z0-9]+";
            this.txtShiftCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtShiftCode.Properties.Mask.ShowPlaceHolders = false;
            this.txtShiftCode.Size = new System.Drawing.Size(42, 20);
            this.txtShiftCode.TabIndex = 1;
            this.txtShiftCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtShiftCode_KeyDown);
            this.txtShiftCode.Validated += new System.EventHandler(this.txtShiftCode_Validated);
            // 
            // Group2
            // 
            this.Group2.Controls.Add(this.grid);
            this.Group2.Location = new System.Drawing.Point(12, 174);
            this.Group2.Name = "Group2";
            this.Group2.Size = new System.Drawing.Size(865, 322);
            this.Group2.TabIndex = 3;
            this.Group2.TabStop = false;
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(3, 17);
            this.grid.MainView = this.gridView1;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(859, 302);
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
            // frmMastShift
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 508);
            this.Controls.Add(this.Group2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMastShift";
            this.Text = "Shift Timing and Configuration";
            this.Load += new System.EventHandler(this.frmMastShift_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOUT_To.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOUT_From.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIN_To.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIN_From.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBreakHrs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftHrs.Properties)).EndInit();
            this.grpUserRights.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftSeq.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftCode.Properties)).EndInit();
            this.Group2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtDescription;
        private DevExpress.XtraEditors.TextEdit txtShiftCode;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtCompCode;
        private DevExpress.XtraEditors.TextEdit txtCompName;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.TextEdit txtShiftSeq;
        private DevExpress.XtraEditors.CheckEdit chkNight;
        private System.Windows.Forms.GroupBox Group2;
        private System.Windows.Forms.GroupBox grpUserRights;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.TimeEdit txtOUT_To;
        private DevExpress.XtraEditors.TimeEdit txtOUT_From;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.TimeEdit txtIN_To;
        private DevExpress.XtraEditors.TimeEdit txtIN_From;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TimeEdit txtShiftEnd;
        private DevExpress.XtraEditors.TimeEdit txtShiftStart;
        private DevExpress.XtraEditors.TextEdit txtBreakHrs;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtShiftHrs;
    }
}