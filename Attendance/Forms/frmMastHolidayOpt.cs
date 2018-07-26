using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Attendance.Forms
{
    public partial class frmMastHolidayOpt : Form
    {
        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";

        public frmMastHolidayOpt()
        {
            InitializeComponent();
        }

        private void frmMastHolidayOpt_Load(object sender, EventArgs e)
        {
            ResetCtrl();
            GRights = Attendance.Classes.Globals.GetFormRights(this.Name);
            SetRights();            
        }

        private string DataValidate()
        {
            string err = string.Empty;



            if (string.IsNullOrEmpty(txtYear.Text))
            {
                err = err + "Please Enter Year.." + Environment.NewLine;
                return err;
            }

            if (string.IsNullOrEmpty(txtDate.Text))
            {
                err = err + "Please Enter Date.." + Environment.NewLine;
                return err;
            }

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                err = err + "Please Holiday Description.." + Environment.NewLine;
            }

            if (txtDate.DateTime.Year != Convert.ToInt32(txtYear.Text.Trim()))
            {
                err = err + "Invalid Year and Date Selection.." + Environment.NewLine;
            }
            

            return err;
        }
        
        private void ResetCtrl()
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            txtYear.Enabled = true;
            txtDate.Enabled = true;

            txtDate.EditValue = null;
            txtDescription.Text = "";

            oldCode = "";
            mode = "NEW";
        }

        private void SetRights()
        {
            if ( txtDate.EditValue == null && mode == "NEW" && GRights.Contains("A") )
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            else if (txtDate.EditValue != null && mode == "OLD")
            {
                btnAdd.Enabled = false;

                if(GRights.Contains("U"))
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
        }

        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        string sql = "Insert into HoliDayOptMast " +
                            "(tYear,tDate,PublicHLTyp,HLDesc," +
                            " AddDt,AddID) Values ('{0}','{1:yyyy-MM-dd}','{2}','{3}'," +
                            " GetDate(),'{4}')";

                        sql = string.Format(sql,txtYear.Text.Trim(),txtDate.DateTime,
                            "OH",txtDescription.Text.Trim().ToString(),                            
                            Utils.User.GUserID);

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record saved...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
                        ResetCtrl();
                        LoadGrid();
                        SetRights();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        string sql = "Update HoliDayOptMast Set HLDesc = '{0}' , UpdDt = GetDate(), UpdID = '{1}' " 
                            + " Where tYear ='{2}' "
                            + " And tDate = '{3:yyyy-MM-dd}' ";

                        sql = string.Format(sql, txtDescription.Text.Trim(),  Utils.User.GUserID,
                            txtYear.Text.Trim(),
                            txtDate.DateTime
                           );

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        ResetCtrl();
                        LoadGrid();
                        MessageBox.Show("Record Updated...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetRights();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (string.IsNullOrEmpty(err))
            {
               
                DialogResult qs = MessageBox.Show("Are You Sure to Delete this Leave...?","Question",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if(qs == DialogResult.No){
                    return;
                }
                
                using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cn.Open();
                            string sql = "Delete From HoliDayOptMast where tYear = '" + txtYear.Text.Trim() + "' "
                            + " and tDate ='" + txtDate.DateTime.ToString("yyyy-MM-dd") + "' "
                            + " And PublicHLTyp ='OH'";
                            cmd.CommandText = sql;
                            cmd.Connection = cn;
                            cmd.ExecuteNonQuery();
                           
                            
                            MessageBox.Show("Record Deleted...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetCtrl();
                            LoadGrid();
                            SetRights();
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

           // MessageBox.Show("Not Implemented...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if ( txtYear.Text.Trim() == "" )
            {
                grid.DataSource = null;
                return;
            }


            DataSet ds = new DataSet();
            string sql = "select tYear,tDate,PublicHLTyp,HLDesc " +
                    "  from HoliDayOptMast "
                    + " where tYear ='" + txtYear.Text.Trim() + "' Order By tYear,tDate";

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
                
                txtYear.Text = gridView1.GetRowCellValue(info.RowHandle, "tYear").ToString();
                txtDate.DateTime = (DateTime)gridView1.GetRowCellValue(info.RowHandle, "tDate");
                txtDescription.Text = gridView1.GetRowCellValue(info.RowHandle, "HLDesc").ToString();

                object o = new object();
                EventArgs e = new EventArgs();                
                mode = "OLD";
                oldCode = txtDate.DateTime.ToString("yyyy-MM-dd");
                txtDate.Enabled = false;
                txtYear.Enabled = false;
            }

            
        }

        

        private void txtYear_Validated(object sender, EventArgs e)
        {
            if (txtYear.Text == "")
            {
                grid.DataSource = null;

                return;
            }
            LoadGrid();
        }

        private void txtDate_Validated(object sender, EventArgs e)
        {
            if (txtDate.DateTime.Year != Convert.ToInt32(txtYear.Text.Trim()))
            {
                txtDate.EditValue = null;
            }
        }

        private void txtDate_EditValueChanged(object sender, EventArgs e)
        {
            if (txtDate.EditValue == null || txtYear.Text.Trim() == "")
            {
                mode = "NEW";
                return;
            }

            DataSet ds = new DataSet();
            string sql = "select * From  HoliDayOptMast where tYear = '" + txtYear.Text.Trim() + "' "
                + " And tDate ='" + txtDate.DateTime.ToString("yyyy-MM-dd") + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    
                    txtYear.Text = dr["tYear"].ToString();
                    txtDate.EditValue = dr["tDate"];
                    txtDescription.Text = dr["HLDesc"].ToString();

                    mode = "OLD";
                    oldCode = txtDate.DateTime.ToString("yyyy-MM-dd");
                    txtYear.Enabled = false;
                    txtDate.Enabled = false;
                }
            }
            else
            {
                mode = "NEW";
                oldCode = "";
                txtYear.Enabled = true;
                txtDate.Enabled = true;
            }


            SetRights();
        }

        private void frmMastHolidayOpt_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter))
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        


    }
}
