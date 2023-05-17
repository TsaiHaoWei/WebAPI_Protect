using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private List<Product> products = new List<Product> {
            new Product("D001","珍珠奶茶","Taiwan",50),
              new Product("F001","大波霸","Japan",500),
                new Product("F002","loli","Japan",250),
                  new Product("B001","SideProject","Taiwan",100)
        };
        private List<Product> GetProductList()
        {
            return products;
        }
        /*
         [HttpGet] 這個標籤表示這個方法要用HttpGet的方法取得
        [Route("getProduct")] 表示getProduct會轉進這個方法處理 EX:127.0.0.1/api/product/getProduct
         */
        [HttpGet]
        [Route("getProduct")]
        public List<Product> GetProducts()
        {
            return GetProductList();
        }
        [HttpPost]
        [Route("CreateProduct")]
        public string CreateProduct(Product product)
        {
            products.Add(product);
            return $"{JsonConvert.SerializeObject(product)}商品已加入清單";
        }
    }
    

   
}
