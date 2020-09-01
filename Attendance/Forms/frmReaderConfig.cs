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
    public partial class frmReaderConfig : Form
    {
        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";

        public frmReaderConfig()
        {
            InitializeComponent();
        }

        private void frmReaderConfig_Load(object sender, EventArgs e)
        {
            ResetCtrl();
            GRights = Attendance.Classes.Globals.GetFormRights(this.Name);
            SetRights();
            LoadGrid();
        }

        private string DataValidate()
        {
            string err = string.Empty;

            if (string.IsNullOrEmpty(txtCompCode.Text))
            {
                err = err + "Please Enter CompCode " + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtCompName.Text))
            {
                err = err + "Please Enter CompName..." + Environment.NewLine;
            }


            if (string.IsNullOrEmpty(txtIPAdd.Text))
            {
                err = err + "Please Enter Machine IP Address.." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                err = err + "Please Enter Machine Description" + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtINOut.Text))
            {
                err = err + "Please Enter Machine In/Out Type" + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtMachineNo.Text))
            {
                err = err + "Please Enter Machine Number.." + Environment.NewLine;
            }

            if(chkRFID.CheckState == CheckState.Unchecked && chkFace.CheckState == CheckState.Unchecked 
                && chkFinger.CheckState == CheckState.Unchecked ) {

                err = err + "Please Select Atleast One Features From (RFID/Face/Finger)" + Environment.NewLine;

            }

            if(chkRestPost.CheckState == CheckState.Checked && string.IsNullOrEmpty(txtRestAPI.Text.Trim().ToString()))
            {
                err = err + "Please Provice REST API URL" + Environment.NewLine;
            }
               
            //check for single master machine..
            if (chkMaster.CheckState == CheckState.Checked)
            {
                using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cn.Open();
                            cmd.Connection = cn;
                            string sql = "Select Count(*) from ReaderConfig where CompCode = '" + txtCompCode.Text.Trim() + "' " 
                                + " And Master = 1 and MachineIP not in ('" + txtIPAdd.Text.Trim().ToString() + "')";

                            cmd.CommandText = sql;
                            int cnt = (int)cmd.ExecuteScalar();
                            if (cnt > 0)
                            {
                                err = err + "You can not add multiple master machine...." + Environment.NewLine;
                            }
                        }
                        catch (Exception ex)
                        {
                            err = err + ex.ToString();
                        }
                    }
                }
            }

            if (chkMaster.CheckState == CheckState.Checked)
            {
                chkLunchInOut.CheckState = CheckState.Unchecked;
                chkGateInOut.CheckState = CheckState.Unchecked;
                chkMessUse.CheckState = CheckState.Unchecked;
                chkActive.CheckState = CheckState.Checked;
                chkAuto.CheckState = CheckState.Unchecked;
                chkRestPost.CheckState = CheckState.Unchecked;
                txtRestAPI.Text = "";
            }

            int t = 0;
            if (chkLunchInOut.CheckState == CheckState.Checked ) 
            {
                t = t + 1;
            }

            if (chkGateInOut.CheckState == CheckState.Checked)
            {
                t = t + 1;
            }

            if (chkMessUse.CheckState == CheckState.Checked)
            {
                t = t + 1;
            }

            if (t > 1)
            {
                err = err + "Please Select Only One Purpose.." + Environment.NewLine;
            }

            return err;
        }
        
        private void ResetCtrl()
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            
            object s = new object();
            EventArgs e = new EventArgs();
            txtCompCode.Text = "01";
            txtCompName.Text = "";
            txtCompCode_Validated(s, e);
           
            txtIPAdd.Text = "";
            txtDescription.Text = "";
            txtLocation.Text = "";
            txtMachineNo.Text = "";
            txtINOut.Text = "IN";

            chkActive.CheckState = CheckState.Unchecked;
            chkMaster.CheckState = CheckState.Unchecked;
            chkAuto.CheckState = CheckState.Unchecked;

            chkFace.CheckState = CheckState.Unchecked;
            chkRFID.CheckState = CheckState.Unchecked;
            chkFinger.CheckState = CheckState.Unchecked;

            chkGateInOut.CheckState = CheckState.Unchecked;
            chkLunchInOut.CheckState = CheckState.Unchecked;
            chkMessUse.CheckState = CheckState.Unchecked;

            chkRestPost.CheckState = CheckState.Unchecked;
            txtRestAPI.Text = "";


            oldCode = "";
            mode = "NEW";
        }

        private void SetRights()
        {
            if ( txtIPAdd.Text.Trim() != "" && mode == "NEW" && GRights.Contains("A") )
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            else if ( txtIPAdd.Text.Trim() != "" && mode == "OLD" )
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


        private void txtIPAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCompCode.Text.Trim() == "")
                return;
            
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select MachineIP,MachineDesc from ReaderConfig Where CompCode ='" + txtCompCode.Text.Trim() + "' and Delflg = 0";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "MachineIP", "MachineIP", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtIPAdd.Text = obj.ElementAt(0).ToString();
                    txtDescription.Text = obj.ElementAt(1).ToString();
                    
                    mode = "OLD";
                }
            }
        }

        private void txtIPAdd_Validated(object sender, EventArgs e)
        {
            if (txtCompCode.Text.Trim() == "" || txtCompName.Text.Trim() == "" || txtIPAdd.Text.Trim() == "")
            {
                mode = "NEW";
                return;
            }

            DataSet ds = new DataSet();
            string sql = "select * From  ReaderConfig where CompCode ='" + txtCompCode.Text.Trim() + "' and MachineIP='" + txtIPAdd.Text.Trim() + "'";
            
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCompCode.Text = dr["CompCode"].ToString();
                    txtIPAdd.Text = dr["MachineIP"].ToString();
                    txtDescription.Text = dr["MachineDesc"].ToString();
                    txtMachineNo.Text = dr["MachineNo"].ToString();
                    txtLocation.Text = dr["Location"].ToString().Trim();

                    switch (dr["IOFLG"].ToString().ToUpper().Trim())
                    {
                        case  "I" :
                            txtINOut.Text = "IN";
                            break;
                        case "O" :
                            txtINOut.Text = "OUT";
                            break;
                        case "B" :
                            txtINOut.Text = "BOTH";
                            break;
                        default :
                            txtINOut.Text = "IN";
                            break;
                    }
                    chkActive.CheckState = Convert.ToBoolean((dr["Active"] == DBNull.Value) ? "FALSE" : dr["Active"]) ? CheckState.Checked : CheckState.Unchecked;
                    chkMaster.CheckState = Convert.ToBoolean((dr["Master"] == DBNull.Value) ? "FALSE" : dr["Master"]) ? CheckState.Checked : CheckState.Unchecked;
                    chkRFID.CheckState = Convert.ToBoolean((dr["RFID"] == DBNull.Value) ? "FALSE" : dr["RFID"]) ? CheckState.Checked : CheckState.Unchecked;
                    chkFace.CheckState = Convert.ToBoolean((dr["FACE"] == DBNull.Value) ? "FALSE" : dr["FACE"]) ? CheckState.Checked : CheckState.Unchecked;
                    chkFinger.CheckState = Convert.ToBoolean((dr["FINGER"] == DBNull.Value) ? "FALSE" : dr["FINGER"]) ? CheckState.Checked : CheckState.Unchecked;
                    chkMessUse.CheckState = Convert.ToBoolean((dr["CanteenFlg"] == DBNull.Value) ? "FALSE" : dr["CanteenFlg"]) ? CheckState.Checked : CheckState.Unchecked;
                    chkAuto.CheckState = Convert.ToBoolean((dr["AutoClear"] == DBNull.Value) ? "FALSE" : dr["AutoClear"]) ? CheckState.Checked : CheckState.Unchecked;
                    chkGateInOut.CheckState = Convert.ToBoolean((dr["GateInOut"] == DBNull.Value) ? "FALSE" : dr["GateInOut"]) ? CheckState.Checked : CheckState.Unchecked;
                    chkLunchInOut.CheckState = Convert.ToBoolean((dr["LunchInOut"] == DBNull.Value) ? "FALSE" : dr["LunchInOut"]) ? CheckState.Checked : CheckState.Unchecked;
                    chkRestPost.CheckState = Convert.ToBoolean((dr["RestPost"] == DBNull.Value) ? "FALSE" : dr["RestPost"]) ? CheckState.Checked : CheckState.Unchecked;
                    txtRestAPI.Text = dr["RestAPI"].ToString().Trim();

                    mode = "OLD";
                    txtCompCode_Validated(sender,e);
                    oldCode = dr["MachineIP"].ToString();
                }
            }
            else
            {
                mode = "NEW";
                oldCode = "";
            }

            SetRights();
        }

        private void txtCompCode_Validated(object sender, EventArgs e)
        {
            if (txtCompCode.Text.Trim() == "")
            {   
                return;
            }

            DataSet ds = new DataSet();
            string sql = "select * from MastComp where CompCode ='" + txtCompCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCompCode.Text = dr["CompCode"].ToString();
                    txtCompName.Text = dr["CompName"].ToString();
                    LoadGrid();
                }
            }
            else
            {
                txtCompName.Text = "";
            }
            
        }

        private void txtCompCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 )
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select CompCode,CompName From MastComp Where 1 = 1";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "CompCode", "CompCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtCompCode.Text = obj.ElementAt(0).ToString();
                    txtCompName.Text = obj.ElementAt(1).ToString();

                }
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
                        string sql = "Insert into ReaderConfig " +
                            "(CompCode,MachineIP,MachineDesc,Location,MachineNo,IOFLG," +
                            " AutoClear,Active,Master,RFID,Finger,FACE," +
                            " CanteenFlg,LunchInOut,GateInOut," +
                            " AddDt,AddID,RestPost,RestAPI) Values ('{0}','{1}','{2}','{3}','{4}','{5}'," +
                            " '{6}','{7}','{8}','{9}','{10}','{11}'," +
                            " '{12}','{13}','{14}',GetDate(),'{15}','{16}','{17}')";

                        sql = string.Format(sql, txtCompCode.Text.Trim().ToString(), txtIPAdd.Text.Trim().ToString(),
                            txtDescription.Text.Trim().ToString(),txtLocation.Text.Trim().ToString(),txtMachineNo.Text.Trim().ToString(),txtINOut.Text.ToString().Substring(0,1),
                            ((chkAuto.Checked)?"1":"0"),((chkActive.Checked)?"1":"0"),((chkMaster.Checked)?"1":"0"),((chkRFID.Checked)?"1":"0"),((chkFinger.Checked)?"1":"0"),((chkFace.Checked)?"1":"0"),
                            ((chkMessUse.Checked)?"1":"0"),((chkLunchInOut.Checked)?"1":"0"),((chkGateInOut.Checked)?"1":"0"),
                            Utils.User.GUserID, ((chkRestPost.Checked) ? "1" : "0"),txtRestAPI.Text.Trim().ToString());

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record saved...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCtrl();
                        LoadGrid();
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
                        string sql = "Update ReaderConfig Set MachineDesc = '{0}', "
                            + " Location='{1}',IOFLG ='{2}',MachineNo = '{3}',"
                            + " RFID ='{4}',FACE='{5}',Finger='{6}',"
                            + " GateInOut = '{7}',LunchInOut = '{8}', CanteenFlg = '{9}'," 
                            + " Active = '{10}', Master = '{11}', AutoClear = '{12}'," 
                            + " UpdDt = GetDate(), UpdID = '{13}', " 
                            + " RestPost = '{14}', RestAPI = '{15}' " 
                            + " Where CompCode = '{16}' and MachineIP = '{17}' ";

                        sql = string.Format(sql, txtDescription.Text.Trim(),
                             txtLocation.Text.Trim().ToString(),txtINOut.Text.ToString().Substring(0,1),txtMachineNo.Text.Trim(),
                             ((chkRFID.Checked)?"1":"0"), ((chkFace.Checked)?"1":"0"),((chkFinger.Checked)?"1":"0"),
                             ((chkGateInOut.Checked)?"1":"0"),((chkLunchInOut.Checked)?"1":"0"),((chkMessUse.Checked)?"1":"0"),
                             ((chkActive.Checked)?"1":"0"),((chkMaster.Checked)?"1":"0"),((chkAuto.Checked)?"1":"0"),
                             Utils.User.GUserID,
                             ((chkRestPost.Checked)?"1":"0"),
                             txtRestAPI.Text.Trim().ToString(),
                             txtCompCode.Text.Trim().ToString(), txtIPAdd.Text.Trim().ToString()
                           );

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        ResetCtrl();
                        LoadGrid();
                        MessageBox.Show("Record Updated...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
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
               
                DialogResult qs = MessageBox.Show("Are You Sure to Delete this machine...?","Question",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
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
                            string sql = "Delete From ReaderConfig where CompCode = '" + txtCompCode.Text.Trim() + "' and MachineIP = '" + txtIPAdd.Text.Trim().ToString() + "'";
                            cmd.CommandText = sql;
                            cmd.Connection = cn;
                            cmd.ExecuteNonQuery();
                           
                            
                            MessageBox.Show("Record Deleted...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            DataSet ds = new DataSet();
            string sql = "select CompCode,MachineIP,MachineDesc,MachineNo,IOFLG,AutoClear,RFID,FACE, " +
                    " Finger,CanteenFLG,LunchInOut,GateInOut,Active,Master from ReaderConfig where DelFlg = 0 Order By MachineNo";

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
               txtIPAdd.Text = gridView1.GetRowCellValue(info.RowHandle, "MachineIP").ToString();
                object o = new object();
                EventArgs e = new EventArgs();                
                mode = "OLD";
                oldCode = txtIPAdd.Text.ToString();
                txtIPAdd_Validated(o, e);
            }


        }

        private void label13_Click(object sender, EventArgs e)
        {

        }


    }
}
