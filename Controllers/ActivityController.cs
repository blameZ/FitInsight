using System;
using FitInsight.Interfaces;
using FitInsight.Models;
using FitInsight.Models.ActivityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitInsight.Controllers
{
	public class ActivityController : Controller
	{
        private readonly IActivityRepository _activityRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ActivityController(IActivityRepository activityRepository, UserManager<ApplicationUser> userManager)
		{
			_activityRepository = activityRepository;
            _userManager = userManager;
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
    }
}

