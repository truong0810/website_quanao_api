using ShopServer.Business.Message.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Inteface
{
    public interface ICategoryBusiness
    {
        CreateCategoryResponse CreateCategory(CreateCategoryRequest request);
        DeleteCategoryResponse DeleteCategory(DeleteCategoryRequest request);
        GetAllCategoryResponse GetAllCategory(GetAllCategoryRequest request);
        GetCategoryByIdResponse GetCategoryById(GetCategoryByIdRequest request);
        GetListCategoryResponse GetListCategory(GetListCategoryRequest request);
        UpdateCategoryResponse UpdateCategory(UpdateCategoryRequest request);
    }
}
