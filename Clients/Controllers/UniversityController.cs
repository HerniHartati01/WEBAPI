using Clients.Models;
using Clients.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clients.Controllers
{
    [Authorize]
    public class UniversityController : Controller
    {
        private readonly IUniversityRepository repository;

        public UniversityController(IUniversityRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var universities = new List<University>();

            if (result.Data != null)
            {
                universities = result.Data?.Select(e => new University
                {
                    Guid = e.Guid,
                    Name = e.Name,
                    Code = e.Code,
                    CreatedDate = e.CreatedDate,
                    ModifiedDate = e.ModifiedDate,
                }).ToList();
            }

            return View(universities);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public async Task<IActionResult> Create(University university)
        {
            if (ModelState.IsValid)
            {
                var result = await repository.Post(university);
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

        [HttpPost]
       /* [ValidateAntiForgeryToken]*/
        public async Task<IActionResult> Edit(University university)
        {
            if (ModelState.IsValid)
            {
                var result = await repository.Put(university);
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
            var university = new University();
            if (result.Data?.Guid is null)
            {
                return View(university);
            }
            else
            {
                university.Guid = result.Data.Guid;
                university.Name = result.Data.Name;
                university.Code = result.Data.Code;
                university.CreatedDate = result.Data.CreatedDate;
                university.ModifiedDate = DateTime.Now;
            }

            return View(university);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var result = await repository.Get(guid);
            var university = new University();
            if (result.Data?.Guid is null)
            {
                return View(university);
            }
            else
            {
                university.Guid = result.Data.Guid;
                university.Name = result.Data.Name;
                university.Code = result.Data.Code;
            }
            return View(university);
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
