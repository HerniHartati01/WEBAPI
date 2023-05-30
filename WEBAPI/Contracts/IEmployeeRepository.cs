using WEBAPI.Models;
using WEBAPI.ViewModels.Employee;

namespace WEBAPI.Contracts
{
    public interface IEmployeeRepository : IRepositoryGeneric<Employee>
    {
        public Guid? FindGuidByEmail(string email);
        IEnumerable<MasterEmployeeVM> GetAllMasterEmployee();

        MasterEmployeeVM? GetMasterEmployeeByGuid(Guid guid);
        /*int CreateWithValidate(Employee employee);*/

        bool CheckEmailAndPhoneAndNik (string value);
        public Employee GetByEmail(string email);
    }
}
