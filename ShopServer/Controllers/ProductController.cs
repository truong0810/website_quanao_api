using Microsoft.AspNetCore.Mvc;
using ShopServer.Attribute;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Product;
using ShopServer.Business.Message.Promotion;
using ShopServer.Business.Message.Resource;

namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusiness _business;

        public ProductController(IProductBusiness business)
        {
            _business = business;
        }
        [HttpPost("get-list")]
        public GetListProductResponse GetListProduct(GetListProductRequest request)
        {
            return _business.GetListProduct(request);
        }
        [HttpPost("get-list-for-admin")]
        public GetListProductForAdminResponse GetListProductForAdmin(GetListProductForAdminRequest request)
        {
            return _business.GetListProductForAmin(request);
        }

        [HttpPost("get-list-filter")]
        public GetListFilterProductResponse GetListFilterProduct(GetListFilterProductRequest request)
        {
            return _business.GetListFilterProduct(request);
        }
        [HttpPost("create")]
        public CreateProductResponse CreateProduct(CreateProductRequest request)
        {
            return _business.CreateProduct(request);
        }
        [HttpPost("get-by-id")]
        public GetProductByIdResponse GetProductById(GetProductByIdRequest request)
        {
            return _business.GetProductById(request);
        }
        [HttpPost("update")]
        public UpdateProductResponse UpdateProduct(UpdateProductRequest request)
        {
            return _business.UpdateProduct(request);
        }
        [HttpPost("delete")]
        public DeleteProductResponse DeleteProduct(DeleteProductRequest request)
        {
            return _business.DeleteProduct(request);
        }
        [HttpPost("get-all")]
        public GetAllProductResponse GetAllProduct(GetAllProductRequest request)
        {
            return _business.GetAllProduct(request);
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
        [HttpPost("get-related-products")]
        public GetRelatedProductResponse GetRelatedProducts(GetRelatedProductRequest request)
        {
            return _business.GetRelatedProducts(request);
        }
        [HttpPost("get-trending-products")]
        public GetTrendingProductsResponse GetTrendingProducts(GetTrendingProductsRequest request)
        {
            return _business.GetTrendingProducts(request);
        }
        [HttpPost("get-new-products")]
        public GetNewProductsResponse GetNewProducts(GetNewProductsRequest request)
        {
            return _business.GetNewProducts(request);
        }
        [HttpPost("get-search-products")]
        public GetSearchProductsResponse GetSearchProducts(GetSearchProductsRequest request)
        {
            return _business.GetSearchProducts(request);
        }
        [HttpPost("get-product-hot")]
        public GetProductHotResponse GetProductHot(GetProductHotRequest request)
        {
            return _business.GetProductHot(request);
        }
    }
}
