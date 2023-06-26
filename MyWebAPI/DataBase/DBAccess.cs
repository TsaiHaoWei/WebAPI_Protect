using Dapper;
using MyWebAPI.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MyWebAPI.DataBase
{
    public class DBAccess
    {
        private string _SysConnstring = string.Empty;
        public DBAccess() {
            DBUtility.DBConnection();
            _SysConnstring = DBUtility.SysConnectString;
        }
        public List<Product> getProduct()
        {
            using (SqlConnection Sysconn = new SqlConnection(_SysConnstring))
            {
                try
                {
                    return Sysconn.Query<Product>("select * from [Product]").ToList();

                }
                catch
                { 
                    return new List<Product>();
                }
            }
        }
    }
}
