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
using Attendance.Classes;

namespace Attendance.Forms
{
    public partial class frmCreateMuster : Form
    {
        
        public string GRights = "XXXV";
        


        public frmCreateMuster()
        {
            InitializeComponent();
        }

        private void frmMastUnit_Load(object sender, EventArgs e)
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

            if (string.IsNullOrEmpty(txtWrkGrpDesc.Text))
            {
                err = err + "Please Enter WrkGrp Description" + Environment.NewLine;
            }

            if (txtFromDt.EditValue == null)
            {
                err = err + "Please Enter From Date" + Environment.NewLine;
                return err;
            }

            if (txtFromDt.DateTime == DateTime.MinValue)
            {
                err = err + "Please Enter From Date" + Environment.NewLine;
                return err;
            }

            if (txtToDate.EditValue == null)
            {
                err = err + "Please Enter ToDate" + Environment.NewLine;
                return err;
            }

            if (txtToDate.DateTime == DateTime.MinValue)
            {
                err = err + "Please Enter ToDate" + Environment.NewLine;
                return err;
            }

            if (txtFromDt.DateTime > txtToDate.DateTime)
            {
                err = err + "Please Enter Valid Date Range.." + Environment.NewLine;                
            }


            return err;
        }

        

        private void ResetCtrl()
        {
            
            btnCreate.Enabled = false;

            
            object s = new object();
            EventArgs e = new EventArgs();
            txtCompCode.Text = "01";
            txtCompName.Text = "";
            txtCompCode_Validated(s, e);
           
            txtWrkGrpCode.Text = "";
            txtWrkGrpDesc.Text = "";
            txtEmpUnqID.Text = "";
            txtEmpName.Text = "";
            txtFromDt.EditValue = null;
            txtToDate.EditValue = null;
            pBar.EditValue = 0;
            pBar.Properties.Step = 1;
            pBar.Properties.PercentView = true;
            pBar.Properties.Minimum = 0;
            
        }

        private void SetRights()
        {
            btnCreate.Enabled = false;
            if (GRights.Contains("A") || GRights.Contains("U") || GRights.Contains("D"))
                btnCreate.Enabled = true;
            
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
            if (txtCompCode.Text.Trim() == "" || txtCompName.Text.Trim() == "" )
            {
                
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
                    txtCompCode_Validated(sender,e);
                    
                }
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

        private void txtEmpUnqID_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtWrkGrpCode.Text.Trim() == "")
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select EmpUnqID,EmpName From MastEmp Where CompCode ='" + txtCompCode.Text.Trim() + "' and WrkGrp = '" + txtWrkGrpCode.Text.Trim() + "' and Active = 1 ";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "EmpUnqID", "EmpUnqID", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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
                    txtEmpName.Text = obj.ElementAt(1).ToString();
                   

                }
            }
        }

        private void txtEmpUnqID_Validated(object sender, EventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtCompName.Text.Trim() == "" 
                || txtWrkGrpCode.Text.Trim() == "" || txtWrkGrpDesc.Text.Trim() == ""
                || txtEmpUnqID.Text.Trim() == "" )
            {

                return;
            }

            //txtEmpUnqID.Text = txtEmpUnqID.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastEmp where CompCode ='" + txtCompCode.Text.Trim() + "' " +
                    " and WrkGrp='" + txtWrkGrpCode.Text.Trim() + "' and  EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "' and Active = 1";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCompCode.Text = dr["CompCode"].ToString();
                    txtWrkGrpCode.Text = dr["WrkGrp"].ToString();
                    txtEmpUnqID.Text = dr["EmpUnqID"].ToString();
                    txtEmpName.Text = dr["EmpName"].ToString();
                    txtCompCode_Validated(sender, e);
                    txtWrkGrpCode_Validated(sender, e);
                    
                }
            }
            else
            {
               
                txtEmpName.Text = "";
                txtEmpUnqID.Text = "";

            }

            

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
 
            if(!string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()) )
            {
                if (string.IsNullOrEmpty(txtEmpName.Text.Trim()))
                {
                    MessageBox.Show("Please Enter Valid Employee", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string sql = string.Empty;
            string question = string.Empty;
            DialogResult drq ;

            if (string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()))
            {
                 question = "Are You Sure to create Muster Table For : " + txtWrkGrpCode.Text.Trim().ToString() + Environment.NewLine
                    + "Processed Data will be deleted between '" + txtFromDt.DateTime.ToString("yyyy-MM-dd") + "' And '" + txtToDate.DateTime.ToString("yyyy-MM-dd") + "' ";


                 sql = "Select CompCode,EmpUnqID,WeekOff From MastEmp Where CompCode ='" + txtCompCode.Text.Trim() + "' and  WrkGrp = '" + txtWrkGrpCode.Text.Trim() + "' And Active = '1' Order By EmpUnqID";
            
            }else{


                question = "Are You Sure to create Muster Table For : " + txtEmpUnqID.Text.Trim().ToString() + Environment.NewLine
                    + "Processed Data will be deleted between '" + txtFromDt.DateTime.ToString("yyyy-MM-dd") + "' And '" + txtToDate.DateTime.ToString("yyyy-MM-dd") + "' ";

                sql = "Select CompCode,EmpUnqID,WeekOff From MastEmp Where CompCode ='" + txtCompCode.Text.Trim() + "' "
                    + " and WrkGrp = '" + txtWrkGrpCode.Text.Trim() + "' "
                    + " and EmpUnqID ='" + txtEmpUnqID.Text.Trim() + "' "
                    + " And Active = '1' Order By EmpUnqID";
            
            }
            drq = MessageBox.Show(question,"Question",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if(drq == DialogResult.No)
            {
                MessageBox.Show("Process Canceled..","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            btnCreate.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                pBar.Properties.Maximum = ds.Tables[0].Rows.Count + 1;
                
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //update progressbar
                    pBar.PerformStep();
                    pBar.Update();
                    string EmpUnqID = dr["EmpUnqID"].ToString();
                    string WeekOff = dr["WeekOff"].ToString();
                    string CompCode = dr["CompCode"].ToString();
                    DateTime fromdt = txtFromDt.DateTime;
                    DateTime todt = txtToDate.DateTime;

                    clsEmp t = new clsEmp();
                    if (t.GetEmpDetails(CompCode, EmpUnqID))
                    {
                       string err1 = string.Empty;
                        bool t1 = t.CreateMuster(fromdt, todt, out err1);
                       if (!t1)
                       {
                           MessageBox.Show(err1, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       }
                    }


                } //foreach loop
            }
            MessageBox.Show("Muster Table Created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cursor.Current = Cursors.Default;
            ResetCtrl();
            SetRights();
            
        }

        private void frmCreateMuster_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter))
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }



    }
}
