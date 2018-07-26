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
    public partial class frmMastRules : Form
    {
        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";

        public frmMastRules()
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

            if (string.IsNullOrEmpty(txtWrkGrpCode.Text))
            {
                err = err + "Please Enter WrkGrpCode..." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtWrkGrpDesc.Text))
            {
                err = err + "Please Enter WrkGrp Description..." + Environment.NewLine;
            }

            
            if (string.IsNullOrEmpty(txtRules.Text))
            {
                err = err + "Please Enter Rules for Search..." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtRuleRepl.Text))
            {
                err = err + "Please Enter Rules for Replace..." + Environment.NewLine;
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
           
            txtWrkGrpCode.Text = "";
            txtWrkGrpDesc.Text = "";
            txtRuleID.Text = "";
            txtRuleRepl.Text = "";
            txtRules.Text = "";

            LoadGrid();
            oldCode = "";
            mode = "NEW";
        }

        private void SetRights()
        {
            if ( txtRuleID.Text.Trim() != "" && mode == "NEW" && GRights.Contains("A") )
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            else if ( txtRuleID.Text.Trim() != "" && mode == "OLD" )
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

        private void txtRuleID_KeyDown(object sender, KeyEventArgs e)
        {
            
            
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select RuleID,WrkGrp,Rules,RuleRepl from AttdRules Where 1 = 1";
                   
                   
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "RuleID", "RuleID", typeof(int), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else
                {
                    obj = (List<string>)hlp.Show(sql, "Rules", "Rules", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtRuleID.Text = obj.ElementAt(0).ToString();
                    txtWrkGrpCode.Text = obj.ElementAt(1).ToString();
                    txtRules.Text = obj.ElementAt(2).ToString();
                    txtRuleRepl.Text = obj.ElementAt(3).ToString();
                    mode = "OLD";
                }
            }
        }

        private void txtRuleID_Validated(object sender, EventArgs e)
        {
            if (txtRuleID.Text.Trim() == "")
            {
                txtRuleID.Text = Utils.Helper.GetDescription("Select isnull(Max(RuleID),0) + 1 from AttdRules where 1 = 1", Utils.Helper.constr);
                txtWrkGrpCode.Text = string.Empty;
                txtWrkGrpDesc.Text = string.Empty;
                txtRules.Text = string.Empty;
                txtRuleRepl.Text = string.Empty;
                mode = "NEW";
                return;
            }

            DataSet ds = new DataSet();
            string sql = "select * From  AttdRules where RuleID ='" + txtRuleID.Text.Trim() + "'";
            
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtRules.Text = dr["Rules"].ToString();
                    txtRuleID.Text = dr["RuleID"].ToString();
                    txtRuleRepl.Text = dr["RuleRepl"].ToString();
                    txtWrkGrpCode.Text = dr["WrkGrp"].ToString();
                    
                    txtWrkGrpCode_Validated(sender,e);
                    mode = "OLD";
                    oldCode = dr["RuleID"].ToString();
                }
            }
            else
            {
                mode = "NEW";
                oldCode = "";
            }

            SetRights();
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
                        txtRuleID.Text = Utils.Helper.GetDescription("Select isnull(Max(RuleID),0) + 1 from AttdRules where 1 = 1", Utils.Helper.constr);
               
                        cn.Open();
                        cmd.Connection = cn;
                        string sql = "Insert into AttdRules " +
                            "(RuleID,WrkGrp,Rules,RuleRepl,AddDt,AddID) " +
                            " Values ('{0}','{1}','{2}','{3}',GetDate(),'{4}')";

                        sql = string.Format(sql,txtRuleID.Text.Trim().ToString(),txtWrkGrpCode.Text.Trim(),
                            txtRules.Text.Trim().ToString(),txtRuleRepl.Text.Trim().ToString(),
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
                        string sql = "Update AttdRules Set WrkGrp ='{0}',Rules='{1}',RuleRepl='{2}',UpdDt=GetDate(),UpdID = '{3}'" 
                           + " Where RuleID = '{4}'  ";

                        sql = string.Format(sql, txtWrkGrpCode.Text.Trim(),txtRules.Text.Trim(),txtRuleRepl.Text.Trim(),
                             Utils.User.GUserID, txtRuleID.Text.Trim().ToString()
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
               
                DialogResult qs = MessageBox.Show("Are You Sure to Delete this Rule...?","Question",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
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
                            string sql = "Delete From AttdRules where RuleID = '" + txtRuleID.Text.Trim().ToString() + "'";
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
            txtWrkGrpCode.Text = "";
            txtWrkGrpDesc.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadGrid()
        {
            

            DataSet ds = new DataSet();
            string sql = "select RuleID,WrkGrp,Rules,RuleRepl " +
                    "  from AttdRules "
                    + " where 1 = 1  Order By RuleID ";

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
                txtRuleID.Text = gridView1.GetRowCellValue(info.RowHandle, "RuleID").ToString();
                txtWrkGrpCode.Text = gridView1.GetRowCellValue(info.RowHandle, "WrkGrp").ToString();
                object o = new object();
                EventArgs e = new EventArgs();                
                mode = "OLD";
                oldCode = txtRuleID.Text.ToString();
                
                txtRuleID_Validated(o, e);
            }

            
        }

        private void txtWrkGrpCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtRules.Text.Trim() == "")
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

        private void txtWrkGrpCode_Validated(object sender, EventArgs e)
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
                   
                    LoadGrid();
                }
            }


        }

        private void frmMastRules_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter))
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }


    }
}
