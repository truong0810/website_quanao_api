using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class Function
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? SortIndex { get; set; }
    }
}
