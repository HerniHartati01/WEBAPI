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
    public class AccountController : BaseController <Account, AccountVM>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper<Account, AccountVM> _accountMapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmailService _emailService;
        public AccountController(IAccountRepository accountRepository,
            IEmployeeRepository employeeRepository,
            IMapper<Account, AccountVM> accountMapper, 
            IEmailService emailService) : base (accountRepository, accountMapper)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _accountMapper = accountMapper;
            _emailService = emailService;
           

        }

        [HttpPost("login")]

        public IActionResult Login(LoginVM loginVM)
        {
           

            var account = _accountRepository.Login(loginVM);

            if (account == null)
            {
                return NotFound(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Account not found",
                    Data = null
                    
                });
            }

            var validatePass = Hashing.ValidatePassword(loginVM.Password, account.Password);
            if (validatePass is false)
            {
                return BadRequest(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Password is invalid",
                    Data = null

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
                        Message = "Bad Request",

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


                    // Send Email Using DI
                    _emailService.SetEmail(email)
                             .SetSubject("Forgot Password")
                             .SetBody($"Your OTP is {isUpdated}")
                             .SendEmailAsync();


                    return Ok(new ResponseVM<AccountResetPasswordVM>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "Succsess",
                        Data = hasil
                    });

            }



        }      
    }
}
