using Microsoft.AspNetCore.Mvc;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Category;

namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryBusiness _business;

        public CategoryController(ICategoryBusiness business)
        {
            _business = business;
        }
        [HttpPost("get-list")]
        public GetListCategoryResponse GetListCategory(GetListCategoryRequest request)
        {
            return _business.GetListCategory(request);
        }
        [HttpPost("create")]
        public CreateCategoryResponse CreateCategory(CreateCategoryRequest request)
        {
            return _business.CreateCategory(request);
        }
        [HttpPost("update")]
        public UpdateCategoryResponse UpdateBrand(UpdateCategoryRequest request)
        {
            return _business.UpdateCategory(request);
        }
        [HttpPost("get-by-id")]
        public GetCategoryByIdResponse GetBrandById(GetCategoryByIdRequest request)
        {
            return _business.GetCategoryById(request);
        }
        [HttpPost("get-all")]
        public GetAllCategoryResponse GetAllCategory(GetAllCategoryRequest request)
        {
            return _business.GetAllCategory(request);
        }
        [HttpPost("delete")]
        public DeleteCategoryResponse DeleteCategory(DeleteCategoryRequest request)
        {
            return _business.DeleteCategory(request);
        }
    }
}
