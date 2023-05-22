﻿using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;

namespace WEBAPI.Repositories
{
    public class AccountRoleRepository : IRepositoryGeneric<AccountRole>
    {
        private readonly BookingMangementDbContext _context;
        public AccountRoleRepository(BookingMangementDbContext context)
        {
            _context = context;
        }

        public AccountRole Create(AccountRole accountRole)
        {
            try
            {
                _context.Set<AccountRole>().Add(accountRole);
                _context.SaveChanges();
                return accountRole;
            }
            catch
            {
                return new AccountRole();
            }
        }


        public bool Update(AccountRole accountRole)
        {
            try
            {
                _context.Set<AccountRole>().Update(accountRole);
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
                var accountRole = GetByGuid(guid);
                if (accountRole == null)
                {
                    return false;
                }

                _context.Set<AccountRole>().Remove(accountRole);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<AccountRole> GetAll()
        {
            return _context.Set<AccountRole>().ToList();
        }


        public AccountRole? GetByGuid(Guid guid)
        {
            return _context.Set<AccountRole>().Find(guid);
        }
    }
}