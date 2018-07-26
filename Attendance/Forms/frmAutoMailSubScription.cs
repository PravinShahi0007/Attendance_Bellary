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
using Attendance.Classes;

namespace Attendance.Forms
{
    public partial class frmAutoMailSubScription : Form
    {
        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";
        public bool GridEditMode = false;

        public string oldWrkGrp = string.Empty;
        public string oldDeptStat = string.Empty;


        public frmAutoMailSubScription()
        {
            InitializeComponent();
        }

        private void frmAutoMailSubScription_Load(object sender, EventArgs e)
        {
            ResetCtrl();
            GRights = Attendance.Classes.Globals.GetFormRights(this.Name);
            SetRights();
            
        }

        private string DataValidate()
        {
            string err = string.Empty;

            if (string.IsNullOrEmpty(txtID.Text))
            {
                err = err + "Please Enter ID ..." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                err = err + "Please Enter Email To ..." + Environment.NewLine;
            }


            if (string.IsNullOrEmpty(txtWrkGrpCode.Text))
            {
                err = err + "Please Enter WrkGrpCode..." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtWrkGrpDesc.Text))
            {
                err = err + "Please Enter WrkGrp Description..." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(Globals.G_DefaultMailID))
            {
                err = err + "Default Mail ID is not configured, please configured from other configuration..." + Environment.NewLine;
            }

            return err;
        }
        
        private void ResetCtrl()
        {
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;

            txtID.Text = "";
            
            txtWrkGrpCode.Text = "";
            txtWrkGrpDesc.Text = "";
            txtUnitCode.Text = "";
            txtUnitDesc.Text = "";
            txtDeptCode.Text = "";
            txtDeptDesc.Text = "";
            txtStatCode.Text = "";
            txtStatDesc.Text = "";

            txtEmail.Text = "";
            txtEmailCopy.Text = "";
            txtBCCTo.Text = "";


            oldCode = "";
            mode = "NEW";
            

            txtWrkGrpCode.Enabled = true;
            txtUnitCode.Enabled = true;
            txtDeptCode.Enabled = true;
            txtStatCode.Enabled = true;

            grid.DataSource = null;
            GridEditMode = false;

            oldWrkGrp = string.Empty;
            oldDeptStat = string.Empty;

        }

