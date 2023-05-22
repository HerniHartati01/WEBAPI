using Microsoft.AspNetCore.Mvc;
using WEBAPI.Contracts;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepositoryGeneric<Employee> _employeeRepository;

        public EmployeeController(IRepositoryGeneric<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _employeeRepository.GetAll();
            if (!employees.Any())
            {
                return NotFound();
            }

            return Ok(employees);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var employees = _employeeRepository.GetByGuid(guid);
            if (employees is null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            var result = _employeeRepository.Create(employee);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Employee employee)
        {
            var isUpdated = _employeeRepository.Update(employee);
            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _employeeRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
