using Microsoft.AspNetCore.Mvc;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.ProductPrices;
namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPriceController : ControllerBase
    {
        private readonly IProductPriceBusiness _business;

        public ProductPriceController(IProductPriceBusiness business)
        {
            _business = business;
        }

        [HttpPost("get-list")]
        public GetListProductPriceResponse GetListProductPrice(GetListProductPriceRequest request)
        {
            return _business.GetListProductPrice(request);
        }
        [HttpPost("create")]
        public CreateProductPriceResponse CreateProductPrice(CreateProductPriceRequest request)
        {
            return _business.CreateProductPrice(request);
        }

        [HttpPost("update")]
        public UpdateProductPriceResponse UpdateProductPrice(UpdateProductPriceRequest request)
        {
            return _business.UpdateProductPrice(request);
        }

        [HttpPost("delete")]
        public DeleteProductPriceResponse DeleteProductPrice(DeleteProductPriceRequest request)
        {
            return _business.DeleteProductPrice(request);
        }
        [HttpPost("get-by-id")]
        public GetProductPriceByIdResponse GetProductPriceById(GetProductPriceByIdRequest request)
        {
            return _business.GetProductPriceById(request);
        }

        [HttpPost("get-by-product")]
        public GetProductPriceByProductResponse GetProductPriceByProduct(GetProductPriceByProductRequest request)
        {
            return _business.GetProductPriceByProduct(request);
        }
    }
}
