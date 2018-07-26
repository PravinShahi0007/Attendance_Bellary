using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Attendance.Classes;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Attendance.Forms
{
    public partial class frmProcess : Form
    {
        public string ProcessName = string.Empty;
        public static DataTable ProcessList = new DataTable();
        public string GRights = "XXXV";
        static string PCName = string.Empty;
        static Timer serverstatus = new Timer(), appstatus = new Timer();
        public static bool ISProcessStarted = false;

        public frmProcess()
        {
            InitializeComponent();
            ProcessList = new DataTable();
            ProcessList.Columns.Add("EmpUnqID",typeof(string));
            ProcessList.Columns.Add("EmpName", typeof(string));
            ProcessList.Columns.Add("FromDate",typeof(DateTime));
            ProcessList.Columns.Add("ToDate",typeof(DateTime));
            ProcessList.Columns.Add("Status", typeof(string));
            ProcessList.Columns.Add("IsDone", typeof(Boolean));
            
            gridApp.DataSource = ProcessList;
            
            appstatus = new Timer();
            serverstatus = new Timer();

            serverstatus.Interval = 10000;
            appstatus.Interval =  10000;
              
            serverstatus.Tick += serverstatus_Tick;
            appstatus.Tick += appstatus_Tick;

            serverstatus.Enabled = true;
            appstatus.Enabled = true;

            PCName = System.Environment.MachineName;
            this.ctrlEmp1.EmpUnqIDValidated += new EventHandler(this.ctrlEmpValidateEvent_Handler);
        }

        private void ctrlEmpValidateEvent_Handler(object sender, EventArgs e)
        {
           
           txtAppFromDt.Focus();
           
        }


        void serverstatus_Tick(object sender, EventArgs e)
        {
            DataSet ds = Utils.Helper.GetData("Select EmpUnqID,FromDt as FromDate,ToDt as ToDate From AttdWorker Where DoneFlg = 0 Order By EmpUnqID", Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                gridServer.DataSource = ds;
                gridServer.DataMember = ds.Tables[0].TableName;
                grpServer.Text = string.Format("Currently Running Process : {0} At Server Side", ds.Tables[0].Rows.Count);
            }
            else
            {
                gridServer.DataSource = null;
                grpServer.Text = string.Format("Currently Running Process : 0 At Server Side", ds.Tables[0].Rows.Count);
            }
            
        }

        void appstatus_Tick(object sender, EventArgs e)
        {
            if (!ISProcessStarted)
                return;
            
            try
            {
                DataRow[] rows = ProcessList.Select("IsDone = 0");

                DataTable dt = rows.CopyToDataTable();

                if (dt.Rows.Count > 0)
                {
                    gridSelf.DataSource = dt;
                    gridSelf.Refresh();
                    grpApp.Text = string.Format("Currently Running Process : {0} At Application Side", dt.Rows.Count);
                }
                else
                {
                    gridSelf.DataSource = null;
                    grpApp.Text = string.Format("Currently Running Process : {0} At Application Side", 0);

                }
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233079)
                {
                    gridSelf.DataSource = null;
                    grpApp.Text = string.Format("Currently Running Process : {0} At Application Side", 0);
                }
            }
            
        }

        private void txtWrkGrpCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCompCode.Text.Trim() == "")
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select WrkGrp,WrkGrpDesc From MastWorkGrp Where CompCode ='" + txtCompCode.Text.Trim() + "'";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "WrkGrp", "WrkGrp", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }

                if (obj.Count == 0)
                {

                    return;
                }
                else if (obj.ElementAt(0).ToString() == "0")
                {
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "")
                {
                    return;
                }
                else
                {

                    txtWrkGrpCode.Text = obj.ElementAt(0).ToString();
                    txtWrkGrpDesc.Text = obj.ElementAt(1).ToString();


                }
            }
        }

        private void txtWrkGrpCode_Validated(object sender, EventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtCompName.Text.Trim() == "")
            {

                return;
            }

            DataSet ds = new DataSet();
            string sql = "select * From MastWorkGrp where CompCode ='" + txtCompCode.Text.Trim() + "' and WrkGrp='" + txtWrkGrpCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCompCode.Text = dr["CompCode"].ToString();
                    txtWrkGrpCode.Text = dr["WrkGrp"].ToString();
                    txtWrkGrpDesc.Text = dr["WrkGrpDesc"].ToString();
                    txtCompCode_Validated(sender, e);

                }
            }
            else
            {
                txtWrkGrpCode.Text = "";
                txtWrkGrpDesc.Text = "";
            }


        }

        private void txtCompCode_Validated(object sender, EventArgs e)
        {
            //if (txtCompCode.Text.Trim() == "")
            //{
            //    return;
            //}

            DataSet ds = new DataSet();
            string sql = "select * from MastComp where CompCode ='" + txtCompCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCompCode.Text = dr["CompCode"].ToString();
                    txtCompName.Text = dr["CompName"].ToString();

                }
            }
            else
            {
                txtCompCode.Text = "";
                txtCompName.Text = "";
            }

        }

        private void txtCompCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select CompCode,CompName From MastComp Where 1 = 1";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "CompCode", "CompCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }

                if (obj.Count == 0)
                {

                    return;
                }
                else if (obj.ElementAt(0).ToString() == "0")
                {
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "")
                {
                    return;
                }
                else
                {

                    txtCompCode.Text = obj.ElementAt(0).ToString();
                    txtCompName.Text = obj.ElementAt(1).ToString();

                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string err = string.Empty;

            if(string.IsNullOrEmpty(ctrlEmp1.txtCompCode.Text.Trim()) || string.IsNullOrEmpty(ctrlEmp1.txtCompDesc.Text.Trim()))
                err += "Please Select Company Code..." + Environment.NewLine;

            if (string.IsNullOrEmpty(ctrlEmp1.txtEmpUnqID.Text.Trim()))
                err += "Please Select EmpUnqID..." + Environment.NewLine;

            clsEmp t = new clsEmp();
            t.CompCode = ctrlEmp1.txtCompCode.Text.Trim();
            t.EmpUnqID = ctrlEmp1.txtEmpUnqID.Text.Trim();
            t.GetEmpDetails(t.CompCode, t.EmpUnqID);
            if (!t.Active)
            {
                err += "Please select active EmpUnqID..." + Environment.NewLine;
            }

            if(txtAppFromDt.EditValue == null)
                err += "Please select from date..." + Environment.NewLine;

            if (txtAppToDate.EditValue == null)
                err += "Please select to date..." + Environment.NewLine;

            if(!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtAppToDate.DateTime < txtAppFromDt.DateTime)
            {
                MessageBox.Show("Invalid Date Range...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //' added on 18-09-2014
            TimeSpan ts = (txtAppFromDt.DateTime - txtAppToDate.DateTime);
            if(Math.Abs(ts.TotalDays) > 31)
            {
                MessageBox.Show("Please Contact To System Administrator for More than 31 days Process", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string tyearmt = txtAppFromDt.DateTime.ToString("yyyyMM");
            string pyearmt = Convert.ToDateTime(Utils.Helper.GetDescription("SELECT DateAdd(month, -1, Convert(date, GetDate()));",Utils.Helper.constr)).ToString("yyyyMM");

            if(Convert.ToInt32(tyearmt) < Convert.ToInt32(pyearmt)){
                MessageBox.Show("Please Contact To System Administrator for Previous Month Process", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //check for duplicate employee...
            foreach (DataRow dr in ProcessList.Rows)
            {
                if (dr["EmpUnqID"].ToString() == t.EmpUnqID)
                {
                    MessageBox.Show("Employee already exist...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
             
            DataRow tdr = ProcessList.NewRow();
            tdr["EmpUnqID"] = t.EmpUnqID;
            tdr["EmpName"] = t.EmpName;
            tdr["FromDate"] = txtAppFromDt.DateTime;
            tdr["ToDate"] = txtAppToDate.DateTime;
            tdr["Status"] = "Pending";
            tdr["IsDone"] = false;
            ProcessList.Rows.Add(tdr);
            ctrlEmp1.ResetCtrl();
            gridApp.Refresh();
            CalcProcessed();


        }

        public void CalcProcessed()
        {
            //lblStatus.Text = "{0}/{1}";
            int total = ProcessList.Rows.Count;
            
            DataRow[] tdr = ProcessList.Select("IsDone = 1");

            int proc = tdr.GetLength(0);

            lblStatus.Text = string.Format("{0}/{1}", total, proc);

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openKeywordsFileDialog = new OpenFileDialog();
            openKeywordsFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openKeywordsFileDialog.Multiselect = false;
            openKeywordsFileDialog.ValidateNames = true;
            //openKeywordsFileDialog.CheckFileExists = true;
            openKeywordsFileDialog.DereferenceLinks = false;        //Will return .lnk in shortcuts.
            openKeywordsFileDialog.Filter = "Files|*.xls;*.xlsx;*.xlsb";
            openKeywordsFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(OpenKeywordsFileDialog_FileOk);
            var dialogResult = openKeywordsFileDialog.ShowDialog();

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                //first check if already exits if found return..
                string filenm = openKeywordsFileDialog.FileName.ToString();
                if (string.IsNullOrEmpty(filenm))
                    return;
                try
                {
                    txtBrowse.Text = openKeywordsFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    txtBrowse.Text = "";
                }
            }
            else
            {
                txtBrowse.Text = "";
            }
        }

        void OpenKeywordsFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OpenFileDialog fileDialog = sender as OpenFileDialog;
            string selectedFile = fileDialog.FileName;
            if (string.IsNullOrEmpty(selectedFile) || selectedFile.Contains(".lnk"))
            {
                MessageBox.Show("Please select a valid File");
                e.Cancel = true;
            }
            return;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            //////////////////////////////

            if (string.IsNullOrEmpty(txtBrowse.Text.Trim().ToString()))
            {
                MessageBox.Show("Please Select Excel File First...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnBrowse.Enabled = false;


            DataTable TempDt = new DataTable();
            TempDt.Columns.Add("EmpUnqID", typeof(string));
            TempDt.Columns.Add("EmpName", typeof(string));
            TempDt.Columns.Add("FromDate", typeof(DateTime));
            TempDt.Columns.Add("ToDate", typeof(DateTime));
            TempDt.Columns.Add("Status", typeof(string));
            TempDt.Columns.Add("IsDone", typeof(Boolean));

            Cursor.Current = Cursors.WaitCursor;
            ProcessList.Clear();
            gridApp.Refresh();

            string filePath = txtBrowse.Text.ToString();

            string sexcelconnectionstring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"";
            //string sexcelconnectionstring = @"provider=microsoft.jet.oledb.4.0;data source=" + filePath + ";extended properties=" + "\"excel 8.0;hdr=yes;IMEX=1;\"";

            OleDbConnection oledbconn = new OleDbConnection(sexcelconnectionstring);
           
            List<SheetName> sheets = ExcelHelper.GetSheetNames(oledbconn);
            oledbconn.Open();
            string str = oledbconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
            string sheetname = "[" + sheets[0].sheetName.Replace("'", "") + "]";

            try
            {
                string myexceldataquery = "select EmpUnqID,EmpName,FromDate,ToDate,'Pending' as Status, 0 as IsDone from " + sheetname;
                OleDbDataAdapter oledbda = new OleDbDataAdapter(myexceldataquery, oledbconn);
                ProcessList.Clear();
                gridApp.Refresh();
                TempDt.Clear();
                oledbda.Fill(TempDt);
                oledbconn.Close();

                TempDt.AcceptChanges();
                foreach (DataRow row in TempDt.Rows)
                {
                    if (string.IsNullOrEmpty(row["EmpUnqID"].ToString().Trim()))
                        row.Delete();
                }
                TempDt.AcceptChanges();


                int errcnt = 0;
                foreach (DataRow dr in TempDt.Rows)
                {
                    //validate each row for 
                    if(string.IsNullOrEmpty(ValidateBulk(dr)))
                    {
                        try
                        {
                            
                            ProcessList.ImportRow(dr);         

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString() , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
                    }
                    else
                    {
                        errcnt += 1;
                    }
                }
                gridApp.Refresh();
                CalcProcessed();

                if (errcnt > 0)
                {
                    MessageBox.Show("there are " + errcnt.ToString() + " errors please check with basic rules for data processing...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            catch (Exception ex)
            {
                oledbconn.Close();
                MessageBox.Show("Please Check upload template..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }

            Cursor.Current = Cursors.Default;

            btnBrowse.Enabled = true;
            
            
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {
            
        }

        private string ValidateBulk(DataRow dr)
        {
            string err = string.Empty;

            clsEmp t = new clsEmp();
            t.CompCode = "01";
            t.EmpUnqID = dr["EmpUnqID"].ToString().Trim();
            t.GetEmpDetails(t.CompCode, t.EmpUnqID);
            if (!t.Active)
            {
                err += "Please select active EmpUnqID..." + Environment.NewLine;
            }

            DateTime tToDate, tFromDt;

            try
            {
                tFromDt = Convert.ToDateTime(dr["FromDate"].ToString());
            }
            catch(Exception ex)
            {
                err += "invalid from date" + Environment.NewLine;
                return err;
            }

            try
            {
                tToDate = Convert.ToDateTime(dr["ToDate"].ToString());
            }
            catch(Exception ex)
            {
                err += "invalid to date" + Environment.NewLine;
                return err;
            }

            

            if (tToDate < tFromDt)
            {
                err += "Invalid Date Range..." + Environment.NewLine;
            }

            //' added on 18-09-2014
            TimeSpan ts = (tFromDt - tToDate);
            if (Math.Abs(ts.TotalDays) > 31)
            {
                err += "Please Contact To System Administrator for More than 31 days Process" + Environment.NewLine;
            }

            string tyearmt = tFromDt.ToString("yyyyMM");
            string pyearmt = Convert.ToDateTime(Utils.Helper.GetDescription("SELECT DateAdd(month, -1, Convert(date, GetDate()));", Utils.Helper.constr)).ToString("yyyyMM");

            if (Convert.ToInt32(tyearmt) < Convert.ToInt32(pyearmt))
            {
                err += "Please Contact To System Administrator for Previous Month Process" + Environment.NewLine;                
            }
            return err;
        }

       
        private void frmProcess_Shown(object sender, EventArgs e)
        {
            if (ProcessName == "ATTD")
            {
                GRights = Attendance.Classes.Globals.GetFormRights("frmAttdProcess");
                this.Text = "Attendance Data Process";
                grpInfo.Visible = true;
                grpMethod.Visible = true;
                serverstatus_Tick(sender, e);
                appstatus_Tick(sender, e);
                serverstatus.Start();
                appstatus.Start();
            }
            else if (ProcessName == "MESS")
            {
                GRights = Attendance.Classes.Globals.GetFormRights("frmMessProcess");
                this.Text = "Mess/Cafeteria Data Process";
                grpInfo.Visible = false;
                grpMethod.Visible = false;
                serverstatus.Stop();
            }
            else if (ProcessName == "LUNCHINOUT")
            {
                GRights = Attendance.Classes.Globals.GetFormRights("frmLunchInOutProcess");
                this.Text = "Lunch In/Out Data Process";
                grpInfo.Visible = false;
                grpMethod.Visible = false;
                serverstatus.Stop();
            }

            if (Utils.User.GUserID == "SERVER")
            {
                GRights = "AUDV";
            }

            if (GRights.Contains("XXXV"))
            {
                btnProcess.Enabled = false;
                btnProcessWrkGrp.Enabled = false;
            }
            else if (GRights.Contains("AU"))
            {
                btnProcess.Enabled = true;
                btnProcessWrkGrp.Enabled = true;
            }
            else
            {
                btnProcess.Enabled = false;
                btnProcessWrkGrp.Enabled = false;
            }

            optSelf.Checked = true;
            optServer.Checked = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gv_app.SelectedRowsCount > 0)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;

                foreach (int i in gv_app.GetSelectedRows())
                {
                    string emp = gv_app.GetRowCellValue(i, "EmpUnqID").ToString();

                    var rows = ProcessList.Select("EmpUnqID = '" + emp + "'");
                    foreach (var row in rows)
                    {
                        row.BeginEdit();
                        row.Delete();
                        row.EndEdit();
                        row.AcceptChanges();
                    }
                   
                    ProcessList.AcceptChanges();
                    RefreshAppGrid(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Please select row first...", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure to Clear all rows ?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                return;
           
            ProcessList.Clear();
            ProcessList.AcceptChanges();
            RefreshAppGrid(sender,e);
        }

        void RefreshAppGrid(object sender, EventArgs e)
        {
            appstatus_Tick(sender, e);
            gridSelf.Refresh();
            gridApp.Refresh();
            CalcProcessed();
        }

        private void frmProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISProcessStarted = false;
            ProcessList.Clear();
            ProcessList.AcceptChanges();
            RefreshAppGrid(sender, e);

            appstatus.Enabled = false;
            serverstatus.Enabled = false;
            appstatus.Stop();
            serverstatus.Stop();

        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (ProcessList.Rows.Count <= 0)
            {
                MessageBox.Show("Please Select Employee first..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (DataRow dr in ProcessList.Rows)
            {
                dr.BeginEdit();
                dr["IsDone"] = 0;
                dr["Status"] = "Pending";
                dr.EndEdit();
                dr.AcceptChanges();
                RefreshAppGrid(sender,e);
            }
            
            ProcessDATA(sender, e, "APP", "EMP");
        }

        private void btnProcessWrkGrp_Click(object sender, EventArgs e)
        {

            if (ISProcessStarted)
            {
                MessageBox.Show("There are still some process is under process please wait..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            if (txtCompCode.Text.Trim() == "" || txtWrkGrpCode.Text.Trim() == "")
            {
                MessageBox.Show("Please Select CompCode/WrkGrpCode...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string err = string.Empty;

            if (txtWrkFromDt.EditValue == null)
                err += "Please select from date..." + Environment.NewLine;

            if (txtWrkToDate.EditValue == null)
                err += "Please select to date..." + Environment.NewLine;

            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtWrkToDate.DateTime < txtWrkFromDt.DateTime)
            {
                MessageBox.Show("Invalid Date Range...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //' added on 18-09-2014
            TimeSpan ts = (txtWrkFromDt.DateTime - txtWrkToDate.DateTime);
            if (Math.Abs(ts.TotalDays) > 31)
            {
                MessageBox.Show("Please Contact To System Administrator for More than 31 days Process", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string tyearmt = txtWrkFromDt.DateTime.ToString("yyyyMM");
            string pyearmt = Convert.ToDateTime(Utils.Helper.GetDescription("SELECT DateAdd(month, -1, Convert(date, GetDate()));", Utils.Helper.constr)).ToString("yyyyMM");

            if (Convert.ToInt32(tyearmt) < Convert.ToInt32(pyearmt))
            {
                MessageBox.Show("Please Contact To System Administrator for Previous Month Process", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string msg = "Are You Sure to Process Data of " + Environment.NewLine + " WrkGrp : " + txtWrkGrpCode.Text + Environment.NewLine +
                " From Date : " + txtWrkFromDt.DateTime.ToString("dd/MM/yyyy") + Environment.NewLine +
               " To Date : " + txtWrkToDate.DateTime.ToString("dd/MM/yyyy") + Environment.NewLine +
               " Current Data will be deleted between From Date And To Date ";

            DialogResult ans = MessageBox.Show(msg, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans != System.Windows.Forms.DialogResult.Yes)
            {
                MessageBox.Show("Process Canceled", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            

            ProcessList.Clear();
            ProcessList.AcceptChanges();
            appstatus_Tick(sender, e);

            if (ProcessName == "ATTD" && optServer.Checked == true)
            {
                
                DataSet ds = Utils.Helper.GetData("Select EmpUnqID,FromDt as FromDate,ToDt as ToDate From AttdWorker Where DoneFlg = 0 Order By EmpUnqID", Utils.Helper.constr);
                bool hasRows = ds.Tables.Cast<DataTable>()
                               .Any(table => table.Rows.Count != 0);

                if (hasRows)
                {
                    MessageBox.Show("There are still some process is under process at server side, please wait..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }



                using(SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                {
                    try
                    {
                        cn.Open();
                        string sql1 = string.Empty;

                        sql1 = "Insert into AttdWorker  ( EmpUnqId,FromDt,ToDt,WorkerId,DoneFlg,PushFlg,addid ) " +
                        " select EmpUnqId ,'" + txtWrkFromDt.DateTime.ToString("yyyy-MM-dd") + "','" + txtWrkToDate.DateTime.ToString("yyyy-MM-dd") + "'," +
                        " '" + Utils.User.GUserID + "',0,0,'" + Utils.User.GUserID + "' " +
                        " From MastEmp Where WrkGrp='" + txtWrkGrpCode.Text.Trim().ToString().ToUpper() + "' " +
                        " and CompCode ='" + txtCompCode.Text.Trim().ToString() + "' and active = 1 Order By EmpUnqID";

                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = cn;
                            cmd.CommandText = sql1;
                            cmd.CommandTimeout = 0;
                            cmd.ExecuteNonQuery();
                            
                        }
                        cn.Close();

                    }catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }//using cn
            }
            else if (ProcessName == "ATTD")
            {
                string sql = "Select EmpUnqId,'" + txtWrkFromDt.DateTime.ToString("yyyy-MM-dd") + "' as FromDate, " +
                    " '" + txtWrkToDate.DateTime.ToString("yyyy-MM-dd") + "' as ToDate, Convert(bit,0) as IsDone " +
                    " from MastEmp where WrkGrp='" + txtWrkGrpCode.Text.Trim().ToString().ToUpper() + "' " +
                    " and CompCode ='" + txtCompCode.Text.Trim().ToString() + "' and active = 1 Order By EmpUnqID";

                DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);

                bool hasRows = ds.Tables.Cast<DataTable>()
                          .Any(table => table.Rows.Count != 0);

                if (hasRows)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        
                        ProcessList.ImportRow(dr);
                    }
                    RefreshAppGrid(sender, e);
                    ProcessDATA(sender, e, "APP", "WRKGRP");
                }
            }
            else if (ProcessName == "MESS")
            {
                DateTime startdt = txtWrkFromDt.DateTime.AddHours(0).AddMinutes(1);
                DateTime enddt = txtWrkToDate.DateTime.AddHours(23).AddMinutes(59);
                string sWrkGrp = txtWrkGrpCode.Text.Trim().ToString();

                string sql = "Select Distinct l.EmpUnqID,'" + startdt.ToString("yyyy-MM-dd") + "' as FromDate," +
                    " '" + enddt.ToString("yyyy-MM-dd") + "' as ToDate, Convert(bit,0) as IsDone " +
                    " From ATTDLOG l Left join MastEmp m on l.EmpUnqID = m.EmpunqID where LunchFLG = 1 " +
                    " and l.ioflg = 'B' and l.PunchDate between '" + startdt.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                    " and '" + enddt.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                    " and m.WrkGrp = '" + sWrkGrp + "'  and m.compcode = '01' and m.MessCode is not null  " +
                    " And l.MachineIP in (Select MachineIP From MastMessReader ) Order By EmpUnqID ";

                DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);

                bool hasRows = ds.Tables.Cast<DataTable>()
                          .Any(table => table.Rows.Count != 0);

                if (hasRows)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        
                        ProcessList.ImportRow(dr);
                    }
                    RefreshAppGrid(sender, e);
                    ProcessDATA(sender, e, "APP", "WRKGRP");
                }
                
            }
            else if (ProcessName == "LUNCHINOUT")
            {

                DateTime startdt = txtWrkFromDt.DateTime.AddHours(11).AddMinutes(0);
                DateTime enddt = txtWrkToDate.DateTime.AddHours(23).AddMinutes(59);
                string sWrkGrp = txtWrkGrpCode.Text.Trim().ToString();

                string sql = " select EmpUnqId,'" + startdt.ToString("yyyy-MM-dd") + "' as FromDate, '" + enddt.ToString("yyyy-MM-dd") + "' as ToDate,Convert(bit,0) as IsDone " +
                    " from MastEmp where Compcode = '01' and WrkGrp = '" + sWrkGrp + "' and Active = 1 " +
                    " and Empunqid in ( " +
                    " select distinct EmpUnqId from attdlog where lunchflg = 1 and ioflg = 'b' and " +
                    " punchdate Between '" + startdt.ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + enddt.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                    " and machineip in (Select Lunchmachine from LunchMachine ) " +
                    " and EmpUnqID in (Select EmpUnqID from MastEmp where WrkGrp = '" + sWrkGrp + "' and Active = 1 and CompCode = '01')) " +
                    " Union " +
                    " select distinct EmpUnqiD,'" + startdt.ToString("yyyy-MM-dd") + "' as FromDate, '" + enddt.ToString("yyyy-MM-dd") + "' as ToDate,Convert(bit,0) as IsDone " +
                    " from AttdLunchGate where lunchflg = 0 and ioflg in ('I','O') " +
                    " and PunchDate Between '" + startdt.ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + enddt.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                    " and EmpUnqID in (Select EmpUnqID from MastEmp where WrkGrp = '" + sWrkGrp + "' and Active = 1 and CompCode = '01') ";

                DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);

                bool hasRows = ds.Tables.Cast<DataTable>()
                          .Any(table => table.Rows.Count != 0);

                if (hasRows)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        
                        ProcessList.ImportRow(dr);
                    }

                    ProcessDATA(sender, e, "APP", "WRKGRP");
                }
            }
            else
            {
                return;
            }

        }


        /// <summary>
        /// Process Names (ATTD,MESS,LUNCHINOUT) GLOBAL VAR OF FORM
        /// Process Mode (APP,SERVER) -> Kind of Process wether application side , server side
        /// ProcessType (EMP,WRKGRP) -> Employee Wise or WrkGrp Wise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="ProcessMode">APP/SERVER</param>
        /// <param name="ProcessType">EMP/WRKGRP</param>
        private void ProcessDATA(object sender, EventArgs e,string tProcessMode = "APP" , string tProcessType = "EMP")
        {

            if (ProcessList.Rows.Count <= 0)
            {
                MessageBox.Show("No Records found to process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnClearList.Enabled = false;
            btnProcessWrkGrp.Enabled = false;
            txtWrkFromDt.Enabled = false;
            txtWrkToDate.Enabled = false;
            txtWrkGrpCode.Enabled = false;

            ISProcessStarted = true;
            appstatus_Tick(sender, e);


            Cursor.Current = Cursors.WaitCursor;

            
                if (ProcessName == "ATTD")
                {
                    foreach (DataRow dr in ProcessList.Rows)
                    {
                        string tEmpUnqID = dr["EmpUnqID"].ToString();
                        DateTime tFromDt = Convert.ToDateTime(dr["FromDate"]);
                        DateTime tToDt = Convert.ToDateTime(dr["ToDate"]);
                        int res;
                        string proerr = string.Empty;
                        clsProcess pr = new clsProcess();
                        pr.AttdProcess(tEmpUnqID, tFromDt,tToDt,out res,out proerr);

                        //update processed status
                        if (res > 0)
                        {
                            dr.BeginEdit();
                            dr["IsDone"] = 1;
                            dr["Status"] = "Processed";
                            dr.EndEdit();
                            dr.AcceptChanges();
                            RefreshAppGrid(sender,e);
                            Application.DoEvents();
                        }
                        
                        if(!string.IsNullOrEmpty(proerr))
                        {
                            dr["Status"] = "Processed but with error : " + proerr;
                        }

                    }
                }
                else if (ProcessName == "MESS")
                {
                    foreach (DataRow dr in ProcessList.Rows)
                    {
                        string tEmpUnqID = dr["EmpUnqID"].ToString();
                        DateTime tFromDt = Convert.ToDateTime(dr["FromDate"]);
                        DateTime tToDt = Convert.ToDateTime(dr["ToDate"]);
                        int res;
                        clsProcess pr = new clsProcess();
                        pr.LunchProcess(tEmpUnqID, tFromDt, tToDt, out res);
                        Application.DoEvents();
                        //update processed status
                        if (res > 0)
                        {
                            dr.BeginEdit();
                            dr["IsDone"] = 1;
                            dr["Status"] = "Processed";
                            dr.EndEdit();
                            dr.AcceptChanges();
                            RefreshAppGrid(sender, e);
                        }

                    }
                }
                else if (ProcessName == "LUNCHINOUT")
                {
                    foreach (DataRow dr in ProcessList.Rows)
                    {
                        string tEmpUnqID = dr["EmpUnqID"].ToString();
                        DateTime tFromDt = Convert.ToDateTime(dr["FromDate"]);
                        DateTime tToDt = Convert.ToDateTime(dr["ToDate"]);
                        int res;
                        clsProcess pr = new clsProcess();
                        pr.LunchInOutProcess(tEmpUnqID, tFromDt, tToDt, out res);
                        Application.DoEvents();
                        //update processed status
                        if (res > 0)
                        {
                            dr.BeginEdit();
                            dr["IsDone"] = 1;
                            dr["Status"] = "Processed";
                            dr.EndEdit();
                            dr.AcceptChanges();
                            RefreshAppGrid(sender, e);
                        }
                    }
                }

            Cursor.Current = Cursors.Default;
            btnAdd.Enabled = true;
            btnDelete.Enabled = true;
            btnProcessWrkGrp.Enabled = true;
            txtWrkFromDt.Enabled = true;
            txtWrkToDate.Enabled = true;
            txtWrkGrpCode.Enabled = true;

            btnClearList.Enabled = true;
            ISProcessStarted = false;
            appstatus_Tick(sender, e);
            MessageBox.Show("Process Completed...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmProcess_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter))
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

    }
}
