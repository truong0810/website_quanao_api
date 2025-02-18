using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class RefreshToken
    {
        public Guid Id { get; set; }
        public string? RefreshCode { get; set; }
        public DateTime? RefreshCodeExpire { get; set; }
        public DateTime? RefreshCodeCreated { get; set; }
        public string? Token { get; set; }
        public DateTime? TokenCreated { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? UpdatePassword { get; set; }
        public bool? IsBlock { get; set; }
    }
}
