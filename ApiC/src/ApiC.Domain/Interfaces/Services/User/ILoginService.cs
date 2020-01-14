using ApiC.Domain.Dtos;
using System.Threading.Tasks;

namespace ApiC.Domain.Interfaces.Services.User
{
    public interface ILoginService
    {
        Task<object> FindByLogin(LoginDto user); 
    }
}
