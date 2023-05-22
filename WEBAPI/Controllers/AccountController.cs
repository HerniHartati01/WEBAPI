using Microsoft.AspNetCore.Mvc;
using WEBAPI.Contracts;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IRepositoryGeneric<Account> _accountRepository;

        public AccountController(IRepositoryGeneric<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var accounts = _accountRepository.GetAll();
            if (!accounts.Any())
            {
                return NotFound();
            }

            return Ok(accounts);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var accounts = _accountRepository.GetByGuid(guid);
            if (accounts is null)
            {
                return NotFound();
            }

            return Ok(accounts);
        }

        [HttpPost]
        public IActionResult Create(Account account)
        {
            var result = _accountRepository.Create(account);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Account account)
        {
            var isUpdated = _accountRepository.Update(account);
            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _accountRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
