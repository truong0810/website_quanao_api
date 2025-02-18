using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Auth
{
    public class RefreshTokenRequest : BaseRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
