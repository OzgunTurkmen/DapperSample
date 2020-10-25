using System;
using System.Collections.Generic;
using System.Text;

namespace Console.IntroApp.Model
{
    public class Product
    {
        public Product()
        {
            ProductCategory = new List<ProductCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public List<ProductCategory> ProductCategory { get; set; }
    }
}
