﻿using ShopServer.Business.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Brand
{
    public class DeleteBrandRequest : BaseRequest
    {
        public Guid Id { get; set; }
    }
}
