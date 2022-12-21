using Shopping.DAL;
using Shopping.Lib;
using System;
using System.Collections.Generic;

namespace Shopping.BLL
{
    public class ShoppingBusiness : IShoppingBusiness
    {
        private readonly IShoppingProductRepository _shoppingProductRepository;
        private readonly IDiscountCalculator _discountCalculator;

        public ShoppingBusiness(IShoppingProductRepository shoppingProductRepository,
            IDiscountCalculator discountCalculator)
        {
            _shoppingProductRepository = shoppingProductRepository;
            _discountCalculator = discountCalculator;
        }

        public IEnumerable<ShoppingProduct> GetProducts()
        {
            var products = _shoppingProductRepository.GetShoppingProducts();
            return products;
        }

        public ShoppingProduct GetProductById(int productId)
        {
            var product = _shoppingProductRepository.GetShoppingProductById(productId);
            if (product == null)
            {
                return null;
            }

            if (product.IsMerchantDiscountAllowed)
            {
                product.FestivalDiscount = _discountCalculator.CalculateFestivalDiscount(DateTime.Now);
            }
            return product;
        }

        public ShoppingProduct ApplyFestivalDiscount(ShoppingProduct product)
        {
            if (product.FestivalDiscount != null)
            {
                product = ApplyDiscount(product, product.FestivalDiscount);
            }
            return product;
        }

        // Make this method testable
        private ShoppingProduct ApplyDiscount(ShoppingProduct product, Discount discount)
        {
            if (product == null || discount == null)
            {
                throw new ArgumentNullException();
            }

            var purchasePrice = product.Price * product.PurchasedQuantity;
            if (purchasePrice < discount.MinimumProductValue)
            {
                product.DiscountAmount = 0M;
                return product;
            }

            if (discount.DiscountBy == DiscountOption.Percentage)
            {
                product.DiscountAmount = purchasePrice * discount.DiscountPercentage / 100;
                //TODO: what if discount is greater than max discount amount?
                // Implement Code and Tests, preferrably using TDD approach
            }
            else if (discount.DiscountBy == DiscountOption.Amount)
            {
                product.DiscountAmount = discount.DiscountAmount;
                //TODO: what if purchase price (Rs.99) is lower than (flat) discount amount (Rs.100) ?
                // Implement Code and Tests, preferrably using TDD approach
            }
            return product;
        }
    }
}
