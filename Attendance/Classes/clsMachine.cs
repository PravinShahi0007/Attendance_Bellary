using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zkemkeeper;
using Attendance.Classes;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Net.NetworkInformation;

namespace Attendance.Classes
{
    class clsMachine
    {
        private zkemkeeper.CZKEM CZKEM1;
        
        private string _ip,_machinedesc , _tableName,_location;
        private bool _connected;
        private string _version,_fingerprintversion;

        private int _port;
        private int _machineno,_LastErrCode,_AttdLogCount;
        private string _ioflg;

        private bool _messflg,_autoclear,_lunchinout,_gateinout,_istft, _rfid,_face,_finger;

        /* Error Codes
         
         1 SUCCESSED
         4 ERR_INVALID_PARAM
         0 ERR_NO_DATA
        -1 ERROR_NOT_INIT
        -2 ERROR_IO
        -3 ERROR_SIZE
        -4 ERROR_NO_SPACE
        -100 ERROR_UNSUPPORT
        
         */

        /// <summary>
        /// get machine info from database->readerconfig
        /// </summary>
        /// <returns></returns>
        private bool GetMachineInfoFromDb()
        {
            bool ret = false;
            string selerr = string.Empty;

            DataSet ds = Utils.Helper.GetData("Select * from ReaderConfig Where MachineIP ='" + _ip + "'", Utils.Helper.constr,out selerr);
            bool hasrows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
            if (hasrows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    _machinedesc = dr["MachineDesc"].ToString();
                    _machineno = Convert.ToInt32(dr["MachineNo"]);
                    _ioflg = dr["IOFLG"].ToString().Substring(0, 1);
                    _autoclear = Convert.ToBoolean(dr["AutoClear"]);
                    _messflg = Convert.ToBoolean(dr["CanteenFlg"]);
                    _lunchinout = Convert.ToBoolean(dr["LunchInOut"]);
                    _gateinout = Convert.ToBoolean(dr["GateInOut"]);
                    _location = dr["Location"].ToString();
                    
                    _rfid = Convert.ToBoolean(dr["RFID"]);
                    _face = Convert.ToBoolean(dr["FACE"]);
                    _finger = Convert.ToBoolean(dr["Finger"]);

                    if (_lunchinout)
                        _tableName = "AttdLunchGate";
                    else if (_messflg)
                        _tableName = "AttdLog";
                    else if (_gateinout)
                        _tableName = "AttdGateInOut";
                    else if (Globals.G_WaterIP.Contains(_ip))
                        _tableName = "AttdWater";
                    else
                        _tableName = "AttdLog";

                    ret = true;
                }
            }
            else
            {
                ret = false;
                _machinedesc = "Not Found...";
                _machineno = 1;
                
                _messflg = false;
                _autoclear = false;
                _lunchinout = false;
                _gateinout = false;
                _location = "Not Found...";
                
                _tableName = "AttdLog";
            }

            return ret;
        } 

        public clsMachine(string IPAddress,string ioflg)
        {
            _ip = IPAddress;
            _ioflg = ioflg;

            _connected = false;
            _port = 4370;
            _LastErrCode = 0;
            _istft = false;
            _version = "";
            _rfid = false;
            _face = false;
            _finger = false;

            CZKEM1 = new zkemkeeper.CZKEM();
        }

        public int GetLastErr { 
            get {
                this.CZKEM1.GetLastError(_LastErrCode);
                return _LastErrCode; 
            } 
        }
        
        public void Connect(out string err)
        {
            err = string.Empty;
           
            _LastErrCode = 0;

            if(string.IsNullOrEmpty(_ip))
            {
                err = "IP Address is required..";
                return;
            }

            if (_ioflg == string.Empty)
            {
                err = "I/O Flg need to set before connect..";
                return;
            }

           

            if (!"I|O|B".Contains(_ioflg))
            {
                err = "Invalid I/O Flg required(I,O,B)";
                return;
            }

            //check ping if Success/networkstatus first
            string status = this.PingMachine(out err);
            if(status.ToUpper() != "SUCCESS" )
            {
                err = "Ping Time Out Expired..";
                return;
            }

            this.GetMachineInfoFromDb();

            try
            {
                _connected = this.CZKEM1.Connect_Net(_ip, _port);
                
            }
            catch (Exception ex)
            {
                err = ex.ToString();
                return;
            }

            if (!_connected)
            {
                this.CZKEM1.GetLastError(_LastErrCode);
                err = "Can not connect machine ErrorCode :" + _LastErrCode.ToString();
                return;
            }

            
            try
            {
                this.CZKEM1.GetFirmwareVersion(_machineno, ref _version);
                _version = _version.Substring(4, 4);
            }
            catch (Exception ex)
            {
                _version = "0";
            }

            _istft = this.CZKEM1.IsTFTMachine(_machineno);
        
            if ( _finger)
            {
                //'Determine Whether the Device Uses ZKFinger10.0 or ZKFinger9 0#
                //'If vValue='10', the device uses ZKFinger10.0
                //'If vValue=‘9’ or vValue=‘’, the device uses ZKFinger9.0

                
                this.CZKEM1.GetSysOption(_machineno, "~ZKFPVersion", out _fingerprintversion);
            }
        }

        public void DisConnect (out string err)
        {
            err = string.Empty;
            

            if(string.IsNullOrEmpty(_ip))
            {
                err = "IP Address is required..";
                return;
            }
            try
            {
                this.CZKEM1.Disconnect();
                _connected = false;
                return;
            }catch(Exception ex ){

                this.CZKEM1.GetLastError(ref _LastErrCode);
                err = "Operation failed,ErrorCode=" + _LastErrCode.ToString() + Environment.NewLine + ex.ToString();
                _connected = true;
            }
            
        }

        public void GetAttdCnt(out int count, out string err)
        {
            count = 0;
            err = string.Empty;

            if(!_connected)
            {
                err = "Machine not connected..";
                return;
            }

            CZKEM1.EnableDevice(_machineno, false);//disable the device
            if (CZKEM1.GetDeviceStatus(_machineno, 6, ref _AttdLogCount)) //Here we use the function "GetDeviceStatus" to get the record's count.The parameter "Status" is 6.
            {
                count = _AttdLogCount;
            }
            else
            {
               CZKEM1.GetLastError(ref _LastErrCode);
                err = "Operation failed,ErrorCode=" + _LastErrCode.ToString();
            }
            CZKEM1.EnableDevice(_machineno, true);//enable the device
        }

