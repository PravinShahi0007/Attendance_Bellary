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
    public partial class frmUserRights : Form
    {
        public static string mode = "";
        
        public frmUserRights()
        {
            InitializeComponent();
        }

        private void txtUserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2 )
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select UserID,UserName,Pass,Active From MastUser Where Active = 'Y'  ";
                if (e.KeyCode == Keys.F1)
                {                   

                    obj = (List<string>)hlp.Show(sql, "UserID", "UserID", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else if (e.KeyCode == Keys.F2)
                {

                    obj = (List<string>)hlp.Show(sql, "UserName", "UserName", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 200, 400, 600, 100, 100);
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

                    txtUserID.Text = obj.ElementAt(0).ToString();
                    txtPersonName.Text = obj.ElementAt(1).ToString();
                    txtPassword.Text = obj.ElementAt(2).ToString();
                    if (obj.ElementAt(3).ToString().Trim() == "Y")
                    {
                        chkActive.Checked = true;
                    }else
                    {
                        chkActive.Checked = false;
                    }
                    mode = "OLD";
                }
            }
        }

        private void txtModID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select FormID,FormName From MastFrm where 1 = 1  ";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "FormID", "FormID", typeof(int), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else if (e.KeyCode == Keys.F2)
                {

                    obj = (List<string>)hlp.Show(sql, "FormName", "FormName", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 200, 400, 600, 100, 100);
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

                    txtModID.Text = obj.ElementAt(0).ToString();
                    txtModName.Text = obj.ElementAt(1).ToString();
                    
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if(!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //userid create/update
            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    cn.Open();
                    string sql = string.Empty;

                    SqlCommand cmd = new SqlCommand("Select Count(*) from MastUser Where UserID ='" + txtUserID.Text.Trim() + "'", cn);
                    int cnt = (int)cmd.ExecuteScalar();
                    if (cnt > 0)
                    {
                        sql = "Update MastUser Set UserName = '" + txtPersonName.Text.Trim() + "', " +
                            " Pass = '" + txtPassword.Text.Trim() + "', Active = '" + ((this.chkActive.Checked) ? "Y" : "N") + "', " +
                            " IsAdmin ='" + ((this.chkSuperUser.Checked) ? "1" :"0") + "'," +
                            " UpdDt = GetDate(), UpdID = '" + Utils.User.GUserID + "' " +
                            " Where UserID = '" + txtUserID.Text.Trim() + "'";
                        cmd = new SqlCommand(sql, cn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User Updated...");
                        LoadGrid();
                    }
                    else
                    {
                        sql = "Insert into MastUser (UserID,UserName,Pass,Active,IsAdmin,AddDt,AddId) values (" +
                            " '" + txtUserID.Text.Trim() + "','" + txtPersonName.Text.Trim() + "','" + txtPassword.Text.Trim() + "'," +
                            " '" + ((this.chkActive.Checked) ? "Y" : "N") + "', '" + " IsAdmin ='" + ((this.chkSuperUser.Checked) ? "1" :"0") + "',GetDate() ,'" + Utils.User.GUserID + "') ";
                        
                        cmd = new SqlCommand(sql, cn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("New User Created...");
                        LoadGrid();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadGrid()
        {
            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    cn.Open();
                    string sql = "Select a.FormID,b.FormName,a.Add1,a.Update1,a.Delete1,a.View1 " + 
                        " from UserRights a, MastFrm b where a.FormID = b.FormID and UserID = '" + txtUserID.Text.Trim() + "'" ;

                    DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
                    GridRights.DataSource = ds;
                    GridRights.DataMember = ds.Tables[0].TableName;
                    
                    
                    txtModID.Text = "";
                    txtModName.Text = "";
                    chkAdd.CheckState = CheckState.Unchecked;
                    chkUpdate.CheckState = CheckState.Unchecked;
                    chkDelete.CheckState = CheckState.Unchecked;
                    chkView.CheckState = CheckState.Unchecked;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private string DataValidate()
        {
            string err = string.Empty;

            if (string.IsNullOrEmpty(txtUserID.Text))
            {
                err = err + "Please Enter UserID " + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtPersonName.Text))
            {
                err = err + "Please Enter User Name " + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                err = err + "Please Enter Password" + Environment.NewLine;
            }
            
            return err;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserID.Text))
                return;
            if (string.IsNullOrEmpty(txtModID.Text.Trim()))
                return;
            //userRights create/update
            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    cn.Open();
                    string sql = string.Empty;

                    SqlCommand cmd = new SqlCommand("Select Count(*) from UserRights Where UserID ='" + txtUserID.Text.Trim() + "' and FormID ='" + txtModID.Text.Trim() + "'", cn);
                    int cnt = (int)cmd.ExecuteScalar();
                    if (cnt > 0)
                    {
                        sql = "Update UserRights Set Add1 = '" + ((this.chkAdd.Checked) ? "1" : "0") + "'," +
                            " Update1 = '" + ((this.chkUpdate.Checked) ? "1" : "0") + "'," +
                            " Delete1 = '" + ((this.chkDelete.Checked) ? "1" : "0") + "'," +
                            " View1 ='" + ((this.chkView.Checked) ? "1" : "0") + "'," +
                            " UpdDt = GetDate(), UpdID = '" + Utils.User.GUserID + "' " +
                            " Where UserID = '" + txtUserID.Text.Trim() + "' and FormID ='" + txtModID.Text.Trim() + "'";
                        cmd = new SqlCommand(sql, cn);
                        cmd.ExecuteNonQuery();
                        LoadGrid();
                    }
                    else
                    {
                        sql = "Insert into UserRights (UserID,FormID,Add1,Update1,Delete1,View1,AddDt,AddId) values (" +     
                            " '" + txtUserID.Text.Trim() + "','" + txtModID.Text.Trim() + "'," +
                            " '" + ((this.chkAdd.Checked) ? "1" : "0") + "','" + ((this.chkUpdate.Checked) ? "1" : "0") + "'," +
                            " '" + ((this.chkDelete.Checked) ? "1" : "0") + "','" + ((this.chkView.Checked) ? "1" : "0") + "'," +
                            " GetDate() ,'" + Utils.User.GUserID + "') ";
                        cmd = new SqlCommand(sql, cn);
                        cmd.ExecuteNonQuery();
                        LoadGrid();
                    }

                    



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserID.Text))
                return;
            if (string.IsNullOrEmpty(txtModID.Text.Trim()))
                return;
            //userRights create/update
            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    cn.Open();
                    string sql = string.Empty;

                    SqlCommand cmd = new SqlCommand("Select Count(*) from UserRights Where UserID ='" + txtUserID.Text.Trim() + "' and FormID ='" + txtModID.Text.Trim() + "'", cn);
                    int cnt = (int)cmd.ExecuteScalar();
                    if (cnt > 0)
                    {
                        sql = "Delete From UserRights Where UserID = '" + txtUserID.Text.Trim() + "' and FormID ='" + txtModID.Text.Trim() + "'";
                        cmd = new SqlCommand(sql, cn);
                        cmd.ExecuteNonQuery();
                        LoadGrid();
                    }
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtUserID_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserID.Text.Trim()))
                return;

            DataSet ds = Utils.Helper.GetData("Select * from MastUser Where UserID ='" + txtUserID.Text.Trim() + "'", Utils.Helper.constr);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    txtUserID.Text = dr["UserID"].ToString();
                    txtPersonName.Text = dr["UserName"].ToString();
                    txtPassword.Text = dr["Pass"].ToString();
                    if (dr["Active"].ToString() == "Y")
                    {
                        chkActive.CheckState = CheckState.Checked;
                        
                    }
                    else
                        chkActive.CheckState = CheckState.Unchecked;

                    if(Convert.ToBoolean(dr["IsAdmin"]))
                    {
                        chkSuperUser.Checked = true;
                    }
                    else
                    {
                        chkSuperUser.Checked = false;
                    }
                }
                else
                {
                    chkSuperUser.Checked = false;
                }

            }
            else
            {
                chkSuperUser.Checked = false;
            }

            LoadGrid();
        }

        private void txtModID_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtModID.Text.Trim()))
                return;


            DataSet ds = Utils.Helper.GetData("Select * from UserRights Where UserID ='" + txtUserID.Text.Trim() + "' and FormID ='" + txtModID.Text.Trim() + "'", Utils.Helper.constr);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                { 
                    DataRow dr = ds.Tables[0].Rows[0];
                    if (Convert.ToBoolean(dr["Add1"]))
                        chkAdd.CheckState = CheckState.Checked;
                  
                    if (Convert.ToBoolean(dr["Update1"]))
                        chkUpdate.CheckState = CheckState.Checked;
                  
                    if (Convert.ToBoolean(dr["Delete1"]))
                        chkDelete.CheckState = CheckState.Checked;
                  
                    if (Convert.ToBoolean(dr["View1"]))
                        chkView.CheckState = CheckState.Checked;
                  
                }
            }

            
        }
    }
}
