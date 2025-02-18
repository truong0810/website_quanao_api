﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Product
{
    public class GetRelatedProductRequest : BaseRequest
    {
        public Guid? CategoryId { get; set; }
        public Guid? BrandId { get; set; }
    }
}
