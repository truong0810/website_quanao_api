using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Order
{
    public class UpdateOrderStatusRequest : BaseRequest
    {
        public Guid Id { get; set; }
        public int OrderStatus { get; set; }
    }
}
