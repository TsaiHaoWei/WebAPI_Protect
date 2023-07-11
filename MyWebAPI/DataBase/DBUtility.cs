using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text.Encodings.Web;
using Dapper;
using System.Data;

namespace MyWebAPI.DataBase
{
    public class DBUtility
    {
        public static string AppPath = System.AppDomain.CurrentDomain.BaseDirectory;
        public static string SysConnectString = string.Empty;
        public static Dictionary<string, string> DBSetup()
        {

            Dictionary<string, string> DBKey = new Dictionary<string, string>();
            DBKey.Add("ServerName", ExternalFile.GetProfileString("SERVER", "ServerName", "DataServer", SystemParameterPath.iniFile));
            DBKey.Add("Database", ExternalFile.GetProfileString("SERVER", "Database", "DataServer", SystemParameterPath.iniFile));
            DBKey.Add("LogId", ExternalFile.GetProfileString("SERVER", "LogId", "DataServer", SystemParameterPath.iniFile));
            DBKey.Add("LogPass", ExternalFile.GetProfileString("SERVER", "LogPass", "DataServer", SystemParameterPath.iniFile));
            return DBKey;
        }
        public static void DBConnection()
        {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = DBSetup()["ServerName"];
            sqlBuilder.InitialCatalog = DBSetup()["Database"];
            sqlBuilder.UserID = DBSetup()["LogId"];
            sqlBuilder.Password = DBSetup()["LogPass"]; 
            sqlBuilder.ApplicationName = "MVCDataBase";
            SysConnectString = sqlBuilder.ToString();

            string PrintFIle = string.Empty;
            PrintFIle += $"SystemPath : DBPath = {SystemParameterPath.iniFile} \t";
            PrintFIle += File.Exists(SystemParameterPath.iniFile) ? "Exist \n" : "No Exist \n";

            PrintFIle += $"DatRead = {SystemParameterPath.ConfigFilePath}\t";
            PrintFIle += File.Exists(SystemParameterPath.ConfigFilePath) ? "Exist \n" : "No Exist \n";

            PrintFIle += $"TcpFile = {SystemParameterPath.TcpIniFile}\t";
            PrintFIle += File.Exists(SystemParameterPath.TcpIniFile) ? "Exist \n" : "No Exist \n";

            Debug.WriteLine(PrintFIle + SysConnectString);
            if (DBOpen(SysConnectString))
                Debug.WriteLine("DB Access Success");
            else
                Debug.WriteLine("DB Access Failure");

            //Console.WriteLine($"SystemPath : DBPath = {SystemParameterPath.iniFile} DatRead = {SystemParameterPath.ConfigFilePath} TcpFile = {SystemParameterPath.TcpIniFile}");

        }
        public static bool DBOpen(string SysConnectString)
        {
            //if (sqlcon.State != ConnectionState.Open)
            //    sqlcon.Open();
            bool bRet = false;

            using (SqlConnection SysConn = new SqlConnection(SysConnectString))
            {
                try
                {
                    SysConn.Open();
                    bRet = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return bRet;
        }
        public static void DBError(string ErrorString)
        {


            //檔案名稱 使用現在日期
            String logFileName = DateTime.Now.Year.ToString() + int.Parse(DateTime.Now.Month.ToString()).ToString("00") + int.Parse(DateTime.Now.Day.ToString()).ToString("00") + ".txt";

            //Log檔內的時間 使用現在時間
            String nowTime = int.Parse(DateTime.Now.Hour.ToString()).ToString("00") + ":" + int.Parse(DateTime.Now.Minute.ToString()).ToString("00") + ":" + int.Parse(DateTime.Now.Second.ToString()).ToString("00");
            if (!Directory.Exists(SystemParameterPath.AppPath+"\\Log"))
            {
                //建立資料夾
                Directory.CreateDirectory(SystemParameterPath.AppPath + "\\Log");
            }
            string path = SystemParameterPath.AppPath + "\\Log\\";

            if (!File.Exists(path + logFileName))
            {
                //建立檔案
                File.Create(path + logFileName).Close();
            }

            using (StreamWriter sw = File.AppendText(path + logFileName))
            {
                //WriteLine為換行 
                sw.Write(nowTime + "---->");
                sw.WriteLine(ErrorString);
                sw.WriteLine("");
            }
        }
        public static int StroreProcedure(string room)
        {
            try
            {
                DynamicParameters parm = new DynamicParameters();
                //intput
                parm.Add("@Room", room, dbType: DbType.String);
                //output
                parm.Add("@SeqNo", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                using (SqlConnection SysConn = new SqlConnection(SysConnectString))
                {
                    SysConn.Execute("GetSequence", parm, commandType: CommandType.StoredProcedure);
                }
                return parm.Get<int>("@SeqNo");
            }
            catch
            {
                return -1;
            }
        }






    }
}
