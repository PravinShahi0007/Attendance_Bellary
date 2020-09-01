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
    public partial class frmMastEmpBlockPunching : Form
    {
        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";

        public frmMastEmpBlockPunching()
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

        private void frmMastEmpBlockPunching_Load(object sender, EventArgs e)
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

            
            if(string.IsNullOrEmpty(txtRemarks.Text.Trim().ToString()))
            {
                err = err + "Please Enter Reason..." + Environment.NewLine;
            }

            return err;
        }

        private void ResetCtrl()
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            

            ctrlEmp1.ResetCtrl();
           
            grid.DataSource = null;
            oldCode = "";
            mode = "NEW";
            SetRights();
        }
        
        private void SetRights()
        {
            if ( GRights.Contains("A") )
            {
                btnAdd.Enabled = true;
                //btnUpdate.Enabled = false;
                
            }
            if (GRights.Contains("U"))
            {
                //btnAdd.Enabled = true;
                btnUpdate.Enabled = true;

            }

            if(GRights.Contains("AU"))
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = true;                
            }
            
            if (GRights.Contains("XXXV"))
            {
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;

            }
            else if (GRights.Contains("XXDV"))
            {
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
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

            string tEmpUnqID = ctrlEmp1.txtEmpUnqID.Text.Trim().ToString();

            //get if already blocked or not
            bool isBlocked = Convert.ToBoolean(Utils.Helper.GetDescription("Select PunchingBlocked from MastEmp where EmpUnqID ='" + tEmpUnqID + "'", Utils.Helper.constr));

            if (isBlocked)
            {
                MessageBox.Show("Employee is already blocked", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


                        int tmaxid = Convert.ToInt32(Utils.Helper.GetDescription("Select isnull(Max(ID),0) + 1 from MastMachineUserOperation", Utils.Helper.constr));

                        string sql = "select MachineIP,IOFLG from readerconfig where canteenflg = 0  and [master] = 0 and compcode = '01' and Active = 1 and MachineIP in ( " +
                            " Select Distinct MachineIP from AttdLog where EmpUnqID = '" + tEmpUnqID +"' and Punchdate between DATEADD(day,-60,getdate()) and DateAdd(day,1,GETDATE()) ) " +
                            " Union Select MachineIP,IOFLG From TripodReaderConfig  ";
                        
                        
                        DataSet ds = Utils.Helper.GetData(sql,Utils.Helper.constr);
                        foreach(DataRow dr in ds.Tables[0].Rows){

                            sql = "insert into MastMachineUserOperation (ID,EmpUnqID,MachineIP,IOFLG,Operation,ReqDt,ReqBy,DoneFlg,AddDt,Remarks) Values ('" + tmaxid + "','" +
                                tEmpUnqID + "','" + dr["MachineIP"].ToString() + "','" + dr["IOFLG"].ToString() + "','BLOCK',GetDate(),'" + Utils.User.GUserID + "',0,GetDate(),'" + txtRemarks.Text.Trim().ToString() + "')";
                            
                            
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();

                        }

                        cmd.CommandText = "Update MastEmp Set PunchingBlocked = 1 where CompCode = '01' and EmpUnqID = '" + tEmpUnqID + "'";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "Update EmpBioData Set Blocked = 1 where EmpUnqID = '" + tEmpUnqID + "'";
                        cmd.ExecuteNonQuery();

                        //string tEmpAdharNo = Utils.Helper.GetDescription("Select AdharNo from MastEmp Where EmpUnqID ='" + tEmpUnqID + "'",Utils.Helper.constr);
                        //cmd.CommandText = "Insert into MastEmpBlackList (Adharno,ISBlock,AddDt,AddID) values ('" + tEmpAdharNo + "',1,GetDate(),'" + Utils.User.GUserID + "')";
                        //cmd.ExecuteNonQuery();

                        sendmail("BLOCK", tEmpUnqID);

                        MessageBox.Show("Employee has been blocked...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        //unblock
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string tEmpUnqID = ctrlEmp1.txtEmpUnqID.Text.Trim().ToString();

            ////get if already blocked or not
            bool isBlocked = Convert.ToBoolean(Utils.Helper.GetDescription("Select PunchingBlocked from MastEmp where EmpUnqID ='" + tEmpUnqID + "'",Utils.Helper.constr));

            if (!isBlocked)
            {
                MessageBox.Show("Employee Blocked Status did not found.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                        int tmaxid = Convert.ToInt32(Utils.Helper.GetDescription("Select isnull(Max(ID),0) + 1 from MastMachineUserOperation", Utils.Helper.constr));

                        string sql = "select * from readerconfig where canteenflg = 0 and lunchinout = 0 and [master] = 0 and compcode = '01'";
                        DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {

                            sql = "insert into MastMachineUserOperation (ID,EmpUnqID,MachineIP,IOFLG,Operation,ReqDt,ReqBy,DoneFlg,AddDt,Remarks) Values ('" + tmaxid + "','" +
                                tEmpUnqID + "','" + dr["MachineIP"].ToString() + "','" + dr["IOFLG"].ToString() + "','UNBLOCK',GetDate(),'" + Utils.User.GUserID + "',1,GetDate(),'" + txtRemarks.Text.Trim().ToString() + "')";
                            
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();

                        }
                        

                        cmd.CommandText = "Update MastEmp Set PunchingBlocked = 0 where CompCode = '01' and EmpUnqID = '" + tEmpUnqID + "'";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "Update EmpBioData Set Blocked = 0 where EmpUnqID = '" + tEmpUnqID + "'";
                        cmd.ExecuteNonQuery();

                        sendmail("UNBLOCK", tEmpUnqID);

                        MessageBox.Show("Record Updated/you may need to re-register employee to concern/required machine...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            string sql = "select * "
            + " From  MastMachineUserOperation "
            + " where EmpUnqID = '" + ctrlEmp1.txtEmpUnqID.Text.Trim().ToString() + "' Order By ID Desc";
           

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

        private void sendmail(string mailtype,string tEmpUnqID)
        {
            string sql = "Select Config_Val from Mast_OtherConfig where Config_Key ='EMPBLOCKNOTIFICATION'";
            
            string tsubject = string.Empty;
            string tbody = string.Empty;
            string to = Utils.Helper.GetDescription(sql,Utils.Helper.constr);
            //to = "anand.acharya@jindalsaw.com";
            string cc = "";
            //string bcc = "anand.acharya@jindalsaw.com";
            string bcc = "";

            if (mailtype == "BLOCK")
            {
                tsubject = "Notification : Card Blocked for  " + ctrlEmp1.cEmp.EmpName + " (" + tEmpUnqID + " ) ";
            }
            string tblokeddt = string.Empty;

            if (mailtype == "UNBLOCK")
            {
                tsubject = "Notification : Card Un-Blocked for " + ctrlEmp1.cEmp.EmpName + " (" + tEmpUnqID + " ) ";
            
                //get last blocked date
                string tsql = "Select Max(ReqDt) From MastMachineUserOperation where EmpUnqID = '" + tEmpUnqID + "' and Operation = 'BLOCK' ";
                tblokeddt = Convert.ToDateTime(Utils.Helper.GetDescription(tsql, Utils.Helper.constr)).ToString("yyyy-MM-dd HH:mm");
            }

            string thead = "<html> " +
                    "<head>" +
                    "<style>" +
                    " table { " +
                        " font-family: arial, sans-serif; " +
                        " border-collapse: collapse; " +
                        " width: 100%; " +
                    "} " +

                    " td, th { " +
                    "    border: 1px solid #dddddd; " +
                    "    text-align: left; " +
                    "    padding: 8px; " +
                    "} " +

                    " tr:nth-child(even) { " +
                    "    background-color: #dddddd;" +
                    "}" +
                    "</style>" +
                    "</head>" +
                    "<body>";



            tbody = "Sir, <br/><p>" + "Subjected Action Performed as per below details:"  + "</p> <br/> <br/> " +
                "<table>" +
                "<tr><td>EmpCode : </td><td>" + tEmpUnqID  + "</td></tr>" +
                "<tr><td>EmpName : </td><td>" + ctrlEmp1.cEmp.EmpName + "</td></tr>" +
                "<tr><td>WrkGrp : </td><td>" + ctrlEmp1.cEmp.WrkGrp  + " " + (ctrlEmp1.cEmp.ContCode) + "</td></tr>" +
                "<tr><td>Department :</td><td>" + ctrlEmp1.cEmp.DeptDesc + "</td></tr>" +
                "<tr><td>Section :</td><td>" + ctrlEmp1.cEmp.StatDesc + "</td></tr>" +
                "<tr><td>Designation :</td><td>" + ctrlEmp1.cEmp.DesgDesc + "</td></tr>" +
                "<tr><td>Grade : </td><td> " + ctrlEmp1.cEmp.GradeDesc + "</td></tr>" +
                (!string.IsNullOrEmpty(tblokeddt)?"<tr><td>Blocked Date</td><td>" + tblokeddt + "</td></tr>":"") +
                "<tr><td>Action performed by</td><td>" + Utils.User.GUserID + "</td>" +
                "<tr><td>Reason</td><td>" + txtRemarks.Text.Trim().ToString() + "</td>" +
                "<tr><td>Date And Time</td><td>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "</td></tr>" +
                "</table><br/><br/> " +
                 "*This is Auto-generated notification, do not reply on this e-mail id. </body></html>";
                
                
            string err = EmailHelper.Email(to, cc, bcc, thead + tbody, tsubject, Globals.G_DefaultMailID,
                        Globals.G_DefaultMailID, "", "");
            
            MessageBox.Show(mailtype + " : Notification : Status : " + err,"Information",MessageBoxButtons.OK,MessageBoxIcon.Information );

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
