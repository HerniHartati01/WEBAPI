using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using WEBAPI.Contracts;
using WEBAPI.Models;
using WEBAPI.ViewModels.Accounts;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper<Account, AccountVM> _accountMapper;

        public AccountController(IAccountRepository accountRepository, 
            IMapper<Account, AccountVM> accountMapper)
        {
            _accountRepository = accountRepository;
            _accountMapper = accountMapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var accounts = _accountRepository.GetAll();
            if (!accounts.Any())
            {
                return NotFound();
            }

            var data = accounts.Select(_accountMapper.Map).ToList();
            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var accounts = _accountRepository.GetByGuid(guid);
            if (accounts is null)
            {
                return NotFound();
            }

            var data = _accountMapper.Map(accounts);
            return Ok(accounts);
        }

        [HttpPost]
        public IActionResult Create(AccountVM accountVM)
        {
            var accountConverted = _accountMapper.Map(accountVM);
            var result = _accountRepository.Create(accountConverted);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(AccountVM accountVM)
        {
            var accountConverted = _accountMapper.Map(accountVM);
            var isUpdated = _accountRepository.Update(accountConverted);
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
