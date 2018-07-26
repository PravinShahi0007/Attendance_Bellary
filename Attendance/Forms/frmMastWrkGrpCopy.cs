using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Attendance.Forms
{
    public partial class frmMastWrkGrpCopy : Form
    {
        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";
        public string selection = "";


        public frmMastWrkGrpCopy()
        {
            InitializeComponent();
        }

        private void frmMastWrkGrpCopy_Load(object sender, EventArgs e)
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

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                err = err + "Please Enter WrkGrp Description" + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtCopyFromWrkGrp.Text))
            {
                err = err + "Please Select WrkGrp in Copy From" + Environment.NewLine;
            }

            return err;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                    try
                    {
                        cn.Open();

                    }
                    catch (Exception ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }    
                        
                
                    SqlTransaction tr = cn.BeginTransaction();
                    try
                    {

                        string sql = "Insert into MastWorkGrp (CompCode,WrkGrp,WrkGrpDesc,AddDt,AddID) Values ('{0}','{1}','{2}',GetDate(),'{3}')";
                        sql = string.Format(sql, txtCompCode.Text.Trim().ToString(), txtWrkGrpCode.Text.Trim().ToString(), txtDescription.Text.Trim(),
                            Utils.User.GUserID);

                        SqlCommand cmd = new SqlCommand(sql, cn, tr);
                        cmd.ExecuteNonQuery();

                        sql = "Insert into MastUnit (CompCode,WrkGrp,UnitCode,UnitName,AddDt,AddID) select CompCode,'{0}',UnitCode,UnitName,GetDate(),'{1}' from MastUnit Where CompCode = '{2}' And WrkGrp = '{3}'";
                        sql = string.Format(sql, txtWrkGrpCode.Text.Trim().ToString(), Utils.User.GUserID, txtCompCode.Text.Trim().ToString(), txtCopyFromWrkGrp.Text.Trim().ToString());
                        SqlCommand cmdUnit = new SqlCommand(sql, cn, tr);
                        cmdUnit.ExecuteNonQuery();

                        sql = "Insert into MastDept (CompCode,WrkGrp,UnitCode,DeptCode,DeptDesc,AddDt,AddID) select CompCode,'{0}',UnitCode,DeptCode,DeptDesc,GetDate(),'{1}' from MastDept Where CompCode = '{2}' And WrkGrp = '{3}'";
                        sql = string.Format(sql, txtWrkGrpCode.Text.Trim().ToString(), Utils.User.GUserID, txtCompCode.Text.Trim().ToString(), txtCopyFromWrkGrp.Text.Trim().ToString());
                        SqlCommand cmdDept = new SqlCommand(sql, cn, tr);
                        cmdDept.ExecuteNonQuery();

                        sql = "Insert into MastStat (CompCode,WrkGrp,UnitCode,DeptCode,StatCode,StatDesc,AddDt,AddID) select CompCode,'{0}',UnitCode,DeptCode,StatCode,StatDesc,GetDate(),'{1}' from MastStat Where CompCode = '{2}' And WrkGrp = '{3}'";
                        sql = string.Format(sql, txtWrkGrpCode.Text.Trim().ToString(), Utils.User.GUserID, txtCompCode.Text.Trim().ToString(), txtCopyFromWrkGrp.Text.Trim().ToString());
                        SqlCommand cmdStat = new SqlCommand(sql, cn, tr);
                        cmdStat.ExecuteNonQuery();


                        sql = "Insert into MastStatSec (CompCode,WrkGrp,UnitCode,DeptCode,StatCode,SecCode,SecDesc,AddDt,AddID) select CompCode,'{0}',UnitCode,DeptCode,StatCode,SecCode,SecDesc,GetDate(),'{1}' from MastStatSec Where CompCode = '{2}' And WrkGrp = '{3}'";
                        sql = string.Format(sql, txtWrkGrpCode.Text.Trim().ToString(), Utils.User.GUserID, txtCompCode.Text.Trim().ToString(), txtCopyFromWrkGrp.Text.Trim().ToString());
                        SqlCommand cmdStatSec = new SqlCommand(sql, cn, tr);
                        cmdStatSec.ExecuteNonQuery();

                        sql = "Insert into MastCat (CompCode,WrkGrp,CatCode,CatDesc,AddDt,AddID) select CompCode,'{0}',CatCode,CatDesc,GetDate(),'{1}' from MastCat Where CompCode = '{2}' And WrkGrp = '{3}'";
                        sql = string.Format(sql, txtWrkGrpCode.Text.Trim().ToString(), Utils.User.GUserID, txtCompCode.Text.Trim().ToString(), txtCopyFromWrkGrp.Text.Trim().ToString());
                        SqlCommand cmdCat = new SqlCommand(sql, cn, tr);
                        cmdCat.ExecuteNonQuery();

                        sql = "Insert into MastDesg (CompCode,WrkGrp,DesgCode,DesgDesc,AddDt,AddID) select CompCode,'{0}',DesgCode,DesgDesc,GetDate(),'{1}' from MastDesg Where CompCode = '{2}' And WrkGrp = '{3}'";
                        sql = string.Format(sql, txtWrkGrpCode.Text.Trim().ToString(), Utils.User.GUserID, txtCompCode.Text.Trim().ToString(), txtCopyFromWrkGrp.Text.Trim().ToString());
                        SqlCommand cmdDesg = new SqlCommand(sql, cn, tr);
                        cmdDesg.ExecuteNonQuery();

                        sql = "Insert into MastGrade (CompCode,WrkGrp,GradeCode,GradeDesc,AddDt,AddID) select CompCode,'{0}',GradeCode,GradeDesc,GetDate(),'{1}' from MastGrade Where CompCode = '{2}' And WrkGrp = '{3}'";
                        sql = string.Format(sql, txtWrkGrpCode.Text.Trim().ToString(), Utils.User.GUserID, txtCompCode.Text.Trim().ToString(), txtCopyFromWrkGrp.Text.Trim().ToString());
                        SqlCommand cmdGrade = new SqlCommand(sql, cn, tr);
                        cmdGrade.ExecuteNonQuery();

                        sql = "Insert into MastEmpType (CompCode,WrkGrp,EmpTypeCode,EmpTypeDesc,AddDt,AddID) select CompCode,'{0}',EmpTypeCode,EmpTypeDesc,GetDate(),'{1}' from MastEmpType Where CompCode = '{2}' And WrkGrp = '{3}'";
                        sql = string.Format(sql, txtWrkGrpCode.Text.Trim().ToString(), Utils.User.GUserID, txtCompCode.Text.Trim().ToString(), txtCopyFromWrkGrp.Text.Trim().ToString());
                        SqlCommand cmdEmpType = new SqlCommand(sql, cn, tr);
                        cmdEmpType.ExecuteNonQuery();

                        sql = "Insert into MastLeave (CompCode,WrkGrp,LeaveTyp,LeaveDesc,KeepBal,KeepAdv,Paid,PublicHL,ShowLeaveEntry,ShowGridBalSeq,AllowHalfPosting,AllowEncash, AddDt,AddID) " +
                            " select CompCode,'{0}', LeaveTyp,LeaveDesc,KeepBal,KeepAdv,Paid,PublicHL,ShowLeaveEntry,ShowGridBalSeq,AllowHalfPosting,AllowEncash,GetDate(),'{1}' from MastLeave " +
                            " Where CompCode = '{2}' And WrkGrp = '{3}'";
                        sql = string.Format(sql, txtWrkGrpCode.Text.Trim().ToString(), Utils.User.GUserID, txtCompCode.Text.Trim().ToString(), txtCopyFromWrkGrp.Text.Trim().ToString());
                        SqlCommand cmdEmpLeave = new SqlCommand(sql, cn, tr);
                        cmdEmpLeave.ExecuteNonQuery();

                        tr.Commit();
                        MessageBox.Show("WrkGrp Created Successfully..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tr.Rollback();
                        tr.Dispose();
                        return;
                    }

                    Cursor.Current = Cursors.Default;
                    tr.Dispose();
                    ResetCtrl();
                


            }//using connection

        }

        private void ResetCtrl()
        {
            btnAdd.Enabled = false;

            object s = new object();
            EventArgs e = new EventArgs();
            txtCompCode.Text = "01";
            txtCompName.Text = "";
            txtCompCode_Validated(s, e);
           
            txtWrkGrpCode.Text = "";
            txtDescription.Text = "";
            txtCopyFromWrkGrp.Text = "";
            oldCode = "";
            mode = "NEW";
        }

        private void SetRights()
        {
            if ( txtWrkGrpCode.Text.Trim() != "" && mode == "NEW" && GRights.Contains("A") )
            {
                btnAdd.Enabled = true;
               
            }
            else if ( txtWrkGrpCode.Text.Trim() != "" && mode == "OLD" )
            {
                btnAdd.Enabled = false;

                
            }

            if (GRights.Contains("XXXV"))
            {
                btnAdd.Enabled = false;
                
            }
        }
        
        private void txtCopyFromWrkGrp_KeyDown(object sender, KeyEventArgs e)
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

                    txtCopyFromWrkGrp.Text = obj.ElementAt(0).ToString();
                    
                }
            }
        }

        private void txtCopyFromWrkGrp_Validated(object sender, EventArgs e)
        {
            

            DataSet ds = new DataSet();
            string sql = "select * From MastWorkGrp where CompCode ='" + txtCompCode.Text.Trim() + "' and WrkGrp='" + txtWrkGrpCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCopyFromWrkGrp.Text = dr["WrkGrp"].ToString();
                }
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
                    txtDescription.Text = obj.ElementAt(1).ToString();
                    
                    mode = "OLD";
                }
            }
        }

        private void txtWrkGrpCode_Validated(object sender, EventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtCompName.Text.Trim() == "")
            {
                mode = "NEW";
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
                    txtDescription.Text = dr["WrkGrpDesc"].ToString();
                    mode = "OLD";
                    txtCompCode_Validated(sender,e);
                    oldCode = dr["WrkGrp"].ToString();
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

    }
}
