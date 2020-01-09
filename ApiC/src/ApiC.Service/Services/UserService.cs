using ApiC.Domain.Entities;
using ApiC.Domain.Interfaces;
using ApiC.Domain.Interfaces.Services.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiC.Service.Services
{
    public class UserService : IUserService
    {
        private IRepository<UserEntity> Repository;

        public UserService(IRepository<UserEntity> repository)
        {
            Repository = repository;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await Repository.DeleteAsync(id);
        }

        public async Task<UserEntity> Get(Guid id)
        {
            return await Repository.SelectAsync(id);
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await Repository.SelectAsync();
        }

        public async Task<UserEntity> Post(UserEntity user)
        {
            return await Repository.InsertAsync(user);
        }

        public async Task<UserEntity> Put(UserEntity user)
        {
            return await Repository.UpdateAsync(user);
        }
    }
}
