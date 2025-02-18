using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class Product
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Title { get; set; }
        public bool? IsHot { get; set; }
        public Guid? PrimaryImageId { get; set; }
    }
}
