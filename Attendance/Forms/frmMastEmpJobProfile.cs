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

namespace Attendance.Forms
{
    public partial class frmMastEmpJobProfile : Form
    {
        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";
        public static clsEmp Emp = new clsEmp();


        public frmMastEmpJobProfile()
        {
            InitializeComponent();
            this.ctrlEmp1.EmpUnqIDValidated += new EventHandler(this.ctrlEmpValidateEvent_Handler);
            //this.ctrlEmp1.CompCodeValidated += new EventHandler(this.ctrlCompValidateEvent_Handler);
        }

        private void ctrlEmpValidateEvent_Handler(object sender, EventArgs e)
        {
                Emp.CompCode = ctrlEmp1.txtCompCode.Text.Trim();
                Emp.EmpUnqID = ctrlEmp1.txtEmpUnqID.Text.Trim();

                if (Emp.GetEmpDetails(Emp.CompCode, Emp.EmpUnqID))
                {
                   
                    mode = "OLD";
                }
                else
                {
                    mode = "NEW";
                    Emp = new clsEmp();
                }
                DisplayData(Emp);
                SetRights();
            
        }

        //private void ctrlCompValidateEvent_Handler(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(ctrlEmp1.txtCompCode.Text.Trim()))
        //        return;
            

        //}

        private void frmMastEmpJobProfile_Load(object sender, EventArgs e)
        {
            ResetCtrl();
            GRights = Attendance.Classes.Globals.GetFormRights(this.Name);
            SetRights();
                       
        }

        private string DataValidate()
        {
            string err = string.Empty;

            if (string.IsNullOrEmpty(ctrlEmp1.txtCompCode.Text.Trim().ToString()))
            {
                err = err + "Please Enter CompCode..." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(ctrlEmp1.cEmp.CompDesc.Trim().ToString()))
            {
                err = err + "Invalid CompCode..." + Environment.NewLine;
            }

            
            if (string.IsNullOrEmpty(ctrlEmp1.txtEmpUnqID.Text.Trim().ToString()))
            {
                err = err + "Please Enter EmpUnqID..." + Environment.NewLine;
            }

            if (!Globals.GetWrkGrpRights(685, "", ctrlEmp1.cEmp.EmpUnqID))
            {
                if (!string.IsNullOrEmpty(ctrlEmp1.cEmp.EmpUnqID) && !ctrlEmp1.IsValid)
                {
                    err = err + "Invalid/InActive EmpUnqID..." + Environment.NewLine;
                }
            }

            if (Emp.UnitCode == "")
            {
                err = err + "Unit Code is required../Please update from EmpBasicData module..." + Environment.NewLine;
            }

            

            return err;
        }

        private void ResetCtrl()
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            ctrlEmp1.ResetCtrl();

            txtCatCode.Text = string.Empty;
            txtCatDesc.Text = string.Empty;
            txtEmpTypeCode.Text = string.Empty;
            txtEmpTypeDesc.Text = string.Empty;
            txtDeptCode.Text = string.Empty;
            txtDeptDesc.Text = string.Empty;
            txtStatCode.Text = string.Empty;
            txtStatDesc.Text = string.Empty;
            txtDesgCode.Text = string.Empty;
            txtDesgDesc.Text = string.Empty;
            txtGradeCode.Text = string.Empty;
            txtGradeDesc.Text = string.Empty;
            txtContCode.Text = "";
            txtContDesc.Text = "";
            txtSecCode.Text = "";
            txtSecDesc.Text = "";


            txtESINo.Text = "";

            txtEmpCode.Text = "";
            txtOldEmpCode.Text = "";
            txtSAPID.Text = "";
            txtWeekOff.Text = "";

            chkAutoShift.Checked = false;
            chkOTFlg.Checked = false;
            chkIsHOD.Checked = false;
            txtLeftDt.EditValue = null;
            txtLeftDt.Enabled = true;


            oldCode = "";
            mode = "NEW";
            ctrlEmp1.txtEmpUnqID.Focus();
        }
        
        private void SetRights()
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            if (ctrlEmp1.txtEmpUnqID.Text.Trim() != "" )
            {
                //btnAdd.Enabled = false;
                if(string.IsNullOrEmpty(Emp.DeptCode) && GRights.Contains("A"))
                {
                    btnAdd.Enabled = true;
                    
                }
                
                if(GRights.Contains("U"))
                    btnUpdate.Enabled = true;
                if (GRights.Contains("D"))
                    btnDelete.Enabled = true;
            }