        private void SetRights()
        {
            if ( txtID.Text.Trim() != "" && mode == "NEW" && GRights.Contains("A") )
            {
                btnAdd.Enabled = true;
               
                btnDelete.Enabled = false;
            }
            else if ( txtID.Text.Trim() != "" && mode == "OLD" )
            {
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnAddStat.Enabled = false;
                btnDeleteStat.Enabled = false;

                if (GRights.Contains("U"))
                {
                    btnUpdate.Enabled = true;
                    btnAddStat.Enabled = true;
                    btnDeleteStat.Enabled = true;
                }

                if (GRights.Contains("D"))
                {
                    btnDelete.Enabled = true;
                    btnAddStat.Enabled = true;
                    btnDeleteStat.Enabled = true;
                }
                    
            }

            if (GRights.Contains("XXXV"))
            {
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnAddStat.Enabled = false;
                btnDeleteStat.Enabled = false;
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
            
            string tID = Utils.Helper.GetDescription("Select isnull(Max(SubScriptionID),0) + 1 from AutomailSubScription where 1 = 1", Utils.Helper.constr);


            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        string sql = "Insert into AutomailSubScription " +
                            "(SubScriptionID,EmailTo,EmailCopy,BCCTo,ReplyTo,AddDt,AddID,Format,param1WrkGrp) " +
                            " Values ('{0}','{1}','{2}','{3}','{4}',GetDate(),'{5}','Excel','{6}')";

                        sql = string.Format(sql,tID,
                            txtEmail.Text.Trim().ToString(),
                            txtEmailCopy.Text.Trim().ToString(),
                            txtBCCTo.Text.Trim().ToString(),
                            Globals.G_DefaultMailID,
                            Utils.User.GUserID,
                            txtWrkGrpCode.Text.Trim()
                            );

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
               
                DialogResult qs = MessageBox.Show("Are You Sure to Delete this Record...?","Question",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
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
                            string sql = "Delete From AutoMailSubScription where SubScriptionID = '" + txtID.Text.Trim().ToString() + "'";
                            cmd.CommandText = sql;
                            cmd.Connection = cn;
                            cmd.ExecuteNonQuery();

                            sql = "Delete From AutoMailDeptStat Where SubScriptionID = '" + txtID.Text.Trim().ToString() + "'";
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Record Deleted...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetCtrl();
                           
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
            if (string.IsNullOrEmpty(txtID.Text.Trim()))
                return;

            DataSet ds = new DataSet();
            string sql = "Select b.WrkGrp, b.UnitCode,a.DeptStat,b.DeptCode,d.DeptDesc,b.StatCode,b.StatDesc "
                       + " From AutomailDeptStat a, "
                       + " mastStat b , MastDept d "
                       + "  Where  "
                       + " a.WRKGRP=b.WrkGrp "
                       + " and a.WRKGRP = d.WrkGrp "
                       + " and Left(a.DeptStat,3) = d.DeptCode "
                       + " and Left(a.DeptStat,3) = b.DeptCode "
                       + " and RIGHT(a.DeptStat,3) = b.StatCode "
                       + " and b.CompCode = '01' and d.CompCode = '01' "
                       + " and a.SubScriptionID ='" + txtID.Text.Trim() + "' ";

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
                //txtID.Text = gridView1.GetRowCellValue(info.RowHandle, "SubScriptionID").ToString();
                txtWrkGrpCode.Text = gridView1.GetRowCellValue(info.RowHandle, "WrkGrp").ToString();
                txtUnitCode.Text = gridView1.GetRowCellValue(info.RowHandle, "UnitCode").ToString();
                txtDeptCode.Text = gridView1.GetRowCellValue(info.RowHandle, "DeptCode").ToString();
                txtDeptDesc.Text = gridView1.GetRowCellValue(info.RowHandle, "DeptDesc").ToString();
                txtStatCode.Text = gridView1.GetRowCellValue(info.RowHandle, "StatCode").ToString();
                txtStatDesc.Text = gridView1.GetRowCellValue(info.RowHandle, "DeptDesc").ToString();

                oldWrkGrp = gridView1.GetRowCellValue(info.RowHandle, "WrkGrp").ToString();
                oldDeptStat = gridView1.GetRowCellValue(info.RowHandle, "DeptCode").ToString() + gridView1.GetRowCellValue(info.RowHandle, "StatCode").ToString();


                txtUnitCode.Enabled = false;
                object sender = new object();
                EventArgs e = new EventArgs();

                txtWrkGrpCode_EditValueChanged(sender, e);                
                txtUnitCode_Validated(sender, e);
                txtDeptCode_Validated(sender, e);
                txtStatCode_Validated(sender, e);

                txtDeptCode.Enabled = false;
                
                txtStatCode.Enabled = false;
                txtWrkGrpCode.Enabled = false;

                GridEditMode = true;
            }
            

            
        }

        private void txtWrkGrpCode_KeyDown(object sender, KeyEventArgs e)
        {
            

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select WrkGrp,WrkGrpDesc From MastWorkGrp Where 1 = 1 " ;
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

        private void txtID_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select SubScriptionID,EmailTo,EmailCopy,BCCTo from AutoMailSubscription where 1 = 1";


                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "SubScriptionID", "SubScriptionID", typeof(int), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else if(e.KeyCode == Keys.F2)
                {
                    obj = (List<string>)hlp.Show(sql, "EmailTo", "EmailTo", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else if (e.KeyCode == Keys.F3)
                {
                    obj = (List<string>)hlp.Show(sql, "EmailCopy", "EmailCopy", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                  100, 300, 400, 600, 100, 100);
                }
                else if (e.KeyCode == Keys.F4)
                {
                    obj = (List<string>)hlp.Show(sql, "BccTo", "BccTo", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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
                    txtID.Text = obj.ElementAt(0).ToString();
                    txtEmail.Text = obj.ElementAt(1).ToString();                    
                    txtID_EditValueChanged (sender, e);
                    mode = "OLD";
                }
            }
        }

        private void txtID_EditValueChanged(object sender, EventArgs e)
        {
            if (txtID.Text.Trim() == "")
            {
                txtID.Text = Utils.Helper.GetDescription("Select isnull(Max(SubScriptionID),0) + 1 from AutoMailSubScription where 1 = 1", Utils.Helper.constr);


                GridEditMode = false;
                oldWrkGrp = string.Empty;
                oldDeptStat = string.Empty;
                txtWrkGrpCode.Enabled = true;
                oldCode = "";
                mode = "NEW";
                return;
            }

            DataSet ds = new DataSet();
            string sql = "select * From  AutoMailSubScription where SubScriptionID ='" + txtID.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    txtID.Text = dr["SubScriptionID"].ToString();
                    txtEmail.Text = dr["EmailTo"].ToString();
                    txtEmailCopy.Text = dr["EmailCopy"].ToString();
                    txtBCCTo.Text = dr["BCCTo"].ToString();
                    txtWrkGrpCode.Text = dr["param1wrkGrp"].ToString();

                    mode = "OLD";
                    oldCode = dr["SubScriptionID"].ToString();
                    GridEditMode = false;
                    oldWrkGrp = string.Empty;
                    oldDeptStat = string.Empty;
                    txtWrkGrpCode.Enabled = false;
                    LoadGrid();
                }
            }
            else
            {
                mode = "NEW";
                oldCode = "";
                GridEditMode = false;
                oldWrkGrp = string.Empty;
                oldDeptStat = string.Empty;
                txtWrkGrpCode.Enabled = true;
            }

            SetRights();
        }

        private void txtWrkGrpCode_EditValueChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string sql = "select * From MastWorkGrp where CompCode ='01'  and WrkGrp='" + txtWrkGrpCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    txtWrkGrpCode.Text = dr["WrkGrp"].ToString();
                    txtWrkGrpDesc.Text = dr["WrkGrpDesc"].ToString();
                   
                }
            }
        }

        private void txtUnitCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtWrkGrpCode.Text.Trim() == "" || txtWrkGrpDesc.Text.Trim() == "")
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select UnitCode,UnitName From MastUnit Where CompCode ='01' and WrkGrp = '" + txtWrkGrpCode.Text.Trim() + "' ";
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
            if ( txtWrkGrpCode.Text.Trim() == "" || txtWrkGrpDesc.Text.Trim() == "")
            {

                return;
            }

            txtUnitCode.Text = txtUnitCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastUnit where CompCode ='01' " +
                    " and WrkGrp='" + txtWrkGrpCode.Text.Trim() + "' and UnitCode ='" + txtUnitCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    
                    txtWrkGrpCode.Text = dr["WrkGrp"].ToString();
                    txtUnitCode.Text = dr["UnitCode"].ToString();
                    txtUnitDesc.Text = dr["UnitName"].ToString();
                    
                    txtWrkGrpCode_EditValueChanged(sender, e);

                }
            }
        }

        private void txtDeptCode_KeyDown(object sender, KeyEventArgs e)
        {
            if ( txtWrkGrpCode.Text.Trim() == "" 
                || txtWrkGrpDesc.Text.Trim() == ""
                || txtUnitCode.Text.Trim() == "")
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";

                sql = "Select DeptCode,DeptDesc From MastDept Where CompCode ='01' " +
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
            if ( txtWrkGrpCode.Text.Trim() == ""
                || txtWrkGrpDesc.Text.Trim() == ""
                || txtUnitCode.Text.Trim() == "")
            {

                return;
            }

            txtDeptCode.Text = txtDeptCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastDept where CompCode ='01' " +
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
                    
                    txtWrkGrpCode.Text = dr["WrkGrp"].ToString();
                    txtUnitCode.Text = dr["UnitCode"].ToString();
                    txtDeptCode.Text = dr["DeptCode"].ToString();
                    txtDeptDesc.Text = dr["DeptDesc"].ToString();
                   
                    txtWrkGrpCode_EditValueChanged(sender, e);
                    txtUnitCode_Validated(sender, e);
                }
            }

        }

        private void txtStatCode_KeyDown(object sender, KeyEventArgs e)
        {
            if ( txtWrkGrpCode.Text.Trim() == ""
                || txtUnitCode.Text.Trim() == "" || txtDeptCode.Text.Trim() == "")
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";

                sql = "Select StatCode,StatDesc From MastStat Where CompCode ='01' " +
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
            if ( txtWrkGrpCode.Text.Trim() == "" || txtWrkGrpDesc.Text.Trim() == ""
                || txtUnitCode.Text.Trim() == "" || txtUnitDesc.Text.Trim() == ""
                || txtDeptCode.Text.Trim() == "" || txtDeptDesc.Text.Trim() == ""
                || txtStatCode.Text.Trim() == "" 
                )
            {

                return;
            }

            txtStatCode.Text = txtStatCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastStat where CompCode ='01' " +
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
                    
                    txtWrkGrpCode.Text = dr["WrkGrp"].ToString();
                    txtUnitCode.Text = dr["UnitCode"].ToString();
                    txtDeptCode.Text = dr["DeptCode"].ToString();

                    txtStatCode.Text = dr["StatCode"].ToString();
                    txtStatDesc.Text = dr["StatDesc"].ToString();
                   
                    txtWrkGrpCode_EditValueChanged(sender, e);
                    txtUnitCode_Validated(sender, e);
                    txtDeptCode_Validated(sender, e);


                    //check if record exist in AutoMailDeptStat
                    sql = "Select Count(*) from AutoMailDeptStat Where SubScriptionID ='{0}' and WrkGrp = '{1}' and DeptStat = '{2}'";
                    sql = string.Format(sql,txtID.Text.Trim(),txtWrkGrpCode.Text.Trim(),
                        txtDeptCode.Text.Trim().ToString() + txtStatCode.Text.Trim().ToString()
                        );
                    string t = Utils.Helper.GetDescription(sql, Utils.Helper.constr);
                    int cnt = 0;
                    if(int.TryParse(t,out cnt))
                    {
                        if (cnt > 0)
                        {
                            GridEditMode = true;
                            oldWrkGrp = txtWrkGrpCode.Text.Trim().ToString();
                            oldDeptStat = txtDeptCode.Text.Trim().ToString() + txtStatCode.Text.Trim().ToString();

                            txtUnitCode.Enabled = false;
                            txtDeptCode.Enabled = false;
                            txtStatCode.Enabled = false;
                            
                        }
                        else
                        {
                            txtUnitCode.Enabled = true;
                            txtDeptCode.Enabled = true;
                            txtStatCode.Enabled = true;
                            

                            GridEditMode = false;
                            oldWrkGrp = string.Empty;
                            oldDeptStat = string.Empty;
                        }
                    }

                }
            }
            

            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (mode != "OLD")
            {
                return;
            }
            
            string err = DataValidate();

            //string tID = Utils.Helper.GetDescription("Select isnull(Max(SrNo),0) + 1 from AutomailSubScription where 1 = 1", Utils.Helper.constr);


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
                        string sql = "Update AutomailSubScription " +
                            "Set EmailTo = '{0}',EmailCopy = '{1}' ,BCCTo = '{2}',ReplyTo = '{3}',UpdDt = GetDate(),UpdID ='{4}', " +
                            " Format = 'Excel' Where SubScriptionID = '{5}' and param1WrkGrp ='{6}' ";

                        sql = string.Format(sql,
                            txtEmail.Text.Trim().ToString(),
                            txtEmailCopy.Text.Trim().ToString(),
                            txtBCCTo.Text.Trim().ToString(),
                            Globals.G_DefaultMailID,
                            Utils.User.GUserID,
                            txtID.Text.Trim().ToString(),
                            txtWrkGrpCode.Text.Trim()
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

        private void btnAddStat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text.Trim()))
                return;

            if(string.IsNullOrEmpty(txtWrkGrpCode.Text.Trim()) || string.IsNullOrEmpty(txtWrkGrpDesc.Text.Trim())
                || string.IsNullOrEmpty(txtUnitCode.Text.Trim()) || string.IsNullOrEmpty(txtUnitDesc.Text.Trim())
                || string.IsNullOrEmpty(txtDeptCode.Text.Trim()) || string.IsNullOrEmpty(txtDeptDesc.Text.Trim())
                || string.IsNullOrEmpty(txtStatCode.Text.Trim()) || string.IsNullOrEmpty(txtStatDesc.Text.Trim())
                )
            {
                return;
            }

            //check if subscription exist or not
            string t = Utils.Helper.GetDescription("Select Count(*) from AutoMailSubScription Where SubScriptionID ='" + txtID.Text.Trim() + "'", Utils.Helper.constr);
        
            int chk = 0;
            if (int.TryParse(t, out chk))
            {
                if (chk <= 0)
                {
                    MessageBox.Show("Please Save Mail Detail First...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            //check for same wrkgrp
            DataSet ds = Utils.Helper.GetData("Select Distinct WrkGrp from AutoMailDeptStat where SubScriptionID ='" + txtID.Text.Trim() + "'", Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                if (ds.Tables[0].Rows.Count > 1)
                {
                    MessageBox.Show("Please Enter Same WrkGrp...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string prvwrkgrp = string.Empty;
                //check for current selected wrkgrp
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    prvwrkgrp = dr["WrkGrp"].ToString();
                }

                if (txtWrkGrpCode.Text.Trim().ToString() != prvwrkgrp)
                {
                    MessageBox.Show("Please Enter Same WrkGrp...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    string sql = string.Empty;
                    cn.Open();

                    
                    
                    if (GridEditMode && oldWrkGrp != string.Empty && oldDeptStat != string.Empty)
                    {
                        sql = "Update AutoMailDeptStat set DeptStat ='" + txtDeptCode.Text.Trim().ToString() + txtStatCode.Text.Trim().ToString() + "' "
                        + " Where SubScriptionID ='" + txtID.Text.Trim().ToString() + "' And DeptStat ='" + oldDeptStat + "' and WrkGrp ='" + oldWrkGrp + "'";
                    }
                    else
                    {
                        sql = "insert into AutoMailDeptStat (SubScriptionID,WrkGrp,DeptStat,AddDt,AddID) Values ('{0}','{1}','{2}',GetDate(),'{3}')";

                        sql = string.Format(sql, txtID.Text.Trim().ToString(), txtWrkGrpCode.Text.Trim(), txtDeptCode.Text.Trim().ToString() + txtStatCode.Text.Trim().ToString(), Utils.User.GUserID);
                    }

                    SqlCommand cmd = new SqlCommand(sql,cn);
                    cmd.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                
                txtUnitCode.Text = "";
                txtUnitDesc.Text = "";
                txtDeptCode.Text = "";
                txtDeptDesc.Text = "";
                txtStatCode.Text = "";
                txtStatDesc.Text = "";                
            }

            txtUnitCode.Enabled = true;
            txtDeptCode.Enabled = true;
            txtStatCode.Enabled = true;
            

            oldWrkGrp = string.Empty;
            oldDeptStat = string.Empty;

            GridEditMode = false;
            LoadGrid();
        }

        private void btnDeleteStat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text.Trim()))
                return;

            if (string.IsNullOrEmpty(txtWrkGrpCode.Text.Trim()) || string.IsNullOrEmpty(txtWrkGrpDesc.Text.Trim())
                || string.IsNullOrEmpty(txtUnitCode.Text.Trim()) || string.IsNullOrEmpty(txtUnitDesc.Text.Trim())
                || string.IsNullOrEmpty(txtDeptCode.Text.Trim()) || string.IsNullOrEmpty(txtDeptDesc.Text.Trim())
                || string.IsNullOrEmpty(txtStatCode.Text.Trim()) || string.IsNullOrEmpty(txtStatDesc.Text.Trim())
                )
            {
                return;
            }

            //check if subscription exist or not
            string t = Utils.Helper.GetDescription("Select Count(*) from AutoMailSubScription Where SubScriptionID ='" + txtID.Text.Trim() + "'", Utils.Helper.constr);

            int chk = 0;
            if (int.TryParse(t, out chk))
            {
                if (chk <= 0)
                {
                    MessageBox.Show("Please Save Mail Detail First...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            //check for same wrkgrp
            DataSet ds = Utils.Helper.GetData("Select Distinct WrkGrp from AutoMailDeptStat where SubScriptionID ='" + txtID.Text.Trim() + "'", Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                if (ds.Tables[0].Rows.Count > 1)
                {
                    MessageBox.Show("Please Enter Same WrkGrp...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string prvwrkgrp = string.Empty;
                //check for current selected wrkgrp
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    prvwrkgrp = dr["WrkGrp"].ToString();
                }

                if (txtWrkGrpCode.Text.Trim().ToString() != prvwrkgrp)
                {
                    MessageBox.Show("Please Enter Same WrkGrp...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    string sql = string.Empty;
                    cn.Open();



                    if (GridEditMode && oldWrkGrp != string.Empty && oldDeptStat != string.Empty)
                    {
                        sql = "Delete From AutoMailDeptStat Where SubScriptionID ='{0}' and WrkGrp = '{1}' And DeptStat = '{2}' ";
                        sql = string.Format(sql, txtID.Text.Trim(), txtWrkGrpCode.Text.Trim(), txtDeptCode.Text.Trim().ToString() + txtStatCode.Text.Trim().ToString(), Utils.User.GUserID);
                    }

                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

               
                txtUnitCode.Text = "";
                txtUnitDesc.Text = "";
                txtDeptCode.Text = "";
                txtDeptDesc.Text = "";
                txtStatCode.Text = "";
                txtStatDesc.Text = "";
            }

            txtUnitCode.Enabled = true;
            txtDeptCode.Enabled = true;
            txtStatCode.Enabled = true;
            
            oldWrkGrp = string.Empty;
            oldDeptStat = string.Empty;

            GridEditMode = false;
            LoadGrid();
        }

        private void frmAutoMailSubScription_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter))
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }
    }
}