        public void GetAttdRec(out List<AttdLog> AttdLogRec,out string err)
        {
            AttdLogRec = new List<AttdLog>();


            err = string.Empty;
            if (!_connected)
            {
                err = "Machine not connected..";
                return;
            }


            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            int idwWorkcode = 0;
            string sdwEnrollNumber = "";
            int odwEnrollNumber = 0; //for old machine
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            
            int idwReserved = 0;
           
            bool m_tft = false;

            //count records
            int cnt = 0; string outerr = string.Empty;
            this.GetAttdCnt(out cnt, out outerr);

            if (cnt == 0)
            {
                err = outerr;
                return;
            }
            
            m_tft = CZKEM1.IsTFTMachine(_machineno);
            
            //'Prepare File Name for writing log data
            //CZKEM1.GetDeviceTime(_machineno, ref idwYear, ref idwMonth, ref idwDay, ref idwHour, ref idwMinute, ref idwSecond);
            string filepath = Utils.Helper.GetLogFilePath();
            string filenm = "AttdLog_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + "-[" + _ip.Replace(".","_") + "].txt";
            string fullpath = Path.Combine(filepath, filenm);
                        
            CZKEM1.EnableDevice(_machineno, false);//disable the device
            if (CZKEM1.ReadGeneralLogData(_machineno))//read all the attendance records to the memory
            {
                if (m_tft)
                {
                    while (CZKEM1.SSR_GetGeneralLogData(_machineno, out sdwEnrollNumber, out idwVerifyMode,
                            out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                    {

                        AttdLog t = new AttdLog();
                        DateTime logdt = new DateTime(idwYear,idwMonth,idwDay,idwHour,idwMinute,idwSecond);
                        

                        t.EmpUnqID = sdwEnrollNumber.ToString();
                        t.PunchDate = logdt;
                        t.MachineIP = _ip;
                        t.IOFLG = _ioflg;
                        t.LunchFlg = _messflg;
                        t.t1Date = new DateTime(idwYear,idwMonth,idwDay);
                        t.tYear = t.t1Date.Year;
                        t.tYearMt = Convert.ToInt32(t.t1Date.Year.ToString() + t.t1Date.Month.ToString("00"));
                        t.AddID = Utils.User.GUserID;
                        t.AddDt = DateTime.Now;
                        t.TableName = _tableName;
                        AttdLogRec.Add(t);
                        
                    }
                }
                else
                {

                    odwEnrollNumber = 0;

                    while (CZKEM1.GetGeneralExtLogData(_machineno,ref odwEnrollNumber,ref idwVerifyMode,
                            ref idwInOutMode,ref idwYear,ref idwMonth,ref idwDay,ref idwHour,ref idwMinute,ref idwSecond,ref idwWorkcode,ref idwReserved ))//get records from the memory
                    {
                        AttdLog t = new AttdLog();
                        DateTime logdt = new DateTime(idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond);


                        t.EmpUnqID = odwEnrollNumber.ToString();
                        t.PunchDate = logdt;
                        t.MachineIP = _ip;
                        t.IOFLG = _ioflg;
                        t.LunchFlg = _messflg;
                        t.t1Date = new DateTime(idwYear, idwMonth, idwDay);
                        t.tYear = t.t1Date.Year;
                        t.tYearMt = Convert.ToInt32(t.t1Date.Year.ToString() + t.t1Date.Month.ToString("00"));
                        t.AddID = Utils.User.GUserID;
                        t.AddDt = DateTime.Now;
                        t.TableName = _tableName;
                        AttdLogRec.Add(t);                        
                    }
                }
                
                
            }
            else
            {
                
                this.CZKEM1.GetLastError(ref _LastErrCode);

                if (_LastErrCode != 0)
                {
                    err =  "Reading data from terminal failed,ErrorCode: " +_LastErrCode.ToString();
                }
                else
                {
                    err = "No Records Found...";
                }
                
            }

            this.CZKEM1.EnableDevice(_machineno, true);//enable the device
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fullpath, true))
            {
                file.WriteLine("");
            }

            string write_err = string.Empty;

            //write text file and also store in db
            foreach (AttdLog t in AttdLogRec)
            {
                string dberr = AttdLogStoreToDb(t);
                if (!string.IsNullOrEmpty(dberr))
                {
                    t.Error = dberr;
                    write_err += dberr;
                    err += "Error while store to db : " + t.EmpUnqID + " : " + dberr + Environment.NewLine;
                }

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fullpath, true))
                {
                    file.WriteLine(t.ToString());
                }
            }

            if (string.IsNullOrEmpty(write_err))
            {
                if (this._autoclear)
                {
                    string terr = string.Empty;
                    AttdLogClear(out terr);
                    err += terr;
                }
            }

            
        }

        /// <summary>
        /// store attendance logs in db
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public string AttdLogStoreToDb(AttdLog t)
        {
            string err = string.Empty;

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    cn.Open();
                    string sql = t.GetDBWriteString();

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    if (ex.ToString().Contains("System.Data.SqlClient.SqlException (0x80131904): Violation of PRIMARY KEY"))
                    {

                    }
                    else {
                        err = ex.ToString();
                    }
                    

                    try
                    {
                        string sql = t.GetDBWriteErrString();

                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        {
                            cmd.ExecuteNonQuery();
                            //err = "Duplicate Data Found..";
                        }

                    }catch(Exception ex1)
                    {
                        err += ex1.ToString();
                    }
                    
                }
            }

            return err;
        }

        /// <summary>
        /// clear the attendance records
        /// </summary>
        /// <param name="err"></param>
        public void AttdLogClear(out string err)
        {
            err = string.Empty;
            if (!_connected)
            {
                err = "Machine not connected..";
                return;
            }

            this.CZKEM1.EnableDevice(_machineno, false);//disable the device
            
            
            if (CZKEM1.ClearGLog(_machineno))
            {
                CZKEM1.RefreshData(_machineno);//the data in the device should be refreshed
            }
            else
            {
                CZKEM1.GetLastError(ref _LastErrCode);
                err = "Operation failed,ErrorCode=" + _LastErrCode.ToString();
            }

            CZKEM1.EnableDevice(_machineno, true);//enable the device
        }

        /// <summary>
        /// set the system's current time in machine
        /// </summary>
        /// <param name="err"></param>
        public void SetTime(out string err)
        {
            err = string.Empty;
            if (!_connected)
            {
                err = "Machine not connected..";
                return;
            }
            
            
            this.CZKEM1.EnableDevice(_machineno,false);

            err = (this.CZKEM1.SetDeviceTime(_machineno) ? "" : "Unable to Set Time...");

            this.CZKEM1.EnableDevice(_machineno, true);
        }

        /// <summary>
        /// restart machine
        /// </summary>
        /// <param name="err"></param>
        public void Restart(out string err)
        {
            err = string.Empty;
            if (!_connected)
            {
                err = "Machine not connected..";
                return;
            }

            bool t = CZKEM1.RestartDevice(_machineno);
            
        }

        /// <summary>
        /// unlock machine clear all administrators
        /// </summary>
        /// <param name="err"></param>
        public void Unlock(out string err)
        {
            err = string.Empty;
            if (!_connected)
            {
                err = "Machine not connected..";
                return;
            }
            CZKEM1.EnableDevice(_machineno, false);
            bool t = CZKEM1.ClearAdministrators(_machineno);
            CZKEM1.RefreshData(_machineno);
            CZKEM1.EnableDevice(_machineno, true);
            
        }

        /// <summary>
        /// this function will handle history of machine users in database according supplied second parameter.
        /// </summary>
        /// <param name="tEmpUnqID">UserID</param>
        /// <param name="reg">true-if register, false -if delete from machine</param>
        public void StoreHistoryinDB(string tEmpUnqID,bool reg)
        {
            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                       
                        if (reg)
                        {
                            cmd.CommandText = "Insert into MastMachineUsers (MachineIP,EmpUnqID,AddDt,AddId) Values (" +
                              "'" + _ip + "','" + tEmpUnqID + "',GetDate(),'" + Utils.User.GUserID + "')";
                        }
                        else
                        {
                            cmd.CommandText = "Delete From MastMachineUsers Where MachineIP ='" + _ip + "' and EmpUnqID ='" + tEmpUnqID + "'";
                        }                       
                        
                        cmd.ExecuteNonQuery();
                    }

                }
                catch(Exception ex)
                {

                }
            }
        }

        /// <summary>
        /// this function will store blocked employee with ip address
        /// </summary>
        /// <param name="tEmpUnqID">UserID</param>
        /// <param name="reg">true-if blocked, false -if unblocked </param>
        public void StoreBlockHistory(string tEmpUnqID, bool reg)
        {
            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;                        

                        if (reg)
                        {
                            cmd.CommandText = "Insert into EmpBlocked (EmpUnqID,IPAdd,Blocked,BlockedDate,BlockedBy) Values (" +
                              "'" + tEmpUnqID + "','" + _ip + "',1,GetDate(),'" + Utils.User.GUserID + "')";
                        }
                        else
                        {
                            cmd.CommandText = "Update EmpBlocked Set Unblockeddt = GetDate(), UnblockedBy = '" + Utils.User.GUserID + "'" +
                              " Where EmpUnqID ='" + tEmpUnqID + "' And IPAdd = '" + _ip + "' And Unblockeddt is null " ;
                        }

                        cmd.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }
        
        /// <summary>
        /// this function will get the bio detail from master data, 
        /// if MessGrp and MessCode not defined and machine is used for canteen
        /// return with err.
        /// </summary>
        /// <param name="tEmpUnqID">EmpUnqID</param>
        /// <param name="err">return string err</param>
        public void Register(string tEmpUnqID, out string err)
        {
            
            err = string.Empty;
            if (!_connected)
            {
                err = "Machine not connected..";
                return;
            }
            if (string.IsNullOrEmpty(tEmpUnqID))
            {
                err = "UserID is required..";
                return;
            }

            

            UserBioInfo emp = new UserBioInfo();
            emp.SetUserInfoForMachine(tEmpUnqID);
            emp.GetBioInfoFromDB(tEmpUnqID);

            ////check user rights for the wrkgrp
            ////'if not move next emp
            //if(!Globals.GetWrkGrpRights(635,emp.WrkGrp,emp.UserID))
            //{
            //    err = "You are not Authorised...";
            //    return;
            //}

            //if(string.IsNullOrEmpty(emp.CardNumber))
            //{
            //    err = "RFID Card Number not found...";
            //    return;
            //}

            if (_messflg)
            {
                if (string.IsNullOrEmpty(emp.MessCode))
                {
                    err = "Employee Mess Code is Required....";
                    return;
                }

                if (string.IsNullOrEmpty(emp.MessGrpCode))
                {
                    err = "Employee Mess Grp Code is Required....";
                    return;
                }
            }

            

            //store registration info in db....
            //this.CZKEM1.EnableDevice(_machineno, false);
            
            StoreHistoryinDB(emp.UserID,true);

            if(!_istft)
            {
                //this.CZKEM1.set_STR_CardNumber(0, emp.UserID);
                this.CZKEM1.set_CardNumber(0,Convert.ToInt32(emp.CardNumber));
                bool x = this.CZKEM1.SetUserInfo(_machineno, Convert.ToInt32(emp.UserID), "", "", 0,true);
                //this.CZKEM1.RefreshData(_machineno);
                //this.CZKEM1.EnableDevice(_machineno, true);
                return;
            }
            
            if(this.CZKEM1.SetStrCardNumber(emp.CardNumber))
            {
                if (this.CZKEM1.SSR_SetUserInfo(_machineno, emp.UserID, "", "", 0, true))
                {

                    //if it not used in Mess set user face and finger
                    if (_messflg == false)
                    {
                        if (_face)
                        {
                            if (!string.IsNullOrEmpty(emp.FaceTemp))
                            {
                                bool x = this.CZKEM1.SetUserFaceStr(_machineno, emp.UserID, 50, emp.FaceTemp, emp.FaceLength);
                            }
                        }

                        if (_finger)
                        {
                            string sql = "Select EmpUnqID,idx,Tmpdata,[length] from EmpBioData " +
                              " where [type] = 'FINGER' AND TMPDATA IS NOT NULL AND MACHINEIP = 'Master' and MachineNo = '9999' " +
                              " and EmpUnqID = '" + emp.UserID + "'";

                            DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
                            bool hasrows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                            if (hasrows)
                            {

                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    if (!string.IsNullOrEmpty(dr["TmpData"].ToString()))
                                    {
                                        this.CZKEM1.SetUserTmpExStr(_machineno, emp.UserID, Convert.ToInt32(dr["idx"]), 1, dr["TmpData"].ToString());  //'upload templates information to the device
                                    }
                                }
                            }
                            
                            
                        }

                        //this.CZKEM1.SetUserInfoEx(_machineno, Convert.ToInt32(emp.UserID), 146, 0);
                    }
                }

                //this.CZKEM1.RefreshData(_machineno);
                //this.CZKEM1.EnableDevice(_machineno, true);
            }

        }

        /// <summary>
        /// this function will help to register as per supplied UserBioInfo
        /// </summary>
        /// <param name="emp">UserBioInfo</param>
        /// <param name="err">Out string err</param>
        public void Register(UserBioInfo emp, out string err)
        {

            err = string.Empty;
            if (!_connected)
            {
                err = "Machine not connected..";
                return;
            }

            if (string.IsNullOrEmpty(emp.UserID)) 
            {
                err = "UserID is required...";
                return;
            }

            //if(string.IsNullOrEmpty(emp.CardNumber))
            //{
            //    err = "RFID Card No. is Required...";
            //    return;
            //}
            
            //store registration info in db....
            StoreHistoryinDB(emp.UserID, true);

            if (!_istft)
            {
                this.CZKEM1.set_CardNumber(0, Convert.ToInt32(emp.CardNumber));
                this.CZKEM1.SetUserInfo(_machineno, Convert.ToInt32(emp.UserID), "", "", 0, true);
                this.CZKEM1.RefreshData(_machineno);
                this.CZKEM1.EnableDevice(_machineno, true);
                return;
            }

            if (this.CZKEM1.SetStrCardNumber(emp.CardNumber))
            {
                this.CZKEM1.SSR_SetUserInfo(_machineno, emp.UserID, "", "", 0, true);


                //if it not used in Mess set user face and finger
                if (_messflg == false)
                {
                    if (_face)
                    {
                        if (!string.IsNullOrEmpty(emp.FaceTemp))
                        {
                            this.CZKEM1.SetUserFaceStr(_machineno, emp.UserID, 50, emp.FaceTemp, emp.FaceLength);
                        }
                    }

                    if (_finger)
                    {
                        string sql = "Select EmpUnqID,idx,Tmpdata,[length] from EmpBioData " +
                              " where [type] = 'FINGER' AND TMPDATA IS NOT NULL AND MACHINEIP = 'Master' and MachineNo = '9999' " +
                              " and EmpUnqID = '" + emp.UserID + "'";

                        DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
                        bool hasrows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                        if (hasrows)
                        {

                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                if (!string.IsNullOrEmpty(dr["TmpData"].ToString()))
                                {
                                    //'upload templates information to the device
                                    this.CZKEM1.SetUserTmpExStr(_machineno, emp.UserID, Convert.ToInt32(dr["idx"]), 1, dr["TmpData"].ToString());  
                                }
                            }
                        }
                    }
                    
                    //this.CZKEM1.SetUserInfoEx(_machineno, Convert.ToInt32(emp.UserID), 146, 0);
                }

            }

        }

        public void EnableDevice(bool isEnabled)
        {
            this.CZKEM1.EnableDevice(_machineno, isEnabled);
        }

        public void RefreshData()
        {
            this.CZKEM1.RefreshData(_machineno);
        }

        /// <summary>
        /// this function will get the bio detail from master data
        /// </summary>
        /// <param name="tUserList">List of UserBioInfo</param>
        /// <param name="RetUserList">return List of UsersBioInfo with err details</param>
        public void Register(List<UserBioInfo> tUserList,out List<UserBioInfo> RetUserList)
        {
            RetUserList = new List<UserBioInfo>();

            if (!_connected)
            {
                foreach (UserBioInfo emp in tUserList)
                {
                    emp.err = "Machine not connected..";
                }
                RetUserList = tUserList;
                return;
            }

            foreach (UserBioInfo emp in tUserList)
            {
                if (string.IsNullOrEmpty(emp.UserID))
                {
                    emp.UserID = "DELETE";
                    continue;
                }
                emp.SetUserInfoForMachine(emp.UserID);
                emp.GetBioInfoFromDB(emp.UserID);

                ////check user rights for the wrkgrp
                ////'if not move next emp
                //if (!Globals.GetWrkGrpRights(635, emp.WrkGrp, emp.UserID))
                //{
                //    emp.err += "You are not Authorised...";
                    
                //}

                //if (string.IsNullOrEmpty(emp.CardNumber))
                //{
                //    emp.err += ",RFID Card Number not found...";
                    
                //}

            }
            
            foreach (UserBioInfo emp in tUserList)
            {
                //store registration info in db....
                
                if (string.IsNullOrEmpty(emp.err))
                {
                    StoreHistoryinDB(emp.UserID,true);

                    if (!_istft)
                    {
                        this.CZKEM1.set_CardNumber(0, Convert.ToInt32(emp.CardNumber));
                        this.CZKEM1.SetUserInfo(_machineno, Convert.ToInt32(emp.UserID), "", "", 0, true);
                        emp.err += "RFID Registered";
                    }
                    else
                    {
                        if (this.CZKEM1.SetStrCardNumber(emp.CardNumber))
                        {
                            emp.err += "RFID Registered,";
                            this.CZKEM1.SSR_SetUserInfo(_machineno, emp.UserID, "", "", 0, true);

                            //if it not used in Mess set user face and finger
                            if (_messflg == false)
                            {
                                if (_face)
                                {
                                    if (!string.IsNullOrEmpty(emp.FaceTemp))
                                    {
                                        this.CZKEM1.SetUserFaceStr(_machineno, emp.UserID, 50, emp.FaceTemp, emp.FaceLength);
                                        emp.err += "Face Registered,";
                                    }
                                }

                                if (_finger)
                                {
                                   //'upload templates information to the device
                                    string sql = "Select EmpUnqID,idx,Tmpdata,[length] from EmpBioData " +
                                      " where [type] = 'FINGER' AND TMPDATA IS NOT NULL AND MACHINEIP = 'Master' and MachineNo = '9999' " +
                                      " and EmpUnqID = '" + emp.UserID + "'";

                                    DataSet ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
                                    bool hasrows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                                    if (hasrows)
                                    {

                                        foreach (DataRow dr in ds.Tables[0].Rows)
                                        {
                                            if (!string.IsNullOrEmpty(dr["TmpData"].ToString()))
                                            {
                                                //'upload templates information to the device
                                                this.CZKEM1.SetUserTmpExStr(_machineno, emp.UserID, Convert.ToInt32(dr["idx"]), 1, dr["TmpData"].ToString());
                                            }
                                        }
                                    }
                                    
                                    
                                }

                                //make sure to access method rfid+face
                                //this.CZKEM1.SetUserInfoEx(_machineno, Convert.ToInt32(emp.UserID), 146, 0);
                                
                              
                            }
                            else
                            {
                                emp.err += "RFID Access Given";
                            }

                        }
                    }// new machine

                }// if no errors found                
            }//end foreach
            
            //this.CZKEM1.RefreshData(_machineno);
            //this.CZKEM1.EnableDevice(_machineno, true);
            
            RetUserList = tUserList;
        }

        /// <summary>
        /// this function stores bio details in master data if supplied fist para to true
        /// </summary>
        /// <param name="isReqToStore">true ->if Store to master data,else false</param>
        /// <param name="err">out string err</param>
        /// <param name="tUsers">out list of users</param>
        public void DownloadALLUsers(bool isReqToStore, out string err, out List<UserBioInfo> tUsers)
        {
            err = string.Empty;
            tUsers = new List<UserBioInfo>();

            if (!_connected)
            {
                err = "Machine not connected..";
                return;
            }

            bool vRet = this.CZKEM1.ReadAllUserID(_machineno) ; // 'read all the user information to the memory
            if(!vRet)
            {
                err = "Error : Can not read All UserID";
                return;
            }

            //if (_finger)
            //{
            //    vRet = this.CZKEM1.ReadAllTemplate(_machineno); //this is for finger print 
            //    if (!vRet)
            //    {
            //        err = "Error : Can not read All User Template";
            //        return;
            //    }
            //}           

            
            UserBioInfo tmpuser = new UserBioInfo();

            string _userid, _username, _password,_cardno,_facetemp,_fingertemp ;
            
            int _prev ,_facelength,_fingerlength,_fingerflg , _useridInt; 
            bool _enabled = false;

            CZKEM1.EnableDevice(_machineno, false);

            if(_istft)
            {
                _userid = string.Empty; _username = string.Empty; _password = string.Empty; _cardno = string.Empty;
                _facetemp = string.Empty; _fingertemp = string.Empty;
                _facelength = 0; _fingerlength = 0; _fingerflg = 0; _useridInt = 0; _prev = 0;
                _enabled = false;

                CZKEM1.ReadAllTemplate(_machineno);
                //loop while users in machine 
                while (CZKEM1.SSR_GetAllUserInfo(_machineno,out _userid, out _username, out _password, out _prev, out _enabled))
	            {
                    tmpuser = new UserBioInfo();
                    tmpuser.UserID = _userid;
                    tmpuser.UserName = _username;
                    tmpuser.Password = _password;
                    tmpuser.Previlege = _prev;
                    tmpuser.Enabled = _enabled;

                    CZKEM1.GetStrCardNumber(out _cardno);
                    
                    tmpuser.CardNumber = _cardno;

                    if(this._face)
                    {
                        if(CZKEM1.GetUserFaceStr(_machineno,_userid , 50,ref _facetemp, ref _facelength))
                        {
                            tmpuser.FaceTemp = _facetemp;
                            tmpuser.FaceLength = _facelength;
                        }
                    }
                    this.StoreHistoryinDB(tmpuser.UserID,true);
                    
                    tUsers.Add(tmpuser);
    
                    //make sure to check version there are difference in get fingerprint
                    if(this._finger)
                    {
                        if(Convert.ToDouble(_version) >= 6.6)
                        {
                            //if(CZKEM1.SSR_GetUserTmpStr(_machineno,_userid,0,out _fingertemp out _fingerlength))
                            for (int i = 0; i <= 9; i++)
                            {
                                UserBioInfo tmpuser2 = new UserBioInfo();
                                tmpuser2.CardNumber = tmpuser.CardNumber;
                                tmpuser2.UserID = tmpuser.UserID;
                                tmpuser2.Password = _password;
                                tmpuser2.Previlege = _prev;
                                tmpuser2.Enabled = _enabled;
                                if (CZKEM1.GetUserTmpExStr(_machineno, _userid, i, out _fingerflg, out _fingertemp, out _fingerlength))
                                {
                                    tmpuser2.FingerIndex = i;
                                    tmpuser2.FingerLength = _fingerlength;
                                    tmpuser2.FingerTemp = _fingertemp;
                                    tUsers.Add(tmpuser2);    
                                }
                            }                               
                        
                        }
                        else if (Convert.ToDouble(_version) < 6.6 )
                        {
                            if(int.TryParse(_userid,out _useridInt))
                            {
                                for (int i = 0; i <= 9; i++)
                                {
                                    UserBioInfo tmpuser2 = new UserBioInfo();
                                    tmpuser2.UserID = tmpuser.UserID;
                                    tmpuser2.CardNumber = tmpuser.CardNumber;
                                    tmpuser2.Password = _password;
                                    tmpuser2.Previlege = _prev;
                                    tmpuser2.Enabled = _enabled;
                                    if (CZKEM1.GetUserTmpStr(_machineno, _useridInt, 0, ref _fingertemp, ref _fingerlength))
                                    {
                                        tmpuser.FingerIndex = i;
                                        tmpuser2.FingerLength = _fingerlength;
                                        tmpuser2.FingerTemp = _fingertemp;
                                        tUsers.Add(tmpuser2);
                                    }
                                }
                            }
                        }
                    }

                    
	            }//end while loop                  
                
            }
            else
            {
                //simple old black machine with only rfid access

                _userid = string.Empty; _username = string.Empty; _password  = string.Empty;_cardno = string.Empty;
                _useridInt = 0; _prev = 0; _enabled = false;
                
               
                while (CZKEM1.GetAllUserInfo(_machineno,ref _useridInt,ref _username,ref _password,ref _prev , ref _enabled))
	            {
	               
                    tmpuser = new UserBioInfo();
                    tmpuser.UserID = _useridInt.ToString();
                    tmpuser.UserName = _username;
                    tmpuser.Password = _password;
                    tmpuser.Previlege = _prev;
                    tmpuser.Enabled = _enabled;

                    CZKEM1.GetStrCardNumber(out _cardno);                    
                    tmpuser.CardNumber = _cardno;
                    tUsers.Add(tmpuser);
	            }
            
            
            }

            CZKEM1.EnableDevice(_machineno, true);
            if (isReqToStore)
            {
                //save to db
                if (tUsers.Count > 0)
                {
                    foreach (UserBioInfo tmp in tUsers)
                    {
                        string allerr = string.Empty;
                        string terr = string.Empty;
                        string stcard = tmp.CardNumber;
                        //tmp.GetBioInfoFromDB(tmp.UserID);
                        if (!string.IsNullOrEmpty(tmp.CardNumber))
                        {
                            tmp.StoreToDb(1, out terr);
                            allerr += terr;
                            terr = string.Empty;
                        }

                        if (!string.IsNullOrEmpty(tmp.FaceTemp))
                        {
                            tmp.StoreToDb(2, out terr);
                            allerr += terr;
                            terr = string.Empty;
                        }

                        if (!string.IsNullOrEmpty(tmp.FingerTemp))
                        {
                            tmp.StoreToDb(3, out terr);
                            allerr += terr;
                            terr = string.Empty;
                        }

                        tmp.err = allerr;

                    }
                }
            }

        }

        /// <summary>
        /// this function help to download bio details from machine and store to master data
        /// </summary>
        /// <param name="tEmpUnqID">EmpUnqID</param>
        /// <param name="err">out string err</param>
        public void DownloadTemplate(string tEmpUnqID,out string err)
        {
            err = string.Empty;
            
            string _userid, _username, _password,_cardno,_facetemp,_fingertemp ;
            
            int _prev ,_facelength,_fingerlength,_fingerflg , _useridInt; 
            bool _enabled = false;

            if (!_connected)
            {
                err = "Machine not connected..";
                return;
            }
            if (string.IsNullOrEmpty(tEmpUnqID))
            {
                err = "UserID is required..";
                return;
            }
            
            this.CZKEM1.EnableDevice(_machineno, false);
            UserBioInfo emp = new UserBioInfo();
            List<UserBioInfo> emplist = new List<UserBioInfo>();

            
            emp.SetUserInfoForMachine(tEmpUnqID);
            
            _userid = string.Empty; _username = string.Empty; _password = string.Empty; _cardno = string.Empty;
            _facetemp = string.Empty; _fingertemp = string.Empty;
            _facelength = 0; _fingerlength = 0; _fingerflg = 0; _useridInt = 0; _prev = 0;
            _enabled = false;
            
            if(this.CZKEM1.ReadAllUserID(_machineno))
            {
                
                if (_istft)
                {
                    this.CZKEM1.GetAllUserID(_machineno,Convert.ToInt32(emp.UserID), _machineno, 0, 0, 1);
                    this.CZKEM1.ReadAllTemplate(_machineno);

                    if (this.CZKEM1.SSR_GetUserInfo(_machineno, emp.UserID, out _username, out _password, out _prev, out _enabled))
                    {
                        emp.Password = _password;
                        emp.Enabled = _enabled;
                        emp.Previlege = _prev;
                        
                    }

                    this.CZKEM1.GetStrCardNumber(out _cardno);
                    if(!string.IsNullOrEmpty(_cardno))
                    {
                        emp.CardNumber = _cardno;
                    }
                    else
                    {
                        err = "RFID Card Number Not Found...";
                        emp.err = err + Environment.NewLine;
                    }

                    if (_face)
                    {
                        this.CZKEM1.GetUserFaceStr(_machineno, emp.UserID, emp.FaceIndex, ref _facetemp, ref _facelength);
                        emp.FaceIndex = 50;
                        emp.FaceLength = _facelength;
                        emp.FaceTemp = _facetemp;

                        if (string.IsNullOrEmpty(_facetemp))
                        {
                            emp.err = emp.err + "Face Template Not Found..." + Environment.NewLine;
                        }
                    }

                    if (_finger)
                    {
                        double fpversion = 0;
                        double.TryParse(_fingerprintversion,out fpversion);

                        emplist.Add(emp);
                        for (int i = 0; i <= 9; i++)
                        {
                            UserBioInfo tmpuser2 = new UserBioInfo();
                            tmpuser2.CardNumber = emp.CardNumber;
                            tmpuser2.UserID = emp.UserID;
                            tmpuser2.Password = _password;
                            tmpuser2.Previlege = _prev;
                            tmpuser2.Enabled = _enabled;
                            if (CZKEM1.GetUserTmpExStr(_machineno, _userid, i, out _fingerflg, out _fingertemp, out _fingerlength))
                            {
                                tmpuser2.FingerIndex = i;
                                tmpuser2.FingerLength = _fingerlength;
                                tmpuser2.FingerTemp = _fingertemp;
                                emplist.Add(tmpuser2);
                            }
                        }      
                        
                        //if(fpversion>= 10 )
                        //{
                        //    this.CZKEM1.GetUserTmpExStr(_machineno, emp.UserID, 0, out _fingerflg, out _fingertemp, out _fingerlength);

                        //}else if(fpversion == 9)
                        //{
                        //    this.CZKEM1.GetUserTmpStr(_machineno, Convert.ToInt32(emp.UserID), 0, ref _fingertemp, ref _fingerlength);
                        //}

                        //emp.FingerTemp = _fingertemp;
                        //emp.FingerLength = _fingerlength;
                    }

                }
                else
                {
                    //old machine
                    if(this.CZKEM1.GetUserInfo(_machineno, Convert.ToInt32(emp.UserID),ref _username,ref _password,ref _prev,ref _enabled))
                    {
                        _cardno = this.CZKEM1.get_CardNumber(0).ToString();
                        emp.CardNumber = _cardno;
                        emp.Enabled = _enabled;
                        emp.Previlege = _prev;
                        emp.Password = _password;

                        if(string.IsNullOrEmpty(_cardno)){
                            emp.err = emp.err + "RFID Card Number Not Found..." + Environment.NewLine;
                        }

                    }                        
                }                
                
            }
            else
            {
                err = "Can not read all users";
                return;
            }
            //save to db

            string allerr =  emp.err;

            foreach (UserBioInfo t in emplist)
            {
                if (_rfid)
                {
                    if (!string.IsNullOrEmpty(t.CardNumber))
                    {
                        t.StoreToDb(1, out err);
                        allerr += err + Environment.NewLine;
                    }
                }

                if (_face)
                {
                    if (!string.IsNullOrEmpty(t.FaceTemp))
                    {
                        t.StoreToDb(2, out err);
                        allerr += err + Environment.NewLine;
                    }
                }

                if (_finger)
                {
                    if (!string.IsNullOrEmpty(t.FingerTemp))
                    {
                        t.StoreToDb(3, out err);
                        allerr += err + Environment.NewLine;
                    }
                }
            }

            err = allerr;

            this.CZKEM1.EnableDevice(_machineno, true);

            }

        /// <summary>
        /// this function help to download bio details from machine and store to master data
        /// </summary>
        /// <param name="tUserList">List of UserBioInfo</param>
        /// <param name="err">out string err</param>
        /// <param name="RetUserList">out List of UserBioInfo with err</param>
        public void DownloadTemplate(List<UserBioInfo> tUserList, out string err,out List<UserBioInfo> RetUserList)
        {
            err = string.Empty;           
            List<UserBioInfo> fingerusers = new  List<UserBioInfo>();

            string _userid, _username, _password, _cardno, _facetemp, _fingertemp;
            int _prev, _facelength, _fingerlength, _fingerflg, _useridInt;
            bool _enabled = false;

            if (!_connected)
            {
                foreach (UserBioInfo emp in tUserList)
                {
                    emp.err = "Machine not connected..";                   
                }
                err = "Machine not connected..";
                RetUserList = tUserList;
                return;
            }

            foreach (UserBioInfo emp in tUserList)
            {
                emp.SetUserInfoForMachine(emp.UserID);
                if (string.IsNullOrEmpty(emp.UserID))
                {
                    emp.err += "UserID is required..";
                }
            }
                       

            this.CZKEM1.EnableDevice(_machineno, false);

            _userid = string.Empty; _username = string.Empty; _password = string.Empty; _cardno = string.Empty;
            _facetemp = string.Empty; _fingertemp = string.Empty;
            _facelength = 0; _fingerlength = 0; _fingerflg = 0; _useridInt = 0; _prev = 0;
            _enabled = false;

            if (this.CZKEM1.ReadAllUserID(_machineno))
            {
                if (_istft)
                {
                    this.CZKEM1.ReadAllTemplate(_machineno);

                    foreach (UserBioInfo emp in tUserList)
                    {
                        this.CZKEM1.GetAllUserID(_machineno, Convert.ToInt32(emp.UserID), _machineno, 0, 0, 1);
                        
                        this.StoreHistoryinDB(emp.UserID, true);

                        if (this.CZKEM1.SSR_GetUserInfo(_machineno, emp.UserID, out _username, out _password, out _prev, out _enabled))
                        {
                            emp.Password = _password;
                            emp.Enabled = _enabled;
                            emp.Previlege = _prev;
                        }

                        this.CZKEM1.GetStrCardNumber(out _cardno);
                        if (!string.IsNullOrEmpty(_cardno))
                        {
                            emp.CardNumber = _cardno;
                        }
                        else
                        {   
                            emp.err += "RFID Card Number Not Found..." +Environment.NewLine;
                        }
                        
                        if (_face)
                        {
                            this.CZKEM1.GetUserFaceStr(_machineno, emp.UserID, emp.FaceIndex, ref _facetemp, ref _facelength);
                            emp.FaceIndex = 50;
                            emp.FaceLength = _facelength;
                            emp.FaceTemp = _facetemp;

                            if (string.IsNullOrEmpty(_facetemp))
                            {
                                emp.err = emp.err + "Face Template Not Found..." + Environment.NewLine;
                            }
                        }

                        if (_finger)
                        {
                            double fpversion = 0;
                            double.TryParse(_fingerprintversion, out fpversion);
                            if (fpversion >= 10)
                            {

                                //if(CZKEM1.SSR_GetUserTmpStr(_machineno,_userid,0,out _fingertemp out _fingerlength))
                                for (int i = 0; i <= 9; i++)
                                {
                                    UserBioInfo tmpuser2 = new UserBioInfo();
                                    tmpuser2.CardNumber = emp.CardNumber;
                                    tmpuser2.UserID = emp.UserID;
                                    tmpuser2.Password = emp.Password;
                                    tmpuser2.Previlege = emp.Previlege;
                                    tmpuser2.Enabled = emp.Enabled;
                                    if (CZKEM1.GetUserTmpExStr(_machineno, _userid, i, out _fingerflg, out _fingertemp, out _fingerlength))
                                    {
                                        tmpuser2.FingerIndex = i;
                                        tmpuser2.FingerLength = _fingerlength;
                                        tmpuser2.FingerTemp = _fingertemp;
                                        fingerusers.Add(tmpuser2);
                                    }
                                }  
                                
                                ///this.CZKEM1.GetUserTmpExStr(_machineno, emp.UserID, 0, out _fingerflg, out _fingertemp, out _fingerlength);

                            }
                            else if (fpversion == 9)
                            {

                                //if(CZKEM1.SSR_GetUserTmpStr(_machineno,_userid,0,out _fingertemp out _fingerlength))
                                for (int i = 0; i <= 9; i++)
                                {
                                    UserBioInfo tmpuser2 = new UserBioInfo();
                                    tmpuser2.CardNumber = emp.CardNumber;
                                    tmpuser2.UserID = emp.UserID;
                                    tmpuser2.Password = emp.Password;
                                    tmpuser2.Previlege = emp.Previlege;
                                    tmpuser2.Enabled = emp.Enabled;
                                    if (this.CZKEM1.GetUserTmpStr(_machineno, Convert.ToInt32(emp.UserID), 0, ref _fingertemp, ref _fingerlength))
                                    {
                                        tmpuser2.FingerIndex = i;
                                        tmpuser2.FingerLength = _fingerlength;
                                        tmpuser2.FingerTemp = _fingertemp;
                                        fingerusers.Add(tmpuser2);
                                    }
                                }  
                                
                                //this.CZKEM1.GetUserTmpStr(_machineno, Convert.ToInt32(emp.UserID), 0, ref _fingertemp, ref _fingerlength);
                            }
                            else
                            {
                                emp.err += emp.err + "Can not determine Finger print version" + Environment.NewLine;
                                continue;
                            }

                            emp.FingerTemp = _fingertemp;
                            emp.FingerLength = _fingerlength;
                            if (string.IsNullOrEmpty(_fingertemp))
                            {
                                emp.err += emp.err + "Finger Template Not Found..." + Environment.NewLine;
                            }
                        }

                    }//end foreach loop
                    

                }
                else
                {
                    foreach (UserBioInfo emp in tUserList)
                    {
                        //old machine
                        if (this.CZKEM1.GetUserInfo(_machineno, Convert.ToInt32(emp.UserID), ref _username, ref _password, ref _prev, ref _enabled))
                        {
                            _cardno = this.CZKEM1.get_CardNumber(0).ToString();
                            emp.CardNumber = _cardno;
                            emp.Enabled = _enabled;
                            emp.Previlege = _prev;
                            emp.Password = _password;

                            if (string.IsNullOrEmpty(_cardno))
                            {
                                emp.err = emp.err + "RFID Card Number Not Found..." + Environment.NewLine;
                            }

                        }
                    }
                    
                }

            }
            else
            {
                foreach(UserBioInfo t in fingerusers)
                {
                    tUserList.Add(t);
                }
                
                RetUserList = tUserList;
                err = "Can not read all users";
                return;
            }
            //save to db

            foreach (UserBioInfo emp in tUserList)
            {
                if (_rfid)
                {
                    if (!string.IsNullOrEmpty(emp.CardNumber))
                    {
                        emp.StoreToDb(1, out err);
                        emp.err += err;
                    }
                }

                if (_face)
                {
                    if (!string.IsNullOrEmpty(emp.FaceTemp))
                    {
                        emp.StoreToDb(2, out err);
                        emp.err += err;
                    }
                }

                if (_finger)
                {
                    if (!string.IsNullOrEmpty(emp.FingerTemp))
                    {
                        emp.StoreToDb(3, out err);
                        emp.err += err;
                    }
                }
            }//end foreach store to db

            RetUserList = tUserList;
            this.CZKEM1.EnableDevice(_machineno, true);

        }
        
        public void DeleteUser(string tEmpUnqID, out string err)
        {
            err = string.Empty;
            if (!_connected)
            {
                err = "Machine not connected..";
                return;
            }
            if (string.IsNullOrEmpty(tEmpUnqID))
            {
                err = "UserID is required..";
                return;
            }

            //UserBioInfo emp = new UserBioInfo();
            //emp.SetUserInfoForMachine(tEmpUnqID);

            //if (!string.IsNullOrEmpty(emp.WrkGrp))
            //{
            //    //check user rights for the wrkgrp
            //    //'if not move next emp
            //    if (Utils.User.GUserID != "SERVER")
            //    {
            //        if (!Globals.GetWrkGrpRights(630, emp.WrkGrp, emp.UserID))
            //        {
            //            err = "You are not Authorised...";
            //            return;
            //        }
            //    }
            //}

            //this.CZKEM1.EnableDevice(_machineno, false);
            string tmpuser = string.Empty, tmppass = string.Empty;
            int tmppre = 0;
            bool tmpenable = false;

            if (!_istft)
            {

                if (this.CZKEM1.GetUserInfo(_machineno, Convert.ToInt32(tEmpUnqID), ref tmpuser, ref tmppass, ref tmppre, ref tmpenable))
                {
                    this.CZKEM1.DeleteEnrollData(_machineno, Convert.ToInt32(tEmpUnqID), _machineno, 0);
                    this.StoreHistoryinDB(tEmpUnqID, false);  
                }             
            }
            else
            {
                if (this.CZKEM1.SSR_GetUserInfo(_machineno, tEmpUnqID, out tmpuser, out tmppass, out tmppre, out tmpenable))
                {
                    //this.CZKEM1.SSR_DeleteEnrollData(_machineno, tEmpUnqID, 0);
                    //this.CZKEM1.DeleteEnrollData(_machineno, Convert.ToInt32(tEmpUnqID), _machineno, 0);
                    this.CZKEM1.SSR_DeleteEnrollDataExt(_machineno, tEmpUnqID, 12);
                    this.CZKEM1.DelUserFace(_machineno, tEmpUnqID, 50);
                    this.StoreHistoryinDB(tEmpUnqID, false);  
                }                               
            }

                  
            //this.CZKEM1.RefreshData(_machineno);
            //this.CZKEM1.EnableDevice(_machineno, true); 
        }

        public void DeleteUser(List<UserBioInfo> tUserList, out string err, out List<UserBioInfo> RetUserList)
        {
            err = string.Empty;
            RetUserList = tUserList;
            if (!_connected)
            {
                foreach (UserBioInfo emp in tUserList)
                {
                    emp.err = "Machine not connected..";
                }
                err = "Machine not connected..";
                RetUserList = tUserList;
                return;
            }

            //if (Utils.User.GUserID != "SERVER")
            //{
            //    foreach (UserBioInfo emp in tUserList)
            //    {
            //        emp.SetUserInfoForMachine(emp.UserID);

            //        //check user rights for the wrkgrp
            //        //'if not move next emp
            //        if (!Globals.GetWrkGrpRights(630, emp.WrkGrp, emp.UserID))
            //        {
            //            emp.err += "You are not Authorised...";
            //        }
            //    }
            //}

            //this.CZKEM1.EnableDevice(_machineno, false);

            foreach (UserBioInfo emp in tUserList)
            {
                //store registration info in db....
                if (string.IsNullOrEmpty(emp.err))
                {
                    StoreHistoryinDB(emp.UserID, false);
                    if (!_istft)
                    {
                        string tmpuser = string.Empty, tmppass = string.Empty;
                        int tmppre = 0;
                        bool tmpenable = false;
                        
                        if (this.CZKEM1.GetUserInfo(_machineno, Convert.ToInt32(emp.UserID), ref tmpuser, ref tmppass, ref tmppre, ref tmpenable))
                        {
                            this.CZKEM1.DeleteEnrollData(_machineno, Convert.ToInt32(emp.UserID), _machineno, 0);
                            this.StoreHistoryinDB(emp.UserID, false);  
                        }

                        
                    }
                    else
                    {
                        string tmpuser = string.Empty, tmppass = string.Empty;
                        int tmppre = 0;
                        bool tmpenable = false;

                        if (this.CZKEM1.SSR_GetUserInfo(_machineno, emp.UserID, out tmpuser, out tmppass, out tmppre, out tmpenable))
                        {
                            //this.CZKEM1.SSR_DeleteEnrollData(_machineno, emp.UserID, 0);
                            //this.CZKEM1.DeleteEnrollData(_machineno, Convert.ToInt32(emp.UserID), _machineno, 0);
                            this.CZKEM1.SSR_DeleteEnrollDataExt(_machineno, emp.UserID, 12);
                            this.CZKEM1.DelUserFace(_machineno, emp.UserID, 50);
                            this.StoreHistoryinDB(emp.UserID, false);  
                        }
                        
                    }
                }// if no errors found 
               
            }//end foreach


            //this.CZKEM1.RefreshData(_machineno);
            //this.CZKEM1.EnableDevice(_machineno, true);

            RetUserList = tUserList;


        }

        public void BlockUser(string tEmpUnqID, out string err)
        {
            err = string.Empty;
           
            if (!_connected)
            {
                err = "Machine not connected..";
                return;
            }
            if (string.IsNullOrEmpty(tEmpUnqID))
            {
                err = "UserID is required..";
                return;
            }

            //simply call delete
            this.DeleteUser(tEmpUnqID, out err);
            this.StoreBlockHistory(tEmpUnqID,true);
        }

        public void UnBlockUser(string tEmpUnqID, out string err)
        {
            err = string.Empty;

            if (!_connected)
            {
                err = "Machine not connected..";
                return;
            }
            if (string.IsNullOrEmpty(tEmpUnqID))
            {
                err = "UserID is required..";
                return;
            }

            //simply call register
            this.Register(tEmpUnqID, out err);
            this.StoreBlockHistory(tEmpUnqID, false);
        }

        public void DeleteLeftEmp(out string err)
        {
            err = string.Empty;
            

            if (!_connected)
            {
                err = "Machine not connected..";
                return;
            }

            bool vRet = this.CZKEM1.ReadAllUserID(_machineno); // 'read all the user information to the memory
            if (!vRet)
            {
                err = "Error : Can not read All UserID";
                return;
            }

            UserBioInfo tmpuser = new UserBioInfo();

            string _userid, _username, _password, _cardno , selerr;
            int _prev,  _useridInt;
            bool _enabled = false;

            this.CZKEM1.EnableDevice(_machineno, false);            
            _userid = string.Empty; _username = string.Empty; _password = string.Empty; _cardno = string.Empty;
            _useridInt = 0; _prev = 0; _enabled = false;
            
            if(_istft)
            {
                while(this.CZKEM1.SSR_GetAllUserInfo(_machineno,out _userid,out _username,out _password,out _prev,out _enabled))
                {
                    tmpuser = new UserBioInfo();
                    tmpuser.UserID = _userid.ToString();
                    tmpuser.UserName = _username;
                    tmpuser.Password = _password;
                    tmpuser.Previlege = _prev;
                    tmpuser.Enabled = _enabled;

                    //3 : superadmin , 0 : normal
                    if (tmpuser.Previlege == 3)
                    {
                        continue;
                    }
                    int tActive = 0;
                    string cnt = Utils.Helper.GetDescription("Select count(*) from MastEmp Where EmpUnqID ='" + tmpuser.UserID + "' and CompCode = '01' ", Utils.Helper.constr,out selerr);
                    if (cnt != "0")
                    {
                        cnt = Utils.Helper.GetDescription("Select convert(int,isnull(Active,0)) from MastEmp Where EmpUnqID ='" + tmpuser.UserID + "' and CompCode = '01' ", Utils.Helper.constr, out selerr);
                        tActive = Convert.ToInt32(cnt);
                    }

                    if(tActive == 0)
                    {
                        string terr = string.Empty;
                        this.DeleteUser(tmpuser.UserID,out terr);
                        if (!string.IsNullOrEmpty(terr))
                        {
                            err += tmpuser.UserID + ":" + terr;
                            //check if Empunqid id Exists in MastMachineUsers if not insert it...
                            cnt = Utils.Helper.GetDescription("Select count(*) from MastMachineUsers Where EmpUnqID ='" + tmpuser.UserID + "' and MachineIP = '" + _ip + "'", Utils.Helper.constr, out selerr);
                            if (string.IsNullOrEmpty(cnt))
                            {
                                cnt = "0";
                            }

                            if (cnt == "0")
                            {
                                using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                                {
                                    try
                                    {
                                        cn.Open();
                                        string tsql = "Insert into MastMachineUsers (EmpUnqID,MachineIP,AddDt,AddID) Values " +
                                            "('" + tmpuser.UserID + "','" + _ip + "',GetDate(),'" + Utils.User.GUserID + "')";

                                        using (SqlCommand cmd = new SqlCommand(tsql, cn))
                                        {
                                            cmd.ExecuteNonQuery();
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        err += ex.ToString();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //check if Empunqid id Exists in MastMachineUsers if not insert it...
                        cnt = Utils.Helper.GetDescription("Select count(*) from MastMachineUsers Where EmpUnqID ='" + tmpuser.UserID + "' and MachineIP = '" + _ip + "'", Utils.Helper.constr, out selerr);
                        if (string.IsNullOrEmpty(cnt))
                        {
                            cnt = "0";
                        }

                        if (cnt == "0")
                        {
                            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                            {
                                try
                                {
                                    cn.Open();
                                    string tsql = "Insert into MastMachineUsers (EmpUnqID,MachineIP,AddDt,AddID) Values " +
                                        "('" + tmpuser.UserID + "','" + _ip + "',GetDate(),'" + Utils.User.GUserID + "')";

                                    using (SqlCommand cmd = new SqlCommand(tsql, cn))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }

                                }
                                catch (Exception ex)
                                {
                                    err += ex.ToString();
                                }
                            }
                        }
                    }
                }//end while
                
            }//end if new machine
            else
            {
                //old machines
                while(this.CZKEM1.GetAllUserInfo(_machineno,ref _useridInt,ref _username,ref _password,ref _prev,ref _enabled))
                {

                    tmpuser = new UserBioInfo();
                    tmpuser.UserID = _useridInt.ToString();
                    tmpuser.UserName = _username;
                    tmpuser.Password = _password;
                    tmpuser.Previlege = _prev;
                    tmpuser.Enabled = _enabled;


                    //3 : superadmin , 0 : normal
                    if (tmpuser.Previlege == 3)
                    {
                        continue;
                    }
                    int tActive = 0;
                   
                    string sql = "select case when (Select convert(int,isnull(Active,0)) from MastEmp Where EmpUnqID = '{0}' and CompCode = '01') is null then 0 " +
                        " else (Select convert(int,isnull(Active,0)) from MastEmp Where EmpUnqID = '{1}' and CompCode = '01') end";
                    sql = string.Format(sql, tmpuser.UserID, tmpuser.UserID);

                    string cnt = Utils.Helper.GetDescription(sql, Utils.Helper.constr, out selerr);
                    if (string.IsNullOrEmpty(cnt))
                        cnt = "0";

                    tActive = Convert.ToInt32(cnt);

                    if(tActive == 0)
                    {
                        string terr = string.Empty;
                        this.DeleteUser(tmpuser.UserID,out terr);
                        if (!string.IsNullOrEmpty(terr))
                        {
                            err += tmpuser.UserID + ":" + terr;
                            //check if Empunqid id Exists in MastMachineUsers if not insert it...
                            cnt = Utils.Helper.GetDescription("Select count(*) from MastMachineUsers Where EmpUnqID ='" + tmpuser.UserID + "' and MachineIP = '" + _ip + "'", Utils.Helper.constr,out selerr);
                            if (string.IsNullOrEmpty(cnt))
                            {
                                cnt = "0";
                            }
                            
                            if (cnt == "0")
                            {
                                using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                                {
                                    try
                                    {
                                        cn.Open();
                                        string tsql = "Insert into MastMachineUsers (EmpUnqID,MachineIP,AddDt,AddID) Values " +
                                            "('" + tmpuser.UserID + "','" + _ip + "',GetDate(),'" + Utils.User.GUserID + "')";

                                        using (SqlCommand cmd = new SqlCommand(tsql, cn))
                                        {
                                            cmd.ExecuteNonQuery();
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        err += ex.ToString();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        cnt = Utils.Helper.GetDescription("Select count(*) from MastMachineUsers Where EmpUnqID ='" + tmpuser.UserID + "' and MachineIP = '" + _ip + "'", Utils.Helper.constr, out selerr);
                        if (string.IsNullOrEmpty(cnt))
                            cnt = "0";

                        if (cnt == "0")
                        {
                            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                            {
                                try
                                {
                                    cn.Open();
                                    string tsql = "Insert into MastMachineUsers (EmpUnqID,MachineIP,AddDt,AddID) Values " +
                                        "('" + tmpuser.UserID + "','" + _ip + "',GetDate(),'" + Utils.User.GUserID + "')";

                                    using (SqlCommand cmd = new SqlCommand(tsql, cn))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }

                                }
                                catch (Exception ex)
                                {
                                    err += ex.ToString();
                                }
                            }
                        }
                    } 

                }

            }

            
                    
            
            this.CZKEM1.RefreshData(_machineno);
            this.CZKEM1.EnableDevice(_machineno, true);
        }

        /// <summary>
        /// this update MastMachineUsers Table which have fresh list of user for handy
        /// also delete inactive users from machine, periodically
        /// </summary>
        /// <param name="err">out string err</param>
        public void GetReFreshUsers(out string err)
        {
            err = string.Empty;


            if (!_connected)
            {
                err = "Machine not connected..";
                return;
            }

            bool vRet = this.CZKEM1.ReadAllUserID(_machineno); // 'read all the user information to the memory
            if (!vRet)
            {
                err = "Error : Can not read All UserID";
                return;
            }

            UserBioInfo tmpuser = new UserBioInfo();

            string _userid, _username, _password, _cardno;
            int _prev, _useridInt;
            bool _enabled = false;

            this.CZKEM1.EnableDevice(_machineno, false);
            _userid = string.Empty; _username = string.Empty; _password = string.Empty; _cardno = string.Empty;
            _useridInt = 0; _prev = 0; _enabled = false;

            if (_istft)
            {
                while (this.CZKEM1.SSR_GetAllUserInfo(_machineno, out _userid, out _username, out _password, out _prev, out _enabled))
                {
                    tmpuser = new UserBioInfo();
                    tmpuser.UserID = _userid.ToString();
                    tmpuser.UserName = _username;
                    tmpuser.Password = _password;
                    tmpuser.Previlege = _prev;
                    tmpuser.Enabled = _enabled;


                    //3 : superadmin , 0 : normal
                    if (tmpuser.Previlege == 3)
                    {
                        continue;
                    }
                    int tActive = 0;
                    string selerr = string.Empty;
                    string sql = "select case when (Select convert(int,isnull(Active,0)) from MastEmp Where EmpUnqID = '{0}' and CompCode = '01') is null then 0 " +
                        " else (Select convert(int,isnull(Active,0)) from MastEmp Where EmpUnqID = '{1}' and CompCode = '01') end";
                    sql = string.Format(sql, tmpuser.UserID, tmpuser.UserID);

                    string cnt = Utils.Helper.GetDescription(sql, Utils.Helper.constr,out selerr);
                    if (string.IsNullOrEmpty(cnt))
                        cnt = "0";
                    
                    tActive = Convert.ToInt32(cnt);
                    
                    if (tActive == 0)
                    {
                        string terr = string.Empty;
                        this.DeleteUser(tmpuser.UserID, out terr);

                        //if any error reinsert
                        if (!string.IsNullOrEmpty(terr))
                        {
                            //check if Empunqid id Exists in MastMachineUsers if not insert it...
                            cnt = Utils.Helper.GetDescription("Select count(*) from MastMachineUsers Where EmpUnqID ='" + tmpuser.UserID + "' and MachineIP = '" + _ip + "'", Utils.Helper.constr, out selerr);

                            if (cnt == "0")
                            {
                                using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                                {
                                    try
                                    {
                                        cn.Open();
                                        string tsql = "Insert into MastMachineUsers (EmpUnqID,MachineIP,AddDt,AddID) Values " +
                                            "('" + tmpuser.UserID + "','" + _ip + "', GetDate(),'" + Utils.User.GUserID + "')";

                                        using (SqlCommand cmd = new SqlCommand(tsql, cn))
                                        {
                                            cmd.ExecuteNonQuery();
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        err += ex.ToString();
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        //check if Empunqid id Exists in MastMachineUsers if not insert it...
                        cnt = Utils.Helper.GetDescription("Select count(*) from MastMachineUsers Where EmpUnqID ='" + tmpuser.UserID + "' and MachineIP = '" + _ip + "'", Utils.Helper.constr,out selerr);

                        if (string.IsNullOrEmpty(cnt))
                            cnt = "0";

                        if (cnt == "0")
                        {
                            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                            {
                                try
                                {
                                    cn.Open();
                                    string tsql = "Insert into MastMachineUsers (EmpUnqID,MachineIP,AddDt,AddID) Values " +
                                        "('" + tmpuser.UserID + "','" + _ip + "', GetDate(),'" + Utils.User.GUserID + "')";

                                    using (SqlCommand cmd = new SqlCommand(tsql, cn))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }

                                }
                                catch (Exception ex)
                                {
                                    err += ex.ToString();
                                }
                            }
                        }
                    }
                }//end while

            }//end if new machine
            else
            {
                //old machines
                while (this.CZKEM1.GetAllUserInfo(_machineno, ref _useridInt, ref _username, ref _password, ref _prev, ref _enabled))
                {

                    tmpuser = new UserBioInfo();
                    tmpuser.UserID = _useridInt.ToString();
                    tmpuser.UserName = _username;
                    tmpuser.Password = _password;
                    tmpuser.Previlege = _prev;
                    tmpuser.Enabled = _enabled;


                    //3 : superadmin , 0 : normal
                    if (tmpuser.Previlege == 3)
                    {
                        continue;
                    }
                    int tActive = 0;

                    string sql = "select case when (Select convert(int,isnull(Active,0)) from MastEmp Where EmpUnqID = '{0}' and CompCode = '01') is null then 0 " +
                        " else (Select convert(int,isnull(Active,0)) from MastEmp Where EmpUnqID = '{1}' and CompCode = '01') end";
                    sql = string.Format(sql, tmpuser.UserID, tmpuser.UserID);

                    string selerr= string.Empty;
                    string cnt = Utils.Helper.GetDescription(sql, Utils.Helper.constr,out selerr);
                    if (string.IsNullOrEmpty(cnt))
                        cnt = "0";
                    
                    tActive = Convert.ToInt32(cnt);
                    
                    if (tActive == 0)
                    {
                        string terr = string.Empty;
                        this.DeleteUser(tmpuser.UserID, out terr);
                        //if any error reinsert
                        if (!string.IsNullOrEmpty(terr))
                        {
                            //check if Empunqid id Exists in MastMachineUsers if not insert it...
                            cnt = Utils.Helper.GetDescription("Select count(*) from MastMachineUsers Where EmpUnqID ='" + tmpuser.UserID + "' and MachineIP = '" + _ip + "'", Utils.Helper.constr, out selerr);
                            if (string.IsNullOrEmpty(cnt))
                                cnt = "0";

                            if (cnt == "0")
                            {
                                using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                                {
                                    try
                                    {
                                        cn.Open();
                                        string tsql = "Insert into MastMachineUsers (EmpUnqID,MachineIP,AddDt,AddID) Values " +
                                            "('" + tmpuser.UserID + "','" + _ip + "', GetDate(),'" + Utils.User.GUserID + "')";

                                        using (SqlCommand cmd = new SqlCommand(tsql, cn))
                                        {
                                            cmd.ExecuteNonQuery();
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        err += ex.ToString();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //if active
                        
                        //check if Empunqid id Exists in MastMachineUsers if not insert it...
                        cnt = Utils.Helper.GetDescription("Select count(*) from MastMachineUsers Where EmpUnqID ='" + tmpuser.UserID + "' and MachineIP = '" + _ip + "'", Utils.Helper.constr, out selerr);
                        if (string.IsNullOrEmpty(cnt))
                        {
                            cnt = "0";
                        }
                        
                        if (cnt == "0")
                        {
                            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                            {
                                try
                                {
                                    cn.Open();
                                    string tsql = "Insert into MastMachineUsers (EmpUnqID,MachineIP,AddDt,AddID) Values " +
                                        "('" + tmpuser.UserID + "','" + _ip + "',GetDate(),'" + Utils.User.GUserID + "')";

                                    using (SqlCommand cmd = new SqlCommand(tsql, cn))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }

                                }
                                catch (Exception ex)
                                {
                                    err += ex.ToString();
                                }
                            }
                        }
                    }

                }

            }
            
            this.CZKEM1.RefreshData(_machineno);
            this.CZKEM1.EnableDevice(_machineno, true);

        }

        public string PingMachine(out string err)
        {
            string status = string.Empty;
            err = string.Empty;

            if(string.IsNullOrEmpty(_ip))
            {
                err = "IP Address is required..";
                status = "Bad Request";
            
                return status;
            }

            try
            {
                Ping myPing = new Ping();
                PingReply reply = myPing.Send(_ip, 2000);

                if (reply.Status == IPStatus.Success)
                {
                    status = "Success";
                }
                else
                {
                    status = reply.Status.ToString();
                }
                
            }
            catch(Exception ex)
            {
                status = "Request timeout";
                err = ex.ToString();
            }

            return status;
        }

        public void DeleteLeftEmp_NEW(out string err)
        {
            err = string.Empty;


            if (!_connected)
            {
                err = "Machine not connected..";
                return;
            }

            bool vRet = this.CZKEM1.ReadAllUserID(_machineno); // 'read all the user information to the memory
            if (!vRet)
            {
                err = "Error : Can not read All UserID";
                return;
            }


            List<UserBioInfo> tUserList = new List<UserBioInfo>();
            UserBioInfo tmpuser = new UserBioInfo();
            string _userid, _username, _password, _cardno;
            int _prev, _useridInt;
            bool _enabled = false;

            this.CZKEM1.EnableDevice(_machineno, false);

            #region collectusers

            _userid = string.Empty; _username = string.Empty; _password = string.Empty; _cardno = string.Empty;
            _useridInt = 0; _prev = 0; _enabled = false;

            if (_istft)
            {
                while (this.CZKEM1.SSR_GetAllUserInfo(_machineno, out _userid, out _username, out _password, out _prev, out _enabled))
                {
                    tmpuser = new UserBioInfo();
                    tmpuser.UserID = _userid.ToString();                    
                    tmpuser.MessCode = _ip;

                    //3 : superadmin , 0 : normal
                    if (tmpuser.Previlege == 3)
                    {
                        continue;
                    }

                    tUserList.Add(tmpuser);
                    
                }//end while

            }//end if new machine
            else
            {
                //old machines
                while (this.CZKEM1.GetAllUserInfo(_machineno, ref _useridInt, ref _username, ref _password, ref _prev, ref _enabled))
                {

                    tmpuser = new UserBioInfo();
                    tmpuser.UserID = _useridInt.ToString();
                    tmpuser.UserName = _username;
                    tmpuser.MessCode = _ip;


                    //3 : superadmin , 0 : normal
                    if (tmpuser.Previlege == 3)
                    {
                        continue;
                    }

                    tUserList.Add(tmpuser);                   
                   
                }
            }
            
            #endregion 

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                string sql = string.Empty;
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cn.Open();
                    sql = "truncate  table t1 ";
                    cmd = new SqlCommand(sql,cn);
                    cmd.ExecuteNonQuery();
                    
                    foreach (UserBioInfo t in tUserList)
                    {
                        sql = "Insert into t1 (EmpUnqID,MachineIP,t1Date,Flg) values ('" + t.UserID + "','" + t.MessCode + "','2018-01-01',0);";
                        cmd = new SqlCommand(sql, cn);
                        cmd.ExecuteNonQuery();
                    }

                    sql = "Update a Set a.Flg = b.Active " +
                        " From t1 a, MastEmp b Where a.EmpUnqID = b.EmpUnqID ";
                    cmd = new SqlCommand(sql, cn);
                    cmd.ExecuteNonQuery();

                    sql = "Delete From MastMachineUsers Where MachineIP='" + _ip + "'  ";
                    cmd = new SqlCommand(sql, cn);
                    cmd.ExecuteNonQuery();

                    sql = "Insert into MastMachineUsers (MachineIP,EmpUnqID,AddID,AddDt) " +
                        " Select MachineIP,EmpUnqID,'" + Utils.User.GUserID + "',GetDate() From t1 Where MachineIP='" + _ip + "' and Flg = 1";
                    cmd = new SqlCommand(sql, cn);
                    cmd.ExecuteNonQuery();

                    DataSet ds = Utils.Helper.GetData("Select EmpUnqID From t1 where Flg = 0", Utils.Helper.constr, out err);
                    bool hasrows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                    if (hasrows)
                    {
                        
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string tEmpUnqID = dr["EmpUnqID"].ToString();
                            string tmpuser2 = string.Empty, tmppass2 = string.Empty;
                            int tmppre2 = 0;
                            bool tmpenable2 = false;


                            if (!_istft)
                            {
                                this.CZKEM1.DeleteEnrollData(_machineno, Convert.ToInt32(tEmpUnqID), _machineno, 0);
                            }
                            else
                            {
                                if (this.CZKEM1.SSR_GetUserInfo(_machineno, tEmpUnqID, out tmpuser2, out tmppass2, out tmppre2, out tmpenable2))
                                {
                                    if (tmppre2 != 3)
                                    {
                                        //this.CZKEM1.SSR_DeleteEnrollData(_machineno, tEmpUnqID, 0);
                                        //this.CZKEM1.DeleteEnrollData(_machineno, Convert.ToInt32(emp.UserID), _machineno, 0);
                                        this.CZKEM1.SSR_DeleteEnrollDataExt(_machineno, tEmpUnqID, 12);
                                        this.CZKEM1.DelUserFace(_machineno, tEmpUnqID, 50);
                                    }

                                }
                                
                                //this.CZKEM1.SSR_DeleteEnrollData(_machineno, tEmpUnqID, 0);                                
                                //this.CZKEM1.DelUserFace(_machineno, tEmpUnqID, 50);
                                //this.CZKEM1.SSR_DelUserTmpExt(_machineno, tEmpUnqID, 13);
                            }             
                            
                        }
                    }

                    sql = "truncate  table t1 ";
                    cmd = new SqlCommand(sql, cn);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    err = ex.ToString();
                   
                }

            }// using connection

            this.CZKEM1.EnableDevice(_machineno, true);

        }

    }
}
