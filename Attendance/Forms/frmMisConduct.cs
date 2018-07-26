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
    public partial class frmMisConduct : Form
    {
        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";
       

        public frmMisConduct()
        {
            InitializeComponent();
            this.ctrlEmp1.EmpUnqIDValidated += new EventHandler(this.ctrlEmpValidateEvent_Handler);
            //this.ctrlEmp1.CompCodeValidated += new EventHandler(this.ctrlCompValidateEvent_Handler);
        }

        private void ctrlEmpValidateEvent_Handler(object sender, EventArgs e)
        {
               LoadGrid();
               SetRights();
               txtID.Focus();
        }

        //private void ctrlCompValidateEvent_Handler(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(ctrlEmp1.txtCompCode.Text.Trim()))
        //        return;
            

        //}

        private void frmMisConduct_Load(object sender, EventArgs e)
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

            if (!string.IsNullOrEmpty(ctrlEmp1.cEmp.EmpUnqID) && !ctrlEmp1.IsValid )
            {
                err = err + "Invalid/InActive EmpUnqID..." + Environment.NewLine;
            }

            if (ctrlEmp1.txtUnitCode.Text.Trim() == "")
            {
                err = err + "Unit Code is required../Please update from EmpBasicData module..." + Environment.NewLine;
            }

            if (txtMisConDt.EditValue == null)
            {
                err += "MisConduct Date is Required..." + Environment.NewLine;
            }

            if (txtMisConDesc.Text.Trim() == "")
            {
                err += "MisConduct Description is Required..." + Environment.NewLine;
            }

            if(txtActionDt.EditValue != null && txtActionDesc.Text.Trim() == "")
            {
                err += "Action Description is Required..." + Environment.NewLine;
            }

            if (txtActionDesc.Text.Trim() != "" && txtActionDt.EditValue == null)
            {
                err += "Action Date is Required..." + Environment.NewLine;
            }
            
            if (txtFinActionDt.EditValue != null && txtFinActionDesc.Text.Trim() == "")
            {
                err += "Action Description is Required..." + Environment.NewLine;
            }

            if (txtFinActionDesc.Text.Trim() != "" && txtFinActionDt.EditValue == null)
            {
                err += "Final Action Date is Required..." + Environment.NewLine;
            }

            if (txtActionDt.EditValue != null && txtMisConDt.EditValue != null)
            {
                if (txtActionDt.DateTime < txtMisConDt.DateTime)
                {
                    err += "Action Date must be gretor/Equal than MisConduct Date..." + Environment.NewLine;
                }
            }

            if (txtActionDt.EditValue != null && txtMisConDt.EditValue != null && txtFinActionDt.EditValue != null)
            {
                if (txtFinActionDt.DateTime < txtMisConDt.DateTime)
                {
                    err += "Final Action Date must be gretor/Equal than MisConduct Date..." + Environment.NewLine;
                }

                if (txtFinActionDt.DateTime < txtActionDt.DateTime)
                {
                    err += "Final Action Date must be gretor/Equal than Action Date..." + Environment.NewLine;
                }
            }

            

            return err;
        }

        private void ResetCtrl()
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            txtID.Text = "";
            txtMisConDesc.Text = "";
            txtActionDesc.Text = "";
            txtFinActionDesc.Text = "";
            txtRemarks.Text = "";
            txtActionDt.EditValue = null;
            txtMisConDt.EditValue = null;
            txtFinActionDt.EditValue = null;
            ctrlEmp1.ResetCtrl();
            grid.DataSource = null;
            mode = "NEW";
        }
        
        private void SetRights()
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            if ( ctrlEmp1.txtEmpUnqID.Text.Trim() != "" && mode == "NEW" && GRights.Contains("A") && txtID.Text.Trim() == "" )
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            else if (ctrlEmp1.txtEmpUnqID.Text.Trim() != "" && mode == "OLD" && txtID.Text.Trim() != "")
            {
                //btnAdd.Enabled = true;

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
                        cmd.CommandType = CommandType.Text;



                        string sql = "Insert into MastEmpMisConduct ( MisConDt,MisConDesc,EmpCode,EmpUnqID,WrkGrp,ActionDt,ActionDesc,FinActionDt,FinActionDesc,Remarks,AddDt,AddId,DelFlg) Values " +
                            " ('{0}','{1}', '{2}','{3}','{4}',{5}," +
                            "  '{6}'  , {7} , '{8}' , '{9}' , GetDate(),'{10}' ,0)";



                        sql = string.Format(sql, txtMisConDt.DateTime.ToString("yyyy-MM-dd"), txtMisConDesc.Text.Trim().ToString(),
                          ctrlEmp1.txtEmpCode.Text.Trim(), ctrlEmp1.txtEmpUnqID.Text.Trim(), ctrlEmp1.txtWrkGrpCode.Text.Trim(),
                          ((txtActionDt.EditValue == null) ? "null" : "'" + txtActionDt.DateTime.ToString("yyyy-MM-dd") + "'"),
                          txtActionDesc.Text.Trim().ToString(),
                          ((txtFinActionDt.EditValue == null) ? "null" : "'" + txtFinActionDt.DateTime.ToString("yyyy-MM-dd") + "'"),
                          txtFinActionDesc.Text.Trim().ToString(), txtRemarks.Text.Trim().ToString(),
                          Utils.User.GUserID
                          );
	

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        txtID.Text = "";
                        txtMisConDesc.Text = "";
                        txtActionDesc.Text = "";
                        txtFinActionDesc.Text = "";
                        txtRemarks.Text = "";
                        txtActionDt.EditValue = null;
                        txtMisConDt.EditValue = null;
                        txtFinActionDt.EditValue = null;
                        LoadGrid();
                        MessageBox.Show("Record Saved...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mode = "NEW";
                        SetRights();

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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
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
                        cmd.CommandType = CommandType.Text;



                        string sql = "Update MastEmpMisConduct set MisConDt='{0}',MisConDesc='{1}', EmpCode='{2}',EmpUnqID='{3}',WrkGrp='{4}',ActionDt={5}," +
                            " ActionDesc = '{6}'  , FinActionDt= {7} , FinActionDesc= '{8}' , Remarks = '{9}' , UpdDt=GetDate(),UpdID ='{10}' Where " +
                            " EmpUnqID ='{11}' and ID = '{12}'";


                        sql = string.Format(sql,txtMisConDt.DateTime.ToString("yyyy-MM-dd") ,txtMisConDesc.Text.Trim().ToString(),
                            ctrlEmp1.txtEmpCode.Text.Trim(),ctrlEmp1.txtEmpUnqID.Text.Trim(),ctrlEmp1.txtWrkGrpCode.Text.Trim(),

                            ((txtActionDt.EditValue == null)?"null":"'" + txtActionDt.DateTime.ToString("yyyy-MM-dd") + "'"),
                            txtActionDesc.Text.Trim().ToString(),
                            ((txtFinActionDt.EditValue == null)?"null":"'" + txtFinActionDt.DateTime.ToString("yyyy-MM-dd") + "'"),
                            txtFinActionDesc.Text.Trim().ToString(),txtRemarks.Text.Trim().ToString(),
                            Utils.User.GUserID,
                            ctrlEmp1.txtEmpUnqID.Text.Trim(), txtID.Text.Trim()
                            );

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        txtID.Text = "";
                        txtMisConDesc.Text = "";
                        txtActionDesc.Text = "";
                        txtFinActionDesc.Text = "";
                        txtRemarks.Text = "";
                        txtActionDt.EditValue = null;
                        txtMisConDt.EditValue = null;
                        txtFinActionDt.EditValue = null;
                        LoadGrid();

                        MessageBox.Show("Record Updated...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mode = "NEW";
                        SetRights();
                        
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
                        cmd.CommandType = CommandType.Text;
                        
                        string sql = "Update MastEmpMisConduct set DelFlg=1,UpdDt = GetDate(), UpdID = '{0}' Where " +
                            " EmpUnqID ='{1}' and ID = '{2}'";


                        sql = string.Format(sql, 
                            Utils.User.GUserID,
                            ctrlEmp1.txtEmpUnqID.Text.Trim(), txtID.Text.Trim()
                            );

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        
                        txtID.Text = "";
                        txtMisConDesc.Text = "";
                        txtActionDesc.Text = "";
                        txtFinActionDesc.Text = "";
                        txtRemarks.Text = "";
                        txtActionDt.EditValue = null;
                        txtMisConDt.EditValue = null;
                        txtFinActionDt.EditValue = null;
                        LoadGrid();
                        mode = "NEW";
                        SetRights();

                        MessageBox.Show("Record Deleted...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
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
       
        private void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (ctrlEmp1.txtCompCode.Text.Trim() == ""
                || ctrlEmp1.txtWrkGrpCode.Text.Trim() == ""
                || ctrlEmp1.txtEmpUnqID.Text.Trim() == ""
               
                )
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select ID,MisConDt,MisConDesc From MastEmpMisConduct Where EmpUnqID ='" + ctrlEmp1.txtEmpUnqID.Text.Trim() + "' and DelFlg = 0 ";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "ID", "ID", typeof(int), Utils.Helper.constr, "System.Data.SqlClient",
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
                    txtID_Validated(sender, e);
                    mode = "OLD";

                }
            }
        }

        private void txtID_Validated(object sender, EventArgs e)
        {
            if (ctrlEmp1.txtCompCode.Text.Trim() == ""
                || ctrlEmp1.txtWrkGrpCode.Text.Trim() == ""
                || ctrlEmp1.txtEmpUnqID.Text.Trim() == ""
                || txtID.Text.Trim() == ""

                )
            {
                mode = "NEW";
                SetRights();
                return;
            }
                

           // txtDesgCode.Text = txtDesgCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastEmpMisConduct where ID = '" + txtID.Text.Trim() + "' and DelFlg = 0";
                   

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtMisConDt.DateTime = Convert.ToDateTime(dr["MisConDt"]);
                    txtMisConDesc.Text = dr["MisConDesc"].ToString();

                    if (dr["ActionDt"] != DBNull.Value)
                    {
                        txtActionDt.DateTime = Convert.ToDateTime(dr["ActionDt"]);
                    }
                    else
                    {
                        txtActionDt.EditValue = null;
                    }

                    
                    txtActionDesc.Text = dr["ActionDesc"].ToString();

                    if (dr["FinActionDt"] != DBNull.Value)
                    {
                        txtFinActionDt.DateTime = Convert.ToDateTime(dr["FinActionDt"]);
                    }
                    else
                    {
                        txtFinActionDt.EditValue = null;
                    }

                    
                    txtFinActionDesc.Text = dr["FinActionDesc"].ToString();

                    txtRemarks.Text = dr["Remarks"].ToString();

                    mode = "OLD";
                    SetRights();
                   
                }
            }
            else
            {
                mode = "NEW";
                txtID.Text = "";
                SetRights();
            }

           

        }

        private void LoadGrid()
        {
            DataSet ds = new DataSet();

            string EmpUnqID = ctrlEmp1.txtEmpUnqID.Text.Trim();

            string famsql = "Select ID,MisConDt,MisConDesc From MastEmpMisConduct Where EmpUnqID ='" + ctrlEmp1.txtEmpUnqID.Text.Trim() + "' and DelFlg = 0 Order By ID ";
            
           
            //family
            ds = Utils.Helper.GetData(famsql, Utils.Helper.constr);
            Boolean hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

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

        private void frmMisConduct_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter))
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void gridview_DoubleClick(object sender, EventArgs e)
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
                txtID.EditValue = gridview.GetRowCellValue(info.RowHandle, "ID").ToString();
                object sender = new object();
                EventArgs e = new EventArgs();
                txtID_Validated(sender, e);
            }
        }
    }
}
