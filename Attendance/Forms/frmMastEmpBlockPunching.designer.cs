namespace Attendance.Forms
{
    partial class frmMastEmpBlockPunching
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
            this.Group2 = new System.Windows.Forms.GroupBox();
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grpUserRights = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.ctrlEmp1 = new Attendance.ctrlEmp();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRemarks = new DevExpress.XtraEditors.TextEdit();
            this.Group2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.grpUserRights.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemarks.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // Group2
            // 
            this.Group2.Controls.Add(this.grid);
            this.Group2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Group2.Location = new System.Drawing.Point(3, 237);
            this.Group2.Name = "Group2";
            this.Group2.Size = new System.Drawing.Size(990, 295);
            this.Group2.TabIndex = 4;
            this.Group2.TabStop = false;
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(3, 17);
            this.grid.MainView = this.gridView1;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(984, 275);
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
            // 
            // grpUserRights
            // 
            this.grpUserRights.Controls.Add(this.label10);
            this.grpUserRights.Controls.Add(this.txtRemarks);
            this.grpUserRights.Controls.Add(this.btnRefresh);
            this.grpUserRights.Controls.Add(this.btnClose);
            this.grpUserRights.Controls.Add(this.btnCancel);
            this.grpUserRights.Controls.Add(this.btnUpdate);
            this.grpUserRights.Controls.Add(this.btnAdd);
            this.grpUserRights.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpUserRights.Location = new System.Drawing.Point(3, 177);
            this.grpUserRights.Name = "grpUserRights";
            this.grpUserRights.Size = new System.Drawing.Size(990, 54);
            this.grpUserRights.TabIndex = 23;
            this.grpUserRights.TabStop = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Cornsilk;
            this.btnRefresh.Location = new System.Drawing.Point(828, 16);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(105, 32);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Refresh&Status";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Cornsilk;
            this.btnClose.Location = new System.Drawing.Point(717, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 32);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Clos&e";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Cornsilk;
            this.btnCancel.Location = new System.Drawing.Point(606, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 32);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Cornsilk;
            this.btnUpdate.Location = new System.Drawing.Point(495, 16);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(105, 32);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "&Unblock";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Cornsilk;
            this.btnAdd.Location = new System.Drawing.Point(384, 16);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(105, 32);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "&Block";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // ctrlEmp1
            // 
            this.ctrlEmp1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlEmp1.Location = new System.Drawing.Point(3, 3);
            this.ctrlEmp1.Name = "ctrlEmp1";
            this.ctrlEmp1.Size = new System.Drawing.Size(933, 168);
            this.ctrlEmp1.TabIndex = 24;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.ctrlEmp1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpUserRights, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Group2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 174F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(996, 488);
            this.tableLayoutPanel1.TabIndex = 25;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 15);
            this.label10.TabIndex = 14;
            this.label10.Text = "Reason :";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(92, 20);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Properties.Mask.EditMask = "[0-9A-Za-z.-: ]+";
            this.txtRemarks.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtRemarks.Properties.MaxLength = 45;
            this.txtRemarks.Size = new System.Drawing.Size(273, 20);
            this.txtRemarks.TabIndex = 13;
            // 
            // frmMastEmpBlockPunching
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 488);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "frmMastEmpBlockPunching";
            this.Text = "Employee Punching Block/Unblock";
            this.Load += new System.EventHandler(this.frmMastEmpBlockPunching_Load);
            this.Group2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.grpUserRights.ResumeLayout(false);
            this.grpUserRights.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRemarks.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Group2;
        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.GroupBox grpUserRights;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private ctrlEmp ctrlEmp1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.TextEdit txtRemarks;
    }
}