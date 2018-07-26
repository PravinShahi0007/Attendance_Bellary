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
    public partial class frmMastCostCodeSanManPower : Form
    {
        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";

        public frmMastCostCodeSanManPower()
        {
            InitializeComponent();
        }

        private void frmMastCostCodeSanManPower_Load(object sender, EventArgs e)
        {
            ResetCtrl();
            GRights = Attendance.Classes.Globals.GetFormRights(this.Name);
            SetRights();
            
            
        }

        private string DataValidate()
        {
            string err = string.Empty;

            if (string.IsNullOrEmpty(txtCostCode.Text))
            {
                err = err + "Please Enter CostCode..." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                err = err + "Please Enter Description..." + Environment.NewLine;
            }

            if (txtValidFrom.EditValue == null)
            {
                err = err + "Please Enter Valid From..." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtCompEmp.Text))
            {
                err = err + "Please Enter Nos of Comp On Roll Employee..." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtContEmp.Text))
            {
                err = err + "Please Enter Nos of Cont On Roll Employee..." + Environment.NewLine;
            }


            int t = 0;

            if(int.TryParse(txtCompEmp.Text.Trim(),out t))
            {
                if(t <0 ){
                    err = err + "Please Enter Nos of Comp On Roll Employee..." + Environment.NewLine;
                }
            }


            if(int.TryParse(txtContEmp.Text.Trim(),out t))
            {
                if(t <0 ){
                    err = err + "Please Enter Nos of Contractual Employee..." + Environment.NewLine;
                }
            }

            string sql = "Select max(ValidFrom) From MastCostCodeSanctionManPower where CostCode = '" + txtCostCode.Text.Trim().ToString() + "' " +
                " and ValidFrom > '" + txtValidFrom.DateTime.ToString("yyyy-MM-dd") + "'";

            string tMaxDt = Utils.Helper.GetDescription(sql, Utils.Helper.constr);

            if (!string.IsNullOrEmpty(tMaxDt))
            {
                err = err + "System Does not Allow to insert/update/delete between...." + Environment.NewLine;
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
            //txtCostCode.Text = "";
            //txtDescription.Text = "";
            txtValidFrom.EditValue = null;
            txtContEmp.EditValue = 0;
            txtCompEmp.EditValue = 0;
            txtCostCode.Enabled = true;
            txtValidFrom.Enabled = true;

            grid.DataSource = null;
            oldCode = "";
            mode = "NEW";
        }
        
        private void SetRights()
        {
            if ( txtCostCode.Text.Trim() != "" && mode == "NEW" && GRights.Contains("A") )
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            else if (txtCostCode.Text.Trim() != "" && mode == "OLD")
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

        private void txtCostCode_Validated(object sender, EventArgs e)
        {
            if (txtCostCode.Text.Trim() == "")
            {   
                return;
            }

            DataSet ds = new DataSet();
            string sql = "select * from MastCostCode where CostCode ='" + txtCostCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCostCode.Text = dr["CostCode"].ToString();
                    txtDescription.Text = dr["CostDesc"].ToString();                    
                    LoadGrid();
                }
            }
        }

        private void txtCostCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 )
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select CostCode,CostDesc From MastCostCode Where 1 = 1";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "CostCode", "CostCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else
                {
                    obj = (List<string>)hlp.Show(sql, "CostDesc", "CostDesc", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtCostCode.Text = obj.ElementAt(0).ToString();
                    txtDescription.Text = obj.ElementAt(1).ToString();
                   
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
                        string sql = "Insert into MastCostCodeSanctionManPower (CostCode,ValidFrom,CompEmp,ContEmp,AddDt,AddID) "
                        + " Values ('{0}','{1}','{2}','{3}',GetDate(),'{4}')";
                        sql = string.Format(sql, txtCostCode.Text.Trim().ToString().ToUpper(),
                            txtValidFrom.DateTime.ToString("yyyy-MM-dd"),
                            txtCompEmp.EditValue,txtContEmp.EditValue,
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

            //MessageBox.Show("Not Implemented....", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);           

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        string sql = "Update MastCostCodeSanctionManPower set "
                        + " CompEmp = '{0}', ContEmp = '{1}',   UpdDt = GetDate(),UpdID ='{2}' where CostCode = '{3}' " 
                        + " and ValidFrom ='{4}'" ;
                        sql = string.Format(sql, txtCompEmp.EditValue, txtContEmp.EditValue,
                            Utils.User.GUserID,
                            txtCostCode.Text.Trim().ToString().ToUpper(),
                            txtValidFrom.DateTime.ToString("yyyy-MM-dd")
                            );

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

                DialogResult qs = MessageBox.Show("Are You Sure to Delete this Record...?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                            string sql = "Delete From MastCostCodeSanctionManPower " 
                            + " where CostCode = '" + txtCostCode.Text.Trim() + "' "
                            + " and ValidFrom ='" + Convert.ToDateTime(txtValidFrom.EditValue).ToString("yyyy-MM-dd") + "'";
                                
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

        private void LoadGrid()
        {
            DataSet ds = new DataSet();
            string sql = "Select CostCode,ValidFrom,CompEmp,ContEmp From MastCostCodeSanctionManPower where " 
            + " CostCode ='" + txtCostCode.Text.Trim().ToString() + "'";

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
                txtCostCode.Text = gridView1.GetRowCellValue(info.RowHandle, "CostCode").ToString();
                txtValidFrom.EditValue = gridView1.GetRowCellValue(info.RowHandle, "ValidFrom").ToString();

                object o = new object();
                EventArgs e = new EventArgs();                
                mode = "OLD";
                oldCode = Convert.ToDateTime(txtValidFrom.EditValue).ToString("yyyy-MM-dd");
                txtValidFrom_EditValueChanged(o, e);
                
            }


        }

        private void txtValidFrom_EditValueChanged(object sender, EventArgs e)
        {
            if (txtValidFrom.EditValue == null || txtCostCode.Text.Trim().ToString() == "")
            {
                return;
            }


            DataSet ds = new DataSet();
            string sql = "select * From MastCostCodeSanctionManPower where CostCode ='" + txtCostCode.Text.Trim().ToString() + "' " +
                    " and ValidFrom ='" + txtValidFrom.DateTime.ToString("yyyy-MM-dd") + "' ";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCostCode.Text = dr["CostCode"].ToString();
                    txtCompEmp.Text = dr["CompEmp"].ToString();
                    txtContEmp.Text = dr["ContEmp"].ToString();
                    txtCostCode_Validated(sender, e);
                    mode = "OLD";
                    oldCode = Convert.ToDateTime(dr["ValidFrom"]).ToString("yyyy-MM-dd");
                    txtCostCode.Enabled = false;
                    txtValidFrom.Enabled = false;
                }
            }
            else
            {
                mode = "NEW";
                txtCostCode.Enabled = true;
                txtValidFrom.Enabled = true;
            }

            SetRights();
        }

        private void frmMastCostCodeSanManPower_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter))
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        

    }
}
