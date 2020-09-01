using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Attendance.Classes;

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Data.SqlClient;

namespace Attendance.Forms
{
    public partial class frmMastConfigKeys : Form
    {
        private string mode = "NEW";
        private string GRights = "XXXV";
        private static string cnstr = Utils.Helper.constr;
        private string oldCode = "";

        private void ResetCtrl()
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            txtConfig_Key.Text = "";
            txtConfig_Val.Text = "";
            

            oldCode = "";
            mode = "NEW";
            SetRights();
            LoadGrid();
        }

        private string DataValidate()
        {
            string err = string.Empty;

            if (string.IsNullOrEmpty(txtConfig_Key.Text.Trim()))
                err += "Key is Required..";

            if (string.IsNullOrEmpty(txtConfig_Val.Text.Trim().ToString()))
                err += "Values is Required..";

            return err;
        }

        public frmMastConfigKeys()
        {
            InitializeComponent();
        }

        private void LoadGrid()
        {
            if (GRights.Contains("V"))
            {
                string err = string.Empty;
                string sql = "Select Config_Key,Config_Val,AddDt,AddId,UpdDt,UpdID From Mast_OtherConfig Where 1=1 Order By Config_Key";
                DataSet ds = Utils.Helper.GetData(sql,cnstr,out err);
                
                if (!string.IsNullOrEmpty(err))
                {
                    MessageBox.Show(err,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                   
                    return;
                }
                
                bool hasrows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

                if (hasrows)
                {
                    gridctrl.DataSource = ds;
                    gridctrl.DataMember = ds.Tables[0].TableName;
                }
            }
        }

        private void frmConfig_MastConfigKeys_Load(object sender, EventArgs e)
        {
            string err = string.Empty;
            GRights = Globals.GetFormRights(this.Name);

            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.BeginInvoke(new MethodInvoker(Close));
            }

            LoadGrid();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetCtrl();
            
        }


        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            DoRowDoubleClick(view, pt);
        }

        private void DoRowDoubleClick(GridView view, Point pt)
        {
            GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                txtConfig_Key.Text = gridView1.GetRowCellValue(info.RowHandle, "Config_Key").ToString();
                object o = new object();
                EventArgs e = new EventArgs();
                mode = "OLD";
                oldCode = txtConfig_Key.Text.ToString();
                txtConfig_Key_Validated(o, e);
            }

        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetRights()
        {
            if (txtConfig_Key.Text.Trim() != "" && mode == "NEW" && GRights.Contains("A"))
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            else if (txtConfig_Key.Text.Trim() != "" && mode == "OLD")
            {
                btnAdd.Enabled = false;

                if (GRights.Contains("U"))
                    btnUpdate.Enabled = true;
                if (GRights.Contains("D"))
                    btnDelete.Enabled = true;
            }

            if (GRights.Contains("XXXV"))
            {
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }

            if (GRights.Contains("XXXX"))
            {
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void txtConfig_Key_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfig_Key.Text.Trim()))
            {
                mode = "NEW";
                oldCode = "";
                return;
            }

            DataSet ds = new DataSet();
            string sql = "select * From Mast_OtherConfig where Config_Key='" + txtConfig_Key.Text.Trim() + "'";
            string err = string.Empty;
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr,out err);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtConfig_Key.Text = dr["Config_Key"].ToString();
                    txtConfig_Val.Text = dr["Config_Val"].ToString();
                    
                    mode = "OLD";
                    oldCode = dr["Config_Key"].ToString();
                }
            }
            else
            {
                mode = "NEW";
                oldCode = "";
            }


            SetRights();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(err))
            {
                using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cn.Open();
                            string sql = "Insert into  Mast_OtherConfig (Config_Key,Config_Val,AddDt,AddID) Values ('" +
                                "" + txtConfig_Key.Text.Trim().ToString() + "','" + txtConfig_Val.Text.Trim().ToString() + "',GetDate(),'" + Utils.User.GUserID + "');";
                            
                            cmd.CommandText = sql;
                            cmd.Connection = cn;
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Record Added...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetCtrl();
                            LoadGrid();
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(err))
            {
                using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cn.Open();
                            string sql = "Update Mast_OtherConfig Set Config_Val='" + txtConfig_Val.Text.Trim().ToString() + "',UpdDt = GetDate(),UpdID = '" + Utils.User.GUserID + "' where Config_Key = '" + txtConfig_Key.Text.Trim().ToString() + "'";
                            cmd.CommandText = sql;
                            cmd.Connection = cn;
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Record Updated...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetCtrl();
                            LoadGrid();
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Not implemented..Please Contact System Administrator", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //if (string.IsNullOrEmpty(err))
            //{

            //    DialogResult qs = MessageBox.Show("Are You Sure to Delete this Record...?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (qs == DialogResult.No)
            //    {
            //        return;
            //    }

            //    using (SqlConnection cn = new SqlConnection(Helper.constr))
            //    {
            //        using (SqlCommand cmd = new SqlCommand())
            //        {
            //            try
            //            {
            //                cn.Open();
            //                string sql = "Update Mast_Location Set Active = 0,UpdDt = GetDate(),UpdID = '" + User.GUserID + "' where LocID = '" + txtConfig_Key.Text.Trim().ToString() + "'";
            //                cmd.CommandText = sql;
            //                cmd.Connection = cn;
            //                cmd.ExecuteNonQuery();


            //                MessageBox.Show("Record Deleted...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                ResetCtrl();
            //                LoadGrid();
            //                return;
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                return;
            //            }
            //        }
            //    }
            //}
        }
    }
}