            if (GRights.Contains("XXXV"))
            {
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            btnUpdate_Click(sender, e);              
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (!chkAutoShift.Checked && txtShiftCode.Text.Trim() == "")
            {
                err = err + "ShiftCode Required...." + Environment.NewLine;
            }


            GrpMain.Enabled = false;

            Cursor.Current = Cursors.WaitCursor;

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        string sql = "insert into MastEmpHistory " +
                           " select 'Before Update Master Data, Action By " + Utils.User.GUserID + "', GetDate(), * from MastEmp where CompCode = '" + ctrlEmp1.txtCompCode.Text.Trim() + "' " +
                           " and EmpUnqID ='" + ctrlEmp1.txtEmpUnqID.Text.Trim() + "'";

                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();


                        //clsEmp t = new clsEmp();
                        //t.CompCode = txtCompCode.Text.Trim();
                        //t.EmpUnqID = txtEmpUnqID.Text.Trim();
                        //t.GetEmpDetails(t.CompCode, t.EmpUnqID);

                        ////WrkGrp is Changed.. need to process in all tables..
                        //if (t.WrkGrp != txtWrkGrpCode.Text.Trim())
                        //{

                        //    try
                        //    {
                        //        sql = "Exec ChangeWrkGrp '" + t.EmpUnqID + "','" + txtWrkGrpCode.Text.Trim() + "';";
                        //        cmd.CommandText = sql;
                        //        cmd.ExecuteNonQuery();
                        //        WrkGrpChange = true;
                        //    }
                        //    catch (Exception ex)
                        //    {

                        //        WrkGrpChange = false;
                        //        GrpMain.Enabled = true;
                        //        Cursor.Current = Cursors.Default;
                        //        MessageBox.Show("Kindly Clear the Job Profile First,(EmpTypeCode,CatCode,GradeCode,DesgCode,DeptCode,StatCode)" + Environment.NewLine +
                        //           "and try again..."
                        //       , "WrkGrp Change Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        //        return;
                        //    }


                        //}
                        //else
                        //{
                        //    WrkGrpChange = false;
                        //}

                        sql = "Update MastEmp set OTFLG='{0}',Weekoff='{1}', EmpCode='{2}',OldEmpCode='{3}',SAPID='{4}',ShiftType='{5}'," +
                            " LeftDt = {6}  , EmpTypeCode = {7} , CatCode = {8} , DeptCode = {9} , StatCode = {10} , DesgCode = {11} , " +
                            " GradCode = {12} , ContCode = {13} , ShiftCode = {14} , Active = '{15}', " +
                            " UpdDt=GetDate(),UpdID ='{16}',isHOD = '{17}', SecCode = {18} , ESINo= '{19}' Where " +
                            " CompCode ='{20}' and EmpUnqID = '{21}'";


                        sql = string.Format(sql, ((chkOTFlg.Checked) ? 1 : 0), txtWeekOff.Text.Trim(),
                            txtEmpCode.Text.Trim(),txtOldEmpCode.Text.Trim(),txtSAPID.Text.Trim(),((chkAutoShift.Checked) ? 1 : 0),
                            ((txtLeftDt.EditValue == null)? "null" : "'"+ txtLeftDt.DateTime.ToString("yyyy-MM-dd") + "'"),
                            ((txtEmpTypeCode.Text.Trim()== "")? "null" : "'"+ txtEmpTypeCode.Text.Trim() + "'"),
                            ((txtCatCode.Text.Trim()== "")? "null" : "'"+ txtCatCode.Text.Trim() + "'"),
                            ((txtDeptCode.Text.Trim()== "")? "null" : "'"+ txtDeptCode.Text.Trim() + "'"),
                            ((txtStatCode.Text.Trim() == "") ? "null" : "'" + txtStatCode.Text.Trim() + "'"),
                            ((txtDesgCode.Text.Trim() == "") ? "null" : "'" + txtDesgCode.Text.Trim() + "'"),
                            ((txtGradeCode.Text.Trim() == "") ? "null" : "'" + txtGradeCode.Text.Trim() + "'"),
                            ((txtContCode.Text.Trim() == "") ? "null" : "'" + txtContCode.Text.Trim() + "'"),
                            ((txtShiftCode.Text.Trim() == "") ? "null" : "'" + txtShiftCode.Text.Trim() + "'"),
                            ((txtLeftDt.EditValue == null)?1:0),
                            Utils.User.GUserID,((chkIsHOD.Checked) ? 1 : 0),
                            ((txtSecCode.Text.Trim() == "") ? "null" : "'" + txtSecCode.Text.Trim() + "'"), txtESINo.Text.Trim(),
                            ctrlEmp1.txtCompCode.Text.Trim(), ctrlEmp1.txtEmpUnqID.Text.Trim()
                            );

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();


                        //if (WrkGrpChange)
                        //{
                        //    MessageBox.Show("Employee Job Profile is Discarded.." + Environment.NewLine +
                        //            "Please Fill the Employee Job Profile Again.."
                        //        , "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}

                        MessageBox.Show("Record Updated...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCtrl();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            GrpMain.Enabled = true;

            Cursor.Current = Cursors.Default;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            txtEmpCode.Text = "";
            txtOldEmpCode.Text = "";
            txtSAPID.Text = "";

            txtContCode.Text = "";
            txtContCode_Validated(sender, e);

            txtDeptCode.Text = "";
            txtDeptCode_Validated(sender, e);

            txtStatCode.Text = "";
            txtStatCode_Validated(sender, e);

            txtDesgCode.Text = "";
            txtDesgCode_Validated(sender, e);

            txtGradeCode.Text = "";
            txtGradeCode_Validated(sender, e);

            txtCatCode.Text = "";
            txtCatCode_Validated(sender, e);

            txtEmpTypeCode.Text = "";
            txtEmpTypeCode_Validated(sender, e);

            chkOTFlg.Checked = false;
            chkAutoShift.Checked = true;
            txtShiftCode.Text = "";
            txtShiftCode_Validated(sender, e);

            txtWeekOff.Text = "SUN";
            txtESINo.Text = "";


            string err = DataValidate();

            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            GrpMain.Enabled = false;

            Cursor.Current = Cursors.WaitCursor;

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        string sql = "insert into MastEmpHistory " +
                           " select 'Before Delete Master Data, Action By " + Utils.User.GUserID + "', GetDate(), * from MastEmp where CompCode = '" + ctrlEmp1.txtCompCode.Text.Trim() + "' " +
                           " and EmpUnqID ='" + ctrlEmp1.txtEmpUnqID.Text.Trim() + "'";

                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();


                        //clsEmp t = new clsEmp();
                        //t.CompCode = txtCompCode.Text.Trim();
                        //t.EmpUnqID = txtEmpUnqID.Text.Trim();
                        //t.GetEmpDetails(t.CompCode, t.EmpUnqID);

                        ////WrkGrp is Changed.. need to process in all tables..
                        //if (t.WrkGrp != txtWrkGrpCode.Text.Trim())
                        //{

                        //    try
                        //    {
                        //        sql = "Exec ChangeWrkGrp '" + t.EmpUnqID + "','" + txtWrkGrpCode.Text.Trim() + "';";
                        //        cmd.CommandText = sql;
                        //        cmd.ExecuteNonQuery();
                        //        WrkGrpChange = true;
                        //    }
                        //    catch (Exception ex)
                        //    {

                        //        WrkGrpChange = false;
                        //        GrpMain.Enabled = true;
                        //        Cursor.Current = Cursors.Default;
                        //        MessageBox.Show("Kindly Clear the Job Profile First,(EmpTypeCode,CatCode,GradeCode,DesgCode,DeptCode,StatCode)" + Environment.NewLine +
                        //           "and try again..."
                        //       , "WrkGrp Change Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        //        return;
                        //    }


                        //}
                        //else
                        //{
                        //    WrkGrpChange = false;
                        //}

                        sql = "Update MastEmp set OTFLG='{0}',Weekoff='{1}', EmpCode='{2}',OldEmpCode='{3}',SAPID='{4}',ShiftType='{5}'," +
                            " LeftDt = {6}  , EmpTypeCode = {7} , CatCode = {8} , DeptCode = {9} , StatCode = {10} , DesgCode = {11} , " +
                            " GradCode = {12} , ContCode = {13} , ShiftCode = {14} , Active = '{15}', " +
                            " UpdDt=GetDate(),UpdID ='{16}' , ESINO = '{17}' Where " +
                            " CompCode ='{18}' and EmpUnqID = '{19}'";


                        sql = string.Format(sql, ((chkOTFlg.Checked) ? 1 : 0), txtWeekOff.Text.Trim(),
                            txtEmpCode.Text.Trim(), txtOldEmpCode.Text.Trim(), txtSAPID.Text.Trim(), ((chkAutoShift.Checked) ? 1 : 0),
                            ((txtLeftDt.EditValue == null) ? "null" : "'" + txtLeftDt.DateTime.ToString("yyyyy-MM-dd") + "'"),
                            ((txtEmpTypeCode.Text.Trim() == "") ? "null" : "'" + txtEmpTypeCode.Text.Trim() + "'"),
                            ((txtCatCode.Text.Trim() == "") ? "null" : "'" + txtCatCode.Text.Trim() + "'"),
                            ((txtDeptCode.Text.Trim() == "") ? "null" : "'" + txtDeptCode.Text.Trim() + "'"),
                            ((txtStatCode.Text.Trim() == "") ? "null" : "'" + txtStatCode.Text.Trim() + "'"),
                            ((txtDesgCode.Text.Trim() == "") ? "null" : "'" + txtDesgCode.Text.Trim() + "'"),
                            ((txtGradeCode.Text.Trim() == "") ? "null" : "'" + txtGradeCode.Text.Trim() + "'"),
                            ((txtContCode.Text.Trim() == "") ? "null" : "'" + txtContCode.Text.Trim() + "'"),
                            ((txtShiftCode.Text.Trim() == "") ? "null" : "'" + txtShiftCode.Text.Trim() + "'"),
                            ((txtLeftDt.EditValue == null) ? 1 : 0),
                            Utils.User.GUserID,
                            txtESINo.Text.Trim(),
                            ctrlEmp1.txtCompCode.Text.Trim(), ctrlEmp1.txtEmpUnqID.Text.Trim()

                            );

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();


                        //if (WrkGrpChange)
                        //{
                        //    MessageBox.Show("Employee Job Profile is Discarded.." + Environment.NewLine +
                        //            "Please Fill the Employee Job Profile Again.."
                        //        , "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}

                        MessageBox.Show("Record Updated...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCtrl();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            GrpMain.Enabled = true;

            Cursor.Current = Cursors.Default;
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetCtrl();
            GRights = Attendance.Classes.Globals.GetFormRights(this.Name);
            SetRights();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisplayData(clsEmp temp)
        {
            txtContCode.Text = temp.ContCode;
            txtEmpCode.Text = temp.EmpCode;
            txtSAPID.Text = temp.SAPID;
            txtOldEmpCode.Text = temp.OLDEmpCode;
            txtShiftCode.Text = temp.ShiftCode;
            txtWeekOff.Text = temp.WeekOffDay;
            chkAutoShift.Checked = temp.AutoShift;
            chkOTFlg.Checked = temp.OTFLG;
            chkIsHOD.Checked = temp.IsHOD;

            txtDeptCode.Text = temp.DeptCode;
            txtDeptDesc.Text = temp.DeptDesc;
            txtStatCode.Text = temp.StatCode;
            txtStatDesc.Text = temp.StatDesc;
            txtEmpTypeCode.Text = temp.EmpTypeCode;
            txtEmpTypeDesc.Text = temp.EmpTypeDesc;
            txtDesgCode.Text = temp.DesgCode;
            txtDesgDesc.Text = temp.DesgDesc;
            txtGradeCode.Text = temp.GradeCode;
            txtGradeDesc.Text = temp.GradeDesc;
            txtCatCode.Text = temp.CatCode;
            txtCatDesc.Text = temp.CatDesc;

            txtSecCode.Text = Utils.Helper.GetDescription("Select SecCode from MastEmp Where EmpUnqID = '" + temp.EmpUnqID + "'", Utils.Helper.constr);
            object s = new object();
            EventArgs e = new EventArgs();
            txtSecCode_Validated(s, e);
            txtESINo.Text = Utils.Helper.GetDescription("Select ESINO from MastEmp Where EmpUnqID = '" + temp.EmpUnqID + "'", Utils.Helper.constr);


            if(temp.LeftDt.HasValue){
                txtLeftDt.DateTime = Convert.ToDateTime(temp.LeftDt);
                txtLeftDt.Enabled = false;
            }
            else
            {
                txtLeftDt.EditValue = null;
                txtLeftDt.Enabled = true;
            }

            if (Globals.GetWrkGrpRights(685,"",temp.EmpUnqID))
            {
                txtLeftDt.Enabled = true;
            }
            SetRights();

           
        }

        private void txtShiftCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (ctrlEmp1.txtCompCode.Text.Trim() == "" )
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select ShiftCode,ShiftDesc from MastShift Where CompCode ='" + ctrlEmp1.txtCompCode.Text.Trim() + "' ";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "ShiftCode", "ShiftCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else
                {
                    obj = (List<string>)hlp.Show(sql, "ShiftDesc", "ShiftDesc", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtShiftCode.Text = obj.ElementAt(0).ToString();
                    txtShiftDesc.Text = obj.ElementAt(1).ToString();

                    
                }
            }
        }

        private void txtShiftCode_Validated(object sender, EventArgs e)
        {
            if (ctrlEmp1.txtCompCode.Text.Trim() == "" || ctrlEmp1.txtCompDesc.Text.Trim() == "" )
            {
               
                return;
            }

            DataSet ds = new DataSet();
            string sql = "select * From  MastShift where CompCode ='" + ctrlEmp1.txtCompCode.Text.Trim() + "' and ShiftCode ='" + txtShiftCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                   
                    txtShiftCode.Text = dr["ShiftCode"].ToString();
                    txtShiftDesc.Text = dr["ShiftDesc"].ToString();
                   
                }
            }
            else
            {
                txtShiftDesc.Text = "";
            }
            
        }

