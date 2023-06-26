using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text.Encodings.Web;

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
  

    }
}
