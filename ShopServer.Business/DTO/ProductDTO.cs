using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? CategoryId { get; set; }
        public string NameBrand { get; set; }
        public string NameCategory { get; set; }
        public decimal? PricePrecent { get; set; }
        public string? Filename { get; set; }
        public bool? IsHot { get; set; }
        public string? Title { get; set; }

        public List<PromotionDTO> Promotions { get; set; }
        public List<string?> Images { get; set; }
    }
}
