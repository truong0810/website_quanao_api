using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class ProductImage
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? ResourceId { get; set; }
    }
}
