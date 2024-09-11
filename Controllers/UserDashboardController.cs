using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FitInsight.Interfaces;
using FitInsight.Models;
using FitInsight.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitInsight.Controllers
{
    public class UserDashboardController : Controller
    {
        private readonly ILogger<UserDashboardController> _logger;
        private readonly IActivityRepository _activityRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserDashboardController(ILogger<UserDashboardController> logger, IActivityRepository activityRepository, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _activityRepository = activityRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            if (!User.Identity.IsAuthenticated)
            {
                _logger.LogInformation("Unauthenticated user accessed the Index action.");
                return View();
            }

            var user = await GetAuthenticatedUserAsync();
            if (user == null) return RedirectToAction("Login", "Authentication");

            if (!Guid.TryParse(user.Id, out Guid userId))
            {
                _logger.LogWarning($"User ID could not be parsed for user {user.UserName}");
                return RedirectToAction("Login", "Authentication");
            }

            startDate ??= DateTime.Now.AddDays(-14);
            endDate ??= DateTime.Now;

            var recentActivities = await GetRecentActivitiesAsync(userId, startDate.Value, endDate.Value);
            var totalActivities = await _activityRepository.GetTotalActivitiesAsync(userId);

            AssignDataToViewBag(recentActivities);

            var userInfo = CreateUserInfoViewModel(user, userId, recentActivities, totalActivities);

            LogActivityResults(recentActivities, userId);

            return View(userInfo);
        }

        private async Task<ApplicationUser> GetAuthenticatedUserAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("Failed to retrieve the authenticated user");
            }
            return user;
        }

        private async Task<List<FitInsight.Models.ActivityModels.cs.Activity>> GetRecentActivitiesAsync(Guid userId, DateTime startDate, DateTime endDate)
        {
            return await _activityRepository.GetRecentActivitiesAsync(userId, startDate, endDate);
        }

        private void AssignDataToViewBag(List<FitInsight.Models.ActivityModels.cs.Activity> recentActivities)
        {
            var activityPieData = recentActivities
                .GroupBy(a => a.ActivityType)
                .Select(g => new { ActivityType = g.Key, Count = g.Count() })
                .ToList();

            ViewBag.ActivityTypes = recentActivities.Select(a => a.ActivityType).ToArray();
            ViewBag.CaloriesBurned = recentActivities.Select(a => a.CaloriesBurned).ToArray();
            ViewBag.PieActivityTypes = activityPieData.Select(a => a.ActivityType).ToArray();
            ViewBag.PieActivityCounts = activityPieData.Select(a => a.Count).ToArray();
            ViewBag.CurrentUserId = _userManager.GetUserId(User);
        }

        private UserInfoViewModel CreateUserInfoViewModel(ApplicationUser user, Guid userId, List<FitInsight.Models.ActivityModels.cs.Activity> recentActivities, int totalActivities)
        {
            return new UserInfoViewModel
            {
                UserName = user.UserName,
                UserId = userId,
                Email = user.Email,
                TotalActivities = totalActivities,
                RecentActivities = recentActivities
            };
        }

        private void LogActivityResults(List<FitInsight.Models.ActivityModels.cs.Activity> recentActivities, Guid userId)
        {
            if (recentActivities.Count == 0)
            {
                _logger.LogInformation($"No recent activities found for user: {userId}");
            }
            else
            {
                _logger.LogInformation($"{recentActivities.Count} recent activities retrieved for user: {userId}");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

