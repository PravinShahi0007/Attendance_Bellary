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
    public partial class frmReaderConfigMess : Form
    {
        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";

        public frmReaderConfigMess()
        {
            InitializeComponent();
        }

        private void frmReaderConfigMess_Load(object sender, EventArgs e)
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

            if (string.IsNullOrEmpty(txtUnitCode.Text))
            {
                err = err + "Please Enter Unit Code" + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtUnitDesc.Text))
            {
                err = err + "Please Enter Unit Name" + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtMessCode.Text))
            {
                err = err + "Please Enter MessCode" + Environment.NewLine;
            }
            else
            {
                string input = txtMessCode.Text.Trim().ToString();
                bool t = Regex.IsMatch(input, @"^\d+$");
                if (!t)
                {
                    err = err + "Please Enter MessCode in Numeric Format..(001,123,012) " + Environment.NewLine;
                }
                

            }
            if (string.IsNullOrEmpty(txtMessDesc.Text))
            {
                err = err + "Please Enter Mess Name" + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtMachineIP.Text))
            {
                err = err + "Please Enter MachineIP.." + Environment.NewLine;
            }
            
            if (!string.IsNullOrEmpty(txtMachineIP.Text))
            {
                using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cn.Open();
                            cmd.Connection = cn;
                            string sql = "Select Count(*) from ReaderConfig where CompCode = '" + txtCompCode.Text.Trim() + "' "
                                + " And Master = 0 and Active = 1 and CanteenFlg = 1 and DelFlg = 0 " 
                                + " and MachineIP in ('" + txtMachineIP.Text.Trim().ToString() + "')";

                            cmd.CommandText = sql;
                            int cnt = (int)cmd.ExecuteScalar();
                            if (cnt == 0)
                            {
                                err = err + "IP Address not Found in Reader Configuration...." + Environment.NewLine;
                            }
                        }
                        catch (Exception ex)
                        {
                            err = err + ex.ToString();
                        }
                    }
                }
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
           
            
            txtUnitCode.Text = "";
            txtUnitDesc.Text = "";
            txtMessCode.Text = "";
            txtMessDesc.Text = "";
            txtMachineIP.Text = "";
           
            oldCode = "";
            mode = "NEW";
        }
        
        private void SetRights()
        {
            if ( txtMachineIP.Text.Trim() != "" && mode == "NEW" && GRights.Contains("A") )
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            else if (txtMachineIP.Text.Trim() != "" && mode == "OLD")
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
                        string sql = "Insert into MastMessReader (CompCode,UnitCode,MessCode,MachineIP,AddDt,AddID) Values ('{0}','{1}','{2}','{3}',GetDate(),'{4}')";
                        sql = string.Format(sql, txtCompCode.Text.Trim().ToString(),
                            txtUnitCode.Text.Trim().ToString(),
                            txtMessCode.Text.Trim().ToString(),
                            txtMachineIP.Text.Trim().ToString().ToUpper(),
                            Utils.User.GUserID);

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record saved...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCtrl();
                        LoadGrid();
                        return;

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

            MessageBox.Show("Not Implemented....", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);           
            

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

                DialogResult qs = MessageBox.Show("Are You Sure to Delete this machine...?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (qs == DialogResult.No)
                {
                    return;
                }

                using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cn.Open();
                            string sql = "Delete From MastMessReader where CompCode = '" + txtCompCode.Text.Trim() + "' " 
                                + " And UnitCode = '" + txtUnitCode.Text.Trim().ToString() + "' "
                                + " And MessCode ='" + txtMessCode.Text.Trim().ToString() + "' "
                                + " and MachineIP = '" + txtMachineIP.Text.Trim().ToString() + "'";
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
            if (txtCompCode.Text.Trim() == "")
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select UnitCode,UnitName From MastUnit Where CompCode ='" + txtCompCode.Text.Trim() + "' ";
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
            if (txtCompCode.Text.Trim() == "" || txtCompName.Text.Trim() == "" )
            {

                return;
            }

            txtUnitCode.Text = txtUnitCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastUnit where CompCode ='" + txtCompCode.Text.Trim() + "' " +
                    " and  UnitCode ='" + txtUnitCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCompCode.Text = dr["CompCode"].ToString();
                    txtUnitCode.Text = dr["UnitCode"].ToString();
                    txtUnitDesc.Text = dr["UnitName"].ToString();
                    txtCompCode_Validated(sender, e);
                    
                }
            }
        }

        private void txtMessCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" ||  txtUnitCode.Text.Trim() == "")
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select MessCode,MessDesc From MastMess Where CompCode ='" + txtCompCode.Text.Trim() + "' " + 
                    " and  UnitCode ='" + txtUnitCode.Text.Trim() + "'";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "MessCode", "MessCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtMessCode.Text = obj.ElementAt(0).ToString();
                    txtMessDesc.Text = obj.ElementAt(1).ToString();

                }
            }
        }

        private void txtMessCode_Validated(object sender, EventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtCompName.Text.Trim() == "" || txtUnitCode.Text.Trim() == "" || txtUnitDesc.Text.Trim() == "")
            {

                return;
            }

            txtMessCode.Text = txtMessCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastMess where CompCode ='" + txtCompCode.Text.Trim() + "' " +
                    " and UnitCode ='" + txtUnitCode.Text.Trim() + "' and MessCode ='" + txtMessCode.Text.Trim() + "'";
                    
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCompCode.Text = dr["CompCode"].ToString();
                    txtUnitCode.Text = dr["UnitCode"].ToString();
                    txtMessCode.Text = dr["MessCode"].ToString();
                    txtMessDesc.Text = dr["MessDesc"].ToString();
                    txtCompCode_Validated(sender, e);
                    txtUnitCode_Validated(sender, e);
                    
                }
            }

        }
        
        private void txtMachineIP_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtUnitCode.Text.Trim() == "" || txtMessCode.Text.Trim() == "" )
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";

                sql = "Select MachineIP,MachineDesc from ReaderConfig Where CompCode ='" + txtCompCode.Text.Trim() + "' ";
                      

                if (e.KeyCode == Keys.F1)
                {
                    obj = (List<string>)hlp.Show(sql, "MachineIP", "MachineIP", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else
                {
                    obj = (List<string>)hlp.Show(sql, "MachineDesc", "MachineDesc", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtMachineIP.Text = obj.ElementAt(0).ToString();
                    
                }
            }
        }

        private void LoadGrid()
        {
            DataSet ds = new DataSet();
            string sql = "select CompCode,UnitCode,MessCode,MachineIP From MastMessReader where CompCode = '" + txtCompCode.Text.Trim() + "' Order By MachineIP";

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
                txtMachineIP.Text = gridView1.GetRowCellValue(info.RowHandle, "MachineIP").ToString();
                txtUnitCode.Text = gridView1.GetRowCellValue(info.RowHandle, "UnitCode").ToString();
                txtMessCode.Text = gridView1.GetRowCellValue(info.RowHandle, "MessCode").ToString();
                object o = new object();
                EventArgs e = new EventArgs();                
                mode = "OLD";
                oldCode = txtMachineIP.Text.ToString();
                txtCompCode_Validated(o, e);
                txtUnitCode_Validated(o, e);
                txtMessCode_Validated(o, e);
                txtMachineIP_Leave(o, e);
            }


        }

        private void txtMachineIP_Leave(object sender, EventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtCompName.Text.Trim() == "" ||
                txtUnitCode.Text.Trim() == "" || txtUnitDesc.Text.Trim() == "" ||
                txtMachineIP.Text.Trim() == "")
            {

                return;
            }

            txtMessCode.Text = txtMessCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastMessReader  where CompCode ='" + txtCompCode.Text.Trim() + "' " +
                    " and UnitCode ='" + txtUnitCode.Text.Trim() + "'"
                    + " and MessCode ='" + txtMessCode.Text.Trim() + "' "
                    + " and MachineIP ='" + txtMachineIP.Text.Trim() + "' ";


            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCompCode.Text = dr["CompCode"].ToString();
                    txtUnitCode.Text = dr["UnitCode"].ToString();
                    txtMessCode.Text = dr["MessCode"].ToString();
                    txtMachineIP.Text = dr["MachineIP"].ToString();
                    txtCompCode_Validated(sender, e);
                    txtUnitCode_Validated(sender, e);
                    txtMessCode_Validated(sender, e);
                    mode = "OLD";
                    oldCode = dr["MachineIP"].ToString();
                }
            }
            else
            {
                mode = "NEW";
                oldCode = "";
            }

            SetRights();
        }

    }
}
