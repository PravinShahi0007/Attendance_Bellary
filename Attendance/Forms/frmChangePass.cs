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
    public partial class frmChangePass : Form
    {
        public frmChangePass()
        {
            InitializeComponent();
        }

        private void frmChangePass_Load(object sender, EventArgs e)
        {
            txtUserID.Text = Utils.User.GUserID;
            txtPassword.Text = Utils.User.GUserPass;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim().Length <= 3)
            {
                MessageBox.Show("Make Sure password length is grater than 3 char.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        cmd.CommandText = "Update MastUser Set Pass = '" + txtPassword.Text.Trim().ToString() + "' where UserID ='" + Utils.User.GUserID + "'";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Password updated you need to relogin to application..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();

                        Form f = Application.OpenForms["frmMain"];
                        f.Hide();
                        Utils.User.GUserID = string.Empty;
                        Utils.User.GUserName = string.Empty;
                        Utils.User.GUserPass = string.Empty;
                        Utils.User.IsAdmin = false;
                        
                        Program.OpenMDIFormOnClose = false;
                        Application.Restart();
                        

                    }catch(Exception ex){
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }
    }
}
