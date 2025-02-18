using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShopServer.Business.Ultis.Constant;
namespace ShopServer.Business.Ultis.Jwt
{
    public class TokenProvider
    {
        public static bool TokenValidate(string token, out Token data)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("web-service-pnps-secret-key-name"));
            var validattionParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = false,
                ValidateAudience = false,
                ValidateIssuer = false,
                RequireExpirationTime = false
            };
            data = new Token();
            try
            {
                new JwtSecurityTokenHandler().
                    ValidateToken(token, validattionParameters, out var rawValidatedToken);
                var claims = ((JwtSecurityToken)rawValidatedToken).Claims;
                foreach(var claim in claims)
                {
                    if (claim.Type.Equals(AuthKey.USER_NAME))
                        data.AuthUsername = claim.Value;
                    if (claim.Type.Equals(AuthKey.USER_ID))
                        data.AuthUserId = Guid.Parse(claim.Value);
                    if (claim.Type.Equals(AuthKey.EXPIRE))
                        data.Expire = double.Parse(claim.Value) + (7 * 60 * 60);
                }
                return true;
            }catch (SecurityTokenExpiredException ex)
            {
                return false;
            }catch (SecurityTokenValidationException ex)
            {
                return false;
            }catch(ArgumentException ex)
            {
                return false;
            }
        }
    }
}
