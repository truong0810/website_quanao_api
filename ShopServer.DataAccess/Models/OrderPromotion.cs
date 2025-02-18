using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class OrderPromotion
    {
        public Guid Id { get; set; }
        public Guid? PromotionId { get; set; }
        public Guid? OrderProductId { get; set; }
        public decimal? DiscountAmount { get; set; }
        public int? GetQuantity { get; set; }
    }
}
