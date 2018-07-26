using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Data.OleDb;
using System.Net.Mail;

namespace Attendance.Classes
{
    
    
    class Globals
    {

        public static string MasterMachineIP = string.Empty;
        
        //used for scheduling jobs
        public static Scheduler G_myscheduler;
        public static DataSet G_DsMachine;
        public static DataSet G_DsAutoTime;
        public static DataSet G_DsAutoLog;
        public static DataSet G_DsAutoArrival;
        
        //used for checkpath
        public static string G_UpdateChkPath;

        public static List<string> GateInOutIP = new List<string>();
        public static string G_GateInOutIP;

        public static List<string> LunchInOutIP = new List<string>();
        public static string G_LunchInOutIP;
        
        public static bool G_AutoProcess;
        public static string G_AutoProcessWrkGrp;
        public static TimeSpan G_AutoProcessTime;
        public static string G_ReportServiceURL;
        public static string G_ReportSerExeUrl;
        public static string G_DefaultMailID;
        public static string G_SmtpHostIP;
        public static string G_ServerWorkerIP;

        public static string G_NetworkDomain;
        public static string G_NetworkUser;
        public static string G_NetworkPass;

        //auto delete left employee
        public static bool G_AutoDelEmp = false;
        public static TimeSpan G_AutoDelEmpTime;

        // auto delete validity expired employee
        public static bool G_AutoDelExpEmp = false;
        public static TimeSpan G_AutoDelExpEmpTime;

        public static bool G_JobNotificationFlg = false;
        public static string G_JobNotificationEmail;

        public static int G_SanDayLimit;
        public static int G_LateComeSec;
        public static int G_EarlyComeSec;
        public static int G_EarlyGoingSec;
        public static int G_GracePeriodSec;
        
        public static bool G_HFFLG_Grace;
        public static bool G_HFFLG_EarlyGoing;
        public static bool G_HFFLG_LateCome;
        public static int G_HFSEC_EarlyGoing;
        public static int G_HFSEC_LateCome;


        public static bool GetGlobalVars()
        {
            bool tset = false;

            DataSet ds = new DataSet();

            string sql = "Select Top 1 * from MastBCFlg ";
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {                
                    G_SanDayLimit = Convert.ToInt32(dr["SanDayLimit"].ToString());
                    G_LateComeSec = Convert.ToInt32(dr["LateComeSec"].ToString());
                    G_EarlyComeSec = Convert.ToInt32(dr["EarlyComeSec"].ToString());
                    G_EarlyGoingSec = Convert.ToInt32(dr["EarlyGoingSec"].ToString());
                    
                    G_GracePeriodSec = Convert.ToInt32(dr["GracePeriodSec"].ToString());
                    G_HFFLG_Grace = Convert.ToBoolean(dr["GraceHalfDayFlg"]);

                    G_HFFLG_LateCome = Convert.ToBoolean(dr["LateHalfDayFlg"]);
                    G_HFFLG_EarlyGoing = Convert.ToBoolean(dr["EarlyGoingHalfDayFlg"]);
                    G_HFSEC_EarlyGoing = Convert.ToInt32(dr["EarlyGoingHalfDaySec"].ToString());
                    G_HFSEC_LateCome = Convert.ToInt32(dr["LateHalfDaySec"].ToString());

                }
            }

            sql = "Select top 1 * From MastNetwork ";
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                tset = true;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if(dr["AutoProcessWrkGrp"].ToString() != string.Empty)
                    {
                        G_AutoProcessWrkGrp = dr["AutoProcessWrkGrp"].ToString().Replace(",", "','");
                        G_AutoProcessWrkGrp = "'" + G_AutoProcessWrkGrp + "'";
                    }


                    //auto process time
                    G_AutoProcess = Convert.ToBoolean(dr["AutoProcessFlg"]);
                    if (G_AutoProcess)
                    {
                        TimeSpan t = new TimeSpan();
                        if (dr["AutoProcessTime"] != DBNull.Value)
                        {
                            TimeSpan.TryParse(dr["AutoProcessTime"].ToString(), out t);
                            G_AutoProcessTime = t;
                        }
                    }
                    

