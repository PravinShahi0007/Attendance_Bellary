using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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
using System.IO;


namespace Attendance.Forms
{
    public partial class frmDataDownload : Form
    {

        public string GRights = "XXXV";

        
        private bool SelAllFlg = false;
        private DataSet srcDs = new DataSet();

        public frmDataDownload()
        {
            InitializeComponent();
        }

        private void LoadGrid()
        {
            string sql = "Select CONVERT(BIT,0) as SEL,0 as Records,'' as Remarks , * From ReaderConfig Where Active = 1 and Master = 0 Order By MachineDesc,IOFLG";
            
            srcDs = Utils.Helper.GetData(sql, Utils.Helper.constr);
            grpGrid.DataSource = srcDs;
            grpGrid.DataMember = srcDs.Tables[0].TableName;

            try
            {
                gv_avbl.Appearance.HeaderPanel.Font = new System.Drawing.Font(gv_avbl.Appearance.ViewCaption.Font, FontStyle.Bold);
                gv_avbl.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                
                foreach (GridColumn gc in gv_avbl.Columns)
                {
                    gc.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    if (gc.FieldName.ToUpper() == "MACHINEDESC")
                    {
                        gc.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                    }
                }

            }
            catch { }
            


            

            gv_avbl.RefreshData();          

        }

        private void SetRights()
        {
            if (Utils.User.GUserID == "SERVER")
            {
                GRights = "AUDV";
            }
            
            
            if (GRights.Contains("AUDV"))
            {
                
                btnDownload.Enabled = true;
                btnRestartMach.Enabled = true;
                btnUnockMach.Enabled = true;
                btnSetTime.Enabled = true;
            }
            else
            {
                
                btnDownload.Enabled = false;
                btnRestartMach.Enabled = false;
                btnUnockMach.Enabled = false;
                btnSetTime.Enabled = false;
            }
        }

        private void LockCtrl()
        {
            grpButtons.Enabled = false;
            grpGrid.Enabled = false;
        }

        private void UnLockCtrl()
        {
            grpButtons.Enabled = true;
            grpGrid.Enabled = true;
        }

        private void frmDataDownload_Load(object sender, EventArgs e)
        {
            LoadGrid();
            GRights = Attendance.Classes.Globals.GetFormRights(this.Name);
            SetRights();
        }

        private void btnSelAll_Click(object sender, EventArgs e)
        {
            if (gv_avbl.DataRowCount <= 0)
            {
                return;
            }
            Cursor.Current = Cursors.WaitCursor;

            SelAllFlg = (!SelAllFlg);

            for (int i = 0; i <= gv_avbl.DataRowCount - 1; i++)
            {
                if (SelAllFlg == true)
                {
                    gv_avbl.SetRowCellValue(i, "SEL", true);
                    
                }
                else
                {
                    gv_avbl.SetRowCellValue(i, "SEL", false);
                    
                }
                
            }

            Cursor.Current = Cursors.Default;
            
        }

        private void btnSetTime_Click(object sender, EventArgs e)
        {
            ResetRemarks();
            LockCtrl();
            Cursor.Current = Cursors.WaitCursor;

            for (int i = 0; i < gv_avbl.DataRowCount; i++)
            {
                string tsel = gv_avbl.GetRowCellValue(i, "SEL").ToString();
                if (!Convert.ToBoolean(tsel))
                    continue;
                
                string ip = gv_avbl.GetRowCellValue(i, "MachineIP").ToString();
                string ioflg = gv_avbl.GetRowCellValue(i, "IOFLG").ToString().Trim();
                gv_avbl.SetRowCellValue(i, "Remraks", "Connecting");

                clsMachine m = new clsMachine(ip, ioflg);
                string err = string.Empty;

                //try to connect
                m.Connect(out err);
                gv_avbl.SetRowCellValue(i, "Remarks", err);

                if (!string.IsNullOrEmpty(err))
                {
                    continue;
                }

                m.SetTime(out err);

                if (string.IsNullOrEmpty(err))
                {
                    gv_avbl.SetRowCellValue(i, "Remarks", "Completed..");
                }
                else
                {
                    gv_avbl.SetRowCellValue(i, "Remarks", err);
                }

                m.DisConnect(out err);

            }


            UnLockCtrl();
            Cursor.Current = Cursors.WaitCursor;
        }

