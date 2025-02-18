using ShopServer.Business.Message.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Inteface
{
    public interface IBlogBusiness
    {
        CreateBlogResponse CreateBlog(CreateBlogRequest request);
        UpdateBlogResponse UpdateBlog(UpdateBlogRequest request);
        DeleteBlogResponse DeleteBlog(DeleteBlogRequest request);
        GetBlogByIdResponse GetBlogById(GetBlogByIdRequest request);
        GetListBlogResponse GetListBlog(GetListBlogRequest request);
        GetAllBlogResponse GetAllBlog(GetAllBlogRequest request);
        SearchTagBlogCategoryResponse SearchTagBlogCategory(SearchTagBlogCategoryRequest request);
    }
}
