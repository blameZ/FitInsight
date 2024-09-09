using System;
using FitInsight.Models.ActivityModels;
using FitInsight.Models.ActivityModels.cs;

namespace FitInsight.Interfaces
{
    public interface IActivityRepository
    {
        Task<List<Activity>> GetRecentActivitiesAsync(Guid userId, DateTime? startDate, DateTime? endDate);
        Task<int> GetTotalActivitiesAsync(Guid userId);
        Task<Activity> GetActivityByIdAsync(int activityId);
        Task AddLikeAsync(Guid userId, int activityId);
        Task<List<ActivityComment>> GetCommentsByActivityIdAsync(int activityId);
        Task AddCommentAsync(Guid userId, string userName, int activityId, string content);
        Task<ActivityComment> GetCommentByIdAsync(int commentId);
        Task DeleteCommentAsync(ActivityComment comment);
        Task UpdateActivityAsync(Activity activity);
    }
}

