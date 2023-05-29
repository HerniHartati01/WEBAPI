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
    public class RoleController : BaseController <Role, RoleVM>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper<Role, RoleVM> _roleMapper;

        public RoleController(IRoleRepository roleRepository,
            IMapper<Role, RoleVM> roleMapper) : base (roleRepository, roleMapper)
        {
            _roleRepository = roleRepository;
            _roleMapper = roleMapper;
        }


    }
}
