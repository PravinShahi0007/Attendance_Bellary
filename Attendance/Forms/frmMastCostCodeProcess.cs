using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Attendance.Classes;

namespace Attendance.Forms
{
    public partial class frmMastCostCodeProcess : Form
    {
        public static string GRights = "XXXV";
        

        public frmMastCostCodeProcess()
        {
            InitializeComponent();
        }

        private void frmMastCostCodeProcess_Load(object sender, EventArgs e)
        {
            GRights = Attendance.Classes.Globals.GetFormRights(this.Name);
            ResetCtrl();
            SetRights();
        }

        private void ResetCtrl()
        {
            btnSubmit.Enabled = false;
            txtDate.EditValue = null;
            Cursor.Current = Cursors.Default;
        }

        private void SetRights()
        {
            
            btnSubmit.Enabled = false;

            if (GRights.Contains("A"))
                btnSubmit.Enabled = true;
            if (GRights.Contains("U"))
                    btnSubmit.Enabled = true;
            if (GRights.Contains("D"))
                    btnSubmit.Enabled = true;
            
        }


        private void call_Calc_StdHrs(DateTime tFromDt,  out string err)
        {
            err = string.Empty;
            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {

                    cn.Open();                    
                    string sql  = string.Empty;
                    sql = "Select EmpUnqID,WrkGrp  From MastEmp where WrkGrp in ('Comp','Cont') And Active = 1 ";
                    DataSet dsEmp = Utils.Helper.GetData(sql, Utils.Helper.constr);
                    bool hasRows = dsEmp.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

                    if (hasRows)
                    {
                        foreach (DataRow drEmp in dsEmp.Tables[0].Rows)
                        {
                            
                            //Open EmpAttdRecord
                            sql = "Select tYear,tDate,CompCode,WrkGrp,EmpUnqID,ScheDuleShift,ConsShift,ConsIN,ConsOut,ConsWrkHrs,ConsOverTime," +
                                "Status,HalfDay,LeaveTyp,LeaveHalf,ActualStatus,Earlycome,EarlyGoing,GracePeriod," +
                                "INPunch1,OutPunch1,WrkHrs1,INPunch2,OutPunch2,WrkHrs2,INPunch3,OutPunch3," +
                                "WrkHrs3,INPunch4,OutPunch4,WrkHrs4,TotalWorkhrs,TotalINPunchCount," +
                                "TotalOutPunchCount,LateCome,Rules,CalcOverTime,HalfDRule,partdate,CostCode,StdHrsOT,StdShftHrs,StdWrkHrs,StdWrkShift " +
                                " From AttdData where CompCode = '01' and tYear ='" + tFromDt.Year.ToString() + "'" +
                                " And WrkGrp ='" + drEmp["WrkGrp"].ToString() + "' and tDate ='" + tFromDt.ToString("yyyy-MM-dd") + "'" +
                                " And EmpUnqID ='" + drEmp["EmpUnqID"].ToString() + "'";

                            //create data adapter
                            DataSet dsAttdData = new DataSet();
                            SqlDataAdapter daAttdData = new SqlDataAdapter(new SqlCommand(sql, cn));
                            SqlCommandBuilder AttdCmdBuilder = new SqlCommandBuilder(daAttdData);

                            daAttdData.Fill(dsAttdData, "AttdData");

                            hasRows = dsAttdData.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                            if (hasRows)
                            {
                                foreach (DataRow drAttd in dsAttdData.Tables[0].Rows)
                                {
                                    clsProcess pr = new clsProcess();
                                    pr.Calc_StdHrs(daAttdData, dsAttdData, drAttd);
                                }
                            }
                                

                        }//foreach employee
                    }
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                }
            }//using
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtDate.EditValue == null)
            {
                MessageBox.Show("Please Select Date....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
           

            DateTime tCurDate,tDate;
            int tCount ;
    
            tCurDate = Convert.ToDateTime(Utils.Helper.GetDescription("Select CONVERT(VARCHAR(10), GETDATE(), 120)", Utils.Helper.constr));
            tDate = txtDate.DateTime;    
            tCount = Convert.ToInt32(Utils.Helper.GetDescription("Select Count(*) From MastCostCodeManPowerRpt where tDate ='" + tDate.ToString("yyyy-MM-dd") + "'", Utils.Helper.constr));
        
            //check for already process...
            TimeSpan ts = (tCurDate - tDate);
            if (Math.Abs(ts.Days) > 1)
            {
                if(tCount > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string err = string.Empty;
                    call_Calc_StdHrs(txtDate.DateTime.Date, out err);
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("System Does not allow to process/Process Already Done....","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
            }

           
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
               
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        cn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_rpt_CostCodeDailyManPower";                        
                        int result =0;

                        ////Creating instance of SqlParameter
                        SqlParameter sPdate = new SqlParameter();
                        sPdate.ParameterName = "@tDate";// Defining Name
                        sPdate.SqlDbType = SqlDbType.DateTime; // Defining DataType
                        sPdate.Direction = ParameterDirection.Input;// Setting the direction
                        sPdate.Value = tDate;
 
                        ////Creating instance of SqlParameter
                        SqlParameter sPresult = new SqlParameter();
                        sPresult.ParameterName = "@result"; // Defining Name
                        sPresult.SqlDbType = SqlDbType.Int; // Defining DataType
                        sPresult.Direction = ParameterDirection.Output;// Setting the direction 
                        sPresult.Value = result;
                        
                        cmd.Parameters.Add(sPdate);
                        cmd.Parameters.Add(sPresult);                        
                        cmd.ExecuteNonQuery();

                        //get the output
                        int t = (int)cmd.Parameters["@result"].Value;
                        if (t == 1)
                        {
                            string sql = string.Empty;
                            tCount = Convert.ToInt32(Utils.Helper.GetDescription("Select Count(*) From MastCostCodeProcessLog where tDate ='" + tDate.ToString("yyyy-MM-dd") + "'", Utils.Helper.constr));
                            
                            if(tCount == 0)
                            {
                                 sql = "Insert into MastCostCodeProcessLog (tDate,ProcessFlg,AddDt,AddId) Values ('{0:yyyy-MM-dd}','{1}',GetDate(),'{2}')";
                                 sql = string.Format(sql,tDate,1,Utils.User.GUserID);

                            }else{
                                sql = "Update MastCostCodeProcessLog set ProcessFlg = '{0}', UpdDt=GetDate(),UpdID = '{1}' where tDate = '{2:yyyy-MM-dd}';";
                                 sql = string.Format(sql,1,Utils.User.GUserID,tDate);
                            }
                            cmd = new SqlCommand();
                            cmd.Connection = cn;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();

                            string err = string.Empty;

                            call_Calc_StdHrs(txtDate.DateTime.Date, out err);
                            MessageBox.Show("Process Completed...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            string err = string.Empty;
                            call_Calc_StdHrs(txtDate.DateTime.Date, out err);
                            MessageBox.Show("Process Completed with some errors", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
                        ResetCtrl();
                        

                    }catch(Exception ex){
                        ResetCtrl();
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                
            }//using connection

            this.Cursor = Cursors.Default;

        }

        private void txtDate_EditValueChanged(object sender, EventArgs e)
        {
            GRights = Attendance.Classes.Globals.GetFormRights(this.Name);
            SetRights();
        }
    }
}
