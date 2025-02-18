using ShopServer.Business.DTO;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.BlogCategory;
using ShopServer.Business.Ultis;
using ShopServer.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Implement
{
    public class BlogCategoryBusiness : IBlogCategoryBusiness
    {
        public CreateBlogCategoryResponse CreateBlogCategory(CreateBlogCategoryRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var blogCategory = AutoMapperUtils.AutoMap<BlogCategoryDTO, BlogCategory>(request.BlogCategory);
                        blogCategory.Id = Guid.NewGuid();
                        blogCategory.CreatedBy = request.AuthUserId;
                        blogCategory.CreatedDate = DateTime.Now;

                        _sqlContext.BlogCategories.Add(blogCategory);
                        _sqlContext.SaveChanges();
                        _db.Commit();
                        return new CreateBlogCategoryResponse()
                        {
                            Code = 1,
                            Message = "OK",
                        };
                    }
                    catch (Exception ex)
                    {
                        _db.Rollback();
                        return new CreateBlogCategoryResponse()
                        {
                            Code = 0,
                            Message = ex.Message,
                        };

                    }
                }
            }
        }
        public UpdateBlogCategoryResponse UpdateBlogCategory(UpdateBlogCategoryRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var blogCategory = _sqlContext.BlogCategories.FirstOrDefault(c => c.Id == request.BlogCategory.Id);
                        if (blogCategory == null) throw new Exception("Không tồn tại");
                        var resourceOld = _sqlContext.Resources.FirstOrDefault(c => c.Id == blogCategory.AvataId && blogCategory.AvataId != request.BlogCategory.AvataId);
                        if(resourceOld != null)
                        {
                            _sqlContext.Resources.Remove(resourceOld);
                        }
                        blogCategory.Name = request.BlogCategory.Name;
                        blogCategory.Description = request.BlogCategory.Description;
                        blogCategory.AvataId = request.BlogCategory.AvataId;
                        blogCategory.UpdatedDate = DateTime.Now;
                        blogCategory.UpdatedBy = request.AuthUserId;

                        _sqlContext.SaveChanges();
                        _db.Commit();
                        return new UpdateBlogCategoryResponse()
                        {
                            Code = 1,
                            Message = "OK",
                        };
                    }
                    catch (Exception ex)
                    {
                        _db.Rollback();
                        return new UpdateBlogCategoryResponse()
                        {
                            Code = 0,
                            Message = ex.Message,
                        };

                    }
                }
            }
        }
        public DeleteBlogCategoryResponse DeleteBlogCategory(DeleteBlogCategoryRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var blogCategory = _sqlContext.BlogCategories.FirstOrDefault(c => c.Id == request.Id);
                        if (blogCategory == null) throw new Exception("Không tồn tại");

                        var resource = _sqlContext.Resources.FirstOrDefault(c => c.Id == blogCategory.AvataId);
                        if (resource != null)
                        {
                            _sqlContext.Resources.Remove(resource);
                        }
                        _sqlContext.BlogCategories.Remove(blogCategory);
                        _sqlContext.SaveChanges();
                        _db.Commit();
                        return new DeleteBlogCategoryResponse()
                        {
                            Code = 1,
                            Message = "OK",
                        };
                    }
                    catch (Exception ex)
                    {
                        _db.Rollback();
                        return new DeleteBlogCategoryResponse()
                        {
                            Code = 0,
                            Message = ex.Message,
                        };

                    }
                }
            }
        }
        public GetAllBlogCategoryResponse GetAllBlogCategories(GetAllBlogCategoryRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var blogCategorys = _sqlContext.BlogCategories.OrderBy(c => c.Name).ToList();
                    var blogCategoryDTOs = AutoMapperUtils.AutoMap<BlogCategory, BlogCategoryDTO>(blogCategorys);

                    return new GetAllBlogCategoryResponse()
                    {
                        Code = 1,
                        Message = "OK",
                        BlogCategories = blogCategoryDTOs
                    };
                }
                catch (Exception ex)
                {
                    return new GetAllBlogCategoryResponse()
                    {
                        Code = 0,
                        Message = ex.Message,
                    };

                }

            }
        }
        public GetBlogCategoryByIdResponse GetBlogCategoryById(GetBlogCategoryByIdRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var blogCategory = _sqlContext.BlogCategories.FirstOrDefault(c => c.Id == request.Id);
                    if (blogCategory == null) throw new Exception("Không tồn tại");
                    var blogCategoryDTO = AutoMapperUtils.AutoMap<BlogCategory, BlogCategoryDTO>(blogCategory);

                    var resource = _sqlContext.Resources.FirstOrDefault(c => c.Id == blogCategoryDTO.AvataId);
                    var resourceDTO = AutoMapperUtils.AutoMap<Resource, ResourceDTO>(resource);

                    return new GetBlogCategoryByIdResponse()
                    {
                        Code = 1,
                        Message = "OK",
                        BlogCategory = blogCategoryDTO,
                        Resource = resourceDTO,
                    };
                }
                catch (Exception ex)
                {
                    return new GetBlogCategoryByIdResponse()
                    {
                        Code = 0,
                        Message = ex.Message,
                    };
                }
            }
        }
        public GetListBlogCategoryResponse GetListBlogCategories(GetListBlogCategoryRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var pagination = request.Pagination;
                    var blogCategorys = _sqlContext.BlogCategories.Where(c =>
                    (string.IsNullOrEmpty(request.Name) || c.Name.Contains(request.Name))
                     && (string.IsNullOrEmpty(request.Description) || c.Description.Contains(request.Description))
                    ).OrderByDescending(c => c.CreatedDate);
                    pagination.Total = blogCategorys.Count();
                    var listBlogCategory = blogCategorys.Skip((pagination.Page - 1) * pagination.Limit).Take(pagination.Limit);
                    var listBlogCategoryDTO = AutoMapperUtils.AutoMap<BlogCategory, BlogCategoryDTO>(listBlogCategory.ToList());

                    return new GetListBlogCategoryResponse()
                    {
                        Code = 1,
                        Message = "OK",
                        Pagination = pagination,
                        BlogCategories = listBlogCategoryDTO,
                    };
                }
                catch (Exception ex)
                {
                    return new GetListBlogCategoryResponse()
                    {
                        Code = 0,
                        Message = ex.Message,
                    };

                }

            }
        }
    }
}
