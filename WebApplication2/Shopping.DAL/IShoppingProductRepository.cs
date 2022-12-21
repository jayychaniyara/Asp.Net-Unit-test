using Shopping.Lib;
using System.Collections.Generic;

namespace Shopping.DAL
{
    public interface IShoppingProductRepository
    {
        IEnumerable<ShoppingProduct> GetShoppingProducts();

        ShoppingProduct GetShoppingProductById(int shoppingProductId);
    }
}