﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Ultis.Jwt
{
    public class JwtToken
    {
        private JwtSecurityToken token;
        internal JwtToken(JwtSecurityToken token)
        {
            this.token = token;
        }
        public DateTime ValidTo => token.ValidTo;
        public string Value => new JwtSecurityTokenHandler().WriteToken(this.token);
    }
}
