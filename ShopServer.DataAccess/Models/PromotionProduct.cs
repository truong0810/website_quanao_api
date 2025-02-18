using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class PromotionProduct
    {
        public Guid Id { get; set; }
        public Guid? PromotionId { get; set; }
        public Guid? ProductId { get; set; }
    }
}
