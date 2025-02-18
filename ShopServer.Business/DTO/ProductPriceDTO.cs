using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.DTO
{
    public class ProductPriceDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string NameProduct { get; set; }
        public string CodeProduct { get; set; }
    }
}
