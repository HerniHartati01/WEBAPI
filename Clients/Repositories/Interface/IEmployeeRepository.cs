using Clients.Models;

namespace Clients.Repositories.Interface
{
    public interface IEmployeeRepository : IGeneralRepository<Employee, Guid>
    {
    }
}
