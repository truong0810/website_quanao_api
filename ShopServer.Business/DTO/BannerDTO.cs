using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.DTO
{
    public class BannerDTO
    {
        public Guid? Id { get; set; }
        public Guid? ImageId { get; set; }
        public string? Name { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal? SortIndex { get; set; }
    }
}
