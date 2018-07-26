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
    public partial class frmMastHoliday : Form
    {
        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";

        public frmMastHoliday()
        {
            InitializeComponent();
        }

        private void frmMastHoliday_Load(object sender, EventArgs e)
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
                err = err + "Please Enter WrkGrpCode..." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtWrkGrpDesc.Text))
            {
                err = err + "Please Enter WrkGrp Description..." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtLeaveTyp.Text))
            {
                err = err + "Please Enter LeaveType.." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtLeaveDesc.Text))
            {
                err = err + "Please Enter Leave Description" + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtYear.Text))
            {
                err = err + "Please Enter Year.." + Environment.NewLine;
                return err;
            }

            if (string.IsNullOrEmpty(txtDate.Text))
            {
                err = err + "Please Enter Date.." + Environment.NewLine;
                return err;
            }

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                err = err + "Please Holiday Description.." + Environment.NewLine;
            }

            if (txtDate.DateTime.Year != Convert.ToInt32(txtYear.Text.Trim()))
            {
                err = err + "Invalid Year and Date Selection.." + Environment.NewLine;
            }

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
            //txtWrkGrpCode.Text = "";
            //txtWrkGrpDesc.Text = "";
            txtLeaveTyp.Text = "";
            txtLeaveDesc.Text = "";
            txtDate.EditValue = null;
            //txtYear.Text = "";
            txtDescription.Text = "";

            oldCode = "";
            mode = "NEW";
        }

        private void SetRights()
        {
            if ( txtDate.EditValue == null && mode == "NEW" && GRights.Contains("A") )
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            else if (txtDate.EditValue != null && mode == "OLD")
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

        private void txtLeaveTyp_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtWrkGrpCode.Text.Trim() == "")
                return;
            
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select LeaveTyp,LeaveDesc from MastLeave Where "
                   + " CompCode ='" + txtCompCode.Text.Trim() + "' "
                   + " and WrkGrp ='" + txtWrkGrpCode.Text.Trim() + "' and PublicHL = 1 ";
                   
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "LeaveTyp", "LeaveTyp", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtLeaveTyp.Text = obj.ElementAt(0).ToString();
                    txtLeaveDesc.Text = obj.ElementAt(1).ToString();
                   
                }
            }
        }

        private void txtLeaveTyp_Validated(object sender, EventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtCompName.Text.Trim() == "" || txtLeaveTyp.Text.Trim() == "")
            {
                return;
            }

            DataSet ds = new DataSet();
            string sql = "select * From  MastLeave where CompCode ='" + txtCompCode.Text.Trim() + "' " 
                +   " And WrkGrp ='" + txtWrkGrpCode.Text.Trim() + "' and LeaveTyp ='" + txtLeaveTyp.Text.Trim() + "' and PublicHL = 1 ";
            
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {                    
                    txtLeaveTyp.Text = dr["LeaveTyp"].ToString();
                    txtLeaveDesc.Text = dr["LeaveDesc"].ToString();                    
                }
            }
            else
            {
                MessageBox.Show("Selected leave is not defined as public holiday", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLeaveTyp.Text = "";
                txtLeaveDesc.Text = "";                
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
                        cn.Open();
                        cmd.Connection = cn;
                        string sql = "Insert into HoliDayMast " +
                            "(CompCode,WrkGrp,tYear,tDate,PublicHLTyp,HLDesc," +
                            " AddDt,AddID) Values ('{0}','{1}','{2}','{3:yyyy-MM-dd}','{4}','{5}'," +
                            " GetDate(),'{6}')";

                        sql = string.Format(sql, txtCompCode.Text.Trim().ToString(), 
                            txtWrkGrpCode.Text.Trim().ToUpper(),txtYear.Text.Trim(),txtDate.DateTime,
                            txtLeaveTyp.Text.Trim().ToString(),txtDescription.Text.Trim().ToString(),                            
                            Utils.User.GUserID);

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record saved...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCtrl();
                        LoadGrid();
                        SetRights();
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
                        cn.Open();
                        cmd.Connection = cn;
                        string sql = "Update HoliDayMast Set HLDesc = '{0}' , UpdDt = GetDate(), UpdID = '{1}' " 
                            + " Where CompCode = '{2}' and WrkGrp = '{3}' and tYear ='{4}' "
                            + " And tDate = '{5:yyyy-MM-dd}' ";

                        sql = string.Format(sql, txtDescription.Text.Trim(),  Utils.User.GUserID,
                            txtCompCode.Text.Trim(), txtWrkGrpCode.Text.Trim().ToString(),txtYear.Text.Trim(),
                            txtDate.DateTime
                           );

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        ResetCtrl();
                        LoadGrid();
                        MessageBox.Show("Record Updated...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetRights();
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
               
                DialogResult qs = MessageBox.Show("Are You Sure to Delete this Leave...?","Question",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
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
                            string sql = "Delete From HoliDayMAst where CompCode = '" + txtCompCode.Text.Trim() + "' " 
                            + " and WrkGrp = '" + txtWrkGrpCode.Text.Trim() + "' "
                            + " and tYear = '" + txtYear.Text.Trim() + "' "
                            + " and tDate ='" + txtDate.DateTime.ToString("yyyy-MM-dd") + "' "
                            + " And PublicHLTyp ='" + txtLeaveTyp.Text.Trim().ToString() + "'";
                            cmd.CommandText = sql;
                            cmd.Connection = cn;
                            cmd.ExecuteNonQuery();
                           
                            
                            MessageBox.Show("Record Deleted...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetCtrl();
                            LoadGrid();
                            SetRights();
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
            if (txtCompCode.Text.Trim() == "" || txtCompName.Text.Trim() == "" || txtWrkGrpCode.Text.Trim() == ""
                || txtYear.Text.Trim() == "" )
            {
                grid.DataSource = null;
                return;
            }


            DataSet ds = new DataSet();
            string sql = "select CompCode,WrkGrp,tYear,tDate,PublicHLTyp,HLDesc " +
                    "  from HoliDayMast "
                    + " where CompCode ='" + txtCompCode.Text.Trim() + "'  "
                    + " And WrkGrp ='" + txtWrkGrpCode.Text.Trim() + "' "
                    + " And tYear ='" + txtYear.Text.Trim() + "' Order By tYear,tDate";

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
                txtCompCode.Text = gridView1.GetRowCellValue(info.RowHandle, "CompCode").ToString();
                txtWrkGrpCode.Text = gridView1.GetRowCellValue(info.RowHandle, "WrkGrp").ToString();
                txtLeaveTyp.Text = gridView1.GetRowCellValue(info.RowHandle, "PublicHLTyp").ToString();
                txtYear.Text = gridView1.GetRowCellValue(info.RowHandle, "tYear").ToString();
                txtDate.DateTime = (DateTime)gridView1.GetRowCellValue(info.RowHandle, "tDate");
                txtDescription.Text = gridView1.GetRowCellValue(info.RowHandle, "HLDesc").ToString();

                object o = new object();
                EventArgs e = new EventArgs();                
                mode = "OLD";
                oldCode = txtDate.DateTime.ToString("yyyy-MM-dd");
                txtCompCode_Validated(o, e);
                txtWrkGrpCode_Validated(o, e);
                txtLeaveTyp_Validated(o, e);
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
            if (txtCompCode.Text.Trim() == "" || txtCompName.Text.Trim() == "" || txtWrkGrpCode.Text.Trim() == "")
            {
                grid.DataSource = null;
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


        }

        private void txtYear_Validated(object sender, EventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtWrkGrpCode.Text.Trim() == ""|| txtYear.Text == "")
            {
                grid.DataSource = null;

                return;
            }
            LoadGrid();
        }

        private void txtDate_Validated(object sender, EventArgs e)
        {
            if (txtDate.DateTime.Year != Convert.ToInt32(txtYear.Text.Trim()))
            {
                txtDate.EditValue = null;
            }
        }

        private void txtDate_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtCompName.Text.Trim() == "" || txtWrkGrpCode.Text.Trim() == "" ||
                txtDate.EditValue == null || txtYear.Text.Trim() == "")
            {
                mode = "NEW";
                return;
            }

            DataSet ds = new DataSet();
            string sql = "select * From  HoliDayMast where CompCode ='" + txtCompCode.Text.Trim() + "' "
                + " And WrkGrp ='" + txtWrkGrpCode.Text.Trim() + "' and tYear = '" + txtYear.Text.Trim() + "' "
                + " And tDate ='" + txtDate.DateTime.ToString("yyyy-MM-dd") + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCompCode.Text = dr["CompCode"].ToString();
                    txtWrkGrpCode.Text = dr["WrkGrp"].ToString();
                    txtYear.Text = dr["tYear"].ToString();
                    txtDate.EditValue = dr["tDate"];
                    txtLeaveTyp.Text = dr["PublicHLTyp"].ToString();
                    txtDescription.Text = dr["HLDesc"].ToString();

                    txtCompCode_Validated(sender, e);
                    txtWrkGrpCode_Validated(sender, e);
                    txtLeaveTyp_Validated(sender, e);
                    mode = "OLD";
                    oldCode = txtDate.DateTime.ToString("yyyy-MM-dd");

                }
            }
            else
            {
                mode = "NEW";
                oldCode = "";
            }


            SetRights();
        }

        private void frmMastHoliday_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter))
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        


    }
}
