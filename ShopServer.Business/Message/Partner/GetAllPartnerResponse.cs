using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Partner
{
    public class GetAllPartnerResponse : BaseResponse
    {
        public List<PartnerDTO> Partners { get; set; }
    }
}
