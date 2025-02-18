using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Order
{
    public class UpdateOrderRequest : BaseRequest
    {
        public OrderDTO Order { get; set; }
    }
}
