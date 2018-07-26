using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace Attendance
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string err = DataValidate();


            Utils.DbCon dbcon = Utils.Helper.ReadConDb("DBCON");
            Utils.DbCon empdbcon = Utils.Helper.ReadConDb("EMPDBCON");

            if (string.IsNullOrEmpty(dbcon.DataSource))
            {
                var b = new FrmConnection();
                b.typeofcon = "DBCON";
                b.ShowDialog();
                return;
            }
            else
            {
                Utils.Helper.constr = dbcon.ToString();
            }


            

            if (string.IsNullOrEmpty(err))
            {
                string sql = "Select * from MastUser Where UserID = '{0}' and Pass = '{1}' and Active = 'Y'" ;
                DataSet ds = Utils.Helper.GetData(string.Format(sql, txtUserName.Text, txtPassword.Text), dbcon.ToString());

                bool hasrows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

                if (hasrows)
                {
                    Utils.User.GUserID = txtUserName.Text.Trim();
                    Utils.User.GUserPass = txtPassword.Text.Trim();
                    Utils.User.IsAdmin = (Convert.ToBoolean(ds.Tables[0].Rows[0]["IsAdmin"])) ? true : false;
                    Utils.User.GUserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                   
                    this.Hide();
                    Program.OpenMDIFormOnClose = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid login id or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string DataValidate()
        {
            string err = string.Empty;

            if(string.IsNullOrEmpty(txtUserName.Text))
            {
                err = err + "UserID is required..." + Environment.NewLine;
            }

            if(string.IsNullOrEmpty(txtPassword.Text))
            {
                err = err + "Password is required..." + Environment.NewLine;
            }

            return err;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtPassword.Text = "";

            #region loadserverside

            try
            {
                Utils.DbCon dbcon = Utils.Helper.ReadConDb("DBCON");

                if (string.IsNullOrEmpty(dbcon.DataSource))
                {
                    
                    return;
                }
                else
                {
                    Utils.Helper.constr = dbcon.ToString();
                }

                DataSet ds = Utils.Helper.GetData("Select Top 1 * From MastNetWork", Utils.Helper.constr);
                bool hasrows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                if (hasrows)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Attendance.Classes.Globals.G_ServerWorkerIP = row["ServerWorkerIP"].ToString();
                    }
                }

                string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
                List<string> localip = Utils.Helper.GetLocalIPAddress();
                if (localip.Count > 0)
                {
                    bool found = false;
                    
                    foreach (string ip in localip)
                    {
                        if (ip == Attendance.Classes.Globals.G_ServerWorkerIP)
                        {
                            Utils.User.GUserID = "SERVER";
                            Utils.User.GUserPass = string.Empty;
                            Utils.User.IsAdmin = false;
                            Utils.User.GUserName = "SERVER";
                            found = true;
                            break;
                        }
                    }
                    if(found)
                    {
                        this.Hide();
                        Program.OpenMDIFormOnClose = true;
                        this.Close();
                    }                    
                }


            }
            catch (Exception ex)
            {

            }
            #endregion

        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter))
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }
    }
}
