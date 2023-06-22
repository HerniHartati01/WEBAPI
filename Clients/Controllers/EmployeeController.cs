using Clients.Models;
using Clients.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clients.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var employees = new List<Employee>();

            if (result.Data != null)
            {
                employees = result.Data?.Select(e => new Employee
                {
                    Guid = e.Guid,
                    Nik = e.Nik,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    BirthDate = e.BirthDate,
                    Gender = e.Gender,
                    HiringDate = e.HiringDate,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    CreatedDate = e.CreatedDate,
                    ModifiedDate = e.ModifiedDate,
                    
                }).ToList();
            }

            return View(employees);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var result = await repository.Post(employee);
                if (result.StatusCode == 200)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (result.StatusCode == 409 )
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        /* [ValidateAntiForgeryToken]*/
       
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var result = await repository.Put(employee);
                if (result.StatusCode == 200)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (result.StatusCode == 409)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid guid)
        {
            var result = await repository.Get(guid);
            var employee = new Employee();
            if (result.Data?.Guid is null)
            {
                return View(employee);
            }
            else
            {
                employee.Guid = result.Data.Guid;
                employee.Nik  = result.Data.Nik;
                employee.FirstName = result.Data.FirstName;
                employee.LastName = result.Data.LastName;
                employee.BirthDate = result.Data.BirthDate;
                employee.Gender = result.Data.Gender;
                employee.HiringDate = result.Data.HiringDate;
                employee.Email = result.Data.Email;
                employee.PhoneNumber = result.Data.PhoneNumber;
                employee.CreatedDate = result.Data.CreatedDate;
                employee.ModifiedDate = DateTime.Now;
            }

            return View(employee);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var result = await repository.Get(guid);
            var employee = new Employee();
            if (result.Data?.Guid is null)
            {
                return View(employee);
            }
            else
            {
                employee.Guid = result.Data.Guid;
                employee.Nik = result.Data.Nik;
                employee.FirstName = result.Data.FirstName;
                employee.LastName = result.Data.LastName;
                employee.BirthDate = result.Data.BirthDate;
                employee.Gender = result.Data.Gender;
                employee.HiringDate = result.Data.HiringDate;
                employee.Email = result.Data.Email;
                employee.PhoneNumber = result.Data.PhoneNumber;
                employee.CreatedDate = result.Data.CreatedDate;
                employee.ModifiedDate = DateTime.Now;
            }
            return View(employee);
        }

        [HttpPost]
       /* [ValidateAntiForgeryToken]*/
        public async Task<IActionResult> Remove(Guid guid)
        {
            var result = await repository.Delete(guid);
            if (result.StatusCode == 200)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
