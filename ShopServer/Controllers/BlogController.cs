using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopServer.Attribute;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Blog;

namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogBusiness _business;
        public BlogController(IBlogBusiness business)
        {
            _business = business;
        }
        [HttpPost("create")]
        public CreateBlogResponse CreateBlog(CreateBlogRequest request)
        {
            return _business.CreateBlog(request);
        }
        [HttpPost("update")]
        public UpdateBlogResponse UpdateBlog(UpdateBlogRequest request)
        {
            return _business.UpdateBlog(request);
        }
        [HttpPost("delete")]
        public DeleteBlogResponse DeleteBlog(DeleteBlogRequest request)
        {
            return _business.DeleteBlog(request);
        }
        [HttpPost("get-by-id")]
        public GetBlogByIdResponse GetBlogById(GetBlogByIdRequest request)
        {
            return _business.GetBlogById(request);
        }
        [HttpPost("get-list")]
        public GetListBlogResponse GetListBlog(GetListBlogRequest request)
        {
            return _business.GetListBlog(request);
        }
        [HttpPost("get-all")]
        public GetAllBlogResponse GetAllBlog(GetAllBlogRequest request)
        {
            return _business.GetAllBlog(request);
        }
        [HttpPost("search-blogtag-blocategory")]
        public SearchTagBlogCategoryResponse SearchTagBlogCategory(SearchTagBlogCategoryRequest request)
        {
            return _business.SearchTagBlogCategory(request);
        }
    }
}