        private void txtDeptCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (ctrlEmp1.txtCompCode.Text.Trim() == "" || ctrlEmp1.txtWrkGrpCode.Text.Trim() == "" || ctrlEmp1.txtUnitCode.Text.Trim() == "")
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";

                sql = "Select DeptCode,DeptDesc From MastDept Where CompCode ='" + ctrlEmp1.txtCompCode.Text.Trim() + "' " +
                    " and WrkGrp = '" + ctrlEmp1.txtWrkGrpCode.Text.Trim() + "' and UnitCode ='" + ctrlEmp1.txtUnitCode.Text.Trim() + "'";

                if (e.KeyCode == Keys.F1)
                {
                    
                    obj = (List<string>)hlp.Show(sql, "DeptCode", "DeptCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else
                {
                   
                    obj = (List<string>)hlp.Show(sql, "DeptDesc", "DeptDesc", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtDeptCode.Text = obj.ElementAt(0).ToString();
                    txtDeptDesc.Text = obj.ElementAt(1).ToString();

                }
            }
        }

        private void txtDeptCode_Validated(object sender, EventArgs e)
        {
            if (ctrlEmp1.txtCompCode.Text.Trim() == "" || ctrlEmp1.txtWrkGrpCode.Text.Trim() == "" 
                || ctrlEmp1.txtUnitCode.Text.Trim() == ""
                
                )
                return;

            //txtDeptCode.Text = txtDeptCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastDept where CompCode ='" + ctrlEmp1.txtCompCode.Text.Trim() + "' " +
                    " and WrkGrp='" + ctrlEmp1.txtWrkGrpCode.Text.Trim() + "' " +
                    " and UnitCode ='" + ctrlEmp1.txtUnitCode.Text.Trim() + "' " +
                    " and DeptCode ='" + txtDeptCode.Text.Trim() + "' ";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                   
                    txtDeptCode.Text = dr["DeptCode"].ToString();
                    txtDeptDesc.Text = dr["DeptDesc"].ToString();
                   
                }
            }
            else
            {
                txtDeptCode.Text = "";
                txtDeptDesc.Text = "";
            }
        }

        private void txtStatCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (ctrlEmp1.txtCompCode.Text.Trim() == "" || ctrlEmp1.txtWrkGrpCode.Text.Trim() == ""
                 || ctrlEmp1.txtUnitCode.Text.Trim() == ""
                 
                 )
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";

                sql = "Select StatCode,StatDesc From MastStat Where CompCode ='" + ctrlEmp1.txtCompCode.Text.Trim() + "' " +
                   " and WrkGrp = '" + ctrlEmp1.txtWrkGrpCode.Text.Trim() + "' and UnitCode ='" + ctrlEmp1.txtUnitCode.Text.Trim() + "' " +
                   " and DeptCode='" + txtDeptCode.Text.Trim() + "'";

                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "StatCode", "StatCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else
                {
                    obj = (List<string>)hlp.Show(sql, "StatDesc", "StatDesc", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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
                    txtStatCode.Text = obj.ElementAt(0).ToString();
                    txtStatDesc.Text = obj.ElementAt(1).ToString();
                }
            }
        }

        private void txtStatCode_Validated(object sender, EventArgs e)
        {
            if (ctrlEmp1.txtCompCode.Text.Trim() == "" || ctrlEmp1.txtWrkGrpCode.Text.Trim() == ""
                || ctrlEmp1.txtUnitCode.Text.Trim() == ""
                || txtDeptCode.Text.Trim() == ""
               
                )
                return;

           // txtStatCode.Text = txtStatCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastStat where CompCode ='" + ctrlEmp1.txtCompCode.Text.Trim() + "' " +
                    " and WrkGrp='" + ctrlEmp1.txtWrkGrpCode.Text.Trim() + "' " +
                    " and UnitCode ='" + ctrlEmp1.txtUnitCode.Text.Trim() + "' " +
                    " and DeptCode ='" + txtDeptCode.Text.Trim() + "' " +
                    " and StatCode ='" + txtStatCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
            
                    txtStatCode.Text = dr["StatCode"].ToString();
                    txtStatDesc.Text = dr["StatDesc"].ToString();
                    
                }
            }
            else
            {
                txtStatCode.Text = "";
                txtStatDesc.Text = "";
            }
            
        }

        private void txtCatCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (ctrlEmp1.txtCompCode.Text.Trim() == "" 
                || ctrlEmp1.txtWrkGrpCode.Text.Trim() == ""
              
                )
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select CatCode,CatDesc From MastCat Where CompCode ='" + ctrlEmp1.txtCompCode.Text.Trim() + "' and WrkGrp = '" + ctrlEmp1.txtWrkGrpCode.Text.Trim() + "' ";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "CatCode", "CatCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else
                {
                    obj = (List<string>)hlp.Show(sql, "CatDesc", "CatDesc", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtCatCode.Text = obj.ElementAt(0).ToString();
                    txtCatDesc.Text = obj.ElementAt(1).ToString();


                }
            }
        }

        private void txtCatCode_Validated(object sender, EventArgs e)
        {
             if (ctrlEmp1.txtCompCode.Text.Trim() == "" 
                || ctrlEmp1.txtWrkGrpCode.Text.Trim() == ""
              
                )
                return;
               
           

           
            DataSet ds = new DataSet();
            string sql = "select * From MastCat where CompCode ='" + ctrlEmp1.txtCompCode.Text.Trim() + "' "
                    + " and WrkGrp='" + ctrlEmp1.txtWrkGrpCode.Text.Trim() + "' "
                    + " and CatCode ='" + txtCatCode.Text.Trim() + "' ";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    txtCatCode.Text = dr["CatCode"].ToString();
                    txtCatDesc.Text = dr["CatDesc"].ToString();
                }
            }
            else
            {
                txtCatCode.Text = "";
                txtCatDesc.Text = "";
            }

        }

        private void txtEmpTypeCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (ctrlEmp1.txtCompCode.Text.Trim() == ""
               || ctrlEmp1.txtWrkGrpCode.Text.Trim() == ""
              
               )
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select EmpTypeCode,EmpTypeDesc From MastEmpType Where CompCode ='" + ctrlEmp1.txtCompCode.Text.Trim() + "' and WrkGrp = '" + ctrlEmp1.txtWrkGrpCode.Text.Trim() + "' ";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "EmpTypeCode", "EmpTypeCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else
                {
                    obj = (List<string>)hlp.Show(sql, "EmpTypeDesc", "EmpTypeDesc", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtEmpTypeCode.Text = obj.ElementAt(0).ToString();
                    txtEmpTypeDesc.Text = obj.ElementAt(1).ToString();
                   

                }
            }
        }

        private void txtEmpTypeCode_Validated(object sender, EventArgs e)
        {
            if (ctrlEmp1.txtCompCode.Text.Trim() == "" || ctrlEmp1.txtWrkGrpCode.Text.Trim() == "" )
                return;

            

            DataSet ds = new DataSet();
            string sql = "select * From MastEmpType where CompCode ='" + ctrlEmp1.txtCompCode.Text.Trim() + "' " +
                    " and WrkGrp='" + ctrlEmp1.txtWrkGrpCode.Text.Trim() + "' and EmpTypeCode ='" + txtEmpTypeCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {                   
                    txtEmpTypeCode.Text = dr["EmpTypeCode"].ToString();
                    txtEmpTypeDesc.Text = dr["EmpTypeDesc"].ToString();                   
                }
            }
            else
            {
                txtEmpTypeCode.Text = "";
                txtEmpTypeDesc.Text = "";
            }
            

        }

        private void txtContCode_KeyDown(object sender, KeyEventArgs e)
        {
           

            if (ctrlEmp1.txtCompCode.Text.Trim() == "" 
                || ctrlEmp1.txtWrkGrpCode.Text.Trim() == ""
                || ctrlEmp1.txtUnitCode.Text.Trim() == ""
                )
                return;


            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";

                sql = "Select ContCode,ContName From MastCont Where CompCode ='" + ctrlEmp1.txtCompCode.Text.Trim() + "' " +
                 " and WrkGrp = '" + ctrlEmp1.txtWrkGrpCode.Text.Trim() + "' and UnitCode ='" + ctrlEmp1.txtUnitCode.Text.Trim() + "'";

                if (e.KeyCode == Keys.F1)
                {
                  
                    obj = (List<string>)hlp.Show(sql, "ContCode", "ContCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else
                {
              
                    obj = (List<string>)hlp.Show(sql, "ContName", "ContName", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtContCode.Text = obj.ElementAt(0).ToString();
                    txtContDesc.Text = obj.ElementAt(1).ToString();

                }
            }
        }

        private void txtContCode_Validated(object sender, EventArgs e)
        {
            if (ctrlEmp1.txtCompCode.Text.Trim() == ""
                || ctrlEmp1.txtWrkGrpCode.Text.Trim() == ""
                || ctrlEmp1.txtUnitCode.Text.Trim() == ""
                )
                return;

            //txtContCode.Text = txtContCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastCont where CompCode ='" + ctrlEmp1.txtCompCode.Text.Trim() + "' " +
                    " and WrkGrp='" + ctrlEmp1.txtWrkGrpCode.Text.Trim() + "' " +
                    " and UnitCode ='" + ctrlEmp1.txtUnitCode.Text.Trim() + "' " +
                    " and ContCode ='" + txtContCode.Text.Trim() + "' ";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtContCode.Text = dr["ContCode"].ToString();
                    txtContDesc.Text = dr["ContName"].ToString();
                   
                }

            }
            else
            {
                txtContCode.Text = "";
                txtContDesc.Text = "";
            }
            
        }

        private void txtGradeCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (ctrlEmp1.txtCompCode.Text.Trim() == ""
                || ctrlEmp1.txtWrkGrpCode.Text.Trim() == ""
                || ctrlEmp1.txtUnitCode.Text.Trim() == ""
                )
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select GradeCode,GradeDesc From MastGrade Where CompCode ='" + ctrlEmp1.txtCompCode.Text.Trim() + "' and WrkGrp = '" + ctrlEmp1.txtWrkGrpCode.Text.Trim() + "' ";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "GradeCode", "GradeCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else
                {
                    obj = (List<string>)hlp.Show(sql, "GradeDesc", "GradeDesc", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtGradeCode.Text = obj.ElementAt(0).ToString();
                    txtGradeDesc.Text = obj.ElementAt(1).ToString();
                    
                }
            }
        }

        private void txtGradeCode_Validated(object sender, EventArgs e)
        {
            if (ctrlEmp1.txtCompCode.Text.Trim() == ""
                || ctrlEmp1.txtWrkGrpCode.Text.Trim() == ""
               
                )
                return;

           // txtGradeCode.Text = txtGradeCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastGrade where CompCode ='" + ctrlEmp1.txtCompCode.Text.Trim() + "' " +
                    " and WrkGrp='" + ctrlEmp1.txtWrkGrpCode.Text.Trim() + "' and GradeCode ='" + txtGradeCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    
                    txtGradeCode.Text = dr["GradeCode"].ToString();
                    txtGradeDesc.Text = dr["GradeDesc"].ToString();
                    
                }
            }
            else
            {
                txtGradeCode.Text = "";
                txtGradeDesc.Text = "";
            }
            

        }

        private void txtDesgCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (ctrlEmp1.txtCompCode.Text.Trim() == ""
                || ctrlEmp1.txtWrkGrpCode.Text.Trim() == ""
               
                )
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select DesgCode,DesgDesc From MastDesg Where CompCode ='" + ctrlEmp1.txtCompCode.Text.Trim() + "' and WrkGrp = '" + ctrlEmp1.txtWrkGrpCode.Text.Trim() + "' ";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "DesgCode", "DesgCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else
                {
                    obj = (List<string>)hlp.Show(sql, "DesgDesc", "DesgDesc", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtDesgCode.Text = obj.ElementAt(0).ToString();
                    txtDesgDesc.Text = obj.ElementAt(1).ToString();
                   

                }
            }
        }

        private void txtDesgCode_Validated(object sender, EventArgs e)
        {
            if (ctrlEmp1.txtCompCode.Text.Trim() == ""
                || ctrlEmp1.txtWrkGrpCode.Text.Trim() == ""
               
                )
                return;

           // txtDesgCode.Text = txtDesgCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastDesg where CompCode ='" + ctrlEmp1.txtCompCode.Text.Trim() + "' " +
                    " and WrkGrp='" + ctrlEmp1.txtWrkGrpCode.Text.Trim() + "' and DesgCode ='" + txtDesgCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    
                    txtDesgCode.Text = dr["DesgCode"].ToString();
                    txtDesgDesc.Text = dr["DesgDesc"].ToString();
                   
                }
            }
            else
            {
                txtDesgCode.Text = "";
                txtDesgDesc.Text = "";
            }

           

        }

        private void txtSecCode_Validated(object sender, EventArgs e)
        {
            if (ctrlEmp1.txtCompCode.Text.Trim() == "" || ctrlEmp1.txtWrkGrpCode.Text.Trim() == ""
                || ctrlEmp1.txtUnitCode.Text.Trim() == "" || txtDeptCode.Text.Trim() == ""
                || txtStatCode.Text.Trim() == "" || txtSecCode.Text.Trim() == "")
            {

                txtSecCode.Text = "";
                txtSecDesc.Text = "";
                return;
            }

            txtSecCode.Text = txtSecCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastStatSec where CompCode ='" + ctrlEmp1.txtCompCode.Text.Trim() + "' " +
                    " and WrkGrp='" + ctrlEmp1.txtWrkGrpCode.Text.Trim() + "' " +
                    " and UnitCode ='" + ctrlEmp1.txtUnitCode.Text.Trim() + "' " +
                    " and DeptCode ='" + txtDeptCode.Text.Trim() + "' " +
                    " and StatCode ='" + txtStatCode.Text.Trim() + "' " +
                    " and SecCode ='" + txtSecCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtSecCode.Text = dr["SecCode"].ToString();
                    txtSecDesc.Text = dr["SecDesc"].ToString();
                }
            }
            else
            {
                txtSecCode.Text = "";
                txtSecDesc.Text = "";
            }

        }

        private void txtSecCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (ctrlEmp1.txtCompCode.Text.Trim() == "" || ctrlEmp1.txtWrkGrpCode.Text.Trim() == ""
                || ctrlEmp1.txtUnitCode.Text.Trim() == "" || txtDeptCode.Text.Trim() == ""
                || txtStatCode.Text.Trim() == ""
                )
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";

                sql = "Select SecCode,SecDesc From MastStatSec Where CompCode ='" + ctrlEmp1.txtCompCode.Text.Trim() + "' " +
                   " and WrkGrp = '" + ctrlEmp1.txtWrkGrpCode.Text.Trim() + "' and UnitCode ='" + ctrlEmp1.txtUnitCode.Text.Trim() + "' " +
                   " and DeptCode='" + txtDeptCode.Text.Trim() + "' and StatCode ='" + txtStatCode.Text.Trim() + "'";

                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "SecCode", "SecCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else
                {
                    obj = (List<string>)hlp.Show(sql, "SecDesc", "SecDesc", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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
                    txtSecCode.Text = obj.ElementAt(0).ToString();
                    txtSecDesc.Text = obj.ElementAt(1).ToString();
                }
            }
        }

        private void frmMastEmpJobProfile_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter))
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

    }
}
