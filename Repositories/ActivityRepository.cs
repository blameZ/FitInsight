using System;
using FitInsight.DataAccess;
using FitInsight.Interfaces;
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
                .ToListAsync();
        }

        public async Task<int> GetTotalActivitiesAsync(Guid userId)
        {
            return await _context.Activities.CountAsync(a => a.UserId == userId);
        }
    }
}

