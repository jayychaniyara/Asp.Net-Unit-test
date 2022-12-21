using Shopping.Lib;
using System;

namespace Shopping.BLL
{
    public interface IDiscountCalculator
    {
        Discount CalculateFestivalDiscount(DateTime shoppingDate);
    }
}