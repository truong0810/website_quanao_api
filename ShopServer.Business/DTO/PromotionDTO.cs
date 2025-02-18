using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.DTO
{
    public class PromotionDTO
    {
        public Guid? Id { get; set; }
        public string PromotionName { get; set; }
        public int? PromotionType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? BuyQuantity { get; set; }
        public int? GetQuantity { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public bool IsApplyAll { get; set; }
    }
}
