using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using Attendance.Classes;
using Attendance.RS2005;
using Attendance.RE2005;
//using Microsoft.ReportingServices.Interfaces;
using ParameterValue = Attendance.RE2005.ParameterValue;
using Warning = Attendance.RE2005.Warning;



namespace Attendance.Forms
{
    public partial class frmAutoMailSender : Form
    {
        private static string networkuser = Globals.G_NetworkUser, networkdomain = Globals.G_NetworkDomain, networkpassword = Globals.G_NetworkPass;
        private static string subscrsql = string.Empty;
        public frmAutoMailSender()
        {
            InitializeComponent();
        }

        private void frmAutoMailSender_Load(object sender, EventArgs e)
        {
            btnReset_Click(sender,e);
            
        }


        private void varinit()
        {
            txtUserName.Text = Utils.User.GUserID;
            txtPassword.Text = Utils.User.GUserPass;
            txtSubScrID.Text = "";
            lstWrkGrp.Items.Clear();
            lstSubScr.Items.Clear();
            txtDtFrom.Value = DateTime.Now;
            txtDtTo.Value =  DateTime.Now;
            subscrsql = string.Empty;

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                
                try
                {
                    cn.Open();
                    DataSet ds = Utils.Helper.GetData("Select WrkGrp From MastWorkGrp Order By WrkGrp", Utils.Helper.constr);
                    
                    bool hasRows = false;
                    hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                    if (hasRows)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            lstWrkGrp.Items.Add( row["WrkGrp"].ToString() );
                        }
                    }
                    btnAdd.Enabled = true;
                    btnRemove.Enabled = true;
                    btnSend.Enabled = true;
                }
                catch (Exception ex)
                {

                    btnAdd.Enabled = false;
                    btnRemove.Enabled = false;
                    btnSend.Enabled = false;
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }


        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            bool userok = GetUserVerify();
            if (Utils.User.GUserID == "SERVER")
            {
                userok = true;
            }
            
            
            bool sqlok = BuildSQL();
            if (!userok)
                return;

