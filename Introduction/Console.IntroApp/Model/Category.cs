using System.Collections.Generic;

namespace Console.IntroApp.Model
{
    public class Category
    {
        public Category()
        {
            ProductCategory = new List<ProductCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductCategory> ProductCategory { get; set; }

    }
}
