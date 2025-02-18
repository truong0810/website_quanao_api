using ShopServer.Business.DTO;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Brand;
using ShopServer.Business.Ultis;
using ShopServer.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Implement
{
    public class BrandBusiness : IBrandBusiness
    {
        public CreateBrandResponse CreateBrand(CreateBrandRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _dbTransaction = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var brand = AutoMapperUtils.AutoMap<BrandDTO, Brand>(request.Brand);
                        brand.Id = Guid.NewGuid();
                        _sqlContext.Brands.Add(brand);
                        _sqlContext.SaveChanges();
                        _dbTransaction.Commit();
                        return new CreateBrandResponse
                        {
                            Code = 1,
                            Message = "ok"
                        };

                    }
                    catch (Exception e)
                    {
                        _dbTransaction.Rollback();
                        return new CreateBrandResponse
                        {
                            Code = 0,
                            Message = e.Message
                        };
                    }
                }
            }
        }
        public UpdateBrandResponse UpdateBrand(UpdateBrandRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            using var _dbTransaction = _sqlContext.Database.BeginTransaction();
            try
            {
                var brand = _sqlContext.Brands.FirstOrDefault(s => s.Id == request.Brand.Id);
                if (brand == null) throw new Exception("Không tồn tại thương hiệu");
                brand.Name = request.Brand.Name;
                brand.Description = request.Brand.Description;
                _sqlContext.SaveChanges();
                _dbTransaction.Commit();
                return new UpdateBrandResponse
                {
                    Code = 1,
                    Message = "ok"
                };
            }
            catch (Exception e)
            {
                return new UpdateBrandResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }
        public GetListBrandResponse GetListBrand(GetListBrandRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var brand = _sqlContext.Brands.Where(s => (string.IsNullOrEmpty(request.Name) || s.Name.Contains(request.Name)));
                request.Pagination.Total = brand.Count();
                var listBrand = brand.Skip((request.Pagination.Page - 1) * request.Pagination.Limit).Take(request.Pagination.Limit).ToList();
                var listBrandDto = AutoMapperUtils.AutoMap<Brand, BrandDTO>(listBrand);
                return new GetListBrandResponse
                {
                    Code = 1,
                    Message = "ok",
                    ListBrand = listBrandDto,
                    Pagination = request.Pagination
                };
            }
            catch (Exception e)
            {
                return new GetListBrandResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public GetBrandByIdResponse GetBrandById(GetBrandByIdRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var brand = _sqlContext.Brands.FirstOrDefault(s => s.Id == request.Id);
                if (brand == null) throw new Exception("Không tồn tại thương hiệu");
                var brandDto = AutoMapperUtils.AutoMap<Brand, BrandDTO>(brand);
                return new GetBrandByIdResponse
                {
                    Code = 1,
                    Message = "ok",
                    Brand = brandDto
                };
            }
            catch (Exception e)
            {
                return new GetBrandByIdResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public GetAllBrandResponse GetAllBrand(GetAllBrandRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var brands = _sqlContext.Brands.ToList();
                var brandDtos = AutoMapperUtils.AutoMap<Brand, BrandDTO>(brands);
                return new GetAllBrandResponse
                {
                    Code = 1,
                    Message = "ok",
                    Brands = brandDtos
                };
            }
            catch (Exception e)
            {
                return new GetAllBrandResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public DeleteBrandResponse DeleteBrand(DeleteBrandRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var brand = _sqlContext.Brands.FirstOrDefault(s => s.Id == request.Id);
                if (brand == null) throw new Exception("Không tồn tại danh mục");
                _sqlContext.Brands.Remove(brand);
                _sqlContext.SaveChanges();
                return new DeleteBrandResponse
                {
                    Code = 1,
                    Message = "ok"
                };
            }
            catch (Exception e)
            {
                return new DeleteBrandResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }
    }
}
