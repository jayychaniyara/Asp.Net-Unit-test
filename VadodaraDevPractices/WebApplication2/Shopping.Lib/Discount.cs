using System;

namespace Shopping.Lib
{
    public class Discount
    {
        public decimal MinimumProductValue { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal DiscountPercentage { get; set; }

        public decimal MaximumDiscount { get; set; }

        public DiscountOption DiscountBy { get; set; } = DiscountOption.Amount;
    }
}
