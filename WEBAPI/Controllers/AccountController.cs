using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Net;
using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;
using WEBAPI.Repositories;
using WEBAPI.Utility;
using WEBAPI.ViewModels.Accounts;
using WEBAPI.ViewModels.Employee;
using WEBAPI.ViewModels.Login;
using WEBAPI.ViewModels.Others;
using WEBAPI.ViewModels.Rooms;
using WEBAPI.ViewModels.Universities;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper<Account, AccountVM> _accountMapper;
        private readonly IEmployeeRepository _employeeRepository;
        public AccountController(IAccountRepository accountRepository,
            IEmployeeRepository employeeRepository,
            IMapper<Account, AccountVM> accountMapper)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _accountMapper = accountMapper;
           

        }

        [HttpPost("login")]

        public IActionResult Login(LoginVM loginVM)
        {
            /*var account = _accountRepository.Login(loginVM);
            if (account is null)
            {
                return NotFound();
            }
            return Ok(account);*/

            var account = _accountRepository.Login(loginVM);

            if (account == null)
            {
                return NotFound(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Account not found"
                });
            }

            if (account.Password != loginVM.Password)
            {
                return BadRequest(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Password is invalid"
                });
            }

            return Ok(new ResponseVM<LoginVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess",
            });

        }

        [HttpPost("Register")]

        public IActionResult Register(RegisterVM registerVM)
        {

            var result = _accountRepository.Register(registerVM);
            switch (result)
            {
                case 0:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Registration failed"
                    });
                case 1:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Email already exists"
                    });
                case 2:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Phone number already exists"
                    });
                case 3:
                    return Ok(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "Registration success"
                    });
            }

            return Ok(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Registration success"
            });

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var accounts = _accountRepository.GetAll();
            if (!accounts.Any())
            {
                return NotFound(new ResponseVM<List<AccountVM>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not found"
                });
            }

            var data = accounts.Select(_accountMapper.Map).ToList();
            return Ok(new ResponseVM<List<AccountVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = data
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var accounts = _accountRepository.GetByGuid(guid);
            if (accounts is null)
            {
                return NotFound(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Account not found"
                });
            }

            var data = _accountMapper.Map(accounts);
            return Ok(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = data
            });
        }

        [HttpPost]
        public IActionResult Create(AccountVM accountVM)
        {
            var accountConverted = _accountMapper.Map(accountVM);
            var result = _accountRepository.Create(accountConverted);
            if (result is null)
            {
                return BadRequest(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess" 
            });
        }

        [HttpPut]
        public IActionResult Update(AccountVM accountVM)
        {
            var accountConverted = _accountMapper.Map(accountVM);
            var isUpdated = _accountRepository.Update(accountConverted);
            if (!isUpdated)
            {
                return BadRequest(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess"
            });
        }


        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _accountRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess"
            });
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
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Bad Request"
                    });
                case 1:
                    return Ok(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "Password has been changed successfully"
                    });
                case 2:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Invalid OTP"
                    });
                case 3:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "OTP has already been used"
                    });
                /*case 4:
                    return BadRequest("OTP expired");*/
                case 5:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "New password and confirm password are not the same"
                    });

            }
            return null;

        }

        [HttpPost("ForgotPassword" + "{email}")]
        public IActionResult UpdateResetPass(String email)
        {

            var getGuid = _employeeRepository.FindGuidByEmail(email);
            if (getGuid == null)
            {
                return NotFound(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Account not found"
                });
            }

            var isUpdated = _accountRepository.UpdateOTP(getGuid);

            switch (isUpdated)
            {
                case 0:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Bad Request"
                    });
                default:
                    var hasil = new AccountResetPasswordVM
                    {
                        Email = email,
                        OTP = isUpdated
                    };

                    MailService mailService = new MailService();
                    mailService.WithSubject("Kode OTP")
                               .WithBody("OTP anda adalah: " + isUpdated.ToString() + ".\n" +
                                         "Mohon kode OTP anda tidak diberikan kepada pihak lain" + ".\n" + "Terima kasih.")
                               .WithEmail(email)
                               .Send();

                    return Ok(new ResponseVM<AccountResetPasswordVM>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "Succsess"
                    });

            }



        }      
    }
}
