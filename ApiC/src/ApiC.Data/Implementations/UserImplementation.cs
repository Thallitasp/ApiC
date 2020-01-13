using ApiC.Data.Context;
using ApiC.Data.Repository;
using ApiC.Domain.Entities;
using ApiC.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiC.Data.Implementations
{
    public class UserImplementation : BaseRepository<UserEntity>, IUserRepository
    {
        private DbSet<UserEntity> DataSet;

        public UserImplementation(MyContext context) : base(context)
        {
            DataSet = context.Set<UserEntity>();
        }

        public async Task<UserEntity> FindByLogin(string email)
        {
            return await DataSet.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }
    }
}
