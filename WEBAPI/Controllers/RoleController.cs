using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEBAPI.Contracts;
using WEBAPI.Models;
using WEBAPI.ViewModels.Employee;
using WEBAPI.ViewModels.Others;
using WEBAPI.ViewModels.Roles;
using WEBAPI.ViewModels.Rooms;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper<Role, RoleVM> _roleMapper;

        public RoleController(IRoleRepository roleRepository,
            IMapper<Role, RoleVM> roleMapper)
        {
            _roleRepository = roleRepository;
            _roleMapper = roleMapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = _roleRepository.GetAll();
            if (!roles.Any())
            {
                return NotFound(new ResponseVM<List<RoleVM>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }
            var data = roles.Select(_roleMapper.Map).ToList();
            return Ok(new ResponseVM<List<RoleVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess",
                Data = data
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var roles = _roleRepository.GetByGuid(guid);
            if (roles is null)
            {
                return NotFound(new ResponseVM<RoleVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }
            var data = _roleMapper.Map(roles);
            return Ok(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Not Found",
                Data = data
            });
        }

        [HttpPost]
        public IActionResult Create(RoleVM rolesVM)
        {
            var roleConverted = _roleMapper.Map(rolesVM);
            var result = _roleRepository.Create(roleConverted);
            if (result is null)
            {
                return BadRequest(new ResponseVM<RoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess"
            });
        }

        [HttpPut]
        public IActionResult Update(RoleVM rolesVM)
        {
            var roleConverted = _roleMapper.Map(rolesVM);
            var isUpdated = _roleRepository.Update(roleConverted);
            if (!isUpdated)
            {
                return BadRequest(new ResponseVM<RoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess"
            });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _roleRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseVM<RoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess"
            });
        }

    }
}
