using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;



namespace Attendance.Classes
{
    public class clsEmp
    {
        private string _EmpUnqID, _EmpName, _FatherName,
            _CompCode, _CompDesc,
            _WrkGrp, _WrkGrpDesc,
            _UnitCode, _UnitDesc,
            _DeptCode, _DeptDesc,
            _StatCode, _StatDesc,
            _EmpTypeCode, _EmpTypeDesc,
            _GradeCode, _GradeDesc,
            _DesgCode, _DesgDesc,
            _CatCode, _CatDesc,
            _ContCode, _ContDesc,
            _MessGrpCode, _MessGrpDesc,
            _MessCode, _MessDesc,
            _CostCode, _CostDesc,
            _ShiftCode,_ShiftDesc,
            _SAPID,_AdharNo, _EmpCode, _OLDEmpCode,_WeekOffDay;



        private DateTime? _JoinDt, _BirthDt;
        private DateTime? _ValidFrom, _ValidTo,_LeftDt;
        private bool _IsHOD, _Active, _OTFLG, _Gender, _ContFlg, _PayrollFlg, _AutoShift, _IsNew, _MedChkFlg, _SafetyTrnFLG;


        public string EmpUnqID { get { return _EmpUnqID; } set { _EmpUnqID = value; } }
        public string EmpName { get { return _EmpName; } set { _EmpName = value; } }
        public string FatherName {get { return _FatherName; } set { _FatherName = value; }}
        
        public string CompCode {get { return _CompCode; } set { _CompCode = value; }}           
        public string CompDesc { get { return _CompDesc; } }
        
        public string WrkGrp { get { return _WrkGrp; } set { _WrkGrp = value; }}
        public string WrkGrpDesc { get { return _WrkGrpDesc; }}

        public string UnitCode { get { return _UnitCode; } set { _UnitCode = value; } }        
        public string UnitDesc { get { return _UnitDesc; }}    
        
        public string DeptCode { get { return _DeptCode; } set { _DeptCode = value; } }
        public string DeptDesc { get { return _DeptDesc; }}  

        public string StatCode { get { return _StatCode; } set { _StatCode = value; } }
        public string StatDesc { get { return _StatDesc; }}  

        public string EmpTypeCode { get { return _EmpTypeCode; } set { _EmpTypeCode = value; } }
        public string EmpTypeDesc { get { return _EmpTypeDesc; }}  

        public string GradeCode { get { return _GradeCode; } set { _GradeCode = value; } }
        public string GradeDesc { get { return _GradeDesc; }}  

        public string DesgCode { get { return _DesgCode; } set { _DesgCode = value; } }
        public string DesgDesc { get { return _DesgDesc; }}  
   
        public string CatCode { get { return _CatCode; } set { _CatCode = value; } }
        public string CatDesc { get { return _CatDesc; }}  
   
        public string ContCode { get { return _ContCode; } set { _ContCode = value; } }
        public string ContDesc { get { return _ContDesc; }}  
   
        public string MessGrpCode { get { return _MessGrpCode; } set { _MessGrpCode = value; } }
        public string MessGrpDesc { get { return _MessGrpDesc; }}  
    
        public string MessCode { get { return _MessCode; } set { _MessCode = value; } }
        public string MessDesc { get { return _MessDesc; }}  
    
        public string CostCode { get { return _CostCode; } set { _CostCode = value; } }
        public string CostDesc { get { return _CostDesc; }}  
    
        public string ShiftCode { get { return _ShiftCode; } set { _ShiftCode = value; } }
        public string ShiftDesc { get { return _ShiftDesc; }}  
    
        public string SAPID { get { return _SAPID; } set { _SAPID = value; } }
        public string AdharNo { get { return _AdharNo; } set { _AdharNo = value; } }
        public string EmpCode { get { return _EmpCode; } set { _EmpCode = value; } }   
        public string OLDEmpCode { get { return _OLDEmpCode; } set { _OLDEmpCode = value; } }   
        public string WeekOffDay { get { return _WeekOffDay; } set { _WeekOffDay = value; } }      
       

        public DateTime? JoinDt { get { return _JoinDt; } set { _JoinDt = value; } }
        public DateTime? BirthDt { get { return _BirthDt; } set { _BirthDt = value; } }
        public DateTime? ValidFrom { get { return _ValidFrom; } set { _ValidFrom = value; } }
        public DateTime? ValidTo { get { return _ValidTo; } set { _ValidTo = value; } }
        public DateTime? LeftDt { get { return _LeftDt; } set { _LeftDt = value; } }

