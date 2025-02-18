﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Message.Auth
{
    public class LoginRequest : BaseRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
