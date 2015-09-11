using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Collection_Of_Products
{
    public class ProductsCollection
    {
        private Dictionary<string, OrderedMultiDictionary<decimal, Product>> productsBySupplier =
            new Dictionary<string, OrderedMultiDictionary<decimal, Product>>();
        private Dictionary<string, OrderedMultiDictionary<decimal, Product>> productsByTitle =
            new Dictionary<string, OrderedMultiDictionary<decimal, Product>>();
        private Dictionary<int, Product> productsById =
            new Dictionary<int, Product>();

        public string AddProduct(int id, string title, decimal price, string supplier)
        {
            if (productsById.ContainsKey(id))
            {
                productsById[id].Title = title;
                productsById[id].Supplier = supplier;
                productsById[id].Price = price;

                return "Product Edited.";
            }
            else
            {
                var newProduct = new Product()
                {
                    Title = title,
                    Supplier = supplier,
                    Price = price
                };

                productsById.Add(id, newProduct);

                if (!this.productsByTitle.ContainsKey(title))
                {
                    this.productsByTitle.Add(
                        title, new OrderedMultiDictionary<decimal, Product>(true));
                }

                this.productsByTitle[title].Add(price, newProduct);

                if (!this.productsBySupplier.ContainsKey(supplier))
                {
                    this.productsByTitle.Add(
                        supplier, new OrderedMultiDictionary<decimal, Product>(true));
                }

                this.productsBySupplier[supplier].Add(price, newProduct);
                return "Product Added.";
            }
        }

        public string FindProductsByTitle(string title)
        {
            ICollection<Product> productsFound = new List<Product>();
            if (this.productsByTitle.ContainsKey(title))
            {
                productsFound = this.productsByTitle[title].Values;
            }
            return PrintProducts(productsFound);
        }

        private string PrintProducts(IEnumerable<Product> products)
        {
            if (products.Any())
            {
                var sortedProducts = products.OrderBy(p => p);
                return string.Join(Environment.NewLine, sortedProducts);
            }

            return "No products found";
        }

        public string FindProductsByTitleAndPrice(string title, decimal price)
        {
            ICollection<Product> productsFound = new List<Product>();
            if (this.productsByTitle.ContainsKey(title))
            {
                productsFound = productsByTitle[title]
                    .Range(price, true, price, true).Values;
            }

            return PrintProducts(productsFound);
        }

        public string FindProductsByTitleAndPriceRange(string title,
            decimal startPrice, decimal endPrice)
        {
            ICollection<Product> productsFound = new List<Product>();
            if (this.productsByTitle.ContainsKey(title))
            {
                productsFound = this.productsByTitle[title]
                    .Range(startPrice, true, endPrice, true).Values;
            }
            return PrintProducts(productsFound);
        }

        public string FindProductsBySupplierAndPrice(string supplier, decimal price)
        {
            ICollection<Product> productsFound = new List<Product>();
            if (this.productsBySupplier.ContainsKey(supplier))
            {
                productsFound = productsBySupplier[supplier]
                    .Range(price, true, price, true).Values;
            }

            return PrintProducts(productsFound);
        }

        public string FindProductsBySupplierAndPriceRange(string supplier,
            decimal startPrice, decimal endPrice)
        {
            ICollection<Product> productsFound = new List<Product>();
            if (this.productsBySupplier.ContainsKey(supplier))
            {
                productsFound = this.productsBySupplier[supplier]
                    .Range(startPrice, true, endPrice, true).Values;
            }
            return PrintProducts(productsFound);
        }

        public string DeleteProductById(int id)
        {
            Product product = null;
            if (productsById.ContainsKey(id))
            {
                product = productsById[id];
                productsByTitle[product.Title][product.Price].Remove(product);
                productsBySupplier[product.Supplier][product.Price].Remove(product);

                return "Product Deleted.";
            }
            else
            {
                return "Product not found.";
            }
        }

        public string ProcessCommand(string commandLine)
        {
            int spaceIndex = commandLine.IndexOf(' ');
            if (spaceIndex == -1)
            {
                return "Invalid command";
            }

            string command = commandLine.Substring(0, spaceIndex);
            string paramsStr = commandLine.Substring(spaceIndex + 1);
            string[] cmdParams = paramsStr.Split(';');
            switch (command)
            {
                case "AddProduct":
                    return this.AddProduct(
                        int.Parse(cmdParams[0]), cmdParams[1], decimal.Parse(cmdParams[2]), cmdParams[3]);
                case "DeleteProduct":
                    return this.DeleteProductById(int.Parse(cmdParams[0]));
                case "FindProductsByTitle":
                    return this.FindProductsByTitle(cmdParams[0]);
                case "FindProductsByTitleAndPrice":
                    return this.FindProductsByTitleAndPrice(cmdParams[0], decimal.Parse(cmdParams[1]));
                case "FindProductsByTitleAndPriceRange":
                    return this.FindProductsByTitleAndPriceRange(
                        cmdParams[0], decimal.Parse(cmdParams[1]), decimal.Parse(cmdParams[2]));
                case "FindProductsBySupplierAndPrice":
                    return this.FindProductsBySupplierAndPrice(cmdParams[0], decimal.Parse(cmdParams[1]));
                case "FindProductsBySupplierAndPriceRange":
                    return this.FindProductsBySupplierAndPriceRange(
                        cmdParams[0], decimal.Parse(cmdParams[1]), decimal.Parse(cmdParams[2]));
                default:
                    return "Invalid command";
            }
        }
    }
}