        public bool Active { get { return _Active; } set { _Active = value; } }
        public bool OTFLG { get { return _OTFLG; } set { _OTFLG = value; } }
        public bool Gender { get { return _Gender; } set { _Gender = value; } }
        public bool ContFlg { get { return _ContFlg; } set { _ContFlg = value; } }
        public bool PayrollFlg { get { return _PayrollFlg; } set { _PayrollFlg = value; } }
        public bool AutoShift { get { return _AutoShift; } set { _AutoShift = value; } }
        public bool MedChkFlg { get { return _MedChkFlg; } set { _MedChkFlg = value; } }
        public bool SafetyTrnFLG { get { return _SafetyTrnFLG; } set { _SafetyTrnFLG = value; } }
        public bool IsHOD { get { return _IsHOD; } set { _IsHOD = value; } }


        public bool IsValid { 
            get 
            {

                if (IsNew)
                {
                    string err = this.BasicValidation();
                    if (string.IsNullOrEmpty(err))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return _Active; 
                } 
            } 
        }
        public bool IsNew { get { return _IsNew; } set { _IsNew = value; } }

        public clsEmp()
        {
            _EmpUnqID = string.Empty; _EmpName = string.Empty; _FatherName = string.Empty;
            _CompCode = string.Empty; _CompDesc = string.Empty;
            _WrkGrp = string.Empty; _WrkGrpDesc = string.Empty;
            _UnitCode = string.Empty; _UnitDesc = string.Empty;
            _DeptCode = string.Empty; _DeptDesc = string.Empty;
            _StatCode = string.Empty; _StatDesc = string.Empty;
            _EmpTypeCode = string.Empty; _EmpTypeDesc = string.Empty;
            _GradeCode = string.Empty; _GradeDesc = string.Empty;
            _DesgCode = string.Empty; _DesgDesc = string.Empty;
            _CatCode = string.Empty; _CatDesc = string.Empty;
            _ContCode = string.Empty; _ContDesc = string.Empty;
            _MessGrpCode = string.Empty; _MessGrpDesc = string.Empty;
            _MessCode = string.Empty; _MessDesc = string.Empty;
            _CostCode = string.Empty; _CostDesc = string.Empty;
            _ShiftCode = string.Empty;_ShiftDesc = string.Empty;
            _SAPID = string.Empty; _AdharNo = string.Empty; _EmpCode = string.Empty; _OLDEmpCode = string.Empty; _WeekOffDay = string.Empty;
            _IsNew = false;
            _IsHOD = false;

            DateTime? dt = new DateTime?();
            _JoinDt = dt; _BirthDt = dt;
            _ValidFrom = dt; _ValidTo = dt;_LeftDt = dt;
            _Active = false; _OTFLG = false; _Gender = false; _ContFlg = false; _PayrollFlg = false; _AutoShift = false;
            _SafetyTrnFLG = false; _MedChkFlg = false;
        }

        public bool GetEmpDetails(string tCompCode,string tEmpUnqID)
        {
            bool returnval = false;

            DataSet ds = new DataSet();
            string sql = "select * From MastEmp where CompCode ='" + tCompCode.Trim() 
                + "' and EmpUnqID ='" + tEmpUnqID.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            _CostDesc = string.Empty;
            if (hasRows)
            {

                _IsNew = true;
                _Active = false;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    IsNew = false;
                    this.CompCode = dr["CompCode"].ToString();
                    this.EmpUnqID = dr["EmpUnqID"].ToString();
                    this.EmpName = dr["EmpName"].ToString();
                    this.FatherName = dr["FatherName"].ToString();
                    this.WrkGrp = dr["WrkGrp"].ToString();
                    this.UnitCode = dr["UnitCode"].ToString();
                    this.DeptCode = dr["DeptCode"].ToString();
                    this.StatCode = dr["StatCode"].ToString();
                    this.CatCode = dr["CatCode"].ToString();
                    this.DesgCode = dr["DesgCode"].ToString();
                    this.GradeCode = dr["GradCode"].ToString();
                    this.EmpTypeCode = dr["EmpTypeCode"].ToString();
                    this.ContCode = dr["ContCode"].ToString();
                    this.MessCode = dr["MessCode"].ToString();
                    this.MessGrpCode = dr["MessGrpCode"].ToString();
                    this.CostCode = dr["CostCode"].ToString();
                    this.ShiftCode = dr["ShiftCode"].ToString();
                    
                    this.SAPID = dr["SAPID"].ToString();
                    this.EmpCode = dr["EmpCode"].ToString();
                    this.OLDEmpCode = dr["OldEmpCode"].ToString();
                    this.AdharNo = dr["AdharNo"].ToString();
                    this.WeekOffDay = dr["WeekOff"].ToString();

                    this.SafetyTrnFLG = Convert.ToBoolean(dr["SafetyTrnFLG"]);
                    this.MedChkFlg = Convert.ToBoolean(dr["MedChkFLG"]);
                    this.IsHOD = Convert.ToBoolean(dr["IsHOD"]);
                    this.Gender = Convert.ToBoolean(dr["Sex"]);
                    this.Active = Convert.ToBoolean(dr["Active"]);
                    this.OTFLG = Convert.ToBoolean(dr["OTFLG"]);
                    this.ContFlg = Convert.ToBoolean(dr["ContractFlg"]);
                    this.PayrollFlg = Convert.ToBoolean(dr["PayrollFlg"]);
                    this.AutoShift = Convert.ToBoolean(dr["ShiftType"]);

                    this.ValidFrom = (dr["ValidFrom"] != DBNull.Value) ? Convert.ToDateTime(dr["ValidFrom"]): new DateTime?();
                    this.ValidTo = (dr["ValidTo"] != DBNull.Value) ? Convert.ToDateTime(dr["ValidTo"]) : new DateTime?();

                    this.JoinDt = (dr["JoinDt"] != DBNull.Value) ? Convert.ToDateTime(dr["JoinDt"]) : new DateTime?();
                    this.BirthDt = (dr["BirthDt"] != DBNull.Value) ? Convert.ToDateTime(dr["BirthDt"]) : new DateTime?();
                    this.LeftDt = (dr["LeftDt"] != DBNull.Value) ? Convert.ToDateTime(dr["LeftDt"]) : new DateTime?();

                    SetAllDesc();
                    returnval = true;
                }
            }

