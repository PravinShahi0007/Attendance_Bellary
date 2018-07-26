using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Attendance.Classes;



namespace Attendance.Forms
{
    public partial class frmLunchHalfDaypost : Form
    {
        
        public string GRights = "XXXV";
        clsProcess pro = new clsProcess();


        public frmLunchHalfDaypost()
        {
            InitializeComponent();
        }

        private void frmLunchHalfDaypost_Load(object sender, EventArgs e)
        {
            ResetCtrl();
            GRights = Attendance.Classes.Globals.GetFormRights(this.Name);
            SetRights();
            
        }

        private string DataValidate()
        {
            string err = string.Empty;

            if (string.IsNullOrEmpty(txtCompCode.Text))
            {
                err = err + "Please Enter CompCode " + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtCompName.Text))
            {
                err = err + "Please Enter CompName..." + Environment.NewLine;
            }


            if (string.IsNullOrEmpty(txtWrkGrpCode.Text))
            {
                err = err + "Please Enter WrkGrpCode " + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtWrkGrpDesc.Text))
            {
                err = err + "Please Enter WrkGrp Description" + Environment.NewLine;
            }

            if (txtFromDt.EditValue == null)
            {
                err = err + "Please Enter From Date" + Environment.NewLine;
                return err;
            }

            if (txtFromDt.DateTime == DateTime.MinValue)
            {
                err = err + "Please Enter From Date" + Environment.NewLine;
                return err;
            }

            if (txtToDate.EditValue == null)
            {
                err = err + "Please Enter ToDate" + Environment.NewLine;
                return err;
            }

            if (txtToDate.DateTime == DateTime.MinValue)
            {
                err = err + "Please Enter ToDate" + Environment.NewLine;
                return err;
            }

            if (txtFromDt.DateTime > txtToDate.DateTime)
            {
                err = err + "Please Enter Valid Date Range.." + Environment.NewLine;                
            }

            TimeSpan ts = (txtFromDt.DateTime - txtToDate.DateTime);

            if (Math.Abs(ts.Days) > 31)
            {
                err += "Please Contact To System Administrator for More than 31 days Process.." + Environment.NewLine;
            }

            //string curmonth = Utils.Helper.GetDescription("SELECT CalendarYearMonth from dbo.F_TABLE_DATE(GetDate(),GetDate())", Utils.Helper.constr);
            string prvmonth = Utils.Helper.GetDescription("SELECT Convert(varchar(6),DateAdd(month,-1,CalendarStartOfMonthDate),112) from dbo.F_TABLE_DATE(GetDate(),GetDate())", Utils.Helper.constr);
            string startmonth = txtFromDt.DateTime.ToString("yyyyMM");

            if (prvmonth != "")
            {
                int pmth = 0;
                int smth = 0;

                pmth = Convert.ToInt32(prvmonth);
                smth = Convert.ToInt32(startmonth);

                if (smth < pmth)
                {
                    err += "Please Contact To System Administrator for Previous Month Process.." + Environment.NewLine;
                }
            }


