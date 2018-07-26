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
    public partial class frmMastException : Form
    {
        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";

        public frmMastException()
        {
            InitializeComponent();
        }

        private void frmMastException_Load(object sender, EventArgs e)
        {
            ResetCtrl();
            GRights = Attendance.Classes.Globals.GetFormRights(this.Name);
            SetRights();
            LoadGrid();
        }

        private string DataValidate()
        {
            string err = string.Empty;

            if (string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()) || string.IsNullOrEmpty(txtEmpName.Text.Trim()))
                err = "EmpUnqId is Required..";


            return err;
        }
        
        private void ResetCtrl()
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            
            object s = new object();
            EventArgs e = new EventArgs();
          
           
          
            chkHalfDay.CheckState = CheckState.Unchecked;
            chkAutoOut.CheckState = CheckState.Unchecked;
            chkEarlyGoing.CheckState = CheckState.Unchecked;
            chkGrace.CheckState = CheckState.Unchecked;
            chkLateCome.CheckState = CheckState.Unchecked;
            txtWrkGrpCode.Text = "";
           
            txtEmpUnqID.Text = "";
            txtEmpName.Text = "";


            oldCode = "";
            mode = "NEW";
        }

        private void SetRights()
        {
            if ( txtEmpUnqID.Text.Trim() != "" && mode == "NEW" && GRights.Contains("A") )
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            else if ( txtEmpUnqID.Text.Trim() != "" && mode == "OLD" )
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
                        string sql = "Insert into MastException " +
                            "(EmpUnqID,EmpName,WrkGrp,ExecAutoOut,ExecLateCome,ExecHalfDay,ExecGracePeriod,ExecEarlyGoing," +
                            " AddDt,AddID) Values ('{0}','{1}','{2}','{3}','{4}','{5}'," +
                            " '{6}','{7}',GetDate(),'{8}')";

                        sql = string.Format(sql, txtEmpUnqID.Text.Trim().ToString(),txtEmpName.Text.Trim(), txtWrkGrpCode.Text.Trim().ToString(),
                            ((chkAutoOut.Checked) ? "1" : "0"), 
                            ((chkLateCome.Checked) ? "1" : "0"),
                            ((chkHalfDay.Checked) ? "1" : "0"),
                            ((chkGrace.Checked)?"1":"0") , 
                            ((chkEarlyGoing.Checked) ? "1" : "0"),                             
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
                        string sql = "Update MastException Set " +
                            " WrkGrp = '" + txtWrkGrpCode.Text.Trim() + "'," +
                            " ExecAutoOut = '" + ((chkAutoOut.Checked) ? "1" : "0") + "'," +
                            " ExecLateCome ='" + ((chkLateCome.Checked) ? "1" : "0") + "'," +                        
                            " ExecHalfDay='" +  ((chkHalfDay.Checked) ? "1" : "0")  + "'," +
                            " ExecGracePeriod = '" + ((chkGrace.Checked) ? "1" : "0") + "'," +
                            " ExecEarlyGoing ='" + ((chkEarlyGoing.Checked) ? "1" : "0") + "'," +
                            " UpdDt = GetDate(), UpdID = '" + Utils.User.GUserID + "'" +
                            " Where EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "'";

                        
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
                            string sql = "Delete From MastException where EmpUnqID = '" + txtEmpUnqID.Text.Trim().ToString() + "'";
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
            string sql = "select a.EmpUnqID,b.EmpName,a.WrkGrp,a.ExecAutoOut,a.AddDt,a.AddID,a.UpdDt,a.UpdID From MastException a, MastEmp b Where a.EmpUnqID = b.EmpUnqId Order By EmpUnqID "; 
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
               txtEmpUnqID.Text = gridView1.GetRowCellValue(info.RowHandle, "EmpUnqID").ToString();
                object o = new object();
                EventArgs e = new EventArgs();                
                mode = "OLD";
                oldCode = txtEmpUnqID.Text.ToString();
                txtEmpUnqID_Validated(o, e);
            }

            
        }

        private void txtEmpUnqID_KeyDown(object sender, KeyEventArgs e)
        {

            

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2 || e.KeyCode == Keys.F3)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";

                sql = "Select EmpUnqID,EmpName,WrkGrp,GradeDesc From v_EmpMast Where CompCode ='01' ";
               
                if (e.KeyCode == Keys.F1)
                {
                    obj = (List<string>)hlp.Show(sql, "EmpUnqID", "EmpUnqID", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                    100, 300, 400, 600, 100, 100);
                }
                else if(e.KeyCode == Keys.F2)
                {
                    obj = (List<string>)hlp.Show(sql, "EmpName", "EmpName", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                    100, 300, 400, 600, 100, 100);
                }
                else
                {
                    obj = (List<string>)hlp.Show(sql, "GradeDesc", "GradeDesc", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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
                    txtEmpUnqID.Text = obj.ElementAt(0).ToString();
                    txtEmpUnqID_Validated(sender, e);
                }
            }
        }

        private void txtEmpUnqID_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()) )
            {
                mode = "NEW";
                oldCode = "";
                return;
            }

            DataSet ds = new DataSet();
            string sql = "select EmpUnqID,EmpName,WrkGrp From MastException where EmpUnqID='" + txtEmpUnqID.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtEmpUnqID.Text = dr["EmpUnqID"].ToString();
                    txtEmpName.Text = dr["EmpName"].ToString();
                    txtWrkGrpCode.Text = dr["WrkGrp"].ToString();
                    mode = "OLD";
                    oldCode = dr["EmpUnqID"].ToString();
                }
            }
            else
            {
                chkAutoOut.CheckState = CheckState.Unchecked;
                txtEmpName.Text = Utils.Helper.GetDescription("Select EmpName From MastEmp Where EmpUnqID='" + txtEmpUnqID.Text.Trim() + "'", Utils.Helper.constr);
                txtWrkGrpCode.Text = Utils.Helper.GetDescription("Select WrkGrp From MastEmp Where EmpUnqID='" + txtEmpUnqID.Text.Trim() + "'", Utils.Helper.constr);
                mode = "NEW";
                oldCode = "";
            }

            
            SetRights();
        }

       

    }
}
