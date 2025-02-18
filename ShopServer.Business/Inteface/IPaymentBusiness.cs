using ShopServer.Business.Message.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Inteface
{
    public interface IPaymentBusiness
    {
        PaymentResponse Payment(PaymentRequest request);
    }
}
