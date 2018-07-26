using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Columns;
using Attendance.Classes;

namespace Attendance.Forms
{
    public partial class frmEmployeeImport : DevExpress.XtraEditors.XtraForm
    {
        public string GRights = "XXXV";

        DataTable dt = new DataTable();

        public frmEmployeeImport()
        {
            InitializeComponent();
            
        }

        
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openKeywordsFileDialog = new OpenFileDialog();
            openKeywordsFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openKeywordsFileDialog.Multiselect = false;
            openKeywordsFileDialog.ValidateNames = true;
            //openKeywordsFileDialog.CheckFileExists = true;
            openKeywordsFileDialog.DereferenceLinks = false;        //Will return .lnk in shortcuts.
            openKeywordsFileDialog.Filter = "Files|*.xls;*.xlsx;*.xlsb";
            openKeywordsFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(OpenKeywordsFileDialog_FileOk);
            var dialogResult = openKeywordsFileDialog.ShowDialog();

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                //first check if already exits if found return..
                string filenm = openKeywordsFileDialog.FileName.ToString();
                if (string.IsNullOrEmpty(filenm))
                    return;
                try
                {
                    txtBrowse.Text = openKeywordsFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    txtBrowse.Text = "";
                }
            }
            else
            {
                txtBrowse.Text = "";
            }
        }

        void OpenKeywordsFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OpenFileDialog fileDialog = sender as OpenFileDialog;
            string selectedFile = fileDialog.FileName;
            if (string.IsNullOrEmpty(selectedFile) || selectedFile.Contains(".lnk"))
            {
                MessageBox.Show("Please select a valid File");
                e.Cancel = true;
            }
            return;
        }

        private string DataValidate(string tEmpUnqID,string tCostCode,DateTime tValidFrom)
        {
            string err = string.Empty;
            clsEmp t = new clsEmp();
            t.CompCode = "01";
            t.EmpUnqID = tEmpUnqID;
            if (!t.GetEmpDetails(t.CompCode,t.EmpUnqID))
            {
                err = err + "Invalid/InActive EmpUnqID..." + Environment.NewLine;
            }

            t.CostCode = tCostCode;
            t.GetCostDesc(tCostCode);
            if (string.IsNullOrEmpty(t.CostDesc))
            {
                err = err + "Invalid CostCode..." + Environment.NewLine;
            }

            

            if (tValidFrom == DateTime.MinValue)
            {
                err = err + "Please Enter Valid From Date..." + Environment.NewLine;
                return err;
            }


            string sql = "Select max(ValidFrom) From MastCostCodeEmp where EmpUnqId = '" + t.EmpUnqID + "' " +
                " and ValidFrom > '" + tValidFrom.ToString("yyyy-MM-dd") + "'";

            string tMaxDt = Utils.Helper.GetDescription(sql, Utils.Helper.constr);

            if (!string.IsNullOrEmpty(tMaxDt))
            {
                err = err + "System Does not Allow to insert/update/delete between...." + Environment.NewLine;
            }


            return err;
        }


        private void btnImport_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;

            DataTable dtMaterial = new DataTable();
            DataTable sortedDT = new DataTable();
            try
            {
                

                foreach (GridColumn column in grd_view1.VisibleColumns)
                {
                    if (column.FieldName != string.Empty)
                        dtMaterial.Columns.Add(column.FieldName, column.ColumnType);
                }


                for (int i = 0; i < grd_view1.DataRowCount; i++)
                {
                    DataRow row = dtMaterial.NewRow();

                    foreach (GridColumn column in grd_view1.VisibleColumns)
                    {
                        row[column.FieldName] = grd_view1.GetRowCellValue(i, column);
                    }
                    dtMaterial.Rows.Add(row);
                }

                DataView dv = dtMaterial.DefaultView;
                dv.Sort = "EmpUnqID asc";
                sortedDT = dv.ToTable();

                using (SqlConnection con = new SqlConnection(Utils.Helper.constr))
                {   
                    con.Open();
                    foreach (DataRow tdr in sortedDT.Rows)
                    {
                        
                        
                        string tEmpUnqID = tdr["EmpUnqID"].ToString();
                        if (string.IsNullOrEmpty(tEmpUnqID))
                        {
                            tdr["Remarks"] = "EmpUnqID is blank...";
                            continue;
                        }
                            
                        
                        string tWrkGrp = tdr["WrkGrp"].ToString();
                        string tUnitCode = tdr["UnitCode"].ToString();
                        string tEmpName = tdr["EmpName"].ToString();
                        string tFatherName = tdr["FatherName"].ToString();
                        
                        bool tSex = (tdr["Gender"].ToString() == "M" ? true : false);
                        bool tActive = (tdr["Active"].ToString() == "Y" ? true : false);
                        bool tPayrollFLG = (tdr["PayrollFlg"].ToString().Trim() == "Y"?true:false);
                        bool tContractFlg = (tdr["ContractFlg"].ToString().Trim() == "Y"?true:false);
                        bool tShiftType = (tdr["AutoShift"].ToString().Trim() == "Y"?true:false);
                        bool tOTFLG = (tdr["OTFLG"].ToString().Trim() == "Y"?true:false);
                        

                        string tEmpCode = tdr["EmpCode"].ToString().Trim().ToUpper();
                        string tContCode = tdr["ContCode"].ToString().Trim().ToUpper();
                        string tWeekoff = tdr["WeekOff"].ToString().Trim().ToUpper();
                        string tEmpTypeCode = tdr["EmpTypeCode"].ToString().Trim();
                        string tCATCODE = tdr["CatCode"].ToString().Trim().ToUpper();
                        string tDeptcode = tdr["DeptCode"].ToString().Trim().ToUpper();
                        string tStatCode = tdr["StatCode"].ToString().Trim().ToUpper();
                        string tDesgCode = tdr["DesgCode"].ToString().Trim().ToUpper();
                        string tGradeCode = tdr["GradCode"].ToString().Trim().ToUpper();
                        string tMessGrpCode = tdr["MessGrpCode"].ToString().Trim().ToUpper();
                        string tMessCode = tdr["MessCode"].ToString().Trim().ToUpper();
                        string tOldEmpCode = tdr["OldEmpCode"].ToString().Trim().ToUpper();
                        string tSAPID = tdr["SAPID"].ToString().Trim().ToUpper();
                        string tCostCode = tdr["CostCode"].ToString().Trim().ToUpper();
                        string tAdharNo = tdr["AdharNo"].ToString().Trim().ToUpper();
                        string tShiftCode = tdr["ShiftCode"].ToString().Trim().ToUpper();

                        double tbasic = 0;
                        try
                        {
                            double.TryParse(tdr["Basic"].ToString(), out tbasic);
                        }
                        catch (Exception ex)
                        {

                        }

                        DateTime? tValidFrom = new DateTime?();
                        DateTime? tValidTo = new DateTime?();
                        
                        try
                        {
                            tValidFrom = Convert.ToDateTime(tdr["ValidFrom"]);
                            tValidTo = Convert.ToDateTime(tdr["ValidTo"]);
                        }
                        catch (Exception ex)
                        {

                        }
                                                
                        DateTime tBirthDt = Convert.ToDateTime(tdr["BirthDt"]);
                        DateTime tJoinDt = Convert.ToDateTime(tdr["JoinDt"]);

                        clsEmp emp = new clsEmp();
                        string err = string.Empty;

                        bool iscreated = emp.CreateEmployee(tEmpUnqID, tWrkGrp,
                            tUnitCode, tEmpName, tFatherName,
                            tSex, tActive, tBirthDt, tJoinDt,
                            tWeekoff, tPayrollFLG, tContractFlg,
                            tShiftType, tOTFLG, false,
                            false, tEmpCode, tContCode,
                                tEmpTypeCode, tCATCODE, tDeptcode, tStatCode,
                                    tDesgCode, tGradeCode, tMessGrpCode, tMessCode,
                                        tOldEmpCode, tSAPID, tCostCode, tAdharNo,
                                            tValidFrom, tValidTo,tbasic, out err);

                        
                        if (string.IsNullOrEmpty(err))
                            tdr["Remarks"] = "Employee Created...";
                        else
                            tdr["Remarks"] = err;
                        
                    }

                    con.Close();
                }

                Cursor.Current = Cursors.Default;
                MessageBox.Show("file uploaded Successfully, please check the remarks for indivisual record status...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            DataSet ds = new DataSet();
            ds.Tables.Add(sortedDT);
            grd_view.DataSource = ds;
            grd_view.DataMember = ds.Tables[0].TableName;
            grd_view.Refresh();

            Cursor.Current = Cursors.Default;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBrowse.Text.Trim().ToString()))
            {
                MessageBox.Show("Please Select Excel File First...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnBrowse.Enabled = false;

            if (GRights.Contains("A") || GRights.Contains("U") || GRights.Contains("D"))
            {
                btnImport.Enabled = true;
            }
            else
            {
                btnImport.Enabled = false;
            }

            

            Cursor.Current = Cursors.WaitCursor;
            grd_view.DataSource = null;
            string filePath = txtBrowse.Text.ToString();

            string sexcelconnectionstring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"";
            //string sexcelconnectionstring = @"provider=microsoft.jet.oledb.4.0;data source=" + filePath + ";extended properties=" + "\"excel 8.0;hdr=yes;IMEX=1;\"";

            
            OleDbConnection oledbconn = new OleDbConnection(sexcelconnectionstring);
            List<SheetName> sheets = ExcelHelper.GetSheetNames(oledbconn);          
            string sheetname = "[" + sheets[0].sheetName.Replace("'", "") + "]";

            try
            {
                oledbconn.Open();
            }
            catch(Exception ex){
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            try
            {
                string myexceldataquery = "select EmpUnqID,WrkGrp,UnitCode,EmpName,FatherName,Gender,Active," +
                    " BirthDt,JoinDt,WeekOff,PayrollFlg,ContractFlg,AutoShift,OTFLG,ShiftCode," +
                    " EmpCode,ContCode,EmpTypeCode,CatCode,DeptCode,StatCode,DesgCode,GradCode,MessGrpCode,MessCode," +
                    " OldEmpCode,SapID,CostCode,AdharNo,ValidFrom,ValidTo,Basic, '' as Remarks from " + sheetname;

                OleDbDataAdapter oledbda = new OleDbDataAdapter(myexceldataquery, oledbconn);
                dt = new DataTable();
                oledbda.Fill(dt);
                dt.AcceptChanges();
                foreach (DataRow row in dt.Rows)
                {
                    if (string.IsNullOrEmpty(row["EmpUnqID"].ToString().Trim()))
                        row.Delete();
                }
                dt.AcceptChanges();

                oledbconn.Close();
                
            }
            catch (Exception ex)
            {
                oledbconn.Close();
                MessageBox.Show("Please Check upload template..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor.Current = Cursors.Default;
                btnImport.Enabled = false;
                grd_view.DataSource = null;
                return;
            }

            

            DataView dv = dt.DefaultView;
            dv.Sort = "EmpUnqID asc";
            DataTable sortedDT = dv.ToTable();


            grd_view.DataSource = sortedDT;

            if (GRights.Contains("A") || GRights.Contains("U") || GRights.Contains("D"))
            {
                btnImport.Enabled = true;
            }
            else
            {
                btnImport.Enabled = false;
            }

            Cursor.Current = Cursors.Default;
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
                            grd_view.ExportToXls(exportFilePath);
                            break;
                        case ".xlsx":
                            grd_view.ExportToXlsx(exportFilePath);
                            break;
                        case ".rtf":
                            grd_view.ExportToRtf(exportFilePath);
                            break;
                        case ".pdf":
                            grd_view.ExportToPdf(exportFilePath);
                            break;
                        case ".html":
                            grd_view.ExportToHtml(exportFilePath);
                            break;
                        case ".mht":
                            grd_view.ExportToMht(exportFilePath);
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

        private void frmEmployeeImport_Load(object sender, EventArgs e)
        {
            GRights = Attendance.Classes.Globals.GetFormRights(this.Name);
            grd_view.DataSource = null;
            btnImport.Enabled = false;
        }
    }
}