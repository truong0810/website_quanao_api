﻿using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Product
{
    public class GetListProductForAdminResponse : BaseResponse
    {
        public List<ProductDTO> Products { get; set; }
        public PaginationDTO Pagination { get; set; }
    }
}
