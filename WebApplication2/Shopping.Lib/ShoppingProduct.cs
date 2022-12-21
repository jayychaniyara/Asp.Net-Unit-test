namespace Shopping.Lib
{
    public class ShoppingProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal ShippingCharge { get; set; }
        public bool IsMerchantDiscountAllowed { get; set; }

        public int PurchasedQuantity { get; set; } = 1;
        public decimal DiscountAmount { get; set; }
        // Write Test cases to test this property
        public decimal FinalPrice => (Price * PurchasedQuantity);
        // Write Test cases to test this property
        public decimal FinalCost => ((Price * PurchasedQuantity) + ShippingCharge - DiscountAmount);
        
        public Discount FestivalDiscount { get; set; }
        public Discount PromoCodeDiscount { get; set; }
    }
}
