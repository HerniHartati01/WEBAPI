using WEBAPI.Models;
using WEBAPI.ViewModels.Accounts;

namespace WEBAPI.Contracts
{
    public interface IAccountRepository : IRepositoryGeneric<Account>
    {
        public int ChangePasswordById(Guid? employeeId, ChangePasswordVM changePasswordVM);
    }
}
