using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Banner
{
    public class GetListBannerRequest:BaseRequest
    {
        public string? Name { get; set; }
        public PaginationDTO Pagination { get; set; }
    }
}
