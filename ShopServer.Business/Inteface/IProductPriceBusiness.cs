using ShopServer.Business.Message.ProductPrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Inteface
{
    public interface IProductPriceBusiness
    {
        CreateProductPriceResponse CreateProductPrice(CreateProductPriceRequest request);
        DeleteProductPriceResponse DeleteProductPrice(DeleteProductPriceRequest request);
        GetListProductPriceResponse GetListProductPrice(GetListProductPriceRequest request);
        GetProductPriceByIdResponse GetProductPriceById(GetProductPriceByIdRequest request);
        GetProductPriceByProductResponse GetProductPriceByProduct(GetProductPriceByProductRequest request);
        UpdateProductPriceResponse UpdateProductPrice(UpdateProductPriceRequest request);
    }
}
