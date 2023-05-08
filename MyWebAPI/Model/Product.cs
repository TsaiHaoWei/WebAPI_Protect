using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebAPI.Model
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public int Price { get; set; }
        public Product(string A1, string A2, string A3, int A4)
        {
            Id = A1;
            Name = A2;
            Origin = A3;
            Price = A4;
        }
    }
}
