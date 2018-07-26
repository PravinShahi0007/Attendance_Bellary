using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Attendance
{
    public partial class FrmConnection : Form
    {
        private Utils.DbCon dbcon = new Utils.DbCon() ;
        public  string typeofcon;
        private bool Sqlconnsts = false;

        public FrmConnection()
        {
            InitializeComponent();
        }

        private void FrmConnection_Load(object sender, EventArgs e)
        {
            if (typeofcon == "DBCON")
                this.Text = "Connection Builder : " + " Main Database";
            else
                this.Text = "Connection Builder : " + " Employee List Database";
            
            
            txtUserID.Text = "";
            txtPassword.Text = "";
            txtPassword.Enabled = true;
            txtUserID.Enabled = true;
            
           dbcon = Utils.Helper.ReadConDb(typeofcon);
           txtDataSource.Text = dbcon.DataSource.ToString();
           txtUserID.Text = dbcon.DbUser.ToString();
           txtPassword.Text = dbcon.Password.ToString();
           txtDataBaseName.Text = dbcon.DbName.ToString();
           cmbAuth.Text = (dbcon.WindowsAuthentication) ? "Windows Authentication" : "SQL Server Authentication";
           
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string err = datavalidate();
            if(!string.IsNullOrEmpty(err)){
                MessageBox.Show(err);
                lblStatus.Text = "";
                return;
            }

            dbcon.DataSource = txtDataSource.Text.Trim();
            dbcon.DbName = txtDataBaseName.Text.Trim();
            dbcon.WindowsAuthentication = (cmbAuth.Text == "Windows Authentication") ? true : false;

            if (!dbcon.WindowsAuthentication)
            {
                dbcon.DbUser = txtUserID.Text.Trim();
                dbcon.Password = txtPassword.Text.Trim();
            }

            using (SqlConnection cn = new SqlConnection(dbcon.ToString()))
            {
                try
                {
                    cn.Open();
                    cn.Close();
                    Sqlconnsts = true;
                    lblStatus.Text = "Connection sucessfully established";
                }catch(SqlException ex )
                {
                    Sqlconnsts = false;
                    lblStatus.Text = ex.ToString();
                }
            }

        }

        private void cmbAuth_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbAuth.Text == "Windows Authentication")
            {
                txtUserID.Text = "";
                txtPassword.Text = "";
                txtPassword.Enabled = false;
                txtUserID.Enabled = false;
            }
            else
            {
                txtUserID.Text = "";
                txtPassword.Text = "";
                txtPassword.Enabled = true;
                txtUserID.Enabled = true;
            }
        }

        private string datavalidate()
        {
            string err = string.Empty;

            if (txtDataSource.Text.Trim() == "")
                err += "Please Enter DataSource Name" + Environment.NewLine;
            if (txtDataBaseName.Text.Trim() == "")
                err += "Please Enter Database Name" + Environment.NewLine;

            switch (cmbAuth.Text.ToString().ToUpper())
            {
                case "WINDOWS AUTHENTICATION":
                    txtUserID.Text = "";
                    txtPassword.Text = "";
                    break;
                case "SQL SERVER AUTHENTICATION" :
                    if (txtUserID.Text.Trim() == "")
                        err += "Please Enter User Name" + Environment.NewLine;
                    if(txtPassword.Text.Trim() == "")
                        err += "Please Enter Password" + Environment.NewLine;
                    break;
                default:
                    
                    break;
            }

            return err;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnTest_Click(sender, e);

            if (Sqlconnsts)
            {
                Utils.Helper.WriteConDb(dbcon,typeofcon);
            }
        }

        private void FrmConnection_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter))
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }


    }
}
