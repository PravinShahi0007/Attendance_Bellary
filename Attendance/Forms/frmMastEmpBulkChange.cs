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
using System.Globalization;

namespace Attendance.Forms
{
    public partial class frmMastEmpBulkChange : DevExpress.XtraEditors.XtraForm
    {
        public string GRights = "XXXV";
       


        DataTable dt = new DataTable();

        public frmMastEmpBulkChange()
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
            clsEmp t = new clsEmp();
            t.CompCode = "01";
            t.EmpUnqID = tdr["EmpUnqID"].ToString();
            if (!t.GetEmpDetails(t.CompCode,t.EmpUnqID))
            {
                err = err + "Invalid/InActive EmpUnqID..." + Environment.NewLine;
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
                    foreach (DataRow dr in sortedDT.Rows)
                    {
                        string tEmpUnqID = dr["EmpUnqID"].ToString();

                        string err = DataValidate(dr);

                        if (!string.IsNullOrEmpty(err))
                        {
                            dr["Remarks"] = err;
                            continue;
                        }

                        #region Chk_AllVals
                        //check all values if all empty skip
                        //if (dr["CatCode"].ToString() == "" && dr["DesgCode"].ToString() == ""
                        //    && dr["GradeCode"].ToString() == "" && dr["Basic"].ToString() == ""
                        //    && dr["SPLALL"].ToString() == "" && dr["BAALL"].ToString() == ""
                        //    && dr["LeftDt"].ToString() == "")
                        //{
                        //    dr["Remarks"] = dr["Remarks"].ToString() + " Nothing to update...";
                        //    continue;
                        //}
                        #endregion
                        string tCatCode = dr["CatCode"].ToString();
                        string tDesgCode = dr["DesgCode"].ToString();
                        string tGradeCode = dr["GradeCode"].ToString();
                        double tSplAll = 0;
                        double tBAAll = 0;
                        DateTime tLeftDt = DateTime.MinValue;
                        double tBasic = 0;




                        try
                        {
                            double.TryParse(dr["Basic"].ToString(), out tBasic);
                        }
                        catch (Exception ex) { }

                        try
                        {
                            double.TryParse(dr["SPLALL"].ToString(), out tSplAll);
                        }
                        catch (Exception ex) { }

                        try
                        {
                            double.TryParse(dr["BAALL"].ToString(), out tBAAll);
                        }
                        catch (Exception ex) { }

                        if (!string.IsNullOrEmpty(dr["LeftDt"].ToString()))
                        {
                            if (!DateTime.TryParseExact(dr["LeftDt"].ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out tLeftDt))
                            {
                                dr["Remarks"] = "Date Conversion failed(yyyy-MM-dd)...";
                                continue;
                            }
                        }


                        #region Final_Update

                        using (SqlCommand cmd = new SqlCommand())
                        {
                            try
                            {

                                cmd.Connection = con;
                                cmd.CommandType = CommandType.Text;

                                string sql = "Update MastEmp SET ";

                                if (!String.IsNullOrEmpty(tCatCode.Trim()))
                                {
                                    sql += " CatCode = '" + tCatCode.Trim() + "', ";
                                }

                                if (!string.IsNullOrEmpty(tDesgCode.Trim()))
                                {
                                    sql += " DesgCode = '" + tDesgCode.Trim() + "', ";
                                }

                                if (!string.IsNullOrEmpty(tGradeCode.Trim()))
                                {
                                    sql += " GradCode = '" + tDesgCode.Trim() + "', ";
                                }


                                if (tBasic > 0)
                                {
                                    sql += "  Basic = '" + tBasic.ToString() + "',";

                                }

                                if(tSplAll > 0)
                                {
                                    sql += " SPLALL = '" + tSplAll.ToString() + "',";
                                }
                               
                                if(tBAAll > 0){
                                    sql += " BAALL = '" + tBAAll.ToString() + "', ";
                                }

                                

                                if (tLeftDt.Date != DateTime.MinValue.Date)
                                {
                                    sql += " LeftDt ='" + tLeftDt.ToString("yyyy-MM-dd") + "', Active = 0 ,";
                                }

                                #region Bank_Contact_BloodGrp

                                if (!string.IsNullOrEmpty(dr["MobileNo"].ToString()))
                                     sql += " ContactNo ='" + dr["MobileNo"].ToString() + "'," ;
                                
                                if(!string.IsNullOrEmpty(dr["BANKNAME"].ToString()))
                                    sql += " BANKNAME ='" + dr["BANKNAME"].ToString() + "'," ;

                                if (!string.IsNullOrEmpty(dr["ACCOUNTNO"].ToString()))
                                    sql += " BankAcNo='" + dr["ACCOUNTNO"].ToString() + "',";

                                if(!string.IsNullOrEmpty(dr["IFSCCODE"].ToString()))
                                    sql+=  " BankIFSCCode='" + dr["IFSCCODE"].ToString() + "',";

                                if (!string.IsNullOrEmpty(dr["PANCARD"].ToString()))
                                    sql += " IDPRF1='PANCARD',IDPRF1NO='" + dr["PANCARD"].ToString() + "',";

                                if (!string.IsNullOrEmpty(dr["UANNO"].ToString()))
                                    sql += " IDPRF2='UAN',IDPRF2NO='" + dr["UANNO"].ToString() + "',";

                                if (!string.IsNullOrEmpty(dr["BloodGroup"].ToString()))
                                    sql += " BLDGrp='" + dr["BloodGroup"].ToString() + "',";

                                if (!string.IsNullOrEmpty(dr["EmailID"].ToString()))
                                    sql += " EmailID='" + dr["EmailID"].ToString() + "',";

                                #endregion

                                #region PERMENENT_ADDRESS


                                if (!string.IsNullOrEmpty(dr["PermenantAdd1"].ToString()))
                                    sql += " PerAdd1='" + dr["PermenantAdd1"].ToString() + "',";

                                if (!string.IsNullOrEmpty(dr["PermenantAdd2"].ToString()))
                                    sql += " PerAdd2='" + dr["PermenantAdd2"].ToString() + "',";

                                if (!string.IsNullOrEmpty(dr["PermenantAdd3"].ToString()))
                                    sql += " PerAdd3='" + dr["PermenantAdd3"].ToString() + "',";

                                if (!string.IsNullOrEmpty(dr["PermenantAdd4"].ToString()))
                                    sql += " PerAdd4='" + dr["PermenantAdd4"].ToString() + "',";

                                if (!string.IsNullOrEmpty(dr["PermenantDistrict"].ToString()))
                                    sql += " PerDistrict='" + dr["PermenantDistrict"].ToString() + "',";

                                if (!string.IsNullOrEmpty(dr["PermenantCity"].ToString()))
                                    sql += " PerCITY='" + dr["PermenantCity"].ToString() + "',";

                                if (!string.IsNullOrEmpty(dr["PermenantState"].ToString()))
                                    sql += " PerState='" + dr["PermenantState"].ToString() + "',";

                                if (!string.IsNullOrEmpty(dr["PermenantPinCode"].ToString()))
                                    sql += " PerPIN='" + dr["PermenantPinCode"].ToString() + "',";
                                #endregion

                                #region Present_Address

                                if (!string.IsNullOrEmpty(dr["PresantAdd1"].ToString()))
                                    sql += " PreAdd1='" + dr["PresantAdd1"].ToString() + "',";

                                if (!string.IsNullOrEmpty(dr["PresantAdd2"].ToString()))
                                    sql += " PreAdd2='" + dr["PresantAdd2"].ToString() + "',";

                                if (!string.IsNullOrEmpty(dr["PresantAdd3"].ToString()))
                                    sql += " PreAdd3='" + dr["PresantAdd3"].ToString() + "',";

                                if (!string.IsNullOrEmpty(dr["PresantAdd4"].ToString()))
                                    sql += " PreAdd4='" + dr["PresantAdd4"].ToString() + "',";

                                if (!string.IsNullOrEmpty(dr["PresantDistrict"].ToString()))
                                    sql += " PreDistrict='" + dr["PresantDistrict"].ToString() + "',";

                                if (!string.IsNullOrEmpty(dr["PresantCity"].ToString()))
                                    sql += " PreCITY='" + dr["PresantCity"].ToString() + "',";

                                if (!string.IsNullOrEmpty(dr["PresantState"].ToString()))
                                    sql += " PreState='" + dr["PresantState"].ToString() + "',";

                                if (!string.IsNullOrEmpty(dr["PresantPinCode"].ToString()))
                                    sql += " PrePIN='" + dr["PresantPinCode"].ToString() + "',";

                                #endregion


                                sql += "  UpdDt=GetDate(), UpdID = '" + Utils.User.GUserID + "' Where CompCode = '01' and EmpUnqID = '" + tEmpUnqID + "'";



                                string sql2 = "insert into MastEmpHistory " +
                               " select 'Before Update Master Data, Action By " + Utils.User.GUserID + "', GetDate(), * from MastEmp where " +
                               " EmpUnqID ='" + tEmpUnqID + "'";

                                cmd.CommandText = sql2;
                                cmd.CommandTimeout = 0;
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = sql;
                                cmd.CommandTimeout = 0;
                                cmd.ExecuteNonQuery();

                                dr["remarks"] = "Updated..";

                            }
                            catch (Exception ex)
                            {
                                dr["remarks"] = dr["remarks"].ToString() + ex.ToString();
                                continue;
                            }

                        }//using sqlcommand
                        #endregion
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
                string myexceldataquery = "select EmpUnqID,CatCode,GradeCode,DesgCode,Basic,SPLALL,BAALL,LeftDt, " +
                    "BANKNAME,IFSCCODE,ACCOUNTNO,PANCARD,UANNO,MobileNo,EmailID,BloodGroup, " +
                    "PermenantAdd1,PermenantAdd2,PermenantAdd3,PermenantAdd4,PermenantDistrict,PermenantCity,PermenantState,PermenantPinCode," +
                    "PresantAdd1,PresantAdd2,PresantAdd3,PresantAdd4,PresantDistrict,PresantCity,PresantState,PresantPinCode ," +
                    " '' as Remarks from " + sheetname;

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

        private void frmMastEmpBulkChange_Load(object sender, EventArgs e)
        {
            GRights = Attendance.Classes.Globals.GetFormRights(this.Name);
                
            grd_view.DataSource = null;
            btnImport.Enabled = false;
        }
    }
}