using ShopServer.Business.DTO;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Order;
using ShopServer.Business.Ultis;
using ShopServer.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Implement
{
    public class OrderBusiness : IOrderBusiness
    {
        public GetListOrderResponse GetListOrder(GetListOrderRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var query  = _sqlContext.Orders.AsQueryable();
                if (request.Name != null) query = query.Where(q => _sqlContext.Users.Any(c => c.Id == q.CustomerId && c.Username.Contains(request.Name)));
                if (request.OrderDate != null)
                {
                    query = query.Where(s => s.OrderDate.Value.Year == request.OrderDate.Value.Year &&
                             s.OrderDate.Value.Month == request.OrderDate.Value.Month &&
                             s.OrderDate.Value.Day == request.OrderDate.Value.Day);
                }
                if (request.OrderStatus != null) query = query.Where(q => q.OrderStatus == request.OrderStatus);
                if (request.PaymentStatus != null) query = query.Where(q => q.PaymentStatus == request.PaymentStatus);
                if (request.Email != null) query = query.Where(q => q.Email == request.Email);
                if (request.Phone != null) query = query.Where(q => q.Phone == request.Phone);
                if (request.CustomerId != null) query = query.Where(q => q.CustomerId == request.CustomerId);

                request.Pagination.Total = query.Count();
                var orders = query.Skip((request.Pagination.Page - 1) * request.Pagination.Limit).Take(request.Pagination.Limit).ToList();
                var orderDTOs = AutoMapperUtils.AutoMap<Order, OrderDTO>(orders);

                orderDTOs.ForEach(item =>
                {
                    var user = _sqlContext.Users.FirstOrDefault(c => c.Id == item.CustomerId);
                    item.User  = AutoMapperUtils.AutoMap<User, UserDTO>(user);
                });

                return new GetListOrderResponse()
                {
                    Code = 1,
                    Message = "ok",
                    Orders = orderDTOs,
                    Pagination = request.Pagination
                };
            }catch(Exception e)
            {
                return new GetListOrderResponse()
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public GetOrderByIdResponse GetOrderById(GetOrderByIdRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var order = _sqlContext.Orders.FirstOrDefault(o => o.Id == request.Id);
                var orderDTO = AutoMapperUtils.AutoMap<Order, OrderDTO>(order);
                var query = _sqlContext.OrderProducts.Where(op => op.OrderId == request.Id).AsQueryable();
                var orderProductDTO = from op in query
                                      join p in _sqlContext.Products on op.ProductId equals p.Id into ProductGroup
                                      from product in ProductGroup.DefaultIfEmpty()
                                      join promotion in _sqlContext.OrderPromotions on op.Id equals promotion.OrderProductId into PromotionGroup
                                      from promtion in PromotionGroup.DefaultIfEmpty()
                                      select new OrderProductDTO()
                                      {
                                          Id = op.Id,
                                          ProductId = product.Id,
                                          ProductCode = product.Code,
                                          ProductName = product.Name,
                                          Quantity = op.Quantity,
                                          UnitPrice = op.UnitPrice
                                      };
                return new GetOrderByIdResponse()
                {
                    Code = 1,
                    Message = "ok",
                    Order = orderDTO,
                    OrderProducts = orderProductDTO.ToList()
                };
            }catch(Exception e)
            {
                return new GetOrderByIdResponse()
                {
                    Code = 0,
                    Message = e.Message
                };

            }
        }

        public UpdateOrderResponse UpdateOrder(UpdateOrderRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            using var _transaction = _sqlContext.Database.BeginTransaction();
            try
            {
                var order = _sqlContext.Orders.FirstOrDefault(o => o.Id == request.Order.Id);
                order.Email = request.Order.Email;
                order.Address = request.Order.Address;
                order.Receiver = request.Order.Receiver;
                order.Phone = request.Order.Phone;
                _sqlContext.SaveChanges();
                _transaction.Commit();
                return new UpdateOrderResponse()
                {
                    Code = 1,
                    Message = "ok",
                };
            }catch(Exception e)
            {
                return new UpdateOrderResponse()
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public UpdateOrderStatusResponse UpdateOrderStatus(UpdateOrderStatusRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            using var _transaction = _sqlContext.Database.BeginTransaction();
            try
            {
                var order = _sqlContext.Orders.FirstOrDefault(o => o.Id == request.Id);
                order.OrderStatus = request.OrderStatus;
                _sqlContext.SaveChanges();
                _transaction.Commit();
                return new UpdateOrderStatusResponse()
                {
                    Code = 1,
                    Message = "ok",
                };
            }
            catch (Exception e)
            {
                return new UpdateOrderStatusResponse()
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }
    }
}
