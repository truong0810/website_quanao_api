using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class BlogOfCategory
    {
        public Guid Id { get; set; }
        public Guid? BlogId { get; set; }
        public Guid? BlogCategoryId { get; set; }
    }
}
