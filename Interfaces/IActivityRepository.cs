using System;
using FitInsight.Models.ActivityModels.cs;

namespace FitInsight.Interfaces
{
    public interface IActivityRepository
    {
        Task<List<Activity>> GetRecentActivitiesAsync(Guid userId, DateTime? startDate, DateTime? endDate);
        Task<int> GetTotalActivitiesAsync(Guid userId);
        Task<Activity> GetActivityByIdAsync(int activityId);
        Task AddLikeAsync(Guid userId, int activityId);
        Task AddCommentAsync(Guid userId, int activityId, string content);
    }
}