            return returnval;
        }

        private void SetAllDesc()
        {
            GetCompDesc(this.CompCode);
            GetWrkDesc(this.CompCode, this.WrkGrp);
            GetUnitDesc(this.CompCode, this.WrkGrp,this.UnitCode);
            GetDeptDesc(this.CompCode, this.WrkGrp, this.UnitCode,this.DeptCode);
            GetStatDesc(this.CompCode, this.WrkGrp, this.UnitCode, this.DeptCode,this.StatCode);
            GetGradeDesc(this.CompCode, this.WrkGrp, this.GradeCode);
            GetDesgDesc(this.CompCode, this.WrkGrp, this.DesgCode);
            GetCatDesc(this.CompCode, this.WrkGrp, this.CatCode);
            GetEmpTypeDesc(this.CompCode, this.WrkGrp, this.EmpTypeCode);
            GetMessDesc(this.CompCode, this.UnitCode, this.MessCode);
            GetMessGrpDesc(this.CompCode, this.UnitCode, this.MessGrpCode);
            GetCostDesc(this.CostCode);
            GetContDesc(this.CompCode, this.WrkGrp, this.UnitCode, this.ContCode);
        }
        
        public string BasicValidation()
        {
            string err = string.Empty;

            if (string.IsNullOrEmpty(_EmpUnqID))
            {
                err += "EmpUnqID is Required...." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(_EmpName))
            {
                err += "EmpName is Required...." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(_FatherName))
            {
                err += "Father Name is Required...." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(_CompCode))
            {
                err += "CompCode is Required...." + Environment.NewLine;
            }
            else
            {
                GetCompDesc(_CompCode);

                if (string.IsNullOrEmpty(_CompDesc))
                {
                    err += "Invalid CompCode...." + Environment.NewLine;
                }
            }

            if (string.IsNullOrEmpty(_WrkGrp))
            {
                err += "WrkGrp is Required...." + Environment.NewLine;
            }
            else
            {
                GetWrkDesc(_CompCode,_WrkGrp);

                if (string.IsNullOrEmpty(_WrkGrpDesc))
                {
                    err += "Invalid WrkGrpCode...." + Environment.NewLine;
                }
            }

            if (string.IsNullOrEmpty(_UnitCode))
            {
                err += "UnitCode is Required...." + Environment.NewLine;
            }
            else
            {
                GetUnitDesc(_CompCode, _WrkGrp,_UnitCode);

                if (string.IsNullOrEmpty(_UnitDesc))
                {
                    err += "Invalid UnitCode...." + Environment.NewLine;
                }
            }



            if (_JoinDt.HasValue == false)
            {
                err += "Join Date is Required...." + Environment.NewLine;
            }
            if (_BirthDt.HasValue == false)
            {
                err += "Birth Date is Required...." + Environment.NewLine;
            }

            if (_WrkGrp != "COMP")
            {
                if (_ValidFrom.HasValue == false)
                {
                    err += "Valid From is Required...." + Environment.NewLine;
                }

                if (_ValidTo.HasValue == false)
                {
                    err += "Valid To is Required...." + Environment.NewLine;
                }

                if (_ValidFrom.HasValue && _ValidTo.HasValue)
                {
                    DateTime tFrom, tTo;

                    tFrom = _ValidFrom.Value;
                    tTo = _ValidTo.Value;

                    if (tFrom > tTo)
                    {
                        err += "ValidFrom Must be Less than Valid To ...." + Environment.NewLine;
                    }
                }
            }
            else
            {
                _ValidFrom = new DateTime?();
                _ValidTo = new DateTime?();
            }

            
            
            if (_BirthDt.HasValue && _JoinDt.HasValue)
            {
                DateTime tJoin, tBirth;

                tJoin = _JoinDt.Value;
                tBirth = _BirthDt.Value;

                if (tJoin < tBirth)
                {
                    err += "Join Date Must be Greator than Birth Date ...." + Environment.NewLine;
                }
            }


            if (string.IsNullOrEmpty(_AdharNo))
            {
                err += "Adhar No is Required...." + Environment.NewLine;
            }


            if (_AdharNo.Trim().Length < 12)
            {
                err += "Please Enter 12 digit Adhar No" + Environment.NewLine;
            }


            //check for duplicate adharno..
            DataSet ds = new DataSet();
            string sql = "select EmpUnqID,EmpName from MastEmp where CompCode ='" + this.CompCode.Trim() + "' " +
                " and AdharNo = '" + this.AdharNo.Trim() + "' and EmpUnqID not in ('" + this.EmpUnqID.Trim() + "')";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    err += "duplicate Adhar No with : " + dr["EmpUnqID"].ToString() + "," + dr["EmpName"].ToString() + Environment.NewLine;
                }
            }


            return err;
        }

        private void GetCompDesc(string tCompCode)
        {
            DataSet ds = new DataSet();
            string sql = "select * From MastComp where CompCode ='" + tCompCode.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            _CompDesc = string.Empty;
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this._CompDesc = dr["CompName"].ToString();

                }
            }
        }
               
        private void GetWrkDesc(string tCompCode, string tWrkGrp)
        {
            DataSet ds = new DataSet();
            string sql = "select * From MastWorkGrp where CompCode ='" + tCompCode.Trim() + "' and WrkGrp='" + tWrkGrp.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);
            
            _WrkGrpDesc = string.Empty;
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {   
                    this._WrkGrpDesc = dr["WrkGrpDesc"].ToString();
                    
                }
            }
        }

        private void GetUnitDesc(string tCompCode, string tWrkGrp,string tUnitCode)
        {
            DataSet ds = new DataSet();
            string sql = "select * From MastUnit where CompCode ='" + tCompCode.Trim() + "' and WrkGrp='" + tWrkGrp.Trim() + "' and UnitCode = '" + tUnitCode.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            _UnitDesc = string.Empty;
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this._UnitDesc = dr["UnitName"].ToString();

                }
            }
        }

        private void GetDeptDesc(string tCompCode, string tWrkGrp,string tUnitCode,string tDeptCode)
        {
            DataSet ds = new DataSet();
            string sql = "select * From MastDept where CompCode ='" + tCompCode.Trim() 
                + "' and WrkGrp='" + tWrkGrp.Trim() + "' and UnitCode = '" + tUnitCode.Trim() 
                + "' and DeptCode='" + tDeptCode.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            _DeptDesc = string.Empty;
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this._DeptDesc = dr["DeptDesc"].ToString();

                }
            }
        }

        private void GetStatDesc(string tCompCode, string tWrkGrp, string tUnitCode, string tDeptCode, string tStatCode)
        {

            DataSet ds = new DataSet();
            string sql = "select * From MastStat where CompCode ='" + tCompCode.Trim()
                + "' and WrkGrp='" + tWrkGrp.Trim() + "' and UnitCode = '" + tUnitCode.Trim()
                + "' and DeptCode='" + tDeptCode.Trim() 
                + "' and StatCode='" + tStatCode.Trim() + "'" ;

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            _StatDesc = string.Empty;
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this._StatDesc = dr["StatDesc"].ToString();

                }
            }
        
        }

        private void GetGradeDesc(string tCompCode, string tWrkGrp, string tGradeCode)
        {
            DataSet ds = new DataSet();
            string sql = "select * From MastGrade where CompCode ='" + tCompCode.Trim() + "' and WrkGrp='" + tWrkGrp.Trim()
                + "' and GradeCode = '" + tGradeCode.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            _GradeDesc = string.Empty;
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this._GradeDesc = dr["GradeDesc"].ToString();

                }
            }
        
        }

        private void GetDesgDesc(string tCompCode, string tWrkGrp, string tDesgCode)
        {
            DataSet ds = new DataSet();
            string sql = "select * From MastDesg where CompCode ='" + tCompCode.Trim() + "' and WrkGrp='" + tWrkGrp.Trim()
                + "' and DesgCode = '" + tDesgCode.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            _DesgDesc = string.Empty;
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this._DesgDesc = dr["DesgDesc"].ToString();

                }
            }
        
        
        }

        private void GetCatDesc(string tCompCode, string tWrkGrp, string tCatCode)
        {

            DataSet ds = new DataSet();
            string sql = "select * From MastCat where CompCode ='" + tCompCode.Trim() + "' and WrkGrp='" + tWrkGrp.Trim()
                + "' and CatCode = '" + tCatCode.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            _CatDesc = string.Empty;
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this._CatDesc = dr["CatDesc"].ToString();

                }
            }
        
        }

        private void GetEmpTypeDesc(string tCompCode, string tWrkGrp, string tEmpTypeCode)
        {
            DataSet ds = new DataSet();
            string sql = "select * From MastEmpType where CompCode ='" + tCompCode.Trim() + "' and WrkGrp='" + tWrkGrp.Trim()
                + "' and EmpTypeCode = '" + tEmpTypeCode.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            _EmpTypeDesc = string.Empty;
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this._EmpTypeDesc = dr["EmpTypeDesc"].ToString();

                }
            }
        
        
        }

        private void GetMessDesc(string tCompCode, string tUnitCode, string tMessCode)
        {
            DataSet ds = new DataSet();
            string sql = "select * From MastMess where CompCode ='" + tCompCode.Trim() + "' and UnitCode='" + tUnitCode.Trim()
                + "' and MessCode = '" + tMessCode.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            _MessDesc = string.Empty;
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this._MessDesc = dr["MessDesc"].ToString();

                }
            }
        
        }

        private void GetMessGrpDesc(string tCompCode, string tUnitCode, string tMessGrpCode)
        {
            DataSet ds = new DataSet();
            string sql = "select * From MastMessGrp where CompCode ='" + tCompCode.Trim() + "' and UnitCode='" + tUnitCode.Trim()
                + "' and MessGrpCode = '" + tMessGrpCode.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            _MessGrpDesc = string.Empty;
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this._MessGrpDesc = dr["MessGrpDesc"].ToString();

                }
            }
        
        }
        
        public void GetCostDesc(string tCostCode)
        {

            DataSet ds = new DataSet();
            string sql = "select * From MastCostCode where CostCode ='" + tCostCode.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            _CostDesc = string.Empty;
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this._CostDesc = dr["CostDesc"].ToString();

                }
            }
        
        }

        private void GetContDesc(string tCompCode, string tWrkGrp, string tUnitCode, string tContCode)
        {
            DataSet ds = new DataSet();
            string sql = "select * From MastCont where CompCode ='" + tCompCode.Trim()
                + "' and WrkGrp='" + tWrkGrp.Trim() + "' and UnitCode = '" + tUnitCode.Trim()
                + "' and ContCode='" + tContCode.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            _ContDesc = string.Empty;
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this._ContDesc = dr["ContName"].ToString();

                }
            }
        }


        public bool CreateMuster(DateTime tFromDt, DateTime tToDt, out string err)
        {
            bool returnval = false;
            //save in db for accountibility
            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CreateMuster";

                    int result = 0;

                    ////Creating instance of SqlParameter
                    SqlParameter sPfdate = new SqlParameter();
                    sPfdate.ParameterName = "@pFromDt";// Defining Name
                    sPfdate.SqlDbType = SqlDbType.DateTime; // Defining DataType
                    sPfdate.Direction = ParameterDirection.Input;// Setting the direction
                    sPfdate.Value = tFromDt;

                    ////Creating instance of SqlParameter
                    SqlParameter sPtdate = new SqlParameter();
                    sPtdate.ParameterName = "@pToDt";// Defining Name
                    sPtdate.SqlDbType = SqlDbType.DateTime; // Defining DataType
                    sPtdate.Direction = ParameterDirection.Input;// Setting the direction
                    sPtdate.Value = tToDt;

                    ////Creating instance of SqlParameter
                    SqlParameter sPEmpUnqID = new SqlParameter();
                    sPEmpUnqID.ParameterName = "@pEmpUnqID";// Defining Name
                    sPEmpUnqID.SqlDbType = SqlDbType.VarChar; // Defining DataType
                    sPEmpUnqID.Size = 10;
                    sPEmpUnqID.Direction = ParameterDirection.Input;// Setting the direction
                    sPEmpUnqID.Value = this.EmpUnqID;

                    ////Creating instance of SqlParameter
                    SqlParameter sPWoDay = new SqlParameter();
                    sPWoDay.ParameterName = "@pWoDay";// Defining Name
                    sPWoDay.SqlDbType = SqlDbType.VarChar; // Defining DataType
                    sPWoDay.Size = 3;
                    sPWoDay.Direction = ParameterDirection.Input;// Setting the direction
                    sPWoDay.Value = this.WeekOffDay;

                    ////Creating instance of SqlParameter
                    SqlParameter sPWrkGrp = new SqlParameter();
                    sPWrkGrp.ParameterName = "@pWrkGrp";// Defining Name
                    sPWrkGrp.SqlDbType = SqlDbType.VarChar; // Defining DataType
                    sPWrkGrp.Size = 10;
                    sPWrkGrp.Direction = ParameterDirection.Input;// Setting the direction
                    sPWrkGrp.Value = "";

                    ////Creating instance of SqlParameter
                    SqlParameter sPresult = new SqlParameter();
                    sPresult.ParameterName = "@result"; // Defining Name
                    sPresult.SqlDbType = SqlDbType.Int; // Defining DataType
                    sPresult.Direction = ParameterDirection.Output;// Setting the direction 
                    sPresult.Value = result;

                    cmd.Parameters.Add(sPWrkGrp);
                    cmd.Parameters.Add(sPEmpUnqID);
                    cmd.Parameters.Add(sPfdate);
                    cmd.Parameters.Add(sPtdate);
                    cmd.Parameters.Add(sPWoDay);
                    cmd.Parameters.Add(sPresult);

                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();
                    //get the output
                    int t = (int)cmd.Parameters["@result"].Value;

                    err = string.Empty;
                    returnval= true;
                }
                catch (Exception ex)
                {
                    err = ex.ToString();
                    returnval = false;
                }

            }//using connection

            return returnval;
        }

        public bool CreateEmployee(string tEmpUnqID, string tWrkGrp,
            string tUnitCode, string tEmpName, string tFatherName , 
                bool tSex , bool tActive,DateTime tBirthDt, DateTime tJoinDt,
                    string tWeekoff , bool tPayrollFLG, bool tContractFlg, 
                        bool tShiftType, bool tOTFLG ,bool tMedChkFlg ,
                             bool  tSafteyFlg, string tEmpCode, string tContCode, 
                                string tEmpTypeCode, string tCATCODE, string tDeptcode, string tStatCode , 
                                    string tDesgCode, string tGradeCode , string tMessGrpCode, string tMessCode, 
                                        string tOldEmpCode, string tSAPID, string tCostCode ,string tAdharNo, 
                                            DateTime? tValidFrom , DateTime? tValidTo, double cbasic, out string err)
        {
            bool retval = false;
            err = "";

            //check if already exist
            this.CompCode = "01";
            this.EmpUnqID = tEmpUnqID;
            if (this.GetEmpDetails(this.CompCode, this.EmpUnqID))
            {
                err = "Employee Already Exist...";
                retval = false;
                return retval;

            }
            err = string.Empty;

            this.EmpName = tEmpName;
            this.FatherName = tFatherName;
            this.BirthDt = tBirthDt;
            this.JoinDt = tJoinDt;
            this.WrkGrp = tWrkGrp;
            this.UnitCode = tUnitCode;
            this.AdharNo = tAdharNo;
            this.ValidFrom = tValidFrom;
            this.ValidTo = tValidTo;
            

            err = this.BasicValidation();
            if (!string.IsNullOrEmpty(err))
            {
                retval = false;
                return retval;
            }
            
            this.Gender = tSex;
            this.Active = tActive;
            this.WeekOffDay = tWeekoff;
            this.PayrollFlg = tPayrollFLG;
            this.ContFlg = tContractFlg;
            this.AutoShift = tShiftType;
            this.OTFLG = tOTFLG;
            this.EmpCode = tEmpCode;
            this.OLDEmpCode = tOldEmpCode;
            this.ContCode = tContCode;
            this.SAPID = tSAPID;
            this.EmpTypeCode = tEmpTypeCode;
            this.CatCode = tCATCODE;
            this.DeptCode = tDeptcode;
            this.StatCode = tStatCode;
            this.GradeCode = tGradeCode;
            this.DesgCode = tDesgCode;
            this.MessCode = tMessCode;
            this.MessGrpCode = tMessGrpCode;
            this.CostCode = tCostCode;
           

            //check for CostCode ..
            if(this.CostCode.Trim() != "")
            {
                string tsql1 = "select CostCode from MastCostCode where CostCode ='" + this.CostCode + "' " ;
                string t3 = Utils.Helper.GetDescription(tsql1,Utils.Helper.constr);
                if(string.IsNullOrEmpty(t3)){
                    err += "Invalid CostCode.." + Environment.NewLine;
                    retval = false;
                    return retval;
                }            
            }
            
            if (this.ContFlg && this.PayrollFlg)
            {
                err += "Please Enter Either Onroll Employee OR Contractual Employee.." + Environment.NewLine;
                retval = false;
                return retval;
            }

            if (this.PayrollFlg && this.CostCode.Trim() == "")
            {
                err += "Please Enter CostCode.." + Environment.NewLine;
                retval = false;
                return retval;
            }


            string weekoff = "SUN,MON,TUE,WED,THU,FRI,SAT";
            if (!weekoff.Contains(this.WeekOffDay))
            {
                err += "Invalid Weekoff Days.." + Environment.NewLine;
                retval = false;
                return retval;
            }

            if (this.ContFlg && this.CostCode.Trim() == "")
            {
                err += "Please Enter CostCode.." + Environment.NewLine;
                retval = false;
                return retval;
            }

            if (!this.Active)
            {
                err += "System Only Allow Active Employee to upload.." + Environment.NewLine;
                retval = false;
                return retval;
            }
            
            //nullify wrong values.

            this.GetDeptDesc(this.CompCode, this.WrkGrp, this.UnitCode,this.DeptCode);
            if (this.DeptDesc.Trim() == "")
            {
                err += "Invalid DeptCode.." + Environment.NewLine;
                this.DeptCode = "";
            }

            this.GetStatDesc(this.CompCode, this.WrkGrp, this.UnitCode, this.DeptCode,this.StatCode);
            if (this.StatDesc.Trim() == "")
            {
                err += "Invalid Station/Section Code.." + Environment.NewLine;
                this.StatCode = "";
            }

            this.GetDesgDesc(this.CompCode, this.WrkGrp, this.DesgCode);
            if (this.DesgDesc.Trim() == "")
            {
                err += "Invalid DesgCode.." + Environment.NewLine;
                this.DesgCode = "";
            }

            this.GetGradeDesc(this.CompCode, this.WrkGrp, this.GradeCode);
            if (this.GradeDesc.Trim() == "")
            {
                err += "Invalid Grade Code.." + Environment.NewLine;
                this.GradeCode = "";
            }

            this.GetEmpTypeDesc(this.CompCode, this.WrkGrp, this.EmpTypeCode);
            if (this.EmpTypeDesc.Trim() == "")
            {
                err += "Invalid Emp Type Code.." + Environment.NewLine;
                this.EmpTypeCode = "";
            }

            this.GetCatDesc(this.CompCode, this.WrkGrp, this.CatCode);
            if (this.CatDesc.Trim() == "")
            {
                err += "Invalid Emp Cat Code.." + Environment.NewLine;
                this.CatCode = "";
            }

            this.GetMessDesc(this.CompCode, this.UnitCode, this.MessCode);
            if (this.MessDesc.Trim() == "")
            {
                err += "Invalid Mess Code..."  + Environment.NewLine;
                this.MessCode = "";
            }


            this.GetMessGrpDesc(this.CompCode, this.UnitCode, this.MessGrpCode);
            if (this.MessGrpDesc.Trim() == "")
            {
                err += "Invalid MessGrp Code..." + Environment.NewLine;
                this.MessGrpCode = "";
            }

            if (this.PayrollFlg && this.ContCode != "")
            {
                this.ContCode = "";
            }

            if (this.WrkGrp == "COMP")
            {
                this.ValidFrom = new DateTime?();
                this.ValidTo = new DateTime?();
            }

            this.MedChkFlg = tMedChkFlg;
            this.SafetyTrnFLG = tSafteyFlg;
            this.AutoShift = tShiftType;
            this.CostCode = tCostCode;
            //
            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    cn.Open();
                    SqlTransaction tr = cn.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.Transaction = tr;
                        cmd.CommandType = CommandType.Text;


                        string sql = "Insert into MastEmp (CompCode,WrkGrp,EmpUnqID,EmpName,FatherName," +
                            " UnitCode,MessCode,MessGrpCode,BirthDt,JoinDt,ValidFrom,ValidTo," +
                            " ADHARNO,IDPRF3,IDPRF3No,Sex,ContractFlg,PayrollFlg,OTFLG,Weekoff,Active," +
                            " ContCode,EmpCode,OldEmpCode,SAPID," +
                            " EmpTypeCode,DeptCode,StatCode,DesgCode,GradCode,CatCode, " +
                            " ShiftType,MedChkFlg,SafetyTrnFLG,ShiftCode,CostCode, " +                            
                            " AddDt,AddID,isHod,Basic) Values (" +
                            "'{0}','{1}','{2}','{3}','{4}' ," +
                            " '{5}',{6},{7},'{8}','{9}',{10},{11}," +
                            " '{12}','ADHARCARD','{13}','{14}','{15}','{16}','{17}','{18}','1'," +
                            " {19},'{20}','{21}','{22}'," +
                            " {23},{24},{25},{26},{27},{28},{29}," +
                            " '{30}','{31}',{32}, " +
                            " '{33}',GetDate(),'{34}',0,'{35}')";

                        sql = string.Format(sql, this.CompCode, this.WrkGrp, this.EmpUnqID, this.EmpName, this.FatherName,
                            this.UnitCode, ((this.MessCode.Trim() == "") ? "null" : "'" + this.MessCode.Trim() + "'"),
                            ((this.MessGrpCode.Trim() == "") ? "null" : "'" + this.MessGrpCode + "'"),
                            Convert.ToDateTime(this.BirthDt).ToString("yyyy-MM-dd"), Convert.ToDateTime(this.JoinDt).ToString("yyyy-MM-dd"),
                           ((this.WrkGrp.Trim() == "COMP") ? "null" : "'" + Convert.ToDateTime(this.ValidFrom).ToString("yyyy-MM-dd") + "'"),
                            ((this.WrkGrp.Trim() == "COMP") ? "null" : "'" + Convert.ToDateTime(this.ValidTo).ToString("yyyy-MM-dd") + "'"),
                            this.AdharNo, this.AdharNo, (this.Gender?1:0),
                             (this.ContFlg?1:0), (this.PayrollFlg?1:0), (this.OTFLG?1:0), this.WeekOffDay,
                            (this.ContCode == "" ? "null" : "'" + this.ContCode + "'"), this.EmpCode, this.OLDEmpCode, this.SAPID,
                            (this.EmpTypeCode == "" ? "null" : "'" + this.EmpTypeCode + "'"), 
                            (this.DeptCode == "" ? "null" : "'" + this.DeptCode + "'"),
                            (this.StatCode == "" ? "null" : "'" + this.StatCode + "'"), 
                            (this.DesgCode == "" ? "null" : "'" + this.DesgCode + "'"), 
                            (this.GradeCode == "" ? "null" : "'" + this.GradeCode + "'"), 
                            (this.CatCode == "" ? "null" : "'" + this.CatCode + "'"),
                            (this.AutoShift?1:0), (this.MedChkFlg?1:0),(this.SafetyTrnFLG?1:0),(this.AutoShift? "null": "'" + this.ShiftCode+"'"),
                            this.CostCode,Utils.User.GUserID,cbasic);

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        retval = true;
                        tr.Commit();

                        try
                        {
                            //createmuster
                            clsEmp t = new clsEmp();
                            string err2 = string.Empty;
                            if (t.GetEmpDetails(this.CompCode.Trim(), this.EmpUnqID.Trim()))
                            {
                                DateTime sFromDt, sToDt, sCurDt;
                                sCurDt = Convert.ToDateTime(Utils.Helper.GetDescription("Select GetDate()", Utils.Helper.constr));
                                if (Convert.ToDateTime(this.JoinDt).Year < sCurDt.Year)
                                {
                                    sFromDt = Convert.ToDateTime(Utils.Helper.GetDescription("Select CalendarStartOfYearDate from dbo.F_TABLE_DATE(GetDate(),GetDate())", Utils.Helper.constr));
                                    sToDt = Convert.ToDateTime(Utils.Helper.GetDescription("Select CalendarEndOfYearDate from dbo.F_TABLE_DATE(GetDate(),GetDate())", Utils.Helper.constr));
                                }
                                else
                                {
                                    sFromDt = Convert.ToDateTime(this.JoinDt);
                                    sToDt = Convert.ToDateTime(Utils.Helper.GetDescription("Select CalendarEndOfYearDate from dbo.F_TABLE_DATE('" + sFromDt.ToString("yyyy-MM-dd") + "','" + sFromDt.ToString("yyyy-MM-dd") + "')", Utils.Helper.constr));
                                }


                                if (!t.CreateMuster(sFromDt, sToDt, out err2))
                                {
                                    err += "Error While Creating Muster Table : " + err2;
                                }

                            }

                            //-added on 04-10-2017 for costcenter wise manpower calculation
                            if(!string.IsNullOrEmpty(this.CostCode)) 
                            {


                                cmd.CommandText = "Insert into MastCostCodeEmp (EmpUnqID,ValidFrom,CostCode,AddDt,AddID) Values ('" + this.EmpUnqID + "','" + Convert.ToDateTime(this.JoinDt).ToString("yyyy-MM-dd") + "','" + this.CostCode + "',GetDate(),'" + Utils.User.GUserID + "')";
                                

                                cmd.CommandText = "Update AttdData Set CostCode ='" + this.CostCode + "' Where EmpUnqID = '" + this.EmpUnqID + "' and CompCode = '" + this.CompCode + "' and tDate >= '" + Convert.ToDateTime(this.JoinDt).ToString("yyyy-MM-dd") + "' ";
                                cmd.ExecuteNonQuery();
                                


                            }
                            


                        }
                        catch (Exception ex)
                        {
                            err += "Error While Creating Muster Table :" + ex.ToString();
                        }
                        


                    }

                }catch(Exception ex)
                {
                    err += ex.ToString();
                }
                
            }

            if (retval && !string.IsNullOrEmpty(err))
            {
                string err2 = "Employee Created.. With Errors : Please check->" + err;
                err = err2;
            }


            return retval;
        }

    }
}
