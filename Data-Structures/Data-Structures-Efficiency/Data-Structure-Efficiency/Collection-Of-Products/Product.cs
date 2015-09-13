using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collection_Of_Products
{
    public class Product : IComparable<Product>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Supplier { get; set; }

        public decimal Price { get; set; }

        public int CompareTo(Product other)
        {
            return Id.CompareTo(other.Id);
        }
    }
}
