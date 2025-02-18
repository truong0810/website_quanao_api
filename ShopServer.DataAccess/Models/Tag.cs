using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class Tag
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
