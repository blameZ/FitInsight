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

            
            return PartialView("_CommentsPartial", await _activityRepository.GetCommentsByActivityIdAsync(comment.ActivityId));
        }


    }
}

