using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopServer.Attribute;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.BlogCategory;

namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategoryController : ControllerBase
    {
        private readonly IBlogCategoryBusiness _business;
        public BlogCategoryController(IBlogCategoryBusiness business)
        {
            _business = business;
        }
        [HttpPost("create")]
        public CreateBlogCategoryResponse CreateBlogCategory(CreateBlogCategoryRequest request)
        {
            return _business.CreateBlogCategory(request);
        }
        [HttpPost("update")]
        public UpdateBlogCategoryResponse UpdateBlogCategory(UpdateBlogCategoryRequest request)
        {
            return _business.UpdateBlogCategory(request);
        }
        [HttpPost("delete")]
        public DeleteBlogCategoryResponse DeleteBlogCategory(DeleteBlogCategoryRequest request)
        {
            return _business.DeleteBlogCategory(request);
        }
        [HttpPost("get-all")]
        public GetAllBlogCategoryResponse GetAllBlogCategories(GetAllBlogCategoryRequest request)
        {
            return _business.GetAllBlogCategories(request);
        }
        [HttpPost("get-by-id")]
        public GetBlogCategoryByIdResponse GetBlogCategoryById(GetBlogCategoryByIdRequest request)
        {
            return _business.GetBlogCategoryById(request);
        }
        [HttpPost("get-list")]
        public GetListBlogCategoryResponse GetListBlogCategories(GetListBlogCategoryRequest request)
        {
            return _business.GetListBlogCategories(request);
        }
    }
}
