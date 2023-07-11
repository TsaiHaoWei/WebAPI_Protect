using Dapper;
using MyWebAPI.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
                catch(Exception ex)
                {
                    DBUtility.DBError(ex.ToString());
                    return new List<Product>();
                }
            }
        }
        public bool insertProduct(List<Product> listProdcut)
        {
            using (SqlConnection Sysconn = new SqlConnection(_SysConnstring))
            {
                try{                 
                    var insert = "Insert into Product values(@ProductID,@ProductName)";
                    Sysconn.Execute(insert, listProdcut);
                    return true;

                }
                catch (Exception ex)
                {
                    DBUtility.DBError(ex.ToString());
                    return false;
                }
            }
        }
        public List<P_MarketPrice> getProductPrice()
        {
            using (SqlConnection Sysconn = new SqlConnection(_SysConnstring))
            {
                try
                {
                    return Sysconn.Query<P_MarketPrice>("select * from Product_MarketPrice").ToList();

                }
                catch (Exception ex)
                {
                    DBUtility.DBError(ex.ToString());
                    return new List<P_MarketPrice>();
                }
            }
        }
        public bool insertProductPrice(List<P_MarketPrice> listProductPrice)
        {
            using (SqlConnection Sysconn = new SqlConnection(_SysConnstring))
            {
                try
                {
                    var insert = "Insert into [Product_MarketPrice] values(@ProductID,@Price,@Location,@Capacity,@Price_100g)";
                    Sysconn.Execute(insert, listProductPrice);
                    return true;

                }
                catch (Exception ex)
                {
                    DBUtility.DBError(ex.ToString());
                    return false;
                }
            }
        }
    }
}
