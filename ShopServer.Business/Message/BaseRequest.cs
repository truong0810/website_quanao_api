using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message
{
    public class BaseRequest
    {
        public Guid? AuthUserId { get; set; }
        public string AuthUsername { get; set; }
        public UserDTO AuthUser { get; set; }
    }
}
