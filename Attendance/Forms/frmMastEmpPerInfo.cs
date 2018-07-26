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
    public partial class frmMastEmpPerInfo : Form
    {
        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";

        public frmMastEmpPerInfo()
        {
            InitializeComponent();
            this.ctrlEmp1.EmpUnqIDValidated += new EventHandler(this.ctrlEmpValidateEvent_Handler);
            //this.ctrlEmp1.CompCodeValidated += new EventHandler(this.ctrlCompValidateEvent_Handler);
        }

        private void ctrlEmpValidateEvent_Handler(object sender, EventArgs e)
        {
            if (!ctrlEmp1.cEmp.Active)
            {
                gridEdu.DataSource = null;
                gridExp.DataSource = null;
                gridFam.DataSource = null;
                mode = "New";
                oldCode = "";
                ResetCtrl();
            }
            else
            {
                mode = "OLD";
                oldCode = ctrlEmp1.cEmp.EmpUnqID;
                DisplayData(ctrlEmp1.txtCompCode.Text.Trim() ,oldCode);
                LoadGrid();
                SetRights();
            }
        }

        //private void ctrlCompValidateEvent_Handler(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(ctrlEmp1.txtCompCode.Text.Trim()))
        //        return;


        //}

        private void frmMastEmpPerInfo_Load(object sender, EventArgs e)
        {
            ResetCtrl();
            GRights = Attendance.Classes.Globals.GetFormRights(this.Name);
            SetRights();

            DataSet ds = Utils.Helper.GetData("Select IDDesc From MastIDProof Order By ID", Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>() .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtIDPrf1.Properties.Items.Add(dr["IDDesc"].ToString());
                    txtIDPrf2.Properties.Items.Add(dr["IDDesc"].ToString());
                    txtIDPrf3.Properties.Items.Add(dr["IDDesc"].ToString());
                }
            }

        }

        private string DataValidate()
        {
            string err = string.Empty;

            if (string.IsNullOrEmpty(ctrlEmp1.txtCompCode.Text.Trim().ToString()))
            {
                err = err + "Please Enter CompCode..." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(ctrlEmp1.txtEmpUnqID.Text.Trim().ToString()))
            {
                err = err + "Please Enter EmpUnqID..." + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(ctrlEmp1.cEmp.EmpUnqID) && !ctrlEmp1.IsValid)
            {
                err = err + "Invalid/InActive EmpUnqID..." + Environment.NewLine;
            }


            if (string.IsNullOrEmpty(ctrlEmp1.cEmp.CompDesc.Trim().ToString()))
            {
                err = err + "Invalid CompCode..." + Environment.NewLine;
            }

            if (chkMed.Checked && txtMedChkDt.DateTime == DateTime.MinValue)
            {
                err = err + "Please Eenter Medical Checkup Date..." + Environment.NewLine;
            }

            if (chkMed.Checked && txtMedRes.Text == string.Empty)
            {
                err = err + "Please Eenter Medical Checkup Result..." + Environment.NewLine;
            }

            if (chkSafety.Checked && txtSafetyDt.DateTime == DateTime.MinValue)
            {
                err = err + "Please Eenter Safety Training Date..." + Environment.NewLine;
            }

            return err;
        }

        private void ResetCtrl()
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            ctrlEmp1.ResetCtrl();


            txtAdd1.Text = "";
            txtAdd2.Text = "";
            txtAdd3.Text = "";
            txtAdd4.Text = "";
            txtCity.Text = "";
            txtDistrict.Text = "";
            txtState.Text = "";
            txtPinCode.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtPoliceST.Text = "";

            txtpAdd1.Text = "";
            txtpAdd2.Text = "";
            txtpAdd3.Text = "";
            txtpAdd4.Text = "";
            txtpCity.Text = "";
            txtpDistrict.Text = "";
            txtpState.Text = "";
            txtpPinCode.Text = "";
            txtpPhone.Text = "";
            txtMobile.Text = "";


            txtIDPrf1.Text = "";
            txtIDPrf2.Text = "";
            txtIDPrf3.Text = "";
            txtIDPrfExpOn1.EditValue = null;
            txtIDPrfExpOn2.EditValue = null;
            txtIDPrfExpOn3.EditValue = null;
            txtIDPrfNo1.Text = "";
            txtIDPrfNo2.Text = "";
            txtIDPrfNo3.Text = "";
            txtBankACNo.Text = "";
            txtBankIFSC.Text = "";
            txtBankName.Text = "";

            txtFamBirthDt.EditValue = null;
            txtFamName.Text = "";
            txtFamRel.Text = "";

            txtEduPer.Text = "";
            txtEduRemark.Text = "";
            txtEduStream.Text = "";
            txtEduSub.Text = "";
            txtEduUni.Text = "";
            txtEduYear.Text = "";

            txtExpDesg.Text = "";
            txtExpEmpCode.Text = "";
            txtExpFDt.EditValue = null;
            txtExpOrgName.Text = "";
            txtExpResp.Text = "";
            txtExpSkill.Text = "";
            txtExpTDt.EditValue = "";
            txtExpYear.Text = "";
            
            gridFam.DataSource = null;
            gridEdu.DataSource = null;
            gridExp.DataSource = null;

            chkMed.Checked = false;
            txtMedChkDt.EditValue = null;
            txtMedRes.Text = "";

            chkSafety.Checked = false;
            txtSafetyDt.EditValue = null;


            oldCode = "";
            mode = "NEW";
        }

        private void SetRights()
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            
            btnFamAdd.Enabled = false;
            btnFamDel.Enabled = false;

            btnEduAdd.Enabled = false;
            btnEduDelete.Enabled = false;

            btnExpAdd.Enabled = false;
            btnExpDelete.Enabled = false;




            if (ctrlEmp1.txtEmpUnqID.Text.Trim() != "" && mode == "NEW" && GRights.Contains("A"))
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

                btnEduAdd.Enabled = true;
                btnEduDelete.Enabled = true;
                btnFamAdd.Enabled = true;
                btnFamDel.Enabled = true;
                btnExpAdd.Enabled = true;
                btnExpDelete.Enabled = true;
            }
            
            if (ctrlEmp1.txtEmpUnqID.Text.Trim() != "" && mode == "OLD" && (GRights.Contains("U") || GRights.Contains("D")))
            {
                btnAdd.Enabled = false;


                btnEduAdd.Enabled = true;
                btnEduDelete.Enabled = true;
                btnFamAdd.Enabled = true;
                btnFamDel.Enabled = true;
                btnExpAdd.Enabled = true;
                btnExpDelete.Enabled = true;


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

                btnEduAdd.Enabled = false;
                btnEduDelete.Enabled = false;
                btnFamAdd.Enabled = false;
                btnFamDel.Enabled = false;
                btnExpAdd.Enabled = false;
                btnExpDelete.Enabled = false;
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

            //MessageBox.Show("....", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

                        string sql = "insert into MastEmpHistory " +
                           " select 'Before Update Emp Personal Data, Action By " + Utils.User.GUserID + "', GetDate(), * from MastEmp where CompCode = '" + ctrlEmp1.txtCompCode.Text.Trim() + "' " +
                           " and EmpUnqID ='" + ctrlEmp1.txtEmpUnqID.Text.Trim() + "'";
                        
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        sql = "Update MastEmp set " +
                            " PreAdd1 = '{0}', PreAdd2 = '{1}', PreAdd3='{2}',PreAdd4='{3}' , PreCity = '{4}',PreDistrict = '{5}', PreState='{6}' , PrePin ='{7}', " +
                            " PrePhone = '{8}', PrePoliceST = '{9}', EmailID = '{10}',ContactNo = '{11}'," +
                            " PerAdd1 = '{12}', PerAdd2 = '{13}', PerAdd3='{14}',PerAdd4='{15}' , PerCity = '{16}',PerDistrict = '{17}', PerState='{18}' , PerPin ='{19}', " +         
                            " IDPRF1 = '{20}',IDPRF1NO = '{21}', IDPRF1EXPON = {22} ," +
                            " IDPRF2 = '{23}',IDPRF2NO = '{24}', IDPRF2EXPON = {25} ," +
                            " IDPRF3 = '{26}',IDPRF3NO = '{27}', IDPRF3EXPON = {28} ," +   
                            " MedChkFlg = {29},MedChkDt = {30}, MedChkSts = '{31}' ," +
                            " SafetyTrnFlg = {32}, SafetyTrnDt = {33} ," +
                            " UpdDt = GetDate(),UpdID ='{34}' , " +
                            " BankAcNo = '{35}' , BankName = '{36}', BankIFSCCode = '{37}' " +
                            " where CompCode = '{38}' and EmpUnqID = '{39}' ";

                        sql = string.Format(sql, 
                            txtAdd1.Text.Trim().ToString(),
                            txtAdd2.Text.Trim().ToString(),
                            txtAdd3.Text.Trim().ToString(),
                            txtAdd4.Text.Trim().ToString(),
                            txtCity.Text.Trim().ToString(),
                            txtDistrict.Text.Trim().ToString(),
                            txtState.Text.Trim().ToString(),
                            txtPinCode.Text.Trim().ToString(),
                            txtPhone.Text.Trim().ToString(),
                            txtPoliceST.Text.Trim().ToString(),
                            txtEmail.Text.Trim().ToString(),
                            txtMobile.Text.Trim().ToString(),
                            txtpAdd1.Text.Trim().ToString(),
                            txtpAdd2.Text.Trim().ToString(),
                            txtpAdd3.Text.Trim().ToString(),
                            txtpAdd4.Text.Trim().ToString(),
                            txtpCity.Text.Trim().ToString(),
                            txtpDistrict.Text.Trim().ToString(),
                            txtpState.Text.Trim().ToString(),
                            txtpPinCode.Text.Trim().ToString(),
                            
                            txtIDPrf1.Text.Trim().ToString(),
                            txtIDPrfNo1.Text.Trim().ToString(),
                            ((txtIDPrfExpOn1.EditValue == DBNull.Value)? "null" : "'" + txtIDPrfExpOn1.DateTime.ToString("yyyy-MM-dd") + "'"),
                            
                            txtIDPrf2.Text.Trim().ToString(),
                            txtIDPrfNo2.Text.Trim().ToString(),
                            ((txtIDPrfExpOn2.EditValue == DBNull.Value) ? "null" : "'" + txtIDPrfExpOn2.DateTime.ToString("yyyy-MM-dd") + "'"),
                            
                            txtIDPrf3.Text.Trim().ToString(),
                            txtIDPrfNo3.Text.Trim().ToString(),
                            ((txtIDPrfExpOn3.EditValue == DBNull.Value) ? "null" : "'" + txtIDPrfExpOn3.DateTime.ToString("yyyy-MM-dd") + "'"),
                            
                            ((chkMed.Checked)?1:0),
                            ((txtMedChkDt.EditValue == DBNull.Value) ? "null" : "'" + txtMedChkDt.DateTime.ToString("yyyy-MM-dd") + "'"),
                            txtMedRes.Text.ToString(),

                            ((chkSafety.Checked)?1:0),
                            ((txtSafetyDt.EditValue == DBNull.Value) ? "null" : "'" + txtSafetyDt.DateTime.ToString("yyyy-MM-dd") + "'"),

                            Utils.User.GUserID,
                            txtBankACNo.Text.Trim().ToString(),
                            txtBankName.Text.Trim().ToString(),
                            txtBankIFSC.Text.Trim().ToString(),

                            ctrlEmp1.txtCompCode.Text.Trim(),
                            ctrlEmp1.txtEmpUnqID.Text.Trim()
                            
                            );

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record saved...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCtrl();
                        
                        return;

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

                DialogResult qs = MessageBox.Show("Are You Sure to Delete this Record...?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (qs == DialogResult.No)
                {
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



                            string sql = "insert into MastEmpHistory " +
                               " select 'Before Delete Emp Personal Data, Action By " + Utils.User.GUserID + "', GetDate(), * from MastEmp where CompCode = '" + ctrlEmp1.txtCompCode.Text.Trim() + "' " +
                               " and EmpUnqID ='" + ctrlEmp1.txtEmpUnqID.Text.Trim() + "'";

                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();

                            sql = "Update MastEmp set " +
                            " PreAdd1 = '{0}', PreAdd2 = '{1}', PreAdd3='{2}',PreAdd4='{3}' , PreCity = '{4}',PreDistrict = '{5}', PreState='{6}' , PrePin ='{7}', " +
                            " PrePhone = '{8}', PrePoliceST = '{9}', EmailID = '{10}',ContactNo = '{11}'," +
                            " PerAdd1 = '{12}', PreAdd2 = '{13}', PerAdd3='{14}',PerAdd4='{15}' , PerCity = '{16}',PerDistrict = '{17}', PerState='{18}' , PerPin ='{19}', " +
                            " IDPRF1 = '{20}',IDPRF1NO = '{21}', IDPRF1EXPON = {22} ," +
                            " IDPRF2 = '{23}',IDPRF2NO = '{24}', IDPRF2EXPON = {25} ," +
                            " IDPRF3 = '{26}',IDPRF1NO = '{27}', IDPRF1EXPON = {28} ," +
                            " UpdDt = GetDate(),UpdID ='{29}' where CompCode = '{30}' and EmpUnqID = '{31}' ";

                            sql = string.Format(sql,
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                               "",
                                "",
                                "",
                                "",
                                "",
                                "",
                               "",
                                "",
                                "",

                                "",
                                "",
                                 "null" ,

                                "",
                                "",
                                 "null",

                               "",
                                "",
                                 "null",


                                Utils.User.GUserID,
                                ctrlEmp1.txtCompCode.Text.Trim(),
                                ctrlEmp1.txtEmpUnqID.Text.Trim()
                                );

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Record Deleted...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetCtrl();
                           
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
            
            string EmpUnqID = ctrlEmp1.txtEmpUnqID.Text.Trim();

            string famsql = "Select [Sr],[Name],[BirthDt],[Relation] From [MastEmpFamily] where EmpUnqID='"  + EmpUnqID + "' order By Sr";
            string edusql = "SELECT [Sr],[PassingYear],[EduName],[Subject],[University],[Per],[OtherInfo] FROM [MastEmpEDU] where EmpUnqID='" + EmpUnqID + "' order By Sr" ;
            string expsql = "SELECT [Sr],[ExpYear],[CompName],[Designation],[JobResp],[FromDt],[ToDt],[EmpCode],[Skill] FROM [MastEmpExp] where EmpUnqID='" + EmpUnqID + "' order By Sr" ;
           
            //family
            ds = Utils.Helper.GetData(famsql, Utils.Helper.constr);
            Boolean hasRows = ds.Tables.Cast<DataTable>() .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                gridFam.DataSource = ds;
                gridFam.DataMember = ds.Tables[0].TableName;
            }
            else
            {
                gridFam.DataSource = null;
            }

            //education
            ds = Utils.Helper.GetData(edusql, Utils.Helper.constr);
            hasRows = ds.Tables.Cast<DataTable>() .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                gridEdu.DataSource = ds;
                gridEdu.DataMember = ds.Tables[0].TableName;
            }
            else
            {
                gridEdu.DataSource = null;
            }


            //education
            ds = Utils.Helper.GetData(expsql, Utils.Helper.constr);
            hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                gridExp.DataSource = ds;
                gridExp.DataMember = ds.Tables[0].TableName;
            }
            else
            {
                gridExp.DataSource = null;
            }



        }

        private void DisplayData(string tCompCode,string tEmpUnqID)
        {
            string sql = "Select * from MastEmp Where CompCode ='" + tCompCode + "' and EmpUnqID = '" + ctrlEmp1.txtEmpUnqID.Text.Trim() + "'";
            
            DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            Boolean hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                DataRow dr = ds.Tables[0].Rows[0];


                //Present Addresss
        
                txtAdd1.Text = dr["PreAdd1"].ToString();
                txtAdd2.Text = dr["PreAdd2"].ToString();
                txtAdd3.Text = dr["PreAdd3"].ToString();
                txtAdd4.Text = dr["PreAdd4"].ToString();
                txtCity.Text = dr["PreCity"].ToString();
                txtDistrict.Text = dr["PreDistrict"].ToString();
                txtState.Text = dr["PreState"].ToString();
                txtPinCode.Text = dr["PrePin"].ToString();
                txtPhone.Text = dr["PrePhone"].ToString();

                txtEmail.Text = dr["EmailID"].ToString();
                txtMobile.Text = dr["ContactNo"].ToString();
                txtPoliceST.Text = dr["PrePoliceST"].ToString();

                //Permanant Address
                txtpAdd1.Text = dr["PerAdd1"].ToString();
                txtpAdd2.Text = dr["PerAdd2"].ToString();
                txtpAdd3.Text = dr["PerAdd3"].ToString();
                txtpAdd4.Text = dr["PerAdd4"].ToString();
                txtpCity.Text = dr["PerCity"].ToString();
                txtpDistrict.Text = dr["PerDistrict"].ToString();
                txtpState.Text = dr["PerState"].ToString();
                txtpPinCode.Text = dr["PerPin"].ToString();

                //IDENTITY1
                txtIDPrf1.Text = dr["IDPRF1"].ToString();
                txtIDPrfNo1.Text = dr["IDPRF1NO"].ToString();
                txtIDPrfExpOn1.EditValue =dr["IDPRF1EXPON"];
       
                txtIDPrf2.Text = dr["IDPRF2"].ToString();
                txtIDPrfNo2.Text = dr["IDPRF2NO"].ToString();
                txtIDPrfExpOn2.EditValue = dr["IDPRF2EXPON"];

                txtIDPrf3.Text = dr["IDPRF3"].ToString();
                txtIDPrfNo3.Text = dr["IDPRF3NO"].ToString();
                txtIDPrfExpOn3.EditValue = dr["IDPRF3EXPON"];

                bool t = false;
                bool.TryParse(dr["MedChkFlg"].ToString(), out t);
                chkMed.Checked = t;
                txtMedChkDt.EditValue = dr["MedChkDt"];
                txtMedRes.Text = dr["MedChkSts"].ToString();

                t = false;
                bool.TryParse(dr["SafetyTrnFlg"].ToString(), out t);
                chkSafety.Checked = t;
                txtSafetyDt.EditValue = dr["SafetyTrnDT"];
                
                txtBankACNo.Text = dr["BankAcNo"].ToString();
                txtBankIFSC.Text = dr["BankIFSCCode"].ToString();
                txtBankName.Text = dr["BankName"].ToString();


                mode = "OLD";
                oldCode = tEmpUnqID;


            }
            else
            {
                mode = "NEW";
                oldCode = "";
            }
            

           
        }

        private void btnFamAdd_Click(object sender, EventArgs e)
        {
            if(mode != "OLD")
            {
                return;
            }
            
            if (string.IsNullOrEmpty(ctrlEmp1.txtEmpUnqID.Text.Trim()))
            {
                MessageBox.Show("Please Enter EmpUnqID...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (string.IsNullOrEmpty(txtFamName.Text.Trim()))
            {
                MessageBox.Show("Please Enter Family Member Name...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtFamRel.Text.Trim()))
            {
                MessageBox.Show("Please Enter Family Member Relation...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtFamBirthDt.EditValue == null)
            {
                MessageBox.Show("Please Enter Family Member BirthDate...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        string sEmpUnqID = ctrlEmp1.txtEmpUnqID.Text.Trim();
                        cn.Open();
                        cmd.Connection = cn;
                        string sql = "Select isnull(Max(Sr),0) + 1 from MastEmpFamily where EmpUnqID='" + sEmpUnqID + "'";

                        string srno = Utils.Helper.GetDescription(sql, Utils.Helper.constr);


                        sql = "Insert into MastEmpFamily (EmpUnqID,Sr,Name,Relation,BirthDt,AddDt,AddID) Values (" +
                            " '{0}', '{1}', '{2}','{3}' , '{4}', GetDate(),'{5}')";
                          

                        sql = string.Format(sql,sEmpUnqID,srno,
                            txtFamName.Text.Trim(),
                            txtFamRel.Text.Trim(),
                            txtFamBirthDt.DateTime.ToString("yyyy-MM-dd"),
                            Utils.User.GUserID
                            );

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record saved...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtFamBirthDt.EditValue = null;
                        txtFamName.Text = "";
                        txtFamRel.Text = "";
                        LoadGrid();
                        return;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnFamDel_Click(object sender, EventArgs e)
        {
            if (gvFam.FocusedRowHandle >= 0)
            {
                if (gvFam.IsValidRowHandle(gvFam.FocusedRowHandle))
                {
                    

                    DialogResult qs = MessageBox.Show("Are You Sure to Delete this Record...?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (qs == DialogResult.No)
                    {
                        return;
                    }


                    string srno =Convert.ToString(gvFam.GetRowCellValue(gvFam.FocusedRowHandle, "Sr"));

                    using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            try
                            {
                                string sEmpUnqID = ctrlEmp1.txtEmpUnqID.Text.Trim();
                                cn.Open();
                                cmd.Connection = cn;
                                
                                string sql = "Delete From MastEmpFamily Where EmpUnqID = '{0}' And Sr = '{1}'";
                                sql = string.Format(sql, sEmpUnqID, srno);

                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Record Deleted...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                LoadGrid();
                                return;

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }

            }
        }

        private void btnEduAdd_Click(object sender, EventArgs e)
        {
            if (mode != "OLD")
            {
                return;
            }

            if (string.IsNullOrEmpty(ctrlEmp1.txtEmpUnqID.Text.Trim()))
            {
                MessageBox.Show("Please Enter EmpUnqID...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (string.IsNullOrEmpty(txtEduYear.Text.Trim()))
            {
                MessageBox.Show("Please Enter Year...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtEduUni.Text.Trim()))
            {
                MessageBox.Show("Please Enter University/School Name...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtEduStream.Text.Trim()))
            {
                MessageBox.Show("Please Enter Stream Name...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtEduPer.Text.Trim()))
            {
                MessageBox.Show("Please Enter Marks(%) ...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        string sEmpUnqID = ctrlEmp1.txtEmpUnqID.Text.Trim();
                        cn.Open();
                        cmd.Connection = cn;
                        string sql = "Select isnull(Max(Sr),0) + 1 from MastEmpEDU where EmpUnqID='" + sEmpUnqID + "'";

                        string srno = Utils.Helper.GetDescription(sql, Utils.Helper.constr);


                        sql = "Insert into MastEmpEDU (EmpUnqID,Sr,PassingYear,EduName,Subject,University,Per,OtherInfo,AddDt,AddID) Values (" +
                            " '{0}', '{1}', '{2}','{3}' , '{4}', '{5}','{6}','{7}',GetDate(),'{8}')";


                        sql = string.Format(sql, sEmpUnqID, srno,
                            txtEduYear.Text.Trim(),
                            txtEduStream.Text.Trim(),
                             txtEduSub.Text.Trim(),
                            txtEduUni.Text.Trim(),
                            txtEduPer.Text.Trim(),
                            txtEduRemark.Text.Trim(),
                            Utils.User.GUserID
                            );

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record saved...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtEduPer.Text = "";
                        txtEduRemark.Text = "";
                        txtEduStream.Text = "";
                        txtEduSub.Text = "";
                        txtEduUni.Text = "";
                        txtEduYear.Text = "";

                        LoadGrid();
                        return;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnEduDelete_Click(object sender, EventArgs e)
        {
            if (gvEdu.FocusedRowHandle >= 0)
            {
                if (gvEdu.IsValidRowHandle(gvEdu.FocusedRowHandle))
                {


                    DialogResult qs = MessageBox.Show("Are You Sure to Delete this Record...?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (qs == DialogResult.No)
                    {
                        return;
                    }


                    string srno = Convert.ToString(gvEdu.GetRowCellValue(gvEdu.FocusedRowHandle, "Sr"));

                    using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            try
                            {
                                string sEmpUnqID = ctrlEmp1.txtEmpUnqID.Text.Trim();
                                cn.Open();
                                cmd.Connection = cn;

                                string sql = "Delete From MastEmpEdu Where EmpUnqID = '{0}' And Sr = '{1}'";
                                sql = string.Format(sql, sEmpUnqID, srno);

                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Record Deleted...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                LoadGrid();
                                return;

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }

            }
        }

        private void btnExpAdd_Click(object sender, EventArgs e)
        {
            if (mode != "OLD")
            {
                return;
            }

            if (string.IsNullOrEmpty(ctrlEmp1.txtEmpUnqID.Text.Trim()))
            {
                MessageBox.Show("Please Enter EmpUnqID...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (string.IsNullOrEmpty(txtExpOrgName.Text.Trim()))
            {
                MessageBox.Show("Please Enter Comp/Org. Name...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtExpDesg.Text.Trim()))
            {
                MessageBox.Show("Please Enter Designation/Post Name...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtExpResp.Text.Trim()))
            {
                MessageBox.Show("Please Enter Job Responcibility...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtExpYear.Text.Trim()))
            {
                MessageBox.Show("Please Enter Job Exp. Year...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtExpFDt.EditValue == null)
            {
                MessageBox.Show("Please Enter From Date ...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtExpTDt.EditValue == null)
            {
                MessageBox.Show("Please Enter To Date ...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtExpFDt.DateTime > txtExpTDt.DateTime)
            {
                MessageBox.Show("Please Invalid Date Range...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TimeSpan t = (txtExpFDt.DateTime - txtExpTDt.DateTime);
            txtExpYear.Text = Convert.ToInt32(Math.Abs(t.TotalDays / 365)).ToString();



            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        string sEmpUnqID = ctrlEmp1.txtEmpUnqID.Text.Trim();
                        cn.Open();
                        cmd.Connection = cn;
                        string sql = "Select isnull(Max(Sr),0) + 1 from MastEmpExp where EmpUnqID='" + sEmpUnqID + "'";

                        string srno = Utils.Helper.GetDescription(sql, Utils.Helper.constr);


                        sql = "Insert into MastEmpExp (EmpUnqID,Sr,CompName,Designation,JobResp,FromDt,ToDt,ExpYear,EmpCode,Skill ,AddDt,AddID) Values (" +
                            " '{0}', '{1}', '{2}','{3}' , '{4}', '{5}','{6}','{7}','{8}','{9}',GetDate(),'{10}')";


                        sql = string.Format(sql, sEmpUnqID, srno,
                                 txtExpOrgName.Text.Trim(),
                                 txtExpDesg.Text.Trim(),
                                 txtExpResp.Text.Trim(),
                                 txtExpFDt.DateTime.ToString("yyyy-MM-dd"),
                                 txtExpTDt.DateTime.ToString("yyyy-MM-dd"),
                                 txtExpYear.Text.Trim(),
                                 txtExpEmpCode.Text.Trim(),
                                 txtExpSkill.Text.Trim(),
                                 Utils.User.GUserID
                            );

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record saved...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        
                        txtExpDesg.Text = "";
                        txtExpEmpCode.Text = "";
                        txtExpFDt.EditValue = null;
                        txtExpTDt.EditValue = null;

                        txtExpYear.Text = "";
                        txtExpSkill.Text = "";
                        txtExpResp.Text = "";
                        txtExpOrgName.Text = "";

                        LoadGrid();
                        return;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnExpDelete_Click(object sender, EventArgs e)
        {
            if (gvExp.FocusedRowHandle >= 0)
            {
                if (gvExp.IsValidRowHandle(gvExp.FocusedRowHandle))
                {


                    DialogResult qs = MessageBox.Show("Are You Sure to Delete this Record...?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (qs == DialogResult.No)
                    {
                        return;
                    }


                    string srno = Convert.ToString(gvExp.GetRowCellValue(gvExp.FocusedRowHandle, "Sr"));

                    using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            try
                            {
                                string sEmpUnqID = ctrlEmp1.txtEmpUnqID.Text.Trim();
                                cn.Open();
                                cmd.Connection = cn;

                                string sql = "Delete From MastEmpExp Where EmpUnqID = '{0}' And Sr = '{1}'";
                                sql = string.Format(sql, sEmpUnqID, srno);

                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Record Deleted...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                LoadGrid();
                                return;

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }

            }
        }

        private void frmMastEmpPerInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter))
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void txtBankName_KeyDown(object sender, KeyEventArgs e)
        {
            //if (txtBankName.Text.Trim() == "")
            //    return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select BankName,* From MastBank Where 1=1";
                if (e.KeyCode == Keys.F1)
                {

                    obj = (List<string>)hlp.Show(sql, "BankName", "BankName", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtBankName.Text = obj.ElementAt(0).ToString();

                }
            }
        }



    }
}
