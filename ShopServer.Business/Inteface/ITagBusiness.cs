using ShopServer.Business.Message.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Inteface
{
    public interface ITagBusiness
    {
        CreateTagResponse CreateTag(CreateTagRequest request);
        UpdateTagResponse UpdateTag(UpdateTagRequest request);
        DeleteTagResponse DeleteTag(DeleteTagRequest request);
        GetListTagResponse GetListTag(GetListTagRequest request);
        GetTagByIdResponse GetTagById(GetTagByIdRequest request);
        GetAllTagResponse GetAllTag(GetAllTagRequest request);
    }
}
