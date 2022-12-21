using System;
using Shopping.DAL;
using Shopping.Lib;

namespace Shopping.BLL
{
    public class DiscountCalculator : IDiscountCalculator
    {
        private readonly IFestivalRepository _festivalRepository;
        private readonly IPromoCodeRepository _promoCodeRepository;
        private const int promoCodeLength = 5;

        public DiscountCalculator(IFestivalRepository festivalRepository,
            IPromoCodeRepository promoCodeRepository)
        {
            _festivalRepository = festivalRepository;
            _promoCodeRepository = promoCodeRepository;
        }

        // Create interface of DateTime and use it for Code and Unit Testing
        public Discount CalculateFestivalDiscount(DateTime shoppingDate)
        {
            // Check if Product is having IsMerchantDiscountAllowed,
            // if NO, then festival discount is not applicable,
            // if YES, then check if Festival Discount is applicable

            Discount festivalDiscount = _festivalRepository.GetFestivalDiscountByShoppingDate(shoppingDate);
            return festivalDiscount;

        }

        // Though this method is not called from UI, please write Unit Tests
        // You can add this method to interface, if needed
        public Discount CalculatePromoCodeDiscount(string promoCode)
        {
            // Promo Code = Coupon Code : Alpha Numeric string
            // In case of Promo Codes, Discount Option must be Amount
            // Flat discount of Discount Amount should be applicable
            // There should be NO Minimum Product Value & NO Maximum Discount Amount

            Discount promoCodeDiscount = null;
            if (IsValidPromoCode(promoCode))
            {
                promoCodeDiscount = _promoCodeRepository.GetPromoCodeDiscountByPromoCode(promoCode);
            }
            return promoCodeDiscount;
        }

        private bool IsValidPromoCode(string promoCode)
        {
            if (string.IsNullOrWhiteSpace(promoCode))
            {
                return false;
            }
            if (promoCode.Length != promoCodeLength)
            {
                return false;
            }
            return true;
        }
    }
}
