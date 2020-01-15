using ApiC.Domain.Dtos;
using ApiC.Domain.Entities;
using ApiC.Domain.Interfaces.Services.User;
using ApiC.Domain.Repository;
using ApiC.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ApiC.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository Repository;
        private SigningConfigurations SigningConfigurations { get; set; }
        private TokenConfiguration TokenConfiguration { get; set; }
        private IConfiguration Configuration { get; set; }

        public LoginService(IUserRepository repository, SigningConfigurations signingConfigurations, 
            TokenConfiguration tokenConfiguration, IConfiguration configuration)
        {
            Repository = repository;
            SigningConfigurations = signingConfigurations;
            TokenConfiguration = tokenConfiguration;
            Configuration = configuration;
        }

        public async Task<object> FindByLogin(LoginDto user)
        {
            var BaseUser = new UserEntity();


            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                BaseUser =  await Repository.FindByLogin(user.Email);
                if(BaseUser == null)
                {
                    return new
                    {
                        authenticated = false,
                        message = "Falha ao autenticar"
                    };
                }
                else
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(BaseUser.Email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                        }
                    );

                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(TokenConfiguration.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    string token = CreateToken(identity, createDate, expirationDate, handler);
                    return SuccessObject(createDate, expirationDate, token, user);
                }
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, 
            JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenConfiguration.Issuer,
                Audience = TokenConfiguration.Audience,
                SigningCredentials = SigningConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, LoginDto user)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                userName = user.Email,
                message = "Usuário Logado com sucesso"
            };
        }
    }
}
