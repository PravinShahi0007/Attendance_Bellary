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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Attendance.Classes;
using DevExpress.XtraGrid.Columns;
using System.Globalization;

namespace Attendance.Forms
{
    public partial class frmLeaveEntry : Form
    {
        private string GRights = "XXXV";
        private static int MeFormID ;//'get formid
        private clsEmp Emp = new clsEmp();

        public frmLeaveEntry()
        {
            InitializeComponent();
            this.ctrlEmp1.EmpUnqIDValidated += new EventHandler(this.ctrlEmpValidateEvent_Handler);
            //this.ctrlEmp1.CompCodeValidated += new EventHandler(this.ctrlCompValidateEvent_Handler);
            
        }

        private void ctrlEmpValidateEvent_Handler(object sender, EventArgs e)
        {
            txtLeaveTyp.Properties.Items.Clear();

            if (!ctrlEmp1.cEmp.Active)
            {
                //grid.DataSource = null;
                Emp = new clsEmp();
                ResetCtrl();
            }
            else
            {
                Emp = ctrlEmp1.cEmp;
                Emp.CompCode = Emp.CompCode;
                Emp.EmpUnqID = Emp.EmpUnqID;
                Emp.GetEmpDetails(Emp.CompCode, Emp.EmpUnqID);

                //'added on 27/06/2016 using new security module
                
                if (!Globals.GetWrkGrpRights(MeFormID, Emp.WrkGrp, Emp.EmpUnqID))
                {
                    Emp = new clsEmp();
                    MessageBox.Show("You are not Authorised,Please Contact System Administrator","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                
                 //load leave types

                string sql = "Select LeaveTyp From MastLeave " +
                     " Where CompCode='" + Emp.CompCode + "' " +
                     " And WrkGrp ='" + Emp.WrkGrp + "' " +
                     " And ShowLeaveEntry = 1 " +
                     " Order By ShowGridBalSeq";
       
                
                DataSet ds = Utils.Helper.GetData(sql,Utils.Helper.constr);
                bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                if (hasRows)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        txtLeaveTyp.Properties.Items.Add(dr["LeaveTyp"].ToString());
                    }
                }

                LoadGrid();
                SetRights();

                LoadLeaveBalGrid();
                txtFromDt.Focus();
            } 
        }

        private void frmSanction_Load(object sender, EventArgs e)
        {
            ResetCtrl();

            GRights = Attendance.Classes.Globals.GetFormRights(this.Name);
           
            MeFormID = Convert.ToInt32("0" + Utils.Helper.GetDescription("Select FormID from MastFrm Where FormName = 'frmLeaveEntry'", Utils.Helper.constr));

            txtLeaveTyp.Properties.Items.Clear();

            txtFromDt.DateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-") + "01");
            txtFromDt2.DateTime = txtFromDt.DateTime;
            txtToDt.DateTime = txtFromDt.DateTime.AddDays(1);

           
        }

        private string DataValidate()
        {
            string err = string.Empty;

            if (string.IsNullOrEmpty(ctrlEmp1.txtCompCode.Text.Trim().ToString()))
            {
                err = err + "Please Enter CompCode..." + Environment.NewLine;
            }
            
            if (string.IsNullOrEmpty(ctrlEmp1.txtEmpUnqID.Text.Trim().ToString()))
            {
                err = err + "Please Enter EmpUnqID..." + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(ctrlEmp1.cEmp.EmpUnqID) && !ctrlEmp1.IsValid )
            {
                err = err + "Invalid/InActive EmpUnqID..." + Environment.NewLine;
            }


            if (string.IsNullOrEmpty(ctrlEmp1.cEmp.CompDesc.Trim().ToString()))
            {
                err = err + "Invalid CompCode..." + Environment.NewLine;
            }

            if (txtFromDt.DateTime == null || txtFromDt.DateTime == DateTime.MinValue)
            {
                err = err + "Please Select From Date..." + Environment.NewLine;
            }

            if (txtToDt.DateTime == null || txtToDt.DateTime == DateTime.MinValue)
            {
                err = err + "Please Select To Date..." + Environment.NewLine;
            }

            if (txtToDt.DateTime < txtFromDt.DateTime)
            {
                err = err + "Invalid Date Range..." + Environment.NewLine;
            }


            return err;
        }

        private void ResetCtrl()
        {
            ctrlEmp1.ResetCtrl();
            
            
            txtTotDays.Value = 0;
            txtWODays.Value = 0;
            txtHolidays.Value = 0;
            txtLeaveDays.Value = 0;

            txtLeaveTyp.Text = "";
            txtRemarks.Text = "";

            btnSanction.Enabled = false;
            btnDel_Leave.Enabled = false;
            btnDel_SanLeave.Enabled = false;

            ResetGrid();

        }
        
        private void SetRights()
        {
            btnSanction.Enabled = false;
            btnDel_Leave.Enabled = false;
            btnDel_SanLeave.Enabled = false;


            if (GRights.Contains("A"))
            {
                //check workgrp rights
                if (Globals.GetWrkGrpRights(280, Emp.WrkGrp, Emp.EmpUnqID))
                {
                    btnSanction.Enabled = true;
                } 
            }
           
            if(GRights.Contains("U") || GRights.Contains("D"))
            {
                //check workgrp rights
                if (Globals.GetWrkGrpRights(280, Emp.WrkGrp, Emp.EmpUnqID))
                {

                    btnDel_Leave.Enabled = true;
                    btnDel_SanLeave.Enabled = true;
                }
            }
                        
        }

