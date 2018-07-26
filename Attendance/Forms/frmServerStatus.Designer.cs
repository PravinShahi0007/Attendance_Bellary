namespace Attendance.Forms
{
    partial class frmServerStatus
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
            this.lblServer = new System.Windows.Forms.Label();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.rtxtLoginMessage = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grd_Upload = new DevExpress.XtraGrid.GridControl();
            this.gv_Upload = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReloadJob = new System.Windows.Forms.Button();
            this.btnRestartSch = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Upload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Upload)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblServer, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.xtraTabControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(652, 434);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblServer
            // 
            this.lblServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServer.Location = new System.Drawing.Point(3, 0);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(646, 50);
            this.lblServer.TabIndex = 1;
            this.lblServer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(3, 53);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(646, 378);
            this.xtraTabControl1.TabIndex = 2;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.rtxtLoginMessage);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(640, 350);
            this.xtraTabPage1.Text = "Status";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.tableLayoutPanel2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(640, 350);
            this.xtraTabPage2.Text = "Scheduled Job";
            // 
            // rtxtLoginMessage
            // 
            this.rtxtLoginMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLoginMessage.Location = new System.Drawing.Point(0, 0);
            this.rtxtLoginMessage.Name = "rtxtLoginMessage";
            this.rtxtLoginMessage.Size = new System.Drawing.Size(640, 350);
            this.rtxtLoginMessage.TabIndex = 1;
            this.rtxtLoginMessage.Text = "";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.grd_Upload, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(640, 350);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // grd_Upload
            // 
            this.grd_Upload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_Upload.Location = new System.Drawing.Point(3, 44);
            this.grd_Upload.MainView = this.gv_Upload;
            this.grd_Upload.Name = "grd_Upload";
            this.grd_Upload.Size = new System.Drawing.Size(634, 303);
            this.grd_Upload.TabIndex = 4;
            this.grd_Upload.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_Upload});
            // 
            // gv_Upload
            // 
            this.gv_Upload.GridControl = this.grd_Upload;
            this.gv_Upload.Name = "gv_Upload";
            this.gv_Upload.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_Upload.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_Upload.OptionsBehavior.Editable = false;
            this.gv_Upload.OptionsBehavior.ReadOnly = true;
            this.gv_Upload.OptionsCustomization.AllowColumnMoving = false;
            this.gv_Upload.OptionsCustomization.AllowFilter = false;
            this.gv_Upload.OptionsCustomization.AllowGroup = false;
            this.gv_Upload.OptionsCustomization.AllowQuickHideColumns = false;
            this.gv_Upload.OptionsCustomization.AllowRowSizing = true;
            this.gv_Upload.OptionsCustomization.AllowSort = false;
            this.gv_Upload.OptionsDetail.AllowZoomDetail = false;
            this.gv_Upload.OptionsDetail.EnableMasterViewMode = false;
            this.gv_Upload.OptionsDetail.ShowDetailTabs = false;
            this.gv_Upload.OptionsDetail.SmartDetailExpand = false;
            this.gv_Upload.OptionsMenu.EnableColumnMenu = false;
            this.gv_Upload.OptionsMenu.EnableFooterMenu = false;
            this.gv_Upload.OptionsMenu.EnableGroupPanelMenu = false;
            this.gv_Upload.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.False;
            this.gv_Upload.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gv_Upload.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.gv_Upload.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gv_Upload.OptionsMenu.ShowSplitItem = false;
            this.gv_Upload.OptionsNavigation.EnterMoveNextColumn = true;
            this.gv_Upload.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gv_Upload.OptionsView.ShowDetailButtons = false;
            this.gv_Upload.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gv_Upload.OptionsView.ShowGroupPanel = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRestartSch);
            this.groupBox1.Controls.Add(this.btnReloadJob);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(1, 1);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(638, 39);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // btnReloadJob
            // 
            this.btnReloadJob.Location = new System.Drawing.Point(7, 11);
            this.btnReloadJob.Name = "btnReloadJob";
            this.btnReloadJob.Size = new System.Drawing.Size(75, 23);
            this.btnReloadJob.TabIndex = 1;
            this.btnReloadJob.Text = "Refresh";
            this.btnReloadJob.UseVisualStyleBackColor = true;
            this.btnReloadJob.Click += new System.EventHandler(this.btnReloadJob_Click);
            // 
            // btnRestartSch
            // 
            this.btnRestartSch.Location = new System.Drawing.Point(88, 11);
            this.btnRestartSch.Name = "btnRestartSch";
            this.btnRestartSch.Size = new System.Drawing.Size(114, 23);
            this.btnRestartSch.TabIndex = 2;
            this.btnRestartSch.Text = "Restart Scheduler";
            this.btnRestartSch.UseVisualStyleBackColor = true;
            this.btnRestartSch.Click += new System.EventHandler(this.btnRestartSch_Click);
            // 
            // frmServerStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 434);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmServerStatus";
            this.Text = "Server Status";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmServerStatus_FormClosing);
            this.Load += new System.EventHandler(this.frmServerStatus_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_Upload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Upload)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblServer;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private System.Windows.Forms.RichTextBox rtxtLoginMessage;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraGrid.GridControl grd_Upload;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_Upload;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRestartSch;
        private System.Windows.Forms.Button btnReloadJob;
    }
}