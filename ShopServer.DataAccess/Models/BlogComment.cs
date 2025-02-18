using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class BlogComment
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string? Description { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
