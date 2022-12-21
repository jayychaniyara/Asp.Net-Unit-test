using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Models
{
    public class ProductRepository : IProductRepository
    {
        private static ConcurrentDictionary<int, Product> products = new ConcurrentDictionary<int, Product>();

        public ProductRepository()
        {
            FetchProducts();
        }

        public void Add(Product item)
        {
            products[item.Id] = item;
        }

        public IEnumerable<Product> GetAll()
        {
            return products.Values; ;
        }

        public Product GetById(int id)
        {
            Product item;
            products.TryGetValue(id, out item);
            return item;
        }

        public Product Remove(int id)
        {
            Product item;
            products.TryGetValue(id, out item);
            products.TryRemove(id, out item);
            return item;
        }

        public void Update(Product item)
        {
            products[item.Id] = item;
        }

        public PagedList<Product> GetPagedProducts(QueryParameters queryParameters)
        {
            return PagedList<Product>.ToPagedList(GetAll().AsQueryable(),
                    queryParameters.PageNumber,
                    queryParameters.PageSize);
        }

        private void FetchProducts()
        {
            const string description = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            Product product;
            for (int productNumber = 1; productNumber <= 100; productNumber++)
            {
                product = new Product
                {
                    Id = productNumber,
                    Name = $"Product Number {productNumber}",
                    Description = $"Product Description: This is the product number: {productNumber}. {description}",
                    Quantity = 10 * productNumber,
                    Price = (decimal)10.0M * productNumber,
                    IsMerchantDiscountAllowed = (productNumber % 2 == 0) ? false : true
                };

                Add(product);
            }
        }
    }
}
