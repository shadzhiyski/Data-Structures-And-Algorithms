namespace Products_In_Price_Range
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    class Program
    {
        static void Main(string[] args)
        {
            var products = new OrderedBag<Product>();
            products.AddMany(
                new Product[] 
                {
                    new Product("Grabage", 24),
                    new Product("Grabage 2", 22),
                    new Product("Grabage 3", 290),
                    new Product("Grabage 4", 29)
                });
            var result = products.Range(
                new Product("", 20), true, 
                new Product("", 30), true);

            foreach (var item in result)
            {
                Console.WriteLine(item.ProductName);
            }

        }
    }
}
