using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class Role
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
