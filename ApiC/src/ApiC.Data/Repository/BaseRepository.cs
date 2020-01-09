using ApiC.Data.Context;
using ApiC.Domain.Entities;
using ApiC.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiC.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MyContext Context;
        private DbSet<T> DataSet;
        public BaseRepository(MyContext myContex)
        {
            Context = myContex;
            DataSet = Context.Set<T>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await DataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (result == null)
                {
                    return false;
                }

                DataSet.Remove(result);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<T> InsertAsync(T entity)
        {
            try
            {
                if(entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }

                entity.CreateAt = DateTime.UtcNow;
                DataSet.Add(entity);

                await Context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                throw e;
            }

            return entity;
        }

        public async Task<bool> ExistAsync (Guid id)
        {
            return await DataSet.AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await DataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await DataSet.ToListAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                var result = await DataSet.SingleOrDefaultAsync(p => p.Id.Equals(entity.Id));
                if(result == null)
                {
                    return null;
                }

                entity.UpdateAt = DateTime.UtcNow;
                entity.CreateAt = result.CreateAt;

                Context.Entry(result).CurrentValues.SetValues(entity);
                await Context.SaveChangesAsync();
            }
            catch(Exception e)
            {

                throw e;
            }

            return entity;
        }
    }
}
