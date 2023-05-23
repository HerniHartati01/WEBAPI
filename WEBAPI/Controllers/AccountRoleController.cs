using Microsoft.AspNetCore.Mvc;
using WEBAPI.Contracts;
using WEBAPI.Models;
using WEBAPI.ViewModels.AccountRoles;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IMapper<AccountRole, AccountRoleVM> _accountRoleVM;

        public AccountRoleController(IAccountRoleRepository accountRoleRepository,
            IMapper<AccountRole, AccountRoleVM> accountRoleVM)
        {
            _accountRoleRepository = accountRoleRepository;
            _accountRoleVM = accountRoleVM;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var accountRoles = _accountRoleRepository.GetAll();
            if (!accountRoles.Any())
            {
                return NotFound();
            }

            var data = accountRoles.Select(_accountRoleVM.Map).ToList();
            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var accountRoles = _accountRoleRepository.GetByGuid(guid);
            if (accountRoles is null)
            {
                return NotFound();
            }

            var data = _accountRoleVM.Map(accountRoles);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create(AccountRoleVM accountRoleVM)
        {
            var accountRoleConverted = _accountRoleVM.Map(accountRoleVM);
            var result = _accountRoleRepository.Create(accountRoleConverted);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(AccountRoleVM accountRoleVM)
        {
            var accountRoleConverted = _accountRoleVM.Map(accountRoleVM);
            var isUpdated = _accountRoleRepository.Update(accountRoleConverted);
            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _accountRoleRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
