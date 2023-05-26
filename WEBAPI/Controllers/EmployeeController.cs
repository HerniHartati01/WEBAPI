using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEBAPI.Contracts;
using WEBAPI.Models;
using WEBAPI.ViewModels.Accounts;
using WEBAPI.ViewModels.Educations;
using WEBAPI.ViewModels.Employee;
using WEBAPI.ViewModels.Others;
using WEBAPI.ViewModels.Universities;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IMapper<Employee, EmployeeVM> _employeeMapper;
        private readonly IMapper<Account, AccountVM> _accountMapper;
        private readonly IMapper<Education, EducationVM> _educationMapper;
        private readonly IMapper<University, UniversityVM> _universityMapper;

        public EmployeeController(IEmployeeRepository employeeRepository,
            IEducationRepository educationRepository,
            IUniversityRepository universityRepository,
            IMapper<Employee, EmployeeVM> employeeMapper, 
            IAccountRepository accountRepository, 
            IMapper<Education, EducationVM> educationMapper, 
            IMapper<University, UniversityVM> universityMapper,
             IMapper<Account, AccountVM> accountMapper)
        {
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
            _employeeMapper = employeeMapper;
            _educationMapper = educationMapper;
            _universityMapper = universityMapper;
            _accountMapper = accountMapper;
        }

        [HttpGet("GetAllMasterEmployee")]
        public IActionResult GetAll()
        {
            var masterEmployees = _employeeRepository.GetAllMasterEmployee();
            if (!masterEmployees.Any())
            {
                return NotFound(new ResponseVM<List<EmployeeVM>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }

            return Ok(new ResponseVM<IEnumerable<MasterEmployeeVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = masterEmployees
            });
        }

        [HttpGet("GetMasterEmployeeByGuid")]
        public IActionResult GetMasterEmployeeByGuid(Guid guid)
        {
            var masterEmployees = _employeeRepository.GetMasterEmployeeByGuid(guid);
            if (masterEmployees is null)
            {
                return NotFound(new ResponseVM<List<EmployeeVM>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }

            return Ok(new ResponseVM<MasterEmployeeVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = masterEmployees
            });
        }

        [HttpGet]
        public IActionResult GetEmploye()
        {
            var employees = _employeeRepository.GetAll();
            if (!employees.Any())
            {
                return NotFound(new ResponseVM<List<EmployeeVM>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }

            var data = employees.Select(_employeeMapper.Map).ToList();
            return Ok(new ResponseVM<List<EmployeeVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Not Found",
                Data = data
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var employees = _employeeRepository.GetByGuid(guid);
            if (employees is null)
            {
                return NotFound(new ResponseVM<EmployeeVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }
            var data = _employeeMapper.Map(employees);
            return Ok(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = data
            });
        }

        [HttpPost]
        public IActionResult Create(EmployeeVM employeeVM)
        {
            var employeeConverted = _employeeMapper.Map(employeeVM);
            var result = _employeeRepository.Create(employeeConverted);
            if (result is null)
            {
                return BadRequest(new ResponseVM<EmployeeVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess"
            });
        }

        [HttpPut]
        public IActionResult Update(EmployeeVM employeeVM)
        {
            var employeeConverted = _employeeMapper.Map(employeeVM);
            var isUpdated = _employeeRepository.Update(employeeConverted);
            if (!isUpdated)
            {
                return BadRequest(new ResponseVM<EmployeeVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess"
            });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _employeeRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseVM<EmployeeVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }

            return Ok(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succsess"
            });
        }

       

    }
}
