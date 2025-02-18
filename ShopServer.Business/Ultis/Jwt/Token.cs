using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Ultis.Jwt
{
    public class Token
    {
        public Guid AuthUserId { get; set; }
        public string AuthUsername { get; set; }
        public UserDTO UserAuth { get; set; }
        public double Expire { get; set; }
    }
}
