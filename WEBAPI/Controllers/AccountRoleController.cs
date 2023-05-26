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
                return NotFound(new ResponseVM<List<AccountRoleVM>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not found"

                });
            }

            var data = accountRoles.Select(_accountRoleVM.Map).ToList();
            return Ok(new ResponseVM<List<AccountRoleVM>>
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
            var accountRoles = _accountRoleRepository.GetByGuid(guid);
            if (accountRoles is null)
            {
                return NotFound(new ResponseVM<AccountRoleVM>
                {
                    Code =StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }

            var data = _accountRoleVM.Map(accountRoles);
            return Ok(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess",
                Data = data
            });
        }

        [HttpPost]
        public IActionResult Create(AccountRoleVM accountRoleVM)
        {
            var accountRoleConverted = _accountRoleVM.Map(accountRoleVM);
            var result = _accountRoleRepository.Create(accountRoleConverted);
            if (result is null)
            {
                return BadRequest(new ResponseVM<AccountRoleVM>
                {
                    Code=StatusCodes.Status400BadRequest,   
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success"
            });
        }

        [HttpPut]
        public IActionResult Update(AccountRoleVM accountRoleVM)
        {
            var accountRoleConverted = _accountRoleVM.Map(accountRoleVM);
            var isUpdated = _accountRoleRepository.Update(accountRoleConverted);
            if (!isUpdated)
            {
                return BadRequest(new ResponseVM<AccountRoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success"
            });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _accountRoleRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseVM<AccountRoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success"
            });
        }

    }
}
