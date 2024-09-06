using System;
using FitInsight.DataAccess;
using FitInsight.Interfaces;
using FitInsight.Models.ActivityModels;
using FitInsight.Models.ActivityModels.cs;
using Microsoft.EntityFrameworkCore;

namespace FitInsight.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ApplicationDbContext _context;

        public ActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Activity>> GetRecentActivitiesAsync(Guid userId, DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.Now.AddDays(-14);
            endDate ??= DateTime.Now;

            return await _context.Activities
                .Where(a => a.UserId == userId && a.StartTime >= startDate && a.StartTime <= endDate)
                .OrderByDescending(a => a.StartTime)
                .Include(a => a.ActivityLikes)
                .Include(a => a.ActivityComments)
                .ToListAsync();
        }


        public async Task<int> GetTotalActivitiesAsync(Guid userId)
        {
            return await _context.Activities.CountAsync(a => a.UserId == userId);
        }

        public async Task<Activity> GetActivityByIdAsync(int activityId)
        {
            return await _context.Activities
                .Include(a => a.ActivityLikes)
                .Where(a => a.ActivityId == activityId).FirstAsync();
        }

        public async Task AddLikeAsync(Guid userId, int activityId)
        {
            var like = new ActivityLike { UserId = userId, ActivityId = activityId };
            _context.ActivityLikes.Add(like);
            await _context.SaveChangesAsync();
        }

        public async Task AddCommentAsync(Guid userId, int activityId, string content)
        {
            var comment = new ActivityComment { UserId = userId, ActivityId = activityId, CommentText = content};
            _context.ActivityComments.Add(comment);
            await _context.SaveChangesAsync();
        }
    }
}

