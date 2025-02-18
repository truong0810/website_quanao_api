using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Banner;

namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly IBannerBusiness _business;
        public BannerController(IBannerBusiness business)
        {
            _business = business;
        }
        [HttpPost("create")]
        public CreateBannerResponse CreateBanner(CreateBannerRequest request)
        {
            return _business.CreateBanner(request);
        }
        [HttpPost("update")]
        public UpdateBannerResponse UpdateBanner(UpdateBannerRequest request)
        {
            return _business.UpdateBanner(request);
        }
        [HttpPost("delete")]
        public DeleteBannerResponse DeleteBanner(DeleteBannerRequest request)
        {
            return _business.DeleteBanner(request);
        }
        [HttpPost("get-by-id")]
        public GetBannerByIdResponse GetBannerById(GetBannerByIdRequest request)
        {
            return _business.GetBannerById(request);
        }
        [HttpPost("get-list")]
        public GetListBannerResponse GetListBanner(GetListBannerRequest request)
        {
            return _business.GetListBanner(request);
        }
        [HttpPost("get-all")]
        public GetAllBannerResponse GetAllBanner(GetAllBannerRequest request)
        {
            return _business.GetAllBanner(request);
        }
    }
}
