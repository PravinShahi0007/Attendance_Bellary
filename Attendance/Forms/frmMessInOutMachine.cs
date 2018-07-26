using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance.Forms
{
    public partial class frmMessInOutMachine : Form
    {
        private string mode = "NEW";
        private string GRights = "XXXV";
        private DataSet dsMess = new DataSet();


        public frmMessInOutMachine()
        {
            InitializeComponent();
        }

        private void frmMessInOutMachine_Load(object sender, EventArgs e)
        {
            GRights = Attendance.Classes.Globals.GetFormRights(this.Name);
            ResetCtrl();
           
        }

        private void LoadGrid()
        {
            dsMess = Utils.Helper.GetData("Select * From LunchMachine where 1=1 Order by Location", Utils.Helper.constr);
            
            bool hasRows = dsMess.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                grd_avbl.DataSource = dsMess;
                grd_avbl.DataMember = dsMess.Tables[0].TableName;
                grd_avbl.Refresh();
            }
            else
            {
                grd_avbl.DataSource = null;
                grd_avbl.Refresh();
            }


        }

        private void ResetCtrl()
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            txtLunchMachine.Enabled = true;
            txtOutMachine.Enabled = true;
            txtInMachine.Enabled = true;


            txtLunchInTime.EditValue = null;
            txtLunchOutTime.EditValue = null;
            txtDinnerInTime.EditValue = null;
            txtDinnerOutTime.EditValue = null;

            txtLocation.Text = "";
            
            txtLunchMachine.Text = "";
            txtOutMachine.Text = "";
            txtInMachine.Text = "";
                        
            mode = "NEW";

            LoadGrid();
            SetRights();
        }

        private void SetRights()
        {
            if ( txtLunchMachine.Text.Trim() != "" && mode == "NEW" && GRights.Contains("A"))
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            else if (txtLunchMachine.Text.Trim() != "" && mode == "OLD")
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
        }

        private string DataValidate()
        {
            string err = string.Empty;

            if (string.IsNullOrEmpty(txtLunchMachine.Text))
            {
                err = err + "Please Enter Lunch Machine " + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtOutMachine.Text))
            {
                err = err + "Please Enter Out Machine..." + Environment.NewLine;
            }


            if (string.IsNullOrEmpty(txtInMachine.Text))
            {
                err = err + "Please Enter In Machine " + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtLocation.Text))
            {
                err = err + "Please Enter Location" + Environment.NewLine;
            }

            if (txtLunchOutTime.EditValue == null)
            {
                err = err + "Please Enter Lunch Out Time..." + Environment.NewLine;
                return err;
            }

            if (txtLunchInTime.EditValue == null)
            {
                err = err + "Please Enter Lunch In Time..." + Environment.NewLine;
                return err;
            }

            if (txtDinnerOutTime.EditValue == null)
            {
                err = err + "Please Enter Dinner Out Time..." + Environment.NewLine;
                return err;
            }

            if (txtDinnerInTime.EditValue == null)
            {
                err = err + "Please Enter Dinner In Time..." + Environment.NewLine;
                return err;
            }

            TimeSpan LunchOut,LunchIn,DinnerOut,DinnerIn;
            try
            {
                TimeSpan.TryParse(txtLunchOutTime.Time.ToString("HH:mm"), out LunchOut);
                TimeSpan.TryParse(txtLunchInTime.Time.ToString("HH:mm"), out LunchIn);
                TimeSpan.TryParse(txtDinnerOutTime.Time.ToString("HH:mm"), out DinnerOut);
                TimeSpan.TryParse(txtDinnerInTime.Time.ToString("HH:mm"), out DinnerIn);

            }
            catch (Exception ex)
            {
                err = err + ex.ToString() + Environment.NewLine;
                return err;
            }

            if (LunchIn <= LunchOut)
            {
                err = err + "Invalid Lunch Out/In Range..." + Environment.NewLine;
            }

            if (DinnerIn <= DinnerOut)
            {
                err = err + "Invalid Dinner Out/In Range..." + Environment.NewLine;
            }


            return err;
        }

        private void grd_avbl_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void DoRowDoubleClick(GridView view, Point pt)
        {
            GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                txtLunchMachine.Text = gv_avbl.GetRowCellValue(info.RowHandle, "LunchMachine").ToString();
                txtOutMachine.Text = gv_avbl.GetRowCellValue(info.RowHandle, "OutMachine").ToString();
                txtInMachine.Text = gv_avbl.GetRowCellValue(info.RowHandle, "InMachine").ToString();
                txtLocation.Text = gv_avbl.GetRowCellValue(info.RowHandle, "Location").ToString();

                txtLunchOutTime.EditValue = gv_avbl.GetRowCellValue(info.RowHandle, "LunchOutTime").ToString();
                txtLunchInTime.EditValue = gv_avbl.GetRowCellValue(info.RowHandle, "LunchInTime").ToString();

                txtDinnerOutTime.EditValue = gv_avbl.GetRowCellValue(info.RowHandle, "DinnerOutTime").ToString();
                txtDinnerInTime.EditValue = gv_avbl.GetRowCellValue(info.RowHandle, "DinnerInTime").ToString();


                mode = "OLD";
                
            }


        }

        private void txtLunchMachine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select MachineIP,MachineDesc From ReaderConfig Where Active = 1 and CanteenFlg = 1 and IOFLG = 'B' and LunchInOut = 0 ";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "MachineIP", "MachineIP", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else
                {
                    obj = (List<string>)hlp.Show(sql, "MachineDesc", "MachineDesc", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtLunchMachine.Text = obj.ElementAt(0).ToString();

                }
            }
        }

        private void txtOutMachine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select MachineIP,MachineDesc From ReaderConfig Where Active = 1 and CanteenFlg = 0 and IOFLG = 'O' and LunchInOut = 1 ";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "MachineIP", "MachineIP", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else
                {
                    obj = (List<string>)hlp.Show(sql, "MachineDesc", "MachineDesc", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtOutMachine.Text = obj.ElementAt(0).ToString();

                }
            }
        }

        private void txtInMachine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select MachineIP,MachineDesc From ReaderConfig Where Active = 1 and CanteenFlg = 0 and IOFLG = 'I' and LunchInOut = 1 ";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "MachineIP", "MachineIP", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else
                {
                    obj = (List<string>)hlp.Show(sql, "MachineDesc", "MachineDesc", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtInMachine.Text = obj.ElementAt(0).ToString();

                }
            }
        }

        private void ValidateAllMachine()
        {
            mode = "NEW";
            if (txtLunchMachine.Text.Trim() == "")
                return;

            if (txtOutMachine.Text.Trim() == "")
                return;

            if (txtInMachine.Text.Trim() == "")
                return;


            string sql = "Select * From LunchMachine Where LunchMachine ='" + txtLunchMachine.Text.Trim() + "' And " +
                " OutMachine = '" + txtOutMachine.Text.Trim() + "' And " +
                " InMachine ='" + txtInMachine.Text.Trim() + "'";

            DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);

            bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                mode = "OLD";
                txtLunchMachine.Enabled = false;
                txtOutMachine.Enabled = false;
                txtInMachine.Enabled = false;


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtLunchMachine.Text = dr["LunchMachine"].ToString();
                    txtOutMachine.Text = dr["OutMachine"].ToString();
                    txtInMachine.Text = dr["InMachine"].ToString();
                    txtLocation.Text = dr["Location"].ToString();

                    txtLunchOutTime.EditValue = dr["LunchOutTime"].ToString();
                    txtLunchInTime.EditValue = dr["LunchInTime"].ToString();

                    txtDinnerOutTime.EditValue = dr["DinnerOutTime"].ToString();
                    txtDinnerInTime.EditValue = dr["DinnerInTime"].ToString();
                }
            }
            else
            {
                mode = "NEW";
                txtLunchMachine.Enabled = true;
                txtOutMachine.Enabled = true;
                txtInMachine.Enabled = true;
            }

            SetRights();
            
        }

        private void txtInMachine_EditValueChanged(object sender, EventArgs e)
        {
            ValidateAllMachine();
        }

        private void txtOutMachine_EditValueChanged(object sender, EventArgs e)
        {
            ValidateAllMachine();
        }

        private void txtLunchMachine_EditValueChanged(object sender, EventArgs e)
        {
            ValidateAllMachine();
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
                        string sql = "insert into LunchMachine (LunchMachine,OutMachine,InMachine,LunchInTime,LunchOutTime,DinnerInTime,DinnerOutTime,Location) Values (" +
                            " '" + txtLunchMachine.Text.Trim() + "','" + txtOutMachine.Text.Trim() + "','" + txtInMachine.Text.Trim() + "', " +
                            " '" + txtLunchInTime.Time.ToString("HH:mm") + "','" + txtLunchOutTime.Time.ToString("HH:mm") + "', " +
                            " '" + txtDinnerInTime.Time.ToString("HH:mm") + "','" + txtDinnerOutTime.Time.ToString("HH:mm") + "'," +
                            " '" + txtLocation.Text.Trim() + "')";


                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record Inserted...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCtrl();

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
                        string sql = "Update LunchMachine Set LunchInTime ='" + txtLunchInTime.Time.ToString("HH:mm") + "'," +
                            " LunchOutTime = '" + txtLunchOutTime.Time.ToString("HH:mm") + "', DinnerOutTime ='" + txtDinnerOutTime.Time.ToString("HH:mm") + "', " +
                            " DinnerInTime = '" + txtDinnerInTime.Time.ToString("HH:mm") + "', Location ='" + txtLocation.Text.Trim() + "' " +
                            " Where LunchMachine ='" + txtLunchMachine.Text.Trim() + "' " +
                            " And OutMachine = '" + txtOutMachine.Text.Trim() + "' And InMachine ='" + txtInMachine.Text.Trim() + "' ";

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record Updated...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCtrl();

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

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        string sql = "Delete From LunchMachine Where LunchMachine ='" + txtLunchMachine.Text.Trim() + "' " +
                            " And OutMachine = '" + txtOutMachine.Text.Trim() + "' And InMachine ='" + txtInMachine.Text.Trim() + "' ";

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record Deleted...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCtrl();

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
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gv_avbl_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            DoRowDoubleClick(view, pt);
        }

    }
}
