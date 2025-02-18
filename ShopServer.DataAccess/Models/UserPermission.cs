using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class UserPermission
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? PermissionId { get; set; }
    }
}
