using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;

namespace WEBAPI.Repositories
{
    public class AccountRepository : RepositoryGeneric<Account>, IAccountRepository
    {
        public AccountRepository(BookingMangementDbContext context) : base(context) { }

        
    }
}
