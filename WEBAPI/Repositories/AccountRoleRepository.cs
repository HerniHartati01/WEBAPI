using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;

namespace WEBAPI.Repositories
{
    public class AccountRoleRepository : RepositoryGeneric<AccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(BookingMangementDbContext context) : base(context) { }

       
    }
}
