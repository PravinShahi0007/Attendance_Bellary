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
    public partial class frmUserSpRights : Form
    {
        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";

        public frmUserSpRights()
        {
            InitializeComponent();
        }

        private void frmMastRules_Load(object sender, EventArgs e)
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

            if (string.IsNullOrEmpty(txtUserID.Text))
            {
                err = err + "Please Enter UserID ..." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                err = err + "Please Enter User Name ..." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtWrkGrpCode.Text))
            {
                err = err + "Please Enter WrkGrpCode..." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtWrkGrpDesc.Text))
            {
                err = err + "Please Enter WrkGrp Description..." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtModuleID.Text))
            {
                err = err + "Please Enter Module ID ..." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtModuleDesc.Text))
            {
                err = err + "Please Enter Module Description..." + Environment.NewLine;
            }

            return err;
        }
        
        private void ResetCtrl()
        {
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;

            txtID.Text = "";
            
            ////txtWrkGrpCode.Text = "";
            ////txtWrkGrpDesc.Text = "";
            ////txtUserID.Text = "";
            ////txtUserName.Text = "";
            //txtModuleID.Text = "";
            //txtModuleDesc.Text = "";

            oldCode = "";
            mode = "NEW";
            grid.DataSource = null;
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

                if(GRights.Contains("U"))
                    btnDelete.Enabled = true;
                if (GRights.Contains("D"))
                    btnDelete.Enabled = true;
            }

            if (GRights.Contains("XXXV"))
            {
                btnAdd.Enabled = false;
                
                btnDelete.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            
            string err = DataValidate();

            string tID = Utils.Helper.GetDescription("Select isnull(Max(SrNo),0) + 1 from UserSpRight where 1 = 1", Utils.Helper.constr);


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
                        string sql = "Insert into UserSPRight " +
                            "(UserID,WrkGrp,FormID,AddDt,AddID) " +
                            " Values ('{0}','{1}','{2}',GetDate(),'{3}')";

                        sql = string.Format(sql,
                            
                            txtUserID.Text.Trim(),                            
                            txtWrkGrpCode.Text.Trim(),
                            txtModuleID.Text.Trim(),
                            Utils.User.GUserID);

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
                            string sql = "Delete From UserSPRight where SrNo = '" + txtID.Text.Trim().ToString() + "'";
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
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
                return;

            DataSet ds = new DataSet();
            string sql = "select a.SrNo,a.UserID,a.WrkGrp,a.FormID as ModuleID,b.FormDesc as ModuleDesc " +
                    "  from UserSPRight a , MastFrm b  Where "
                    + " a.FormID = b.FormID and a.UserID = '" + txtUserID.Text.Trim() + "'";

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
                txtID.Text = gridView1.GetRowCellValue(info.RowHandle, "SrNo").ToString();
                txtWrkGrpCode.Text = gridView1.GetRowCellValue(info.RowHandle, "WrkGrp").ToString();
                txtUserID.Text = gridView1.GetRowCellValue(info.RowHandle, "UserID").ToString();
                txtModuleID.Text = gridView1.GetRowCellValue(info.RowHandle, "ModuleID").ToString();

                object o = new object();
                EventArgs e = new EventArgs();                
                mode = "OLD";
                oldCode = txtID.Text.ToString();
                
                txtID_Validated(o, e);
            }

            
        }

        private void txtWrkGrpCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtUserID.Text.Trim() == "")
                return;

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

        private void txtUserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select UserID,UserName From MastUser Where Active = 'Y'  ";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "UserID", "UserID", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else if (e.KeyCode == Keys.F2)
                {

                    obj = (List<string>)hlp.Show(sql, "UserName", "UserName", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 200, 400, 600, 100, 100);
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

                    txtUserID.Text = obj.ElementAt(0).ToString();
                    txtUserName.Text = obj.ElementAt(1).ToString();
                    
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


                sql = "Select SrNo,UserID,WrkGrp,FormID from UserSpRight Where active = 1";


                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "SrNo", "SrNo", typeof(int), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else
                {
                    obj = (List<string>)hlp.Show(sql, "UserID", "UserID", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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
                    txtUserID.Text = obj.ElementAt(1).ToString();
                    txtWrkGrpCode.Text = obj.ElementAt(2).ToString();
                    txtModuleID.Text = obj.ElementAt(3).ToString();

                    txtID_Validated(sender, e);

                    mode = "OLD";
                }
            }
        }

        private void txtModuleID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select FormID,FormDesc From MastFrm where 1 = 1  ";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "FormID", "FormID", typeof(int), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else if (e.KeyCode == Keys.F2)
                {

                    obj = (List<string>)hlp.Show(sql, "FormDesc", "FormDesc", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 200, 400, 600, 100, 100);
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

                    txtModuleID.Text = obj.ElementAt(0).ToString();
                    txtModuleDesc.Text = obj.ElementAt(1).ToString();

                }
            }
        }

        private void frmUserSpRights_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter))
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void txtModuleID_Validated(object sender, EventArgs e)
        {
            
            DataSet ds = Utils.Helper.GetData("Select FormID,FormDesc from  MastFrm Where Formid = '" + txtModuleID.Text.Trim() + "'",Utils.Helper.constr);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    txtModuleID.Text = dr["FormID"].ToString();
                    txtModuleDesc.Text = dr["FormDesc"].ToString();

                    if(!string.IsNullOrEmpty(txtUserID.Text.Trim()) && !string.IsNullOrEmpty(txtWrkGrpDesc.Text.Trim()))
                    {
                        //if already exist
                        ds = Utils.Helper.GetData("Select * From UserSpRight where UserID ='" + txtUserID.Text.Trim()
                            + "' And WrkGrp = '" + txtWrkGrpCode.Text.Trim() + "' And FormID ='" + txtModuleID.Text.Trim() + "'",
                            Utils.Helper.constr
                            );
                        bool hasrows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                        if (hasrows)
                        {
                            foreach (DataRow drw in ds.Tables[0].Rows)
                            {
                                txtID.Text = drw["SrNo"].ToString();
                                oldCode = drw["SrNo"].ToString();
                                mode = "OLD";
                            }                            
                        }
                    }

                }
                else
                {
                    txtModuleID.Text = "";
                    txtModuleDesc.Text = "";
                }
            }
            else
            {
                txtModuleID.Text = "";
                txtModuleDesc.Text = "";                
            }

            ValidateAll();
            SetRights();
        }

        private void txtWrkGrpCode_Validated(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtWrkGrpCode.Text.Trim()))
            {
                txtWrkGrpDesc.Text = "";
                mode = "New";
                SetRights();
                return;
            }
            
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
            else
            {
                txtWrkGrpDesc.Text = "";
            }
            ValidateAll();
        }

        private void ValidateAll()
        {
            string sql = "Select * from UserSpRight where Userid ='" + txtUserID.Text.Trim() + "' and WrkGrp ='" + txtWrkGrpCode.Text.Trim() + "' and FormID ='" + txtModuleID.Text.Trim() + "'";
            DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);

            bool hasrows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
            if (hasrows)
            {
                mode = "OLD";
                foreach (DataRow drw in ds.Tables[0].Rows)
                {
                    txtID.Text = drw["SrNo"].ToString();
                }
            }
            else
            {
                mode = "NEW";
                txtID.Text = "";                
            }

            if (txtID.Text.Trim() == "")
            {
                txtID.Text = Utils.Helper.GetDescription("Select isnull(Max(SrNo),0) + 1 from UserSpRight where 1 = 1", Utils.Helper.constr);

                oldCode = "";
                mode = "NEW";
                
            }

            SetRights();
        }


        private void txtUserID_Validated(object sender, EventArgs e)
        {
            

            DataSet ds = Utils.Helper.GetData("Select * from MastUser Where UserID ='" + txtUserID.Text.Trim() + "'", Utils.Helper.constr);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    txtUserID.Text = dr["UserID"].ToString();
                    txtUserName.Text = dr["UserName"].ToString();
                    
                }
            }

            ValidateAll();

            LoadGrid();
        }

        private void txtID_Validated(object sender, EventArgs e)
        {
            if (txtID.Text.Trim() == "")
            {
                txtID.Text = Utils.Helper.GetDescription("Select isnull(Max(SrNo),0) + 1 from UserSpRight where 1 = 1", Utils.Helper.constr);

                oldCode = "";
                mode = "NEW";
                SetRights();
                return;
            }

            DataSet ds = new DataSet();
            string sql = "select * From  UserSPRight where SrNo ='" + txtID.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    txtID.Text = dr["SrNo"].ToString();
                    txtUserID.Text = dr["UserID"].ToString();
                    txtWrkGrpCode.Text = dr["WrkGrp"].ToString();
                    txtModuleID.Text = dr["FormID"].ToString();
                    mode = "OLD";
                    oldCode = dr["SrNo"].ToString();

                    txtUserID_Validated(sender, e);
                    txtWrkGrpCode_Validated(sender, e);
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
