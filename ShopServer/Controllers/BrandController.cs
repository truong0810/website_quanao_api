using Microsoft.AspNetCore.Mvc;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Brand;

namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandBusiness _business;

        public BrandController(IBrandBusiness business)
        {
            _business = business;
        }
        [HttpPost("get-list")]
        public GetListBrandResponse GetListBrand(GetListBrandRequest request)
        {
            return _business.GetListBrand(request);
        }
        [HttpPost("create")]
        public CreateBrandResponse CreateBrand(CreateBrandRequest request)
        {
            return _business.CreateBrand(request);
        }
        [HttpPost("update")]
        public UpdateBrandResponse UpdateBrand(UpdateBrandRequest request)
        {
            return _business.UpdateBrand(request);
        }
        [HttpPost("get-by-id")]
        public GetBrandByIdResponse GetBrandById(GetBrandByIdRequest request)
        {
            return _business.GetBrandById(request);
        }
        [HttpPost("get-all")]
        public GetAllBrandResponse GetAllBrand(GetAllBrandRequest request)
        {
            return _business.GetAllBrand(request);
        }
        [HttpPost("delete")]
        public DeleteBrandResponse DeleteBrand(DeleteBrandRequest request)
        {
            return _business.DeleteBrand(request);
        }
    }
}
