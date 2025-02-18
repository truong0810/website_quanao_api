using ShopServer.Business.DTO;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Banner;
using ShopServer.Business.Message.Blog;
using ShopServer.Business.Ultis;
using ShopServer.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Implement
{
    public class BannerBusiness : IBannerBusiness
    {
        public CreateBannerResponse CreateBanner(CreateBannerRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var banner = AutoMapperUtils.AutoMap<BannerDTO, Banner>(request.Banner);
                        banner.Id = Guid.NewGuid();
                        banner.CreatedBy = request.AuthUserId;
                        banner.CreatedDate = DateTime.Now;

                        _sqlContext.Banners.Add(banner);
                        _sqlContext.SaveChanges();
                        _db.Commit();
                        return new CreateBannerResponse()
                        {
                            Code = 1,
                            Message = "OK",
                        };
                    }
                    catch (Exception ex)
                    {
                        _db.Rollback();
                        return new CreateBannerResponse()
                        {
                            Code = 0,
                            Message = ex.Message
                        };
                    }
                }
            }
        }
        public UpdateBannerResponse UpdateBanner(UpdateBannerRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var banner = _sqlContext.Banners.FirstOrDefault(c => c.Id == request.Banner.Id);
                        if (banner == null) throw new Exception("Không tìm thấy");
                        var resourceOld = _sqlContext.Resources.FirstOrDefault(c => c.Id == banner.ImageId && banner.ImageId != request.Banner.ImageId);
                        if (resourceOld != null)
                        {
                            _sqlContext.Resources.Remove(resourceOld);
                        }
                        banner.Name = request.Banner.Name;
                        banner.SortIndex = request.Banner.SortIndex;
                        banner.UpdatedBy = request.AuthUserId;
                        banner.UpdatedDate = DateTime.Now;
                        banner.ImageId = request.Banner.ImageId;

                        _sqlContext.SaveChanges();
                        _db.Commit();
                        return new UpdateBannerResponse()
                        {
                            Code = 1,
                            Message = "OK",
                        };
                    }
                    catch (Exception ex)
                    {
                        _db.Rollback();
                        return new UpdateBannerResponse()
                        {
                            Code = 0,
                            Message = ex.Message
                        };
                    }
                }
            }
        }
        public DeleteBannerResponse DeleteBanner(DeleteBannerRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var banner = _sqlContext.Banners.FirstOrDefault(c => c.Id == request.Id);
                        if (banner == null) throw new Exception("Không tìm thấy");
                        var resourceOld = _sqlContext.Resources.FirstOrDefault(c => c.Id == banner.ImageId);
                        if (resourceOld != null)
                        {
                            _sqlContext.Resources.Remove(resourceOld);
                        }
                        _sqlContext.Banners.Remove(banner);
                        _sqlContext.SaveChanges();
                        _db.Commit();
                        return new DeleteBannerResponse()
                        {
                            Code = 1,
                            Message = "OK",
                        };
                    }
                    catch (Exception ex)
                    {
                        _db.Rollback();
                        return new DeleteBannerResponse()
                        {
                            Code = 0,
                            Message = ex.Message
                        };
                    }
                }
            }
        }
        public GetBannerByIdResponse GetBannerById(GetBannerByIdRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var banner = _sqlContext.Banners.FirstOrDefault(c => c.Id == request.Id);
                    if (banner == null) throw new Exception("Không tìm thấy");
                    var bannerDTO = AutoMapperUtils.AutoMap<Banner, BannerDTO>(banner);
                    var resource = _sqlContext.Resources.FirstOrDefault(c => c.Id == banner.ImageId);
                    var resourceDTO = AutoMapperUtils.AutoMap<Resource, ResourceDTO>(resource);

                    return new GetBannerByIdResponse()
                    {
                        Code = 1,
                        Message = "OK",
                        Banner = bannerDTO,
                        Resource = resourceDTO
                    };
                }
                catch (Exception ex)
                {
                    return new GetBannerByIdResponse()
                    {
                        Code = 0,
                        Message = ex.Message
                    };
                }

            }
        }
        public GetListBannerResponse GetListBanner(GetListBannerRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var pagination = request.Pagination;
                    var banners = _sqlContext.Banners.Where(c =>
                    (string.IsNullOrEmpty(request.Name) || c.Name.Contains(request.Name))
                    ).OrderBy(c => c.SortIndex);
                    pagination.Total = banners.Count();
                    var listBanner = banners.Skip((pagination.Page - 1) * pagination.Limit).Take(pagination.Limit);
                    var listBannerDTO = AutoMapperUtils.AutoMap<Banner, BannerDTO>(listBanner.ToList());
                    return new GetListBannerResponse()
                    {
                        Code = 1,
                        Message = "OK",
                        Banners = listBannerDTO,
                       Pagination = pagination,
                    };
                }
                catch (Exception ex)
                {
                    return new GetListBannerResponse()
                    {
                        Code = 0,
                        Message = ex.Message
                    };
                }

            }
        }
        public GetAllBannerResponse GetAllBanner(GetAllBannerRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {             
                try
                {
                    var banners = _sqlContext.Banners.OrderByDescending(c => c.CreatedDate).ToList();
                    var bannerDTOs = AutoMapperUtils.AutoMap<Banner, BannerDTO>(banners);
                    return new GetAllBannerResponse()
                    {
                        Code = 1,
                        Message = "OK",
                        Banners = bannerDTOs
                    };
                }
                catch (Exception ex)
                {
                    return new GetAllBannerResponse()
                    {
                        Code = 0,
                        Message = ex.Message
                    };
                }

            }
        }
    }
}
