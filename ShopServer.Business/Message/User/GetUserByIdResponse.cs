﻿using ShopServer.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.User
{
    public class GetUserByIdResponse:BaseResponse
    {
        public UserDTO User { get; set; }   
    }
}
