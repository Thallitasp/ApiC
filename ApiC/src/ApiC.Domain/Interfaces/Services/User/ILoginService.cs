using ApiC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiC.Domain.Interfaces.Services.User
{
    public interface ILoginService
    {
        Task<object> FindByLogin(UserEntity user); 
    }
}
