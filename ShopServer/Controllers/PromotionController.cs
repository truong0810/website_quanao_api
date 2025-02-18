using Microsoft.AspNetCore.Mvc;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Promotion;
using ShopServer.Business.Message.Resource;

namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionBusiness _business;

        public PromotionController(IPromotionBusiness business)
        {
            _business = business;
        }
        [HttpPost("get-list")]
        public GetListPromotionResponse GetListPromotion(GetListPromotionRequest request)
        {
            return _business.GetListPromotion(request);
        }
        [HttpPost("create")]
        public CreatePromotionResponse CreatePromotion(CreatePromotionRequest request)
        {
            return _business.CreatePromotion(request);
        }
        [HttpPost("get-by-id")]
        public GetPromotionByIdResponse GetPromotionById(GetPromotionByIdRequest request)
        {
            return _business.GetPromotionById(request);
        }
        [HttpPost("update")]
        public UpdatePromotionResponse UpdatePromotion(UpdatePromotionRequest request)
        {
            return _business.UpdatePromotion(request);
        }
        [HttpPost("delete")]
        public DeletePromotionResponse DeletePromotion(DeletePromotionRequest request)
        {
            return _business.DeletePromotion(request);
        }
        [HttpPost("get-all")]
        public GetAllPromotionResponse GetAllPromotion(GetAllPromotionRequest request)
        {
            return _business.GetAllPromotion(request);
        }
        [HttpPost("get-list-promotion-product")]
        public GetListPromotionProductResponse GetListPromotionProduct(GetListPromotionProductRequest request)
        {
            return _business.GetListPromotionProduct(request);
        }
        [HttpPost("upload-image")]
        public Task<UploadFileResponse> UploadImage([FromForm] UploadFileRequest request)
        {
            return _business.UploadFile(request);
        }
        [HttpGet("get-link-image/{fileName}")]
        public IActionResult GetLinkImage([FromRoute] string fileName)
        {
            var response = _business.GetLink(fileName);
            FileStreamResult imageLink = null;
            if (response.Code == 1)
            {
                imageLink = File(new MemoryStream(response.File), "image/jpeg");
            }
            return imageLink;
        }
    }
}
