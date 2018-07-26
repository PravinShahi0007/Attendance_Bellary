using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Helpers;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using Attendance.Classes;
using System.Threading;
using System.IO;
using System.Net;
using ConnectUNCWithCredentials;
using System.Reflection;

namespace Attendance
{
    public partial class frmMain : XtraForm
    {
        public static string cnstr = Utils.Helper.constr;
        public static Utils.DbCon tdb = Utils.Helper.ReadConDb("DBCON");
        

        public frmMain()
        {
            InitializeComponent();
            stsUserID.Text = Utils.User.GUserID;
            stsUserDesc.Text = Utils.User.GUserName;

            this.Text = "Attendance System (JSAW): (Server->" + tdb.DataSource + ")";
        }

        private void mnuUserRights_Click(object sender, EventArgs e)
        {
           
            Form t = Application.OpenForms["frmUserRights"];

            if (t == null)
            {
                Attendance.Forms.frmUserRights m = new Attendance.Forms.frmUserRights();
                m.MdiParent = this;
                m.Show();
            }

        }

        private void mnuLogOff_Click(object sender, EventArgs e)
        {
            Utils.User.GUserID = string.Empty;
            Utils.User.GUserName = string.Empty;
            Utils.User.GUserPass = string.Empty;
            Utils.User.IsAdmin = false;


            Program.OpenMDIFormOnClose = false;
            this.Hide();
            Application.Restart();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
           


            ToolStripItemCollection tmnu = menuStrip1.Items;
            SetToolStripItems(tmnu);

            mnuAdmin.Enabled = true;
            mnuConfig.Enabled = true;
            mnuMast.Enabled = true;        
            mnuProfile.Enabled = true;
            mnuEmployee.Enabled = true;
            mnuMess.Enabled = true;
            mnuTranS.Enabled = true;
            mnuLeave.Enabled = true;        
            mnuShift.Enabled = true;
            mnuSanction.Enabled = true;
            mnuData.Enabled = true;
            mnuCostCent.Enabled = true;
            mnuChangePass.Enabled = true;
            mnuLogOff.Enabled = true;

            DataSet ds = new DataSet();
            string sql = "select menuname from  MastFrm where formid in (select FormId from userRights where UserId ='" + Utils.User.GUserID + "' and View1=1) order by seqid";
            ds = Utils.Helper.GetData(sql,cnstr);
            
            mnuUser.Enabled = true;
            Boolean hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);
                
            
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string mnu = dr["menuname"].ToString();

                    ToolStripItem[] t = tmnu.Find(mnu, true);

