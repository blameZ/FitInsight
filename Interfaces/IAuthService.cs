using System;
using FitInsight.Models;
using FitInsight.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace FitInsight.Interfaces
{
	public interface IAuthService
	{
        Task<IdentityResult> RegisterUserAsync(AccountViewModel model);
        Task<LoginResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}