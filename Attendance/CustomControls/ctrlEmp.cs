using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Attendance.Classes;
namespace Attendance
{
    public partial class ctrlEmp : UserControl
    {
        public clsEmp cEmp = new clsEmp();
        public bool IsValid = false;

        public event EventHandler CompCodeValidated;
        public event EventHandler EmpUnqIDValidated;


        public ctrlEmp()
        {
            InitializeComponent();
        }

        private void txtEmpUnqID_Validated(object sender, EventArgs e)
        {
            cEmp = new clsEmp();

           

            if (string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()) || string.IsNullOrEmpty(txtCompCode.Text.Trim()))
            {
                IsValid = false;
                return;
            }

            bool t = cEmp.GetEmpDetails(txtCompCode.Text.Trim().ToString(),txtEmpUnqID.Text.Trim().ToString());

            if (t)
            {
                DisplayData();

            }
            else
            {
                IsValid = false;
                ResetCtrl();
            }

            if (this.EmpUnqIDValidated != null) this.EmpUnqIDValidated(sender, e);
        }

        private void ctrlEmp_Load(object sender, EventArgs e)
        {
            ResetCtrl();

        }

        private void txtCompCode_Validated(object sender, EventArgs e)
        {

            if (this.CompCodeValidated != null) this.CompCodeValidated(sender, e);
            
            if(string.IsNullOrEmpty(txtCompCode.Text.Trim()))
            {
                return;
            }
            
            string tCompCode = txtCompCode.Text.Trim();

            DataSet ds = new DataSet();
            string sql = "select * From MastComp where CompCode ='" + tCompCode + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtCompDesc.Text = dr["CompName"].ToString();

                }
            }
        }

        public void ResetCtrl()
        {
            IsValid = false;
            txtCompCode.Text = "01";
            object sender = new object();
            EventArgs e = new EventArgs();
            txtCompCode_Validated(sender, e);
            cEmp = new clsEmp();
            txtEmpUnqID.Text = string.Empty;
            txtEmpName.Text = string.Empty;
            txtUnitCode.Text = string.Empty;
            txtUnitDesc.Text = string.Empty;
            txtWrkGrpCode.Text = string.Empty;
            txtWrkGrpDesc.Text = string.Empty;
            txtDeptCode.Text = string.Empty;
            txtDeptDesc.Text = string.Empty;
            txtStatCode.Text = string.Empty;
            txtStatDesc.Text = string.Empty;
            txtGradeCode.Text = string.Empty;
            txtGradeDesc.Text = string.Empty;
            txtDesgCode.Text = string.Empty;
            txtDesgDesc.Text = string.Empty;
            txtCatCode.Text = string.Empty;
            txtCatDesc.Text = string.Empty;
            txtEmpType.Text = string.Empty;
            txtEmpTypeDesc.Text = string.Empty;
            
            txtShiftCode.Text = string.Empty;
            txtShiftDesc.Text = string.Empty;
            txtContCode.Text = string.Empty;
            txtContCode1.Text = string.Empty;

            txtContDesc.Text = string.Empty;
            txtMessCode.Text = string.Empty;
            txtMessDesc.Text = string.Empty;
            txtMessGrpCode.Text = string.Empty;
            txtMessGrpDesc.Text = string.Empty;
            txtCostCode.Text = string.Empty;
            txtCostDesc.Text = string.Empty;
            
            txtEmpCode.Text = string.Empty;
            txtOldEmpCode.Text = string.Empty;
            txtSAPID.Text = string.Empty;
            txtAdharNo.Text = string.Empty;

            chkActive.Checked = false;
            chkComp.Checked = false;
            chkCont.Checked = false;

            chkAutoShift.Checked = false;            
            chkOTFlg.Checked = false;
            chkMale.Checked = false;
            
            txtBirthDT.EditValue = null;
            txtJoinDt.EditValue = null;
            txtLeftDt.EditValue = null;
            txtValidFrom.EditValue = null;
            txtValidTo.EditValue = null;

            xtraTabControl1.SelectedTabPage = xtraTabPage1;
            this.ActiveControl = txtEmpUnqID;
            

        }

        private void DisplayData()
        {

            txtEmpName.Text = cEmp.EmpName;
            txtUnitCode.Text = cEmp.UnitCode;
            txtUnitDesc.Text = cEmp.UnitDesc;
            txtWrkGrpCode.Text = cEmp.WrkGrp;
            txtWrkGrpDesc.Text = cEmp.WrkGrpDesc;
            txtDeptCode.Text = cEmp.DeptCode;
            txtDeptDesc.Text = cEmp.DeptDesc;
            txtStatCode.Text = cEmp.StatCode;
            txtStatDesc.Text = cEmp.StatDesc;
            txtGradeCode.Text = cEmp.GradeCode;
            txtGradeDesc.Text = cEmp.GradeDesc;
            txtDesgCode.Text = cEmp.DesgCode;
            txtDesgDesc.Text = cEmp.DesgDesc;
            txtCatCode.Text = cEmp.CatCode;
            txtCatDesc.Text = cEmp.CatDesc;
            txtEmpType.Text = cEmp.EmpTypeCode;
            txtEmpTypeDesc.Text = cEmp.EmpTypeDesc;

            txtShiftCode.Text = cEmp.ShiftCode;
            txtShiftDesc.Text = cEmp.ShiftDesc;
            txtContCode.Text = cEmp.ContCode;
            txtContCode1.Text = cEmp.ContCode;

            txtContDesc.Text = cEmp.ContDesc;
            txtMessCode.Text = cEmp.MessCode;
            txtMessDesc.Text = cEmp.MessDesc;
            txtMessGrpCode.Text = cEmp.MessGrpCode;
            txtMessGrpDesc.Text = cEmp.MessGrpDesc;
            txtCostCode.Text = cEmp.CostCode;
            txtCostDesc.Text = cEmp.CostDesc;

            txtEmpCode.Text = cEmp.EmpCode;
            txtOldEmpCode.Text = cEmp.OLDEmpCode;
            txtSAPID.Text = cEmp.SAPID;
            txtAdharNo.Text = cEmp.AdharNo;

            chkActive.Checked = cEmp.Active;
            chkComp.Checked = cEmp.PayrollFlg;
            chkCont.Checked = cEmp.ContFlg;

            chkAutoShift.Checked = cEmp.AutoShift;
            chkOTFlg.Checked = cEmp.OTFLG;
            chkMale.Checked = cEmp.Gender;
            IsValid = cEmp.Active;
            
            txtJoinDt.EditValue = cEmp.JoinDt;
            txtLeftDt.EditValue = cEmp.LeftDt;
            txtValidFrom.EditValue = cEmp.ValidFrom;
            txtValidTo.EditValue = cEmp.ValidTo;
            txtBirthDT.EditValue = cEmp.BirthDt;

        }

        private void txtEmpUnqID_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCompCode.Text.Trim() == "")
                return;

            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select EmpUnqID,EmpName,WrkGrp,CompCode From MastEmp Where CompCode ='" + txtCompCode.Text.Trim() + "' ";
                if (e.KeyCode == Keys.F1)
                {
                    obj = (List<string>)hlp.Show(sql, "EmpUnqID", "EmpUnqID", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                    100, 300, 400, 600, 100, 100);
                }
                else
                {
                    obj = (List<string>)hlp.Show(sql, "EmpName", "EmpName", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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

                    txtCompCode.Text = obj.ElementAt(3).ToString();
                    txtEmpUnqID.Text = obj.ElementAt(0).ToString();
                    txtEmpUnqID_Validated(sender, e);
                }
            }
        }

    }
}