                    foreach (ToolStripItem ti in t)
                    {
                        ti.Enabled = true;
                    }

                }    
            }

            this.mnuHelp.Enabled = true;
            this.mnuAbout.Enabled = true;
            this.mnuStatus.Enabled = true;
            this.mnuServerStat.Enabled = true;


            //Set GateInOutIP
            Globals.SetGateInOutIPList();

            //set LunchInOutIP
            Globals.SetLunchInOutIPList();

            //set waterip
            Globals.SetWaterIPList();

            //set ShiftList
            Globals.SetShiftList();

            //set global vars
            Globals.GetGlobalVars();

            //get localmodification date
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string localfile = Uri.UnescapeDataString(uri.Path);
            
            if (IsNetworkPath(localfile))
            {
                MessageBox.Show("Does not allow to run from remote location/shared folder..," +
                    Environment.NewLine +
                    "Please Copy to Local Drive and Run Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
            }


            //here we can start Quartz if host is server
            if (Utils.User.GUserID == "SERVER")
            {
                Globals.G_myscheduler = new Scheduler();
                
                mnuRFIDUser.Enabled = true;
                mnuDataDownload.Enabled = true;
                mnuDataProcess.Enabled = true;
                mnuAutoMailSender.Enabled = true;
                mnuCostCodeManPowerProcess.Enabled = true;

                Form t = Application.OpenForms["frmServerStatus"];

                if (t == null)
                {
                    Attendance.Forms.frmServerStatus m = new Attendance.Forms.frmServerStatus();
                    m.MdiParent = this;
                    m.WindowState = FormWindowState.Maximized;
                    m.Show();
                }

                Thread.Sleep(TimeSpan.FromSeconds(5));
             
                Globals.G_myscheduler.Start();
                //create triggers
                Globals.G_myscheduler.RegSchedule_AutoTimeSet();
                Globals.G_myscheduler.RegSchedule_WorkerProcess();
                Globals.G_myscheduler.RegSchedule_AutoArrival();
                Globals.G_myscheduler.RegSchedule_AutoProcess();
                Globals.G_myscheduler.RegSchedule_DownloadPunch();
                Globals.G_myscheduler.RegSchedule_AutoMail();
            }
            else
            {
                //check for update version.
                DateTime servermodified = new DateTime();
                DateTime localmodified = new DateTime();

                if (!string.IsNullOrEmpty(Globals.G_UpdateChkPath))
                {
                    this.Cursor = Cursors.WaitCursor;
                    Application.DoEvents();
                    
                    using (UNCAccessWithCredentials unc = new UNCAccessWithCredentials())
                    {
                        if (unc.NetUseWithCredentials(Globals.G_UpdateChkPath,
                                                      Globals.G_NetworkUser,
                                                      Globals.G_NetworkDomain,
                                                      Globals.G_NetworkPass))
                        {
                            string fullpath = Path.Combine(Globals.G_UpdateChkPath, "AttendanceJSAW.exe");
                            if (File.Exists(fullpath))
                            {
                                servermodified = File.GetLastWriteTime(fullpath);
                            }
                        }
                    }

                    
                    
                    
                    
                    localmodified = File.GetLastWriteTime(localfile);
                    if (servermodified > localmodified)
                    {
                        MessageBox.Show("New Upgrade is available, please update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        
                    }
                    this.Cursor = Cursors.Default;
                }

            }

        }

        public static string GetNetworkPathFromServerName(string serverName)
        {
            // Assume we can't connect to the server to start with.
            var networkPath = String.Empty;

            // If this is a rooted path, just make sure it is available.
            if (Path.IsPathRooted(serverName))
            {
                // If the path exists, use it.
                if (Directory.Exists(serverName))
                    networkPath = serverName;
            }
            // Else this is a network path.
            else
            {
                // If the server name has a backslash in it, remove the backslash and everything after it.
                serverName = serverName.Trim(@"\".ToCharArray());
                if (serverName.Contains(@"\"))
                    serverName = serverName.Remove(serverName.IndexOf(@"\", StringComparison.Ordinal));

                try
                {
                    // If the server is available, format the network path properly to use it.
                    if (Dns.GetHostEntry(serverName) != null)
                    {
                        // Root the path as a network path (i.e. add \\ to the front of it).
                        networkPath = String.Format("\\\\{0}", serverName);
                    }
                }
                // Eat any Host Not Found exceptions for if we can't connect to the server.
                catch (System.Net.Sockets.SocketException)
                { }
            }

            return networkPath;
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //we need to shutdown our scheduler quartz

            if (Utils.User.GUserID == "SERVER")
            {
                Globals.G_myscheduler.Stop();
            }

        }

        private void SetToolStripItems(ToolStripItemCollection dropDownItems)
        {
            try
            {
                foreach (object obj in dropDownItems)
                //for each object.
                {
                    ToolStripMenuItem subMenu = obj as ToolStripMenuItem;
                    //Try cast to ToolStripMenuItem as it could be toolstrip separator as well.

                    if (subMenu != null)
                    //if we get the desired object type.
                    {
                        if (subMenu.HasDropDownItems) // if subMenu has children
                        {
                            SetToolStripItems(subMenu.DropDownItems); // Call recursive Method.
                        }
                        else // Do the desired operations here.
                        {
                            subMenu.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SetToolStripItems",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuChangePass_Click(object sender, EventArgs e)
        {   
            Form t = Application.OpenForms["frmChangePass"];

            if (t == null)
            {
                Attendance.Forms.frmChangePass m = new Attendance.Forms.frmChangePass();
                m.MdiParent = this;
                m.Show();
            }

        }

        private void mnuDomainConfig_Click(object sender, EventArgs e)
        {
           
            Form t = Application.OpenForms["frmDomainConfig"];

            if (t == null)
            {
                Attendance.Forms.frmDomainConfig m = new Attendance.Forms.frmDomainConfig();
                m.MdiParent = this;
               
                m.Show();
            }

        }

        private void mnuDBConn_Click(object sender, EventArgs e)
        {
            
            Form t = Application.OpenForms["FrmConnection"];

            if (t == null)
            {
                FrmConnection m = new FrmConnection();
                m.MdiParent = this;
                m.typeofcon = "DBCON";
                m.Show();
            }

        }

        private void mnuMastWrkGrp_Click(object sender, EventArgs e)
        {
            
            Form t = Application.OpenForms["frmMastWrkGrp"];

            if (t == null)
            {
                Attendance.Forms.frmMastWrkGrp m = new Attendance.Forms.frmMastWrkGrp();
                m.MdiParent = this;                
                m.Show();
            }

        }

        private void mnuMastUnit_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastUnit"];

            if (t == null)
            {
                Attendance.Forms.frmMastUnit m = new Attendance.Forms.frmMastUnit();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastDept_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastDept"];

            if (t == null)
            {
                Attendance.Forms.frmMastDept m = new Attendance.Forms.frmMastDept();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastCat_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastCat"];

            if (t == null)
            {
                Attendance.Forms.frmMastCat m = new Attendance.Forms.frmMastCat();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastDesg_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastDesg"];

            if (t == null)
            {
                Attendance.Forms.frmMastDesg m = new Attendance.Forms.frmMastDesg();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastGrade_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastGrade"];

            if (t == null)
            {
                Attendance.Forms.frmMastGrade m = new Attendance.Forms.frmMastGrade();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastEmpType_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastEmpType"];

            if (t == null)
            {
                Attendance.Forms.frmMastEmpType m = new Attendance.Forms.frmMastEmpType();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastComp_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastComp"];

            if (t == null)
            {
                Attendance.Forms.frmMastComp m = new Attendance.Forms.frmMastComp();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastCont_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastCont"];

            if (t == null)
            {
                Attendance.Forms.frmMastCont m = new Attendance.Forms.frmMastCont();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMessConfig_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastMess"];

            if (t == null)
            {
                Attendance.Forms.frmMastMess m = new Attendance.Forms.frmMastMess();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastFood_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastMessFood"];

            if (t == null)
            {
                Attendance.Forms.frmMastMessFood m = new Attendance.Forms.frmMastMessFood();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastMessGrp_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastMessGrp"];

            if (t == null)
            {
                Attendance.Forms.frmMastMessGrp m = new Attendance.Forms.frmMastMessGrp();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastRate_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastMessRate"];

            if (t == null)
            {
                Attendance.Forms.frmMastMessRate m = new Attendance.Forms.frmMastMessRate();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuConfig_Click(object sender, EventArgs e)
        {

        }

        private void mnuMastTime_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastMessTime"];

            if (t == null)
            {
                Attendance.Forms.frmMastMessTime m = new Attendance.Forms.frmMastMessTime();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void MnuReaderConfig_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmReaderConfig"];

            if (t == null)
            {
                Attendance.Forms.frmReaderConfig m = new Attendance.Forms.frmReaderConfig();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuReaderMessAsign_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmReaderConfigMess"];

            if (t == null)
            {
                Attendance.Forms.frmReaderConfigMess m = new Attendance.Forms.frmReaderConfigMess();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastLeave_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastLeave"];

            if (t == null)
            {
                Attendance.Forms.frmMastLeave m = new Attendance.Forms.frmMastLeave();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastShift_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastShift"];

            if (t == null)
            {
                Attendance.Forms.frmMastShift m = new Attendance.Forms.frmMastShift();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastRules_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastRules"];

            if (t == null)
            {
                Attendance.Forms.frmMastRules m = new Attendance.Forms.frmMastRules();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastHoliday_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastHoliday"];

            if (t == null)
            {
                Attendance.Forms.frmMastHoliday m = new Attendance.Forms.frmMastHoliday();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuCostCodeMast_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastCostCode"];

            if (t == null)
            {
                Attendance.Forms.frmMastCostCode m = new Attendance.Forms.frmMastCostCode();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuCostCodeManPowerProcess_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastCostCodeProcess"];

            if (t == null)
            {
                Attendance.Forms.frmMastCostCodeProcess m = new Attendance.Forms.frmMastCostCodeProcess();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuEmpCostCode_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastCostCodeEmp"];

            if (t == null)
            {
                Attendance.Forms.frmMastCostCodeEmp m = new Attendance.Forms.frmMastCostCodeEmp();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuCostCodeSanManPower_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastCostCodeSanManPower"];

            if (t == null)
            {
                Attendance.Forms.frmMastCostCodeSanManPower m = new Attendance.Forms.frmMastCostCodeSanManPower();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuEmpCostCodeBulk_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmBulkCostCodeUpdate"];

            if (t == null)
            {
                Attendance.Forms.frmBulkCostCodeUpdate m = new Attendance.Forms.frmBulkCostCodeUpdate();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuValidityMass_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmValidityExtend"];

            if (t == null)
            {
                Attendance.Forms.frmValidityExtend m = new Attendance.Forms.frmValidityExtend();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastStat_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastStat"];

            if (t == null)
            {
                Attendance.Forms.frmMastStat m = new Attendance.Forms.frmMastStat();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuCreateMuster_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmCreateMuster"];

            if (t == null)
            {
                Attendance.Forms.frmCreateMuster m = new Attendance.Forms.frmCreateMuster();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuUserSpRights_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmUserSpRights"];
            if (t == null)
            {
                Attendance.Forms.frmUserSpRights m = new Attendance.Forms.frmUserSpRights();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuUserDSRights_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuUserEmpRights_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuAutoMail_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Globals.G_DefaultMailID))
            {
                Form t = Application.OpenForms["frmAutoMailSubScription"];
                if (t == null)
                {
                    Attendance.Forms.frmAutoMailSubScription m = new Attendance.Forms.frmAutoMailSubScription();
                    m.MdiParent = this;
                    m.Show();
                }
            }
            else
            {
                MessageBox.Show("Default MailID is not configured..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
           
        }

        private void mnuLeaveBalUpload_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmUploadLeaveBal"];
            if (t == null)
            {
                Attendance.Forms.frmUploadLeaveBal m = new Attendance.Forms.frmUploadLeaveBal();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuLeaveBalEntry_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmLeaveBalEntry"];
            if (t == null)
            {
                Attendance.Forms.frmLeaveBalEntry m = new Attendance.Forms.frmLeaveBalEntry();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuLeaveEntryLunch_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmLunchHalfDaypost"];
            if (t == null)
            {
                Attendance.Forms.frmLunchHalfDaypost m = new Attendance.Forms.frmLunchHalfDaypost();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastEmp_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastEmpBasicData"];
            if (t == null)
            {
                Attendance.Forms.frmMastEmpBasicData m = new Attendance.Forms.frmMastEmpBasicData();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastJob_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastEmpJobProfile"];
            if (t == null)
            {
                Attendance.Forms.frmMastEmpJobProfile m = new Attendance.Forms.frmMastEmpJobProfile();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastEmpPer_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastEmpPerInfo"];
            if (t == null)
            {
                Attendance.Forms.frmMastEmpPerInfo m = new Attendance.Forms.frmMastEmpPerInfo();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuImportEmp_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmEmployeeImport"];
            if (t == null)
            {
                Attendance.Forms.frmEmployeeImport m = new Attendance.Forms.frmEmployeeImport();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMisConduct_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMisConduct"];
            if (t == null)
            {
                Attendance.Forms.frmMisConduct m = new Attendance.Forms.frmMisConduct();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuDataProcess_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmProcess"];
            if (t == null)
            {
                Attendance.Forms.frmProcess m = new Attendance.Forms.frmProcess();
                m.MdiParent = this;
                m.ProcessName = "ATTD";
                m.Show();
            }
        }

        private void mnuMessDataProcess_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmProcess"];
            if (t == null)
            {
                Attendance.Forms.frmProcess m = new Attendance.Forms.frmProcess();
                m.MdiParent = this;
                m.ProcessName = "MESS";
                m.Show();
            }
        }

        private void mnuLunchInOutProcess_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmProcess"];
            if (t == null)
            {
                Attendance.Forms.frmProcess m = new Attendance.Forms.frmProcess();
                m.MdiParent = this;
                m.ProcessName = "LUNCHINOUT";
                m.Show();
            }
        }

        private void mnuCreateUser_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmUserRights"];

            if (t == null)
            {
                Attendance.Forms.frmUserRights m = new Attendance.Forms.frmUserRights();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuBulkSan_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmBulkSanction"];

            if (t == null)
            {
                Attendance.Forms.frmBulkSanction m = new Attendance.Forms.frmBulkSanction();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuOtherConfig_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmOtherConfig"];

            if (t == null)
            {
                Attendance.Forms.frmOtherConfig m = new Attendance.Forms.frmOtherConfig();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuManualSan_Click(object sender, EventArgs e)
        {
            Attendance.Forms.frmSanction m = new Attendance.Forms.frmSanction();
            m.MdiParent = this;
            m.Show();
        }

        private void mnuLeaveEntry_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmLeaveEntry"];

            if (t == null)
            {
                Attendance.Forms.frmLeaveEntry m = new Attendance.Forms.frmLeaveEntry();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuShiftSchUpload_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmUploadShiftSchedule"];

            if (t == null)
            {
                Attendance.Forms.frmUploadShiftSchedule m = new Attendance.Forms.frmUploadShiftSchedule();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuWeekoffSan_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmBulkWOChange"];

            if (t == null)
            {
                Attendance.Forms.frmBulkWOChange m = new Attendance.Forms.frmBulkWOChange();
                m.GType = "WO";
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuShiftChange_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmBulkWOChange"];

            if (t == null)
            {
                Attendance.Forms.frmBulkWOChange m = new Attendance.Forms.frmBulkWOChange();
                m.GType = "SHIFT";
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuHalfDayCheck_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmHalfDayCheck"];

            if (t == null)
            {
                Attendance.Forms.frmHalfDayCheck m = new Attendance.Forms.frmHalfDayCheck();

                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuRulesCheck_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmRulesCheck"];

            if (t == null)
            {
                Attendance.Forms.frmRulesCheck m = new Attendance.Forms.frmRulesCheck();
                
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMessInOutMachine_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMessInOutMachine"];

            if (t == null)
            {
                Attendance.Forms.frmMessInOutMachine m = new Attendance.Forms.frmMessInOutMachine();

                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuAutoMailSender_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmAutoMailSender"];

            if (t == null)
            {
                Attendance.Forms.frmAutoMailSender m = new Attendance.Forms.frmAutoMailSender();

                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuDataDownload_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmDataDownload"];

            if (t == null)
            {
                Attendance.Forms.frmDataDownload m = new Attendance.Forms.frmDataDownload();

                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuRFIDUser_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastUserManagement"];

            if (t == null)
            {
                Attendance.Forms.frmMastUserManagement m = new Attendance.Forms.frmMastUserManagement();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuServerStat_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmServerStatus"];

            if (t == null)
            {
                Attendance.Forms.frmServerStatus m = new Attendance.Forms.frmServerStatus();
                m.MdiParent = this;
                m.WindowState = FormWindowState.Maximized;
                m.Show();
            }

        }
        
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            string msg = "Attedance System" + Environment.NewLine +
                "Version 2.1 " + Environment.NewLine +
                "Design & Devloped By : Anand Achraya " + Environment.NewLine;

            MessageBox.Show(msg, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuMastStatSec_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastStatSec"];

            if (t == null)
            {
                Attendance.Forms.frmMastStatSec m = new Attendance.Forms.frmMastStatSec();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastHolidayOpt_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastHolidayOpt"];

            if (t == null)
            {
                Attendance.Forms.frmMastHolidayOpt m = new Attendance.Forms.frmMastHolidayOpt();
                m.MdiParent = this;
                m.Show();
            }
        }
        
        public static Boolean IsNetworkPath(String path)
        {

            try
            {
                Uri uri = new Uri(path);
                if (uri.IsUnc)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return true;
            }
            
            
            
        }

        private void mnuMastException_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastException"];

            if (t == null)
            {
                Attendance.Forms.frmMastException m = new Attendance.Forms.frmMastException();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuCopyWrkGrpToOther_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMastWrkGrpCopy"];

            if (t == null)
            {
                Attendance.Forms.frmMastWrkGrpCopy m = new Attendance.Forms.frmMastWrkGrpCopy();
                m.MdiParent = this;
                m.Show();
            }
        }
        

    }
}