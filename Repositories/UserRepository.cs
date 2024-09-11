using System;
using FitInsight.DataAccess;
using FitInsight.Interfaces;
using FitInsight.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace FitInsight.Repositories
{
	public class UserRepository : IUserRepository
	{
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetUserNameByIdAsync(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.ToString());

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            return user.UserName;
        }

        public async Task AddUserWeightHistoryAsync(string userId, float weight)
        {
            _context.UserWeightHistories.Add(new UserWeightHistory
            {
                UserId = userId,
                Weight = weight,
                Date = DateTime.Now
            });

            await _context.SaveChangesAsync();
        }

        public async Task<List<UserWeightHistory>> GetUserWeightHistoriesByUserIdAsync(string userId)
        {
            return await _context.UserWeightHistories
                .Where(wh => wh.UserId == userId)
                .OrderBy(wh => wh.Date)
                .ToListAsync();
        }
    }
}

