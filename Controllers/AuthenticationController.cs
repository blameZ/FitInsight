using FitInsight.Interfaces;
using FitInsight.Models;
using FitInsight.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FitInsight.Controllers
{
	public class AuthenticationController : Controller
	{
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthService _service;

        public AuthenticationController(ILogger<HomeController> logger, IAuthService service)
		{
			_logger = logger;
            _service = service;
		}

        public IActionResult Login()
        {
            return View(); 
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.RegisterUserAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Authentication");
                }
                foreach (var error in result.Errors)
                {
                    if(error.Code == "DuplicateUserName")
                    {
                        ModelState.AddModelError(string.Empty, $"Adres e-mail {model.Email} jest już wykorzystywany.");
                        return View(model);
                    }
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _service.LoginAsync(model);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if(result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "Konto jest zablokowane.");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Dane logowania są nieprawidłowe.");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _service.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}