using FitInsight.Interfaces;
using FitInsight.Models;
using FitInsight.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace FitInsight.Services
{
	public class AuthService : IAuthService
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AuthService> _logger;

		public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AuthService> logger)
		{
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
		}

        public async Task<IdentityResult> RegisterUserAsync(AccountViewModel model)
        {
            _logger.LogInformation($"User registration attempt for email: {model.Email}");
            
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation($"User {model.Email} successfully registered.");
            }
            else
            {
                _logger.LogWarning($"User registration failed for email: {model.Email}. Errors: {result.Errors}");
            }

            return result;
        }

        public async Task<LoginResult> LoginAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent:false, lockoutOnFailure: false);

            if(result.Succeeded)
            {
                _logger.LogInformation($"User {model.Email} logged in.");
                return new LoginResult { Succeeded = true };
            }
            else
            {
                var errors = new List<string>();
                if(result.IsLockedOut)
                {
                    _logger.LogWarning($"User account {model.Email} is locked out.");
                    errors.Add($"Konto {model.Email} jest zablokowane");
                    return new LoginResult { IsLockedOut = true, Errors = errors };
                }
                else
                {
                    _logger.LogWarning($"Invalid login attempt for {model.Email} user.");
                    errors.Add($"Nieudana próba logowania dla użytkownika {model.Email}.");
                    return new LoginResult { Errors = errors };
                }
            }
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
        }        
    }
}