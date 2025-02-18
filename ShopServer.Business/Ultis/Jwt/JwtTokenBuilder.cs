using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Ultis.Jwt
{
    public sealed class JwtTokenBuilder
    {
        private SecurityKey SecurityKey = null;
        private string Subject = "";
        private string Issuer = "";
        private string Audience = "";
        private Dictionary<string, string> claims = new Dictionary<string, string>();
        private double expiryInMinutes = 0;
        public JwtTokenBuilder AddSecurityKey(SecurityKey securityKey)
        {
            this.SecurityKey = securityKey;
            return this;
        }
        public JwtTokenBuilder AddSubject(string subject)
        {
            this.Subject = subject;
            return this;
        }
        public JwtTokenBuilder AddIssuer(string issuer)
        {
            this.Issuer = issuer;
            return this;
        }
        public JwtTokenBuilder AddAudience(string audience)
        {
            this.Audience = audience;
            return this;
        }

        public JwtTokenBuilder AddClaim(string type, string value)
        {
            this.claims.Add(type, value);
            return this;
        }

        public JwtTokenBuilder AddClaims(Dictionary<string, string> claims)
        {
            this.claims.Union(claims);
            return this;
        }

        public JwtTokenBuilder AddExpiry(int expiryInMinutes)
        {
            this.expiryInMinutes = expiryInMinutes;
            return this;
        }

        public JwtToken Build()
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.Sub, Subject),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }
            .Union(this.claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(
                              issuer: this.Issuer,
                              audience: this.Audience,
                              claims: claims,
                              expires: DateTime.Now.AddMinutes(expiryInMinutes),
                              signingCredentials: new SigningCredentials(
                                                        this.SecurityKey,
                                                        SecurityAlgorithms.HmacSha256));

            return new JwtToken(token);
        }

        #region " private "

        private void EnsureArguments()
        {
            if (this.SecurityKey == null)
                throw new ArgumentNullException("Security Key");

            if (string.IsNullOrEmpty(this.Subject))
                throw new ArgumentNullException("Subject");

            if (string.IsNullOrEmpty(this.Issuer))
                throw new ArgumentNullException("Issuer");

            if (string.IsNullOrEmpty(this.Audience))
                throw new ArgumentNullException("Audience");
        }

        #endregion
    }
}
