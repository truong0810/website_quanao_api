
using ShopServer.Business.DTO;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.ProductPrices;
using ShopServer.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopServer.Business.Ultis;
namespace ShopServer.Business.Implement
{
    public class ProductPriceBusiness : IProductPriceBusiness
    {
        public CreateProductPriceResponse CreateProductPrice(CreateProductPriceRequest request)
        {
            using var _sqlCotnext = new PnpWebContext();
            using var _dbTransaction = _sqlCotnext.Database.BeginTransaction();
            try
            {
                var productPrice = AutoMapperUtils.AutoMap<ProductPriceDTO, ProductPrice>(request.ProductPrice);
                productPrice.Id = Guid.NewGuid();
                _sqlCotnext.ProductPrices.Add(productPrice);
                _sqlCotnext.SaveChanges();
                _dbTransaction.Commit();
                return new CreateProductPriceResponse
                {
                    Code = 1,
                    Message = "ok"
                };
            }
            catch (Exception e)
            {
                return new CreateProductPriceResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public DeleteProductPriceResponse DeleteProductPrice(DeleteProductPriceRequest request)
        {
            using var _sqlCotnext = new PnpWebContext();
            using var _dbTransaction = _sqlCotnext.Database.BeginTransaction();
            try
            {
                var productPrice = _sqlCotnext.ProductPrices.FirstOrDefault(s => s.Id == request.Id);
                if (productPrice == null) throw new Exception("Không tồn tại giá sản phẩm này");
                _sqlCotnext.ProductPrices.Remove(productPrice);
                _sqlCotnext.SaveChanges();
                _dbTransaction.Commit();
                return new DeleteProductPriceResponse
                {
                    Code = 1,
                    Message = "ok"
                };
            }
            catch (Exception e)
            {
                return new DeleteProductPriceResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public GetListProductPriceResponse GetListProductPrice(GetListProductPriceRequest request)
        {
            using var _sqlCotnext = new PnpWebContext();
            try
            {
                var query = _sqlCotnext.ProductPrices.AsQueryable();
                if (request.ProductId != null)
                {
                    query = query.Where(s => s.ProductId == request.ProductId);
                }
                if (request.Price != null)
                {
                    query = query.Where(s => s.Price == request.Price);
                }
                if (request.EndDate != null)
                {
                    query = query.Where(s => s.EndDate.Value.Year == request.EndDate.Value.Year &&
                             s.EndDate.Value.Month == request.EndDate.Value.Month &&
                             s.EndDate.Value.Day == request.EndDate.Value.Day);
                }
                if (request.StartDate != null)
                {
                    query = query.Where(s => s.StartDate.Value.Year == request.StartDate.Value.Year &&
                             s.StartDate.Value.Month == request.StartDate.Value.Month &&
                             s.StartDate.Value.Day == request.StartDate.Value.Day);
                }
                var queryProductPrices = from pr in query
                                         join p in _sqlCotnext.Products on pr.ProductId equals p.Id into ProductGroup
                                         from product in ProductGroup.DefaultIfEmpty()
                                         select new ProductPriceDTO
                                         {
                                             ProductId = product.Id,
                                             Id = pr.Id,
                                             NameProduct = product.Name,
                                             Price = (decimal)pr.Price,
                                             EndDate = pr.EndDate,
                                             StartDate = pr.StartDate,
                                             CodeProduct = product.Code
                                         };
                request.Pagination.Total = queryProductPrices.Count();
                var productPriceDtos = queryProductPrices.Skip((request.Pagination.Page - 1) * request.Pagination.Limit).Take(request.Pagination.Limit).ToList();
                return new GetListProductPriceResponse
                {
                    Code = 1,
                    Message = "ok",
                    ProductPrices = productPriceDtos,
                    Pagination = request.Pagination
                };
            }
            catch (Exception e)
            {
                return new GetListProductPriceResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public GetProductPriceByIdResponse GetProductPriceById(GetProductPriceByIdRequest request)
        {
            using var _sqlCotnext = new PnpWebContext();
            try
            {
                var productPrice = _sqlCotnext.ProductPrices.FirstOrDefault(s => s.Id == request.Id);
                if (productPrice == null) throw new Exception("Không tồn tại giá sản phẩm này");
                var productPriceDto = AutoMapperUtils.AutoMap<ProductPrice, ProductPriceDTO>(productPrice);
                return new GetProductPriceByIdResponse
                {
                    Code = 1,
                    Message = "ok",
                    ProductPrice = productPriceDto
                };
            }
            catch (Exception e)
            {
                return new GetProductPriceByIdResponse
                {
                    Code = 0,
                    Message = e.Message
                };

            }
        }

        public GetProductPriceByProductResponse GetProductPriceByProduct(GetProductPriceByProductRequest request)
        {
            using var _sqlCotnext = new PnpWebContext();
            try
            {
                var productPrice = _sqlCotnext.ProductPrices.Where(s => s.ProductId == request.ProductId).ToList();
                if (productPrice == null) throw new Exception("Không tồn tại giá sản phẩm này");
                var productPriceDto = AutoMapperUtils.AutoMap<ProductPrice, ProductPriceDTO>(productPrice);
                return new GetProductPriceByProductResponse
                {
                    Code = 1,
                    Message = "ok",
                    ProductPrices = productPriceDto
                };
            }
            catch (Exception e)
            {
                return new GetProductPriceByProductResponse
                {
                    Code = 0,
                    Message = e.Message
                };

            }
        }

        public UpdateProductPriceResponse UpdateProductPrice(UpdateProductPriceRequest request)
        {
            using var _sqlCotnext = new PnpWebContext();
            using var _dbTransaction = _sqlCotnext.Database.BeginTransaction();
            try
            {
                var productPrice = _sqlCotnext.ProductPrices.FirstOrDefault(s => s.Id == request.ProductPrice.Id);
                if (productPrice == null) throw new Exception("Không tồn tại giá sản phẩm này");
                productPrice.ProductId = request.ProductPrice.ProductId;
                productPrice.Price = request.ProductPrice.Price;
                productPrice.StartDate = (DateTime)request.ProductPrice.StartDate;
                productPrice.EndDate = (DateTime)request.ProductPrice.EndDate;
                _sqlCotnext.SaveChanges();
                _dbTransaction.Commit();
                return new UpdateProductPriceResponse
                {
                    Code = 1,
                    Message = "ok"
                };

            }
            catch (Exception e)
            {
                return new UpdateProductPriceResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }
    }
}