        private void LoadGrid()
        {

            #region Chk_Primary

            if (Emp.EmpUnqID == string.Empty)
            {
                ResetGrid();
                return;
            }

            if (Emp.Active == false)
            {
                ResetGrid();
                return;
            }

            #endregion

            string SqlAttd = string.Empty;
            string SqlPunch = string.Empty;
            string SqlSanc = string.Empty;
            string SqlLeave = string.Empty;


            DateTime FromDt = new DateTime();
            DateTime ToDt = new DateTime();


            if (txtFromDt.DateTime == DateTime.MinValue)
            {
                FromDt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-") + "01");
            }
            else
            {
                FromDt = txtFromDt.DateTime;
            }

            if (txtToDt.DateTime == DateTime.MinValue)
            {
                ToDt = FromDt.AddMonths(1);
            }
            else
            {
                ToDt = txtToDt.DateTime;
            }


            SqlAttd = "Select Top 40 " +
                    " tDate , upper(left(datename(dw, tdate),3)) as Day, ScheduleShift as SchShift,ConsShift, ConsIn, ConsOut, Status,ConsOverTime as OT, ConsWrkHrs as WrkHrs,  HalfDay,LeaveTyp,LeaveHalf,LateCome,EarlyGoing,EarlyCome " +
                    " ,GracePeriod From AttdData " +
                    " Where EmpUnqId ='" + Emp.EmpUnqID + "' And tDate >= '" + txtFromDt2.DateTime.ToString("yyyy-MM-dd") + "' And CompCode = '01' And WrkGrp = '" + Emp.WrkGrp + "' Order By tDate" ;
    
            SqlSanc = "Select Top 40 " +
                      " SanID,tDate,ConsInTime,ConsOutTime,ConsOverTime,ConsShift,SchLeave,AddID,AddDT,Remarks " +
                      " From MastLeaveSchedule " +
                      " Where EmpUnqId ='" + Emp.EmpUnqID + "' And tDate >= '" + txtFromDt2.DateTime.ToString("yyyy-MM-dd") + "' and WrkGrp = '" + Emp.WrkGrp + "' " +
                      " And isnull(SchLeave,'') <> '' and tYear = '" + FromDt.Year + "' Order By SanID Desc ";

            SqlPunch = "Select Top 100 " +
                      " PunchDate,IOFLG,MachineIP,AddDt,AddID " +
                      " From AttdLog " +
                      " Where EmpUnqId ='" + Emp.EmpUnqID + "' And PunchDate >= '" + txtFromDt2.DateTime.ToString("yyyy-MM-dd") + "' " +
                      " and LunchFlg = 0 and IOFLG in ('I','O') Order by PunchDate";


            SqlLeave = "Select CONVERT(VARCHAR(10), FromDt, 103) as FromDt ,CONVERT(VARCHAR(10), ToDt, 103) as ToDt,LeaveTyp,TotDay,WODay,PublicHL,LeaveDed,LeaveAdv,LeaveHalf,Remark,AddDt,AddID " +
                    " From LeaveEntry Where " +
                    " CompCode ='" + Emp.CompCode + "' " +
                    " And WrkGrp ='" + Emp.WrkGrp + "' " +
                    " And tYear ='" + FromDt.Year + "' " +
                    " And EmpUnqID ='" + Emp.EmpUnqID + "' " +
                    " Order By FromDt Desc ";


            //'Punch Details
            DataSet ds = Utils.Helper.GetData(SqlAttd,Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
            if (hasRows){ grd_Attd.DataSource = ds.Tables[0]; } else { grd_Attd.DataSource = null; }


            ds = Utils.Helper.GetData(SqlPunch, Utils.Helper.constr);
            hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
            if (hasRows) { grd_InOut.DataSource = ds.Tables[0]; } else { grd_InOut.DataSource = null; }


            ds = Utils.Helper.GetData(SqlSanc, Utils.Helper.constr);
            hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
            if (hasRows) { grd_Sanction.DataSource = ds.Tables[0]; } else { grd_Sanction.DataSource = null; }

            ds = Utils.Helper.GetData(SqlLeave, Utils.Helper.constr);
            hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
            if (hasRows) { grd_LeaveList.DataSource = ds.Tables[0]; } else { grd_LeaveList.DataSource = null; }


            GridFormat();

        }

        private void ResetGrid()
        {
            grd_Attd.DataSource = null;
            grd_Sanction.DataSource = null;
            grd_LeaveBal.DataSource = null;
            grd_LeaveList.DataSource = null;
            grd_Sanction.DataSource = null;
            grd_InOut.DataSource = null;
            
        }

        private void GridFormat()
        {
            gv_Attd.Appearance.HeaderPanel.Font = new System.Drawing.Font(gv_Attd.Appearance.ViewCaption.Font, FontStyle.Bold);
            gv_Attd.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
           
            gv_InOut.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv_InOut.Appearance.HeaderPanel.Font = new System.Drawing.Font(gv_Attd.Appearance.ViewCaption.Font, FontStyle.Bold);
            
            gv_Sanction.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv_Sanction.Appearance.HeaderPanel.Font = new System.Drawing.Font(gv_Attd.Appearance.ViewCaption.Font, FontStyle.Bold);

            gv_LeaveList.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv_LeaveList.Appearance.HeaderPanel.Font = new System.Drawing.Font(gv_Attd.Appearance.ViewCaption.Font, FontStyle.Bold);


            GridColumn colDate = new GridColumn();
            
            if (grd_Attd.DataSource != null)
            {
                colDate = gv_Attd.Columns["tDate"];
                colDate.DisplayFormat.Format = new CultureInfo("en");
                colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                colDate.DisplayFormat.FormatString = "dd/MM/yy";
            
                colDate = gv_Attd.Columns["ConsIn"];
                colDate.DisplayFormat.Format = new CultureInfo("en");
                colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                colDate.DisplayFormat.FormatString = "dd/MM/yy HH:mm";
                        
                colDate = gv_Attd.Columns["ConsOut"];
                colDate.DisplayFormat.Format = new CultureInfo("en");
                colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                colDate.DisplayFormat.FormatString = "dd/MM/yy HH:mm";

                foreach (GridColumn gc in gv_Attd.Columns)
                {
                    gc.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }
            }

            if (grd_LeaveList.DataSource != null)
            {
                colDate = gv_LeaveList.Columns["FromDt"];
                colDate.DisplayFormat.Format = new CultureInfo("en");
                colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                colDate.DisplayFormat.FormatString = "dd/MM/yy";

                colDate = gv_LeaveList.Columns["ToDt"];
                colDate.DisplayFormat.Format = new CultureInfo("en");
                colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                colDate.DisplayFormat.FormatString = "dd/MM/yy";

                colDate = gv_LeaveList.Columns["AddDt"];
                colDate.DisplayFormat.Format = new CultureInfo("en");
                colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                colDate.DisplayFormat.FormatString = "dd/MM/yy HH:mm";

                foreach (GridColumn gc in gv_LeaveList.Columns)
                {
                    gc.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }
            }


            if (grd_Sanction.DataSource != null)
            {
                //sanction
                colDate = gv_Sanction.Columns["ConsInTime"];
                colDate.DisplayFormat.Format = new CultureInfo("en");
                colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                colDate.DisplayFormat.FormatString = "dd/MM/yy HH:mm";

                colDate = gv_Sanction.Columns["tDate"];
                colDate.DisplayFormat.Format = new CultureInfo("en");
                colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                colDate.DisplayFormat.FormatString = "dd/MM/yy";

                colDate = gv_Sanction.Columns["ConsOutTime"];
                colDate.DisplayFormat.Format = new CultureInfo("en");
                colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                colDate.DisplayFormat.FormatString = "dd/MM/yy HH:mm";

                foreach (GridColumn gc in gv_Sanction.Columns)
                {
                    gc.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }
            }


            if (grd_InOut.DataSource != null)
            {
                //inout punch
                colDate = gv_InOut.Columns["PunchDate"];
                colDate.DisplayFormat.Format = new CultureInfo("en");
                colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                colDate.DisplayFormat.FormatString = "dd/MM/yy HH:mm";

                colDate = gv_InOut.Columns["AddDt"];
                colDate.DisplayFormat.Format = new CultureInfo("en");
                colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                colDate.DisplayFormat.FormatString = "dd/MM/yy HH:mm";

                foreach (GridColumn gc in gv_InOut.Columns)
                {
                    gc.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }

            }

            

        }

        private void txtFromDt_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void LoadLeaveBalGrid()
        {
            int tYear = 0;

            if (Emp.EmpUnqID == "")
            {
                grd_LeaveBal.DataSource = null;
                return;
            }

            if(txtFromDt.EditValue != null || txtFromDt.DateTime != DateTime.MinValue)
            {
                tYear = txtFromDt.DateTime.Year;
            }
            
            string sql = "Select a.LeaveTyp,a.Opn,a.Avl,(a.OPN-(a.AVL+a.ADV+a.ENC)) as Bal ,a.Adv,a.ENC from leaveBal a,MastLeave b " +
             " where a.CompCode='" + Emp.CompCode + "'" +
             " and a.WrkGrp='" + Emp.WrkGrp  + "'" +
             " and a.EmpUnqID='" + Emp.EmpUnqID + "'" +
             " and a.tYear ='" + tYear.ToString() + "'" +
             " and a.WrkGrp = b.WrkGrp and a.LeaveTyp = b.LeaveTyp " +
             " Order By ShowGridBalSeq" ;

            DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr); 

            bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
            if (hasRows)
            {
                grd_LeaveBal.DataSource = ds.Tables[0];

                gv_LeaveBal.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gv_LeaveBal.Appearance.HeaderPanel.Font = new System.Drawing.Font(gv_Attd.Appearance.ViewCaption.Font, FontStyle.Bold);

                foreach (GridColumn gc in gv_LeaveBal.Columns)
                {
                    gc.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }

            }
            else
            {
                grd_LeaveBal.DataSource = null;
            }
        }

