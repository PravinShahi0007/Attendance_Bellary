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

namespace Attendance.Forms
{
    public partial class frmMastStat : Form
    {
        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";

        public frmMastStat()
        {
            InitializeComponent();
        }

        private void frmMastDept_Load(object sender, EventArgs e)
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

            if (string.IsNullOrEmpty(txtUnitCode.Text))
            {
                err = err + "Please Enter Unit Code" + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtUnitDesc.Text))
            {
                err = err + "Please Enter Unit Name" + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtDeptCode.Text))
            {
                err = err + "Please Enter Dept Code" + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtStatCode.Text.Trim()))
            {
                err = err + "Please Enter Station Code" + Environment.NewLine;
            }

            else
            {
                string input = txtStatCode.Text.Trim().ToString();
                bool t = Regex.IsMatch(input, @"^\d+$");
                if (!t)
                {
                    err = err + "Please Enter Station Code in Numeric Format..(001,123,012) " + Environment.NewLine;
                }
                

            }
            if (string.IsNullOrEmpty(txtStatDesc.Text))
            {
                err = err + "Please Enter Station/Section Name" + Environment.NewLine;
            }
            


            return err;
        }

       
        private void ResetCtrl()
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            btnAddMan.Enabled = false;
            btnDeleteMan.Enabled = false;

            object s = new object();
            EventArgs e = new EventArgs();
            txtCompCode.Text = "01";
            txtCompName.Text = "";
            txtCompCode_Validated(s, e);
           
            //txtWrkGrpCode.Text = "";
            //txtWrkGrpDesc.Text = "";
            //txtUnitCode.Text = "";
            //txtUnitDesc.Text = "";
            //txtDeptCode.Text = "";
            //txtDeptDesc.Text = "";
            txtStatCode.Text = "";
            txtStatDesc.Text = "";

            oldCode = "";
        }

        private void SetRights()
        {
            if ( txtStatCode.Text.Trim() != "" && mode == "NEW" && GRights.Contains("A") )
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                btnAddMan.Enabled = true;
                btnDeleteMan.Enabled = false;

            }
            else if (txtStatCode.Text.Trim() != "" && mode == "OLD")
            {
                btnAdd.Enabled = false;
                if (GRights.Contains("U"))
                {
                    btnUpdate.Enabled = true;
                    btnAddMan.Enabled = true;
                    btnDeleteMan.Enabled = true;
                }

                if (GRights.Contains("D"))
                {
                    btnDelete.Enabled = true;
                    btnAddMan.Enabled = true;
                    btnDeleteMan.Enabled = true;
                }                    
            }

            if (GRights.Contains("XXXV"))
            {
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                btnAddMan.Enabled = false;
                btnDeleteMan.Enabled = false;
            }
        }

        private void LoadGrid()
        {
            DataSet ds = new DataSet();
            string sql = "select CatCode,CatDesc,Shift,ManPower "
            + " From  MastStatManPower where CompCode ='" + txtCompCode.Text.Trim() + "' "
                    + " and WrkGrp='" + txtWrkGrpCode.Text.Trim() + "' "
                    + " And UnitCode='" + txtUnitCode.Text.Trim() + "' "
                    + " And DeptCode='" + txtDeptCode.Text.Trim() + "' "
                    + " And StatCode ='" + txtStatCode.Text.Trim() + "' ";
                   

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

        private void btnAddMan_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtCatCode.Text.Trim()))
            {
                MessageBox.Show("Please Enter CatCode..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtShiftCode.Text.Trim()))
            {
                MessageBox.Show("Please Enter ShiftCode..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtManPower.Text.Trim()))
            {
                MessageBox.Show("Please Enter ManPower..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            //check if StatCode exist ?
            string sql = "Select StatCode from MastStat Where "
                + " CompCode = '" + txtCompCode.Text.Trim().ToString() + "' "
                + " and WrkGrp ='" + txtWrkGrpCode.Text.Trim().ToString() + "' "
                + " and UnitCode='" + txtUnitCode.Text.Trim().ToString() + "' "
                + " and DeptCode ='" + txtDeptCode.Text.Trim().ToString() + "' "
                + " and Statcode='" +  txtStatCode.Text.Trim().ToString() + "' ";



            string tcode = Utils.Helper.GetDescription(sql, Utils.Helper.constr);

            if (string.IsNullOrEmpty(tcode))
            {
                MessageBox.Show("Please Save Station/Section First....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        sql = "Insert into MastStatManPower "
                            + "(CompCode,WrkGrp,UnitCode,DeptCode,StatCode,CatCode,CatDesc,Shift,ManPower,AddDt,AddID) "
                            + " Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',GetDate(),'{9}')";
                        sql = string.Format(sql, txtCompCode.Text.Trim().ToString(),
                            txtWrkGrpCode.Text.Trim().ToString(),
                            txtUnitCode.Text.Trim().ToString(),
                            txtDeptCode.Text.Trim().ToString(),
                            txtStatCode.Text.Trim().ToString(),
                            txtCatCode.Text.Trim().ToString(),
                            txtCatDesc.Text.Trim().ToString(),
                            txtShiftCode.Text.Trim().ToString(),
                            txtManPower.Text.Trim().ToString(),
                            Utils.User.GUserID);

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record saved...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadGrid();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDeleteMan_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtCatCode.Text.Trim()))
            {
                MessageBox.Show("Please Enter CatCode..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtShiftCode.Text.Trim()))
            {
                MessageBox.Show("Please Enter ShiftCode..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        string sql = "Delete From MastStatManPower Where "
                            + "CompCode = '{0}' and WrkGrp = '{1}' and UnitCode = '{2}' "
                            + " DeptCode = '{3}' and StatCode = '{4}' and CatCode = '{5}' and Shift = '{6}' ";
                            
                        sql = string.Format(sql, txtCompCode.Text.Trim().ToString(),
                            txtWrkGrpCode.Text.Trim().ToString(),
                            txtUnitCode.Text.Trim().ToString(),
                            txtDeptCode.Text.Trim().ToString(),
                            txtStatCode.Text.Trim().ToString(),
                            txtCatCode.Text.Trim().ToString(),                          
                            txtShiftCode.Text.Trim().ToString()
                            );

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record deleted...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadGrid();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
                        string sql = "Insert into MastStat (CompCode,WrkGrp,UnitCode,DeptCode,StatCode,StatDesc,AddDt,AddID) Values ('{0}','{1}','{2}','{3}','{4}','{5}',GetDate(),'{6}')";
                        sql = string.Format(sql, txtCompCode.Text.Trim().ToString(),
                            txtWrkGrpCode.Text.Trim().ToString(),
                            txtUnitCode.Text.Trim().ToString(),
                            txtDeptCode.Text.Trim().ToString(),
                            txtStatCode.Text.Trim().ToString(),
                            txtStatDesc.Text.Trim().ToString(),
                            Utils.User.GUserID);

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record saved...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCtrl();

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
                        string sql = "Update MastStat Set StatDesc = '{0}', UpdDt = GetDate(), UpdID = '{1}' " +
                            " Where CompCode = '{2}' and WrkGrp = '{3}' and UnitCode = '{4}' and DeptCode = '{5}'  and StatCode = '{6}'";

                        sql = string.Format(sql, txtStatDesc.Text.ToString(),
                             Utils.User.GUserID, txtCompCode.Text.Trim().ToString(), txtWrkGrpCode.Text.Trim(),
                             txtUnitCode.Text.Trim(), txtDeptCode.Text.Trim(),txtStatCode.Text.Trim()
                           );

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record Updated...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCtrl();

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
            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    cn.Open();
                    
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        string sql = "Delete From MastStatManPower Where CompCode='{0}' and WrkGrp='{1}' and UnitCode='{2}' and DeptCode = '{3}' and StatCode = '{4}'";
                        sql = string.Format(sql, txtCompCode.Text.Trim(), txtWrkGrpCode.Text.Trim(), txtUnitCode.Text.Trim(), txtDeptCode.Text.Trim(), txtStatCode.Text.Trim());
                        cmd.CommandText = sql;
                        cmd.Connection = cn;
                        cmd.ExecuteNonQuery();

                        sql = "Delete From MastStat Where CompCode='{0}' and WrkGrp='{1}' and UnitCode='{2}' and DeptCode = '{3}' and StatCode = '{4}'";
                        sql = string.Format(sql, txtCompCode.Text.Trim(), txtWrkGrpCode.Text.Trim(), txtUnitCode.Text.Trim(), txtDeptCode.Text.Trim(), txtStatCode.Text.Trim());
                        cmd.CommandText = sql;
                        cmd.Connection = cn;
                        cmd.ExecuteNonQuery();


                        cn.Close();
                        MessageBox.Show("Station Deleted Successfully...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCtrl();
                        return;

                    }

                }
                catch (SqlException sex)
                {
                    MessageBox.Show(sex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
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

        private void txtUnitCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtWrkGrpCode.Text.Trim() == "")
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select UnitCode,UnitName From MastUnit Where CompCode ='" + txtCompCode.Text.Trim() + "' and WrkGrp = '" + txtWrkGrpCode.Text.Trim() + "' ";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "UnitCode", "UnitCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtUnitCode.Text = obj.ElementAt(0).ToString();
                    txtUnitDesc.Text = obj.ElementAt(1).ToString();
                   
                }
            }
        }

        private void txtUnitCode_Validated(object sender, EventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtCompName.Text.Trim() == "" || txtWrkGrpCode.Text.Trim() == "" || txtWrkGrpDesc.Text.Trim() == "")
            {

                return;
            }

            txtUnitCode.Text = txtUnitCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastUnit where CompCode ='" + txtCompCode.Text.Trim() + "' " +
                    " and WrkGrp='" + txtWrkGrpCode.Text.Trim() + "' and UnitCode ='" + txtUnitCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCompCode.Text = dr["CompCode"].ToString();
                    txtWrkGrpCode.Text = dr["WrkGrp"].ToString();
                    txtUnitCode.Text = dr["UnitCode"].ToString();
                    txtUnitDesc.Text = dr["UnitName"].ToString();
                    txtCompCode_Validated(sender, e);
                    txtWrkGrpCode_Validated(sender, e);
                    
                }
            }
        }

        private void txtDeptCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtWrkGrpCode.Text.Trim() == "" || txtUnitCode.Text.Trim() == "")
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";

                sql = "Select DeptCode,DeptDesc From MastDept Where CompCode ='" + txtCompCode.Text.Trim() + "' " +
                    " and WrkGrp = '" + txtWrkGrpCode.Text.Trim() + "' and UnitCode ='" + txtUnitCode.Text.Trim() + "'";
                
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
            if (txtCompCode.Text.Trim() == "" || txtWrkGrpCode.Text.Trim() == "" 
                || txtUnitCode.Text.Trim() == "" )
            {

                return;
            }

            txtDeptCode.Text = txtDeptCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastDept where CompCode ='" + txtCompCode.Text.Trim() + "' " +
                    " and WrkGrp='" + txtWrkGrpCode.Text.Trim() + "' " + 
                    " and UnitCode ='" + txtUnitCode.Text.Trim() + "' " + 
                    " and DeptCode ='" + txtDeptCode.Text.Trim() + "' ";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCompCode.Text = dr["CompCode"].ToString();
                    txtWrkGrpCode.Text = dr["WrkGrp"].ToString();
                    txtUnitCode.Text = dr["UnitCode"].ToString();
                    txtDeptCode.Text = dr["DeptCode"].ToString();
                    txtDeptDesc.Text = dr["DeptDesc"].ToString();
                    txtCompCode_Validated(sender, e);
                    txtWrkGrpCode_Validated(sender, e);
                    txtUnitCode_Validated(sender, e);                    
                }
            }
            
        }

        private void txtStatCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtWrkGrpCode.Text.Trim() == "" 
                || txtUnitCode.Text.Trim() == "" || txtDeptCode.Text.Trim() == "" )
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";

                sql = "Select StatCode,StatDesc From MastStat Where CompCode ='" + txtCompCode.Text.Trim() + "' " +
                   " and WrkGrp = '" + txtWrkGrpCode.Text.Trim() + "' and UnitCode ='" + txtUnitCode.Text.Trim() + "' " +
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
            if (txtCompCode.Text.Trim() == "" ||  txtWrkGrpCode.Text.Trim() == "" 
                || txtUnitCode.Text.Trim() == "" || txtDeptCode.Text.Trim() == "" 
                || txtStatCode.Text.Trim() == "" )
            {

                return;
            }

            txtStatCode.Text = txtStatCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastStat where CompCode ='" + txtCompCode.Text.Trim() + "' " +
                    " and WrkGrp='" + txtWrkGrpCode.Text.Trim() + "' " +
                    " and UnitCode ='" + txtUnitCode.Text.Trim() + "' " +
                    " and DeptCode ='" + txtDeptCode.Text.Trim() + "' " +
                    " and StatCode ='" + txtStatCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCompCode.Text = dr["CompCode"].ToString();
                    txtWrkGrpCode.Text = dr["WrkGrp"].ToString();
                    txtUnitCode.Text = dr["UnitCode"].ToString();
                    txtDeptCode.Text = dr["DeptCode"].ToString();
                    
                    txtStatCode.Text = dr["StatCode"].ToString();
                    txtStatDesc.Text = dr["StatDesc"].ToString();
                    txtCompCode_Validated(sender, e);
                    txtWrkGrpCode_Validated(sender, e);
                    txtUnitCode_Validated(sender, e);
                    txtDeptCode_Validated(sender, e);
                    
                    mode = "OLD";
                    oldCode = dr["StatCode"].ToString();
                    
                }
            }
            else
            {
                mode = "NEW";
                oldCode = "";
            }

            LoadGrid();
            SetRights();
        }

        private void txtCatCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtWrkGrpCode.Text.Trim() == "")
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select CatCode,CatDesc From MastCat Where CompCode ='" + txtCompCode.Text.Trim() + "' and WrkGrp = '" + txtWrkGrpCode.Text.Trim() + "' ";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "CatCode", "CatCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

        private void txtCatCode_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" ||  txtWrkGrpCode.Text.Trim() == "" 
                || txtUnitCode.Text.Trim() == "" || txtDeptCode.Text.Trim() == ""
                || txtDeptCode.Text.Trim() == "" || txtStatCode.Text.Trim() == "" 
                || txtCatCode.Text.Trim() == "" )
            {

                return;
            }
            if (mode == "NEW")
            {
                return;
            }

            txtCatCode.Text = txtCatCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastCat where CompCode ='" + txtCompCode.Text.Trim() + "' " 
                    + " and WrkGrp='" + txtWrkGrpCode.Text.Trim() + "' "                     
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
            
        }

        private void txtShiftCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtWrkGrpCode.Text.Trim() == "")
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select ShiftCode,ShiftDesc From MastShift Where CompCode ='" + txtCompCode.Text.Trim() + "' ";
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

                }
            }
        }

        private void txtShiftCode_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtWrkGrpCode.Text.Trim() == ""
                || txtUnitCode.Text.Trim() == "" || txtDeptCode.Text.Trim() == ""
                || txtDeptCode.Text.Trim() == "" || txtStatCode.Text.Trim() == ""
                || txtCatCode.Text.Trim() == "" || txtShiftCode.Text.Trim() == "")
            {

                return;
            }
            if (mode == "NEW")
            {
                return;
            }

            //txtCatCode.Text = txtCatCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastStatManPower where CompCode ='" + txtCompCode.Text.Trim() + "' "
                    + " and WrkGrp='" + txtWrkGrpCode.Text.Trim() + "' "
                    + " And UnitCode='" + txtUnitCode.Text.Trim() + "' "
                    + " And DeptCode='" + txtDeptCode.Text.Trim() + "' "
                    + " And StatCode ='" + txtStatCode.Text.Trim() + "' "
                    + " And CatCode='" + txtCatCode.Text.Trim() + "' "
                    + " and Shift ='" + txtShiftCode.Text.Trim() + "' ";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    txtCatCode.Text = dr["CatCode"].ToString();
                    txtCatDesc.Text = dr["CatDesc"].ToString();
                    txtManPower.Text = dr["ManPower"].ToString();
                    btnAddMan.Enabled = false;
                    btnDeleteMan.Enabled = true;
                }
            }
            else
            {
                txtManPower.Text = "0";
                btnAddMan.Enabled = true;
                btnDeleteMan.Enabled = false;
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
                txtCatCode.Text = gridView1.GetRowCellValue(info.RowHandle, "CatCode").ToString();
                txtCatDesc.Text = gridView1.GetRowCellValue(info.RowHandle, "CatDesc").ToString();
                txtShiftCode.EditValue = gridView1.GetRowCellValue(info.RowHandle, "Shift").ToString();
                txtManPower.Text = gridView1.GetRowCellValue(info.RowHandle, "ManPower").ToString();
                object o = new object();
                EventArgs e = new EventArgs();
                btnDeleteMan.Enabled = true;
                btnAddMan.Enabled = false;
            }


        }


       

    }
}
