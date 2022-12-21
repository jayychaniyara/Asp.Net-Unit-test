using Newtonsoft.Json;
using Shopping.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Shopping.DAL
{
    public class ShoppingProductRepository : IShoppingProductRepository
    {
        public IEnumerable<ShoppingProduct> GetShoppingProducts()
        {
            IEnumerable<ShoppingProduct> products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:32864/");
                var responseTask = client.GetAsync("api/product/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;
                if (!result.IsSuccessStatusCode)
                {
                    // web api error - log response / error
                    return Enumerable.Empty<ShoppingProduct>();
                }

                var readTask = result.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<ShoppingProduct>>(readTask);
            }
            return products;
        }

        public ShoppingProduct GetShoppingProductById(int shoppingProductId)
        {
            ShoppingProduct product;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:32864/");
                var responseTask = client.GetAsync($"api/product/GetById/{shoppingProductId}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (!result.IsSuccessStatusCode)
                {
                    // web api error - log response / error
                    return null;
                }

                var readTask = result.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<ShoppingProduct>(readTask);
            }
            return product;
        }
    }
}
