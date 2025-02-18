using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Payment;
using ShopServer.DataAccess.Models;

namespace ShopServer.Business.Implement
{
    public class PaymentBusiness : IPaymentBusiness
    {
        public PaymentResponse Payment(PaymentRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            using var transaction = _sqlContext.Database.BeginTransaction();
            try
            {
                //check 
                foreach(var item in request.Carts)
                {
                    _sqlContext.Products.Where(p => p.Id == item.Id);
                    var price = _sqlContext.ProductPrices.Where(pp => pp.ProductId == item.Id && pp.StartDate <= DateTime.Now && pp.EndDate >= DateTime.Now).Select(pp => pp.Price).FirstOrDefault();
                    if(price != item.Price)
                    {
                        return new PaymentResponse()
                        {
                            Code = 0,
                            Message = "Giá của sản phẩm đang bị sai"
                        };

                    }
                }
                var orderId = Guid.NewGuid();
                decimal totalAmount = 0;
                foreach ( var item in request.Carts)
                {
                    totalAmount += item.Quantity * item.Price;
                    var orderProduct = new OrderProduct()
                    {
                        Id = Guid.NewGuid(),
                        OrderId = orderId,
                        ProductId = item.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.Price
                    };
                    _sqlContext.OrderProducts.Add(orderProduct);
                }
                var order = new Order()
                {
                    Id = orderId,
                    CustomerId = request.AuthUserId,
                    OrderDate = DateTime.Now,
                    TotalAmount = totalAmount,
                    CreatedAt = DateTime.Now,
                    CreatedBy = request.AuthUserId,
                    Receiver = request.Name,
                    Address = request.Address,
                    Email = request.Email,
                    Phone = request.Phone,
                    PaymentStatus = 0,
                    OrderStatus = 0
                };
                _sqlContext.Orders.Add(order);
                _sqlContext.SaveChanges();
                transaction.Commit();
                return new PaymentResponse()
                {
                    Code = 1,
                    Message = "ok"
                };
            }catch(Exception e)
            {
                transaction.Rollback();
                return new PaymentResponse()
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }
    }
}
