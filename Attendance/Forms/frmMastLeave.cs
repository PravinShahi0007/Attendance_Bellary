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
    public partial class frmMastLeave : Form
    {
        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";

        public frmMastLeave()
        {
            InitializeComponent();
        }

        private void frmMastLeave_Load(object sender, EventArgs e)
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

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                err = err + "Please Enter Leave Description" + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtSeqNo.Text))
            {
                err = err + "Please Enter Seq. Number.." + Environment.NewLine;
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
            txtDescription.Text = "";
           
            txtSeqNo.Text = "";
           
            chkPaid.CheckState = CheckState.Unchecked;
            chkKeepAdv.CheckState = CheckState.Unchecked;
            chkKeepBal.CheckState = CheckState.Unchecked;
            chkPublicHL.CheckState = CheckState.Unchecked;
            chkHalf.CheckState = CheckState.Unchecked;
            chkShowEntry.CheckState = CheckState.Unchecked;
            chkEncash.CheckState = CheckState.Unchecked;



            oldCode = "";
            mode = "NEW";
        }

        private void SetRights()
        {
            if ( txtLeaveTyp.Text.Trim() != "" && mode == "NEW" && GRights.Contains("A") )
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            else if ( txtLeaveTyp.Text.Trim() != "" && mode == "OLD" )
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
                   + " CompCode ='" + txtCompCode.Text.Trim() + "' and "
                   + " WrkGrp ='" + txtWrkGrpCode.Text.Trim() + "' ";
                   
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
                    txtDescription.Text = obj.ElementAt(1).ToString();
                    
                    mode = "OLD";
                }
            }
        }

        private void txtLeaveTyp_Validated(object sender, EventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtCompName.Text.Trim() == "" || txtLeaveTyp.Text.Trim() == "")
            {
                mode = "NEW";
                return;
            }

            DataSet ds = new DataSet();
            string sql = "select * From  MastLeave where CompCode ='" + txtCompCode.Text.Trim() + "' " 
                +   " And WrkGrp ='" + txtWrkGrpCode.Text.Trim() + "' and LeaveTyp ='" + txtLeaveTyp.Text.Trim() + "'";
            
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCompCode.Text = dr["CompCode"].ToString();
                    txtLeaveTyp.Text = dr["LeaveTyp"].ToString();
                    txtDescription.Text = dr["LeaveDesc"].ToString();
                    txtSeqNo.Text = dr["ShowGridBalSeq"].ToString();
                    
                    chkPaid.CheckState = (Convert.ToBoolean(dr["Paid"])) ? CheckState.Checked : CheckState.Unchecked;
                    chkKeepBal.CheckState = (Convert.ToBoolean(dr["KeepBal"])) ? CheckState.Checked : CheckState.Unchecked;
                    chkKeepAdv.CheckState = (Convert.ToBoolean(dr["KeepAdv"])) ? CheckState.Checked : CheckState.Unchecked;
                    chkPublicHL.CheckState = (Convert.ToBoolean(dr["PublicHL"])) ? CheckState.Checked : CheckState.Unchecked;
                    chkEncash.CheckState = (Convert.ToBoolean(dr["AllowEncash"])) ? CheckState.Checked : CheckState.Unchecked;

                    chkHalf.CheckState = (Convert.ToBoolean(dr["AllowHalfPosting"])) ? CheckState.Checked : CheckState.Unchecked;
                    chkShowEntry.CheckState = (Convert.ToBoolean(dr["ShowLeaveEntry"])) ? CheckState.Checked : CheckState.Unchecked;
                    
                    
                    mode = "OLD";
                    txtCompCode_Validated(sender,e);

                    oldCode = dr["LeaveTyp"].ToString();
                }
            }
            else
            {
                mode = "NEW";
                oldCode = "";
            }

            SetRights();
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
                        string sql = "Insert into MastLeave " +
                            "(CompCode,WrkGrp,LeaveTyp,LeaveDesc,ShowGridBalSeq," +
                            " Paid,KeepBal,PublicHL,KeepAdv," +
                            " AllowEncash,ShowLeaveEntry,AllowHalfPosting," +
                            " AddDt,AddID) Values ('{0}','{1}','{2}','{3}','{4}','{5}'," +
                            " '{6}','{7}','{8}','{9}','{10}','{11}'," +
                            " GetDate(),'{12}')";

                        sql = string.Format(sql, 
                            txtCompCode.Text.Trim().ToString(), 
                            txtWrkGrpCode.Text.Trim().ToString(),
                            txtLeaveTyp.Text.Trim().ToString(),
                            txtDescription.Text.Trim().ToString(),
                            txtSeqNo.Text.Trim().ToString(),
                            ((chkPaid.Checked)?"1":"0"),
                            ((chkKeepBal.Checked)?"1":"0"),
                            ((chkPublicHL.Checked)?"1":"0"),
                            ((chkKeepAdv.Checked)?"1":"0"),
                            ((chkEncash.Checked)?"1":"0"),
                            ((chkShowEntry.Checked)?"1":"0"),
                            ((chkHalf.Checked)?"1":"0"),
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
                        string sql = "Update MastLeave Set LeaveDesc = '{0}',ShowGridBalSeq = '{1}'," 
                           + " Paid = '{2}',KeepBal='{3}',PublicHL='{4}',KeepAdv = '{5}'," 
                           + " AllowEncash ='{6}',ShowLeaveEntry = '{7}', AllowHalfPosting = '{8}' ," 
                           + " UpdDt = GetDate(), UpdID = '{9}' "
                           + " Where CompCode = '{10}' and WrkGrp = '{11}' and LeaveTyp ='{12}' ";

                        sql = string.Format(sql, txtDescription.Text.Trim(),txtSeqNo.Text.Trim().ToString(),
                            ((chkPaid.Checked) ? "1" : "0"), ((chkKeepBal.Checked) ? "1" : "0"), ((chkPublicHL.Checked) ? "1" : "0"), ((chkKeepAdv.Checked) ? "1" : "0"),
                            ((chkEncash.Checked) ? "1" : "0"), ((chkShowEntry.Checked) ? "1" : "0"), ((chkHalf.Checked) ? "1" : "0"),
                             Utils.User.GUserID, txtCompCode.Text.Trim().ToString(), txtWrkGrpCode.Text.Trim().ToString(),txtLeaveTyp.Text.Trim().ToString()
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
                            string sql = "Delete From MastLeave where CompCode = '" + txtCompCode.Text.Trim() + "' " 
                            + " and WrkGrp = '" + txtWrkGrpCode.Text.Trim() + "' "
                            + " And LeaveTyp ='" + txtLeaveTyp.Text.Trim().ToString() + "'";
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
            if (txtCompCode.Text.Trim() == "" || txtCompName.Text.Trim() == "" || txtWrkGrpCode.Text.Trim() == "")
            {
                grid.DataSource = null;
                return;
            }


            DataSet ds = new DataSet();
            string sql = "select CompCode,WrkGrp,LeaveTyp,LeaveDesc,KeepBal,KeepAdv,Paid,PublicHL,ShowLeaveEntry, " +
                    " ShowGridBalSeq,AllowHalfPosting,AllowEncash from MastLeave "
                    + " where CompCode ='" + txtCompCode.Text.Trim() + "'  "
                    + " And WrkGrp ='" + txtWrkGrpCode.Text.Trim() + "' Order By ShowGridBalSeq ";

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
               txtLeaveTyp.Text = gridView1.GetRowCellValue(info.RowHandle, "LeaveTyp").ToString();
                object o = new object();
                EventArgs e = new EventArgs();                
                mode = "OLD";
                oldCode = txtLeaveTyp.Text.ToString();
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
                    LoadGrid();
                }
            }


        }


    }
}
