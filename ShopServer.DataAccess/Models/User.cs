using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class User
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public Guid? RoleId { get; set; }
        public bool? IsStaff { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public bool? Actived { get; set; }
        public string? Note { get; set; }
    }
}
