using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEBAPI.Contracts;
using WEBAPI.Models;
using WEBAPI.ViewModels.AccountRoles;
using WEBAPI.ViewModels.Accounts;
using WEBAPI.ViewModels.Others;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : BaseController <AccountRole, AccountRoleVM>
    {
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IMapper<AccountRole, AccountRoleVM> _accountRoleVM;

        public AccountRoleController(IAccountRoleRepository accountRoleRepository,
            IMapper<AccountRole, AccountRoleVM> accountRoleVM) : base (accountRoleRepository, accountRoleVM)
        {
            _accountRoleRepository = accountRoleRepository;
            _accountRoleVM = accountRoleVM;
        }

        

    }
}
