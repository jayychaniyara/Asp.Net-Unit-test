using System.Collections.Generic;

namespace ShoppingApi.Models
{
    public interface IProductRepository
    {
        void Add(Product item);

        IEnumerable<Product> GetAll();

        Product GetById(int id);

        Product Remove(int id);

        void Update(Product item);

        PagedList<Product> GetPagedProducts(QueryParameters queryParameters);
    }
}
