using ShopServer.Business.Message.Product;
using ShopServer.Business.Message.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Inteface
{
    public interface IProductBusiness
    {
        CreateProductResponse CreateProduct(CreateProductRequest request);
        DeleteProductResponse DeleteProduct(DeleteProductRequest request);
        GetAllProductResponse GetAllProduct(GetAllProductRequest request);
        GetLinkImageResponse GetLink(string fileName);
        GetListFilterProductResponse GetListFilterProduct(GetListFilterProductRequest request);
        GetListProductResponse GetListProduct(GetListProductRequest request);
        GetListProductForAdminResponse GetListProductForAmin(GetListProductForAdminRequest request);
        GetNewProductsResponse GetNewProducts(GetNewProductsRequest request);
        GetProductByIdResponse GetProductById(GetProductByIdRequest request);
        GetRelatedProductResponse GetRelatedProducts(GetRelatedProductRequest request);
        GetSearchProductsResponse GetSearchProducts(GetSearchProductsRequest request);
        GetTrendingProductsResponse GetTrendingProducts(GetTrendingProductsRequest request);
        UpdateProductResponse UpdateProduct(UpdateProductRequest request);
        Task<UploadFileResponse> UploadFile(UploadFileRequest request);
        GetProductHotResponse GetProductHot(GetProductHotRequest request);
    }
}
