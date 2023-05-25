using WEBAPI.Models;
using WEBAPI.ViewModels.Accounts;
using WEBAPI.ViewModels.Login;

namespace WEBAPI.Contracts
{
    public interface IAccountRepository : IRepositoryGeneric<Account>
    {
        public int ChangePasswordById(Guid? employeeId, ChangePasswordVM changePasswordVM);
        AccountEmpVM Login(LoginVM loginVM);
        int UpdateOTP(Guid? employeeId);
        int Register(RegisterVM registerVM);
    }
}