            if (!sqlok)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(networkdomain) || string.IsNullOrWhiteSpace(networkuser) ||
                string.IsNullOrWhiteSpace(networkpassword))
            {
                MessageBox.Show("Network Authentication is not available to send reports", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //check if report type is selected or not
            
            if (cmbRptType.SelectedIndex < 0)
            {
                MessageBox.Show("Please Select the Report Type", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string err = string.Empty;
            switch (cmbRptType.SelectedItem.ToString())
            {
                case "Monthly Attendance Report":
                    err = ValidateMonthlyAttd();
                    break; /* optional */
                case "Daily Performance Report":
                    err = ValidateDailyPerformance();
                    break; /* optional */
                case "Monthly Lunch Halfday Report":
                    err = ValidateDailyPerformance();
                    break; /* optional */
            }

            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            /* now lock all controls */
            btnAdd.Enabled = false;
            btnRemove.Enabled = false;
            cmbRptType.Enabled = false;
            txtUserName.Enabled = false;
            txtPassword.Enabled = false;
            btnReset.Enabled = false;
            lstWrkGrp.Enabled = false;
            btnSend.Enabled = false;

            DataSet ds = Utils.Helper.GetData(subscrsql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
            if (!hasRows)
            {
                MessageBox.Show("No any Subscription found in selected criteria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }
            pBar1.Minimum = 0;
            pBar1.Maximum = ds.Tables[0].Rows.Count;
            pBar1.Value = 0;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                NetworkCredential clientCredentials = new NetworkCredential(networkuser, networkpassword, networkdomain);
                ReportingService2010 rs = new ReportingService2010();
                rs.Credentials = clientCredentials;
                
                //rs.Url = "http://172.16.12.47/reportserver/reportservice2010.asmx";
                rs.Url = Globals.G_ReportServiceURL;

                ReportExecutionService rsExec = new ReportExecutionService();
                rsExec.Credentials = clientCredentials;
                //rsExec.Url = "http://172.16.12.47/reportserver/reportexecution2005.asmx";
                rsExec.Url = Globals.G_ReportSerExeUrl;
                string historyID = null;
                string reportPath = string.Empty;
                string deviceInfo = null;
                string extension;
                string encoding;
                string mimeType;
                Warning[] warnings = null;
                string[] streamIDs = null;
                string format = "EXCEL";
                Byte[] results;
                string subscrid = row["SubScriptionID"].ToString();
                

                switch (cmbRptType.SelectedItem.ToString())
                {
                        

                    case "Monthly Attendance Report":
                        reportPath = "/Attendance/Automail Reports/Monthly Attendance Report";
                        rsExec.LoadReport(reportPath, historyID);
                        ParameterValue[] executionParams = new ParameterValue[4];
                        executionParams[0] = new ParameterValue();
                        executionParams[0].Name = "WrkGrp";
                        executionParams[0].Value = row["Param1WrkGrp"].ToString();

                        executionParams[1] = new ParameterValue();
                        executionParams[1].Name = "SubScriptionID";
                        executionParams[1].Value = row["SubScriptionID"].ToString();

                        executionParams[2] = new ParameterValue();
                        executionParams[2].Name = "FromDt";
                        executionParams[2].Value = txtDtFrom.Value.ToString("yyyy-MM-dd");

                        executionParams[3] = new ParameterValue();
                        executionParams[3].Name = "ToDate";
                        executionParams[3].Value = txtDtTo.Value.ToString("yyyy-MM-dd");
                        rsExec.SetExecutionParameters(executionParams, "en-us");
                        string substr1 = "Monthly Attendance Report For " + txtDtFrom.Value.ToString("dd-MMM") + " To " + txtDtTo.Value.ToString("dd-MMM");
                        results = rsExec.Render(format, deviceInfo, out extension, out mimeType, out encoding,out warnings, out streamIDs);
                        MailAttachment m = new MailAttachment(results, "Monthly Attendance Report.xls");
                        Email(row["EmailTo"].ToString(), row["EmailCopy"].ToString(), row["BCCTo"].ToString(),
                            "Monthly Attendance Report" , substr1, Globals.G_DefaultMailID, "Attendance System", "", "",subscrid, m );

                        break; /* optional */

                    case "Daily Performance Report":
                        reportPath = "/Attendance/Automail Reports/New Daily Performance Report With Date Para";
                        rsExec.LoadReport(reportPath, historyID);
                        ParameterValue[] executionParams1 = new ParameterValue[3];
                        executionParams1[0] = new ParameterValue();
                        executionParams1[0].Name = "WrkGrp";
                        executionParams1[0].Value = row["Param1WrkGrp"].ToString();

                        executionParams1[1] = new ParameterValue();
                        executionParams1[1].Name = "SubScriptionID";
                        executionParams1[1].Value = row["SubScriptionID"].ToString();

                        executionParams1[2] = new ParameterValue();
                        executionParams1[2].Name = "tDate";
                        executionParams1[2].Value = txtDtFrom.Value.ToString("yyyy-MM-dd");
                        rsExec.SetExecutionParameters(executionParams1, "en-us");
                        string substr2 = "Daily Performance Report For " + txtDtFrom.Value.ToString("dd-MMM");
                        results = rsExec.Render(format, deviceInfo, out extension, out mimeType, out encoding,out warnings, out streamIDs);
                        MailAttachment m1 = new MailAttachment(results, "Daily Performance Report.xls");
                        Email(row["EmailTo"].ToString(), row["EmailCopy"].ToString(), row["BCCTo"].ToString(),
                            "Daily Performance Report",substr2, Globals.G_DefaultMailID, "Attendance System", "", "", subscrid,m1);

                        break; /* optional */
                    case "Monthly Lunch Halfday Report":
                        reportPath = "/Attendance/Automail Reports/Monthly Lunch Halfday Report";
                        rsExec.LoadReport(reportPath, historyID);
                        ParameterValue[] executionParams2 = new ParameterValue[4];
                        executionParams2[0] = new ParameterValue();
                        executionParams2[0].Name = "WrkGrp";
                        executionParams2[0].Value = row["Param1WrkGrp"].ToString();

                        executionParams2[1] = new ParameterValue();
                        executionParams2[1].Name = "SubScriptionID";
                        executionParams2[1].Value = row["SubScriptionID"].ToString();

                        executionParams2[2] = new ParameterValue();
                        executionParams2[2].Name = "pFromDt";
                        executionParams2[2].Value = txtDtFrom.Value.ToString("yyyy-MM-dd");

                        executionParams2[3] = new ParameterValue();
                        executionParams2[3].Name = "pToDt";
                        executionParams2[3].Value = txtDtTo.Value.ToString("yyyy-MM-dd");
                        rsExec.SetExecutionParameters(executionParams2, "en-us");
                        string substr3 = "Monthly Lunch Halfday Report For " + txtDtFrom.Value.ToString("dd-MMM") + " To " + txtDtTo.Value.ToString("dd-MMM");
                       
                        results = rsExec.Render(format, deviceInfo, out extension, out mimeType, out encoding,out warnings, out streamIDs);
                        MailAttachment m2 = new MailAttachment(results, "Monthly Lunch Halfday Report.xls");
                        Email(row["EmailTo"].ToString(), row["EmailCopy"].ToString(), row["BCCTo"].ToString(),
                            "Monthly Lunch Halfday Report", substr3, Globals.G_DefaultMailID, "Attendance System", "", "",subscrid, m2);




                        break;
                }
                lblSid.Text = "Exec : SID - " + subscrid;
                lblSid.Update();
                pBar1.Value += 1;
                if (string.IsNullOrEmpty(reportPath))
                    break;


            }


            /* now unlock all controls */
            btnAdd.Enabled = true;
            btnRemove.Enabled = true;
            cmbRptType.Enabled = true;
            txtUserName.Enabled = true;
            txtPassword.Enabled = true;
            btnReset.Enabled = true;
            lstWrkGrp.Enabled = true;
            btnSend.Enabled = true;

        }


        /// <summary>
        /// Send an email
        /// </summary>
        /// <param name="to">Message to address</param>
        /// <param name="bcc"></param>
        /// <param name="body">Text of message to send</param>
        /// <param name="subject">Subject line of message</param>
        /// <param name="fromAddress">Message from address</param>
        /// <param name="fromDisplay">Display name for "message from address"</param>
        /// <param name="credentialUser">User whose credentials are used for message send</param>
        /// <param name="credentialPassword">User password used for message send</param>
        /// <param name="subscriptionid">SubScriptionID used for error msg</param>
        /// <param name="attachments">Optional attachments for message</param>
        /// <param name="cc"></param>
        public static void Email(string to,
                                 string cc,
                                 string bcc,
                                 string body,
                                 string subject,
                                 string fromAddress,
                                 string fromDisplay,
                                 string credentialUser,
                                 string credentialPassword,
                                 string subscriptionid,
                                 params MailAttachment[] attachments)
        {
            string host = Globals.G_SmtpHostIP;
            //body = "";// UpgradeEmailFormat(body);
            try
            {
                MailMessage mail = new MailMessage();
                mail.Body = body;
                mail.IsBodyHtml = true;
                
                string[] mailto = to.Split(';');
                string[] mailcc = cc.Split(';');
                string[] mailbcc = bcc.Split(';');

                
                foreach (string tto in mailto)
                {
                    if (!string.IsNullOrWhiteSpace(tto))
                    {
                        mail.To.Add(new MailAddress(tto));
                    }
                   
                }

                foreach (string tcc in mailcc)
                {
                    if (!string.IsNullOrWhiteSpace(tcc))
                    {
                        mail.CC.Add(new MailAddress(tcc));
                    }
                   
                }

                foreach (string tbcc in mailbcc)
                {
                    if (!string.IsNullOrWhiteSpace(tbcc))
                    {
                        mail.Bcc.Add(new MailAddress(tbcc));
                    }
                    
                }

                if (mailto.Count() <= 0 && to.Trim().Length > 0)
                {
                    mail.To.Add(new MailAddress(to));
                }

                if (mailcc.Count() <= 0 && cc.Trim().Length > 0)
                {
                    mail.CC.Add(new MailAddress(cc));
                }

                if (mailbcc.Count() <= 0 && bcc.Trim().Length > 0)
                {
                    mail.Bcc.Add(new MailAddress(bcc));
                }

                mail.From = new MailAddress(fromAddress, fromDisplay, Encoding.UTF8);
                mail.Subject = subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.Normal;
                foreach (MailAttachment ma in attachments)
                {
                    mail.Attachments.Add(ma.File);
                }
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new System.Net.NetworkCredential(credentialUser, credentialPassword);
                smtp.Host = host;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder(1024);
                sb.Append("\nSubScriptionID:" + subscriptionid);
                sb.Append("\nTo:" + to);
                sb.Append("\nCC:" + cc);
                sb.Append("\nBCC:" + bcc);
                sb.Append("\nbody:" + body);
                sb.Append("\nsubject:" + subject);
                sb.Append("\nfromAddress:" + fromAddress);
                sb.Append("\nfromDisplay:" + fromDisplay);
               
                MessageBox.Show(ex.ToString() + Environment.NewLine + sb,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            varinit();
            
        }


        private string ValidateMonthlyAttd()
        {
            string err = string.Empty;
           
            if (txtDtFrom.Value > txtDtTo.Value)
            {
                err += "Form Date should not be greator than to date";
            }
            DateTime fromDate = txtDtFrom.Value;
            DateTime todate = txtDtTo.Value;
            TimeSpan tspan = fromDate - todate;
            int differenceInDays = tspan.Days;

            if (differenceInDays > 31)
                err += "Does not Allow more than 31 days";

            

            return err;
        }

        private bool GetUserVerify()
        {
             string err = string.Empty;

            if (string.IsNullOrEmpty(txtUserName.Text.ToString().Trim()))
                err += "UserName is required" + Environment.NewLine;
            
            if (string.IsNullOrEmpty(txtPassword.Text.ToString().Trim()))
                err += "Password is required";

            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error",  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
                

            DataSet ds = new DataSet();
            string sql = "Select top 1 a.*, b.Add1,b.Update1,b.Delete1,b.View1 from MastUser a,UserRights b Where a.UserID = B.UserID And a.Active = 'Y' and a.UserID='" + txtUserName.Text.Trim() +
                         "' And a.Pass ='" + txtPassword.Text.Trim() + "' and b.FormID = 490";
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            Boolean hasRows = false;
            hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
            if (!hasRows)
            {
                MessageBox.Show("Invalid UserName/Password Or Not Authorized", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            else
            {
                bool tAdd = Convert.ToBoolean(ds.Tables[0].Rows[0]["Add1"]);
                bool tUpd = Convert.ToBoolean(ds.Tables[0].Rows[0]["Update1"]);
                bool tDel = Convert.ToBoolean(ds.Tables[0].Rows[0]["Delete1"]);
                bool tVie = Convert.ToBoolean(ds.Tables[0].Rows[0]["View1"]);


                if (!tAdd || !tUpd || !tDel || !tVie)
                {
                    MessageBox.Show("Invalid UserName/Password Or Not Authorized", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    return false;
                }
            }

            
            return true;
        }

        private string ValidateDailyPerformance()
        {
            string err = string.Empty;
            

            return err;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSubScrID.Text))
            {
                MessageBox.Show("Please Enter Subscription ID..");
                return;
            }

            int number;

            bool result = Int32.TryParse(txtSubScrID.Text.ToString(), out number);
            if (!result)
            {
                MessageBox.Show("Please Enter Valid Subscription ID..");
            }
            else
            {
                lstSubScr.Items.Add(number.ToString());
                txtSubScrID.Text = "";
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstSubScr.Items.Count > 0 )
            {
                lstSubScr.Items.RemoveAt(lstSubScr.SelectedIndex);
            }
        }

        private bool BuildSQL()
        {
            subscrsql = "Select * From AutoMailSubScription Where ";
            bool chk = false;
            if (lstSubScr.Items.Count > 0)
            {
                StringBuilder items = new StringBuilder();
                foreach (object item in lstSubScr.Items)
                {
                    items.Append("'" + item + "'").Append(",");

                }
                lstWrkGrp.ClearSelected();
                lstWrkGrp.Refresh();
                
                subscrsql += " SubScriptionID in (" + string.Join(",", items.ToString().TrimEnd(',')) + ")";
                chk = true;
            }
            else
            {
                if (lstWrkGrp.CheckedItems.Count > 0)
                {
                    StringBuilder items = new StringBuilder();
                    foreach (object checkedItem in lstWrkGrp.CheckedItems)
                    {
                        items.Append("'" + checkedItem + "'").Append(",");
                    }
                    subscrsql += " param1wrkgrp in (" + items.ToString().TrimEnd(',') + ")";
                    chk = true;
                }
                else
                {
                    MessageBox.Show("WrkGrp OR SubScriptinID is required..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return chk;
        }

        private void txtSubScrID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select SubScriptionID,EmailTo,EmailCopy From AutoMailSubScription Where 1=1";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "SubScriptionID", "SubScriptionID", typeof(int), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else if (e.KeyCode == Keys.F2)
                {
                    obj = (List<string>)hlp.Show(sql, "EmailTo", "EmailTo", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtSubScrID.Text = obj.ElementAt(0).ToString();
                    
                }
            }
        }
    }




}