        private void ResetRemarks()
        {
            for (int i = 0; i < gv_avbl.DataRowCount; i++)
            {
                
                    gv_avbl.SetRowCellValue(i, "Records", 0);
                    gv_avbl.SetRowCellValue(i, "Remarks", "");
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            ResetRemarks();
            
            
            LockCtrl();
            Cursor.Current = Cursors.WaitCursor;

            for (int i = 0; i < gv_avbl.DataRowCount; i++)
            {
                //check if selected...
                string tsel = gv_avbl.GetRowCellValue(i, "SEL").ToString();
                if (!Convert.ToBoolean(tsel))
                    continue;
                
                string ip = gv_avbl.GetRowCellValue(i, "MachineIP").ToString();
                string ioflg = gv_avbl.GetRowCellValue(i, "IOFLG").ToString().Trim();
                
                clsMachine m = new clsMachine(ip,ioflg);
                string err = string.Empty;
                List<AttdLog> records = new List<AttdLog>();
                
                //try to connect
                m.Connect(out err);

                gv_avbl.SetRowCellValue(i, "Records", 0);
                gv_avbl.SetRowCellValue(i, "Remarks", err);


                string nerr = string.Empty;

                if (!string.IsNullOrEmpty(err))
                {
                    m.DisConnect(out nerr);
                    gv_avbl.SetRowCellValue(i, "Remarks", err + ";" + nerr);
                    continue;
                }

                //get records
                m.GetAttdRec(out records,out err);
                gv_avbl.SetRowCellValue(i, "Remarks", err);

                gv_avbl.SetRowCellValue(i, "Records", records.Count());
                

                if (string.IsNullOrEmpty(err))
                {
                
                    gv_avbl.SetRowCellValue(i, "Remarks", "Download Completed...");
                }
                else
                {
                    gv_avbl.SetRowCellValue(i, "Remarks", err);
                }
                m.DisConnect(out nerr);


                
            }
            
            UnLockCtrl();
            Cursor.Current = Cursors.WaitCursor;
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
                            gv_avbl.ExportToXls(exportFilePath);
                            break;
                        case ".xlsx":
                            gv_avbl.ExportToXlsx(exportFilePath);
                            break;
                        case ".rtf":
                            gv_avbl.ExportToRtf(exportFilePath);
                            break;
                        case ".pdf":
                            gv_avbl.ExportToPdf(exportFilePath);
                            break;
                        case ".html":
                            gv_avbl.ExportToHtml(exportFilePath);
                            break;
                        case ".mht":
                            gv_avbl.ExportToMht(exportFilePath);
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

        private void btnRestartMach_Click(object sender, EventArgs e)
        {
            ResetRemarks();


            LockCtrl();
            Cursor.Current = Cursors.WaitCursor;

            for (int i = 0; i < gv_avbl.DataRowCount; i++)
            {
                //check if selected...
                string tsel = gv_avbl.GetRowCellValue(i, "SEL").ToString();
                if (!Convert.ToBoolean(tsel))
                    continue;

                string ip = gv_avbl.GetRowCellValue(i, "MachineIP").ToString();
                string ioflg = gv_avbl.GetRowCellValue(i, "IOFLG").ToString().Trim();

                clsMachine m = new clsMachine(ip, ioflg);
                string err = string.Empty;
                
                //try to connect
                m.Connect(out err);

                gv_avbl.SetRowCellValue(i, "Records", 0);
                gv_avbl.SetRowCellValue(i, "Remarks", err);


                string nerr = string.Empty;
                if (!string.IsNullOrEmpty(err))
                {
                    m.DisConnect(out nerr);
                    gv_avbl.SetRowCellValue(i, "Remarks", err + ";" + nerr);
                    continue;
                }

                
                m.Restart(out err);
                gv_avbl.SetRowCellValue(i, "Remarks", err);

                if (string.IsNullOrEmpty(err))
                {

                    gv_avbl.SetRowCellValue(i, "Remarks", "Restart Completed...");
                }
                else
                {
                    gv_avbl.SetRowCellValue(i, "Remarks",  err);
                }
                m.DisConnect(out nerr);



            }

            UnLockCtrl();
            Cursor.Current = Cursors.WaitCursor;
        }

        private void btnUnockMach_Click(object sender, EventArgs e)
        {
            ResetRemarks();


            LockCtrl();
            Cursor.Current = Cursors.WaitCursor;

            for (int i = 0; i < gv_avbl.DataRowCount; i++)
            {
                //check if selected...
                string tsel = gv_avbl.GetRowCellValue(i, "SEL").ToString();
                if (!Convert.ToBoolean(tsel))
                    continue;

                string ip = gv_avbl.GetRowCellValue(i, "MachineIP").ToString();
                string ioflg = gv_avbl.GetRowCellValue(i, "IOFLG").ToString().Trim();

                clsMachine m = new clsMachine(ip, ioflg);
                string err = string.Empty;

                //try to connect
                m.Connect(out err);

                gv_avbl.SetRowCellValue(i, "Records", 0);
                gv_avbl.SetRowCellValue(i, "Remarks", err);


                string nerr = string.Empty;
                if (!string.IsNullOrEmpty(err))
                {
                    m.DisConnect(out nerr);
                    gv_avbl.SetRowCellValue(i, "Remarks", err + ";" + nerr);
                    continue;
                }


                m.Unlock(out err);
                gv_avbl.SetRowCellValue(i, "Remarks", err);

                if (string.IsNullOrEmpty(err))
                {

                    gv_avbl.SetRowCellValue(i, "Remarks", "Machine Unlocked...");
                }
                else
                {
                    gv_avbl.SetRowCellValue(i, "Remarks",  err);
                }

                m.DisConnect(out nerr);

            }

            UnLockCtrl();
            Cursor.Current = Cursors.WaitCursor;
        }

        private void btnClearMach_Click(object sender, EventArgs e)
        {
            string msg = "Make Sure to Download all data in multiple application if running with common machines" + Environment.NewLine +
                "However Application will download data first, which only accounted in running application. " + Environment.NewLine + 
                "Are You Sure to Clear selected machine ?";


            DialogResult dr = MessageBox.Show(msg, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(dr != DialogResult.Yes)
            {
                return;
            }
           
            ResetRemarks();
            btnDownload_Click(sender, e);
            ResetRemarks();
            LockCtrl(); 
            
            Cursor.Current = Cursors.WaitCursor;

            for (int i = 0; i < gv_avbl.DataRowCount; i++)
            {
                //check if selected...
                string tsel = gv_avbl.GetRowCellValue(i, "SEL").ToString();
                if (!Convert.ToBoolean(tsel))
                    continue;

                string ip = gv_avbl.GetRowCellValue(i, "MachineIP").ToString();
                string ioflg = gv_avbl.GetRowCellValue(i, "IOFLG").ToString().Trim();

                clsMachine m = new clsMachine(ip, ioflg);
                string err = string.Empty;
                

                //try to connect
                m.Connect(out err);

                gv_avbl.SetRowCellValue(i, "Records", 0);
                gv_avbl.SetRowCellValue(i, "Remarks", err);


                string nerr = string.Empty;

                if (!string.IsNullOrEmpty(err))
                {
                    m.DisConnect(out nerr);
                    gv_avbl.SetRowCellValue(i, "Remarks", err + ";" + nerr);
                    continue;
                }

                //Clear Machine
                m.AttdLogClear(out err);
                if (string.IsNullOrEmpty(err))
                {
                    gv_avbl.SetRowCellValue(i, "Remarks", "Log Clear...");
                }
                else
                {
                    gv_avbl.SetRowCellValue(i, "Remarks", err);
                }
                m.DisConnect(out nerr);

            }

            UnLockCtrl();
            Cursor.Current = Cursors.WaitCursor;
        }
    }
}
