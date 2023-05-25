using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;
using WEBAPI.Repositories;
using WEBAPI.Utility;
using WEBAPI.ViewModels.Accounts;
using WEBAPI.ViewModels.Employee;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper<Account, AccountVM> _accountMapper;
        private readonly IMapper<Employee, EmployeeVM> _emailMapper;
        private readonly IMapper<Account, ChangePasswordVM> _changePasswordMapper;
        private readonly IEmployeeRepository _employeeRepository;
        public AccountController(IAccountRepository accountRepository,
            IEmployeeRepository employeeRepository,
            IMapper<Account, AccountVM> accountMapper, 
            IMapper<Account, ChangePasswordVM> changePasswordMapper, 
            IMapper<Employee, EmployeeVM> emailMapper)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _accountMapper = accountMapper;
            _changePasswordMapper = changePasswordMapper;
            _emailMapper = emailMapper;

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

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            // Cek apakah email dan OTP valid
            var account = _employeeRepository.FindGuidByEmail(changePasswordVM.Email);
            var changePass = _accountRepository.ChangePasswordById(account, changePasswordVM);
            switch (changePass)
            {
                case 0:
                    return BadRequest("");
                case 1:
                    return Ok("Password has been changed successfully");
                case 2:
                    return BadRequest("Invalid OTP");
                case 3:
                    return BadRequest("OTP has already been used");
                /*case 4:
                    return BadRequest("OTP expired");*/
                case 5:
                    return BadRequest("Cek ...");

            }
            return null;

        }

       





    }
}
