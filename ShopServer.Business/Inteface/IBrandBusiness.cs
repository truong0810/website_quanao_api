using ShopServer.Business.Message.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Inteface
{
    public interface IBrandBusiness
    {
        CreateBrandResponse CreateBrand(CreateBrandRequest request);
        DeleteBrandResponse DeleteBrand(DeleteBrandRequest request);
        GetAllBrandResponse GetAllBrand(GetAllBrandRequest request);
        GetBrandByIdResponse GetBrandById(GetBrandByIdRequest request);
        GetListBrandResponse GetListBrand(GetListBrandRequest request);
        UpdateBrandResponse UpdateBrand(UpdateBrandRequest request);
    }
}
