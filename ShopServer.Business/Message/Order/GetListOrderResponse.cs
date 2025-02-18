using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Order
{
    public class GetListOrderResponse : BaseResponse
    {
        public List<OrderDTO> Orders { get; set; } 
        public PaginationDTO Pagination { get; set; }
    }
}
