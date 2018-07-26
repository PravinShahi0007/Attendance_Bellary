using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Attendance.Classes;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using DevExpress.XtraPrinting;

namespace Attendance.Forms
{
    public partial class frmBulkWOChange : Form
    {
        DataTable SelDt = new DataTable();
        public string GType = string.Empty;
        clsProcess pro = new clsProcess();
        public string GRights = "XXXV";
        


        public frmBulkWOChange()
        {
            InitializeComponent();
        }
        
        private void txtWrkGrpCode_KeyDown(object sender, KeyEventArgs e)
        {
            

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select WrkGrp,WrkGrpDesc From MastWorkGrp Where CompCode ='01'";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "WrkGrp", "WrkGrp", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtWrkGrpCode.Text = obj.ElementAt(0).ToString();
                    txtWrkGrpDesc.Text = obj.ElementAt(1).ToString();


                }
            }
        }

        private void txtWrkGrpCode_Validated(object sender, EventArgs e)
        {
            if (txtWrkGrpCode.Text.Trim() == "")
            {
                txtWrkGrpCode.Text = "";
                txtWrkGrpDesc.Text = "";
                txtUnitCode.Text = "";
                txtUnitDesc.Text = "";
                txtDeptDesc.Text = "";
                txtDeptCode.Text = "";
                txtStatDesc.Text = "";
                txtStatCode.Text = "";
                LoadGrid();
                return;
            }

            DataSet ds = new DataSet();
            string sql = "select * From MastWorkGrp where CompCode ='01' and WrkGrp='" + txtWrkGrpCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                   
                    txtWrkGrpCode.Text = dr["WrkGrp"].ToString();
                    txtWrkGrpDesc.Text = dr["WrkGrpDesc"].ToString();
                   
                }
            }
            LoadGrid();

        }
        
        private void txtUnitCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtWrkGrpCode.Text.Trim() == "")
            {

                txtWrkGrpCode.Text = "";
                txtWrkGrpDesc.Text = "";
                txtUnitCode.Text = "";
                txtUnitDesc.Text = "";
                txtDeptCode.Text = "";
                txtDeptDesc.Text = "";
                txtStatCode.Text = "";
                txtStatDesc.Text = "";
               
                return;
            }

            
               

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select UnitCode,UnitName From MastUnit Where CompCode ='01' and WrkGrp = '" + txtWrkGrpCode.Text.Trim() + "' ";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "UnitCode", "UnitCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtUnitCode.Text = obj.ElementAt(0).ToString();
                    txtUnitDesc.Text = obj.ElementAt(1).ToString();

                }
            }
        }

        private void txtUnitCode_Validated(object sender, EventArgs e)
        {
            if ( txtWrkGrpCode.Text.Trim() == "" || txtWrkGrpDesc.Text.Trim() == ""
                 
                )
            {

                txtWrkGrpCode.Text = "";
                txtWrkGrpDesc.Text = "";
                txtUnitCode.Text = "";
                txtUnitDesc.Text = "";
                txtDeptCode.Text = "";
                txtDeptDesc.Text = "";
                txtStatCode.Text = "";
                txtStatDesc.Text = "";
                LoadGrid();
                return;
            }

            if (txtUnitCode.Text.Trim() == "")
            {
                txtUnitCode.Text = "";
                txtUnitDesc.Text = "";
                txtDeptCode.Text = "";
                txtDeptDesc.Text = "";
                txtStatCode.Text = "";
                txtStatDesc.Text = "";
                LoadGrid();
                return;
            }


            txtUnitCode.Text = txtUnitCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastUnit where CompCode ='01' " +
                    " and WrkGrp='" + txtWrkGrpCode.Text.Trim() + "' and UnitCode ='" + txtUnitCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    
                    txtWrkGrpCode.Text = dr["WrkGrp"].ToString();
                    txtUnitCode.Text = dr["UnitCode"].ToString();
                    txtUnitDesc.Text = dr["UnitName"].ToString();
                    txtWrkGrpCode_Validated(sender, e);

                }
            }
            LoadGrid();
        }

        private void txtDeptCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtWrkGrpCode.Text.Trim() == "" || txtUnitCode.Text.Trim() == "" )
            {
                txtWrkGrpCode.Text = "";
                txtWrkGrpDesc.Text = "";
                txtUnitCode.Text = "";
                txtUnitDesc.Text = "";
                txtDeptCode.Text = "";
                txtDeptDesc.Text = "";
                txtStatCode.Text = "";
                txtStatDesc.Text = "";
                
                return;
            }
                

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";

                sql = "Select DeptCode,DeptDesc From MastDept Where CompCode ='01' " +
                    " and WrkGrp = '" + txtWrkGrpCode.Text.Trim() + "' and UnitCode ='" + txtUnitCode.Text.Trim() + "'";

                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "DeptCode", "DeptCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else
                {

                    obj = (List<string>)hlp.Show(sql, "DeptDesc", "DeptDesc", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtDeptCode.Text = obj.ElementAt(0).ToString();
                    txtDeptDesc.Text = obj.ElementAt(1).ToString();

                }
            }
        }

        private void txtDeptCode_Validated(object sender, EventArgs e)
        {
            if (txtWrkGrpCode.Text.Trim() == ""  )
            {

                txtWrkGrpCode.Text = "";
                txtWrkGrpDesc.Text = "";
                txtUnitCode.Text = "";
                txtUnitDesc.Text = "";
                txtDeptCode.Text = "";
                txtDeptDesc.Text = "";
                txtStatCode.Text = "";
                txtStatDesc.Text = "";
                LoadGrid();
                return;
            }

            if (txtUnitCode.Text.Trim() == "")
            {
                txtUnitCode.Text = "";
                txtUnitDesc.Text = "";
                txtDeptCode.Text = "";
                txtDeptDesc.Text = "";
                txtStatCode.Text = "";
                txtStatDesc.Text = "";
                LoadGrid();
                return;
            }

            if (txtDeptCode.Text.Trim() == "")
            {
                txtDeptCode.Text = "";
                txtDeptDesc.Text = "";
                txtStatCode.Text = "";
                txtStatDesc.Text = "";
                LoadGrid();
                return;
            }

            txtDeptCode.Text = txtDeptCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastDept where CompCode ='01' " +
                    " and WrkGrp='" + txtWrkGrpCode.Text.Trim() + "' " +
                    " and UnitCode ='" + txtUnitCode.Text.Trim() + "' " +
                    " and DeptCode ='" + txtDeptCode.Text.Trim() + "' ";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    
                    txtWrkGrpCode.Text = dr["WrkGrp"].ToString();
                    txtUnitCode.Text = dr["UnitCode"].ToString();
                    txtDeptCode.Text = dr["DeptCode"].ToString();
                    txtDeptDesc.Text = dr["DeptDesc"].ToString();
                }
            }

            LoadGrid();
        }

        private void txtStatCode_KeyDown(object sender, KeyEventArgs e)
        {
            if ( txtWrkGrpCode.Text.Trim() == ""
                || txtUnitCode.Text.Trim() == "" || txtDeptCode.Text.Trim() == "")
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";

                sql = "Select StatCode,StatDesc From MastStat Where CompCode ='01' " +
                   " and WrkGrp = '" + txtWrkGrpCode.Text.Trim() + "' and UnitCode ='" + txtUnitCode.Text.Trim() + "' " +
                   " and DeptCode='" + txtDeptCode.Text.Trim() + "'";

                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "StatCode", "StatCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else
                {
                    obj = (List<string>)hlp.Show(sql, "StatDesc", "StatDesc", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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
                    txtStatCode.Text = obj.ElementAt(0).ToString();
                    txtStatDesc.Text = obj.ElementAt(1).ToString();
                }
            }
        }

        private void txtStatCode_Validated(object sender, EventArgs e)
        {
            if ( txtWrkGrpCode.Text.Trim() == "" )
            {
                txtWrkGrpCode.Text = "";
                txtWrkGrpDesc.Text = "";
                txtUnitCode.Text = "";
                txtUnitDesc.Text = "";
                txtDeptCode.Text = "";
                txtDeptDesc.Text = "";
                txtStatCode.Text = "";
                txtStatDesc.Text = "";
                LoadGrid();
                return;
            }

            

            if (txtUnitCode.Text.Trim() == "")
            {
                txtUnitCode.Text = "";
                txtUnitDesc.Text = "";
                txtDeptCode.Text = "";
                txtDeptDesc.Text = "";
                txtStatCode.Text = "";
                txtStatDesc.Text = "";
                LoadGrid();
                return;
            }

            if (txtDeptCode.Text.Trim() == "")
            {
                txtDeptCode.Text = "";
                txtDeptDesc.Text = "";
                txtStatCode.Text = "";
                txtStatDesc.Text = "";
                LoadGrid();
                return;
            }


            if (txtStatCode.Text.Trim() == "")
            {
                txtStatCode.Text = "";
                txtStatDesc.Text = "";
                LoadGrid();
                return;
            }


            txtStatCode.Text = txtStatCode.Text.Trim().ToString().PadLeft(3, '0');

            DataSet ds = new DataSet();
            string sql = "select * From MastStat where CompCode ='01' " +
                    " and WrkGrp='" + txtWrkGrpCode.Text.Trim() + "' " +
                    " and UnitCode ='" + txtUnitCode.Text.Trim() + "' " +
                    " and DeptCode ='" + txtDeptCode.Text.Trim() + "' " +
                    " and StatCode ='" + txtStatCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    
                    txtWrkGrpCode.Text = dr["WrkGrp"].ToString();
                    txtUnitCode.Text = dr["UnitCode"].ToString();
                    txtDeptCode.Text = dr["DeptCode"].ToString();
                    txtStatCode.Text = dr["StatCode"].ToString();
                    txtStatDesc.Text = dr["StatDesc"].ToString();
                    

                }
            }
            else
            {
                txtStatCode.Text = "";
                txtStatDesc.Text = "";
            }

            LoadGrid();
            
        }

        private void LoadGrid()
        {

            if (txtWrkGrpCode.Text.Trim() == "")
                return;
            
            string sql = "Select CONVERT(BIT,0) as SEL, EmpUnqID,EmpName From MastEmp Where CompCode = '01' And ";

            if (!string.IsNullOrEmpty(txtWrkGrpCode.Text.Trim()))
            {
                sql += " WrkGrp ='" + txtWrkGrpCode.Text.Trim().ToString() + "' And ";
            }

            if (!string.IsNullOrEmpty(txtUnitCode.Text.Trim()))
            {
                sql += " UnitCode ='" + txtUnitCode.Text.Trim().ToString() + "' And ";
            }

            if (!string.IsNullOrEmpty(txtDeptCode.Text.Trim()))
            {
                sql += " DeptCode ='" + txtDeptCode.Text.Trim().ToString() + "' And ";
            }

            if (!string.IsNullOrEmpty(txtStatCode.Text.Trim()))
            {
                sql += " StatCode ='" + txtStatCode.Text.Trim().ToString() + "' And ";
            }

            sql += " Active = 1";

            DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            grd_avbl.DataSource = ds;
            grd_avbl.DataMember = ds.Tables[0].TableName;
            gv_avbl.RefreshData();

        }

        private void frmBulkWOChange_Load(object sender, EventArgs e)
        {
            if (GType == "WO")
            {
                lblDesc.Text = "Wo Days:";
                cmbList.Properties.Items.Clear();
                cmbList.Properties.Items.Add("SUN");
                cmbList.Properties.Items.Add("MON");
                cmbList.Properties.Items.Add("TUE");
                cmbList.Properties.Items.Add("WED");
                cmbList.Properties.Items.Add("THU");
                cmbList.Properties.Items.Add("FRI");
                cmbList.Properties.Items.Add("SAT");

                GRights = Attendance.Classes.Globals.GetFormRights("frmBulkWOChange");
            }
            else
            {
                lblDesc.Text = "Shift :";
                cmbList.Properties.Items.Clear();
                string[] shifts = Globals.G_ShiftList.Split(',');
                foreach (string t in shifts)
                {
                    cmbList.Properties.Items.Add(t);
                }

                GRights = Attendance.Classes.Globals.GetFormRights("frmBulkShiftChange");
            }

            SelDt.Columns.Add("EmpUnqID", typeof(string));
            SelDt.Columns.Add("EmpName", typeof(string));
            SelDt.Columns.Add("Remarks", typeof(string));


        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            if(gv_avbl.DataRowCount <= 0){
                return;
            }
            string tEmpUnqID = string.Empty, tEmpName = string.Empty;

            for (int i = 0; i <= gv_avbl.DataRowCount - 1; i++)
            {
                tEmpUnqID = string.Empty; tEmpName = string.Empty;
                gv_avbl.SetRowCellValue(i, "SEL", true);
                tEmpUnqID = gv_avbl.GetRowCellValue(i, "EmpUnqID").ToString();
                tEmpName = gv_avbl.GetRowCellValue(i, "EmpName").ToString();
                DataRow dr = SelDt.NewRow();
                dr["EmpUnqID"] = tEmpUnqID;
                dr["EmpName"] = tEmpName;
                SelDt.Rows.Add(dr);
                SelDt.AcceptChanges();

            }
            grd_Sel.DataSource = SelDt;
            grd_Sel.Refresh();
            gv_Sel.RefreshData();
        }

        private void gv_avbl_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var gv = sender as GridView;
            //int rowHandle = gv.GetRowHandle(gv.DataRowCount);

            if (e.RowHandle == -2147483647)
            {
                return;
            }
            if (e.Column.FieldName == "SEL")
            {
                if (Convert.ToBoolean(e.Value) == true)
                {
                    string tEmp = gv.GetRowCellValue(e.RowHandle, "EmpUnqID").ToString();
                    string tEmpName = gv.GetRowCellValue(e.RowHandle, "EmpName").ToString();
                    DataRow dr = SelDt.NewRow();
                    dr["EmpUnqID"] = tEmp;
                    dr["EmpName"] = tEmpName;
                    SelDt.Rows.Add(dr);
                    SelDt.AcceptChanges();
                    grd_Sel.DataSource = SelDt;
                    grd_Sel.Refresh();
                    gv_Sel.RefreshData();
                }
                else
                {
                    string tEmp = gv.GetRowCellValue(e.RowHandle, "EmpUnqID").ToString();
                    var rows = SelDt.Select("EmpUnqID = '" + tEmp + "'");
                    foreach (DataRow r in rows)
                    {
                        r.Delete();
                    }
                    SelDt.AcceptChanges();
                    grd_Sel.DataSource = SelDt;
                    grd_Sel.Refresh();
                    gv_Sel.RefreshData();
                }

            }
        }

        private void gv_avbl_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            

        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            SelDt.Rows.Clear();
            SelDt.AcceptChanges();
            grd_Sel.Refresh();
            gv_Sel.RefreshData();

            for (int i = 0; i <= gv_avbl.RowCount - 1; i++)
            {
                gv_avbl.SetRowCellValue(i, "SEL", false);
            }
        }

        private void txtFromDt_EditValueChanged(object sender, EventArgs e)
        {
            txtToDt.Properties.MinValue = txtFromDt.DateTime;
        }

        private string DataValidate()
        {
            string err = string.Empty;

            if (txtFromDt.DateTime == DateTime.MinValue || txtToDt.DateTime == DateTime.MinValue)
                err += "FromDate/ToDate is required..." + Environment.NewLine;


            if (cmbList.Text == "")
            {
                if(GType == "WO")
                    err += "Please Select Day from available option." + Environment.NewLine;
                else
                    err += "Please Select Shift from available option." + Environment.NewLine;
            }

            if (SelDt.Rows.Count <= 0)
            {
                err += "Please at least one Employee..." + Environment.NewLine;
                
            }

            ////make sure to to check months between dates
            //if (txtFromDt.DateTime.ToString("yyyyMM") != txtToDt.DateTime.ToString("yyyyMM"))
            //{
            //    err += "Cross Month Changes are not allowed";
            //}

            ////block previous month changes
            //int mth = Convert.ToInt32(txtFromDt.DateTime.ToString("yyyyMM"));
            //int curmth = Convert.ToInt32(Utils.Helper.GetDescription("SELECT LEFT(CONVERT(varchar, GetDate(),112),6)",Utils.Helper.constr));
            //if (mth < curmth)
            //{
            //    err += "Previous Month Changes are not allowed";
            //}

            if (!GRights.Contains("AUDV"))
            {
                err += "You are not Authorised...";
            }

            return err;
        }
        
        private void btnSanction_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
           if(!string.IsNullOrEmpty(err))
           {
               MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               return;
           }

            btnSelectAll.Enabled = false;
            btnClearAll.Enabled = false;
            txtWrkGrpCode.Enabled = false;
            txtUnitCode.Enabled = false;
            txtDeptCode.Enabled = false;
            txtStatCode.Enabled = false;
            txtFromDt.Enabled = false;
            txtToDt.Enabled = false;

            string tType = cmbList.Text.Trim();


            Cursor.Current = Cursors.WaitCursor;
            foreach (DataRow dr in SelDt.Rows)
            {
                dr["Remarks"] = "Processing";
                string tEmpUnqID = dr["EmpUnqID"].ToString();
                string status = string.Empty;

                if(GType == "WO")
                {
                    pro.WoChange(tEmpUnqID, txtFromDt.DateTime, txtToDt.DateTime,tType,out status);
                }
                else
                {
                    pro.ShiftChange(tEmpUnqID, txtFromDt.DateTime, txtToDt.DateTime, tType, out status);
                }

                if (string.IsNullOrEmpty(status))
                {
                    dr["Remarks"] = "Posted..";
                }
                else
                {
                    dr["Remarks"] = status;
                }

                dr.AcceptChanges();
            }

            gv_Sel.RefreshData();

            Cursor.Current = Cursors.Default;

            MessageBox.Show("Data Posting is compleated, please export result for check Remarks of indivisual status", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


            btnSelectAll.Enabled = true;
            btnClearAll.Enabled = true;
            txtWrkGrpCode.Enabled = true;
            txtUnitCode.Enabled = true;
            txtDeptCode.Enabled = true;
            txtStatCode.Enabled = true;
            txtFromDt.Enabled = true;
            txtToDt.Enabled = true;

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                if (saveDialog.ShowDialog() != DialogResult.Cancel)
                {
                    string exportFilePath = saveDialog.FileName;
                    string fileExtenstion = new FileInfo(exportFilePath).Extension;

                    switch (fileExtenstion)
                    {
                        case ".xls":
                            gv_Sel.ExportToXls(exportFilePath);
                            break;
                        case ".xlsx":
                            gv_Sel.ExportToXlsx(exportFilePath);
                            break;
                        case ".rtf":
                            gv_Sel.ExportToRtf(exportFilePath);
                            break;
                        case ".pdf":
                            gv_Sel.ExportToPdf(exportFilePath);
                            break;
                        case ".html":
                            gv_Sel.ExportToHtml(exportFilePath);
                            break;
                        case ".mht":
                            gv_Sel.ExportToMht(exportFilePath);
                            break;
                        default:
                            break;
                    }

                    if (File.Exists(exportFilePath))
                    {
                        try
                        {
                            //Try to open the file and let windows decide how to open it.
                            System.Diagnostics.Process.Start(exportFilePath);
                        }
                        catch
                        {
                            String msg = "The file could not be opened." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                            MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        String msg = "The file could not be saved." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                        MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void frmBulkWOChange_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter))
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        
    }
}
