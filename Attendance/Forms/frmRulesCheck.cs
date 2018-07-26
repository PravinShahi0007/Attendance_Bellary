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

namespace Attendance.Forms
{
    public partial class frmRulesCheck : Form
    {
        
        public string GRights = "XXXV";
        private DataSet LeaveRules = new DataSet();

        public frmRulesCheck()
        {
            InitializeComponent();
        }

        private void frmMastUnit_Load(object sender, EventArgs e)
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

            btnProcess.Enabled = true;
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
                 question = "Are You Sure to Process/Post Leave Rules For : " + sWrkGrp + Environment.NewLine
                    + "Processed Data will be deleted between '" + txtFromDt.DateTime.ToString("yyyy-MM-dd") + "' And '" + txtToDate.DateTime.ToString("yyyy-MM-dd") + "' ";


                 sql = "Select CompCode,WrkGrp,EmpUnqID,OTFlg,WeekOff from mastemp where CompCode = '01' and Wrkgrp = '" + sWrkGrp + "' And Active = 1  Order By EmpUnqID";
    
            }else{


                question = "Are You Sure to to Process/Post Leave Rules For : " + txtEmpUnqID.Text.Trim().ToString() + Environment.NewLine
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


            LeaveRules = Utils.Helper.GetData("Select Rules,RuleRepl from AttdRules Where WrkGrp ='" + txtWrkGrpCode.Text.Trim() + "'  order by RuleID", Utils.Helper.constr);

            bool hasRows = LeaveRules.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (!hasRows)
            {
                MessageBox.Show("There are no Rules Defined...,Process Canceled", "Information", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            btnProcess.Enabled = false;
            txtWrkGrpCode.Enabled = false;
            txtEmpUnqID.Enabled = false;
            txtFromDt.Enabled = false;
            txtToDate.Enabled = false;

            string sFromDt = txtFromDt.DateTime.ToString("yyyy-MM-dd");
            string sToDt = txtToDate.DateTime.ToString("yyyy-MM-dd");

            DateTime dFromDt = Convert.ToDateTime(sFromDt);
            DateTime dToDt = Convert.ToDateTime(sToDt);

            Cursor.Current = Cursors.WaitCursor;

            DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {

                #region Log_Process
                using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                {

                    try
                    {
                        cn.Open();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ResetCtrl();
                        SetRights();
                        return;
                    }

                    SqlTransaction tr = cn.BeginTransaction();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.Transaction = tr;

                    if (txtEmpUnqID.Text.Trim() != "")
                    {
                        sql = "Insert into ProcessLog (AddDt,AddId,ProcessType,FromDt,ToDt,EmpUnqID ) Values (" +
                             " GetDate(),'" + Utils.User.GUserID + "','LeaveRule','" + sFromDt + "'," +
                             " '" + sToDt + "','" + txtEmpUnqID.Text.Trim() + "')";
                    }
                    else
                    {
                        sql = "Insert into ProcessLog (AddDt,AddId,ProcessType,FromDt,ToDt,WrkGrp ) Values (" +
                            " GetDate(),'" + Utils.User.GUserID + "','LeaveRule','" + sFromDt + "'," +
                            " '" + sToDt + "','" + txtWrkGrpCode.Text.Trim() + "')";
                    }

                    try
                    {
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ResetCtrl();
                        SetRights();
                        return;
                    }

                    try
                    {
                        tr.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tr.Rollback();
                        ResetCtrl();
                        SetRights();

                    }
                    
                }//using connection
                
                #endregion
                
                pBar.Properties.Maximum = ds.Tables[0].Rows.Count + 1;
                
                foreach (DataRow drs in ds.Tables[0].Rows)
                {
                    //update progressbar
                    pBar.PerformStep();
                    pBar.Update();
                    string  tEmpUnqID = drs["EmpUnqID"].ToString();
                    string WeekOff = drs["WeekOff"].ToString();
                    txtError.Text = "Processing Emp :" + sEmpUnqID + Environment.NewLine;
                    //save in db for accountibility
                    using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                    {
                        
                        try
                        {
                            cn.Open();
                        }
                        catch (Exception ex)
                        {
                            txtError.Text += "Processing Emp :" + tEmpUnqID + " " + ex.ToString() + Environment.NewLine;
                            continue;
                        }

                        SqlTransaction tr = cn.BeginTransaction("LeaveRulesCheck");
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = cn;
                        cmd.Transaction = tr;

                        
                        sql = "SELECT '" + tEmpUnqID + "' AS EmpUnqID, F_TABLE_DATE_1.Date , " +
                             " dbo.AttdData.LeaveTyp, ISNULL(dbo.AttdData.Status, 'A') AS Status " +
                             " FROM dbo.F_TABLE_DATE('" + sFromDt + "','" + sToDt + "') AS F_TABLE_DATE_1 LEFT OUTER JOIN " +
                             " dbo.AttdData ON F_TABLE_DATE_1.Date = dbo.AttdData.tDate AND '" + tEmpUnqID + "' = dbo.AttdData.EmpUnqID Order By F_TABLE_DATE_1.Date  ";

                        DataSet dsdaily = Utils.Helper.GetData(sql, Utils.Helper.constr);
                        hasRows = dsdaily.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                        if (!hasRows)
                        {
                            continue;
                        }

                        string lstr = string.Empty;

                        foreach (DataRow tdr in dsdaily.Tables[0].Rows)
                        {
                            string LeaveTyp = tdr["LeaveTyp"].ToString();
                            
                            if (LeaveTyp != "WO" && LeaveTyp != "HL" && LeaveTyp != "LW" && LeaveTyp != "AB" && LeaveTyp != "SP")
                            {
                                lstr += tdr["Status"].ToString() + ";" ;
                            }else
                            {
                                lstr += tdr["LeaveTyp"].ToString() + ";" ;
                            }
                                
                        }//foreach day status 

                        if (string.IsNullOrEmpty(lstr))
                        {
                            continue;
                        }

                        lstr = lstr.Substring(0, lstr.Length - 1);

                        foreach (DataRow tmp in LeaveRules.Tables[0].Rows)
                        {
                            
                            lstr = lstr.Replace(tmp["Rules"].ToString(),tmp["RuleRepl"].ToString());

                            //lstr = Replace(lstr, ruleary(i, 0), ruleary(i, 1))
                           
                        }
                        string[] dtary = lstr.Split(';');

                        DateTime tmpDt = dFromDt;

                        foreach (string d in dtary)
                        {
                            if (d == "A")
                            {
                                sql = "Update AttdData Set LeaveTyp = Null,Status = 'A' ,Rules = 1 Where EmpUnqID = '" + tEmpUnqID + "' " +
                                " And tDate ='" + tmpDt.ToString("yyyy-MM-dd") + "' and ConsIn is null and ConsOut is null";

                                try
                                {
                                    cmd.CommandText = sql;
                                    cmd.ExecuteNonQuery();
                                    
                                }
                                catch (Exception ex)
                                {
                                    
                                    txtError.Text += "Error while processing : " + tEmpUnqID + " " + ex.ToString() + Environment.NewLine;
                                    
                                }
                                
                            
                            }

                            tmpDt = tmpDt.AddDays(1);
                        }

                        try
                        {
                            tr.Commit();
                        }
                        catch (Exception ex)
                        {
                            tr.Rollback();
                        }

                    }//using connection

                } //foreach loop drs
            }
            MessageBox.Show("Process Completed...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cursor.Current = Cursors.Default;
            ResetCtrl();
            SetRights();
            
        }



    }
}
