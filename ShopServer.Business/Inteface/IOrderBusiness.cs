using ShopServer.Business.Message.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Inteface
{
    public interface IOrderBusiness
    {
        GetListOrderResponse GetListOrder(GetListOrderRequest request);
        GetOrderByIdResponse GetOrderById(GetOrderByIdRequest request);
        UpdateOrderResponse UpdateOrder(UpdateOrderRequest request);
        UpdateOrderStatusResponse UpdateOrderStatus(UpdateOrderStatusRequest request);
    }
}
