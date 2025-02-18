using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class BlogTag
    {
        public Guid Id { get; set; }
        public Guid? BlogId { get; set; }
        public Guid? TagId { get; set; }
    }
}
