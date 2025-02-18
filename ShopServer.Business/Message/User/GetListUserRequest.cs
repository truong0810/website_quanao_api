using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.User
{
    public class GetListUserRequest:BaseRequest
    {
        public string  UserName { get; set; }   
        public PaginationDTO Pagination { get; set; }
    }
}
