using Microsoft.AspNetCore.Mvc;
using WEBAPI.Contracts;
using WEBAPI.Models;
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
                return NotFound();
            }
            var data = roles.Select(_roleMapper.Map).ToList();
            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var roles = _roleRepository.GetByGuid(guid);
            if (roles is null)
            {
                return NotFound();
            }
            var data = _roleMapper.Map(roles);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create(RoleVM rolesVM)
        {
            var roleConverted = _roleMapper.Map(rolesVM);
            var result = _roleRepository.Create(roleConverted);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(RoleVM rolesVM)
        {
            var roleConverted = _roleMapper.Map(rolesVM);
            var isUpdated = _roleRepository.Update(roleConverted);
            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _roleRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
