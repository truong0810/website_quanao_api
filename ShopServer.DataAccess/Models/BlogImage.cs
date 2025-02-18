using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class BlogImage
    {
        public Guid Id { get; set; }
        public Guid? BlogId { get; set; }
        public Guid? BlogImageId { get; set; }
        public decimal? SortIndex { get; set; }
    }
}
