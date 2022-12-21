using Shopping.Lib;

namespace Shopping.DAL
{
    public interface IPromoCodeRepository
    {
        Discount GetPromoCodeDiscountByPromoCode(string promoCode);
    }
}
