using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class ProductPrice
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
