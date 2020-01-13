using ApiC.Domain.Entities;
using ApiC.Domain.Interfaces.Services.User;
using ApiC.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiC.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository Repository;

        public LoginService(IUserRepository repository)
        {
            Repository = repository;
        }

        public async Task<object> FindByLogin(UserEntity user)
        {
            var BaseUser = new UserEntity();
            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                return await Repository.FindByLogin(user.Email);
            }
            else
            {
                return null; 
            }
        }
    }
}
