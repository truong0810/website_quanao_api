using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShopServer.Business.DTO;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Promotion;
using ShopServer.Business.Message.Resource;
using ShopServer.Business.Ultis;
using ShopServer.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Implement
{
    public class PromotionBusiness : IPromotionBusiness
    {
        public readonly IConfiguration _configuration;

        public PromotionBusiness(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CreatePromotionResponse CreatePromotion(CreatePromotionRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            using var _dbTransaction = _sqlContext.Database.BeginTransaction();
            try
            {
                var promotion = AutoMapperUtils.AutoMap<PromotionDTO, Promotion>(request.Promotion);
                promotion.Id = Guid.NewGuid();
                promotion.CreatedBy = request.AuthUserId;
                promotion.CreatedAt = DateTime.Now;
                if ((bool)!promotion.IsApplyAll)
                {
                    foreach (var item in request.Products)
                    {
                        var promotionProduct = new PromotionProduct
                        {
                            Id = Guid.NewGuid(),
                            PromotionId = promotion.Id,
                            ProductId = item,
                        };
                        _sqlContext.PromotionProducts.Add(promotionProduct);
                    }
                }
                _sqlContext.Promotions.Add(promotion);
                _sqlContext.SaveChanges();
                _dbTransaction.Commit();
                return new CreatePromotionResponse
                {
                    Code = 1,
                    Message = "ok",
                    PromotionId = promotion.Id
                };
            }
            catch (Exception e)
            {
                return new CreatePromotionResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }
        public async Task<UploadFileResponse> UploadFile(UploadFileRequest request)
        {
            using (var _context = new PnpWebContext())
            {
                using (var _dbTransaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        IFormFile file = request.File;
                        var fileId = Guid.NewGuid();
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fileExtension = fileName.Substring(fileName.LastIndexOf('.'));
                        var pathToSave = Path.Combine(_configuration["resource-path"], fileName);
                        var directory = Path.GetDirectoryName(pathToSave);
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }
                        using (var bits = new FileStream(pathToSave, FileMode.Create))
                        {
                            await file.CopyToAsync(bits);
                        }
                        var promotionImage = new PromotionImage
                        {
                            Id = Guid.NewGuid(),
                            PromotionId = request.Id,
                            ResourceId = fileId
                        };
                        var resource = new Resource
                        {
                            Id = fileId,
                            FileName = fileName,
                            Extension = fileExtension,
                            FilePath = pathToSave,
                            CreatedDate = DateTime.Now,
                            CreatedBy = request.AuthUserId
                        };

                        _context.PromotionImages.Add(promotionImage);
                        _context.Resources.Add(resource);
                        _context.SaveChanges();
                        _dbTransaction.Commit();
                        return new UploadFileResponse()
                        {
                            Code = 1,
                            Message = "Thành công"
                        };
                    }
                    catch (Exception ex)
                    {
                        _dbTransaction.Rollback();
                        return new UploadFileResponse()
                        {
                            Code = 0,
                            Message = ex.Message,
                        };
                    }
                }
            }
        }
        public DeletePromotionResponse DeletePromotion(DeletePromotionRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var promotion = _sqlContext.Promotions.FirstOrDefault(s => s.Id == request.Id);
                if (promotion == null) throw new Exception("Không tồn tại giảm giá ");
                var promotionProductsToDelete = _sqlContext.PromotionProducts.Where(s => s.PromotionId == request.Id).ToList();
                var promotionImagesToDelete = _sqlContext.PromotionImages.Where(s => s.PromotionId == request.Id).ToList();
                foreach (PromotionImage item in promotionImagesToDelete)
                {
                    var resource = _sqlContext.Resources.Where(r => r.Id == item.ResourceId).FirstOrDefault();
                    _sqlContext.Resources.Remove(resource);
                    this.DeleteFile(resource.FilePath);
                }
                _sqlContext.PromotionProducts.RemoveRange(promotionProductsToDelete);
                _sqlContext.PromotionImages.RemoveRange(promotionImagesToDelete);
                _sqlContext.Promotions.Remove(promotion);
                _sqlContext.SaveChanges();
                return new DeletePromotionResponse
                {
                    Code = 1,
                    Message = "ok",

                };

            }
            catch (Exception e)
            {
                return new DeletePromotionResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        private Boolean DeleteFile(string fullPath)
        {
            if (!File.Exists(fullPath)) { throw new Exception("Tệp này không tồn tại"); }
            File.Delete(fullPath);
            return true;
        }
        public GetAllPromotionResponse GetAllPromotion(GetAllPromotionRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var listPromotions = _sqlContext.Promotions.ToList();
                var promotionDtos = AutoMapperUtils.AutoMap<Promotion, PromotionDTO>(listPromotions);
                return new GetAllPromotionResponse
                {
                    Code = 1,
                    Message = "ok",
                    Promotions = promotionDtos
                };
            }
            catch (Exception e)
            {
                return new GetAllPromotionResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public GetLinkImageResponse GetLink(string fileName)
        {
            using (var _context = new PnpWebContext())
            {
                try
                {
                    var filePath = "DEFAULT\\default-img.png";
                    if (File.Exists(Path.Combine(_configuration["resource-path"], fileName)))
                        filePath = fileName;

                    using (var webClient = new WebClient())
                    {
                        byte[] imageByte = webClient.DownloadData(Path.Combine(_configuration["resource-path"], filePath));
                        return new GetLinkImageResponse()
                        {
                            Code = 1,
                            Message = "Thành công",
                            File = imageByte,
                        };
                    }

                }
                catch (Exception ex)
                {
                    return new GetLinkImageResponse()
                    {
                        Code = 0,
                        Message = ex.Message,
                    };
                }
            }
        }

        public GetListPromotionResponse GetListPromotion(GetListPromotionRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var query = _sqlContext.Promotions.AsQueryable();
                    if (!string.IsNullOrEmpty(request.PromotionName))
                    {
                        query = query.Where(s => s.PromotionName.Contains(request.PromotionName));
                    }
                    if (request.PromotionType != null)
                    {
                        query = query.Where(s => s.PromotionType == request.PromotionType);
                    }
                    if (request.BuyQuantity != null)
                    {
                        query = query.Where(s => s.BuyQuantity == request.BuyQuantity);
                    }
                    if (request.GetQuantity != null)
                    {
                        query = query.Where(s => s.GetQuantity == request.GetQuantity);
                    }
                    if (request.DiscountAmount != null)
                    {
                        query = query.Where(s => s.DiscountAmount == request.DiscountAmount);
                    }
                    if (request.DiscountPercentage != null)
                    {
                        query = query.Where(s => s.DiscountPercentage == request.DiscountPercentage);
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
                    request.Pagination.Total = query.Count();
                    var promotions = query.Skip((request.Pagination.Page - 1) * request.Pagination.Limit).Take(request.Pagination.Limit).ToList();
                    var promotionDtos = AutoMapperUtils.AutoMap<Promotion, PromotionDTO>(promotions);
                    return new GetListPromotionResponse
                    {
                        Code = 1,
                        Promotions = promotionDtos,
                        Pagination = request.Pagination
                    };

                }
                catch (Exception e)
                {
                    return new GetListPromotionResponse
                    {
                        Code = 0,
                        Message = e.Message
                    };
                }
            }
        }

        public GetListPromotionProductResponse GetListPromotionProduct(GetListPromotionProductRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var query = _sqlContext.PromotionProducts.AsQueryable();
                query = query.Where(s => s.PromotionId == request.Id);
                var promotionProduct = from pp in query
                                       join p in _sqlContext.Products on pp.ProductId equals p.Id into ProductGroup
                                       from product in ProductGroup.DefaultIfEmpty()
                                       select new ProductDTO
                                       {
                                           Id = (Guid)pp.ProductId,
                                           Name = product.Name,
                                           Code = product.Code
                                       };
                return new GetListPromotionProductResponse
                {
                    Code = 1,
                    Message = "ok",
                    Products = promotionProduct.ToList()
                };
            }
            catch (Exception e)
            {
                return new GetListPromotionProductResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public GetPromotionByIdResponse GetPromotionById(GetPromotionByIdRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var promotion = _sqlContext.Promotions.FirstOrDefault(s => s.Id == request.Id);
                var resources = _sqlContext.Resources.Where(r => _sqlContext.PromotionImages.Any(pi => pi.PromotionId == request.Id && r.Id == pi.ResourceId)).ToList();
                var promotionDto = AutoMapperUtils.AutoMap<Promotion, PromotionDTO>(promotion);
                var images = AutoMapperUtils.AutoMap<Resource, ResourceDTO>(resources);
                return new GetPromotionByIdResponse
                {
                    Code = 1,
                    Message = "ok",
                    Promotion = promotionDto,
                    Images = images
                };
            }
            catch (Exception e)
            {
                return new GetPromotionByIdResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public UpdatePromotionResponse UpdatePromotion(UpdatePromotionRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            using var _dbTransaction = _sqlContext.Database.BeginTransaction();
            try
            {
                var promotion = _sqlContext.Promotions.FirstOrDefault(s => s.Id == request.Promotion.Id);
                if (promotion == null) throw new Exception("Không tồn tại giảm giá này");
                promotion.PromotionName = request.Promotion.PromotionName;
                promotion.PromotionType = request.Promotion.PromotionType;
                promotion.StartDate = request.Promotion.StartDate;
                promotion.EndDate = request.Promotion.EndDate;
                promotion.BuyQuantity = request.Promotion.BuyQuantity;
                promotion.GetQuantity = request.Promotion.GetQuantity;
                promotion.DiscountAmount = request.Promotion.DiscountAmount;
                promotion.DiscountPercentage = request.Promotion.DiscountPercentage;
                promotion.UpdatedBy = request.AuthUserId;
                promotion.UpdatedAt = DateTime.Now;
                promotion.IsApplyAll = request.Promotion.IsApplyAll;
                //xóa record promotionProduct cũ
                var promotionProductsToDelete = _sqlContext.PromotionProducts.Where(s => s.PromotionId == request.Promotion.Id).ToList();
                _sqlContext.PromotionProducts.RemoveRange(promotionProductsToDelete);
                //thêm mới record promotionProduct
                if ((bool)!promotion.IsApplyAll)
                {
                    foreach (var item in request.Products)
                    {
                        var promotionProduct = new PromotionProduct
                        {
                            Id = Guid.NewGuid(),
                            PromotionId = promotion.Id,
                            ProductId = item,
                        };
                        _sqlContext.PromotionProducts.Add(promotionProduct);
                    }
                }
                foreach (Guid id in request.FileDeleted)
                {
                    var promotionImage = _sqlContext.PromotionImages.FirstOrDefault(s => s.Id == id);
                    if (promotionImage == null) throw new Exception("File không tồn tại");
                    //this.DeleteFile(promotionImage.FilePath);
                    var resource = _sqlContext.Resources.FirstOrDefault(s => s.Id == id);
                    if (resource == null) throw new Exception("File không tồn tại");
                    this.DeleteFile(resource.FilePath);
                    _sqlContext.PromotionImages.Remove(promotionImage);
                    _sqlContext.Resources.Remove(resource);
                }
                _sqlContext.SaveChanges();
                _dbTransaction.Commit();
                return new UpdatePromotionResponse
                {
                    Code = 1,
                    Message = "ok"
                };
            }
            catch (Exception e)
            {
                return new UpdatePromotionResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }
    }
}
