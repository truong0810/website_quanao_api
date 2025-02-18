using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Order
{
    public class GetOrderByIdResponse : BaseResponse
    {
        public OrderDTO Order { get; set; }
        public List<OrderProductDTO> OrderProducts { get; set; }
    }
}
