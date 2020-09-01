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
using System.Net.Http;
using Newtonsoft.Json;

namespace Attendance.Forms
{
    public partial class frmBulkLeaveUpload : DevExpress.XtraEditors.XtraForm
    {
        public string GRights = "XXXV";
        private int GFormID = 0;


        DataTable dt = new DataTable();

        public frmBulkLeaveUpload()
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

        private string DataValidate(DataRow tdr)
        {
            string err = string.Empty;
            string sql = "Select Compcode,WrkGrp,EmpUnqID,Active from MastEmp where EmpUnqID='" + tdr["EmpUnqID"].ToString() + "'";
            DataSet ds = Utils.Helper.GetData(sql,Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
            string tWrkGrp = string.Empty;
            string tCompCode = string.Empty;
            string tLeaveType = string.Empty;
            string tUserID = tdr["PostID"].ToString();
            tLeaveType = tdr["LeaveType"].ToString();

            if (string.IsNullOrEmpty(tLeaveType)){
                err += "Leave Type is required..";
            }

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    tWrkGrp = dr["WrkGrp"].ToString();
                    tCompCode = dr["CompCode"].ToString();
                    //check emplyee active status
                    if (!Convert.ToBoolean(dr["Active"]))
                    {
                        err += "InActive Employee.";
                        return err;
                    }
                }

                if (tWrkGrp != tdr["WrkGrp"].ToString())
                {
                    err += "Invalid Supplied WrkGrp.";
                }

                //check leave type
                int tcnt = 0;
                sql = "Select Count(*) from MastLeave where CompCode ='" + tCompCode + "' And WrkGrp ='" + tWrkGrp + "' And LeaveTyp ='" + tLeaveType + "'";
                int.TryParse(Utils.Helper.GetDescription(sql, Utils.Helper.constr),out tcnt);
                if(tcnt <= 0)
                {
                    err += "Invalid Leave Type.";
                }

            }else{
                err += "Employee does not exist.";
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

                int srno = 0;

                using (SqlConnection con = new SqlConnection(Utils.Helper.constr))
                {
                    DateTime fdt;
                    DateTime tdt;

                    con.Open();
                    foreach (DataRow dr in sortedDT.Rows)
                    {
                        string tEmpUnqID = dr["EmpUnqID"].ToString();
                        srno += 1;        
                        


                        try
                        {
                            fdt = Convert.ToDateTime(dr["FromDate"]);
                        }
                        catch (Exception ex)
                        {
                            dr["Remarks"] = "FromDate Conversion failed...";
                            continue; 
                        }

                        try
                        {
                            tdt = Convert.ToDateTime(dr["ToDate"]);
                        }
                        catch (Exception ex)
                        {
                            dr["Remarks"] = "ToDate Conversion failed...";
                            continue;
                        }

                        if (tdt < fdt)
                        {
                            dr["Remarks"] = "Invalid Date Range";
                            continue;
                        }

                        if((tdt - fdt).Days > 0 && !string.IsNullOrEmpty(dr["HalfDayFlg"].ToString().Trim()))
                        {
                            if (dr["HalfDayFlg"].ToString().Trim() == "0")
                                dr["HalfDayFlg"] = 0;
                            else if (dr["HalfDayFlg"].ToString().Trim() == "1")
                                dr["HalfDayFlg"] = 1;
                            else
                                dr["HalfDayFlg"] = 0;

                            if (dr["HalfDayFlg"].ToString() == "1")
                            {
                                dr["Remarks"] = "Multiple HalfDay not allowed";
                                continue;
                            }
                            
                        }

                        if(string.IsNullOrEmpty(dr["HalfDayFlg"].ToString().Trim()))
                        {
                            dr["HalfDayFlg"] = 0;
                        }

                        if(!string.IsNullOrEmpty(dr["HalfDayFlg"].ToString().Trim()))
                        {
                            if(dr["HalfDayFlg"].ToString().Trim() == "0")
                                dr["HalfDayFlg"] = 0;
                            else if (dr["HalfDayFlg"].ToString().Trim() == "1")
                                dr["HalfDayFlg"] = 1;
                            else
                                dr["HalfDayFlg"] = 0;
                        }


                        string err = DataValidate(dr);

                        if (!string.IsNullOrEmpty(err))
                        {
                            dr["Remarks"] = err;
                            continue; 
                        }

                        AttdLeavePost tmpvar = new AttdLeavePost();
                        tmpvar.AppID = srno;                       
                        tmpvar.EmpUnqID = dr["EmpUnqID"].ToString().Trim();                       
                        tmpvar.FromDate = Convert.ToDateTime(dr["FromDate"]);
                        tmpvar.ToDate = Convert.ToDateTime(dr["ToDate"]);                        
                        tmpvar.LeaveTyp = dr["LeaveType"].ToString().Trim().ToUpper();
                        tmpvar.HalfDay = Convert.ToBoolean(dr["HalfDayFlg"]);
                        tmpvar.AttdUser = (string.IsNullOrEmpty(dr["PostID"].ToString()) ? Utils.User.GUserID : dr["PostID"].ToString()).Trim();
                        tmpvar.ERROR = string.Empty;
                        tmpvar.Location = (string.IsNullOrEmpty(dr["Location"].ToString()) ? "IPU" : dr["Location"].ToString()).Trim();
                        tmpvar.Remarks = "";

                        bool outres = false;
                        string tloc = (string.IsNullOrEmpty(dr["Location"].ToString()) ? "IPU" : dr["Location"].ToString()).Trim();

                        AttdLeavePost retObj = AttdPostLeave(tmpvar, tloc, out outres);

                        if(retObj.PostedFlg){
                            dr["Remarks"] = "Leave Posted Sucussfully";
                        }

                        if(!string.IsNullOrEmpty(retObj.ERROR)){
                            dr["Remarks"] = dr["Remarks"].ToString().Trim() + retObj.ERROR;
                        }
                        


                    }//using foreach

                    con.Close();
                }//using connection

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


        private AttdLeavePost AttdPostLeave(AttdLeavePost attdLeaveObj, string location, out bool output)
        {
            string baseuri = Utils.Helper.GetDescription("select AttdWebApiHost From MastNetwork", Utils.Helper.constr);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseuri);

                var content = new StringContent(JsonConvert.SerializeObject(attdLeaveObj),
                    Encoding.UTF8, "application/json");

                var responseTask = client.PostAsync("/api/leavepost", content);
                responseTask.Wait();

                var result = responseTask.Result;
                output = result.IsSuccessStatusCode;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<AttdLeavePost>();
                    readTask.Wait();

                    var attdLeave = readTask.Result;

                    return attdLeave;

                }
                else
                {
                    var readTask = result.Content.ReadAsAsync<AttdLeavePost>();
                    readTask.Wait();

                    var attdLeave = readTask.Result;
                    // Some error was there, return it without changing posting flags


                    return attdLeave;
                }
            }

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            


