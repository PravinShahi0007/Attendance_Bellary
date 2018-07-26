namespace Attendance.Forms
{
    partial class frmReaderConfigMess
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
            this.grpUserRights = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtMachineIP = new DevExpress.XtraEditors.TextEdit();
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
            this.Group2 = new System.Windows.Forms.GroupBox();
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1.SuspendLayout();
            this.grpUserRights.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMachineIP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompCode.Properties)).BeginInit();
            this.Group2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grpUserRights);
            this.groupBox1.Controls.Add(this.txtMachineIP);
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
            this.groupBox1.Size = new System.Drawing.Size(818, 124);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // grpUserRights
            // 
            this.grpUserRights.Controls.Add(this.btnClose);
            this.grpUserRights.Controls.Add(this.btnCancel);
            this.grpUserRights.Controls.Add(this.btnDelete);
            this.grpUserRights.Controls.Add(this.btnUpdate);
            this.grpUserRights.Controls.Add(this.btnAdd);
            this.grpUserRights.Location = new System.Drawing.Point(448, 10);
            this.grpUserRights.Name = "grpUserRights";
            this.grpUserRights.Size = new System.Drawing.Size(359, 104);
            this.grpUserRights.TabIndex = 2;
            this.grpUserRights.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Cornsilk;
            this.btnClose.Location = new System.Drawing.Point(89, 50);
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
            this.btnCancel.Location = new System.Drawing.Point(8, 51);
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
            this.btnDelete.Location = new System.Drawing.Point(89, 13);
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
            this.btnUpdate.Location = new System.Drawing.Point(170, 13);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 32);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Cornsilk;
            this.btnAdd.Location = new System.Drawing.Point(8, 13);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 32);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtMachineIP
            // 
            this.txtMachineIP.Location = new System.Drawing.Point(102, 94);
            this.txtMachineIP.Name = "txtMachineIP";
            this.txtMachineIP.Properties.Mask.EditMask = "[A-Za-z]+";
            this.txtMachineIP.Properties.Mask.ShowPlaceHolders = false;
            this.txtMachineIP.Properties.ReadOnly = true;
            this.txtMachineIP.Size = new System.Drawing.Size(340, 20);
            this.txtMachineIP.TabIndex = 15;
            this.txtMachineIP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMachineIP_KeyDown);
            this.txtMachineIP.Leave += new System.EventHandler(this.txtMachineIP_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "Machine IP";
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
            this.txtMessCode.TabIndex = 12;
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
            this.txtUnitCode.TabIndex = 4;
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
            // Group2
            // 
            this.Group2.Controls.Add(this.grid);
            this.Group2.Location = new System.Drawing.Point(12, 133);
            this.Group2.Name = "Group2";
            this.Group2.Size = new System.Drawing.Size(818, 347);
            this.Group2.TabIndex = 4;
            this.Group2.TabStop = false;
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(3, 17);
            this.grid.MainView = this.gridView1;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(812, 327);
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
            // frmReaderConfigMess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 488);
            this.Controls.Add(this.Group2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmReaderConfigMess";
            this.Text = "Mess/Canteen Reader Asignment Master";
            this.Load += new System.EventHandler(this.frmReaderConfigMess_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpUserRights.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMachineIP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompCode.Properties)).EndInit();
            this.Group2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
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
        private DevExpress.XtraEditors.TextEdit txtMachineIP;
        private System.Windows.Forms.GroupBox Group2;
        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}