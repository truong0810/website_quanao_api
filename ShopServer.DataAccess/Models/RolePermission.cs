using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class RolePermission
    {
        public Guid Id { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? PermissionId { get; set; }
    }
}
