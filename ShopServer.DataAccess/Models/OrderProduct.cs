using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class OrderProduct
    {
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