            return err;
        }

        

        private void ResetCtrl()
        {
            
            btnProcess.Enabled = false;

            
            object s = new object();
            EventArgs e = new EventArgs();
            txtCompCode.Text = "01";
            txtCompName.Text = "";
            txtCompCode_Validated(s, e);
           
            txtWrkGrpCode.Text = "";
            txtWrkGrpDesc.Text = "";
            txtEmpUnqID.Text = "";
            txtEmpName.Text = "";
            txtFromDt.EditValue = null;
            txtToDate.EditValue = null;
            pBar.EditValue = 0;
            pBar.Properties.Step = 1;
            pBar.Properties.PercentView = true;
            pBar.Properties.Minimum = 0;

          
            txtWrkGrpCode.Enabled = true;
            txtEmpUnqID.Enabled = true;
            txtFromDt.Enabled = true;
            txtToDate.Enabled = true;

        }

        private void SetRights()
        {
            btnProcess.Enabled = false;
            if (GRights.Contains("A") || GRights.Contains("U") || GRights.Contains("D"))
                btnProcess.Enabled = true;
            
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
            if (txtCompCode.Text.Trim() == "" || txtCompName.Text.Trim() == "" )
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
                    txtCompCode_Validated(sender,e);
                    
                }
            }

            
        }

        private void txtCompCode_Validated(object sender, EventArgs e)
        {
            if (txtCompCode.Text.Trim() == "")
            {   
                return;
            }

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
                txtCompName.Text = "";
            }
            
        }

        private void txtCompCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 )
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

        private void txtEmpUnqID_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtWrkGrpCode.Text.Trim() == "")
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select EmpUnqID,EmpName From MastEmp Where CompCode ='" + txtCompCode.Text.Trim() + "' and WrkGrp = '" + txtWrkGrpCode.Text.Trim() + "' and Active = 1 ";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "EmpUnqID", "EmpUnqID", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtEmpUnqID.Text = obj.ElementAt(0).ToString();
                    txtEmpName.Text = obj.ElementAt(1).ToString();
                   

                }
            }
        }

        private void txtEmpUnqID_Validated(object sender, EventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtCompName.Text.Trim() == "" 
                || txtWrkGrpCode.Text.Trim() == "" || txtWrkGrpDesc.Text.Trim() == ""
                || txtEmpUnqID.Text.Trim() == "" )
            {

                return;
            }

            //txtEmpUnqID.Text = txtEmpUnqID.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastEmp where CompCode ='" + txtCompCode.Text.Trim() + "' " +
                    " and WrkGrp='" + txtWrkGrpCode.Text.Trim() + "' and  EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "' and Active = 1";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCompCode.Text = dr["CompCode"].ToString();
                    txtWrkGrpCode.Text = dr["WrkGrp"].ToString();
                    txtEmpUnqID.Text = dr["EmpUnqID"].ToString();
                    txtEmpName.Text = dr["EmpName"].ToString();
                    txtCompCode_Validated(sender, e);
                    txtWrkGrpCode_Validated(sender, e);
                    
                }
            }
            else
            {
               
                txtEmpName.Text = "";
                txtEmpUnqID.Text = "";

            }

            

        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
 
            if(!string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()) )
            {
                if (string.IsNullOrEmpty(txtEmpName.Text.Trim()))
                {
                    MessageBox.Show("Please Enter Valid Employee", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (!GRights.Contains("AUDV"))
            {
                MessageBox.Show("You are not fully authorised..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            string sql = string.Empty;
            string question = string.Empty;
            DialogResult drq ;

            string sWrkGrp = txtWrkGrpCode.Text.Trim().ToString();
            string sEmpUnqID = txtEmpUnqID.Text.Trim().ToString();
            

            if (string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()))
            {
                 question = "Are You Sure to Process/Post Lunch Half Day Check For : " + sWrkGrp + Environment.NewLine
                    + "Processed Data will be deleted between '" + txtFromDt.DateTime.ToString("yyyy-MM-dd") + "' And '" + txtToDate.DateTime.ToString("yyyy-MM-dd") + "' ";


                 sql = "Select CompCode,WrkGrp,EmpUnqID,OTFlg,WeekOff from mastemp where CompCode = '01' and Wrkgrp = '" + sWrkGrp + "' And Active = 1  Order By EmpUnqID";
    
            }else{


                question = "Are You Sure to to Process/Post Lunch Half Day Check For : " + txtEmpUnqID.Text.Trim().ToString() + Environment.NewLine
                    + "Processed Data will be deleted between '" + txtFromDt.DateTime.ToString("yyyy-MM-dd") + "' And '" + txtToDate.DateTime.ToString("yyyy-MM-dd") + "' ";

                sql = "Select CompCode,WrkGrp,EmpUnqID,OTFlg,WeekOff From MastEmp Where CompCode ='" + txtCompCode.Text.Trim() + "' "
                    + " and WrkGrp = '" + sWrkGrp + "' "
                    + " and EmpUnqID ='" + sEmpUnqID + "' ";
                   
            
            }
            drq = MessageBox.Show(question,"Question",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if(drq == DialogResult.No)
            {
                MessageBox.Show("Process Canceled..","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            btnProcess.Enabled = false;
            txtWrkGrpCode.Enabled = false;
            txtEmpUnqID.Enabled = false;
            txtFromDt.Enabled = false;
            txtToDate.Enabled = false;

            string sFromDt = txtFromDt.DateTime.ToString("yyyy-MM-dd");
            string sToDt = txtToDate.DateTime.ToString("yyyy-MM-dd");


            Cursor.Current = Cursors.WaitCursor;

            DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                pBar.Properties.Maximum = ds.Tables[0].Rows.Count + 1;
                
                foreach (DataRow drs in ds.Tables[0].Rows)
                {
                    //update progressbar
                    pBar.PerformStep();
                    pBar.Update();
                    sEmpUnqID = drs["EmpUnqID"].ToString();
                    string WeekOff = drs["WeekOff"].ToString();
                    //save in db for accountibility
                    using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                    {
                        try
                        {
                            cn.Open();
                            SqlTransaction tr = cn.BeginTransaction("LunchHalfDayPost");
                            
                            

                            DataSet AttdLunchDs = new DataSet();

                            sql = "Select * From AttdLunchHistory where tDate " +
                                 " Between '" + sFromDt + "' and '" + sToDt + "' and " +
                                 " EmpUnqID = '" + drs["EmpUnqID"].ToString() + "' and ignore = 0 and posted = 0 Order By tDate Asc ";

                            AttdLunchDs = Utils.Helper.GetData(sql, Utils.Helper.constr);
                            int lngRecAffected = 0, tLeaveHalf = 0;
                            
                            bool posted = false;

                            hasRows = AttdLunchDs.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                            #region trsloop
                            if (hasRows)
                            {  
                                posted = false;

                                foreach (DataRow trs in AttdLunchDs.Tables[0].Rows)
                                {
                                    lngRecAffected = 0; tLeaveHalf = 0;

                                    #region posting
                                    if ( (trs["LeaveStatus"].ToString()) != "" && (trs["LeaveStatus"].ToString()) == "AB")
                                    {
                                        lngRecAffected = 0;
                                        tLeaveHalf = 1;

                                        sql = "Select Count(*) from LeaveEntry where CompCode = '" + drs["CompCode"] + "' and " +
                                        " WrkGrp = '" + drs["WrkGrp"] + "' and tYear = '" + Convert.ToDateTime(trs["tDate"]).ToString("yyyy") + "' " +
                                        " And EmpUnqID = '" + drs["EmpUnqID"].ToString() + "' and FromDt ='" + Convert.ToDateTime(trs["tDate"]).ToString("yyyy-MM-dd") + "' and isnull(LeaveTyp,'') <> '' ";

                                        int cnt = Convert.ToInt32(Utils.Helper.GetDescription(sql, Utils.Helper.constr));

                                        if (cnt == 0)
                                        {
                                           sql = " Insert into LeaveEntry (CompCode,WrkGrp,tYear,EmpUnqID,FromDt,ToDt,LeaveTyp,TotDay," +
                                                 " WoDay,PublicHL,LeaveDed,LeaveAdv,LeaveHalf,Remark,AddDt,AddID,DelFlg)" +
                                                 " Values (" +
                                                    " '" + drs["CompCode"].ToString() + "', " +
                                                    " '" + drs["WrkGrp"].ToString() + "'," +
                                                    " '" + Convert.ToDateTime(trs["tDate"]).ToString("yyyy") + "', " + 
                                                    " '" + drs["EmpUnqID"].ToString() +  "'," +
                                                    " '" + Convert.ToDateTime(trs["tDate"]).ToString("yyyy-MM-dd") + "', " + 
                                                    " '" + Convert.ToDateTime(trs["tDate"]).ToString("yyyy-MM-dd") + "', " + 
                                                    " 'AB',0.5,0,0,0.5,0,1,'Lunch Rules', GetDate(),'Sys',0)" ;

                                            SqlCommand cmd = new SqlCommand(sql,cn,tr);
                                            cmd.CommandType = CommandType.Text;
                                            lngRecAffected = (int)cmd.ExecuteNonQuery();

                                            if (lngRecAffected > 0)
                                            {
                                                posted = true;
                                                lngRecAffected = 0 ;
                                                 sql = "Insert into MastLeaveSchedule " +
                                                      "(EmpUnqID,WrkGrp,tDate,AddDt,AddID,schLeave,SchLeaveHalf) " +
                                                        " Values ('" + trs["EmpUnqID"].ToString()  + "'," +
                                                        " '" + trs["WrkGrp"].ToString() + "','" + Convert.ToDateTime(trs["tDate"]).ToString("yyyy-MM-dd") + "'," +
                                                        " GetDate(),'Sys','AB','" + tLeaveHalf + "')";
                            
                                                 cmd = new SqlCommand(sql,cn,tr);
                                                 cmd.CommandType = CommandType.Text;
                                                 lngRecAffected = (int)cmd.ExecuteNonQuery();
                                                
                                                if(lngRecAffected > 0)
                                                {
                                                
                                                    sql = "Update AttdLunchHistory Set Posted = 1, PostedBy ='" + Utils.User.GUserID + "' " +
                                                        " where tDate '" + Convert.ToDateTime(trs["tDate"]).ToString("yyyy-MM-dd") + "' And " +
                                                        " EmpUnqID = '" + trs["EmpUnqID"].ToString() + "' ";
                                                    cmd = new SqlCommand(sql, cn, tr);
                                                    cmd.CommandType = CommandType.Text;
                                                    cmd.ExecuteNonQuery();
                                                }
                                                    
                                                
                                            }

                                        }
                                        else
                                        {
                                            txtError.Text += drs["EmpUnqID"].ToString() + " : " + "Transaction Failur.." + Environment.NewLine;

                                            int res = 0;
                                            pro.LunchInOutProcess(drs["EmpUnqID"].ToString(), Convert.ToDateTime(trs["tDate"]), Convert.ToDateTime(trs["tDate"]), out res);
                                            
                                        }


                                    }
                                     #endregion posting

                                }//for each AttdLunchDs Rows
                                try
                                {
                                    tr.Commit();
                                }
                                catch (Exception ex)
                                {
                                    txtError.Text += txtError.Text + ex.ToString();
                                    tr.Rollback();
                                    continue;
                                }
                                #region processAttendance
                                if (posted == true)
                                {

                                    //get minimum date of postedflg = true between from date and todate
                                    //get maximum date of posted flg = true between from date and todate
                                    string minsql = "Select Convert(date,Min(tDate),121) from AttdLunchHistory where Empunqid = '" + drs["EmpUnqID"].ToString() + "' and tDate Between '" + sFromDt + "' and '" + sToDt + "' and posted = 1" ;
                                    string maxsql = "Select Convert(date,Max(tDate),121) from AttdLunchHistory where Empunqid = '" + drs["EmpUnqID"].ToString() + "' and tDate Between '" + sFromDt + "' and '" + sToDt + "' and posted = 1";

                                    string s1dt = Utils.Helper.GetDescription(minsql, Utils.Helper.constr);
                                    string s2dt = Utils.Helper.GetDescription(maxsql, Utils.Helper.constr);

                                    if(!string.IsNullOrEmpty(s1dt) && !string.IsNullOrEmpty(s2dt))
                                    {
                                        int res = 0;
                                        err = string.Empty;
                                        DateTime tFromDt = Convert.ToDateTime(s1dt);
                                        DateTime tToDt = Convert.ToDateTime(s2dt);
                                        string tEmpUnq = drs["EmpUnqID"].ToString();
                                        pro.AttdProcess(tEmpUnq, tFromDt, tToDt,out res, out err);
                                    
                                    }

                                    

                                }
                                #endregion processAttendance

                            }//if has row

                            
                            #endregion trsloop

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }


                    }//using connection

                } //foreach loop drs
            }
            MessageBox.Show("Process Completed..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cursor.Current = Cursors.Default;
            ResetCtrl();
            SetRights();
            
        }



    }
}
