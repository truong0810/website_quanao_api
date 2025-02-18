using Microsoft.AspNetCore.Mvc;
using ShopServer.Attribute;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Payment;

namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentBusiness _business;

        public PaymentController(IPaymentBusiness business)
        {
            _business = business;
        }
        [ParseTokenAdmin]
        [HttpPost("payment")]
        public PaymentResponse Payment(PaymentRequest request)
        {
            return _business.Payment(request);
        }
    }
}
