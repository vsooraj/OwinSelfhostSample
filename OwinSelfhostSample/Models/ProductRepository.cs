using System.Collections.Generic;
using System.Linq;

namespace OwinSelfhostSample.Models
{
    public class ProductRepository
    {
        private static List<Product> _products = new List<Product>()
            {
                new Product { ProductId = 1, ProductName="Lumia",Price=12000.00M ,Quantity=100, Type="Mobile" },
                new Product { ProductId = 2, ProductName="Nexus",Price=22000.00M ,Quantity=100, Type="Mobile" },
                new Product { ProductId = 3, ProductName="iPhone6",Price=32000.00M ,Quantity=100, Type="Mobile" },
                new Product { ProductId = 4, ProductName="MicroMax",Price=42000.00M ,Quantity=100, Type="Mobile" },
                new Product { ProductId = 5, ProductName="Honor",Price=12000.00M ,Quantity=100, Type="Mobile" },
                new Product { ProductId = 6, ProductName="Huwai",Price=12000.00M ,Quantity=100, Type="Mobile" },
                new Product { ProductId = 7, ProductName="Sony",Price=12000.00M ,Quantity=100, Type="Mobile" },
                new Product { ProductId = 8, ProductName="Samsung",Price=12000.00M ,Quantity=100, Type="Mobile" },
                new Product { ProductId = 9, ProductName="Alcatel",Price=12000.00M ,Quantity=100, Type="Mobile" },
                new Product { ProductId = 10, ProductName="IBall",Price=12000.00M ,Quantity=100, Type="Mobile" },
                new Product { ProductId = 11, ProductName="Lumia",Price=12000.00M ,Quantity=100, Type="Mobile" },
                new Product { ProductId = 12, ProductName="Lumia",Price=12000.00M ,Quantity=100, Type="Mobile" },
                new Product { ProductId = 13, ProductName="Lumia",Price=12000.00M ,Quantity=100, Type="Mobile" },
                new Product { ProductId = 14, ProductName="Lumia",Price=12000.00M ,Quantity=100, Type="Mobile" },
                new Product { ProductId = 15, ProductName="Lumia",Price=12000.00M ,Quantity=100, Type="Mobile" }
            };
        public IQueryable<Product> Products
        {
            get { return _products.AsQueryable(); }
        }
    };

}

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Type { get; set; }
}

