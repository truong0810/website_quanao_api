using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Auth
{
    public class RegisterRequest : BaseRequest
    {
        public UserDTO User { get; set; }
        public string PasswordCheck { get; set; }
    }
}
