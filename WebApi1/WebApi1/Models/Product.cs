using System.Collections.Generic;

namespace ShoppingApi.Models
{
    public class Product
    {
        public IEnumerable<object> StatusCode;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsMerchantDiscountAllowed { get; set; }

        public decimal ShippingCharge => (0.01M * Price);
    }
}
