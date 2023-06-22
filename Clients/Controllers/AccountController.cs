using Clients.Repositories.Interface;
using Clients.ViewModels.Others;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clients.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository repository;

        public AccountController(IAccountRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult LoginClient()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
       /* [ValidateAntiForgeryToken]*/
        public async Task<IActionResult> LoginClient(LoginVM loginVM)
        {
            var result = await repository.LoginClient(loginVM);
            if (result is null)
            {
                return RedirectToAction("Error", "Home");
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            else if (result.Code == 200)
            {
                HttpContext.Session.SetString("JWToken", result.Data);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Account/LoginClient");
        }


        [HttpGet]
        public IActionResult RegisterClient()
        {
            return View();
        }

        [HttpPost]
       /* [ValidateAntiForgeryToken]*/
        public async Task<IActionResult> RegisterClient(RegisterVM registerVM)
        {

            var result = await repository.RegisterClient(registerVM);
            if (result is null)
            {
                return RedirectToAction("Error", "Home");
            }
            else if (result.StatusCode == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                TempData["Error"] = $"Something Went Wrong! - {result.Message}!";
                return View();
            }
            else if (result.StatusCode == 200)
            {
                TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("LoginClient", "Account");
        }
    }
}
