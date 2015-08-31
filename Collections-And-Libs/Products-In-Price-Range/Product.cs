using System;
namespace Products_In_Price_Range
{
    public class Product : IComparable<Product>
    {
        public Product(string name, decimal price)
        {
            ProductName = name;
            Price = price;
        }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int CompareTo(Product other)
        {
            return this.Price > other.Price ? 1 : 
                    this.Price < other.Price ? -1 : 0;
        }
    }
}
