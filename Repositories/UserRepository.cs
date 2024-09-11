using System;
using FitInsight.DataAccess;
using FitInsight.Interfaces;
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
    }
}