            try
            {
                string myexceldataquery = "select WrkGrp,EmpUnqID,FromDate,ToDate,LeaveType,HalfDayFlg,PostID,Location,'' as Remarks from " + sheetname;
                OleDbDataAdapter oledbda = new OleDbDataAdapter(myexceldataquery, oledbconn);
                dt.Clear();
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
                MessageBox.Show("Please Check upload template.." + Environment.NewLine + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor.Current = Cursors.Default;
                btnImport.Enabled = false;
                oledbconn.Close();
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

        private void frmBulkLeaveUpload_Load(object sender, EventArgs e)
        {
            GRights = Attendance.Classes.Globals.GetFormRights(this.Name);
            
            //string s = Utils.Helper.GetDescription("Select SanDayLimit from MastBCFlg", Utils.Helper.constr);
            //if(string.IsNullOrEmpty(s)){
            //    rSanDayLimit = 0;
            //    MessageBox.Show("Please Contact to Admin : for some confuguraiton required.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //}else{
            //    rSanDayLimit = Convert.ToInt32(s);

            //    if(rSanDayLimit == 0)
            //    {
            //        MessageBox.Show("Please Contact to Admin : for some confuguraiton required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}

            GFormID = Convert.ToInt32(Utils.Helper.GetDescription("Select FormId from MastFrm Where FormName ='" + this.Name + "'",Utils.Helper.constr));

                
            grd_view.DataSource = null;
            btnImport.Enabled = false;
        }
    }
}