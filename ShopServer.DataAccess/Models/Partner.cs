using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class Partner
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? NumberPhone { get; set; }
        public string? Address { get; set; }
        public Guid? ImageId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? Note { get; set; }
    }
}
