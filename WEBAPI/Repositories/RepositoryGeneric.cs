﻿using Microsoft.EntityFrameworkCore;
using System.Data;
using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;

namespace WEBAPI.Repositories
{
    public class RepositoryGeneric<TEntity> : IRepositoryGeneric<TEntity>
        where TEntity : class
    {

        protected readonly BookingMangementDbContext _context;
        public RepositoryGeneric(BookingMangementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity? GetByGuid(Guid guid)
        {
            var entity = _context.Set<TEntity>().Find(guid);
            _context.ChangeTracker.Clear();
            return entity;
        }

        public TEntity? Create(TEntity entity)
        {
            try
            {
                typeof(TEntity).GetProperty("CreatedDate")!
                    .SetValue(entity, DateTime.Now);
                typeof(TEntity).GetProperty("ModifiedDate")!
                    .SetValue(entity, DateTime.Now);

                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch
            {
                return default(TEntity);
            }
        }

        public bool Update(TEntity entity)
        {
            try
            {
                var guid =(Guid)typeof(TEntity).GetProperty("Guid")!
                    .GetValue(entity)!;
                var oldEntity = GetByGuid(guid);
                if (oldEntity == null)
                {
                    return false;
                }

                var getCreatedDate = typeof(TEntity).GetProperty("CreatedDate")!
                    .GetValue(oldEntity)!;

                typeof(TEntity).GetProperty("CreatedDate")!
                    .SetValue(entity, getCreatedDate);
                typeof(TEntity).GetProperty("ModifiedDate")!
                    .SetValue(entity, DateTime.Now);

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
