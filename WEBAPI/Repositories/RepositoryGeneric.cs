using Microsoft.EntityFrameworkCore;
using System.Data;
using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;

namespace WEBAPI.Repositories
{
    public class RepositoryGeneric<TEntity> : IRepositoryGeneric<TEntity>
        where TEntity : class
    {

        private readonly BookingMangementDbContext _context;
        public RepositoryGeneric(BookingMangementDbContext context)
        {
            _context = context;
        }

        /*public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToArrayAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid guid)
        {
            return await _context.Set<TEntity>().FindAsync(guid);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _context.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }


        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }*/

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity? GetByGuid(Guid guid)
        {
            return _context.Set<TEntity>().Find(guid);
        }

        public TEntity Create(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch
            {
                return default(TEntity);
            }
        }

        bool IRepositoryGeneric<TEntity>.Update(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Guid guid)
        {
            try
            {
                var entity = GetByGuid(guid);
                if (entity == null)
                {
                    return false;
                }

                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
