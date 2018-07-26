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
    public partial class frmMastCostCodeEmp : Form
    {
        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";

        public frmMastCostCodeEmp()
        {
            InitializeComponent();
            this.ctrlEmp1.EmpUnqIDValidated += new EventHandler(this.ctrlEmpValidateEvent_Handler);
            //this.ctrlEmp1.CompCodeValidated += new EventHandler(this.ctrlCompValidateEvent_Handler);
        }

        private void ctrlEmpValidateEvent_Handler(object sender, EventArgs e)
        {
            if (!ctrlEmp1.cEmp.Active)
            {
                grid.DataSource = null;
            }
            else
            {
                LoadGrid();
            } 
        }

        //private void ctrlCompValidateEvent_Handler(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(ctrlEmp1.txtCompCode.Text.Trim()))
        //        return;
            

        //}

        private void frmMastCostCodeEmp_Load(object sender, EventArgs e)
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
                err = err + "Please Enter Valid From Date..." + Environment.NewLine;
            }
            

            string sql = "Select max(ValidFrom) From MastCostCodeEmp where EmpUnqId = '" + ctrlEmp1.cEmp.EmpUnqID + "' " +
                " and ValidFrom > '" + txtValidFrom.DateTime.ToString("yyyy-MM-dd") + "'";

            string tMaxDt = Utils.Helper.GetDescription(sql,Utils.Helper.constr);
    
            if(!string.IsNullOrEmpty(tMaxDt))
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

            ctrlEmp1.ResetCtrl();
            txtCostCode.Text = "";
            txtDescription.Text = "";
            grid.DataSource = null;
            txtCostCode.Enabled = true;
            txtValidFrom.Enabled = true;
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
                   
                }
            }
            
        }

        private void txtCostCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
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
                    mode = "OLD";
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
                        string sql = "Insert into MastCostCodeEmp (EmpUnqID,CostCode,ValidFrom,AddDt,AddID) Values ('{0}','{1}','{2}',GetDate(),'{3}')";
                        sql = string.Format(sql, ctrlEmp1.txtEmpUnqID.Text.Trim(), txtCostCode.Text.Trim().ToString().ToUpper(),
                            txtValidFrom.DateTime.ToString("yyyy-MM-dd"),
                            Utils.User.GUserID);
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        sql = "Update MastEmp set CostCode = '" + txtCostCode.Text.Trim().ToString().ToUpper() + "' Where EmpUnqID ='" + ctrlEmp1.txtEmpUnqID.Text.Trim() + "' and CompCode = '01'";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();


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

            MessageBox.Show("Not Allowed....", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mode = "NEW";
            oldCode = "";
            ResetCtrl();
            return;

            //using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            //        try
            //        {
            //            cn.Open();
            //            cmd.Connection = cn;
            //            string sql = "Update MastCostCode set CostDesc = '{0}',UpdDt = GetDate(),UpdID ='{1}' where CostCode = '{2}'";
            //            sql = string.Format(sql, txtDescription.Text.Trim().ToString(),
            //                Utils.User.GUserID,
            //                txtCostCode.Text.Trim().ToString().ToUpper()
            //                );

            //            cmd.CommandText = sql;
            //            cmd.ExecuteNonQuery();
            //            MessageBox.Show("Record saved...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            ResetCtrl();
            //            LoadGrid();
            //            return;

            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //}

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
                            string sql = "Delete From MastCostCodeEmp where EmpUnqID ='" + ctrlEmp1.cEmp.EmpUnqID + "' and  CostCode = '" + txtCostCode.Text.Trim() + "' and ValidFrom ='" + txtValidFrom.DateTime.ToString("yyyy-MM-dd") + "'";
                                
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = sql;
                            cmd.Connection = cn;
                            cmd.ExecuteNonQuery();
                                                       
                            
                            string tMaxDt = Utils.Helper.GetDescription("Select isnull(Convert(varchar(10),max(ValidFrom),121),'') From MastCostCodeEmp where EmpUnqId = '" + ctrlEmp1.cEmp.EmpUnqID + "'",Utils.Helper.constr);
                            
                            if(tMaxDt == ""){

                                sql = "Update MastEmp Set CostCode = '' where EmpUnqID ='"  + ctrlEmp1.cEmp.EmpUnqID + "'";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();

                                sql = "Update AttdData Set CostCode='' where EmpUnqID ='" + ctrlEmp1.cEmp.EmpUnqID + "'";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            } 
                            else
                            {
                                string tCostCode = Utils.Helper.GetDescription("Select CostCode from MastCostCodeEmp where EmpUnqId = '" +  ctrlEmp1.cEmp.EmpUnqID + "' and ValidFrom ='" + tMaxDt + "'", Utils.Helper.constr);
                                sql = "Update MastEmp Set CostCode = '" + tCostCode + "' where EmpUnqID ='" + ctrlEmp1.cEmp.EmpUnqID + "'";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();

                                sql = "Update AttdData Set CostCode='' where EmpUnqID ='" + ctrlEmp1.cEmp.EmpUnqID + "' and CompCode ='" + ctrlEmp1.cEmp.CompCode + "' " 
                                    + " and tDate >='" + tMaxDt + "'";
                                cmd.CommandText = sql; 
                                cmd.ExecuteNonQuery();

                            }
                            

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
            string sql = "select a.EmpUnqID,a.CostCode,a.ValidFrom,b.CostDesc " 
            + " From  MastCostCodeEmp a, MastCostCode b " 
            + " where a.EmpUnqID = '" + ctrlEmp1.txtEmpUnqID.Text.Trim().ToString() + "' "
            + " and a.CostCode = b.CostCode ";

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
                txtValidFrom.EditValue = gridView1.GetRowCellValue(info.RowHandle, "ValidFrom");
                object o = new object();
                EventArgs e = new EventArgs();                
                mode = "OLD";
                oldCode = txtCostCode.Text.ToString();
                txtCostCode_Validated(o, e);
                txtValidFrom_EditValueChanged(o, e);
            }
            else
            {
                mode = "NEW";
            }
            SetRights();

        }

        private void txtValidFrom_EditValueChanged(object sender, EventArgs e)
        {
            if (ctrlEmp1.cEmp.CompCode == "" || ctrlEmp1.cEmp.CompDesc == "" || ctrlEmp1.cEmp.EmpUnqID == "" 
                || ctrlEmp1.cEmp.EmpName == "" || txtValidFrom.EditValue == null)
            {
                mode = "NEW";
                SetRights();
                return;
            }

            
            DataSet ds = new DataSet();
            string sql = "select * From MastCostCodeEmp where EmpUnqID ='" + ctrlEmp1.cEmp.EmpUnqID + "' " +
                    " and ValidFrom ='" + txtValidFrom.DateTime.ToString("yyyy-MM-dd") + "' ";
                    

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCostCode.Text = dr["CostCode"].ToString();
                    txtCostCode_Validated(sender, e);
                    mode = "OLD";
                    oldCode = dr["CostCode"].ToString();
                    txtCostCode.Enabled = false;
                    txtValidFrom.Enabled = false;
                }
            }
            else
            {
                mode = "NEW";
                oldCode = "";
                txtCostCode.Enabled = true;
                txtValidFrom.Enabled = true;
            }

            SetRights();
        }

        private void frmMastCostCodeEmp_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter))
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        

    }
}