        private void txtLeaveTyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txtLeaveTyp.Text != "" && Emp.EmpUnqID != "" ) 
            {
                 string sql = "Select AllowHalfPosting from MastLeave Where LeaveTyp ='" + txtLeaveTyp.Text.Trim() + "'" +
                    " and CompCode = '" + Emp.CompCode + "' And WrkGrp='" + Emp.WrkGrp +  "'" ;
                 
                 string flg = Utils.Helper.GetDescription(sql,Utils.Helper.constr);

                 if(string.IsNullOrEmpty(flg))
                     flg = "0";

                 if(Convert.ToBoolean(flg))
                 {
                     chkHalf.Checked = false;
                     chkHalf.Visible = true;
                 }
                 else
                 {
                     chkHalf.Checked = false;
                     chkHalf.Visible = false;
                 }

                 if (txtFromDt.DateTime != DateTime.MinValue && txtToDt.DateTime != DateTime.MinValue)
                 {
                     LoadLeaveDetails();
                 }

            }
            else
            {
                chkHalf.Checked = false;
                chkHalf.Visible = false;
            }
 
        }

        private void txtToDt_EditValueChanged(object sender, EventArgs e)
        {
            
            
        }

        /// <summary>
        /// calculate leave days which are deductible
        /// </summary>
        private void LoadLeaveDetails()
        {
            if (Emp.EmpUnqID == "")
            {
                txtWODays.Value = 0;
                txtTotDays.Value = 0;
                txtHolidays.Value = 0;
                txtLeaveDays.Value = 0;
                return;
            }
            
            if( txtFromDt.DateTime == DateTime.MinValue || txtToDt.DateTime == DateTime.MinValue )
            {
                txtWODays.Value = 0;
                txtTotDays.Value = 0;
                txtHolidays.Value = 0;
                txtLeaveDays.Value = 0;
                return;
            }

            if (txtLeaveTyp.Text.Trim() == "")
            {
                txtWODays.Value = 0;
                txtTotDays.Value = 0;
                txtHolidays.Value = 0;
                txtLeaveDays.Value = 0;
                return;
            }

            DateTime FromDt = txtFromDt.DateTime, ToDt = txtToDt.DateTime;
            decimal TotDays = 0, WODayNo = 0, HLDay = 0;
            bool halfflg = false;

            halfflg = this.chkHalf.Checked;

            TimeSpan ts = (txtToDt.DateTime - txtFromDt.DateTime);
            TotDays = ts.Days + 1;


            string WOSql = "Select Count(*) From AttdData where ScheduleShift ='WO' and  tDate between '" + FromDt.ToString("yyyy-MM-dd") + "' And '" + ToDt.ToString("yyyy-MM-dd") + "'" +
            " And EmpUnqID='" + Emp.EmpUnqID + "'" ;

            WODayNo = Convert.ToDecimal(Utils.Helper.GetDescription(WOSql, Utils.Helper.constr));


            string hlsql = "Select tDate from HoliDayMast Where " +
             " CompCode = '" + Emp.CompCode + "' " +
             " And WrkGrp ='" + Emp.WrkGrp + "' " +
             " And tDate between '" + FromDt.ToString("yyyy-MM-dd") + "' and '" + ToDt.ToString("yyyy-MM-dd") + "' ";

             //'check hlDay on WeekOff...
             
            HLDay = 0;
            DataSet ds = Utils.Helper.GetData(hlsql,Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //Get  from AttdData Table , if ScheduleShift = "WO" on Holiday
                    WOSql = "Select ScheduleShift from AttdData Where EmpUnqId ='" + Emp.EmpUnqID + "' and tDate ='" + Convert.ToDateTime(dr["tDate"]).ToString("yyyy-MM-dd") + "'";
                    string WODay = Utils.Helper.GetDescription(WOSql,Utils.Helper.constr);
                    if(WODay == "WO")
                    {
                        WODayNo -= 1;
                    }
                    HLDay += 1;
                }
            }
            
            txtTotDays.Value = TotDays;
            if(txtLeaveTyp.Text.Trim() == "AB" || txtLeaveTyp.Text.Trim() == "LW" || txtLeaveTyp.Text.Trim() == "SP")
            {
                WODayNo = 0;
                HLDay = 0;
            }
             
            txtWODays.Value = WODayNo;
            txtHolidays.Value = HLDay;
    
            if(halfflg)
            {
                txtLeaveDays.Value = (TotDays - (WODayNo + HLDay))/2;
            }else
            {
                txtLeaveDays.Value = (TotDays - (WODayNo + HLDay));
            }

        }

        private void chkHalf_CheckedChanged(object sender, EventArgs e)
        {
            LoadLeaveDetails();
        }

        private void btnSanction_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool KeepAdv = false;
            bool IsBalanced = false;
            bool WoEntReq = false;
            bool IsHalf = false;

            string LeaveTyp = "";
            string sql = "";

            
            decimal LeaveADV = 0;
            decimal LeaveDays = 0;
            decimal WoDaysNo = 0;
            decimal HLDaysNo = 0;
            decimal TotalDays = 0;

            
            DateTime FromDt = txtFromDt.DateTime;
            DateTime ToDt = txtToDt.DateTime;

            TimeSpan ts = (ToDt - FromDt);

            TotalDays = ts.Days + 1;


            #region Chk_AlreadyPosted
            sql = "Select * from LeaveEntry Where " +
           " compcode = '" + Emp.CompCode  + "'" +
           " and WrkGrp ='" + Emp.WrkGrp  + "'" +
           " And tYear ='" + FromDt.Year + "'" +
           " And EmpUnqID='" + Emp.EmpUnqID + "'" +
           " And (     FromDt between '" + FromDt.ToString("yyyy-MM-dd") + "' And '" + ToDt.ToString("yyyy-MM-dd") + "' " +
           "  OR       ToDt Between '" + FromDt.ToString("yyyy-MM-dd") + "'   And '" + ToDt.ToString("yyyy-MM-dd") + "' " +
           "  OR '" + FromDt.ToString("yyyy-MM-dd") + "' Between FromDt And ToDt " +
           "  OR '" + ToDt.ToString("yyyy-MM-dd") + "' Between FromDt And ToDt " +
           "     ) ";

            DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
               DataRow dr = ds.Tables[0].Rows[0];
                
                string MsgStr = "From : " + Convert.ToDateTime(dr["FromDt"]).ToString("yyyy-MM-dd") + " - " + Convert.ToDateTime(dr["ToDt"]).ToString("yyyy-MM-dd") + " Type : " + dr["LeaveTyp"].ToString() + Environment.NewLine;
                MessageBox.Show("There Are Already Some Leave Found,,," + Environment.NewLine + MsgStr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            WoDaysNo = txtWODays.Value;
            HLDaysNo = txtHolidays.Value;
            LeaveTyp = txtLeaveTyp.Text.Trim();

            #region Chk_ValidLeaveTyp
            
            sql = "Select * from MastLeave where " +
               " compcode = '" + Emp.CompCode + "'" +
               " and WrkGrp ='" + Emp.WrkGrp + "'" +
               " and LeaveTyp ='" + LeaveTyp + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                IsBalanced = Convert.ToBoolean(dr["KeepBal"]);
                KeepAdv = Convert.ToBoolean(dr["KeepAdv"]);
            }
            else
            {
                MessageBox.Show("Invalid Leave Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            #endregion

            LeaveDays = txtLeaveDays.Value;
            LeaveADV = 0;

            #region Chk_LeaveBal_Rec
            if (IsBalanced)
            {
               
                sql = "Select * from LeaveBal where " +
                   " compcode = '" + Emp.CompCode + "'" +
                   " and WrkGrp ='" + Emp.WrkGrp + "'" +
                   " and LeaveTyp ='" + LeaveTyp + "'" +
                   " And tYear ='" + FromDt.Year + "'" +
                   " And EmpUnqID='" + Emp.EmpUnqID + "'";
                
                ds = Utils.Helper.GetData(sql,Utils.Helper.constr);
                 hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

                if (!hasRows)
                {
                    MessageBox.Show("Leave Balance Record Not Available...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    decimal opn = Convert.ToDecimal(dr["Opn"]);
                    decimal avl = Convert.ToDecimal(dr["Avl"]);
                    decimal enc = Convert.ToDecimal(dr["Enc"]);

                    if(KeepAdv == false)
                    {
                        
                        if((opn - avl + enc) < txtLeaveDays.Value) 
                        {
                            MessageBox.Show("InSufficient Balance...Current Balance is : " + (opn - avl + enc).ToString() 
                                , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        
                    }
                    else
                    {
                        if((opn - avl + enc) < txtLeaveDays.Value) 
                        {
                            LeaveDays = opn-avl+enc ;
                            LeaveADV = txtLeaveDays.Value - LeaveDays ;
                        }
                        else
                        {
                            LeaveADV = 0;
                        }
                    }                    
                }
            }

            #endregion
            
            #region Warn_ADV_Leave_Posting
            //'--Warn Advance Leave Posting
            if(LeaveADV > 0 )
            {
                DialogResult ans = MessageBox.Show("Are You Sure To Post Advance Leave ?","Question",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if(ans == DialogResult.No)
                {
                    MessageBox.Show("Posting Canceled...","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }                
            }
            #endregion

            #region set_WoEnt_Required

            switch (LeaveTyp)
            {
                case "LW" :
                    WoEntReq = false;
                    break;
                case "AB" :
                    WoEntReq = false;
                    break;
                case "SP" :
                    WoEntReq = false;
                    break;
                default :
                    WoEntReq = true;
                    break;
            }

            #endregion

            #region Chk_HalfDay

            if (chkHalf.Checked)
                IsHalf = true;
            else
                IsHalf = false;
            

            if(IsHalf)
            {
                if(LeaveADV > 0)
                {
                    LeaveADV = LeaveADV/2;
                }
            }

            #endregion

            Cursor.Current = Cursors.WaitCursor;

            #region MainProc
            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    cn.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cursor.Current = Cursors.Default;
                    return;
                }

                SqlTransaction tr = cn.BeginTransaction();
                sql = "Select * from LeaveEntry Where " +
                   " compcode = '" + Emp.CompCode + "'" +
                   " and WrkGrp ='" + Emp.WrkGrp + "'" +
                   " and LeaveTyp ='" + LeaveTyp + "'" +
                   " And tYear ='" + FromDt.Year + "'" +
                   " And EmpUnqID='" + Emp.EmpUnqID + "'" +
                   " And FromDt ='" + FromDt.ToString("yyyy-MM-dd") + "'" +
                   " and ToDt ='" +  ToDt.ToString("yyyy-MM-dd") + "'" ;
                ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
                hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                if (!hasRows)
                {
                    try
                    {
                        string insertsql = "insert into LeaveEntry (CompCode,WrkGrp,tYear,EmpUnqID,FromDt,ToDt," +
                            " LeaveTyp,TotDay,WoDay,PublicHL,LeaveDed,LeaveADV,LeaveHalf,Remark,AddID,AddDt,DelFlg) " +
                            " Values ('" + Emp.CompCode + "','" + Emp.WrkGrp + "','" + FromDt.Year.ToString() + "','" + Emp.EmpUnqID + "','" + FromDt.ToString("yyyy-MM-dd") + "','" + ToDt.ToString("yyyy-MM-dd") + "', " +
                            " '" + LeaveTyp + "','" + TotalDays.ToString() + "','" + WoDaysNo.ToString() + "','" + HLDaysNo.ToString() + "'," +
                            " '" + LeaveDays.ToString() + "','" + LeaveADV.ToString() + "','" + (IsHalf ? 1 : 0) + "','" + txtRemarks.Text.Trim() + "'," +
                            " '" + Utils.User.GUserID + "',GetDate(),0)";

                        SqlCommand cmd = new SqlCommand(insertsql, cn, tr);
                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                }
                //gettting timeout
                SqlCommand cmd1 = new SqlCommand(sql, cn, tr);
                SqlDataReader sdr = cmd1.ExecuteReader();
                if(sdr.HasRows)
                {

                    //close datareader
                    if (sdr != null)
                    {
                        sdr.Close();
                    }
                    
                    
                    string datestr = "Select * from V_LV_USE where CAL_Date between '" + FromDt.ToString("yyyy-MM-dd") + "' and '" + ToDt.ToString("yyyy-MM-dd") + "'";

                    DataSet trsLV = Utils.Helper.GetData(datestr, Utils.Helper.constr);
                    
                     

                    //used for leave days count
                    decimal tmpleavecons  = 0;
                    #region UpdateSchLeave
                    foreach (DataRow trslr in trsLV.Tables[0].Rows)
                    {
                        DateTime CalDt = Convert.ToDateTime(trslr["Cal_Date"]);
                            
                        string sqlSch = "Select * from MastLeaveSchedule Where EmpUnqID ='" + Emp.EmpUnqID + "' And tdate ='" + CalDt.ToString("yyyy-MM-dd") + "'";

                        //create data adapter
                        DataSet dsSchLv = new DataSet();
                        SqlDataAdapter daSchLv = new SqlDataAdapter(new SqlCommand(sqlSch, cn,tr));
                        SqlCommandBuilder cmdbSchLv = new SqlCommandBuilder(daSchLv);
                                                
                        daSchLv.InsertCommand = cmdbSchLv.GetInsertCommand();
                        daSchLv.InsertCommand.Transaction = tr;
                        daSchLv.UpdateCommand = cmdbSchLv.GetUpdateCommand();
                        daSchLv.UpdateCommand.Transaction = tr;
                        daSchLv.DeleteCommand = cmdbSchLv.GetDeleteCommand();
                        daSchLv.DeleteCommand.Transaction = tr;
                        daSchLv.AcceptChangesDuringUpdate = false;

                        daSchLv.Fill(dsSchLv, "MastLeaveSchedule");                        
                        hasRows = dsSchLv.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                        
                        if (hasRows)
                        {
                            
                            try
                            {

                               
                                string sqldel = "Delete From MastLeaveSchedule Where " +
                                " EmpUnqID='" + Emp.EmpUnqID + "' " +
                                " And tDate ='" + CalDt.ToString("yyyy-MM-dd") + "'" +
                                " And SchLeave='" + LeaveTyp + "' and WrkGrp ='" + Emp.WrkGrp + "' AND  ConsInTime is null and ConsOutTime is null " +
                                " And ConsOverTime is null and ConsShift is null ";

                                SqlCommand cmd = new SqlCommand(sqldel, cn, tr);
                                int t = (int)cmd.ExecuteNonQuery();

                                sqldel = "Update MastLeaveSchedule Set SchLeave = null, SchLeaveHalf = 0,SchLeaveAdv = 0 Where " +
                                " EmpUnqID='" + Emp.EmpUnqID + "' " +
                                " And tDate ='" + CalDt.ToString("yyyy-MM-dd") +"'" +
                                " And SchLeave='" + LeaveTyp + "' and WrkGrp ='" + Emp.WrkGrp + "' ";

                                SqlCommand cmd2 = new SqlCommand(sqldel, cn, tr);
                                t = (int)cmd2.ExecuteNonQuery();


                                
                                
                            }
                            catch (Exception ex)
                            {
                                tr.Rollback();
                                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Cursor.Current = Cursors.Default;
                                return;
                            }
                        }
                        
                        dsSchLv.Clear();
                        daSchLv.Fill(dsSchLv, "MastLeaveSchedule");
                        
                        DataRow drSch;
                        hasRows = dsSchLv.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                        if (!hasRows)
                        {
                            try
                            {
                                string tsql3 = "Insert into MastLeaveSchedule (EmpUnqId,WrkGrp,tDate,AddDt,AddID) Values (" +
                                    "'" + Emp.EmpUnqID + "','" + Emp.WrkGrp + "','" + CalDt.ToString("yyyy-MM-dd") + "',GetDate(),'" + Utils.User.GUserID + "')";
                                SqlCommand cmd = new SqlCommand(tsql3, cn, tr);
                                cmd.ExecuteNonQuery();

                                dsSchLv.Clear();
                                daSchLv.Fill(dsSchLv, "MastLeaveSchedule");
                                drSch = dsSchLv.Tables["MastLeaveSchedule"].Rows[0];
                            }
                            catch (Exception ex)
                            {
                                tr.Rollback();
                                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Cursor.Current = Cursors.Default;
                                return;
                            }

                        }
                        else
                        {
                            drSch = dsSchLv.Tables["MastLeaveSchedule"].Rows[0];
                        }
                            
                        string PublicHLTyp = string.Empty;
                        string SchShift = string.Empty;
                        string tsql = string.Empty;

                        tsql = "Select PublicHLTyp from HolidayMast Where WrkGrp ='" + Emp.WrkGrp +  "' and tDate ='" + CalDt.ToString("yyyy-MM-dd") + "'";
                        PublicHLTyp = Utils.Helper.GetDescription(tsql,Utils.Helper.constr);

                        tsql = "Select ScheduleShift from AttdData where  EmpUnqID ='" + Emp.EmpUnqID + "' " +
                            " And CompCode = '" + Emp.CompCode + "' And WrkGrp = '" + Emp.WrkGrp + "' " + 
                            " And tYear = '" + CalDt.Year.ToString() + "' And tDate ='" + CalDt.ToString("yyyy-MM-dd") + "'";
                        SchShift = Utils.Helper.GetDescription(tsql,Utils.Helper.constr);   
                                                       
                        if(WoEntReq == false)
                        {
                            drSch["schLeave"] = LeaveTyp;
                            drSch["AddId"] = Utils.User.GUserID;
                            drSch["AddDt"] = Globals.GetSystemDateTime();
                            drSch["UpdDt"] = Globals.GetSystemDateTime();
                            drSch["UpdId"] = Utils.User.GUserID;
                            tmpleavecons += 1;
                        }
                        else if (WoEntReq && PublicHLTyp != "")
                        {
                            drSch["schLeave"] =  PublicHLTyp;
                            drSch["AddId"] = "HLCal";
                            drSch["AddDt"] = Globals.GetSystemDateTime();
                            drSch["UpdDt"] = Globals.GetSystemDateTime();
                            drSch["UpdId"] = Utils.User.GUserID;
                        }
                        else if (WoEntReq && PublicHLTyp == "" && drSch["SchLeave"].ToString() == "WO" ){
                            drSch["schLeave"] =  "WO";
                            drSch["AddId"] = "ShiftSch";
                            drSch["UpdDt"] = Globals.GetSystemDateTime();
                            drSch["UpdId"] = Utils.User.GUserID;
                        }else if (WoEntReq && PublicHLTyp == "" && SchShift == "WO")
                        {
                            drSch["schLeave"] =  "WO";
                            drSch["AddId"] = "ShiftSch";
                            drSch["UpdDt"] = Globals.GetSystemDateTime();
                            drSch["UpdId"] = Utils.User.GUserID;

                        }
                        else
                        {
                            drSch["schLeave"] =  LeaveTyp;
                            drSch["AddId"] = Utils.User.GUserID;
                            drSch["AddDt"] = Globals.GetSystemDateTime();
                            drSch["UpdDt"] = Globals.GetSystemDateTime();
                            drSch["UpdId"] = Utils.User.GUserID;
                            tmpleavecons += 1;
                        }
                                
                        if( tmpleavecons > LeaveDays) 
                        {
                            drSch["SchLeaveAdv"] = 1;
                        }

                        drSch["SchLeaveHalf"] = (IsHalf ? 1 : 0);                                
                        //dsSchLv.Tables["MastLeaveSchedule"].Rows.Add(drSch);
                        dsSchLv.AcceptChanges();

                        try
                        {
                            string sql2 = "Update MastLeaveSchedule Set AddId ='" + drSch["AddId"].ToString() + "'," +
                                " AddDt='" + Convert.ToDateTime(drSch["AddDt"]).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                " UpdDt={0},UpdID={1}, SchLeave='" + drSch["SchLeave"].ToString() + "'," +
                                " SchLeaveAdv='" + (Convert.ToBoolean(drSch["SchLeaveAdv"]) ? 1 : 0).ToString() + "'," +
                                " SchLeaveHalf ='" + (IsHalf ? 1 : 0).ToString() + "' Where SanId = '" + drSch["SanID"].ToString() + "'";
                            
                            sql2 = string.Format(sql2,
                                ((drSch["UpdDt"] == DBNull.Value)? " null " : "'" + Convert.ToDateTime(drSch["UpdDt"]).ToString("yyyy-MM-dd HH:mm") + "'"),
                                (drSch["UpdID"] == DBNull.Value) ? " null " : "'" + Utils.User.GUserID + "'"
                                );

                            SqlCommand cmd = new SqlCommand(sql2, cn, tr);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            tr.Rollback();
                            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Cursor.Current = Cursors.Default;
                            return;
                        }

                    }//foreach dateloop - trsLV
                    #endregion

                    #region UpdateLeaveBal
                    try
                    {
                        string tsql1 = "Update LeaveBal " +
                            " Set AVL = AVL + '" + LeaveDays.ToString() + "'" +
                            " ,ADV = Adv + '" + LeaveADV.ToString() + "'" +
                            " Where " +
                            " CompCode ='" + Emp.CompCode + "'" +
                            " And WrkGrp='" + Emp.WrkGrp + "'" +
                            " And tYear ='" + FromDt.Year.ToString() + "'" +
                            " And EmpUnqID ='" + Emp.EmpUnqID + "'" +
                            " And LeaveTyp='" + LeaveTyp + "'";

                        SqlCommand cmd2 = new SqlCommand(tsql1, cn, tr);
                        cmd2.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Cursor.Current = Cursors.Default;
                        return;
                    }

                    #endregion
                }

                


                try
                {
                    tr.Commit();
                    #region ProcessData
                    clsProcess pro = new clsProcess();

                    int res = 0;
                    string outerr = string.Empty;
                    pro.AttdProcess(Emp.EmpUnqID, FromDt, ToDt, out res, out outerr);

                    //process lunchinout
                    pro.LunchInOutProcess(Emp.EmpUnqID, FromDt, ToDt, out res);
                    MessageBox.Show("Posting Done..", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    #endregion
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cursor.Current = Cursors.Default;
                    return;
                   
                }
                LoadGrid();
                LoadLeaveBalGrid();
            }//end sqlcon using
            #endregion
           
            Cursor.Current = Cursors.Default;
        }

        private void btnDel_Leave_Click(object sender, EventArgs e)
        {
            if (gv_LeaveList.SelectedRowsCount == 0)
            {
                MessageBox.Show("Please Select a Row First..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            DialogResult dr = MessageBox.Show("Are you sure to Delete selected row ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                return;
            }

            string LeaveTyp = string.Empty;
            string cellFromDt = string.Empty;
            string cellToDt = string.Empty;

            foreach (int i in gv_LeaveList.GetSelectedRows())
            {
               LeaveTyp = gv_LeaveList.GetRowCellValue(i, "LeaveTyp").ToString();
               cellFromDt = gv_LeaveList.GetRowCellValue(i, "FromDt").ToString();
               cellToDt = gv_LeaveList.GetRowCellValue(i, "ToDt").ToString();
            }

            if(LeaveTyp == "" || cellFromDt == "" || cellToDt == ""){

                return;
            }

            string sfromdt = cellFromDt.Substring(0, 10);
            string stodt = cellToDt.Substring(0, 10);

            DateTime FromDt = DateTime.ParseExact(sfromdt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ToDt = DateTime.ParseExact(stodt, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //DateTime FromDt = Convert.ToDateTime(cellFromDt);
            //DateTime ToDt = Convert.ToDateTime(cellToDt);

            Cursor.Current = Cursors.WaitCursor;

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {

                try
                {
                    cn.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SqlTransaction tr = cn.BeginTransaction();

                string sql = "Select * from LeaveEntry where " +
                       "  CompCode='" + Emp.CompCode + "' " +
                       " And WrkGrp='" + Emp.WrkGrp + "' " +
                       " And EmpUnqID ='" + Emp.EmpUnqID + "' " +
                       " And LeaveTyp='" + LeaveTyp + "'" +
                       " And FromDT ='" + FromDt.ToString("yyyy-MM-dd") + "'" +
                       " And ToDt ='" + ToDt.ToString("yyyy-MM-dd") + "'" +
                       " And tYear ='" + FromDt.Year + "'";

                DataSet LeaveDs = Utils.Helper.GetData(sql, Utils.Helper.constr);
                bool hasRows = LeaveDs.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                if (hasRows)
                {
                    
                    
                    #region Delete_LeaveSchedule
                    foreach (DataRow r in LeaveDs.Tables[0].Rows)
                    {
                        
                        try
                        {
                            string tsql = "Delete From MastLeaveSchedule Where " +
                             " EmpUnqID='" + Emp.EmpUnqID + "' " +
                             " And tDate Between '" + FromDt.ToString("yyyy-MM-dd") + "' And '" + ToDt.ToString("yyyy-MM-dd") + "'" +
                             " And SchLeave='" + LeaveTyp + "' and WrkGrp ='" + Emp.WrkGrp + "' AND  ConsInTime is null and ConsOutTime is null " +
                             " And ConsOverTime is null and ConsShift is null ";

                            SqlCommand cmd = new SqlCommand(tsql, cn, tr);
                            cmd.ExecuteNonQuery();

                            tsql = "Update MastLeaveSchedule Set SchLeave = null, SchLeaveHalf = 0,SchLeaveAdv = 0 Where " +
                            " EmpUnqID='" + Emp.EmpUnqID + "' " +
                            " And tDate Between '" + FromDt.ToString("yyyy-MM-dd") + "' And '" + ToDt.ToString("yyyy-MM-dd") + "'" +
                            " And SchLeave='" + LeaveTyp + "' and WrkGrp ='" + Emp.WrkGrp + "' ";

                            SqlCommand cmd2 = new SqlCommand(tsql, cn, tr);
                            cmd2.ExecuteNonQuery();
                            
                            tsql = "Update LeaveBal " +
                            " Set Avl = Avl-" + r["LeaveDed"].ToString() + "," +
                            "     Adv = Adv-" + r["LeaveADV"].ToString() + " " +
                            " Where " +
                            " CompCode='" + Emp.CompCode + "' " +
                            " And WrkGrp='" + Emp.WrkGrp + "' " +
                            " And tYear ='" + r["tYear"].ToString() + "' " +
                            " And EmpUnqID ='" + Emp.EmpUnqID + "' " +
                            " And LeaveTyp='" + LeaveTyp + "'";

                            cmd = new SqlCommand(tsql, cn, tr);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            tr.Rollback();
                            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Cursor.Current = Cursors.Default;
                            return;
                        }
                        
                        
                    }
                    #endregion

                    #region Delete_LeaveEntry

                    
                    try
                    {
                        //delete from leave_entry
                        sql = "Delete from LeaveEntry where " +
                           "  CompCode='" + Emp.CompCode + "' " +
                           " And WrkGrp='" + Emp.WrkGrp + "' " +
                           " And EmpUnqID ='" + Emp.EmpUnqID + "' " +
                           " And LeaveTyp='" + LeaveTyp + "'" +
                           " And FromDT ='" + FromDt.ToString("yyyy-MM-dd") + "'" +
                           " And ToDt ='" + ToDt.ToString("yyyy-MM-dd") + "'";
                        
                        
                        SqlCommand cmd1 = new SqlCommand(sql, cn, tr);
                        cmd1.ExecuteNonQuery();

                        //need to remove all weekoff if there is no sanction of other type.

                        sql = "Delete from MastLeaveSchedule where EmpUnqID ='" + Emp.EmpUnqID + "' " +
                            " and WrkGrp ='" + Emp.WrkGrp + "' and tDate between '" + FromDt.ToString("yyyy-MM-dd") + "' And " +
                            " '" + ToDt.ToString("yyyy-MM-dd") + "' and SchLeave = 'WO'  and WrkGrp ='" + Emp.WrkGrp + "' " +
                            " and ConsInTime is null and ConsOutTime is null And ConsOverTime is null And ConsShift is null And SchShift is null ";

                        cmd1 = new SqlCommand(sql, cn, tr);
                        cmd1.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                    
                    
                    #endregion

                }

                #region ReEnter_WO
                sql = "Select tDate from attddata where ScheDuleShift = 'WO' and tDate between '" + FromDt.ToString("yyyy-MM-dd") + "' and '" + ToDt.ToString("yyyy-MM-dd") + "' and EmpUnqID ='" + Emp.EmpUnqID + "'";
                DataSet ds = Utils.Helper.GetData(sql,Utils.Helper.constr);
                foreach(DataRow r in ds.Tables[0].Rows)
                {
                    DateTime tDate = Convert.ToDateTime(r["tDate"]);
                    
                    try
                    {
                        sql = " Insert into MastLeaveSchedule " +
                                " ( EmpUnqID,WrkGrp,tDate,SchLeave,Adddt,AddId )" +
                                " Values ('" + Emp.EmpUnqID + "','" + Emp.WrkGrp + "','" + tDate.ToString("yyyy-MM-dd") + "','WO',GetDate(),'ShiftSch')";

                        SqlCommand cmd = new SqlCommand(sql, cn, tr);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Cursor.Current = Cursors.Default;
                        return;
                    }

                }

                tr.Commit();

                #endregion

                #region Add_Update_HL

               

                sql = " Select '" + Emp.EmpUnqID + "' as EmpUnqID ,WrkGrp,tDate,PublicHLTyp,GetDate() as AddDt,'HLCal' as AddId " +
                 " From HoliDayMast " +
                 " Where WrkGrp='" + Emp.WrkGrp + "' and tDate between '" + FromDt.ToString("yyyy-MM-dd") + "' and '" + ToDt.ToString("yyyy-MM-dd") + "'";

                ds = Utils.Helper.GetData(sql,Utils.Helper.constr);
                foreach(DataRow r in ds.Tables[0].Rows)
                {
                    
                   

                    DateTime tDate = Convert.ToDateTime(r["tDate"]);

                    sql = "Select Count(*) from MastLeaveSchedule Where EmpUnqID ='" + Emp.EmpUnqID + "' And " +
                            " tDate = '" + tDate.ToString("yyyy-MM-dd") + "' And isnull(SchLeave,'') <> '' and WrkGrp = '" + Emp.WrkGrp + "' and tYear ='" + tDate.Year + "'" ;
                    string err = string.Empty;
                    int t = Convert.ToInt32(Utils.Helper.GetDescription(sql, Utils.Helper.constr,out err));

                    if (t == 0)
                    {
                        sql = " Insert into MastLeaveSchedule " +
                                " ( EmpUnqID,WrkGrp,tDate,SchLeave,Adddt,AddId )" +
                                " Values ('" + Emp.EmpUnqID + "','" + Emp.WrkGrp + "','" + tDate.ToString("yyyy-MM-dd") + "','" + r["PublicHLTyp"].ToString() + "','GetDate()','HLCal')";

                        SqlCommand cmd = new SqlCommand(sql, cn);
                        cmd.ExecuteNonQuery();

                    }
                    else
                    {
                        sql = " Update MastLeaveSchedule " +
                               " Set SchLeave ='" + r["PublicHLTyp"].ToString() + "', Upddt = GetDate(),UpdId = 'HLCal' Where " +
                               " EmpUnqID = '" + Emp.EmpUnqID + "' And WrkGrp = '" + Emp.WrkGrp + "' And tDate = '" + tDate.ToString("yyyy-MM-dd") + "' and tYear ='" + tDate.Year + "'";

                        SqlCommand cmd = new SqlCommand(sql, cn);
                        cmd.ExecuteNonQuery();
                    }

                }

                #endregion

                #region Commit_ProcessData
                
                try
                {

                    //tr.Commit();

                    #region ProcessData
                    clsProcess pro = new clsProcess();

                    int res = 0;
                    string outerr = string.Empty;
                    pro.AttdProcess(Emp.EmpUnqID, FromDt, ToDt, out res, out outerr);

                    //process lunchinout
                    pro.LunchInOutProcess(Emp.EmpUnqID, FromDt, ToDt, out res);

                    #endregion

                    MessageBox.Show("Deleted..", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    //tr.Rollback();
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cursor.Current = Cursors.Default;
                    return;

                }

                LoadGrid();
                LoadLeaveBalGrid();


                #endregion


            }//using sqlconnection

            Cursor.Current = Cursors.Default;
        }

        private void btnDel_SanLeave_Click(object sender, EventArgs e)
        {
            if (gv_Sanction.SelectedRowsCount == 0)
            {
                MessageBox.Show("Please Select a Row First..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            DialogResult dr = MessageBox.Show("Are you sure to Delete selected row ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                return;
            }

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    cn.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SqlTransaction tr = cn.BeginTransaction();

                string sanid = string.Empty;
                DateTime tDate = new DateTime() ;
                string leavtyp = string.Empty;
                foreach (int i in gv_Sanction.GetSelectedRows())
                {
                    sanid = gv_Sanction.GetRowCellValue(i, "SanID").ToString();
                    tDate = Convert.ToDateTime(gv_Sanction.GetRowCellValue(i, "tDate").ToString());
                    leavtyp = gv_Sanction.GetRowCellValue(i, "SchLeave").ToString().Trim();

                    //string sql = "Delete From MastLeaveSchedule where SanID = '" + sanid + "'";
                    try
                    {
                        string sql = "Delete From MastLeaveSchedule where SanID = '" + sanid.ToString() + "' and SchLeave is not null " +
                            " and ConsInTime is not null  and ConsOutTime is not null " +
                            " and ConsShift is not null and ConsOverTime is not null " +
                            " and ConsShift is not null and SchShift is not null and SchLeave ='" + leavtyp + "'" ;
                                
                                        
                        SqlCommand cmd = new SqlCommand(sql, cn,tr);
                        cmd.ExecuteNonQuery();

                        sql = "Update MastLeaveSchedule set SchLeave = null , SchLeaveHalf = 0  where SanID = '" + sanid.ToString() + "' and SchLeave ='" + leavtyp + "'";
                        SqlCommand cmd2 = new SqlCommand(sql, cn,tr);
                        cmd2.ExecuteNonQuery();

                        //SqlCommand cmd2 = new SqlCommand(sql, cn, tr);
                        //cmd2.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                }

                try
                {
                    tr.Commit();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                #region ProcessData
                if (tDate != DateTime.MinValue)
                {
                    clsProcess pro = new clsProcess();

                    int res = 0;
                    string outerr = string.Empty;
                    pro.AttdProcess(Emp.EmpUnqID, tDate.AddDays(-1), tDate.AddDays(1), out res, out outerr);

                    //process lunchinout
                    pro.LunchInOutProcess(Emp.EmpUnqID, tDate, tDate, out res);

                    MessageBox.Show("Deleted..", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadGrid();
                LoadLeaveBalGrid();

                #endregion


            }
        }

        private void frmLeaveEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter))
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void txtFromDt2_Validated(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void txtFromDt_Validated(object sender, EventArgs e)
        {
            txtToDt.EditValue = null;
            txtToDt.Properties.MinValue = txtFromDt.DateTime;
            LoadLeaveBalGrid();
            LoadLeaveDetails();
            LoadGrid();
        }

        private void txtToDt_Validated(object sender, EventArgs e)
        {
            LoadLeaveDetails();
            LoadGrid();
        }

    }
}
