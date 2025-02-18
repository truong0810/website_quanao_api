
using ShopServer.Business.DTO;
using ShopServer.Business.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Brand
{
    public class GetListBrandRequest : BaseRequest
    {
        public PaginationDTO Pagination { get; set; }

        public string Name { get; set; }
    }
}
