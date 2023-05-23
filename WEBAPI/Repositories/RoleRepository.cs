using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;

namespace WEBAPI.Repositories
{
    public class RoleRepository :RepositoryGeneric<Role>, IRoleRepository
    {
        public RoleRepository(BookingMangementDbContext context) : base(context) { }

       
    }
}
