using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;

namespace WEBAPI.Repositories
{
    public class EmployeeRepository : RepositoryGeneric<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BookingMangementDbContext context) : base(context) { }

        
    }
}

