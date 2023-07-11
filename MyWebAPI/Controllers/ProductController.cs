using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.DataBase;
using MyWebAPI.Model;
using MyWebAPI.Model.UserModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]//網站位址 EX:127.0.0.1/api/product
    [ApiController] //Controler會出現的
    public class ProductController : ControllerBase
    {
        //private static List<Product> products = new List<Product>
        //{
        //    new Product{Id = "D001", Name = "珍珠奶茶",Origin = "Taiwan",Price = 50},
        //    new Product{Id = "F001", Name = "香蕉",Origin = "Taiwan",Price = 20},
        //    new Product { Id = "F002", Name = "蘋果", Origin = "Japan", Price = 50 },
        //    new Product { Id = "B001", Name = "工程師的自我修養", Origin = "US", Price = 200 }
        //};
        /*
         [HttpGet] 這個標籤表示這個方法要用HttpGet的方法取得
        [Route("getProduct")] 表示getProduct會轉進這個方法處理 EX:127.0.0.1/api/product/getProduct
         */
        private static DBAccess DBController = null;
     
        public void DBAccess()
        {
             DBController = new DBAccess();
        }      
        
        [HttpGet]
        [Route("getProduct")]
        public List<Product> GetProducts()
        {
            DBAccess();
            return DBController.getProduct();
        }
        
        [HttpPost]
        [Route("CreateProduct")]
        public string CreateProduct(ProductUserModel product)
        {
            DBAccess();

            List<Product> listProduct = new List<Product>();
            listProduct.Add(new Product { ProductID = String.Format("{0:0000}", DBUtility.StroreProcedure("ProductID")), ProductName = product.ProductName });
            //PName.Add(product.ProductName);

            if (DBController.insertProduct(listProduct))
                return $"{JsonConvert.SerializeObject(product)}商品已加入清單";
            else
                return "資料庫錯誤";
        }
        [HttpGet]
        [Route("getProductPrice")]
        public List<P_MarketPrice> GetProductPrice()
        {
            DBAccess();
            return DBController.getProductPrice();
        }

        [HttpPost]
        [Route("CreateProductPrice")]
        public string CreateProduct(P_MarketPriceUserModel product)
        {
            DBAccess();

            List<P_MarketPrice> listP_Price = new List<P_MarketPrice>();
            listP_Price.Add(new P_MarketPrice
            {
                ProductID = DBController.getProduct().Find(var => var.ProductName.Equals(product.ProductName)).ProductID,
                Price = int.Parse(product.Price),
                Location = product.Location,
                Capacity =int.Parse(product.Capacity),
                Price_100g =(int)Convert.ToDouble(int.Parse(product.Price)*100) / int.Parse(product.Capacity) 
            });

            if (DBController.insertProductPrice(listP_Price))
                return $"{JsonConvert.SerializeObject(product)}商品已加入清單";
            else
                return "資料庫錯誤";
        }
    }



}
