using Shopping.Lib;
using System.Collections.Generic;

namespace Shopping.BLL
{
    public interface IShoppingBusiness
    {
        IEnumerable<ShoppingProduct> GetProducts();

        ShoppingProduct GetProductById(int productId);

        ShoppingProduct ApplyFestivalDiscount(ShoppingProduct product);
    }
}