                    //auto delete left employee
                    G_AutoDelEmp = Convert.ToBoolean(dr["AutoDelEmpFlg"]);
                    if (G_AutoDelEmp)
                    {
                        TimeSpan t = new TimeSpan();
                        if(TimeSpan.TryParse(dr["AutoDelEmpTime"].ToString(),out t))
                        {
                            G_AutoDelEmpTime = t;
                        }                        
                    }


                    //auto delete validity expire emp.
                    G_AutoDelExpEmp = Convert.ToBoolean(dr["AutoDelEmpExpireValFlg"]);
                    if (G_AutoDelExpEmp)
                    {
                        TimeSpan t = new TimeSpan();
                        if (TimeSpan.TryParse(dr["AutoDelEmpExpireTime"].ToString(), out t))
                        {
                            G_AutoDelExpEmpTime = t;
                        }
                    }

                    //auto notification of schedule job completion
                    G_JobNotificationFlg = Convert.ToBoolean(dr["JobNotificationFlg"]);
                    if (G_JobNotificationFlg)
                    {
                        G_JobNotificationEmail = dr["JobNotificationEmail"].ToString();
                    }


                    G_UpdateChkPath = dr["UpdateChkPath"].ToString();
                    G_ReportServiceURL = dr["ReportServiceURL"].ToString();
                    G_ReportSerExeUrl = dr["ReportSerExeURL"].ToString();
                    G_DefaultMailID = dr["DefaultMailID"].ToString();
                    G_SmtpHostIP = dr["SmtpHostIP"].ToString();
                    G_ServerWorkerIP = dr["ServerWorkerIP"].ToString();                    
                    G_NetworkDomain = dr["NetworkDomain"].ToString();
                    G_NetworkUser = dr["NetworkUser"].ToString();
                    G_NetworkPass = dr["NetworkPass"].ToString();
                    
