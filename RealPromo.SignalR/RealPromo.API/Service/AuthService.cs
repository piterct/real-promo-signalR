using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using RealPromo.API.ViewModels;
using RealPromo.API.Settings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace RealPromo.API.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppSettings _appSettings;
        public AuthService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<LoginResponseViewModel> GerarJwt(string email, string password)
        {
            var user = await GetUser(email, password);
            var claims = new List<Claim>();
            var userRoles = user.Claims;

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole.Value));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        public async Task<UserTokenViewModel> GetUser(string email, string password)
        {
            if (email == "signalR@teste.com.br" && password == "signalRTeste")
            {
                return await Task.FromResult(new UserTokenViewModel
                {
                    Id = "EFB17D2F-8D5E-4DC5-89B6-C3FF9856A949",
                    Email = email,
                    Claims = new List<ClaimViewModel>() { new ClaimViewModel { Type = "Admin", Value = "Editar" },
                        new ClaimViewModel { Type = "Admin", Value = "Adicionar" },
                        new ClaimViewModel { Type = "Admin", Value = "Excluir" } }
                });
            }

            return null;
        }

        
    }
}
