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
    public partial class frmMastComp : Form
    {
        public string GRights = "XXXV";
        
        public frmMastComp()
        {
            InitializeComponent();
        }



        private void frmMastComp_Load(object sender, EventArgs e)
        {
            string sql = "Select top 1 * from MastComp Where 1 = 1" ;
                DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);

                bool hasrows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

                if (hasrows)
                {
                    txtCompCode.EditValue = ds.Tables[0].Rows[0]["CompCode"].ToString();
                    txtCompName.EditValue = ds.Tables[0].Rows[0]["CompName"].ToString();
                    txtCompSName.EditValue = ds.Tables[0].Rows[0]["CompSName"].ToString();
                    txtAdd1.EditValue = ds.Tables[0].Rows[0]["Add1"].ToString();
                    txtAdd2.EditValue = ds.Tables[0].Rows[0]["Add2"].ToString();
                    txtCity.EditValue = ds.Tables[0].Rows[0]["City"].ToString();
                    txtPinCode.EditValue = ds.Tables[0].Rows[0]["Pin"].ToString();
                    txtState.EditValue = ds.Tables[0].Rows[0]["State"].ToString();
                    txtCountry.EditValue = ds.Tables[0].Rows[0]["Country"].ToString();
                }
                else
                {

                    txtCompCode.EditValue = string.Empty;
                    txtCompName.EditValue = string.Empty;
                    txtCompSName.EditValue = string.Empty;
                    txtAdd1.EditValue = string.Empty;
                    txtAdd2.EditValue = string.Empty;
                    txtCity.EditValue = string.Empty;
                    txtPinCode.EditValue = string.Empty;
                    txtState.EditValue = string.Empty;
                    txtCountry.EditValue = string.Empty;
                }

                GRights = Attendance.Classes.Globals.GetFormRights("frmCompMast");
                SetRights();
            
        }

        private void SetRights()
        {
            if (GRights.Contains("A") || GRights.Contains("U"))
            {
                btnSubmit.Enabled = true;
            }
            

            if (GRights.Contains("XXXV"))
            {
                btnSubmit.Enabled = false;            
            }
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cn.Open();
                        cmd.Connection = cn;

                        string sql = "Select Count(*) from MastComp ";
                        cmd.CommandText = sql;
                        int t = (int)cmd.ExecuteScalar();

                        if (t == 0)
                        {
                            sql = "Insert into MastComp (CompCode,CompName,CompSName,Add1,Add2,City,Pin,Country,State,AddDt,AddId) values (" 
                              + "'" + txtCompCode.Text.Trim().ToString() + "'," 
                              + "'" + txtCompName.Text.Trim().ToString() + "',"
                            + "'" + txtCompSName.Text.Trim().ToString() + "',"
                            + "'" + txtAdd1.Text.Trim().ToString() + "',"
                            + "'" + txtAdd2.Text.Trim().ToString() + "'," 
                            + "'" + txtCity.Text.Trim().ToString() + "'"
                            + "'" + txtPinCode.Text.Trim().ToString() + "'"
                            + "'" + txtState.Text.Trim().ToString() + "'"
                            + "'" + txtCountry.Text.Trim().ToString() + "',GetDate(),'" + Utils.User.GUserID + "')";
                            

                        }
                        else
                        {
                            sql = "Update MastComp set CompName=" + "'" + txtCompCode.Text.Trim().ToString() + "',"
                              + " CompName = '" + txtCompName.Text.Trim().ToString() + "',"
                            + " CompSName = '" + txtCompSName.Text.Trim().ToString() + "',"
                            + " Add1 = '" + txtAdd1.Text.Trim().ToString() + "',"
                            + " Add2 = '" + txtAdd2.Text.Trim().ToString() + "',"
                            + " City = '" + txtCity.Text.Trim().ToString() + "'"
                            + " Pin = '" + txtPinCode.Text.Trim().ToString() + "'"
                            + " State = '" + txtState.Text.Trim().ToString() + "'"
                            + " Country = '" + txtCountry.Text.Trim().ToString() + "',UpdDt = GetDate()," 
                            + " UpdID = '" + Utils.User.GUserID + "' " 
                            + " where CompCode = '" + txtCompCode.Text.Trim().ToString() + "'";
                        }

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Company configuration saved...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        

                    }catch(Exception ex){
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        
    }
}