                    Utils.DomainUserConfig.DomainName = dr["NetworkDomain"].ToString();
                    Utils.DomainUserConfig.DomainUser = dr["NetworkUser"].ToString();
                    Utils.DomainUserConfig.DomainPassword = dr["NetworkPass"].ToString();

                }


                
                
            }
            else
            {
                tset = false ;
            }

            //load all dataset
            G_DsMachine = Utils.Helper.GetData("Select * From ReaderConfig Where Master = 0 and DelFlg = 0", Utils.Helper.constr);
            G_DsAutoTime = Utils.Helper.GetData("Select * From AutoTimeSet Where 1 = 1 Order By SchTime ", Utils.Helper.constr);
            G_DsAutoLog = Utils.Helper.GetData("Select * From AutoTimeLog Where 1 = 1 Order By SchTime ", Utils.Helper.constr);
            G_DsAutoArrival = Utils.Helper.GetData("Select * From AutoTimeArrival Where 1 = 1 Order By SchTime ", Utils.Helper.constr);


            //sql = "Select * From AutoTimeSet";
            //ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            //hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
            //if (hasRows)
            //{
            //    G_SchAutoTimeSet = new List<string>();
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        G_SchAutoTimeSet.Add(dr["SchTime"].ToString());

            //    }
            //}

            MasterMachineIP = MasterMachineIP = Utils.Helper.GetDescription("Select MachineIP From ReaderConfig Where master = 1", Utils.Helper.constr);

            return tset;


        }


        public static List<string> WaterIP = new List<string>();
        public static string G_WaterIP;

        public static List<ShiftData> ShiftList = new List<ShiftData>();
        public static string G_ShiftList;

        public static DataTable dtShift;

        public static void SetGateInOutIPList()
        {
            //Setting GateInOut IP
            DataSet ds = new DataSet();
            string sql = "Select MachineIP From ReaderConfig where GateInOut = 1 Order By MachineIP";
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);


            G_GateInOutIP = string.Empty;

            if (hasRows)
            {
                GateInOutIP.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GateInOutIP.Add(dr["MachineIP"].ToString());
                    G_GateInOutIP = G_GateInOutIP + dr["MachineIP"].ToString() + ",";
                }

            }
        }

        public static void SetLunchInOutIPList()
        {
            //Setting LunchInOut IP
            DataSet ds = new DataSet();
            string sql = "Select MachineIP From ReaderConfig where LunchInOut = 1 Order By MachineIP";
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            G_LunchInOutIP = string.Empty;
            if (hasRows)
            {
                LunchInOutIP.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    LunchInOutIP.Add(dr["MachineIP"].ToString());
                    G_LunchInOutIP = G_LunchInOutIP + dr["MachineIP"].ToString() + ",";
                }

            }
        }

        

        public static void SetWaterIPList()
        {
            //Setting WaterIP
            DataSet ds = new DataSet();
            string sql = "Select MachineIP From ReaderConfig where MachineDesc like '%Water%' Order By MachineIP";
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            G_WaterIP = string.Empty;

            if (hasRows)
            {
                WaterIP.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    WaterIP.Add(dr["MachineIP"].ToString());
                    G_WaterIP = G_WaterIP  + dr["MachineIP"].ToString() + ",";
                }

            }
        }

        public static void SetShiftList()
        {
            //Setting WaterIP
            DataSet ds = new DataSet();
            string sql = "Select * From MastShift where CompCode = '01' Order By ShiftSeq";
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            G_ShiftList = string.Empty;
            if (hasRows)
            {
                ShiftList.Clear();
                dtShift = ds.Tables[0].Copy();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ShiftData t = new ShiftData();
                    t.ShiftCode = dr["ShiftCode"].ToString();
                    t.ShiftDesc = dr["ShiftDesc"].ToString();

                    t.ShiftStart = (TimeSpan)dr["ShiftStart"];
                    t.ShiftEnd = (TimeSpan)dr["ShiftEnd"];                 
                    t.ShiftInFrom = (TimeSpan)dr["ShiftInFrom"];                   
                    t.ShiftInTo = (TimeSpan)dr["ShiftInTo"];                   
                    t.ShiftOutFrom = (TimeSpan)dr["ShiftOutFrom"];                   
                    t.ShiftOutTo = (TimeSpan)dr["ShiftOutTo"];

                    t.BreakHrs = Convert.ToInt32(dr["BreakHrs"]);
                    t.ShiftHrs = Convert.ToInt32(dr["ShiftHrs"]);

                    ShiftList.Add(t);
                    G_ShiftList = G_ShiftList + dr["ShiftCode"].ToString() + ",";
                }

            }
        }

        public static DateTime GetSystemDateTime()
        {
            DateTime dt = new DateTime();

            DataSet ds = new DataSet();
            string sql = "Select GetDate() as CurrentDate ";
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            dt = DateTime.Now;
            if (hasRows)
            {
               

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dt = (DateTime)dr["CurrentDate"];
                }

            }
            return dt;
        }

        public static string GetFormRights(string FormName1)
        {
            string frmrights = "XXXV";
            int FormID = 0;

             //Setting WaterIP
            DataSet ds = new DataSet();
            string sql = "select FormId from MastFrm where FormName ='" + FormName1.Trim() + "'";
            
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    FormID = Convert.ToInt32(dr["FormID"]);
                }
            }

            sql = "Select top 1 * From UserRights where FormId = '" + FormID.ToString() + "' and Userid = '" + Utils.User.GUserID + "'";
            ds = new DataSet();
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    frmrights = ((Convert.ToBoolean(dr["Add1"])) ? "A" : "X")
                        + ((Convert.ToBoolean(dr["UpDate1"])) ? "U" : "X")
                        + ((Convert.ToBoolean(dr["Delete1"])) ? "D" : "X")
                        + ((Convert.ToBoolean(dr["View1"])) ? "V" : "X");

                }
            }

            return frmrights;
        }

        public static bool GetWrkGrpRights(int Formid, string WrkGrp, string EmpUnqID)
        {
            bool returnval = false;

            DataSet ds = new DataSet();

            if ( EmpUnqID != "")
            {
                WrkGrp = Utils.Helper.GetDescription("Select WrkGrp From MastEmp Where EmpUnqID ='" + EmpUnqID + "'", Utils.Helper.constr);
            }
            
            if (WrkGrp == "" && EmpUnqID == "")
            {
                return false;
            }
            

            string wkgsql = "Select * from UserSpRight where UserID = '" + Utils.User.GUserID + "' and FormID = '" + Formid.ToString() + "' and WrkGrp = '" + WrkGrp + "' and Active = 1";


            ds = Utils.Helper.GetData(wkgsql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                returnval = true;
            }
            else
            {
                returnval = false;
            }

            return returnval;
        }


        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

    }

    class ShiftData
    {
        public string ShiftCode;
        public int ShiftSeq;
        public string ShiftDesc;
        public TimeSpan ShiftStart;
        public TimeSpan ShiftEnd;
        public TimeSpan ShiftInFrom;
        public TimeSpan ShiftInTo;
        
        public TimeSpan ShiftOutFrom;
        public TimeSpan ShiftOutTo;
        public bool NightFLG;
        public double BreakHrs;
        public double ShiftHrs;

    }

    [Serializable]
    public class ServerMsg
    {
        public DateTime MsgTime;
        public string MsgType;
        public string Message;

        public override string ToString()
        {
            return MsgTime.ToString("yyyy-MM-dd HH:mm:ss") + " : " + MsgType + " - " + Message + Environment.NewLine; 
        }
    } 

    public class UserBioInfo
    {
        public string UserID, UserName, WrkGrp, Password, CardNumber, FaceTemp, FingerTemp, MessCode, MessGrpCode, err;
        
        public long Previlege;
        public bool Enabled;
        public long VerifyStyle;
        public int FaceLength = 0;
        public int FaceIndex = 50 ;
        public int FingerIndex = 0;
        public int FingerLength = 0;
        
        public UserBioInfo()
        {
            UserID = "";
            UserName = "";
            WrkGrp = "";
            Password = "";
            Previlege = 0;
            Enabled = false;
            VerifyStyle = 0;
            FaceTemp = "";
            FingerTemp = "";
            FaceLength = 0;
            FingerLength = 0;
            MessCode = "";
            MessGrpCode = "";
            err = "";
        }

        public void GetBioInfoFromDB(string tEmpUnqID){

            if (tEmpUnqID == string.Empty)
            {
                this.err = "User ID is required...";
                return;
            }

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    cn.Open();
                }
                catch (Exception ex)
                {
                    this.err = ex.ToString();
                    return;
                }

                try
                {
                    string sql = "Select EmpUnqID,EmpName,WrkGrp,MessCode,MessGrpCode from MastEmp Where EmpUnqId ='" + tEmpUnqID + "'";
                    DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
                    bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

                    if (hasRows)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            this.WrkGrp = dr["WrkGrp"].ToString();
                            this.MessCode = dr["MessCode"].ToString();
                            this.MessGrpCode = dr["MessGrpCode"].ToString();
                            this.UserID = dr["EmpUnqID"].ToString();
                            this.UserName = dr["EmpName"].ToString();
                        }
                    }

                    sql = "Select [EmpUnqID],[MachineIP],[Type],[RFIDNO],[TmpData],[Length] " +
                          " FROM [EmpBioData] " +
                          " where EmpUnqID = '" + tEmpUnqID + "' and [Type] in ('RFID','FACE','FINGER') " +
                          " and MachineNo = 9999 ";

                    SqlCommand cmd = new SqlCommand(sql, cn);

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                if (sdr["Type"].ToString() == "RFID")
                                    this.CardNumber = sdr["RFIDNO"].ToString();
                                if (sdr["Type"].ToString() == "FACE")
                                {
                                    this.FaceTemp = sdr["TmpData"].ToString();
                                    this.FaceLength = Convert.ToInt32(sdr["Length"].ToString());
                                }

                                if (sdr["Type"].ToString() == "FINGER")
                                {
                                    this.FingerTemp = sdr["TmpData"].ToString();
                                    this.FingerLength = Convert.ToInt32(sdr["Length"].ToString());
                                }
                                    
                            }
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    err = ex.ToString();
                    return;
                }
            }//using sqlconnection
        }

        public void SetUserInfoForMachine(string tEmpUnqID)
        {

            if (tEmpUnqID == string.Empty)
            {
                this.err = "User ID is required...";
                return;
            }

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    cn.Open();
                }
                catch (Exception ex)
                {
                    this.err = ex.ToString();
                    return;
                }

                try
                {
                    string sql = "Select EmpUnqID,EmpName,WrkGrp,MessCode,MessGrpCode from MastEmp Where EmpUnqId ='" + tEmpUnqID + "'";
                    DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
                    bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

                    if (hasRows)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            this.WrkGrp = dr["WrkGrp"].ToString();
                            this.MessCode = dr["MessCode"].ToString();
                            this.MessGrpCode = dr["MessGrpCode"].ToString();
                            this.UserID = dr["EmpUnqID"].ToString();
                            this.UserName = dr["EmpName"].ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    err = ex.ToString();
                    return;
                }
            }//using sqlconnection
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tInfoType">1->RFID,2->Face,3->Finger</param>
        /// <param name="err">1->return error</param>
        public void StoreToDb(int tInfoType,out string err)
        {
            err = "";
            if(tInfoType < 1 || tInfoType > 3)
            {
                err = "Invalid infotype";
                return;
            }
            if (string.IsNullOrEmpty(this.UserID))
            {
                err = "UserID is Required...";
                return;
            }
            
            //if (string.IsNullOrEmpty(this.CardNumber))
            //{
            //    err = "RFID Card Number is Required...";
            //    return;
            //}

            if (tInfoType == 2 && string.IsNullOrEmpty(this.FaceTemp))
            {
                err = "Face Template is Required...";
                return;
            }
            if (tInfoType == 3 && string.IsNullOrEmpty(this.FingerTemp))
            {
                err = "Finger Template is Required...";
                return;
            }

            if(tInfoType == 2 && this.FaceLength == 0 )
            {
                err = "Data Template Length is Required...";
                return;
            }
            if (tInfoType == 3 && this.FingerLength == 0)
            {
                err = "Data Template Length is Required...";
                return;
            }


            //store to db
            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    cn.Open();
                }
                catch (Exception ex)
                {
                    err = ex.ToString();
                    return;
                }

                string sql = string.Empty;
                string delsql = string.Empty;

                #region set_sqlstr
                switch (tInfoType)
                {
                    case 1:
                        sql = "Insert Into EmpBioData (EmpUnqID,MachineIP,Type,idx,MachineNo,EmpName,Pass,Privilege,Blocked,RFIDNO,AddDt,AddID)" +
                                " Values ('" + this.UserID + "' " +
                                " ,'" + "Master" +  "'" +
                                " ,'RFID',10 " +
                                " ,'9999'  " +
                                " ,'" + this.UserName + "' " +
                                " ,'" + this.Password + "' " +
                                " ,'" + this.Previlege.ToString() + "'" +
                                " ,'" + ((this.Enabled)?"0":"1") + "'" +
                                " ,'" + this.CardNumber +  "',GetDate(),'" + Utils.User.GUserID + "')";
                        delsql = "Delete From EmpBioData Where EmpUnqID = '" + this.UserID + "' And Type = 'RFID' and MachineNo = 9999 ";

                        break;
                    case 2:
                        sql = "Insert Into EmpBioData (EmpUnqID,MachineIP,Type,idx,MachineNo,EmpName,Pass,Privilege,Blocked,RFIDNO,tmpData,Length,AddDt,AddID)" +
                                " Values ('" + this.UserID + "' " +
                                " ,'" + "Master" +  "'" +
                                " ,'FACE','" + this.FaceIndex.ToString() + "'" +
                                " ,'9999'  " +
                                " ,'" + this.UserName + "' " +
                                " ,'" + this.Password + "' " +
                                " ,'" + this.Previlege.ToString() + "'" +
                                " ,'" + ((this.Enabled)?"0":"1") + "'" +
                                " ,'" + this.CardNumber + "','" + this.FaceTemp + "','" + this.FaceLength.ToString() + "',GetDate(),'" + Utils.User.GUserID + "' )";

                        delsql = "Delete From EmpBioData Where EmpUnqID = '" + this.UserID + "' And Type = 'FACE' and MachineNo = 9999 ";

                                break;
                    case 3:
                         sql = "Insert Into EmpBioData (EmpUnqID,MachineIP,Type,idx,MachineNo,EmpName,Pass,Privilege,Blocked,RFIDNO,tmpData,Length,AddDt,AddID)" +
                                " Values ('" + this.UserID + "' " +
                                " ,'" + "Master" +  "'" +
                                " ,'FINGER','" + this.FingerIndex.ToString() + "'" +
                                " ,'9999'  " +
                                " ,'" + this.UserName + "' " +
                                " ,'" + this.Password + "' " +
                                " ,'" + this.Previlege.ToString() + "'" +
                                " ,'" + ((this.Enabled)?"0":"1") + "'" +
                                " ,'" + this.CardNumber + "','" + this.FingerTemp + "','" + this.FingerLength.ToString() + "',GetDate(),'" + Utils.User.GUserID + "' )";

                         delsql = "Delete From EmpBioData Where EmpUnqID = '" + this.UserID + "' And Type = 'FINGER' and MachineNo = 9999 and idx ='" + this.FingerIndex.ToString() + "'" ;

                                break;

                    default:
                        break;
                }
                #endregion

                if (string.IsNullOrEmpty(sql) || string.IsNullOrEmpty(delsql))
                {
                    err = "could not find steps to perform storage";
                    return;
                }

                
                using (SqlTransaction tr = cn.BeginTransaction())
                {
                    try
                    {
                        //perform delete first
                        SqlCommand cmd = new SqlCommand(delsql, cn, tr);
                        cmd.ExecuteNonQuery();
                        //insert record..
                        cmd = new SqlCommand(sql, cn, tr);
                        cmd.ExecuteNonQuery();
                        tr.Commit();
                    }
                    catch (Exception ex)
                    {
                        err = ex.ToString();
                        tr.Rollback();
                        return;
                    }
                }
            }
        }


    }

    class AttdLog
    {
        public string EmpUnqID;
        public DateTime PunchDate;
        public string IOFLG;
        public string MachineIP;
        public bool LunchFlg;
        public int tYear;
        public int tYearMt;
        public DateTime t1Date;
        public DateTime AddDt;
        public string AddID;
        public string TableName;
        public string Error;

        public override string ToString()
        {
            return IOFLG.ToString() + ";" + PunchDate.ToString("yyyy-MM-dd HH:mm:ss") + ";" + EmpUnqID.ToString() + ";" + MachineIP + ";" + ((LunchFlg) ? "1" : "0");
        }

        public string GetDBWriteString()
        {
            string dbstr = string.Empty;
            if (this.TableName == string.Empty)
                this.TableName = "AttdLog";

            dbstr = "Insert into " + this.TableName.Trim() + " (PunchDate,EmpUnqID,IOFLG,MachineIP,LunchFlg,tYear,tYearMt,t1Date,AddDt,AddID) Values (" +
                " '" + this.PunchDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + this.EmpUnqID + "','" + this.IOFLG + "','" + this.MachineIP + "'," +
                " '" + ((this.LunchFlg) ? "1" : "0") + "','" + this.tYear.ToString() + "'," +
                " '" + this.tYearMt.ToString() + "','" + this.t1Date.ToString("yyyy-MM-dd") + "'," +
                " '" + this.AddDt.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Utils.User.GUserID + "');";

            return dbstr;
        }

        public string GetDBWriteErrString()
        {
            string dbstr = string.Empty;
           
            //if duplicate punch found place in errAttdLog
            dbstr = "Insert into errATTDLOG (PunchDate,EmpUnqID,IOFLG,MachineIP,LunchFlg,tYear,tYearMt,t1Date,AddDt,AddID) Values (" +
                " '" + this.PunchDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + this.EmpUnqID + "','" + this.IOFLG + "','" + this.MachineIP + "'," +
                " '" + ((this.LunchFlg) ? "1" : "0") + "','" + this.tYear.ToString() + "'," +
                " '" + this.tYearMt.ToString() + "','" + this.t1Date.ToString("yyyy-MM-dd") + "'," +
                " '" + this.AddDt.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Utils.User.GUserID + "');";

            return dbstr;
        }
    }


    class SheetName
    {
        public string sheetName { get; set; }
        public string sheetType { get; set; }
        public string sheetCatalog { get; set; }
        public string sheetSchema { get; set; }
    }


    class ExcelHelper
    {
        public static List<SheetName> GetSheetNames(OleDbConnection conn)
        {
            List<SheetName> sheetNames = new List<SheetName>();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            DataTable excelSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            foreach (DataRow row in excelSchema.Rows)
            {
                if (!row["TABLE_NAME"].ToString().Contains("FilterDatabase"))
                {
                    sheetNames.Add(new SheetName() { sheetName = row["TABLE_NAME"].ToString(), sheetType = row["TABLE_TYPE"].ToString(), sheetCatalog = row["TABLE_CATALOG"].ToString(), sheetSchema = row["TABLE_SCHEMA"].ToString() });
                }
            }
            conn.Close();
            return sheetNames;
        }
    }


    class EmailHelper
    {

        public static string Email(string to,
                                     string cc,
                                     string bcc,
                                     string body,
                                     string subject,
                                     string fromAddress,
                                     string fromDisplay,
                                     string credentialUser,
                                     string credentialPassword,
                                     params MailAttachment[] attachments)
        {
            string host = Globals.G_SmtpHostIP;
            //body = "";// UpgradeEmailFormat(body);
            try
            {
                MailMessage mail = new MailMessage();
                mail.Body = body;
                mail.IsBodyHtml = true;

                string[] mailto = to.Split(';');
                string[] mailcc = cc.Split(';');
                string[] mailbcc = bcc.Split(';');


                foreach (string tto in mailto)
                {
                    if (!string.IsNullOrWhiteSpace(tto))
                    {
                        mail.To.Add(new MailAddress(tto));
                    }

                }

                foreach (string tcc in mailcc)
                {
                    if (!string.IsNullOrWhiteSpace(tcc))
                    {
                        mail.CC.Add(new MailAddress(tcc));
                    }

                }

                foreach (string tbcc in mailbcc)
                {
                    if (!string.IsNullOrWhiteSpace(tbcc))
                    {
                        mail.Bcc.Add(new MailAddress(tbcc));
                    }

                }

                if (mailto.Count() <= 0 && to.Trim().Length > 0)
                {
                    mail.To.Add(new MailAddress(to));
                }

                if (mailcc.Count() <= 0 && cc.Trim().Length > 0)
                {
                    mail.CC.Add(new MailAddress(cc));
                }

                if (mailbcc.Count() <= 0 && bcc.Trim().Length > 0)
                {
                    mail.Bcc.Add(new MailAddress(bcc));
                }

                mail.From = new MailAddress(fromAddress, fromDisplay, Encoding.UTF8);
                mail.Subject = subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.Normal;
                foreach (MailAttachment ma in attachments)
                {
                    mail.Attachments.Add(ma.File);
                }
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new System.Net.NetworkCredential(credentialUser, credentialPassword);
                smtp.Host = host;
                smtp.Send(mail);
                return "Send";
            }
            catch (Exception ex)
            {
                return "Error : " + ex.ToString();
               
            }
        }


        public static string Email(string to,
                                     string cc,
                                     string bcc,
                                     string body,
                                     string subject,
                                     string fromAddress,
                                     string fromDisplay,
                                     string credentialUser,
                                     string credentialPassword)
        {
            string host = Globals.G_SmtpHostIP;
            //body = "";// UpgradeEmailFormat(body);
            try
            {
                MailMessage mail = new MailMessage();
                mail.Body = body;
                mail.IsBodyHtml = true;

                string[] mailto = to.Split(';');
                string[] mailcc = cc.Split(';');
                string[] mailbcc = bcc.Split(';');


                foreach (string tto in mailto)
                {
                    if (!string.IsNullOrWhiteSpace(tto))
                    {
                        mail.To.Add(new MailAddress(tto));
                    }

                }

                foreach (string tcc in mailcc)
                {
                    if (!string.IsNullOrWhiteSpace(tcc))
                    {
                        mail.CC.Add(new MailAddress(tcc));
                    }

                }

                foreach (string tbcc in mailbcc)
                {
                    if (!string.IsNullOrWhiteSpace(tbcc))
                    {
                        mail.Bcc.Add(new MailAddress(tbcc));
                    }

                }

                if (mailto.Count() <= 0 && to.Trim().Length > 0)
                {
                    mail.To.Add(new MailAddress(to));
                }

                if (mailcc.Count() <= 0 && cc.Trim().Length > 0)
                {
                    mail.CC.Add(new MailAddress(cc));
                }

                if (mailbcc.Count() <= 0 && bcc.Trim().Length > 0)
                {
                    mail.Bcc.Add(new MailAddress(to));
                }

                mail.From = new MailAddress(fromAddress, fromDisplay, Encoding.UTF8);
                mail.Subject = subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.Normal;
                
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new System.Net.NetworkCredential(credentialUser, credentialPassword);
                smtp.Host = host;
                smtp.Send(mail);
                return "Send";
            }
            catch (Exception ex)
            {
                return "Error : " + ex.ToString();

            }
        }
    }


}
