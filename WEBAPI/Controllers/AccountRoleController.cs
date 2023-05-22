using Microsoft.AspNetCore.Mvc;
using WEBAPI.Contracts;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : ControllerBase
    {
        private readonly IRepositoryGeneric<AccountRole> _accountRoleRepository;

        public AccountRoleController(IRepositoryGeneric<AccountRole> accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var accountRoles = _accountRoleRepository.GetAll();
            if (!accountRoles.Any())
            {
                return NotFound();
            }

            return Ok(accountRoles);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var accountRoles = _accountRoleRepository.GetByGuid(guid);
            if (accountRoles is null)
            {
                return NotFound();
            }

            return Ok(accountRoles);
        }

        [HttpPost]
        public IActionResult Create(AccountRole accountRoles)
        {
            var result = _accountRoleRepository.Create(accountRoles);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(AccountRole accountRole)
        {
            var isUpdated = _accountRoleRepository.Update(accountRole);
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
