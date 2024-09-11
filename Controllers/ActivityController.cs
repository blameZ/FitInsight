using System;
using FitInsight.Interfaces;
using FitInsight.Models;
using FitInsight.Models.ActivityModels;
using FitInsight.Models.ActivityModels.cs;
using FitInsight.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitInsight.Controllers
{
    [Authorize]
	public class ActivityController : Controller
	{
        private readonly ILogger<ActivityController> _logger;
        private readonly IActivityRepository _activityRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ActivityController(IActivityRepository activityRepository, ILogger<ActivityController> logger, UserManager<ApplicationUser> userManager)
		{
			_activityRepository = activityRepository;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddActivityViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var activity = new Activity
                {
                    ActivityType = model.ActivityType,
                    Distance = model.Distance,
                    Duration = model.Duration,
                    CaloriesBurned = model.CaloriesBurned,
                    StartTime = model.StartTime,
                    UserId = Guid.Parse(user.Id) 
                };

                await _activityRepository.AddActivityAsync(activity);
                return RedirectToAction("Index", "UserDashboard");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddLike(int activityId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var activity = await _activityRepository.GetActivityByIdAsync(activityId);                

            if (activity == null)
            {
                return NotFound();
            }

            Guid.TryParse(user.Id, out Guid userId);

            if (!activity.ActivityLikes.Any(l => l.UserId == userId))
            {
                await _activityRepository.AddLikeAsync(userId, activityId);                
            }

            return Json(new { likeCount = activity.ActivityLikes.Count });
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int activityId, string commentText)
            {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || string.IsNullOrWhiteSpace(commentText))
            {
                return BadRequest("Invalid user or comment text.");
            }

            var activity = await _activityRepository.GetActivityByIdAsync(activityId);

            if (activity == null)
            {
                return NotFound("Activity not found.");
            }

            Guid.TryParse(user.Id, out Guid userId);

            await _activityRepository.AddCommentAsync(userId, user.UserName, activityId, commentText);

            var comments = await _activityRepository.GetCommentsByActivityIdAsync(activityId);

            ViewBag.CurrentUserId = _userManager.GetUserId(User);

            return PartialView("_CommentsPartial", comments);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var comment = await _activityRepository.GetCommentByIdAsync(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            
            await _activityRepository.DeleteCommentAsync(comment);

            ViewBag.CurrentUserId = _userManager.GetUserId(User);

            return PartialView("_CommentsPartial", await _activityRepository.GetCommentsByActivityIdAsync(comment.ActivityId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TogglePrivacy(int activityId)
        {

            var user = await _userManager.GetUserAsync(User);
            Guid.TryParse(user.Id, out Guid userId);
            if(user == null)
            {
                _logger.LogWarning("User is not authenticated");
                return RedirectToAction("Login", "Authentication");
            }

            var activity = await _activityRepository.GetActivityByIdAsync(activityId);


            if (activity == null)
            {
                _logger.LogWarning($"Activity with ID {activityId} not found");
                return NotFound();
            }

            if(activity.UserId != userId)
            {
                _logger.LogWarning($"User {user.UserName} tried to change privacy settings for activity {activityId}");
                return Forbid();
            }

            activity.IsPrivate = !activity.IsPrivate;

            await _activityRepository.UpdateActivityAsync(activity);

            _logger.LogInformation($"User {user.UserName} changed privacy status of activity {activityId} to {(activity.IsPrivate ? "private" : "public")}");

            return RedirectToAction("Index", "UserDashboard");
        }

    }
}

