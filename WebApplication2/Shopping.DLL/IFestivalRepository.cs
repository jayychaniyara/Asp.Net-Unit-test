using Shopping.Lib;
using System;

namespace Shopping.DAL
{
    public interface IFestivalRepository
    {
        Discount GetFestivalDiscountByShoppingDate(DateTime shoppingDate);
    }
}