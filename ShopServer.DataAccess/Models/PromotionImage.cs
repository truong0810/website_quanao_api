using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class PromotionImage
    {
        public Guid Id { get; set; }
        public Guid? PromotionId { get; set; }
        public Guid? ResourceId { get; set; }
    }
}
