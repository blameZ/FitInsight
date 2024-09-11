using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FitInsight.Models;
using FitInsight.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FitInsight.Models.ViewModels;
using FitInsight.Interfaces;
using FitInsight.Repositories;

namespace FitInsight.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IActivityRepository _activityRepository;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(ILogger<HomeController> logger,IActivityRepository activityRepository, IUserRepository userRepository, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _activityRepository = activityRepository;
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
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

        var activities = await _activityRepository.GetAllPublicOrOwnActivitiesAsync(userId);

        var allActivities = new List<ActivityViewModel>();


        foreach (var activity in activities)
        {
            allActivities.Add(new ActivityViewModel
            {
                ActivityId = activity.ActivityId,
                UserId = activity.UserId,
                UserName = await _userRepository.GetUserNameByIdAsync(activity.UserId),
                IsPrivate = activity.IsPrivate,
                ActivityType = activity.ActivityType,
                Distance = activity.Distance,
                Duration = activity.Duration,
                CaloriesBurned = activity.CaloriesBurned,
                StartTime = activity.StartTime,
                ActivityComments = activity.ActivityComments,
                ActivityLikes = activity.ActivityLikes
            });
        }
        
        AssignDataToViewBag();

        var userInfo = CreateUserInfoViewModel(user, userId, allActivities);

        LogActivityResults(allActivities, userId);

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

    private void AssignDataToViewBag()
    {     
        ViewBag.CurrentUserId = _userManager.GetUserId(User);
    }

    private UserInfoViewModel CreateUserInfoViewModel(ApplicationUser user, Guid userId, List<ActivityViewModel> allActivities)
    {
        return new UserInfoViewModel
        {
            UserName = user.FirstName,
            UserId = userId,
            Email = user.Email,
            AllActivities = allActivities
        };
    }

    private void LogActivityResults(List<ActivityViewModel> allActivities, Guid userId)
    {
        if (allActivities.Count == 0)
        {
            _logger.LogInformation($"No recent activities found for user: {userId}");
        }
        else
        {
            _logger.LogInformation($"{allActivities.Count} recent activities retrieved for user: {userId}");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}