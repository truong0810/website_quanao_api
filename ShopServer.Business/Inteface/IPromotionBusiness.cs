using ShopServer.Business.Message.Promotion;
using ShopServer.Business.Message.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Inteface
{
    public interface IPromotionBusiness
    {
        CreatePromotionResponse CreatePromotion(CreatePromotionRequest request);
        DeletePromotionResponse DeletePromotion(DeletePromotionRequest request);
        GetAllPromotionResponse GetAllPromotion(GetAllPromotionRequest request);
        GetLinkImageResponse GetLink(string id);
        GetListPromotionResponse GetListPromotion(GetListPromotionRequest request);
        GetListPromotionProductResponse GetListPromotionProduct(GetListPromotionProductRequest request);
        GetPromotionByIdResponse GetPromotionById(GetPromotionByIdRequest request);
        UpdatePromotionResponse UpdatePromotion(UpdatePromotionRequest request);
        Task<UploadFileResponse> UploadFile(UploadFileRequest request);
    }
}
