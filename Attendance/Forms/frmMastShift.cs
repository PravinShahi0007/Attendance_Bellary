using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Attendance.Forms
{
    public partial class frmMastShift : Form
    {
        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";

        public frmMastShift()
        {
            InitializeComponent();
        }

        private void frmMastShift_Load(object sender, EventArgs e)
        {
            ResetCtrl();
            GRights = Attendance.Classes.Globals.GetFormRights(this.Name);
            SetRights();
            LoadGrid();
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


            if (string.IsNullOrEmpty(txtShiftCode.Text))
            {
                err = err + "Please Enter Shift Code .." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                err = err + "Please Enter Shift Description.." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtShiftSeq.Text))
            {
                err = err + "Please Enter Seq. Number.." + Environment.NewLine;
            }


            if (string.IsNullOrEmpty(txtShiftHrs.Text))
            {
                err = err + "Please Enter Shift Hrs." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtBreakHrs.Text))
            {
                err = err + "Please Enter Break Hrs." + Environment.NewLine;
            }

            int t = 0;
            if (int.TryParse(txtShiftHrs.Text.ToString(), out t))
            {
                if (t <= 0)
                    err = err + "Please Enter Shift Hrs." + Environment.NewLine;
            }
            t = 0;
            if (int.TryParse(txtShiftSeq.Text.ToString(), out t))
            {
                if (t <= 0)
                    err = err + "Please Enter Shift Seq. No" + Environment.NewLine;
            }

            if (txtShiftStart.EditValue == null)
            {
                err = err + "Please Enter Shift Start Time" + Environment.NewLine;
            }
            if (txtShiftEnd.EditValue == null)
            {
                err = err + "Please Enter Shift End Time" + Environment.NewLine;
            }
            if (txtIN_From.EditValue == null)
            {
                err = err + "Please Enter Allowed In Time Range.." + Environment.NewLine;
            }
            if (txtIN_To.EditValue == null)
            {
                err = err + "Please Enter Allowed In Time Range.." + Environment.NewLine;
            }
            if (txtOUT_From.EditValue == null)
            {
                err = err + "Please Enter Allowed Out Time Range.." + Environment.NewLine;
            }
            if (txtOUT_To.EditValue == null)
            {
                err = err + "Please Enter Allowed Out Time Range.." + Environment.NewLine;
            }
            TimeSpan ShiftStart, ShiftEnd, ShiftInFrom, ShiftInTo, ShiftOutFrom, ShiftOutTo;

            ShiftStart = new TimeSpan(txtShiftStart.Time.Hour, txtShiftStart.Time.Minute,txtShiftStart.Time.Second);
            ShiftEnd = new TimeSpan(txtShiftEnd.Time.Hour, txtShiftEnd.Time.Minute, txtShiftEnd.Time.Second);
            ShiftInFrom = new TimeSpan(txtIN_From.Time.Hour, txtIN_From.Time.Minute, txtIN_From.Time.Second);
            ShiftInTo = new TimeSpan(txtIN_To.Time.Hour, txtIN_To.Time.Minute, txtIN_To.Time.Second);
            ShiftOutFrom = new TimeSpan(txtOUT_From.Time.Hour, txtOUT_From.Time.Minute, txtOUT_From.Time.Second);
            ShiftOutTo = new TimeSpan(txtOUT_To.Time.Hour, txtOUT_To.Time.Minute, txtOUT_To.Time.Second);


            if (ShiftStart > ShiftEnd && chkNight.Checked == false)
            {
                err = err + "Shift Start must be less than shift end.." + Environment.NewLine;
            }

            //if (ShiftInFrom > ShiftInTo)
            //{
            //    err = err + "Invalid Allowed Range for Shift In From -to .." + Environment.NewLine;
            //}

            //if (ShiftOutFrom > ShiftOutTo)
            //{
            //    err = err + "Invalid Allowed Range for Shift Out From - to ." + Environment.NewLine;
            //}

            //if (ShiftInFrom > ShiftStart)
            //{
            //    err = err + "Shift In from must be less than  Shift Start.." + Environment.NewLine;
            //}

            //if (ShiftInTo < ShiftStart || ShiftInTo >= ShiftEnd)
            //{
            //    err = err + "Shift In To must be under Shift Start-End.." + Environment.NewLine;
            //}



            return err;
        }
        
        private void ResetCtrl()
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            object s = new object();
            EventArgs e = new EventArgs();
            txtCompCode.Text = "01";
            txtCompName.Text = "";
            txtCompCode_Validated(s, e);
           
            txtShiftCode.Text = "";
            txtDescription.Text = "";
            txtShiftStart.EditValue = null;
            txtShiftEnd.EditValue = null;
            txtIN_From.EditValue = null;
            txtIN_To.EditValue = null;
            txtOUT_From.EditValue = null;
            txtOUT_To.EditValue = null;

            txtShiftHrs.EditValue = null;
            txtBreakHrs.EditValue = null;
            txtShiftSeq.EditValue = null;

            chkNight.CheckState = CheckState.Unchecked;

            oldCode = "";
            mode = "NEW";
        }

        private void SetRights()
        {
            if ( txtShiftCode.Text.Trim() != "" && mode == "NEW" && GRights.Contains("A") )
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            else if ( txtShiftCode.Text.Trim() != "" && mode == "OLD" )
            {
                btnAdd.Enabled = false;

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


        private void txtShiftCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCompCode.Text.Trim() == "")
                return;
            
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select ShiftCode,ShiftDesc from MastShift Where CompCode ='" + txtCompCode.Text.Trim() + "' ";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "ShiftCode", "ShiftCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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
                    txtDescription.Text = obj.ElementAt(1).ToString();
                    
                    mode = "OLD";
                }
            }
        }

        private void txtShiftCode_Validated(object sender, EventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtCompName.Text.Trim() == "" || txtShiftCode.Text.Trim() == "")
            {
                mode = "NEW";
                return;
            }

            DataSet ds = new DataSet();
            string sql = "select * From  MastShift where CompCode ='" + txtCompCode.Text.Trim() + "' and ShiftCode ='" + txtShiftCode.Text.Trim() + "'";
            
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCompCode.Text = dr["CompCode"].ToString();
                    txtShiftCode.Text = dr["ShiftCode"].ToString();
                    txtDescription.Text = dr["ShiftDesc"].ToString();
                    
                    txtShiftStart.EditValue = (TimeSpan)dr["ShiftStart"];
                    txtShiftEnd.EditValue = (TimeSpan)dr["ShiftEnd"];
                    txtIN_From.EditValue = (TimeSpan)dr["ShiftInFrom"];
                    txtIN_To.EditValue = (TimeSpan)dr["ShiftInTo"];
                    txtOUT_From.EditValue = (TimeSpan)dr["ShiftOutFrom"];
                    txtOUT_To.EditValue = (TimeSpan)dr["ShiftOutTo"];

                    txtShiftHrs.EditValue = dr["ShiftHrs"].ToString();
                    txtBreakHrs.EditValue = dr["BreakHrs"].ToString();
                    txtShiftSeq.Text = dr["ShiftSeq"].ToString();

                    chkNight.CheckState = (Convert.ToBoolean(dr["NightFlg"])) ? CheckState.Checked : CheckState.Unchecked;
                    
                    
                    mode = "OLD";
                    txtCompCode_Validated(sender,e);
                    oldCode = dr["ShiftCode"].ToString();
                }
            }
            else
            {
                mode = "NEW";
                oldCode = "";
            }
            
             SetRights();
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
                    LoadGrid();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        TimeSpan ShiftStart, ShiftEnd, ShiftInFrom, ShiftInTo, ShiftOutFrom, ShiftOutTo;

                        ShiftStart = new TimeSpan(txtShiftStart.Time.Hour, txtShiftStart.Time.Minute, txtShiftStart.Time.Second);
                        ShiftEnd = new TimeSpan(txtShiftEnd.Time.Hour, txtShiftEnd.Time.Minute, txtShiftEnd.Time.Second);
                        ShiftInFrom = new TimeSpan(txtIN_From.Time.Hour, txtIN_From.Time.Minute, txtIN_From.Time.Second);
                        ShiftInTo = new TimeSpan(txtIN_To.Time.Hour, txtIN_To.Time.Minute, txtIN_To.Time.Second);
                        ShiftOutFrom = new TimeSpan(txtOUT_From.Time.Hour, txtOUT_From.Time.Minute, txtOUT_From.Time.Second);
                        ShiftOutTo = new TimeSpan(txtOUT_To.Time.Hour, txtOUT_To.Time.Minute, txtOUT_To.Time.Second);

                        
                        
                        cn.Open();
                        cmd.Connection = cn;
                        string sql = "Insert into MastShift " +
                            "(CompCode,ShiftCode,ShiftDesc,ShiftSeq,ShiftHrs,BreakHrs," +
                            " ShiftStart,ShiftEnd,ShiftInFrom,ShiftInTo, " +
                            " ShiftOutFrom,ShiftOutTo, NightFLG, " +
                            " AddDt,AddID) Values ('{0}','{1}','{2}','{3}','{4}','{5}'," +
                            " '{6}','{7}','{8}','{9}'," +
                            " '{10}','{11}','{12}',GetDate(),'{13}')";
                        sql = string.Format(sql, txtCompCode.Text.Trim().ToString(), txtShiftCode.Text.Trim().ToString(),
                            txtDescription.Text.Trim().ToString(),txtShiftSeq.Text.Trim().ToString(),txtShiftHrs.Text.Trim().ToString(),txtBreakHrs.Text.ToString(),
                            ShiftStart.ToString(@"hh\:mm\:ss"), ShiftEnd.ToString(@"hh\:mm\:ss"), ShiftInFrom.ToString(@"hh\:mm\:ss"), ShiftInTo.ToString(@"hh\:mm\:ss"),
                            ShiftOutFrom.ToString(@"hh\:mm\:ss"), ShiftOutTo.ToString(@"hh\:mm\:ss"),
                            ((chkNight.Checked)?"1":"0"),Utils.User.GUserID
                            
                            );

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record saved...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCtrl();
                        LoadGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }
        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        TimeSpan ShiftStart, ShiftEnd, ShiftInFrom, ShiftInTo, ShiftOutFrom, ShiftOutTo;

                        ShiftStart = new TimeSpan(txtShiftStart.Time.Hour, txtShiftStart.Time.Minute, txtShiftStart.Time.Second);
                        ShiftEnd = new TimeSpan(txtShiftEnd.Time.Hour, txtShiftEnd.Time.Minute, txtShiftEnd.Time.Second);
                        ShiftInFrom = new TimeSpan(txtIN_From.Time.Hour, txtIN_From.Time.Minute, txtIN_From.Time.Second);
                        ShiftInTo = new TimeSpan(txtIN_To.Time.Hour, txtIN_To.Time.Minute, txtIN_To.Time.Second);
                        ShiftOutFrom = new TimeSpan(txtOUT_From.Time.Hour, txtOUT_From.Time.Minute, txtOUT_From.Time.Second);
                        ShiftOutTo = new TimeSpan(txtOUT_To.Time.Hour, txtOUT_To.Time.Minute, txtOUT_To.Time.Second);

                        

                        cn.Open();
                        cmd.Connection = cn;
                        string sql = "Update MastShift Set ShiftDesc = '{0}', " +
                            " ShiftSeq = '{1}',ShiftHrs='{2}',BreakHrs='{3}'," +
                            " ShiftStart = '{4}',ShiftEnd='{5}',ShiftInFrom='{6}',ShiftInTo='{7}', " +
                            " ShiftOutFrom ='{8}',ShiftOutTo='{9}', NightFLG ='{10}'," +
                            " UpdDt = GetDate(), UpdID = '{11}' Where CompCode = '{12}' and ShiftCode = '{13}' ";

                        sql = string.Format(sql, txtDescription.Text.Trim(),
                             txtShiftSeq.Text.Trim().ToString(), txtShiftHrs.Text.Trim().ToString(), txtBreakHrs.Text.ToString(),
                            ShiftStart.ToString(@"hh\:mm\:ss"), ShiftEnd.ToString(@"hh\:mm\:ss"), ShiftInFrom.ToString(@"hh\:mm\:ss"), ShiftInTo.ToString(@"hh\:mm\:ss"),
                            ShiftOutFrom.ToString(@"hh\:mm\:ss"), ShiftOutTo.ToString(@"hh\:mm\:ss"), ((chkNight.Checked) ? "1" : "0"),
                             Utils.User.GUserID, txtCompCode.Text.Trim().ToString(), txtShiftCode.Text.Trim().ToString()
                           );

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        ResetCtrl();
                        LoadGrid();
                        MessageBox.Show("Record Updated...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(err))
            {
               
                DialogResult qs = MessageBox.Show("Are You Sure to Delete this Shift...?","Question",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if(qs == DialogResult.No){
                    return;
                }
                
                using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cn.Open();
                            string sql = "Delete From MastShift where CompCode = '" + txtCompCode.Text.Trim() + "' and ShiftCode = '" + txtShiftCode.Text.Trim().ToString() + "'";
                            cmd.CommandText = sql;
                            cmd.Connection = cn;
                            cmd.ExecuteNonQuery();
                           
                            
                            MessageBox.Show("Record Deleted...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetCtrl();
                            LoadGrid();
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }

           // MessageBox.Show("Not Implemented...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void LoadGrid()
        {
            DataSet ds = new DataSet();
            string sql = "select CompCode,ShiftCode,ShiftDesc,ShiftSeq,ShiftHrs,BreakHrs,ShiftStart,ShiftEnd, " +
                    " ShiftInFrom,ShiftInTo,ShiftOutFrom,ShiftOutTo,NightFlg from MastShift where CompCode = '" + txtCompCode.Text.Trim() + "' Order By ShiftSeq ";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);

            Boolean hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);


            if (hasRows)
            {
                grid.DataSource = ds;
                grid.DataMember = ds.Tables[0].TableName;
            }
            else
            {
                grid.DataSource = null;
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            DoRowDoubleClick(view, pt);
        }

        private void DoRowDoubleClick(GridView view, Point pt)
        {
            GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
               txtShiftCode.Text = gridView1.GetRowCellValue(info.RowHandle, "ShiftCode").ToString();
               txtCompCode.Text = gridView1.GetRowCellValue(info.RowHandle, "CompCode").ToString();
                object o = new object();
                EventArgs e = new EventArgs();                
                mode = "OLD";
                oldCode = txtShiftCode.Text.ToString();
                txtCompCode_Validated(o, e);
                txtShiftCode_Validated(o, e);
            }

            
        }


    }
}
