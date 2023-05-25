using WEBAPI.Models;

namespace WEBAPI.Contracts
{
    public interface IEmployeeRepository : IRepositoryGeneric<Employee>
    {
        public Guid? FindGuidByEmail(string email);
    }
}
