using ShopServer.Business.Message.BlogCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Inteface
{
    public interface IBlogCategoryBusiness
    {
        CreateBlogCategoryResponse CreateBlogCategory(CreateBlogCategoryRequest request);
        UpdateBlogCategoryResponse UpdateBlogCategory(UpdateBlogCategoryRequest request);
        DeleteBlogCategoryResponse DeleteBlogCategory(DeleteBlogCategoryRequest request);
        GetAllBlogCategoryResponse GetAllBlogCategories(GetAllBlogCategoryRequest request);
        GetBlogCategoryByIdResponse GetBlogCategoryById(GetBlogCategoryByIdRequest request);
        GetListBlogCategoryResponse GetListBlogCategories(GetListBlogCategoryRequest request);
    }
}
