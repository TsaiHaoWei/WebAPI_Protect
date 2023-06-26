using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.DataBase;
using MyWebAPI.Model;
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
        public string CreateProduct(Product product)
        {
            //products.Add(product);
            return $"{JsonConvert.SerializeObject(product)}商品已加入清單";
        }
    }
    

   
}
