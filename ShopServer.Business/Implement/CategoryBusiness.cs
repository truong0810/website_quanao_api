using ShopServer.Business.DTO;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Category;
using ShopServer.Business.Ultis;
using ShopServer.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Implement
{
    public class CategoryBusiness : ICategoryBusiness
    {
        public CreateCategoryResponse CreateCategory(CreateCategoryRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _dbTransaction = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var category = AutoMapperUtils.AutoMap<CategoryDTO, Category>(request.Category);
                        category.Id = Guid.NewGuid();
                        _sqlContext.Categories.Add(category);
                        _sqlContext.SaveChanges();
                        _dbTransaction.Commit();
                        return new CreateCategoryResponse
                        {
                            Code = 1,
                            Message = "ok"
                        };

                    }
                    catch (Exception e)
                    {
                        _dbTransaction.Rollback();
                        return new CreateCategoryResponse
                        {
                            Code = 0,
                            Message = e.Message
                        };
                    }
                }
            }
        }

        public DeleteCategoryResponse DeleteCategory(DeleteCategoryRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var category = _sqlContext.Categories.FirstOrDefault(s => s.Id == request.Id);
                if (category == null) throw new Exception("Danh mục không tồn tại");
                _sqlContext.Categories.Remove(category);
                _sqlContext.SaveChanges();
                return new DeleteCategoryResponse
                {
                    Code = 1,
                    Message = "ok"
                };
            }
            catch (Exception e)
            {
                return new DeleteCategoryResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public GetAllCategoryResponse GetAllCategory(GetAllCategoryRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var categories = _sqlContext.Categories.ToList();
                var categoryDtos = AutoMapperUtils.AutoMap<Category, CategoryDTO>(categories);
                return new GetAllCategoryResponse
                {
                    Code = 1,
                    Message = "ok",
                    Categories = categoryDtos
                };
            }
            catch (Exception e)
            {
                return new GetAllCategoryResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public GetCategoryByIdResponse GetCategoryById(GetCategoryByIdRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var category = _sqlContext.Categories.FirstOrDefault(s => s.Id == request.Id);
                if (category == null) throw new Exception("Không tồn tại thương hiệu");
                var categoryDto = AutoMapperUtils.AutoMap<Category, CategoryDTO>(category);
                return new GetCategoryByIdResponse
                {
                    Code = 1,
                    Message = "ok",
                    Category = categoryDto
                };
            }
            catch (Exception e)
            {
                return new GetCategoryByIdResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }
        public GetListCategoryResponse GetListCategory(GetListCategoryRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            try
            {
                var categories = _sqlContext.Categories.Where(s => (string.IsNullOrEmpty(request.Name) || s.Name.Contains(request.Name)));
                request.Pagination.Total = categories.Count();
                var listCategory = categories.Skip((request.Pagination.Page - 1) * request.Pagination.Limit).Take(request.Pagination.Limit).ToList();
                var listCategoryDto = AutoMapperUtils.AutoMap<Category, CategoryDTO>(listCategory);
                return new GetListCategoryResponse
                {
                    Code = 1,
                    Message = "ok",
                    ListCategory = listCategoryDto,
                    Pagination = request.Pagination
                };
            }
            catch (Exception e)
            {
                return new GetListCategoryResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }

        public UpdateCategoryResponse UpdateCategory(UpdateCategoryRequest request)
        {
            using var _sqlContext = new PnpWebContext();
            using var _dbTransaction = _sqlContext.Database.BeginTransaction();
            try
            {
                var brand = _sqlContext.Categories.FirstOrDefault(s => s.Id == request.Category.Id);
                if (brand == null) throw new Exception("Không tồn tại thương hiệu");
                brand.Name = request.Category.Name;
                brand.Description = request.Category.Description;
                _sqlContext.SaveChanges();
                _dbTransaction.Commit();
                return new UpdateCategoryResponse
                {
                    Code = 1,
                    Message = "ok"
                };
            }
            catch (Exception e)
            {
                return new UpdateCategoryResponse
                {
                    Code = 0,
                    Message = e.Message
                };
            }
        }
    }
}
