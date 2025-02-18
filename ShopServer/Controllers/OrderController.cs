using Microsoft.AspNetCore.Mvc;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Order;

namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness _orderBusiness;

        public OrderController(IOrderBusiness orderBusiness)
        {
            _orderBusiness = orderBusiness;
        }

        [HttpPost("get-list")]
        public GetListOrderResponse GetListOrder(GetListOrderRequest request)
        {
            return _orderBusiness.GetListOrder(request);
        }
        [HttpPost("get-by-id")]
        public GetOrderByIdResponse GetOrderById(GetOrderByIdRequest request)
        {
            return _orderBusiness.GetOrderById(request);
        }
        [HttpPost("update")]
        public UpdateOrderResponse UpdateOrder(UpdateOrderRequest request)
        {
            return _orderBusiness.UpdateOrder(request);
        }
        [HttpPost("update-order-status")]
        public UpdateOrderStatusResponse UpdateOrderStatus(UpdateOrderStatusRequest request)
        {
            return _orderBusiness.UpdateOrderStatus(request);
        }
    }
}
