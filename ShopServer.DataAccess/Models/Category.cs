﻿using System;
using System.Collections.Generic;

namespace ShopServer.DataAccess.Models
{
    public partial class Category
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
