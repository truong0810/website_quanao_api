using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Banner
{
    public class DeleteBannerRequest : BaseRequest
    {
        public Guid? Id { get; set; }
    }
}
