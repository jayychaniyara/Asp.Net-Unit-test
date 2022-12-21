using Shopping.Lib;
using System;
using System.Collections.Concurrent;

namespace Shopping.DAL
{
    public class FestivalRepository : IFestivalRepository
    {
        private readonly ConcurrentDictionary<DateTime, Discount> festivals = new ConcurrentDictionary<DateTime, Discount>();

        public FestivalRepository()
        {
            FetchFestivals();
        }

        public Discount GetFestivalDiscountByShoppingDate(DateTime shoppingDate)
        {
            if (!festivals.ContainsKey(shoppingDate.Date))
            {
                return null;
            }

            return festivals[shoppingDate.Date];
        }

        private ConcurrentDictionary<DateTime, Discount> FetchFestivals()
        {
            // In Live System, these will be fetched from actual Database
            festivals.TryAdd(new DateTime(2021, 1, 14), new Discount { DiscountPercentage = 5, MaximumDiscount = 100.0M, DiscountBy = DiscountOption.Percentage });
            festivals.TryAdd(new DateTime(2021, 1, 30), new Discount { DiscountAmount = 100.0M, MinimumProductValue = 1000.0M, DiscountBy = DiscountOption.Amount });
            festivals.TryAdd(new DateTime(2021, 5, 31), new Discount { DiscountPercentage = 4, MaximumDiscount = 100.0M, DiscountBy = DiscountOption.Percentage });
            festivals.TryAdd(new DateTime(2021, 3, 29), new Discount { DiscountAmount = 200.0M, MinimumProductValue = 5000.0M, DiscountBy = DiscountOption.Amount });
            festivals.TryAdd(new DateTime(2021, 8, 30), new Discount { DiscountPercentage = 3, MaximumDiscount = 100.0M, DiscountBy = DiscountOption.Percentage });
            festivals.TryAdd(new DateTime(2021, 10, 2), new Discount { DiscountAmount = 300.0M, MinimumProductValue = 900.0M, DiscountBy = DiscountOption.Amount });
            festivals.TryAdd(new DateTime(2021, 9, 21), new Discount { DiscountPercentage = 10M, MaximumDiscount = 100.0M, DiscountBy = DiscountOption.Percentage, MinimumProductValue = 50M });
            festivals.TryAdd(new DateTime(2021, 9, 23), new Discount { DiscountPercentage = 10M, MaximumDiscount = 100.0M, DiscountBy = DiscountOption.Percentage, MinimumProductValue = 50M });
            festivals.TryAdd(new DateTime(2021, 11, 4), new Discount { DiscountAmount = 111.0M, MinimumProductValue = 1111.0M, DiscountBy = DiscountOption.Amount });
            festivals.TryAdd(new DateTime(2021, 11, 5), new Discount { DiscountPercentage = 5.1M, MaximumDiscount = 100.0M, DiscountBy = DiscountOption.Percentage });
            festivals.TryAdd(new DateTime(2021, 11, 6), new Discount { DiscountAmount = 100.0M, MinimumProductValue = 1000.0M, DiscountBy = DiscountOption.Amount });
            festivals.TryAdd(new DateTime(2021, 12, 25), new Discount { DiscountPercentage = 5.5M, MaximumDiscount = 100.0M, DiscountBy = DiscountOption.Percentage });
            festivals.TryAdd(new DateTime(2021, 1, 26), new Discount { DiscountAmount = 100.0M, MinimumProductValue = 1000.0M, DiscountBy = DiscountOption.Amount });
            festivals.TryAdd(new DateTime(2021, 8, 15), new Discount { DiscountPercentage = 5.2M, MaximumDiscount = 100.0M, DiscountBy = DiscountOption.Percentage });

            festivals.TryAdd(new DateTime(2022, 1, 14), new Discount { DiscountPercentage = 5, MaximumDiscount = 100.0M, DiscountBy = DiscountOption.Percentage });
            festivals.TryAdd(new DateTime(2022, 1, 15), new Discount { DiscountAmount = 100.0M, MinimumProductValue = 1000.0M, DiscountBy = DiscountOption.Amount });
            festivals.TryAdd(new DateTime(2022, 2, 21), new Discount { DiscountPercentage = 5.5M, MaximumDiscount = 100.0M, DiscountBy = DiscountOption.Percentage });
            festivals.TryAdd(new DateTime(2022, 3, 10), new Discount { DiscountPercentage = 4.75M, MaximumDiscount = 100.0M, DiscountBy = DiscountOption.Percentage });
            festivals.TryAdd(new DateTime(2022, 8, 3), new Discount { DiscountPercentage = 5.99M, MaximumDiscount = 1000.0M, MinimumProductValue = 5000M, DiscountBy = DiscountOption.Percentage });
            festivals.TryAdd(new DateTime(2022, 10, 2), new Discount { DiscountPercentage = 2, MaximumDiscount = 200.0M, DiscountBy = DiscountOption.Percentage });
            festivals.TryAdd(new DateTime(2022, 11, 14), new Discount { DiscountPercentage = 3, MaximumDiscount = 300.0M, DiscountBy = DiscountOption.Percentage });
            festivals.TryAdd(new DateTime(2022, 11, 15), new Discount { DiscountPercentage = 4, MaximumDiscount = 400.0M, DiscountBy = DiscountOption.Percentage });
            festivals.TryAdd(new DateTime(2022, 11, 16), new Discount { DiscountPercentage = 2.5M, MaximumDiscount = 100.0M, DiscountBy = DiscountOption.Percentage });
            festivals.TryAdd(new DateTime(2022, 12, 25), new Discount { DiscountPercentage = 7.5M, MaximumDiscount = 100.0M, DiscountBy = DiscountOption.Percentage });
            festivals.TryAdd(new DateTime(2022, 1, 26), new Discount { DiscountPercentage = 8.5M, MaximumDiscount = 250.0M, DiscountBy = DiscountOption.Percentage });
            festivals.TryAdd(new DateTime(2022, 8, 15), new Discount { DiscountPercentage = 10, MaximumDiscount = 100.0M, DiscountBy = DiscountOption.Percentage });

            return festivals;
        }
    }
}
