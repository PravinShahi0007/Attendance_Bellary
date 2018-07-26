using Attendance.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Utils
{
    
    
    class Helper
    {
        //public static string DbConstr = Properties.Settings.Default.dbConn.ToString();
        public static string confile = "connection.xml";
        public static string empconfile = "empconnection.xml";
        public static string constr = string.Empty;
        public static string Empconstr = string.Empty;

        static public BindingSource SampleListbds = new BindingSource();
        static public DataSet SampleListDataSet = new DataSet();

        static public string GetLogFilePath()
        {
            string dir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            dir = System.IO.Path.Combine(dir, "PunchLogData");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }

        static public string GetTraceFilePath()
        {
            string dir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            dir = System.IO.Path.Combine(dir, "Trace");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }


        static public string GetErrLogFilePath()
        {
            string dir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            dir = System.IO.Path.Combine(dir, "ErrLog");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }

        static public string GetInfoLogFilePath()
        {
            string dir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            dir = System.IO.Path.Combine(dir, "InfoLog");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }

        static public string GetUserDataPath()
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            dir = System.IO.Path.Combine(dir, "AttendanceSystemBEL");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }

        /// <summary>
        /// Convert Image to Byte[]
        /// </summary>
        /// <param name="imageToConvert"></param>
        /// <param name="formatOfImage"></param>
        /// <returns></returns>
        public byte[] ConvertImageToBytes(System.Drawing.Image imageToConvert,
                                         System.Drawing.Imaging.ImageFormat formatOfImage)
        {
            byte[] Ret;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    Ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }
            return Ret;
        }
        /// <summary>
        /// Convert Byte[] to image
        /// </summary>
        /// <param name="byteArrayIn"></param>
        /// <returns></returns>
        public Image ConvertBytesToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }


        public static bool WriteConDb(DbCon cnstr,string typeofcon)
        {
            string filepath = string.Empty;
            switch (typeofcon.ToUpper())
            {
                case "DBCON":
                    filepath = System.IO.Path.Combine(GetUserDataPath(), confile);
                    break;
                case "EMPDBCON" :
                    filepath = System.IO.Path.Combine(GetUserDataPath(), empconfile);
                    break;
                default:
                    filepath = System.IO.Path.Combine(GetUserDataPath(), confile);
                    break;
            }
               
       
            System.Xml.Serialization.XmlSerializer writer = 
                new System.Xml.Serialization.XmlSerializer(typeof(DbCon));

            try { 
                System.IO.StreamWriter file = new System.IO.StreamWriter(filepath);
                writer.Serialize(file, cnstr );
                file.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            
        }
                
        public static DbCon ReadConDb(string typeofcon)
        {


            string filepath = string.Empty;
            switch (typeofcon.ToUpper())
            {
                case "DBCON":
                    filepath = System.IO.Path.Combine(GetUserDataPath(), confile);
                    break;
                case "EMPDBCON":
                    filepath = System.IO.Path.Combine(GetUserDataPath(), empconfile);
                    break;
                default:
                    filepath = System.IO.Path.Combine(GetUserDataPath(), confile);
                    break;
            }
           

            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(DbCon));

            try
            {
                // Now we can read the serialized book ...
                System.Xml.Serialization.XmlSerializer reader =
                    new System.Xml.Serialization.XmlSerializer(typeof(DbCon));
                System.IO.StreamReader file = new System.IO.StreamReader(
                    filepath);
                DbCon overview = (DbCon)reader.Deserialize(file);
                file.Close();

                return overview;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                DbCon blank = new DbCon();
                return blank ;
            }

        }
        
        public static DataSet GetData(string sql, string ConnectionString)
        {
            DataSet Result = new DataSet();
            if (string.IsNullOrEmpty(sql))
            {
                return Result;
            }

            if (string.IsNullOrEmpty(ConnectionString))
            {
                return Result;
            }


            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand(sql, conn) { CommandType = CommandType.Text };
            SqlDataAdapter da = new SqlDataAdapter();


            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                da.SelectCommand = command;
                da.Fill(Result, "RESULT");
                conn.Close();
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message.ToString()); }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally
            {
                conn.Close();
            }

            return Result;
        }

        public static DataSet GetData(string sql, string ConnectionString,out string err)
        {
            err = string.Empty;
            DataSet Result = new DataSet();
            if (string.IsNullOrEmpty(sql))
            {
                err = "Query is not defined";
                return Result;
            }

            if (string.IsNullOrEmpty(ConnectionString))
            {
                err = "Connection String is not defined";
                return Result;
            }


            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand(sql, conn) { CommandType = CommandType.Text };
            SqlDataAdapter da = new SqlDataAdapter();


            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                da.SelectCommand = command;
                da.Fill(Result, "RESULT");
                conn.Close();
            }
            catch (SqlException ex) { err = ex.Message.ToString(); }
            catch (Exception ex) { err = ex.Message.ToString(); }
            finally
            {
                if(conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return Result;
        }

        public static List<string> GetLocalIPAddress()
        {
            List<string> t = new List<string>();
            
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    t.Add(ip.ToString());
                }
            }
            return t;        
        }

        public static string GetLocalPCName()
        {
            
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host.HostName;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql">Select Single Column with single row result</param>
        /// <param name="ConnectionString">sql connection</param>
        /// <returns>string</returns>
        public static string GetDescription(string sql, string ConnectionString)
        {
            object result;

            string returndesc = string.Empty;
            if (string.IsNullOrEmpty(sql))
            {
                return returndesc;
            }

            if (string.IsNullOrEmpty(ConnectionString))
            {
                return returndesc;
            }

            if (sql.Contains("insert"))
            {
                return returndesc;
            }
            if (sql.Contains("update"))
            {
                return returndesc;
            }
            if (sql.Contains("delete"))
            {
                return returndesc;
            }

            if (!sql.Contains("TOP 1"))
            {
                sql = sql.ToUpper().Replace("SELECT", "SELECT TOP 1 ");
            }

            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand(sql, conn) { CommandType = CommandType.Text };
            

            try
            {
                conn.Open();
                result = command.ExecuteScalar();    
                if(result != null)           
                    returndesc = Convert.ToString(result);

                conn.Close();
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message.ToString()); }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally
            {
                conn.Close();
            }

            return returndesc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql">Select Single Column with single row result</param>
        /// <param name="ConnectionString">sql connection</param>
        /// <returns>string</returns>
        public static string GetDescription(string sql, string ConnectionString,out string err)
        {
            object result;
            err = string.Empty;

            string returndesc = string.Empty;
            if (string.IsNullOrEmpty(sql))
            {
                return returndesc;
            }

            if (string.IsNullOrEmpty(ConnectionString))
            {
                return returndesc;
            }

            if (sql.Contains("insert"))
            {
                return returndesc;
            }
            if (sql.Contains("update"))
            {
                return returndesc;
            }
            if (sql.Contains("delete"))
            {
                return returndesc;
            }

            if (!sql.Contains("TOP 1"))
            {
                sql = sql.ToUpper().Replace("SELECT", "SELECT TOP 1 ");
            }

            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand(sql, conn) { CommandType = CommandType.Text };


            try
            {
                conn.Open();
                result = command.ExecuteScalar();
                if (result != null)
                    returndesc = Convert.ToString(result);

                conn.Close();
            }
            catch (SqlException ex) { err = ex.Message.ToString(); }
            catch (Exception ex) { err = ex.Message.ToString(); }
            finally
            {
                conn.Close();
            }

            return returndesc;
        }

        public static bool IsFrmAlreadyOpen(Type formType)
        {
            bool isOpen = false;
            foreach (Form f in Application.OpenForms)
            {

                if (f.GetType() == formType)
                {

                    f.BringToFront();
                    f.WindowState = FormWindowState.Maximized;
                    isOpen = true;
                }

            }
            return isOpen;
        }

        /// <summary>
        /// by Anand Acharya
        /// </summary>
        /// <param name="ModID">int moduleid</param>
        /// <param name="userid">string loggedin userid</param>
        /// <param name="FieldName">string FieldName eg.(ADD1,Update1,DELETE1,VIEW1)</param>
        /// <returns>boolean from Fieldvalue </returns>
        public static bool GetModuleRights(int ModID, string userid, string FieldName,string connectionstr)
        {
            bool result = false;

            string tablename = "UserRights";
            string sql = "Select " + FieldName + " From " + tablename + " where ModID ='" + ModID.ToString().Trim() + "' and UserID ='" + userid + "'";
            using(SqlConnection cn = new SqlConnection(connectionstr))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(sql,cn);
                    result = (bool)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {

                }
            }

            return result;
        }
    }

    public class DbCon 
    {
        private string _DbName;
        private string _DataSource;
        private string _DbUser;
        private Boolean _WinAuth;
        private string _DbPass;

        //Data Source=172.16.12.14;Initial Catalog=Dia;User ID=ipu_dia

       

        public string DataSource
        {
             get { return _DataSource; }
            set { _DataSource = value; }
        }

        public string DbName
        {
            get { return _DbName; }
            set { _DbName = value; }
        }

        public  bool WindowsAuthentication
        {
             get { return _WinAuth; }
            set { 
                _WinAuth = value;
                if (value)
                {
                    _DbPass = "";
                    _DbUser = "";
                    this.DbUser = _DbUser;
                    this.Password = _DbPass;
                }            
            }
        }

        public  string DbUser
        {
             get { return _DbUser; }
            set { _DbUser = value; }
        }

        public string Password
        {
            get { return _DbPass; }
            set { _DbPass = value; }
        }

        public DbCon()
        {
            _DbName = "";
            _DbUser = "";
            _DataSource = "";
            _WinAuth = false;
            _DbPass = "";

            this.DataSource = _DataSource;
            this.DbName = _DbName;
            this.DbUser = _DbUser;
            this.Password = _DbPass;
            this.WindowsAuthentication = _WinAuth;
        }

        public override string ToString()
        {
 	         string str = string.Empty;
            if(_WinAuth){
                str += "Data Source = " + _DataSource + ";Initial Catalog=" + _DbName + ";Integrated Security=SSPI;";
            }else{
                str += "Data Source = " + _DataSource + ";Initial Catalog=" + _DbName + ";User ID=" + _DbUser + ";Password="+ _DbPass + ";";
            }
            return str;

        }
    }

    public static class User
    {

        /// Static value protected by access routine.
        static string _GUserID;
        static string _GUserName;       
        static string _GUserPass;
        
        public static List<string> GUserApp = new List<string>();

        public static string GUserPass
        {
            get { return _GUserPass; }
            set { _GUserPass = value; }
        }

        /// Access routine for global variable.
        public static string GUserID
        {
            get
            {
                return _GUserID;
            }
            set
            {
                _GUserID = value;
            }
        }

        /// Access routine for global variable.
        public static string GUserName
        {
            get
            {
                return _GUserName;
            }
            set
            {
                _GUserName = value;
            }
        }

        public static bool IsAdmin;

    }


    public static class DomainUserConfig
    {
        static string _DmnName;
        static string _DmnUser;
        static string _DmnPass;
        static string _NetworkShare;

        public static string DomainUser
        {
            get { return _DmnUser; }
            set { _DmnUser = value; }
        }

        public static string DomainPassword
        {
            get { return _DmnPass; }
            set { _DmnPass = value; }
        }

        public static string DomainName
        {
            get { return _DmnName; }
            set { _DmnName = value; }
        }

        public static string NetworkShare
        {
            get { return _NetworkShare; }
            set { _NetworkShare = value; }
        }
       

    }
   
    //bool hasrows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

    public static class EmailConfig
    {
        /// Static value protected by access routine.
        static string _HODGrpEMailID;
        static string _AccountEMailID;
        static string _AccountUser;
        static string _AccountPass;
        static string _Host;

        public static string AccountPass
        {
            get { return _AccountPass; }
            set { _AccountPass = value; }
        }

       
        public static string AccountUser
        {
            get
            {
                return _AccountUser;
            }
            set
            {
                _AccountUser = value;
            }
        }


        public static string HodGrpEmailID
        {
            get
            {
                return _HODGrpEMailID;
            }
            set
            {
                _HODGrpEMailID = value;
            }
        }

        public static string AccountEMailID
        {
            get
            {
                return _AccountEMailID;
            }
            set
            {
                _AccountEMailID = value;
            }
        }

        public static string Host
        {
            get
            {
                return _Host;
            }
            set
            {
                _Host = value;
            }
        }

    }

   
}
