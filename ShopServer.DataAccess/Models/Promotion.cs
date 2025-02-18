using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class Promotion
    {
        public Guid Id { get; set; }
        public string? PromotionName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? BuyQuantity { get; set; }
        public int? GetQuantity { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? PromotionType { get; set; }
        public bool? IsApplyAll { get; set; }
    }
}
