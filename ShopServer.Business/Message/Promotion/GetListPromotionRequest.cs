using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Promotion
{
    public class GetListPromotionRequest : BaseRequest
    {
        public PaginationDTO Pagination { get; set; }
        public string PromotionName { get; set; }
        public int? PromotionType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? BuyQuantity { get; set; }
        public int? GetQuantity { get; set; }
        public string VoucherCode { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPercentage { get; set; }
    }
}
