using ShopServer.Business.DTO;
using ShopServer.Business.Inteface;
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
    public class BlogBusiness : IBlogBusiness
    {
        public CreateBlogResponse CreateBlog(CreateBlogRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var blog = AutoMapperUtils.AutoMap<BlogDTO, Blog>(request.Blog);
                        blog.Id = Guid.NewGuid();
                        blog.CreatedBy = request.AuthUserId;
                        blog.CreatedDate = DateTime.Now;
                        _sqlContext.Blogs.Add(blog);

                        if (request.BlogTags != null && request.BlogTags.Count() > 0)
                        {
                            var blogTags = AutoMapperUtils.AutoMap<BlogTagDTO, BlogTag>(request.BlogTags);
                            blogTags.ForEach((item =>
                            {
                                item.Id = Guid.NewGuid();
                                item.BlogId = blog.Id;
                            }));
                            _sqlContext.BlogTags.AddRange(blogTags);
                        }
                        if (request.BlogOfCategories != null && request.BlogOfCategories.Count() > 0)
                        {
                            var blogOfCategory = AutoMapperUtils.AutoMap<BlogOfCategoryDTO, BlogOfCategory>(request.BlogOfCategories);
                            blogOfCategory.ForEach(item =>
                            {
                                item.Id = Guid.NewGuid();
                                item.BlogId = blog.Id;
                            });
                            _sqlContext.BlogOfCategories.AddRange(blogOfCategory);
                        }

                        if (request.BlogImages.Count() > 0 && request.BlogImages != null)
                        {
                            var blogImage = AutoMapperUtils.AutoMap<BlogImageDTO, BlogImage>(request.BlogImages);
                            blogImage.ForEach(item =>
                            {
                                item.Id = Guid.NewGuid();
                                item.BlogId = blog.Id;
                            });
                            _sqlContext.BlogImages.AddRange(blogImage);
                        }

                        _sqlContext.SaveChanges();
                        _db.Commit();
                        return new CreateBlogResponse()
                        {
                            Code = 1,
                            Message = "OK",
                        };
                    }
                    catch (Exception ex)
                    {
                        _db.Rollback();
                        return new CreateBlogResponse()
                        {
                            Code = 0,
                            Message = ex.Message
                        };
                    }
                }
            }
        }
        public UpdateBlogResponse UpdateBlog(UpdateBlogRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var blog = _sqlContext.Blogs.FirstOrDefault(b => b.Id == request.Blog.Id);
                        if (blog == null) throw new Exception("Không tồn tại");
                        blog.Text = request.Blog.Text;
                        blog.Description = request.Blog.Description;
                        blog.BlogDate = request.Blog.BlogDate;
                        blog.Title = request.Blog.Title;
                        blog.UpdatedDate = DateTime.Now;
                        blog.UpdatedBy = request.AuthUserId;

                        var oldBlogTags = _sqlContext.BlogTags.Where(old => old.BlogId == blog.Id);
                        _sqlContext.BlogTags.RemoveRange(oldBlogTags);
                        if (request.BlogTags != null && request.BlogTags.Count() > 0)
                        {
                            var blogTags = AutoMapperUtils.AutoMap<BlogTagDTO, BlogTag>(request.BlogTags);
                            blogTags.ForEach((item =>
                            {
                                item.Id = Guid.NewGuid();
                                item.BlogId = blog.Id;
                            }));
                            _sqlContext.BlogTags.AddRange(blogTags);
                        }

                        var oldBlogOfCategory = _sqlContext.BlogOfCategories.Where(old => old.BlogId == blog.Id);
                        _sqlContext.BlogOfCategories.RemoveRange(oldBlogOfCategory);
                        if (request.BlogOfCategories != null && request.BlogOfCategories.Count() > 0)
                        {
                            var blogOfCategory = AutoMapperUtils.AutoMap<BlogOfCategoryDTO, BlogOfCategory>(request.BlogOfCategories);
                            blogOfCategory.ForEach(item =>
                            {
                                item.Id = Guid.NewGuid();
                                item.BlogId = blog.Id;
                            });
                            _sqlContext.BlogOfCategories.AddRange(blogOfCategory);
                        }
                        var blogImageNotDelete = request.BlogImages.Where(c => c.Id != Guid.Empty).Select(cd => cd.Id).ToList();
                        var oldBlogImages = _sqlContext.BlogImages.Where(old => old.BlogId == blog.Id && !blogImageNotDelete.Any(cd => cd == old.Id));
                        _sqlContext.BlogImages.RemoveRange(oldBlogImages);
                        if (request.BlogImages.Count() > 0 && request.BlogImages != null)
                        {
                            var blogImage = AutoMapperUtils.AutoMap<BlogImageDTO, BlogImage>(request.BlogImages.Where(cd => cd.Id == Guid.Empty || cd.Id == null).ToList());
                            blogImage.ForEach(item =>
                            {
                                item.Id = Guid.NewGuid();
                                item.BlogId = blog.Id;
                            });
                            _sqlContext.BlogImages.AddRange(blogImage);
                        }

                        _sqlContext.SaveChanges();
                        _db.Commit();
                        return new UpdateBlogResponse()
                        {
                            Code = 1,
                            Message = "OK",
                        };
                    }
                    catch (Exception ex)
                    {
                        _db.Rollback();
                        return new UpdateBlogResponse()
                        {
                            Code = 0,
                            Message = ex.Message
                        };
                    }
                }
            }
        }
        public DeleteBlogResponse DeleteBlog(DeleteBlogRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var blog = _sqlContext.Blogs.FirstOrDefault(b => b.Id == request.Id);
                        if (blog == null) { throw new Exception("Không tồn tại"); }
                        _sqlContext.Blogs.Remove(blog);

                        var blogTags = _sqlContext.BlogTags.Where(c => c.BlogId == blog.Id);
                        _sqlContext.BlogTags.RemoveRange(blogTags);

                        var blogOfCategorys = _sqlContext.BlogOfCategories.Where(c => c.BlogId == blog.Id);
                        _sqlContext.BlogOfCategories.RemoveRange(blogOfCategorys);

                        var blogImages = _sqlContext.BlogImages.Where(c => c.BlogId == blog.Id);
                        _sqlContext.BlogImages.RemoveRange(blogImages);

                        _sqlContext.SaveChanges();
                        _db.Commit();
                        return new DeleteBlogResponse()
                        {
                            Code = 1,
                            Message = "OK",
                        };
                    }
                    catch (Exception ex)
                    {
                        _db.Rollback();
                        return new DeleteBlogResponse()
                        {
                            Code = 0,
                            Message = ex.Message
                        };
                    }
                }
            }
        }
        public GetBlogByIdResponse GetBlogById(GetBlogByIdRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var blog = _sqlContext.Blogs.FirstOrDefault(c => c.Id == request.Id);
                    if (blog == null) throw new Exception("Không tồn tại");
                    var blogDTO = AutoMapperUtils.AutoMap<Blog, BlogDTO>(blog);

                    var blogTags = _sqlContext.BlogTags.Where(c => c.BlogId == blog.Id);
                    var blogTagDTOs = AutoMapperUtils.AutoMap<BlogTag, BlogTagDTO>(blogTags.ToList());
                    blogTagDTOs.ForEach(item =>
                    {
                        var tag = _sqlContext.Tags.FirstOrDefault(c => c.Id == item.TagId);
                        item.Tag = AutoMapperUtils.AutoMap<Tag, TagDTO>(tag);
                    });

                    var blogOfCategories = _sqlContext.BlogOfCategories.Where(c => c.BlogId == blog.Id);
                    var blogOfCategorieDTOs = AutoMapperUtils.AutoMap<BlogOfCategory, BlogOfCategoryDTO>(blogOfCategories.ToList());
                    blogOfCategorieDTOs.ForEach(item =>
                    {
                        var blogCategory = _sqlContext.BlogCategories.FirstOrDefault(c => c.Id == item.BlogCategoryId);
                        item.BlogCategory = AutoMapperUtils.AutoMap<BlogCategory, BlogCategoryDTO>(blogCategory);
                    });

                    var blogImages = _sqlContext.BlogImages.Where(c => c.BlogId == blog.Id);
                    var blogImageDTOs = AutoMapperUtils.AutoMap<BlogImage, BlogImageDTO>(blogImages.ToList());
                    blogImageDTOs.ForEach(item =>
                    {
                        var resource = _sqlContext.Resources.FirstOrDefault(c => c.Id == item.BlogImageId);
                        item.Resource = AutoMapperUtils.AutoMap<Resource, ResourceDTO>(resource);
                    });

                    return new GetBlogByIdResponse()
                    {
                        Code = 1,
                        Message = "OK",
                        Blog = blogDTO,
                        BlogImages = blogImageDTOs,
                        BlogOfCategories = blogOfCategorieDTOs,
                        BlogTags = blogTagDTOs,
                    };
                }
                catch (Exception ex)
                {
                    return new GetBlogByIdResponse()
                    {
                        Code = 0,
                        Message = ex.Message
                    };
                }
            }
        }
        public GetListBlogResponse GetListBlog(GetListBlogRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var blogTags = _sqlContext.BlogTags.Where(c => c.TagId == request.BlogTagId).Select(c => c.BlogId);
                    var blogOfCategories = _sqlContext.BlogOfCategories.Where(c => c.BlogCategoryId == request.BlogCategoryId).Select(c => c.BlogId);
                    var pagination = request.Pagination;

                    var blogs = _sqlContext.Blogs.Where(c =>
                    (string.IsNullOrEmpty(request.Title) || c.Title.Contains(request.Title))
                    && (!blogTags.Any() || blogTags.Contains(c.Id))
                    && (!blogOfCategories.Any() || blogOfCategories.Contains(c.Id))
                    ).OrderByDescending(obd => obd.CreatedDate);

                    pagination.Total = blogs.Count();
                    var listBlog = blogs.Skip((pagination.Page - 1) * pagination.Limit).Take(pagination.Limit).ToList();
                    var blogDTOs = AutoMapperUtils.AutoMap<Blog, BlogDTO>(listBlog);
                    blogDTOs.ForEach(item =>
                    {
                        var blogTags = _sqlContext.BlogTags.Where(c => c.BlogId == item.Id);
                        var tags = _sqlContext.Tags.Where(c => blogTags.Any(bt => bt.TagId == c.Id));
                        var tagDTOs = AutoMapperUtils.AutoMap<Tag, TagDTO>(tags.ToList());

                        var blogImages = _sqlContext.BlogImages.Where(c => c.BlogId == item.Id);
                        item.BlogImages = AutoMapperUtils.AutoMap<BlogImage, BlogImageDTO>(blogImages.ToList());

                        var blogOfCategories = _sqlContext.BlogOfCategories.Where(c => c.BlogId == item.Id);
                        var blogCategories = _sqlContext.BlogCategories.Where(c => blogOfCategories.Any(bt => bt.BlogCategoryId == c.Id));
                        var blogCategoryDTOs = AutoMapperUtils.AutoMap<BlogCategory, BlogCategoryDTO>(blogCategories.ToList());

                    });

                    return new GetListBlogResponse()
                    {
                        Code = 1,
                        Message = "OK",
                        Blogs = blogDTOs,
                        Pagination = pagination,
                    };
                }
                catch (Exception ex)
                {
                    return new GetListBlogResponse()
                    {
                        Code = 0,
                        Message = ex.Message
                    };
                }
            }
        }
        public GetAllBlogResponse GetAllBlog(GetAllBlogRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    return new GetAllBlogResponse()
                    {
                        Code = 1,
                        Message = "OK",
                    };
                }
                catch (Exception ex)
                {
                    return new GetAllBlogResponse()
                    {
                        Code = 0,
                        Message = ex.Message
                    };
                }
            }
        }
        public SearchTagBlogCategoryResponse SearchTagBlogCategory(SearchTagBlogCategoryRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var blogTags = _sqlContext.BlogTags.Where(c => c.TagId == request.BlogTagId).Select(c => c.BlogId);
                    var blogOfCategories = _sqlContext.BlogOfCategories.Where(c => c.BlogCategoryId == request.BlogCategoryId).Select(c => c.BlogId);

                    var blogs = _sqlContext.Blogs.Where(c =>
                    (blogTags.Contains(c.Id) || blogOfCategories.Contains(c.Id))
                    ).OrderByDescending(obd => obd.CreatedDate);

                    var blogDTOs = AutoMapperUtils.AutoMap<Blog, BlogDTO>(blogs.ToList());
                    return new SearchTagBlogCategoryResponse()
                    {
                        Code = 1,
                        Message = "OK",
                        Blogs = blogDTOs,

                    };
                }
                catch (Exception ex)
                {
                    return new SearchTagBlogCategoryResponse()
                    {
                        Code = 0,
                        Message = ex.Message
                    };
                }
            }
        }
    }
}
