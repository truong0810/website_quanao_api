using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopServer.Business.DTO;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Product;
using ShopServer.Business.Message.Resource;
using ShopServer.Business.Ultis;
using ShopServer.DataAccess.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using static ShopServer.Business.Ultis.TextUtils;
namespace ShopServer.Business.Implement
{
    public class ProductBusiness : IProductBusiness
    {
        public readonly IConfiguration _configuration;

        public ProductBusiness(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CreateProductResponse CreateProduct(CreateProductRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            using var _dbTransaction = _sqlContext.Database.BeginTransaction();
            try
            {
                var product = AutoMapperUtils.AutoMap<ProductDTO, Product>(request.Product);
                product.Id = Guid.NewGuid();
                product.CreatedBy = request.AuthUserId;
                product.CreatedAt = DateTime.Now;
                _sqlContext.Products.Add(product);
                _sqlContext.SaveChanges();
                _dbTransaction.Commit();
                return new CreateProductResponse
                {
                    Code = 1,
                    Id = product.Id,
                    Message = "ok"
                };
            }
            catch (Exception e)
            {
                return new CreateProductResponse
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
                        var productImage = new ProductImage
                        {
                            Id = Guid.NewGuid(),
                            ProductId = request.Id,
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

                        _context.ProductImages.Add(productImage);
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

        public DeleteProductResponse DeleteProduct(DeleteProductRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var product = _sqlContext.Products.FirstOrDefault(s => s.Id == request.Id);
                if (product == null) throw new Exception("Không tồn tại sản phẩm");
                _sqlContext.Products.Remove(product);
                _sqlContext.SaveChanges();
                return new DeleteProductResponse
                {
                    Code = 1,
                    Message = "ok",

                };

            }
            catch (Exception e)
            {
                return new DeleteProductResponse
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

        public GetAllProductResponse GetAllProduct(GetAllProductRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var listProducts = _sqlContext.ViewProducts.ToList();
                var products = AutoMapperUtils.AutoMap<ViewProduct, ProductDTO>(listProducts);
                return new GetAllProductResponse
                {
                    Code = 1,
                    Message = "ok",
                    Products = products
                };
            }
            catch (Exception e)
            {
                return new GetAllProductResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public GetListProductResponse GetListProduct(GetListProductRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var query = _sqlContext.ViewProducts.Where(q => q.PricePrecent > 0).AsQueryable();
                    if (!string.IsNullOrEmpty(request.Name))
                    {
                        query = query.Where(s => s.Name.Contains(request.Name));
                    }
                    if (!string.IsNullOrEmpty(request.Code))
                    {
                        query = query.Where(s => s.Code.Contains(request.Code));
                    }
                    if (request.BrandId != null)
                    {
                        query = query.Where(s => s.BrandId == request.BrandId);
                    }
                    if (request.CategoryId != null)
                    {
                        query = query.Where(s => s.CategoryId == request.CategoryId);
                    }
                    request.Pagination.Total = query.Count();
                    var listProduct = query.Skip((request.Pagination.Page - 1) * request.Pagination.Limit).Take(request.Pagination.Limit).ToList();
                    var productDtos = AutoMapperUtils.AutoMap<ViewProduct, ProductDTO>(listProduct);
                    return new GetListProductResponse
                    {
                        Code = 1,
                        Products = productDtos,
                        Pagination = request.Pagination
                    };

                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        public GetProductByIdResponse GetProductById(GetProductByIdRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var product = _sqlContext.ViewProducts.FirstOrDefault(s => s.Id == request.Id);
                var productDto = AutoMapperUtils.AutoMap<ViewProduct, ProductDTO>(product);
                var products = _sqlContext.ViewProducts.Where(q => q.PricePrecent > 0).Where(p => p.BrandId == product.BrandId && p.CategoryId == product.CategoryId).Take(4).ToList();
                var productDTOs = AutoMapperUtils.AutoMap<ViewProduct, ProductDTO>(products);
                var imageProduct = _sqlContext.Resources.Where(i => _sqlContext.ProductImages.Any(pi => pi.ProductId == request.Id && pi.ResourceId == i.Id)).ToList();
                var images = AutoMapperUtils.AutoMap<Resource, ResourceDTO>(imageProduct);
                productDto.Images = images.Select(s => s.FileName).ToList();
                foreach (var item in productDTOs)
                {
                    var filenames = _sqlContext.Resources.Where(i => _sqlContext.ProductImages.Any(pi => pi.ProductId == item.Id && pi.ResourceId == i.Id)).Select(i => i.FileName).ToList();
                    item.Images = filenames;
                }
                return new GetProductByIdResponse
                {
                    Code = 1,
                    Message = "ok",
                    Product = productDto,
                    Images = images,
                    RelatedProducts = productDTOs
                };
            }
            catch (Exception e)
            {
                return new GetProductByIdResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public UpdateProductResponse UpdateProduct(UpdateProductRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            using var _dbTransaction = _sqlContext.Database.BeginTransaction();
            try
            {
                var product = _sqlContext.Products.FirstOrDefault(s => s.Id == request.Product.Id);
                if (product == null) throw new Exception("Không tồn tại sản phẩm này");
                product.Name = request.Product.Name;
                product.Code = request.Product.Code;
                product.Description = request.Product.Description;
                product.UpdatedBy = request.AuthUserId;
                product.UpdatedAt = DateTime.Now;
                product.CategoryId = request.Product.CategoryId;
                product.BrandId = request.Product.BrandId;
                product.IsHot = request.Product.IsHot;

                foreach (Guid id in request.FileDeleted)
                {
                    var resource = _sqlContext.Resources.FirstOrDefault(s => s.Id == id);
                    var productImage = _sqlContext.ProductImages.Where(pi => pi.ResourceId == id).FirstOrDefault();
                    if (productImage == null) throw new Exception("File không tồn tại");
                    this.DeleteFile(resource.FilePath);
                    _sqlContext.Resources.Remove(resource);
                    _sqlContext.ProductImages.Remove(productImage);
                }
                _sqlContext.SaveChanges();
                _dbTransaction.Commit();
                return new UpdateProductResponse
                {
                    Code = 1,
                    Message = "ok"
                };
            }
            catch (Exception e)
            {
                return new UpdateProductResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public GetListFilterProductResponse GetListFilterProduct(GetListFilterProductRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var query = _sqlContext.ViewProducts.Where(q => q.PricePrecent > 0).AsQueryable();
                if (request.Brands.Count > 0)
                {
                    query = query.Where(p => request.Brands.Contains(p.BrandId));
                }
                if (request.Categories.Count > 0)
                {
                    query = query.Where(p => request.Categories.Contains(p.CategoryId));
                }
                request.Pagination.Total = query.Count();
                var viewProducts = query.Skip((request.Pagination.Page - 1) * request.Pagination.Limit).Take(request.Pagination.Limit).ToList();
                var productDtos = AutoMapperUtils.AutoMap<ViewProduct, ProductDTO>(viewProducts);
                foreach (var item in productDtos)
                {
                    var filenames = _sqlContext.Resources.Where(i => _sqlContext.ProductImages.Any(pi => pi.ProductId == item.Id && pi.ResourceId == i.Id)).Select(i => i.FileName).ToList();
                    item.Images = filenames;
                }
                return new GetListFilterProductResponse()
                {
                    Code = 1,
                    Message = "ok",
                    Products = productDtos,
                    Pagination = request.Pagination
                };
            }
            catch (Exception e)
            {

                return new GetListFilterProductResponse()
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public GetRelatedProductResponse GetRelatedProducts(GetRelatedProductRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var products = _sqlContext.ViewProducts.Where(q => q.PricePrecent > 0).Where(p => p.BrandId == request.BrandId && p.CategoryId == request.CategoryId).Take(4).ToList();
                var productDTOs = AutoMapperUtils.AutoMap<ViewProduct, ProductDTO>(products);
                foreach (var item in productDTOs)
                {
                    var filename = _sqlContext.Resources.Where(i => _sqlContext.ProductImages.Any(pi => pi.ProductId == item.Id && pi.ResourceId == i.Id)).Select(i => i.FileName).ToList();
                    item.Images = filename;
                }
                return new GetRelatedProductResponse()
                {
                    Code = 1,
                    Message = "ok",
                    Products = productDTOs
                };
            }
            catch (Exception e)
            {
                return new GetRelatedProductResponse()
                {
                    Code = 1,
                    Message = e.Message
                };
            }
        }

        public GetTrendingProductsResponse GetTrendingProducts(GetTrendingProductsRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                //var productDTOs = _sqlContext.OrderProducts.GroupBy(op => op.ProductId).OrderByDescending(g => g.Sum(op => op.Quantity ?? 0))
                //             .Take(4)
                //             .Join(_sqlContext.Products, g => g.Key, p => p.Id, (g, p) => new ProductDTO()
                //             {
                //                 Id = p.Id,
                //                 Name = p.Name,
                //                 Code = p.Code,
                //                 Description = p.Description,
                //                 BrandId = p.BrandId,
                //                 CategoryId = p.CategoryId,
                //             })
                //             .ToList();
                var listProducts = _sqlContext.ViewProducts.Where(q => q.PricePrecent > 0 && q.IsHot == true).Take(4).ToList();
                var productDTOs = AutoMapperUtils.AutoMap<ViewProduct, ProductDTO>(listProducts);
                productDTOs.ForEach(item =>
                {
                    var promotionProducts = _sqlContext.PromotionProducts.Where(c => c.ProductId == item.Id);
                    var promotion = _sqlContext.Promotions.Where(c => promotionProducts.Any(any => any.PromotionId == c.Id));
                    item.Promotions = AutoMapperUtils.AutoMap<Promotion, PromotionDTO>(promotion.ToList());

                    var filenames = _sqlContext.Resources.Where(i => _sqlContext.ProductImages.Any(pi => pi.ProductId == item.Id && pi.ResourceId == i.Id)).Select(i => i.FileName).ToList();
                    item.Images = filenames;
                });
                return new GetTrendingProductsResponse()
                {
                    Code = 1,
                    Message = "ok",
                    Products = productDTOs
                };
            }
            catch (Exception e)
            {
                return new GetTrendingProductsResponse()
                {
                    Code = 0,
                    Message = e.Message
                };

            }

        }

        public GetNewProductsResponse GetNewProducts(GetNewProductsRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var products = _sqlContext.ViewProducts.Where(q => q.PricePrecent > 0 && (q.IsHot == false || q.IsHot == null)).OrderByDescending(p => p.CreatedAt).Take(12).ToList();
                var productDTOs = AutoMapperUtils.AutoMap<ViewProduct, ProductDTO>(products);
                productDTOs.ForEach(item =>
                {
                    var promotionProducts = _sqlContext.PromotionProducts.Where(c => c.ProductId == item.Id);
                    var promotion = _sqlContext.Promotions.Where(c => promotionProducts.Any(any => any.PromotionId == c.Id));
                    item.Promotions = AutoMapperUtils.AutoMap<Promotion, PromotionDTO>(promotion.ToList());

                    var filenames = _sqlContext.Resources.Where(i => _sqlContext.ProductImages.Any(pi => pi.ProductId == item.Id && pi.ResourceId == i.Id)).Select(i => i.FileName).ToList();
                    item.Images = filenames;
                });
                return new GetNewProductsResponse()
                {
                    Code = 1,
                    Message = "ok",
                    Products = productDTOs
                };
            }
            catch (Exception e)
            {
                return new GetNewProductsResponse()
                {
                    Code = 1,
                    Message = e.Message
                };
            }
        }

        public GetSearchProductsResponse GetSearchProducts(GetSearchProductsRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var query = _sqlContext.ViewProducts.AsQueryable();
                if (request.CategoryId != null) query = query.Where(p => p.CategoryId == request.CategoryId);
                if (request.BrandId != null) query = query.Where(p => p.BrandId == request.BrandId);

                if (request.ProductName != null)
                {
                    query = query.Where(p => p.Name.Contains(request.ProductName));
                }
                request.Pagination.Total = query.Where(q => q.PricePrecent > 0).Count();
                var products = query.Skip((request.Pagination.Page - 1) * request.Pagination.Limit).Take(request.Pagination.Limit).ToList();
                var productDTOs = AutoMapperUtils.AutoMap<ViewProduct, ProductDTO>(products);
                foreach (var item in productDTOs)
                {
                    var filenames = _sqlContext.Resources.Where(i => _sqlContext.ProductImages.Any(pi => pi.ProductId == item.Id && pi.ResourceId == i.Id)).Select(i => i.FileName).ToList();
                    item.Images = filenames;
                }
                return new GetSearchProductsResponse()
                {
                    Code = 1,
                    Message = "ok",
                    Products = productDTOs,
                    Pagination = request.Pagination

                };
            }
            catch (Exception e)
            {
                return new GetSearchProductsResponse()
                {
                    Code = 0,
                    Message = e.Message
                };

            }
        }
        public GetProductHotResponse GetProductHot(GetProductHotRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var productHots = _sqlContext.Products.Where(c => c.IsHot == true).OrderByDescending(obd => obd.CreatedAt);
                var productHotDTOs = AutoMapperUtils.AutoMap<Product, ProductDTO>(productHots.ToList());
                productHotDTOs.ForEach(item =>
                {
                    var promotionProducts = _sqlContext.PromotionProducts.Where(c => c.ProductId == item.Id);
                    var promotion = _sqlContext.Promotions.Where(c => promotionProducts.Any(any => any.PromotionId == c.Id));
                    item.Promotions = AutoMapperUtils.AutoMap<Promotion, PromotionDTO>(promotion.ToList());

                    var filenames = _sqlContext.Resources.Where(i => _sqlContext.ProductImages.Any(pi => pi.ProductId == item.Id && pi.ResourceId == i.Id)).Select(i => i.FileName).ToList();
                    item.Images = filenames;
                });
                return new GetProductHotResponse()
                {
                    Code = 1,
                    Message = "ok",
                    Products = productHotDTOs,
                };
            }
            catch (Exception e)
            {
                return new GetProductHotResponse()
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public GetListProductForAdminResponse GetListProductForAmin(GetListProductForAdminRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var query = _sqlContext.ViewProducts.Where(q => q.PricePrecent > 0).AsQueryable();
                    if (!string.IsNullOrEmpty(request.Name))
                    {
                        query = query.Where(s => s.Name.Contains(request.Name));
                    }
                    if (!string.IsNullOrEmpty(request.Code))
                    {
                        query = query.Where(s => s.Code.Contains(request.Code));
                    }
                    if (request.BrandId != null)
                    {
                        query = query.Where(s => s.BrandId == request.BrandId);
                    }
                    if (request.CategoryId != null)
                    {
                        query = query.Where(s => s.CategoryId == request.CategoryId);
                    }
                    request.Pagination.Total = query.Count();
                    var listProduct = query.Skip((request.Pagination.Page - 1) * request.Pagination.Limit).Take(request.Pagination.Limit).ToList();
                    var productDtos = AutoMapperUtils.AutoMap<ViewProduct, ProductDTO>(listProduct);
                    return new GetListProductForAdminResponse
                    {
                        Code = 1,
                        Products = productDtos,
                        Pagination = request.Pagination
                    };

                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
    }
}
