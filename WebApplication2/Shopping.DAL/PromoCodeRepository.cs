using Shopping.Lib;
using System.Collections.Concurrent;

namespace Shopping.DAL
{
    public class PromoCodeRepository : IPromoCodeRepository
    {
        private readonly ConcurrentDictionary<string, Discount> promoCodes = new ConcurrentDictionary<string, Discount>();

        public PromoCodeRepository()
        {
            FetchPromoCodes();
        }

        public Discount GetPromoCodeDiscountByPromoCode(string promoCode)
        {
            if (!promoCodes.ContainsKey(promoCode))
            {
                return null;
            }

            return promoCodes[promoCode];
        }

        private ConcurrentDictionary<string, Discount> FetchPromoCodes()
        {
            // In Live System, these will be fetched from actual Database
            promoCodes.TryAdd("ABCD1", new Discount { DiscountAmount = 5M, DiscountBy = DiscountOption.Amount });
            promoCodes.TryAdd("BCDE2", new Discount { DiscountAmount = 10M, DiscountBy = DiscountOption.Amount });
            promoCodes.TryAdd("CDEF3", new Discount { DiscountAmount = 15M, DiscountBy = DiscountOption.Amount });
            promoCodes.TryAdd("HIJK9", new Discount { DiscountAmount = 20M, DiscountBy = DiscountOption.Amount });
            promoCodes.TryAdd("MNOPQ", new Discount { DiscountAmount = 5M, DiscountBy = DiscountOption.Amount });
            promoCodes.TryAdd("1STUV", new Discount { DiscountAmount = 10M, DiscountBy = DiscountOption.Amount });
            promoCodes.TryAdd("DEFGH", new Discount { DiscountAmount = 15M, DiscountBy = DiscountOption.Amount });
            promoCodes.TryAdd("E2GHI", new Discount { DiscountAmount = 5.0M, DiscountBy = DiscountOption.Amount });
            promoCodes.TryAdd("FGH5J", new Discount { DiscountAmount = 5.50M, DiscountBy = DiscountOption.Amount });
            promoCodes.TryAdd("GH3JK", new Discount { DiscountAmount = 10.50M, DiscountBy = DiscountOption.Amount });
            promoCodes.TryAdd("IJKLM", new Discount { DiscountAmount = 15.50M, DiscountBy = DiscountOption.Amount });
            promoCodes.TryAdd("JKLMN", new Discount { DiscountAmount = 50.0M, DiscountBy = DiscountOption.Amount });
            promoCodes.TryAdd("KL4YZ", new Discount { DiscountAmount = 100M, DiscountBy = DiscountOption.Amount });

            return promoCodes;
        }
    }
}
